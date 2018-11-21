Imports System.Collections
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmMenuReportesContables
#Region "Variables"
    Private strUsuario, strEmpresa As String
    Private intMes, intAnnio As Integer
    Private dtbDatos, dtbSubRepDatos As DataTable
    Private servicioReportes As IReporteService
#End Region

#Region "Métodos"
#End Region

#Region "Eventos Controles"
    Private Sub CmdVistaPrevia_Click(sender As Object, e As EventArgs) Handles CmdVistaPrevia.Click
        Dim formReport As New frmRptViewer()
        If LstReporte.SelectedIndex >= 0 Then
            strUsuario = FrmMenuPrincipal.usuarioGlobal.CodigoUsuario
            strEmpresa = FrmMenuPrincipal.empresaGlobal.NombreEmpresa
            intMes = dtpFechaInicial.Value.Month
            intAnnio = dtpFechaInicial.Value.Year
            Select Case LstReporte.Text
                Case "Detalle de Movimientos de Diario"
                    Dim reptMovimientosContables As New rptMovimientosContables
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteMovimientosContables(FrmMenuPrincipal.empresaGlobal.IdEmpresa, dtpFechaInicial.Text, dtpFechaFinal.Text)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptMovimientosContables.SetDataSource(dtbDatos)
                    reptMovimientosContables.SetParameterValue(0, strUsuario)
                    reptMovimientosContables.SetParameterValue(1, strEmpresa)
                    reptMovimientosContables.SetParameterValue(2, dtpFechaInicial.Value)
                    reptMovimientosContables.SetParameterValue(3, dtpFechaFinal.Value)
                    formReport.crtViewer.ReportSource = reptMovimientosContables
                    formReport.ShowDialog()
                Case "Balance de Comprobación Histórico"
                    Dim reptBalanceComprobacion As New rptBalanceComprobacion
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteBalanceComprobacion(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intMes, intAnnio)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptBalanceComprobacion.SetDataSource(dtbDatos)
                    reptBalanceComprobacion.SetParameterValue(0, strUsuario)
                    reptBalanceComprobacion.SetParameterValue(1, strEmpresa)
                    reptBalanceComprobacion.SetParameterValue(2, intMes)
                    reptBalanceComprobacion.SetParameterValue(3, intAnnio)
                    formReport.crtViewer.ReportSource = reptBalanceComprobacion
                    formReport.ShowDialog()
                Case "Balance de Comprobación Actual"
                    Dim reptBalanceComprobacion As New rptBalanceComprobacion
                    Try
                        dtbDatos = servicioReportes.ObtenerReporteBalanceComprobacion(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try

                    reptBalanceComprobacion.SetDataSource(dtbDatos)
                    reptBalanceComprobacion.SetParameterValue(0, strUsuario)
                    reptBalanceComprobacion.SetParameterValue(1, strEmpresa)
                    reptBalanceComprobacion.SetParameterValue(2, Date.Today.Month)
                    reptBalanceComprobacion.SetParameterValue(3, Date.Today.Year)
                    formReport.crtViewer.ReportSource = reptBalanceComprobacion
                    formReport.ShowDialog()
                Case "Balance de Perdidas y Ganancias"
                    Dim reptPerdidasyGanancias As New rptPerdidasyGanacias
                    Try
                        dtbDatos = servicioReportes.ObtenerReportePerdidasyGanancias(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    reptPerdidasyGanancias.SetDataSource(dtbDatos)
                    reptPerdidasyGanancias.SetParameterValue(0, strUsuario)
                    reptPerdidasyGanancias.SetParameterValue(1, strEmpresa)
                    formReport.crtViewer.ReportSource = reptPerdidasyGanancias
                    formReport.ShowDialog()
                Case "Detalle de movimientos de Cuentas de Balance"
                    Dim reptDetalleBalanceComprobacion As New rptDetalleBalanceComprobacion
                    Dim formaCuentaDeBalance As New FrmMenuCuentaDeBalance
                    If formaCuentaDeBalance.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
                        Try
                            dtbDatos = servicioReportes.ObtenerReporteDetalleMovimientosCuentasDeBalance(FrmMenuPrincipal.empresaGlobal.IdEmpresa, FrmMenuPrincipal.intBusqueda, dtpFechaInicial.Text, dtpFechaFinal.Text)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        reptDetalleBalanceComprobacion.SetDataSource(dtbDatos)
                        reptDetalleBalanceComprobacion.SetParameterValue(0, strUsuario)
                        reptDetalleBalanceComprobacion.SetParameterValue(1, strEmpresa)
                        reptDetalleBalanceComprobacion.SetParameterValue(2, dtpFechaInicial.Value)
                        reptDetalleBalanceComprobacion.SetParameterValue(3, dtpFechaFinal.Value)
                        formReport.crtViewer.ReportSource = reptDetalleBalanceComprobacion
                        formReport.ShowDialog()
                    End If
            End Select
        Else
            MsgBox("Debe seleccionar un reporte de la lista.", MsgBoxStyle.Information + MsgBoxStyle.OkOnly)
        End If
    End Sub

    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioReportes = FrmMenuPrincipal.unityContainer.Resolve(Of IReporteService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub LstReporte_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles LstReporte.DoubleClick
        CmdVistaPrevia_Click(CmdVistaPrevia, New EventArgs())
    End Sub
#End Region
End Class