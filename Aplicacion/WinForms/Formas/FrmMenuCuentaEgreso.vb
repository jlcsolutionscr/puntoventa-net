Imports System.Collections
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmMenuCuentaEgreso
#Region "Variables"
    Private servicioEgreso As IEgresoService
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        Try
            cboIdCuentaEgreso.ValueMember = "IdCuenta"
            cboIdCuentaEgreso.DisplayMember = "Descripcion"
            cboIdCuentaEgreso.DataSource = servicioEgreso.ObtenerListaCuentasEgreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdCuentaEgreso.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioEgreso = FrmMenuPrincipal.unityContainer.Resolve(Of IEgresoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdCuentaEgreso.SelectedValue IsNot Nothing Then
            FrmMenuPrincipal.intBusqueda = cboIdCuentaEgreso.SelectedValue
            Close()
        Else
            FrmMenuPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class