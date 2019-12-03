﻿Imports System.Collections.Generic
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports Microsoft.Reporting.WinForms
Imports System.Reflection
Imports System.IO

Public Class FrmMenuReportes
#Region "Variables"
    Private strUsuario, Valida, strEmpresa As String
    Private proveedor As Proveedor
    Private cliente As Cliente
    Private newFormReport As FrmReportViewer
    Private assembly As Assembly = Assembly.LoadFrom("Core.dll")
#End Region

#Region "Métodos"
#End Region

#Region "Eventos Controles"
    Private Async Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                cliente = Await Puntoventa.ObtenerCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtCliente.Text = cliente.Nombre
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If
    End Sub

    Private Async Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                proveedor = Await Puntoventa.ObtenerProveedor(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtProveedor.Text = proveedor.Nombre
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If
    End Sub

    Private Async Sub CmdVistaPrevia_Click(sender As Object, e As EventArgs) Handles CmdVistaPrevia.Click
        Dim intIdCliente As Integer
        Dim intIdProveedor As Integer
        Dim dtListaFormaPago As List(Of LlaveDescripcion)
        If LstReporte.SelectedIndex >= 0 Then
            strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
            strEmpresa = IIf(FrmPrincipal.empresaGlobal.NombreComercial = "", FrmPrincipal.empresaGlobal.NombreEmpresa, FrmPrincipal.empresaGlobal.NombreComercial)
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
            CmdVistaPrevia.Enabled = False
            newFormReport = New FrmReportViewer
            newFormReport.Visible = False
            Select Case LstReporte.Text
                Case "Ventas en general"
                    Dim datosReporte As List(Of ReporteVentas)
                    Dim formaMenuTipoTransaccion As New FrmMenuTipoTransaccion
                    Dim banco As BancoAdquiriente
                    Dim intFormaPago = 0
                    Dim intBancoAdquiriente = 0
                    Dim strDescripcionReporte As String = ""
                    Try
                        dtListaFormaPago = Await Puntoventa.ObtenerListadoCondicionVentaYFormaPagoFactura(FrmPrincipal.usuarioGlobal.Token)
                        formaMenuTipoTransaccion.clFormasPago = dtListaFormaPago
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    If formaMenuTipoTransaccion.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        intFormaPago = FrmPrincipal.intBusqueda
                        If intFormaPago = -1 Then
                            strDescripcionReporte = "Reporte de Ventas Generales"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.Credito Then
                            strDescripcionReporte = "Reporte de Ventas de Credito"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoEfectivo Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Efectivo"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Depósito Bancario"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Cheque"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTarjeta Then
                            Dim formaBancoAdquiriente As New FrmMenuBancoAdquiriente
                            If formaBancoAdquiriente.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                                intBancoAdquiriente = FrmPrincipal.intBusqueda
                                banco = Await Puntoventa.ObtenerBancoAdquiriente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                                strDescripcionReporte = "Reporte de Ventas con Tarjeta del Banco " & banco.Descripcion
                            Else
                                intFormaPago = 0
                            End If
                        End If
                        If intFormaPago <> 0 Then
                            Try
                                datosReporte = Await Puntoventa.ObtenerReporteVentasPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.NoNulo, intFormaPago, intBancoAdquiriente, FrmPrincipal.usuarioGlobal.Token)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                CmdVistaPrevia.Enabled = True
                                Exit Sub
                            End Try
                            Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                            newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                            newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                            newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptVentas.rdlc")
                            newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                            Dim parameters(4) As ReportParameter
                            parameters(0) = New ReportParameter("pUsuario", strUsuario)
                            parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                            parameters(2) = New ReportParameter("pNombreReporte", strDescripcionReporte)
                            parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                            parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                            newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                            newFormReport.ShowDialog()
                            CmdVistaPrevia.Enabled = True
                        End If
                    End If
                Case "Ventas anuladas"
                    Dim datosReporte As List(Of ReporteVentas)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteVentasPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.Nulo, -1, 0, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptVentas.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Ventas Anuladas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Ventas por vendedor"
                    Dim datosReporte As List(Of ReporteVentasPorVendedor)
                    Dim formaMenuVendedor As New FrmMenuVendedor
                    If formaMenuVendedor.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteVentasPorVendedor(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptVentasPorVendedor.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(3) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    End If
                'Case "Compras en general"
                '    Dim reptCompras As New rptCompras
                '    Dim datosReporte As List(Of ReporteCompras)
                '    Dim formaMenuTipoTransaccion As New FrmMenuTipoTransaccion
                '    Dim intFormaPago = 0
                '    Dim intBancoAdquiriente = 0
                '    Dim strDescripcionReporte = ""
                '    Try
                '        dtListaFormaPago = Await Puntoventa.ObtenerListadoCondicionVentaYFormaPagoCompra()
                '        formaMenuTipoTransaccion.clFormasPago = dtListaFormaPago
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        CmdVistaPrevia.Enabled = True
                '        Exit Sub
                '    End Try
                '    If formaMenuTipoTransaccion.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '        intFormaPago = FrmPrincipal.intBusqueda
                '        If intFormaPago = -1 Then
                '            strDescripcionReporte = "Reporte de Compras Generales"
                '        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.Credito Then
                '            strDescripcionReporte = "Reporte de Compras con pago en Efectivo"
                '        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoEfectivo Then
                '            strDescripcionReporte = "Reporte de Compras de Credito"
                '        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                '            strDescripcionReporte = "Reporte de Compras con pago en Depósito Bancario"
                '        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                '            strDescripcionReporte = "Reporte de Compras pago enCheque"
                '        End If
                '        If intFormaPago <> 0 Then
                '            Try
                '                datosReporte = Await Puntoventa.ObtenerReporteComprasPorProveedor(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor, StaticTipoNulo.NoNulo, intFormaPago, FrmPrincipal.usuarioGlobal.Token)
                '            Catch ex As Exception
                '                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '                Exit Sub
                '            End Try
                '            reptCompras.SetDataSource(datosReporte)
                '            reptCompras.SetParameterValue(0, strUsuario)
                '            reptCompras.SetParameterValue(1, strEmpresa)
                '            reptCompras.SetParameterValue(2, strDescripcionReporte)
                '            formReport.crtViewer.ReportSource = reptCompras
                '            formReport.ShowDialog()
                '        End If
                '    End If
                'Case "Compras anuladas"
                '    Dim reptCompras As New rptCompras
                '    Dim datosReporte As List(Of ReporteCompras)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteComprasPorProveedor(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor, StaticTipoNulo.Nulo, -1, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    reptCompras.SetDataSource(datosReporte)
                '    reptCompras.SetParameterValue(0, strUsuario)
                '    reptCompras.SetParameterValue(1, strEmpresa)
                '    reptCompras.SetParameterValue(2, "Reporte de Compras Anuladas")
                '    formReport.crtViewer.ReportSource = reptCompras
                '    formReport.ShowDialog()
                'Case "Cuentas por cobrar a clientes"
                '    Dim reptCxC As New rptCuentasxCobrar
                '    Dim datosReporte As List(Of ReporteCuentasPorCobrar)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteCuentasPorCobrarClientes(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    reptCxC.SetDataSource(datosReporte)
                '    reptCxC.SetParameterValue(0, strUsuario)
                '    reptCxC.SetParameterValue(1, strEmpresa)
                '    reptCxC.SetParameterValue(2, "Reporte de Cuentas por Cobrar a Clientes")
                '    formReport.crtViewer.ReportSource = reptCxC
                '    formReport.ShowDialog()
                'Case "Cuentas por pagar a proveedores"
                '    Dim reptCxP As New rptCuentasxPagar
                '    Dim datosReporte As List(Of ReporteCuentasPorPagar)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteCuentasPorPagarProveedores(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    reptCxP.SetDataSource(datosReporte)
                '    reptCxP.SetParameterValue(0, strUsuario)
                '    reptCxP.SetParameterValue(1, strEmpresa)
                '    reptCxP.SetParameterValue(2, "Reporte de Cuentas por Pagar a Proveedores")
                '    formReport.crtViewer.ReportSource = reptCxP
                '    formReport.ShowDialog()
                'Case "Pagos a cuentas por cobrar de clientes"
                '    Dim reptReciboCxC As New rptReciboCxC
                '    Dim datosReporte As List(Of ReporteMovimientosCxC)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteMovimientosCxCClientes(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    reptReciboCxC.SetDataSource(datosReporte)
                '    reptReciboCxC.SetParameterValue(0, strUsuario)
                '    reptReciboCxC.SetParameterValue(1, strEmpresa)
                '    reptReciboCxC.SetParameterValue(2, "Reporte de Recibos Aplicados a Cuentas por Cobrar de Clientes")
                '    formReport.crtViewer.ReportSource = reptReciboCxC
                '    formReport.ShowDialog()
                'Case "Pagos a cuentas por pagar de proveedores"
                '    Dim reptReciboCxP As New rptReciboCxP
                '    Dim datosReporte As List(Of ReporteMovimientosCxP)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteMovimientosCxPProveedores(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, intIdProveedor, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    reptReciboCxP.SetDataSource(datosReporte)
                '    reptReciboCxP.SetParameterValue(0, strUsuario)
                '    reptReciboCxP.SetParameterValue(1, strEmpresa)
                '    reptReciboCxP.SetParameterValue(2, "Reporte de Recibos Aplicados a Cuentas por Pagar de Proveedores")
                '    formReport.crtViewer.ReportSource = reptReciboCxP
                '    formReport.ShowDialog()
                'Case "Conciliación bancaria"
                '    Dim reptConciliacionBancaria As New rptConciliacionBancaria
                '    Dim formaCuentaBanco As New FrmMenuCuentaBanco
                '    Dim datosReporte As List(Of ReporteMovimientosBanco)
                '    If formaCuentaBanco.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '        If FrmPrincipal.intBusqueda > 0 Then
                '            Try
                '                datosReporte = Await Puntoventa.ObtenerReporteMovimientosBanco(FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                '            Catch ex As Exception
                '                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '                Exit Sub
                '            End Try
                '            reptConciliacionBancaria.SetDataSource(datosReporte)
                '            reptConciliacionBancaria.SetParameterValue(0, strUsuario)
                '            reptConciliacionBancaria.SetParameterValue(1, strEmpresa)
                '            formReport.crtViewer.ReportSource = reptConciliacionBancaria
                '            formReport.ShowDialog()
                '        End If
                '    End If
                Case "Resumen de movimientos"
                    Dim datosReporte As List(Of ReporteEstadoResultados)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteEstadoResultados(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptResumenMovimientos.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(3) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Detalle de egresos"
                    Dim formaCuentaEgreso As New FrmMenuCuentaEgreso
                    Dim datosReporte As List(Of ReporteDetalleEgreso)
                    If formaCuentaEgreso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDetalleEgreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalleEgresos.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(3) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    End If
                'Case "Detalle de ingresos"
                '    Dim reptDetalleIngresos As New rptDetalleIngresos
                '    Dim formaCuentaIngreso As New FrmMenuCuentaIngreso
                '    Dim datosReporte As List(Of ReporteDetalleIngreso)
                '    If formaCuentaIngreso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '        Try
                '            datosReporte = Await Puntoventa.ObtenerReporteDetalleIngreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                '        Catch ex As Exception
                '            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Exit Sub
                '        End Try
                '        reptDetalleIngresos.SetDataSource(datosReporte)
                '        reptDetalleIngresos.SetParameterValue(0, strUsuario)
                '        reptDetalleIngresos.SetParameterValue(1, strEmpresa)
                '        formReport.crtViewer.ReportSource = reptDetalleIngresos
                '        formReport.ShowDialog()
                '    End If
                'Case "Reporte resumido de ventas por línea"
                '    Dim reptVentasxLineaResumen As New rptVentasxLineaResumen
                '    Dim datosReporte As List(Of ReporteVentasPorLineaResumen)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteVentasPorLineaResumen(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    If datosReporte.Count = 0 Then
                '        MessageBox.Show("No existen registros de ventas para los parámetros ingresados", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error, FrmPrincipal.usuarioGlobal.Token)
                '        Exit Sub
                '    End If
                '    reptVentasxLineaResumen.SetDataSource(datosReporte)
                '    reptVentasxLineaResumen.SetParameterValue(0, strUsuario)
                '    reptVentasxLineaResumen.SetParameterValue(1, strEmpresa)
                '    formReport.crtViewer.ReportSource = reptVentasxLineaResumen
                '    formReport.ShowDialog()
                'Case "Reporte detallado de ventas por línea"
                '    Dim reptVentasxLineaDetalle As New rptVentasxLineaDetalle
                '    Dim datosReporte As List(Of ReporteVentasPorLineaDetalle)
                '    Dim formaMenuLinea As New FrmMenuLinea
                '    If formaMenuLinea.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '        Try
                '            datosReporte = Await Puntoventa.ObtenerReporteVentasPorLineaDetalle(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                '        Catch ex As Exception
                '            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Exit Sub
                '        End Try
                '        If datosReporte.Count = 0 Then
                '            MessageBox.Show("No existen registros de ventas para los parámetros ingresados", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Exit Sub
                '        End If
                '        reptVentasxLineaDetalle.SetDataSource(datosReporte)
                '        reptVentasxLineaDetalle.SetParameterValue(0, strUsuario)
                '        reptVentasxLineaDetalle.SetParameterValue(1, strEmpresa)
                '        formReport.crtViewer.ReportSource = reptVentasxLineaDetalle
                '        formReport.ShowDialog()
                '    End If
                Case "Facturas electrónicas emitidas"
                    Dim datosReporte As List(Of ReporteDocumentoElectronico)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteFacturasElectronicasEmitidas(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptComprobanteElectronico.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Listado de Facturas Electrónicas Emitidas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Notas de crédito electrónicas emitidas"
                    Dim datosReporte As List(Of ReporteDocumentoElectronico)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteNotasCreditoElectronicasEmitidas(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptComprobanteElectronico.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Listado de Notas de Crédito Electrónicas Emitidas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Facturas electrónicas recibidas"
                    Dim datosReporte As List(Of ReporteDocumentoElectronico)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteFacturasElectronicasRecibidas(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptComprobanteElectronico.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Listado de Facturas Electrónicas Recibidas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Notas de crédito electrónicas recibidas"
                    Dim datosReporte As List(Of ReporteDocumentoElectronico)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteNotasCreditoElectronicasRecibidas(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptComprobanteElectronico.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Listado de Notas de Crédito Electrónicas Recibidas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Resumen de comprobantes electrónicos"
                    Dim datosReporte As List(Of ReporteResumenMovimiento)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteResumenDocumentosElectronicos(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptResumenComprobanteElectronico.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(3) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
            End Select
            CmdVistaPrevia.Enabled = True
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

    Private Sub FrmMenuReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LstReporte.DataSource = FrmPrincipal.lstListaReportes
    End Sub

    Private Sub FrmMenuReportes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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