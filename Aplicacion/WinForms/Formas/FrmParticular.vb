Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmParticular
#Region "Variables"
    Public intIdParticular As Integer
    Private datos As Particular
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtNombre.Text = "" Then
            pCampo = "Nombre"
            Return False
        Else
            Return True
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmParticular_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdParticular > 0 Then
            Try
                'datos = servicioMantenimiento.ObtenerParticular(intIdParticular)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El particular seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdParticular.Text = datos.IdParticular
            txtIdentificacion.Text = datos.Identificacion
            txtNombre.Text = datos.Nombre
            txtDireccion.Text = datos.Direccion
            txtTelefono.Text = datos.Telefono
            txtCelular.Text = datos.Celular
            txtFax.Text = datos.Fax
            txtEMail.Text = datos.EMail
        Else
            datos = New Particular
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If datos.IdParticular = 0 Then
            datos.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Identificacion = txtIdentificacion.Text
        datos.Nombre = txtNombre.Text
        datos.Direccion = txtDireccion.Text
        datos.Telefono = txtTelefono.Text
        datos.Celular = txtCelular.Text
        datos.Fax = txtFax.Text
        datos.EMail = txtEMail.Text
        Try
            If datos.IdParticular = 0 Then
                'servicioMantenimiento.AgregarParticular(datos)
                txtIdParticular.Text = datos.IdParticular
            Else
                'servicioMantenimiento.ActualizarParticular(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub Identificacion_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtIdentificacion.Validated
        If txtIdentificacion.Text <> "" Then
            Dim particular As Particular = Nothing 'servicioMantenimiento.ValidaIdentificacionParticular(txtIdentificacion.Text)
            If particular IsNot Nothing Then
                If (datos IsNot Nothing And datos.Identificacion <> txtIdentificacion.Text) Or datos Is Nothing Then
                    MessageBox.Show("La identificación ingresada ya se encuentra registrada en la base de datos de particulares del sistema.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    txtIdParticular.Text = particular.IdParticular
                    txtNombre.Text = particular.Nombre
                    txtDireccion.Text = particular.Direccion
                    txtTelefono.Text = particular.Telefono
                    txtCelular.Text = particular.Celular
                    txtFax.Text = particular.Fax
                    txtEMail.Text = datos.EMail
                End If
            End If
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class