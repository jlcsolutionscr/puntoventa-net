Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmBancoAdquiriente
#Region "Variables"
    Public intIdBanco As Integer
    Private datos As BancoAdquiriente
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtCodigo.Text = "" Then
            pCampo = "Código"
            Return False
        ElseIf txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        ElseIf txtRetencion.Text = "" Then
            pCampo = "Porcentaje de retención"
            Return False
        ElseIf txtComision.Text = "" Then
            pCampo = "Porcentaje de comisión"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmBancoAdquiriente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub

    Private Async Sub FrmBancoAdquiriente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdBanco > 0 Then
            Try
                datos = Await Puntoventa.ObtenerBancoAdquiriente(intIdBanco, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El banco adquiriente seleccionado no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdBanco.Text = datos.IdBanco
            txtCodigo.Text = datos.Codigo
            txtDescripcion.Text = datos.Descripcion
            txtRetencion.Text = datos.PorcentajeRetencion
            txtComision.Text = datos.PorcentajeComision
        Else
            datos = New BancoAdquiriente
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            btnGuardar.Enabled = True
            Exit Sub
        End If
        If datos.IdBanco = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Codigo = txtCodigo.Text
        datos.Descripcion = txtDescripcion.Text
        datos.PorcentajeRetencion = txtRetencion.Text
        datos.PorcentajeComision = txtComision.Text
        Try
            If datos.IdBanco = 0 Then
                Await Puntoventa.AgregarBancoAdquiriente(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarBancoAdquiriente(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
            MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        Close()
    End Sub

    Private Sub txtRetencion_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtRetencion.Validated
        If txtRetencion.Text = "" Then txtRetencion.Text = "0"
        txtRetencion.Text = FormatNumber(txtRetencion.Text, 2)
    End Sub

    Private Sub txtComision_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtComision.Validated
        If txtComision.Text = "" Then txtComision.Text = "0"
        txtComision.Text = FormatNumber(txtComision.Text, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtRetencion.KeyPress, txtComision.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class