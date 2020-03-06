Imports System.Collections.Generic
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports Microsoft.Reporting.WinForms
Imports System.Reflection
Imports System.IO
Imports System.Threading.Tasks

Public Class FrmMenuReportes
#Region "Variables"
    Private strUsuario, strEmpresa As String
    Private proveedor As Proveedor
    Private cliente As Cliente
    Private newFormReport As FrmReportViewer
    Private assembly As Assembly = Assembly.LoadFrom("Core.dll")
#End Region

#Region "Métodos"
    Private Async Function CargarCombos() As Task
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.usuarioGlobal.Modifica
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmMenuReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LstReporte.DataSource = FrmPrincipal.lstListaReportes
    End Sub

    Private Async Sub FrmMenuReportes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarCombos()
            FechaInicio.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
            FechaFinal.Text = Date.Now.Day & "/" & Date.Now.Month & "/" & Date.Now.Year
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub
    Private Async Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                cliente = Await Puntoventa.ObtenerCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtCliente.Text = cliente.Nombre
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

        End If
    End Sub

    Private Async Sub CmdVistaPrevia_Click(sender As Object, e As EventArgs) Handles CmdVistaPrevia.Click
        Dim intIdCliente As Integer = 0
        Dim intIdProveedor As Integer = 0
        Dim dtListaFormaPago As List(Of LlaveDescripcion)
        If LstReporte.SelectedIndex >= 0 Then
            strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
            strEmpresa = IIf(FrmPrincipal.empresaGlobal.NombreComercial = "", FrmPrincipal.empresaGlobal.NombreEmpresa, FrmPrincipal.empresaGlobal.NombreComercial)
            If cliente IsNot Nothing Then intIdCliente = cliente.IdCliente
            If proveedor IsNot Nothing Then intIdProveedor = proveedor.IdProveedor
            CmdVistaPrevia.Enabled = False
            newFormReport = New FrmReportViewer
            newFormReport.Visible = False
            Select Case LstReporte.Text
                Case "Proformas en general"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteProformas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, StaticTipoNulo.NoNulo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Proformas en General")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Proformas anuladas"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteProformas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, StaticTipoNulo.Nulo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Proformas Anuladas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Apartados en general"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteApartados(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, StaticTipoNulo.NoNulo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Apartados en General")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Apartados anulados"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteApartados(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, StaticTipoNulo.Nulo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Apartados Anulados")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Ordenes de servicio en general"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteOrdenesServicio(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, StaticTipoNulo.NoNulo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Ordenes de Servicio en General")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Ordenes de servicio anuladas"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteOrdenesServicio(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, StaticTipoNulo.Nulo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Ordenes de Servicio en General")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Ventas en general"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Dim formaMenuTipoTransaccion As New FrmMenuTipoTransaccion
                    Dim intFormaPago As Integer
                    Dim strDescripcionReporte As String = ""
                    Try
                        dtListaFormaPago = Await Puntoventa.ObtenerListadoCondicionVentaYFormaPagoFactura(FrmPrincipal.usuarioGlobal.Token)
                        formaMenuTipoTransaccion.clFormasPago = dtListaFormaPago
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    If formaMenuTipoTransaccion.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        intFormaPago = FrmPrincipal.intBusqueda
                        If intFormaPago = -1 Then
                            strDescripcionReporte = "Reporte de Ventas Generales"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.Credito Then
                            strDescripcionReporte = "Reporte de Ventas de Crédito"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoEfectivo Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Efectivo"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Depósito Bancario"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                            strDescripcionReporte = "Reporte de Ventas con pago en Cheque"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTarjeta Then
                            strDescripcionReporte = "Reporte de Ventas con Tarjeta"
                        End If
                        If intFormaPago <> 0 Then
                            Try
                                datosReporte = Await Puntoventa.ObtenerReporteVentasPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.NoNulo, intFormaPago, FrmPrincipal.usuarioGlobal.Token)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                CmdVistaPrevia.Enabled = True
                                Exit Sub
                            End Try
                            Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                            newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                            newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                            newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
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
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteVentasPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.Nulo, -1, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
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
                            datosReporte = Await Puntoventa.ObtenerReporteVentasPorVendedor(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Case "Compras en general"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Dim formaMenuTipoTransaccion As New FrmMenuTipoTransaccion
                    Dim intFormaPago As Integer
                    Dim strDescripcionReporte As String = ""
                    Try
                        dtListaFormaPago = Await Puntoventa.ObtenerListadoCondicionVentaYFormaPagoCompra(FrmPrincipal.usuarioGlobal.Token)
                        formaMenuTipoTransaccion.clFormasPago = dtListaFormaPago
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        CmdVistaPrevia.Enabled = True
                        Exit Sub
                    End Try
                    If formaMenuTipoTransaccion.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        intFormaPago = FrmPrincipal.intBusqueda
                        If intFormaPago = -1 Then
                            strDescripcionReporte = "Reporte de Compras Generales"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.Credito Then
                            strDescripcionReporte = "Reporte de Compras de Crédito"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoEfectivo Then
                            strDescripcionReporte = "Reporte de Compras con pago en Efectivo"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                            strDescripcionReporte = "Reporte de Compras con pago en Transferencia/Depósito"
                        ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                            strDescripcionReporte = "Reporte de Compras pago en Cheque"
                        End If
                        If intFormaPago <> 0 Then
                            Try
                                datosReporte = Await Puntoventa.ObtenerReporteComprasPorProveedor(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdProveedor, StaticTipoNulo.NoNulo, intFormaPago, FrmPrincipal.usuarioGlobal.Token)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                            Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                            newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                            newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                            newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                            newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                            Dim parameters(4) As ReportParameter
                            parameters(0) = New ReportParameter("pUsuario", strUsuario)
                            parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                            parameters(2) = New ReportParameter("pNombreReporte", strDescripcionReporte)
                            parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                            parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                            newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                            newFormReport.ShowDialog()
                        End If
                    End If
                Case "Compras anuladas"
                    Dim datosReporte As List(Of ReporteDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteComprasPorProveedor(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdProveedor, StaticTipoNulo.Nulo, -1, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Compras Anuladas")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Cuentas por cobrar a clientes"
                    Dim datosReporte As List(Of ReporteCuentas)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteCuentasPorCobrarClientes(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptCuentas.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Cuentas por Cobrar a Clientes")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Cuentas por pagar a proveedores"
                    Dim datosReporte As List(Of ReporteCuentas)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteCuentasPorPagarProveedores(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdProveedor, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptCuentas.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Cuentas por Pagar a Proveedores")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Pagos a cuentas por cobrar de clientes"
                    Dim datosReporte As List(Of ReporteGrupoDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteMovimientosCxCClientes(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptGrupoDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Pagos a Cuentas por Cobrar")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                Case "Pagos a cuentas por pagar de proveedores"
                    Dim datosReporte As List(Of ReporteGrupoDetalle)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteMovimientosCxPProveedores(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdProveedor, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                    newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                    newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                    newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                    Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptGrupoDetalle.rdlc")
                    newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                    Dim parameters(4) As ReportParameter
                    parameters(0) = New ReportParameter("pUsuario", strUsuario)
                    parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                    parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Pagos a Cuentas por Pagar")
                    parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                    parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                    newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                    newFormReport.ShowDialog()
                'Case "Conciliación bancaria"
                '    Dim reptConciliacionBancaria As New rptConciliacionBancaria
                '    Dim formaCuentaBanco As New FrmMenuCuentaBanco
                '    Dim datosReporte As List(Of ReporteMovimientosBanco)
                '    If formaCuentaBanco.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                '        If FrmPrincipal.intBusqueda > 0 Then
                '            Try
                '                datosReporte = Await Puntoventa.ObtenerReporteMovimientosBanco(FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                '            Catch ex As Exception
                '                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    Dim datosReporte As List(Of DescripcionValor)
                    Try
                        datosReporte = Await Puntoventa.ObtenerReporteEstadoResultados(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    Dim datosReporte As List(Of ReporteGrupoDetalle)
                    If formaCuentaEgreso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDetalleEgreso(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptGrupoDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(4) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Detalle de egresos")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    End If
                Case "Detalle de ingresos"
                    Dim formaCuentaIngreso As New FrmMenuCuentaIngreso
                    Dim datosReporte As List(Of ReporteGrupoDetalle)
                    If formaCuentaIngreso.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDetalleIngreso(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Core.PlantillaReportes.rptGrupoDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(4) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Detalle de ingresos")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    End If
                'Case "Reporte resumido de ventas por línea"
                '    Dim reptVentasxLineaResumen As New rptVentasxLineaResumen
                '    Dim datosReporte As List(Of ReporteVentasPorLineaResumen)
                '    Try
                '        datosReporte = Await Puntoventa.ObtenerReporteVentasPorLineaResumen(FrmPrincipal.empresaGlobal.IdEmpresa, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                '    Catch ex As Exception
                '        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '        Exit Sub
                '    End Try
                '    If datosReporte.Count = 0 Then
                '        MessageBox.Show("No existen registros de ventas para los parámetros ingresados", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error, FrmPrincipal.usuarioGlobal.Token)
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
                '            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                '            Exit Sub
                '        End Try
                '        If datosReporte.Count = 0 Then
                '            MessageBox.Show("No existen registros de ventas para los parámetros ingresados", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                        datosReporte = Await Puntoventa.ObtenerReporteFacturasElectronicasEmitidas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                        datosReporte = Await Puntoventa.ObtenerReporteNotasCreditoElectronicasEmitidas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                        datosReporte = Await Puntoventa.ObtenerReporteFacturasElectronicasRecibidas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                        datosReporte = Await Puntoventa.ObtenerReporteNotasCreditoElectronicasRecibidas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                        datosReporte = Await Puntoventa.ObtenerReporteResumenDocumentosElectronicos(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            cliente = Nothing
            proveedor = Nothing
            txtCliente.Text = ""
            txtProveedor.Text = ""
        Else
            MsgBox("Debe seleccionar un reporte de la lista.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub LstReporte_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles LstReporte.DoubleClick
        CmdVistaPrevia_Click(CmdVistaPrevia, New EventArgs())
    End Sub
#End Region
End Class