Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.ClienteWCF

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
    Private Async Sub FrmBancoAdquiriente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdBanco > 0 Then
            Try
                datos = Await PuntoventaWCF.ObtenerBancoAdquiriente(intIdBanco)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El banco adquiriente seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                Dim strIdBanco As String = Await PuntoventaWCF.AgregarBancoAdquiriente(datos)
                txtIdBanco.Text = strIdBanco
            Else
                Await PuntoventaWCF.ActualizarBancoAdquiriente(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
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

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtRetencion.KeyPress, txtComision.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class