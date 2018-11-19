Imports LeandroSoftware.Puntoventa.Servicios
Imports Unity

Public Class FrmActualizarClave
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Eventos Controles"
    Private Sub FrmActualizarClave_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If TxtClave1.Text = TxtClave2.Text Then
            Try
                FrmMenuPrincipal.usuarioGlobal = servicioMantenimiento.ActualizarClaveUsuario(FrmMenuPrincipal.usuarioGlobal.IdUsuario, TxtClave1.Text, FrmMenuPrincipal.strAppThumptPrint)
                MessageBox.Show("Transacci�n completa exit�samente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            MessageBox.Show("No coinciden las contrase�as.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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