Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.Utilities

Public Class FrmActualizarClave
#Region "Variables"
    Private strKey As String
#End Region

#Region "Eventos Controles"
    Private Async Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If TxtClave1.Text = TxtClave2.Text Then
            Try
                Dim strClaveEncriptada = Utilitario.EncriptarDatos(TxtClave1.Text, FrmPrincipal.strKey)
                Dim usuario As Usuario = Await Puntoventa.ActualizarClaveUsuario(FrmPrincipal.usuarioGlobal.IdUsuario, strClaveEncriptada, FrmPrincipal.usuarioGlobal.Token)
                FrmPrincipal.usuarioGlobal = usuario
                MessageBox.Show("Transacción completa exitósamente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            MessageBox.Show("No coinciden las contraseñas.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtClave1.Text = ""
            TxtClave2.Text = ""
            TxtClave1.Focus()
        End If
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Close()
    End Sub
#End Region
End Class