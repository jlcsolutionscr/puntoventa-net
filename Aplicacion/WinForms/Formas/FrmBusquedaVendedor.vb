Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmBusquedaVendedor
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn

        FlexVendedor.Columns.Clear()
        FlexVendedor.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdVendedor"
        dvcId.Width = 50
        FlexVendedor.Columns.Add(dvcId)
        dvcNombre.HeaderText = "Nombre"
        dvcNombre.DataPropertyName = "Nombre"
        dvcNombre.Width = 570
        FlexVendedor.Columns.Add(dvcNombre)
    End Sub

    Private Sub ActualizarDatos()
        Try
            FlexVendedor.DataSource = servicioMantenimiento.ObtenerListaVendedores(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtNombre.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        FlexVendedor.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        EstablecerPropiedadesDataGridView()
        ActualizarDatos()
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles FlexVendedor.DoubleClick
        If FlexVendedor.RowCount > 0 Then
            FrmMenuPrincipal.intBusqueda = FlexVendedor.CurrentRow.Cells(0).Value
            Close()
        End If
    End Sub

    Private Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ActualizarDatos()
    End Sub
#End Region
End Class
