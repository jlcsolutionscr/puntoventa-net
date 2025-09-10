Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Seguridad

Public Class FrmAutorizacionEspecial
#Region "Variables"
    Private decMontoDescuento As Decimal
    Public decPorcentaje As Decimal
    Public decPrecioVenta As Decimal
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

    Private Sub FrmAutorizaPrecio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        TxtUsuario.Text = ""
        TxtClave.Text = ""
        txtPorcentaje.Text = FormatNumber(decPorcentaje, 5)
        decMontoDescuento = decPrecioVenta * decPorcentaje / 100
        txtPrecioFinal.Text = FormatNumber(decPrecioVenta - decMontoDescuento, 2)
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
        DialogResult = DialogResult.None
        Try
            If TxtUsuario.Text <> "" And TxtClave.Text <> "" And txtPorcentaje.Text <> "" Then
                Dim strEncryptedPassword As String = Encriptador.EncriptarDatos(TxtClave.Text)
                Dim decPorcentaje As Decimal
                Dim decDescAutorizado As Decimal = txtPorcentaje.Text
                Try
                    decPorcentaje = Await Puntoventa.AutorizacionPorcentaje(TxtUsuario.Text, strEncryptedPassword, FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                    If decPorcentaje < decDescAutorizado Then
                        MessageBox.Show("El usuario ingresado no puede autorizar el porcentaje solicitado.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        TxtUsuario.Focus()
                    Else
                        FrmPrincipal.decDescAutorizado = decDescAutorizado
                        Close()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    TxtUsuario.Focus()
                End Try
            Else
                MessageBox.Show("Información incompleta. Por favor verifique.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TxtUsuario.Focus()
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtUsuario.Focus()
        End Try
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        FrmPrincipal.decDescAutorizado = 0
        Close()
    End Sub

    Private Sub txtPorcentaje_Validated(sender As Object, e As EventArgs) Handles txtPorcentaje.Validated
        txtPorcentaje.Text = FormatNumber(txtPorcentaje.Text, 5)
        decMontoDescuento = decPrecioVenta * CDec(txtPorcentaje.Text) / 100
        txtPrecioFinal.Text = FormatNumber(decPrecioVenta - decMontoDescuento, 2)
    End Sub

    Private Sub txtMonto_Validated(sender As Object, e As EventArgs) Handles txtPrecioFinal.Validated
        txtPrecioFinal.Text = FormatNumber(txtPrecioFinal.Text, 2)
        decMontoDescuento = decPrecioVenta - txtPrecioFinal.Text
        txtPorcentaje.Text = FormatNumber(decMontoDescuento / decPrecioVenta * 100, 5)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtPorcentaje.KeyPress, txtPrecioFinal.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class