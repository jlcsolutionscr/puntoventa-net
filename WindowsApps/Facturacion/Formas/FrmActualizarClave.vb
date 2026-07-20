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
        Dim bolModificar = False
        Dim usuario As Usuario = FrmPrincipal.usuarioGlobal
        If TxtClave1.Text <> "" Then
            If TxtClave1.Text = TxtClave2.Text Then
                Try
                    Dim strClaveEncriptada = Encriptador.EncriptarDatos(TxtClave1.Text)
                    usuario.Clave = strClaveEncriptada
                    bolModificar = True
                Catch ex As Exception
                    MessageBox.Show("Ocurrio un error encriptando la información.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                MessageBox.Show("Las contraseñas ingresadas no coinciden. Verifiue la información!", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        ElseIf txtCodigoPIN.Text <> "" Then
            usuario.CodigoPIN = txtCodigoPIN.Text
            bolModificar = True
        End If
        If bolModificar Then
            Try
                Await Puntoventa.ActualizarUsuario(usuario, FrmPrincipal.usuarioGlobal.Token)
                FrmPrincipal.usuarioGlobal = usuario
                MessageBox.Show("Transacción completa exitósamente.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Catch ex As Exception
                MessageBox.Show("Ocurrió un error actualizando la información del usuario!", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Close()
    End Sub
#End Region
End Class