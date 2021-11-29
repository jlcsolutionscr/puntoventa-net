Imports System.Collections.Generic
Imports System.Linq
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports Microsoft.Reporting.WinForms
Imports System.IO
Imports System.Reflection
Imports LeandroSoftware.ClienteWCF

Public Class FrmConsultaCierreCaja
#Region "Variables"
    Public intIdCierre As Integer
    Private strUsuario, strEmpresa As String
    Private decEfectivoEnCaja As Decimal = 0
    Private dtbDetalleEfectivo As DataTable
    Private dtrRowDetEfectivo As DataRow
    Private cierreCaja As CierreCaja
    Private lstReporte As List(Of DescripcionValor)
    Private ReadOnly assembly As Assembly = Assembly.LoadFrom("Core.dll")
    Private comprobanteImpresion As ModuloImpresion.ClsCierreCaja
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
            dtrRowDetEfectivo.Item(3) = Math.Round(detalle.Cantidad * detalle.Denominacion, 2)
            dtbDetalleEfectivo.Rows.Add(dtrRowDetEfectivo)
        Next
        dgvDetalleEfectivoCierreCaja.Sort(dgvDetalleEfectivoCierreCaja.Columns(0), System.ComponentModel.ListSortDirection.Ascending)
        dgvDetalleEfectivoCierreCaja.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCierre_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
    Private Async Sub FrmCierre_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
        strEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
        IniciaTablasDeDetalle()
        EstablecerPropiedadesDataGridView()
        dgvDetalleEfectivoCierreCaja.DataSource = dtbDetalleEfectivo
        Try
            cierreCaja = Await Puntoventa.ObtenerCierreCaja(intIdCierre, FrmPrincipal.usuarioGlobal.Token)
            Dim decTotalTarjeta = cierreCaja.VentasTarjeta + cierreCaja.AdelantosApartadoTarjeta + cierreCaja.AdelantosOrdenTarjeta + cierreCaja.PagosCxCTarjeta
            txtFondoInicio.Text = FormatNumber(cierreCaja.FondoInicio, 2)
            txtAdelantosApartadoEfectivo05.Text = FormatNumber(cierreCaja.AdelantosApartadoEfectivo, 2)
            txtAdelantosApartadoBancos07.Text = FormatNumber(cierreCaja.AdelantosApartadoBancos, 2)
            txtAdelantosApartadoTarjeta06.Text = FormatNumber(cierreCaja.AdelantosApartadoTarjeta, 2)
            txtTotalAdelantosApartado.Text = FormatNumber(cierreCaja.AdelantosApartadoEfectivo + cierreCaja.AdelantosApartadoBancos + cierreCaja.AdelantosApartadoTarjeta, 2)
            txtAdelantosOrdenEfectivo08.Text = FormatNumber(cierreCaja.AdelantosOrdenEfectivo, 2)
            txtAdelantosOrdenBancos10.Text = FormatNumber(cierreCaja.AdelantosOrdenBancos, 2)
            txtAdelantosOrdenTarjeta09.Text = FormatNumber(cierreCaja.AdelantosOrdenTarjeta, 2)
            txtTotalAdelantosOrden.Text = FormatNumber(cierreCaja.AdelantosOrdenEfectivo + cierreCaja.AdelantosOrdenBancos + cierreCaja.AdelantosOrdenTarjeta, 2)
            txtVentasEfectivo01.Text = FormatNumber(cierreCaja.VentasEfectivo, 2)
            txtVentasCredito04.Text = FormatNumber(cierreCaja.VentasCredito, 2)
            txtVentasTarjeta02.Text = FormatNumber(cierreCaja.VentasTarjeta, 2)
            txtVentasBancos03.Text = FormatNumber(cierreCaja.VentasBancos, 2)
            txtRetencionIVA.Text = FormatNumber(cierreCaja.RetencionTarjeta, 2)
            txtComision.Text = FormatNumber(cierreCaja.ComisionTarjeta, 2)
            txtLiquidacionTarjeta.Text = FormatNumber(decTotalTarjeta - cierreCaja.RetencionTarjeta - cierreCaja.ComisionTarjeta, 2)
            txtTotalVentas.Text = FormatNumber(cierreCaja.VentasEfectivo + cierreCaja.VentasCredito + cierreCaja.VentasTarjeta + cierreCaja.VentasBancos, 2)
            txtPagosCxCEfectivo11.Text = FormatNumber(cierreCaja.PagosCxCEfectivo, 2)
            txtPagosCxCBancos13.Text = FormatNumber(cierreCaja.PagosCxCBancos, 2)
            txtPagosCxCTarjeta12.Text = FormatNumber(cierreCaja.PagosCxCTarjeta, 2)
            txtTotalPagoCxC.Text = FormatNumber(cierreCaja.PagosCxCEfectivo + cierreCaja.PagosCxCBancos + cierreCaja.PagosCxCTarjeta, 2)
            txtIngresosEfectivo14.Text = FormatNumber(cierreCaja.IngresosEfectivo, 2)
            txtTotalIngresos.Text = FormatNumber(cierreCaja.AdelantosApartadoEfectivo + cierreCaja.AdelantosOrdenEfectivo + cierreCaja.VentasEfectivo + cierreCaja.PagosCxCEfectivo + cierreCaja.IngresosEfectivo, 2)
            txtComprasEfectivo15.Text = FormatNumber(cierreCaja.ComprasEfectivo, 2)
            txtComprasBancos16.Text = FormatNumber(cierreCaja.ComprasBancos, 2)
            txtComprasCredito17.Text = FormatNumber(cierreCaja.ComprasCredito, 2)
            txtTotalCompras.Text = FormatNumber(cierreCaja.ComprasEfectivo + cierreCaja.ComprasBancos + cierreCaja.ComprasCredito, 2)
            txtPagosCxPEfectivo18.Text = FormatNumber(cierreCaja.PagosCxPEfectivo, 2)
            txtPagosCxPBancos19.Text = FormatNumber(cierreCaja.PagosCxPBancos, 2)
            txtTotalPagoCxP.Text = FormatNumber(cierreCaja.PagosCxPEfectivo + cierreCaja.PagosCxPBancos, 2)
            txtEgresosEfectivo20.Text = FormatNumber(cierreCaja.EgresosEfectivo, 2)
            txtTotalEgresos.Text = FormatNumber(cierreCaja.ComprasEfectivo + cierreCaja.PagosCxPEfectivo + cierreCaja.EgresosEfectivo, 2)
            txtTotalEfectivo.Text = FormatNumber(CDbl(txtFondoInicio.Text) + CDbl(txtTotalIngresos.Text) - CDbl(txtTotalEgresos.Text), 2)
            txtTotalIngresosTarjeta.Text = FormatNumber(decTotalTarjeta, 2)
            txtRetiroEfectivo.Text = FormatNumber(cierreCaja.RetiroEfectivo, 2)
            txtCierreEfectivoProx.Text = FormatNumber(CDbl(txtTotalEfectivo.Text) - CDbl(txtRetiroEfectivo.Text), 2)
            txtObservaciones.Text = cierreCaja.Observaciones
            CargarDetalleEfectivo(cierreCaja)
            txtEfectivoCaja.Text = FormatNumber(decEfectivoEnCaja, 2)
            txtSaldo.Text = FormatNumber(decEfectivoEnCaja - CDbl(txtTotalEfectivo.Text), 2)
            btnReporte.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnReporte.Click
        Dim newFormReport As FrmReportViewer = New FrmReportViewer With {
            .Visible = False
        }
        Try
            lstReporte = Await Puntoventa.ObtenerReporteCierreDeCaja(cierreCaja.IdCierre, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", lstReporte)
        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDescripcionValor.rdlc")
        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
        Dim parameters(4) As ReportParameter
        parameters(0) = New ReportParameter("pUsuario", strUsuario)
        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
        parameters(2) = New ReportParameter("pNombreReporte", "Cierre de Flujo de Efectivo")
        parameters(3) = New ReportParameter("pFechaHora", cierreCaja.FechaCierre)
        parameters(4) = New ReportParameter("pSucursal", FrmPrincipal.equipoGlobal.NombreSucursal)
        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
        newFormReport.ShowDialog()
    End Sub

    Private Sub BtnTiquete_Click(sender As Object, e As EventArgs) Handles btnTiquete.Click
        Try
            comprobanteImpresion = New ModuloImpresion.ClsCierreCaja With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strFecha = cierreCaja.FechaCierre,
                .strTotalIngresos = txtTotalIngresos.Text,
                .strTotalEgresos = txtTotalEgresos.Text,
                .strTotalEfectivo = txtTotalEfectivo.Text,
                .strEfectivoCaja = txtEfectivoCaja.Text,
                .strSobrante = txtSaldo.Text,
                .strRetiroEfectivo = txtRetiroEfectivo.Text,
                .strCierreEfectivoProx = txtCierreEfectivoProx.Text,
                .strObservaciones = txtObservaciones.Text,
                .strVentasEfectivo = txtVentasEfectivo01.Text,
                .strVentasTarjeta = txtVentasTarjeta02.Text,
                .strVentasTransferencia = txtVentasBancos03.Text,
                .strVentasCredito = txtVentasCredito04.Text,
                .strTotalVentas = txtTotalVentas.Text,
                .strAdelantosEfectivo = FormatNumber(CDec(txtAdelantosApartadoEfectivo05.Text) + CDec(txtAdelantosOrdenEfectivo08.Text), 2),
                .strAdelantosTarjeta = FormatNumber(CDec(txtAdelantosApartadoTarjeta06.Text) + CDec(txtAdelantosOrdenTarjeta09.Text), 2),
                .strAdelantosTransferencia = FormatNumber(CDec(txtAdelantosApartadoBancos07.Text) + CDec(txtAdelantosOrdenBancos10.Text), 2),
                .strTotalAdelantos = FormatNumber(CDec(txtAdelantosApartadoEfectivo05.Text) + CDec(txtAdelantosOrdenEfectivo08.Text) + CDec(txtAdelantosApartadoTarjeta06.Text) + CDec(txtAdelantosOrdenTarjeta09.Text) + CDec(txtAdelantosApartadoBancos07.Text) + CDec(txtAdelantosOrdenBancos10.Text), 2)
            }
            comprobanteImpresion.arrDetalleIngresos = New List(Of ModuloImpresion.ClsDesgloseFormaPago) From {
                New ModuloImpresion.ClsDesgloseFormaPago("Inicio efectivo", FormatNumber(cierreCaja.FondoInicio)),
                New ModuloImpresion.ClsDesgloseFormaPago("Adelanto apart.", FormatNumber(cierreCaja.AdelantosApartadoEfectivo)),
                New ModuloImpresion.ClsDesgloseFormaPago("Adelanto orden.", FormatNumber(cierreCaja.AdelantosOrdenEfectivo)),
                New ModuloImpresion.ClsDesgloseFormaPago("Ventas efectivo", FormatNumber(cierreCaja.VentasEfectivo)),
                New ModuloImpresion.ClsDesgloseFormaPago("Abonos a CxC", FormatNumber(cierreCaja.PagosCxCEfectivo)),
                New ModuloImpresion.ClsDesgloseFormaPago("Otros ingresos", FormatNumber(cierreCaja.IngresosEfectivo))
            }
            comprobanteImpresion.arrDetalleEgresos = New List(Of ModuloImpresion.ClsDesgloseFormaPago) From {
                New ModuloImpresion.ClsDesgloseFormaPago("Compras efect.", FormatNumber(cierreCaja.ComprasEfectivo)),
                New ModuloImpresion.ClsDesgloseFormaPago("Pagos a CxP", FormatNumber(cierreCaja.PagosCxPEfectivo)),
                New ModuloImpresion.ClsDesgloseFormaPago("Otros egresos", FormatNumber(cierreCaja.EgresosEfectivo))
            }
            ModuloImpresion.ImprimirCierreEfectivo(comprobanteImpresion)
        Catch ex As Exception
            MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub TextField_DoubleClick(sender As Object, e As EventArgs) Handles txtVentasEfectivo01.DoubleClick, txtVentasBancos03.DoubleClick, txtVentasTarjeta02.DoubleClick, txtVentasCredito04.DoubleClick, txtAdelantosApartadoEfectivo05.DoubleClick, txtAdelantosApartadoTarjeta06.DoubleClick, txtAdelantosApartadoBancos07.DoubleClick, txtAdelantosOrdenEfectivo08.DoubleClick, txtAdelantosOrdenTarjeta09.DoubleClick, txtAdelantosOrdenBancos10.DoubleClick, txtPagosCxCEfectivo11.DoubleClick, txtPagosCxCTarjeta12.DoubleClick, txtPagosCxCBancos13.DoubleClick, txtIngresosEfectivo14.DoubleClick, txtComprasEfectivo15.DoubleClick, txtComprasBancos16.DoubleClick, txtComprasCredito17.DoubleClick, txtPagosCxPEfectivo18.DoubleClick, txtPagosCxPBancos19.DoubleClick, txtEgresosEfectivo20.DoubleClick
        Dim current As TextBox = Controls(sender.Name)
        If CDbl(current.Text) > 0 Then
            Dim intTipo As Integer = CInt(sender.Name.ToString().Substring(sender.Name.ToString().Length - 2))
            Dim detalle As List(Of DetalleMovimientoCierreCaja) = cierreCaja.DetalleMovimientoCierreCaja.Where(Function(x) x.Tipo = intTipo).ToList()
            Dim formDetalle As New FrmDetalleCierreCaja()
            formDetalle.dtDatos = detalle
            formDetalle.ShowDialog()
        End If
    End Sub

    Private Sub TxtDepositoBancario_Validated(sender As Object, e As EventArgs) Handles txtRetiroEfectivo.Validated
        If txtRetiroEfectivo.Text = "" Then txtRetiroEfectivo.Text = "0"
        txtRetiroEfectivo.Text = FormatNumber(txtRetiroEfectivo.Text, 2)
        txtCierreEfectivoProx.Text = FormatNumber(CDbl(txtTotalEfectivo.Text) - CDbl(txtRetiroEfectivo.Text), 2)
    End Sub
#End Region
End Class