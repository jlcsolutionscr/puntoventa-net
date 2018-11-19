Imports System.Collections
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports LeandroSoftware.PuntoVenta.Core.CommonTypes
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmMenuReportes
#Region "Variables"
    Private strUsuario, Valida, strEmpresa As String
    Private dtbDatos As DataTable
    Private servicioReportes As IReporteService
    Private servicioMantenimiento As IMantenimientoService
    Private servicioCompras As ICompraService
    Private servicioFacturacion As IFacturacionService
    Private proveedor As Proveedor
    Private cliente As Cliente
#End Region

#Region "Métodos"
#End Region

#Region "Eventos Controles"
    Private Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                cliente = servicioFacturacion.ObtenerCliente(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtCliente.Text = cliente.Nombre
        End If
    End Sub

    Private Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                proveedor = servicioCompras.ObtenerProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtProveedor.Text = proveedor.Nombre
        End If
    End Sub

    Private Sub CmdVistaPrevia_Click(sender As Object, e As EventArgs) Handles CmdVistaPrevia.Click
        Dim formReport As New frmRptViewer
        Dim intIdCliente As Integer
        Dim intIdProveedor As Integer
        Dim dtListaFormaPago
        If LstReporte.SelectedIndex >= 0 Then
            strUsuario = FrmMenuPrincipal.usuarioGlobal.CodigoUsuario
            strEmpresa = FrmMenuPrincipal.empresaGlobal.NombreEmpresa
            If cliente Is Nothing Then
                intIdCliente = 0
            Else
                intIdCliente = cliente.IdCliente
            End If
            If proveedor Is Nothing Then
                intIdProveedor = 0
            Else
                intIdProveedor = proveedor.IdProveedor
            End If
            Select Case LstReporte.Text
                Case "Ventas En General"
                    Dim reptVentas As New rptVentas
                    Dim formaMenuTipoTransaccion As New FrmMenuTipoTransaccion
                    Dim banco As BancoAdquiriente
                    Dim intFormaPago = 0
                    Dim intBancoAdquiriente = 0
                    Dim strDescripcionReporte = ""
                    Try
                        dtListaFormaPago = servicioReportes.ObtenerListaCondicionVentaYFormaPagoFactura()
                        formaMenuTipoTransaccion.clFormasPago = dtListaFormaPago
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    If formaMenuTipoTransaccion.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        intFormaPago = FrmMenuPrincipal.intBusqueda
                        If intFormaPago = -1 Then
                            strDescripcionReporte = "Reporte de Ventas Generales"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.Credito Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Efectivo"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoEfectivo Then
                            strDescripcionReporte = "Reporte de Ventas de Credito"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Depósito Bancario"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                            strDescripcionReporte = "Reporte de Ventas pago enCheque"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTarjeta Then
                            Dim formaBancoAdquiriente As New FrmMenuBancoAdquiriente
                            If formaBancoAdquiriente.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                intBancoAdquiriente = FrmMenuPrincipal.intBusqueda
                                banco = servicioMantenimiento.ObtenerBancoAdquiriente(FrmMenuPrincipal.intBusqueda)
                                strDescripcionReporte = "Reporte de Ventas con Tarjeta del Banco " & banco.Descripcion
                            Else
                                intFormaPago = 0
                            End If
                        End If
                        If intFormaPago <> 0 Then
                            Try
                                dtbDatos = servicioReportes.ObtenerReporteVentasPorCliente(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.NoNulo, intFormaPago, intBancoAdquiriente)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                            reptVentas.SetDataSource(dtbDatos)
                            reptVentas.SetParameterValue(0, strUsuario)
                            reptVentas.SetParameterValue(1, strEmpresa)
                            reptVentas.SetParameterValue(2, strDescripcionReporte)
                            formReport.crtViewer.ReportSource = reptVentas
                            formReport.ShowDialog()
                        End If
                    End If
                Case "Ventas Anuladas"
                    Dim reptVentas As New rptVentas
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteVentasPorCliente(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.Nulo, 0, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptVentas.SetDataSource(dtbDatos)
                    reptVentas.SetParameterValue(0, strUsuario)
                    reptVentas.SetParameterValue(1, strEmpresa)
                    reptVentas.SetParameterValue(2, "Reporte de Ventas Anuladas")
                    formReport.crtViewer.ReportSource = reptVentas
                    formReport.ShowDialog()
                Case "Ventas por Vendedor"
                    Dim reptVentas As New rptVentasPorVendedor
                    Dim formaMenuVendedor As New FrmMenuVendedor
                    If formaMenuVendedor.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            dtbDatos = servicioReportes.ObtenerReporteVentasPorVendedor(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmMenuPrincipal.intBusqueda)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        reptVentas.SetDataSource(dtbDatos)
                        reptVentas.SetParameterValue(0, strUsuario)
                        reptVentas.SetParameterValue(1, strEmpresa)
                        formReport.crtViewer.ReportSource = reptVentas
                        formReport.ShowDialog()
                    End If
                Case "Compras En General"
                    Dim reptCompras As New rptCompras
                    Dim formaMenuTipoTransaccion As New FrmMenuTipoTransaccion
                    Dim intFormaPago = 0
                    Dim intBancoAdquiriente = 0
                    Dim strDescripcionReporte = ""
                    Try
                        dtListaFormaPago = servicioReportes.ObtenerListaCondicionVentaYFormaPagoCompra()
                        formaMenuTipoTransaccion.clFormasPago = dtListaFormaPago
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    If formaMenuTipoTransaccion.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        intFormaPago = FrmMenuPrincipal.intBusqueda
                        If intFormaPago = -1 Then
                            strDescripcionReporte = "Reporte de Compras Generales"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.Credito Then
                            strDescripcionReporte = "Reporte de Compras con pago en Efectivo"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoEfectivo Then
                            strDescripcionReporte = "Reporte de Compras de Credito"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                            strDescripcionReporte = "Reporte de Compras con pago en Depósito Bancario"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                            strDescripcionReporte = "Reporte de Compras pago enCheque"
                        End If
                        If intFormaPago <> 0 Then
                            Try
                                dtbDatos = servicioReportes.ObtenerReporteComprasPorProveedor(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor, StaticTipoNulo.NoNulo, intFormaPago)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                            reptCompras.SetDataSource(dtbDatos)
                            reptCompras.SetParameterValue(0, strUsuario)
                            reptCompras.SetParameterValue(1, strEmpresa)
                            reptCompras.SetParameterValue(2, strDescripcionReporte)
                            formReport.crtViewer.ReportSource = reptCompras
                            formReport.ShowDialog()
                        End If
                    End If
                Case "Compras Anuladas"
                    Dim reptCompras As New rptCompras
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteComprasPorProveedor(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor, StaticTipoNulo.Nulo, 0)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptCompras.SetDataSource(dtbDatos)
                    reptCompras.SetParameterValue(0, strUsuario)
                    reptCompras.SetParameterValue(1, strEmpresa)
                    reptCompras.SetParameterValue(2, "Reporte de Compras Anuladas")
                    formReport.crtViewer.ReportSource = reptCompras
                    formReport.ShowDialog()
                Case "Cuentas por Cobrar a Clientes"
                    Dim reptCxC As New rptCuentasxCobrar
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteCuentasPorCobrarClientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptCxC.SetDataSource(dtbDatos)
                    reptCxC.SetParameterValue(0, strUsuario)
                    reptCxC.SetParameterValue(1, strEmpresa)
                    reptCxC.SetParameterValue(2, "Reporte de Cuentas por Cobrar a Clientes")
                    formReport.crtViewer.ReportSource = reptCxC
                    formReport.ShowDialog()
                Case "Cuentas por Pagar a Proveedores"
                    Dim reptCxP As New rptCuentasxPagar
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteCuentasPorPagarProveedores(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptCxP.SetDataSource(dtbDatos)
                    reptCxP.SetParameterValue(0, strUsuario)
                    reptCxP.SetParameterValue(1, strEmpresa)
                    reptCxP.SetParameterValue(2, "Reporte de Cuentas por Pagar a Proveedores")
                    formReport.crtViewer.ReportSource = reptCxP
                    formReport.ShowDialog()
                Case "Pagos a Cuentas por Cobrar de Clientes"
                    Dim reptReciboCxC As New rptReciboCxC
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteMovimientosCxCClientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptReciboCxC.SetDataSource(dtbDatos)
                    reptReciboCxC.SetParameterValue(0, strUsuario)
                    reptReciboCxC.SetParameterValue(1, strEmpresa)
                    reptReciboCxC.SetParameterValue(2, "Reporte de Recibos Aplicados a Cuentas por Cobrar de Clientes")
                    formReport.crtViewer.ReportSource = reptReciboCxC
                    formReport.ShowDialog()
                Case "Pagos a Cuentas por Pagar de Proveedores"
                    Dim reptReciboCxP As New rptReciboCxP
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteMovimientosCxPProveedores(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptReciboCxP.SetDataSource(dtbDatos)
                    reptReciboCxP.SetParameterValue(0, strUsuario)
                    reptReciboCxP.SetParameterValue(1, strEmpresa)
                    reptReciboCxP.SetParameterValue(2, "Reporte de Recibos Aplicados a Cuentas por Pagar de Proveedores")
                    formReport.crtViewer.ReportSource = reptReciboCxP
                    formReport.ShowDialog()
                Case "Conciliación Bancaria"
                    Dim reptConciliacionBancaria As New rptConciliacionBancaria
                    Dim formaCuentaBanco As New FrmMenuCuentaBanco
                    If formaCuentaBanco.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        If FrmMenuPrincipal.intBusqueda > 0 Then
                            Try
                                dtbDatos = servicioReportes.ObtenerReporteMovimientosBanco(FrmMenuPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                            reptConciliacionBancaria.SetDataSource(dtbDatos)
                            reptConciliacionBancaria.SetParameterValue(0, strUsuario)
                            reptConciliacionBancaria.SetParameterValue(1, strEmpresa)
                            formReport.crtViewer.ReportSource = reptConciliacionBancaria
                            formReport.ShowDialog()
                        End If
                    End If
                Case "Resumen de Movimientos"
                    Dim reptResumenMovimientos As New rptResumenMovimientos
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteEstadoResultados(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptResumenMovimientos.SetDataSource(dtbDatos)
                    reptResumenMovimientos.SetParameterValue(0, strUsuario)
                    reptResumenMovimientos.SetParameterValue(1, strEmpresa)
                    formReport.crtViewer.ReportSource = reptResumenMovimientos
                    formReport.ShowDialog()
                Case "Detalle de Egresos"
                    Dim reptDetalleEgresos As New rptDetalleEgresos
                    Dim formaCuentaEgreso As New FrmMenuCuentaEgreso
                    If formaCuentaEgreso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            dtbDatos = servicioReportes.ObtenerReporteDetalleEgreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FrmMenuPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        reptDetalleEgresos.SetDataSource(dtbDatos)
                        reptDetalleEgresos.SetParameterValue(0, strUsuario)
                        reptDetalleEgresos.SetParameterValue(1, strEmpresa)
                        formReport.crtViewer.ReportSource = reptDetalleEgresos
                        formReport.ShowDialog()
                    End If
                Case "Detalle de Ingresos"
                    Dim reptDetalleIngresos As New rptDetalleIngresos
                    Dim formaCuentaIngreso As New FrmMenuCuentaIngreso
                    If formaCuentaIngreso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            dtbDatos = servicioReportes.ObtenerReporteDetalleIngreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FrmMenuPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        reptDetalleIngresos.SetDataSource(dtbDatos)
                        reptDetalleIngresos.SetParameterValue(0, strUsuario)
                        reptDetalleIngresos.SetParameterValue(1, strEmpresa)
                        formReport.crtViewer.ReportSource = reptDetalleIngresos
                        formReport.ShowDialog()
                    End If
                Case "Reporte Resumido de Ventas por Línea"
                    Dim reptVentasxLineaResumen As New rptVentasxLineaResumen
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteVentasPorLineaResumen(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptVentasxLineaResumen.SetDataSource(dtbDatos)
                    reptVentasxLineaResumen.SetParameterValue(0, strUsuario)
                    reptVentasxLineaResumen.SetParameterValue(1, strEmpresa)
                    formReport.crtViewer.ReportSource = reptVentasxLineaResumen
                    formReport.ShowDialog()
                Case "Reporte Detallado de Ventas por Línea"
                    Dim reptVentasxLineaDetalle As New rptVentasxLineaDetalle
                    Dim formaMenuLinea As New FrmMenuLinea
                    If formaMenuLinea.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            dtbDatos = servicioReportes.ObtenerReporteVentasPorLineaDetalle(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FrmMenuPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        reptVentasxLineaDetalle.SetDataSource(dtbDatos)
                        reptVentasxLineaDetalle.SetParameterValue(0, strUsuario)
                        reptVentasxLineaDetalle.SetParameterValue(1, strEmpresa)
                        formReport.crtViewer.ReportSource = reptVentasxLineaDetalle
                        formReport.ShowDialog()
                    End If
            End Select
            Valida = ""
            cliente = Nothing
            proveedor = Nothing
            txtCliente.Text = ""
            txtProveedor.Text = ""
            Valida = "1"
        Else
            MsgBox("Debe seleccionar un reporte de la lista.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
            servicioReportes = FrmMenuPrincipal.unityContainer.Resolve(Of IReporteService)()
            servicioCompras = FrmMenuPrincipal.unityContainer.Resolve(Of ICompraService)()
            servicioFacturacion = FrmMenuPrincipal.unityContainer.Resolve(Of IFacturacionService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Valida = ""
        FechaInicio.Text = "01/" & Date.Now.Month & "/" & Date.Now.Year
        FechaFinal.Text = Date.DaysInMonth(Date.Now.Year, Date.Now.Month) & "/" & Date.Now.Month & "/" & Date.Now.Year
        Valida = "1"
    End Sub

    Private Sub LstReporte_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles LstReporte.DoubleClick
        CmdVistaPrevia_Click(CmdVistaPrevia, New EventArgs())
    End Sub
#End Region
End Class