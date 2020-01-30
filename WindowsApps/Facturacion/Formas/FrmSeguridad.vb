Public Class FrmSeguridad
#Region "Variables"
#End Region

#Region "Eventos Controles"
    Private Sub FrmSeguridad_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            cboEmpresa.DataSource = FrmPrincipal.listaEmpresa
            cboEmpresa.ValueMember = "Id"
            cboEmpresa.DisplayMember = "Descripcion"
            CmdAceptar.Enabled = True
            CmdCancelar.Enabled = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub
    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        FrmPrincipal.strCodigoUsuario = TxtUsuario.Text
        FrmPrincipal.strContrasena = TxtClave.Text
        FrmPrincipal.strIdEmpresa = cboEmpresa.SelectedValue.ToString()
        Close()
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FrmPrincipal.bolSalir = True
        Close()
    End Sub

    Private Sub TxtUsuario_TextChanged(sender As Object, e As EventArgs) Handles TxtUsuario.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub

    Private Sub TxtUsuario_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtUsuario.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            e.Handled = True
        End If
    End Sub

    Private Sub TxtClave_TextChanged(sender As Object, e As EventArgs) Handles TxtClave.TextChanged
        If TxtUsuario.Text <> "" And TxtClave.Text <> "" Then
            CmdAceptar.Enabled = True
        Else
            CmdAceptar.Enabled = False
        End If
    End Sub

    Private Sub TxtClave_KeyPress(sender As Object, e As KeyPressEventArgs) Handles TxtClave.KeyPress
        If Asc(e.KeyChar) = Keys.Space Then
            e.Handled = True
        End If
    End Sub
#End Region
End Class