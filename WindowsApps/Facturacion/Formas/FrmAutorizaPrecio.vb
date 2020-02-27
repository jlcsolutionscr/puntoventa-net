Imports LeandroSoftware.Core.Utilitario

Public Class FrmAutorizaPrecio
#Region "Variables"
    Private autorizado As Boolean
#End Region

#Region "Eventos Controles"
    Private Sub FrmAutorizaPrecio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        Try
            If TxtUsuario.Text <> "" And TxtClave.Text <> "" And txtPrecio.Text <> "" Then
                Dim strEncryptedPassword As String = Utilitario.EncriptarDatos(TxtClave.Text)
                FrmPrincipal.strCodigoUsuario = TxtUsuario.Text
                FrmPrincipal.strContrasena = strEncryptedPassword
                FrmPrincipal.strBusqueda = txtPrecio.Text
                Close()
            Else
                MessageBox.Show("Información incompleta. Por favor verifique. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtUsuario.Focus()
        End Try
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        TxtUsuario.Text = ""
        TxtClave.Text = ""
        Close()
    End Sub

    Private Sub txtPrecio_TextChanged(sender As Object, e As EventArgs) Handles txtPrecio.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class