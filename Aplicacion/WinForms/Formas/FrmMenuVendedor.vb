Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmMenuVendedor
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        Try
            cboIdVendedor.ValueMember = "IdVendedor"
            cboIdVendedor.DisplayMember = "Nombre"
            cboIdVendedor.DataSource = servicioMantenimiento.ObtenerListaVendedores(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboIdVendedor.SelectedValue = 0
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
        If cboIdVendedor.SelectedValue IsNot Nothing Then
            FrmMenuPrincipal.intBusqueda = cboIdVendedor.SelectedValue
            Close()
        Else
            FrmMenuPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class