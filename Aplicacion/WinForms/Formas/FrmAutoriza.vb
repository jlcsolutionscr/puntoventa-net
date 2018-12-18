Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmAutoriza
#Region "Variables"
    Private SqlSentence, strClave, strEmpresa, strFechaVence As String
    Private usuarioAutorizador As Usuario
#End Region

#Region "Eventos Controles"
    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Try
            'usuarioAutorizador = servicioMantenimiento.ValidarUsuario(TxtUsuario.Text, TxtClave.Text, FrmMenuPrincipal.strThumbprint)
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