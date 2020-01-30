Public Class FrmPagoFactura
#Region "Variables"
    Public decTotalEfectivo As Decimal = 0
    Private decCambio As Decimal = 0
#End Region

#Region "Eventos Controles"
    Private Sub FrmPagoFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub

    Private Sub FrmPagoFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        decCambio = 0 - decTotalEfectivo
        txtTotalPagoEfectivo.Text = FormatNumber(decTotalEfectivo, 2)
        txtCambio.Text = FormatNumber(decCambio, 2)
        txtPagoDelCliente.Focus()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtPagoDelCliente.Text <> "" Then
            FrmPrincipal.intBusqueda = txtPagoDelCliente.Text
        Else
            MessageBox.Show("Debe ingresar el monto de pago del cliente para poder continuar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub txtPagoDelCliente_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPagoDelCliente.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtPagoDelCliente.Text = "" Then
                txtPagoDelCliente.Text = FormatNumber(decTotalEfectivo, 2)
            ElseIf txtPagoDelCliente.Text < decTotalEfectivo Then
                txtPagoDelCliente.Text = FormatNumber(decTotalEfectivo, 2)
                MessageBox.Show("El monto con el que el cliente paga no puede ser menor al total de la factura. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Else
                txtPagoDelCliente.Text = FormatNumber(txtPagoDelCliente.Text, 2)
            End If
            txtCambio.Text = FormatNumber(txtPagoDelCliente.Text - (decTotalEfectivo), 2)
        End If
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtPagoDelCliente.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub
#End Region
End Class