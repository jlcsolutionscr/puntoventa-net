Imports System.Collections.Generic
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Reflection

Public Class FrmCierreDeCaja
#Region "Variables"
    Private strUsuario, strEmpresa As String
    Private decTotalEnCaja As Decimal
    Private cierreCaja As CierreCaja
    Private lstReporte As List(Of DescripcionValor)
    Private assembly As Assembly = Assembly.LoadFrom("Core.dll")
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private initialized As Boolean = False
#End Region

#Region "Metodos"
    Private Sub CalcularFlujoEfectivo()
        decTotalEnCaja = CDbl(txtTotal5.Text) + CDbl(txtTotal10.Text) + CDbl(txtTotal25.Text) + CDbl(txtTotal50.Text) + CDbl(txtTotal100.Text) + CDbl(txtTotal500.Text) + CDbl(txtTotal1000.Text) + CDbl(txtTotal2000.Text) + CDbl(txtTotal5000.Text) + CDbl(txtTotal10000.Text) + CDbl(txtTotal20000.Text) + CDbl(txtTotal50000.Text)
        txtEfectivoCaja.Text = FormatNumber(decTotalEnCaja, 2)
        txtSaldo.Text = FormatNumber(decTotalEnCaja - CDbl(txtTotalEfectivo.Text), 2)
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCierre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub
    Private Async Sub FrmCierre_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
        strEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
        Try
            cierreCaja = Await Puntoventa.GenerarDatosCierreCaja(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        txtFondoInicio.Text = FormatNumber(cierreCaja.FondoInicio, 2)
        txtAdelantosApartadoEfectivo.Text = FormatNumber(cierreCaja.AdelantosApartadoEfectivo, 2)
        txtAdelantosApartadoBancos.Text = FormatNumber(cierreCaja.AdelantosApartadoBancos, 2)
        txtAdelantosApartadoTarjeta.Text = FormatNumber(cierreCaja.AdelantosApartadoTarjeta, 2)
        txtTotalAdelantosApartado.Text = FormatNumber(cierreCaja.AdelantosApartadoEfectivo + cierreCaja.AdelantosApartadoBancos + cierreCaja.AdelantosApartadoTarjeta, 2)
        txtAdelantosOrdenEfectivo.Text = FormatNumber(cierreCaja.AdelantosOrdenEfectivo, 2)
        txtAdelantosOrdenBancos.Text = FormatNumber(cierreCaja.AdelantosOrdenBancos, 2)
        txtAdelantosOrdenTarjeta.Text = FormatNumber(cierreCaja.AdelantosOrdenTarjeta, 2)
        txtTotalAdelantosOrden.Text = FormatNumber(cierreCaja.AdelantosOrdenEfectivo + cierreCaja.AdelantosOrdenBancos + cierreCaja.AdelantosOrdenTarjeta, 2)
        txtVentasEfectivo.Text = FormatNumber(cierreCaja.VentasEfectivo, 2)
        txtVentasCredito.Text = FormatNumber(cierreCaja.VentasCredito, 2)
        txtVentasTarjeta.Text = FormatNumber(cierreCaja.VentasTarjeta, 2)
        txtVentasBancos.Text = FormatNumber(cierreCaja.VentasBancos, 2)
        txtRetencionIVA.Text = FormatNumber(cierreCaja.RetencionTarjeta, 2)
        txtComision.Text = FormatNumber(cierreCaja.ComisionTarjeta, 2)
        txtLiquidacionTarjeta.Text = FormatNumber(cierreCaja.VentasTarjeta - cierreCaja.RetencionTarjeta - cierreCaja.ComisionTarjeta, 2)
        txtTotalVentas.Text = FormatNumber(cierreCaja.VentasEfectivo + cierreCaja.VentasCredito + cierreCaja.VentasTarjeta + cierreCaja.VentasBancos, 2)
        txtPagosCxCEfectivo.Text = FormatNumber(cierreCaja.PagosCxCEfectivo, 2)
        txtPagosCxCBancos.Text = FormatNumber(cierreCaja.PagosCxCBancos, 2)
        txtPagosCxCTarjeta.Text = FormatNumber(cierreCaja.PagosCxCTarjeta, 2)
        txtTotalPagoCxC.Text = FormatNumber(cierreCaja.PagosCxCEfectivo + cierreCaja.PagosCxCBancos + cierreCaja.PagosCxCTarjeta, 2)
        txtDevolucionesProveedores.Text = FormatNumber(cierreCaja.DevolucionesProveedores, 2)
        txtIngresosEfectivo.Text = FormatNumber(cierreCaja.IngresosEfectivo, 2)
        txtTotalIngresos.Text = FormatNumber(cierreCaja.AdelantosApartadoEfectivo + cierreCaja.AdelantosOrdenEfectivo + cierreCaja.VentasEfectivo + cierreCaja.PagosCxCEfectivo + cierreCaja.DevolucionesClientes + cierreCaja.IngresosEfectivo, 2)
        txtComprasEfectivo.Text = FormatNumber(cierreCaja.ComprasEfectivo, 2)
        txtComprasBancos.Text = FormatNumber(cierreCaja.ComprasBancos, 2)
        txtComprasCredito.Text = FormatNumber(cierreCaja.ComprasCredito, 2)
        txtTotalCompras.Text = FormatNumber(cierreCaja.ComprasEfectivo + cierreCaja.ComprasBancos + cierreCaja.ComprasCredito, 2)
        txtPagosCxPEfectivo.Text = FormatNumber(cierreCaja.PagosCxPEfectivo, 2)
        txtPagosCxPBancos.Text = FormatNumber(cierreCaja.PagosCxPBancos, 2)
        txtTotalPagoCxP.Text = FormatNumber(cierreCaja.PagosCxPEfectivo + cierreCaja.PagosCxPBancos, 2)
        txtDevolucionesClientes.Text = FormatNumber(cierreCaja.DevolucionesClientes, 2)
        txtEgresosEfectivo.Text = FormatNumber(cierreCaja.EgresosEfectivo, 2)
        txtTotalEgresos.Text = FormatNumber(cierreCaja.ComprasEfectivo + cierreCaja.PagosCxPEfectivo + cierreCaja.DevolucionesClientes + cierreCaja.EgresosEfectivo, 2)
        txtTotalEfectivo.Text = FormatNumber(CDbl(txtFondoInicio.Text) + CDbl(txtTotalIngresos.Text) - CDbl(txtTotalEgresos.Text), 2)
        txtTotalIngresosTarjeta.Text = FormatNumber(CDbl(txtVentasTarjeta.Text) + CDbl(txtPagosCxCTarjeta.Text), 2)
        txtRetiroEfectivo.Text = FormatNumber(cierreCaja.RetiroEfectivo, 2)
        txtCierreEfectivoProx.Text = FormatNumber(CDbl(txtTotalEfectivo.Text), 2)
        MessageBox.Show("Verifique la información del cierre. Si desea registrar el cierre presione el botón Guardar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnGuardar.Enabled = True
        txtCantidad5.Focus()
        initialized = True
    End Sub

    Private Async Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Try
            cierreCaja.FechaCierre = Now
            cierreCaja.RetiroEfectivo = CDbl(txtRetiroEfectivo.Text)
            cierreCaja.FondoCierre = CDbl(txtCierreEfectivoProx.Text)
            cierreCaja.Observaciones = txtObservaciones.Text
            cierreCaja.DetalleEfectivoCierreCaja.Clear()
            For Each c As Control In grbDetalleEfectivo.Controls
                If c.Name.Contains("txtCantidad") Then
                    Dim denominacion As String = c.Name.Substring(11)
                    Dim detalle As DetalleEfectivoCierreCaja = New DetalleEfectivoCierreCaja With {
                        .Denominacion = CInt(denominacion),
                        .Cantidad = CInt(c.Text)
                    }
                    cierreCaja.DetalleEfectivoCierreCaja.Add(detalle)
                End If
            Next
            Await Puntoventa.GuardarDatosCierreCaja(cierreCaja, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Información guardada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnReporte.Enabled = True
        btnReporte.Focus()
        btnTiquete.Enabled = True
        btnGuardar.Enabled = False
    End Sub

    Private Async Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim newFormReport As FrmReportViewer = New FrmReportViewer
        newFormReport.Visible = False
        Try
            lstReporte = Await Puntoventa.ObtenerReporteCierreDeCaja(cierreCaja.IdCierre, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", lstReporte)
        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDescripcionValor.rdlc")
        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
        Dim parameters(3) As ReportParameter
        parameters(0) = New ReportParameter("pUsuario", strUsuario)
        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
        parameters(2) = New ReportParameter("pNombreReporte", "Cierre de Flujo de Efectivo")
        parameters(3) = New ReportParameter("pFechaHora", cierreCaja.FechaCierre)
        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
        newFormReport.ShowDialog()
    End Sub

    Private Sub btnTiquete_Click(sender As Object, e As EventArgs) Handles btnTiquete.Click
        Try
            comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strFecha = cierreCaja.FechaCierre,
                .strDescuento = txtTotalIngresos.Text,
                .strImpuesto = txtTotalEgresos.Text,
                .strClaveNumerica = txtTotalEfectivo.Text,
                .strNombre = txtEfectivoCaja.Text,
                .strDireccion = txtSaldo.Text,
                .strCambio = txtRetiroEfectivo.Text,
                .strPagoCon = txtCierreEfectivoProx.Text,
                .strDocumento = cierreCaja.Observaciones
            }
            comprobanteImpresion.arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Inicio efectivo", FormatNumber(cierreCaja.FondoInicio)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Adelanto apart.", FormatNumber(cierreCaja.AdelantosApartadoEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Adelanto orden.", FormatNumber(cierreCaja.AdelantosOrdenEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Ventas efectivo", FormatNumber(cierreCaja.VentasEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Abonos a CxC", FormatNumber(cierreCaja.PagosCxCEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Devol. proveedo", FormatNumber(cierreCaja.DevolucionesProveedores)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Otros ingresos", FormatNumber(cierreCaja.IngresosEfectivo)))
            comprobanteImpresion.arrDetalleComprobante = New List(Of ModuloImpresion.ClsDetalleComprobante)
            comprobanteImpresion.arrDetalleComprobante.Add(New ModuloImpresion.ClsDetalleComprobante With {
                .strDescripcion = "Compras efect.",
                .strTotalLinea = FormatNumber(cierreCaja.ComprasEfectivo)
            })
            comprobanteImpresion.arrDetalleComprobante.Add(New ModuloImpresion.ClsDetalleComprobante With {
                .strDescripcion = "Pagos a CxP",
                .strTotalLinea = FormatNumber(cierreCaja.PagosCxPEfectivo)
            })
            comprobanteImpresion.arrDetalleComprobante.Add(New ModuloImpresion.ClsDetalleComprobante With {
                .strDescripcion = "Devol. clientes",
                .strTotalLinea = FormatNumber(cierreCaja.DevolucionesClientes)
            })
            comprobanteImpresion.arrDetalleComprobante.Add(New ModuloImpresion.ClsDetalleComprobante With {
                .strDescripcion = "Otros egresos",
                .strTotalLinea = FormatNumber(cierreCaja.EgresosEfectivo)
            })
            ModuloImpresion.ImprimirCierreEfectivo(comprobanteImpresion)
        Catch ex As Exception
            MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub fields_Enter(sender As Object, e As EventArgs) Handles txtCantidad5.Enter, txtCantidad10.Enter, txtCantidad25.Enter, txtCantidad50.Enter, txtCantidad100.Enter, txtCantidad500.Enter, txtCantidad1000.Enter, txtCantidad2000.Enter, txtCantidad5000.Enter, txtCantidad10000.Enter, txtCantidad20000.Enter, txtCantidad50000.Enter
        If initialized Then
            Dim current As TextBox = grbDetalleEfectivo.Controls(sender.Name)
            If current IsNot Nothing Then current.SelectAll()
        End If
    End Sub

    Private Sub fields_Leave(sender As Object, e As EventArgs) Handles txtCantidad5.Leave, txtCantidad10.Leave, txtCantidad25.Leave, txtCantidad50.Leave, txtCantidad100.Leave, txtCantidad500.Leave, txtCantidad1000.Leave, txtCantidad2000.Leave, txtCantidad5000.Leave, txtCantidad10000.Leave, txtCantidad20000.Leave, txtCantidad50000.Leave
        If initialized Then
            Dim current As TextBox = grbDetalleEfectivo.Controls(sender.Name)
            If current IsNot Nothing Then
                Dim denominacion As Integer = sender.Name.ToString().Substring(11)
                current.SelectionLength = 0
                Dim total As TextBox = grbDetalleEfectivo.Controls("txtTotal" & denominacion)
                total.Text = FormatNumber(CInt(current.Text) * denominacion, 2)
            End If
            CalcularFlujoEfectivo()
        End If
    End Sub

    Private Sub fields_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCantidad5.PreviewKeyDown, txtCantidad10.PreviewKeyDown, txtCantidad25.PreviewKeyDown, txtCantidad50.PreviewKeyDown, txtCantidad100.PreviewKeyDown, txtCantidad500.PreviewKeyDown, txtCantidad1000.PreviewKeyDown, txtCantidad2000.PreviewKeyDown, txtCantidad5000.PreviewKeyDown, txtCantidad10000.PreviewKeyDown, txtCantidad20000.PreviewKeyDown, txtCantidad50000.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            Dim current As TextBox = grbDetalleEfectivo.Controls(sender.Name)
            If current IsNot Nothing Then
                grbDetalleEfectivo.SelectNextControl(current, True, True, False, True)
            End If
        End If
    End Sub

    Private Sub txtCierreEfectivoProx_PreviewKeyDown(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCierreEfectivoProx.PreviewKeyDown
        If e.KeyCode = Keys.Tab Or e.KeyCode = Keys.Enter Then
            If txtCierreEfectivoProx.Text = "" Then txtCierreEfectivoProx.Text = "0"
            txtCierreEfectivoProx.Text = FormatNumber(txtCierreEfectivoProx.Text, 2)
            txtRetiroEfectivo.Text = FormatNumber(CDbl(txtTotalEfectivo.Text) - CDbl(txtCierreEfectivoProx.Text), 2)
        End If
    End Sub

    Private Async Sub FrmCierreDeCaja_Closing(sender As Object, e As System.ComponentModel.CancelEventArgs) Handles MyBase.Closing
        Try
            If (Not cierreCaja Is Nothing) Then
                Await Puntoventa.AbortarCierreCaja(cierreCaja.IdEmpresa, cierreCaja.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtCantidad5.KeyPress, txtCantidad10.KeyPress, txtCantidad25.KeyPress, txtCantidad50.KeyPress, txtCantidad100.KeyPress, txtCantidad500.KeyPress, txtCantidad1000.KeyPress, txtCantidad2000.KeyPress, txtCantidad5000.KeyPress, txtCantidad10000.KeyPress, txtCantidad20000.KeyPress, txtCantidad50000.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtRetiroEfectivo.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class