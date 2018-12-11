Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmSeguridad
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
    Private usuario As Usuario
    Private empresa As Empresa
#End Region

#Region "Eventos Controles"
    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Try
            usuario = servicioMantenimiento.ValidarUsuario(txtIdentificacion.Text, TxtUsuario.Text, TxtClave.Text, FrmMenuPrincipal.strAppThumptPrint)
            empresa = servicioMantenimiento.ObtenerEmpresa(txtIdentificacion.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        FrmMenuPrincipal.usuarioGlobal = usuario
        FrmMenuPrincipal.empresaGlobal = empresa
        Close()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Close()
    End Sub
#End Region
End Class