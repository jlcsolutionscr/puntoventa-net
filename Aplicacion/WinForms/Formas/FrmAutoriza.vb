Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmAutoriza
#Region "Variables"
    Private SqlSentence, strClave, strEmpresa, strFechaVence As String
    Private usuarioAutorizador As Usuario
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Eventos Controles"
    Private Sub FrmAutoriza_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Try
            usuarioAutorizador = servicioMantenimiento.ValidarUsuario(FrmMenuPrincipal.empresaGlobal.IdEmpresa, TxtUsuario.Text, TxtClave.Text, FrmMenuPrincipal.strAppThumptPrint)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtUsuario.Text = ""
            TxtClave.Text = ""
            TxtUsuario.Focus()
            Exit Sub
        End Try
        Close()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        TxtUsuario.Text = ""
        TxtClave.Text = ""
        Close()
    End Sub
#End Region
End Class