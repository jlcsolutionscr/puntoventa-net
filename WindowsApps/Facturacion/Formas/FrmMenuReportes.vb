Imports Microsoft.Reporting.WinForms
Imports System.Reflection
Imports System.IO
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes
Imports System.Collections.Generic

Public Class FrmMenuReportes
#Region "Variables"
    Private strUsuario, strEmpresa As String
    Private proveedor As Proveedor
    Private cliente As Cliente
    Private newFormReport As FrmReportViewer
    Private assembly As Assembly = Assembly.LoadFrom("Common.dll")
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = FrmPrincipal.ObtenerListadoSucursales()
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.bolSeleccionaSucursal
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmMenuReportes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        LstReporte.DataSource = FrmPrincipal.lstListaReportes
    End Sub

    Private Sub FrmMenuReportes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarCombos()
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
            Try
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Proformas en General")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Proformas Anuladas")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Apartados en General")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Apartados Anulados")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Ordenes de Servicio en General")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Ordenes de Servicio Anuladas")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                                strDescripcionReporte = "Reporte de Ventas con Pago en Efectivo"
                            ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                                strDescripcionReporte = "Reporte de Ventas con Pago en Depósito Bancario"
                            ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                                strDescripcionReporte = "Reporte de Ventas con Pago en Cheque"
                            ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTarjeta Then
                                strDescripcionReporte = "Reporte de Ventas con Pago en Tarjeta"
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
                                Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                                newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                                Dim parameters(5) As ReportParameter
                                parameters(0) = New ReportParameter("pUsuario", strUsuario)
                                parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                                parameters(2) = New ReportParameter("pNombreReporte", strDescripcionReporte)
                                parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                                parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                                parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Ventas Anuladas")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Devoluciones de clientes"
                        Dim datosReporte As List(Of ReporteDetalle)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDevolucionesPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.NoNulo, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Devoluciones de Clientes")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Devoluciones de clientes anuladas"
                        Dim datosReporte As List(Of ReporteDetalle)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDevolucionesPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, StaticTipoNulo.Nulo, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Devoluciones de Clientes")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptVentasPorVendedor.rdlc")
                            newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                            Dim parameters(4) As ReportParameter
                            parameters(0) = New ReportParameter("pUsuario", strUsuario)
                            parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                            parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                            parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                            parameters(4) = New ReportParameter("pSucursal", cboSucursal.Text)
                            newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                            newFormReport.ShowDialog()
                        End If
                    Case "Reporte de costos de inventario"
                        Dim datosReporte As List(Of ReporteInventario)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteInventario(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, False, True, 0, "", "", "", FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim strFecha As String = Now().ToString("dd/MM/yyyy hh:mm:ss")
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport = New FrmReportViewer
                        newFormReport.Visible = False
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptCostoInventario.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(3) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", FrmPrincipal.usuarioGlobal.CodigoUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pSucursal", cboSucursal.Text)
                        parameters(3) = New ReportParameter("pFecha", strFecha)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
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
                                strDescripcionReporte = "Reporte de Compras con Pago en Efectivo"
                            ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoTransferenciaDepositoBancario Then
                                strDescripcionReporte = "Reporte de Compras con Pago en Transferencia/Depósito"
                            ElseIf intFormaPago = StaticReporteCondicionVentaFormaPago.ContadoCheque Then
                                strDescripcionReporte = "Reporte de Compras con Pago en Cheque"
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
                                Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                                newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                                Dim parameters(5) As ReportParameter
                                parameters(0) = New ReportParameter("pUsuario", strUsuario)
                                parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                                parameters(2) = New ReportParameter("pNombreReporte", strDescripcionReporte)
                                parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                                parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                                parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Compras Anuladas")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Cuentas por cobrar a clientes"
                        Dim datosReporte As List(Of ReporteCuentas)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteCuentasPorCobrarClientes(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, True, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptCuentas.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Cuentas por Cobrar a Clientes")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Cuentas por cobrar canceladas"
                        Dim datosReporte As List(Of ReporteCuentas)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteCuentasPorCobrarClientes(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdCliente, False, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptCuentas.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Cuentas por Cobrar Canceladas")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Cuentas por pagar a proveedores"
                        Dim datosReporte As List(Of ReporteCuentas)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteCuentasPorPagarProveedores(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdProveedor, True, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptCuentas.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Cuentas por Pagar a Proveedores")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Cuentas por pagar canceladas"
                        Dim datosReporte As List(Of ReporteCuentas)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteCuentasPorPagarProveedores(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, intIdProveedor, False, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptCuentas.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Cuentas por Pagar Canceladas")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptGrupoDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Pagos a Cuentas por Cobrar")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptGrupoDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Pagos a Cuentas por Pagar")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Conciliación bancaria"
                        Dim datosReporte As List(Of ReporteMovimientosBanco)
                        Dim formaCuentaBanco As New FrmMenuCuentaBanco
                        If formaCuentaBanco.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                            If FrmPrincipal.intBusqueda > 0 Then
                                Try
                                    datosReporte = Await Puntoventa.ObtenerReporteMovimientosBanco(FrmPrincipal.intBusqueda, FrmPrincipal.equipoGlobal.IdSucursal, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                                Catch ex As Exception
                                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                    Exit Sub
                                End Try
                                Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                                newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                                newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                                newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                                Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptMovimientoBanco.rdlc")
                                newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                                Dim parameters(4) As ReportParameter
                                parameters(0) = New ReportParameter("pUsuario", strUsuario)
                                parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                                parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                                parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                                parameters(4) = New ReportParameter("pSucursal", cboSucursal.Text)
                                newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                                newFormReport.ShowDialog()
                            End If
                        End If
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptResumenMovimientos.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(4) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(4) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptGrupoDetalle.rdlc")
                            newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                            Dim parameters(5) As ReportParameter
                            parameters(0) = New ReportParameter("pUsuario", strUsuario)
                            parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                            parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Detalle de Egresos")
                            parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                            parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                            parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptGrupoDetalle.rdlc")
                            newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                            Dim parameters(5) As ReportParameter
                            parameters(0) = New ReportParameter("pUsuario", strUsuario)
                            parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                            parameters(2) = New ReportParameter("pNombreReporte", "Reporte de Detalle de Ingresos")
                            parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                            parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                            parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                            newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                            newFormReport.ShowDialog()
                        End If
                    Case "Resumen de ventas por línea"
                        Dim datosReporte As List(Of DescripcionValor)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteVentasPorLineaResumen(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptResumenLineaDetalle.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(4) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(4) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Detalle de ventas por línea"
                        Dim datosReporte As List(Of ReporteGrupoLineaDetalle)
                        Dim formaMenuLinea As New FrmMenuLinea
                        If formaMenuLinea.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                            Try
                                datosReporte = Await Puntoventa.ObtenerReporteVentasPorLineaDetalle(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FrmPrincipal.intBusqueda, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                            Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                            newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                            newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                            newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                            Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptGrupoLineaDetalle.rdlc")
                            newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                            Dim parameters(4) As ReportParameter
                            parameters(0) = New ReportParameter("pUsuario", strUsuario)
                            parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                            parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                            parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                            parameters(4) = New ReportParameter("pSucursal", cboSucursal.Text)
                            newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                            newFormReport.ShowDialog()
                        End If
                    Case "Documentos electrónicos emitidos"
                        Dim datosReporte As List(Of ReporteDocumentoElectronico)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDocumentosElectronicosEmitidos(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptComprobanteElectronico.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Listado de Documentos Electrónicos Emitidos")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                    Case "Documentos electrónicos recibidos"
                        Dim datosReporte As List(Of ReporteDocumentoElectronico)
                        Try
                            datosReporte = Await Puntoventa.ObtenerReporteDocumentosElectronicosRecibidos(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FechaInicio.Text, FechaFinal.Text, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            CmdVistaPrevia.Enabled = True
                            Exit Sub
                        End Try
                        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
                        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
                        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
                        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptComprobanteElectronico.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(5) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pNombreReporte", "Listado de Documentos Electrónicos Recibidos")
                        parameters(3) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(4) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(5) = New ReportParameter("pSucursal", cboSucursal.Text)
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
                        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptResumenComprobanteElectronico.rdlc")
                        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
                        Dim parameters(4) As ReportParameter
                        parameters(0) = New ReportParameter("pUsuario", strUsuario)
                        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
                        parameters(2) = New ReportParameter("pFechaDesde", FechaInicio.Text)
                        parameters(3) = New ReportParameter("pFechaHasta", FechaFinal.Text)
                        parameters(4) = New ReportParameter("pSucursal", cboSucursal.Text)
                        newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
                        newFormReport.ShowDialog()
                End Select
            Catch ex As Exception
                CmdVistaPrevia.Enabled = True
                MessageBox.Show("Error al procesar el reporte: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            CmdVistaPrevia.Enabled = True
            cliente = Nothing
            proveedor = Nothing
            txtCliente.Text = ""
            txtProveedor.Text = ""
        Else
            MessageBox.Show("Debe seleccionar un reporte de la lista.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub LstReporte_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles LstReporte.DoubleClick
        CmdVistaPrevia_Click(CmdVistaPrevia, New EventArgs())
    End Sub
#End Region
End Class