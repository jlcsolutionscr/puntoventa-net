Imports System.Collections.Generic
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Reflection

Public Class FrmConsultaCierreCaja
#Region "Variables"
    Public intIdCierre As Integer
    Private strUsuario, strEmpresa As String
    Private decEfectivoEnCaja As Decimal = 0
    Private dtbDetalleEfectivo As DataTable
    Private dtrRowDetEfectivo As DataRow
    Private cierreCaja As CierreCaja
    Private lstReporte As List(Of DescripcionValor)
    Private assembly As Assembly = Assembly.LoadFrom("Core.dll")
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleEfectivo = New DataTable()
        dtbDetalleEfectivo.Columns.Add("DENOMINACION", GetType(Integer))
        dtbDetalleEfectivo.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleEfectivo.Columns.Add("CANTIDAD", GetType(Integer))
        dtbDetalleEfectivo.Columns.Add("TOTAL", GetType(Decimal))
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        dgvDetalleEfectivoCierreCaja.Columns.Clear()
        dgvDetalleEfectivoCierreCaja.AutoGenerateColumns = False

        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcDenominacion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn

        dvcId.DataPropertyName = "DENOMINACION"
        dvcId.HeaderText = "Id"
        dvcId.Width = 0
        dvcId.Visible = False
        dgvDetalleEfectivoCierreCaja.Columns.Add(dvcId)

        dvcDenominacion.DataPropertyName = "DESCRIPCION"
        dvcDenominacion.HeaderText = "Denominación"
        dvcDenominacion.Width = 190
        dvcDenominacion.Visible = True
        dvcDenominacion.ReadOnly = True
        dgvDetalleEfectivoCierreCaja.Columns.Add(dvcDenominacion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 40
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvInteger
        dgvDetalleEfectivoCierreCaja.Columns.Add(dvcCantidad)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvDetalleEfectivoCierreCaja.Columns.Add(dvcTotal)
    End Sub

    Private Sub CargarDetalleEfectivo(cierreCaja As CierreCaja)
        dtbDetalleEfectivo.Rows.Clear()
        For Each detalle As DetalleEfectivoCierreCaja In cierreCaja.DetalleEfectivoCierreCaja
            decEfectivoEnCaja += detalle.Cantidad * detalle.Denominacion
            dtrRowDetEfectivo = dtbDetalleEfectivo.NewRow
            dtrRowDetEfectivo.Item(0) = detalle.Denominacion
            dtrRowDetEfectivo.Item(1) = IIf(detalle.Denominacion > 500, "Billetes de ", "Monedas de ") & detalle.Denominacion
            dtrRowDetEfectivo.Item(2) = detalle.Cantidad
            dtrRowDetEfectivo.Item(3) = Math.Round(detalle.Cantidad * detalle.Denominacion, 2, MidpointRounding.AwayFromZero)
            dtbDetalleEfectivo.Rows.Add(dtrRowDetEfectivo)
        Next
        dgvDetalleEfectivoCierreCaja.Sort(dgvDetalleEfectivoCierreCaja.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        dgvDetalleEfectivoCierreCaja.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCierre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub
    Private Async Sub FrmCierre_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
        strEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
        IniciaTablasDeDetalle()
        EstablecerPropiedadesDataGridView()
        dgvDetalleEfectivoCierreCaja.DataSource = dtbDetalleEfectivo
        Try
            cierreCaja = Await Puntoventa.ObtenerCierreCaja(intIdCierre, FrmPrincipal.usuarioGlobal.Token)
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
            txtRetiroEfectivo.Text = FormatNumber(cierreCaja.DepositoBancario, 2)
            txtCierreEfectivoProx.Text = FormatNumber(CDbl(txtTotalEfectivo.Text) - CDbl(txtRetiroEfectivo.Text), 2)
            txtObservaciones.Text = cierreCaja.Observaciones
            CargarDetalleEfectivo(cierreCaja)
            txtEfectivoCaja.Text = FormatNumber(decEfectivoEnCaja, 2)
            txtSaldo.Text = FormatNumber(decEfectivoEnCaja - CDbl(txtTotalEfectivo.Text), 2)
            btnReporte.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
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
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Abonos a apartados", FormatNumber(cierreCaja.AdelantosApartadoEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Abonos a ordenes", FormatNumber(cierreCaja.AdelantosOrdenEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Ventas efectivo", FormatNumber(cierreCaja.VentasEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Abonos a CxC", FormatNumber(cierreCaja.PagosCxCEfectivo)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Devol. proveedores", FormatNumber(cierreCaja.DevolucionesProveedores)))
            comprobanteImpresion.arrDesglosePago.Add(New ModuloImpresion.ClsDesgloseFormaPago("Otros ingresos", FormatNumber(cierreCaja.IngresosEfectivo)))
            comprobanteImpresion.arrDetalleComprobante = New List(Of ModuloImpresion.ClsDetalleComprobante)
            comprobanteImpresion.arrDetalleComprobante.Add(New ModuloImpresion.ClsDetalleComprobante With {
                .strDescripcion = "Compras efectivo",
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

    Private Sub txtDepositoBancario_Validated(sender As Object, e As EventArgs) Handles txtRetiroEfectivo.Validated
        If txtRetiroEfectivo.Text = "" Then txtRetiroEfectivo.Text = "0"
        txtRetiroEfectivo.Text = FormatNumber(txtRetiroEfectivo.Text, 2)
        txtCierreEfectivoProx.Text = FormatNumber(CDbl(txtTotalEfectivo.Text) - CDbl(txtRetiroEfectivo.Text), 2)
    End Sub
#End Region
End Class