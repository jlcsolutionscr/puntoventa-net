Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmProveedor
#Region "Variables"
    Public intIdProveedor As Integer
    Private datos As Proveedor
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtIdentificacion.Text = "" Then
            pCampo = "Identificación"
            Return False
        ElseIf txtNombre.Text = "" Then
            pCampo = "Nombre"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmProveedor_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdProveedor > 0 Then
            Try
                datos = Await ClienteFEWCF.ObtenerProveedor(intIdProveedor)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El proveedor seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdProveedor.Text = datos.IdProveedor
            txtIdentificacion.Text = datos.Identificacion
            txtNombre.Text = datos.Nombre
            txtDireccion.Text = datos.Direccion
            txtTelefono1.Text = datos.Telefono1
            txtTelefono2.Text = datos.Telefono2
            txtFax.Text = datos.Fax
            txtCorreo.Text = datos.Correo
            txtPlazo.Text = IIf(datos.PlazoCredito Is Nothing, "0", datos.PlazoCredito)
            txtContacto1.Text = datos.Contacto1
            txtTelContacto1.Text = datos.TelCont1
            txtContacto2.Text = datos.Contacto2
            txtTelContacto2.Text = datos.TelCont2
        Else
            datos = New Proveedor
            txtPlazo.Text = "0"
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
        If datos.IdProveedor = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Identificacion = txtIdentificacion.Text
        datos.Nombre = txtNombre.Text
        datos.Direccion = txtDireccion.Text
        datos.Telefono1 = txtTelefono1.Text
        datos.Telefono2 = txtTelefono2.Text
        datos.Fax = txtFax.Text
        datos.Correo = txtCorreo.Text
        datos.PlazoCredito = txtPlazo.Text
        datos.Contacto1 = txtContacto1.Text
        datos.TelCont1 = txtTelContacto1.Text
        datos.Contacto2 = txtContacto2.Text
        datos.TelCont2 = txtTelContacto2.Text
        Try
            If datos.IdProveedor = 0 Then
                Dim strIdProveedor As String = Await ClienteFEWCF.AgregarProveedor(datos)
                txtIdProveedor.Text = strIdProveedor
            Else
                Await ClienteFEWCF.ActualizarProveedor(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub TopeCredito_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPlazo.Validated
        If txtPlazo.Text = "" Then txtPlazo.Text = "0"
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPlazo.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub
#End Region
End Class