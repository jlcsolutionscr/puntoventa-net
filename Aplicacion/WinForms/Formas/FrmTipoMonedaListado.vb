Imports LeandroSoftware.Puntoventa.Servicios
Imports Unity

Public Class FrmTipoMonedaListado
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcTipoCambioCompra As New DataGridViewTextBoxColumn
        Dim dvcTipoCambioVenta As New DataGridViewTextBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdTipoMoneda"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 400
        dgvDatos.Columns.Add(dvcDescripcion)
        dvcTipoCambioCompra.HeaderText = "TC Compra"
        dvcTipoCambioCompra.DataPropertyName = "TipoCambioCompra"
        dvcTipoCambioCompra.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcTipoCambioCompra.Width = 100
        dgvDatos.Columns.Add(dvcTipoCambioCompra)
        dvcTipoCambioVenta.HeaderText = "TC Venta"
        dvcTipoCambioVenta.DataPropertyName = "TipoCambioVenta"
        dvcTipoCambioVenta.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcTipoCambioVenta.Width = 100
        dgvDatos.Columns.Add(dvcTipoCambioVenta)
    End Sub

    Private Sub ActualizarDatos()
        Try
            Dim listado As IList = servicioMantenimiento.ObtenerListaTipoMoneda(txtDescripcion.Text)
            dgvDatos.DataSource = listado
            If listado.Count() > 0 Then
                btnEditar.Enabled = True
            Else
                btnEditar.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        dgvDatos.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmTipoMonedaListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
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

    Private Sub BtnActualizar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmTipoMoneda With {
            .intIdTipoMoneda = dgvDatos.CurrentRow.Cells(0).Value,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ActualizarDatos()
    End Sub
#End Region
End Class