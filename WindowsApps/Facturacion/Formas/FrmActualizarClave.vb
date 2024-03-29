Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Seguridad

Public Class FrmActualizarClave
#Region "Eventos Controles"
    Private Sub FrmActualizarClave_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub

    Private Async Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If TxtClave1.Text = TxtClave2.Text Then
            Try
                Dim strClaveEncriptada = Encriptador.EncriptarDatos(TxtClave1.Text)
                Dim usuario As Usuario = Await Puntoventa.ActualizarClaveUsuario(FrmPrincipal.usuarioGlobal.IdUsuario, strClaveEncriptada, FrmPrincipal.usuarioGlobal.Token)
                FrmPrincipal.usuarioGlobal = usuario
                MessageBox.Show("Transacción completa exitósamente.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            MessageBox.Show("No coinciden las contraseñas.  Intente de nuevo. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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