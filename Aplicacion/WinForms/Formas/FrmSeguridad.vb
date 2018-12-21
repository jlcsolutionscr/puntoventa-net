
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmSeguridad
#Region "Variables"
#End Region

#Region "Eventos Controles"
    Private Sub FrmSeguridad_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtIdentificacion.Text = FrmMenuPrincipal.strIdentificacion
    End Sub
    Private Async Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Dim usuario As Usuario = Nothing
        Try
            CmdAceptar.Enabled = False
            usuario = Await ClienteWCF.ValidarCredenciales(txtIdentificacion.Text, TxtUsuario.Text, TxtClave.Text)
        Catch ex As Exception
            CmdAceptar.Enabled = True
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        FrmMenuPrincipal.usuarioGlobal = usuario
        FrmMenuPrincipal.empresaGlobal = usuario.Empresa
        Close()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Close()
    End Sub

    Private Sub TxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles TxtUsuario.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" And txtIdentificacion.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub

    Private Sub TxtClave_TextChanged(sender As Object, e As EventArgs) Handles TxtClave.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" And txtIdentificacion.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub

    Private Sub txtIdentificacion_TextChanged(sender As Object, e As EventArgs) Handles txtIdentificacion.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" And txtIdentificacion.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub
#End Region
End Class