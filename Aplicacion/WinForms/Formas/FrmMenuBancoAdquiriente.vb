Imports System.Collections
Imports CrystalDecisions.Shared
Imports CrystalDecisions.CrystalReports.Engine
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmMenuBancoAdquiriente
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        Try
            cboIdBancoAdquiriente.ValueMember = "IdBanco"
            cboIdBancoAdquiriente.DisplayMember = "Descripcion"
            cboIdBancoAdquiriente.DataSource = servicioMantenimiento.ObtenerListaBancoAdquiriente(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdBancoAdquiriente.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        CargarCombos()
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboIdBancoAdquiriente.SelectedValue IsNot Nothing Then
            FrmMenuPrincipal.intBusqueda = cboIdBancoAdquiriente.SelectedValue
            Close()
        Else
            MessageBox.Show("Debe seleccionar una cuenta bancaria para continuar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region
End Class