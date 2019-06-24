Imports System.Collections.Generic
Imports LeandroSoftware.AccesoDatos.ClienteWCF
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.Puntoventa.CommonTypes

Public Class FrmCierreDeCaja
#Region "Variables"
    Private Criterio, strUsuario, strEmpresa As String
    Private cierreCaja As CierreCaja
    Private lstReporte As List(Of ReporteCierreDeCaja)
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmCierre_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
        strEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
        Try
            cierreCaja = Await PuntoventaWCF.GenerarDatosCierreCaja(FrmPrincipal.empresaGlobal.IdEmpresa, Today)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        txtFondoInicio.Text = FormatNumber(cierreCaja.FondoInicio, 2)
        txtVentasContado.Text = FormatNumber(cierreCaja.VentasContado, 2)
        txtVentasCrédito.Text = FormatNumber(cierreCaja.VentasCredito, 2)
        txtVentasTarjeta.Text = FormatNumber(cierreCaja.VentasTarjeta, 2)
        txtOtrasVentas.Text = FormatNumber(cierreCaja.OtrasVentas, 2)
        txtRetencionIVA.Text = FormatNumber(cierreCaja.RetencionIVA, 2)
        txtComision.Text = FormatNumber(cierreCaja.ComisionVT, 2)
        txtLiquidacionTarjeta.Text = FormatNumber(cierreCaja.LiquidacionTarjeta, 2)
        txtTotalVentas.Text = FormatNumber(CDbl(txtVentasContado.Text) + CDbl(txtVentasCrédito.Text) + CDbl(txtVentasTarjeta.Text) + CDbl(txtOtrasVentas.Text), 2)
        txtIngresoCxCEfectivo.Text = FormatNumber(cierreCaja.IngresoCxCEfectivo, 2)
        txtIngresoCxCTarjeta.Text = FormatNumber(cierreCaja.IngresoCxCTarjeta, 2)
        txtDevolucionesProveedores.Text = FormatNumber(cierreCaja.DevolucionesProveedores, 2)
        txtOtrosIngresos.Text = FormatNumber(cierreCaja.OtrosIngresos, 2)
        txtTotalIngresos.Text = FormatNumber(CDbl(txtVentasContado.Text) + CDbl(txtIngresoCxCEfectivo.Text) + CDbl(txtDevolucionesProveedores.Text) + CDbl(txtOtrosIngresos.Text), 2)
        txtComprasContado.Text = FormatNumber(cierreCaja.ComprasContado, 2)
        txtComprasCredito.Text = FormatNumber(cierreCaja.ComprasCredito, 2)
        txtOtrasCompras.Text = FormatNumber(cierreCaja.OtrasCompras, 2)
        txtTotalCompras.Text = FormatNumber(CDbl(txtComprasContado.Text) + CDbl(txtComprasCredito.Text) + CDbl(txtOtrasCompras.Text), 2)
        txtEgresoCxPEfectivo.Text = FormatNumber(cierreCaja.EgresoCxPEfectivo, 2)
        txtDevolucionesClientes.Text = FormatNumber(cierreCaja.DevolucionesClientes, 2)
        txtOtrosGastos.Text = FormatNumber(cierreCaja.OtrosEgresos, 2)
        txtTotalEgresos.Text = FormatNumber(CDbl(txtComprasContado.Text) + CDbl(txtEgresoCxPEfectivo.Text) + CDbl(txtDevolucionesClientes.Text) + CDbl(txtOtrosGastos.Text), 2)
        txtTotalEfectivo.Text = FormatNumber(CDbl(txtFondoInicio.Text) + CDbl(txtTotalIngresos.Text) - CDbl(txtTotalEgresos.Text), 2)
        txtTotalIngresosTarjeta.Text = FormatNumber(CDbl(txtVentasTarjeta.Text) + CDbl(txtIngresoCxCTarjeta.Text), 2)
        cierreCaja.FondoCierre = CDbl(txtTotalEfectivo.Text)
        MessageBox.Show("Verifique la información del cierre. Si desea registrar el cierre presione el botón Guardar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnGuardar.Enabled = True
        btnGuardar.Focus()
    End Sub

    Private Async Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            cierreCaja = Await PuntoventaWCF.GuardarDatosCierreCaja(cierreCaja)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Información guardada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGuardar.Enabled = False
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        'Dim reptCierre As New rptCierreCaja()
        'Dim formReport As New ReportViewer()
        'Try
        '    lstReporte = Await PuntoventaWCF.ObtenerReporteCierreDeCaja(cierreCaja.IdCierre)
        'Catch ex As Exception
        '    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        '    Exit Sub
        'End Try
        'reptCierre.SetDataSource(lstReporte)
        'reptCierre.SetParameterValue(0, strUsuario)
        'reptCierre.SetParameterValue(1, strEmpresa)
        'formReport.crtViewer.ReportSource = reptCierre
        'formReport.ShowDialog()
    End Sub

    Private Async Sub FrmCierreDeCaja_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If (Not cierreCaja Is Nothing) Then
                Await PuntoventaWCF.AbortarCierreCaja(cierreCaja.IdEmpresa)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region
End Class