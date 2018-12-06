Imports System.Collections
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmMenuCuentaDeBalance
#Region "Variables"
    Private servicioContabilidad As IContabilidadService
#End Region

#Region "M�todos"
    Private Sub CargarCombos()
        Try
            cboIdCuentaBanco.ValueMember = "IdCuenta"
            cboIdCuentaBanco.DisplayMember = "Descripcion"
            cboIdCuentaBanco.DataSource = servicioContabilidad.ObtenerListaCuentasDeBalance(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        cboIdCuentaBanco.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioContabilidad = FrmMenuPrincipal.unityContainer.Resolve(Of IContabilidadService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdCuentaBanco.SelectedValue IsNot Nothing Then
            FrmMenuPrincipal.intBusqueda = cboIdCuentaBanco.SelectedValue
            Close()
        Else
            MessageBox.Show("Debe seleccionar una cuenta contable para continuar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class