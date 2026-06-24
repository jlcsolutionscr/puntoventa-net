Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades

Public Class FrmBusquedaNotaCredito
#Region "Variables"
    Private notaCredito As NotaCreditoCliente
#End Region

#Region "M�todos"
#End Region

#Region "Eventos Controles"
    Private Sub FrmAplicaNotaCredito_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
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

    Private Async Sub BtnBuscar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscar.Click
        If txtId.Text = "" Then
            MessageBox.Show("Debe ingresar el numero de nota de credito!", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        Try
            notaCredito = Await Puntoventa.ObtenerNotaCreditoCliente(txtId.Text, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        txtFecha.Text = notaCredito.Fecha
        txtMontoOriginal.Text = FormatNumber(notaCredito.MontoOriginal, 2)
        txtSaldo.Text = FormatNumber(notaCredito.Saldo, 2)
        txtDetalle.Text = notaCredito.Detalle
    End Sub

    Private Sub btnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        FrmPrincipal.intBusqueda = notaCredito.IdNotaCredito
        Close()
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtId.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub
#End Region
End Class