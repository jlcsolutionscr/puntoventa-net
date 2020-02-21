Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmCuentaBanco
#Region "Variables"
    Public intIdCuenta As Integer
    Private datos As CuentaBanco
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        ElseIf txtCodigo.Text = "" Then
            pCampo = "Código"
            Return False
        ElseIf txtSaldo.Text = "" Then
            pCampo = "Saldo"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmCuentaBanco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmCuentaBanco_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdCuenta > 0 Then
            Try
                datos = Await Puntoventa.ObtenerCuentaBanco(intIdCuenta, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    MessageBox.Show("La cuenta bancaria seleccionada no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                    Exit Sub
                End If
                txtIdCuenta.Text = datos.IdCuenta
                txtCodigo.Text = datos.Codigo
                txtDescripcion.Text = datos.Descripcion
                txtSaldo.Text = FormatNumber(datos.Saldo, 2)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End Try
        Else
            datos = New CuentaBanco
            txtSaldo.Text = FormatNumber(0, 2)
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
            Exit Sub
        End If
        If datos.IdCuenta = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Codigo = txtCodigo.Text
        datos.Descripcion = txtDescripcion.Text
        datos.Saldo = txtSaldo.Text
        Try
            If datos.IdCuenta = 0 Then
                Await Puntoventa.AgregarCuentaBanco(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarCuentaBanco(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub Saldo_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtSaldo.Validated
        If txtSaldo.Text = "" Then txtSaldo.Text = "0"
        txtSaldo.Text = FormatNumber(txtSaldo.Text, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSaldo.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class