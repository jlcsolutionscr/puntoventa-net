Public Class FrmSucursalListado
#Region "Variables"
    Private listado As IList
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcSaldo As New DataGridViewTextBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdSucursal"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcDescripcion.HeaderText = "Nombre"
        dvcDescripcion.DataPropertyName = "Nombre"
        dvcDescripcion.Width = 150
        dgvDatos.Columns.Add(dvcDescripcion)
        dvcCodigo.HeaderText = "Dirección"
        dvcCodigo.DataPropertyName = "Direccion"
        dvcCodigo.Width = 350
        dgvDatos.Columns.Add(dvcCodigo)
        dvcSaldo.HeaderText = "Teléfono"
        dvcSaldo.DataPropertyName = "Telefono"
        dvcSaldo.Width = 100
        dvcSaldo.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dgvDatos.Columns.Add(dvcSaldo)
    End Sub

    Private Sub ActualizarDatos()
        Try
            'listado = servicioTraslados.ObtenerListaSucursales(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtNombre.Text)
            dgvDatos.DataSource = listado
            If listado.Count() > 0 Then
                btnEditar.Enabled = True
                btnEliminar.Enabled = True
            Else
                btnEditar.Enabled = False
                btnEliminar.Enabled = False
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        dgvDatos.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmSucursalListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        EstablecerPropiedadesDataGridView()
        ActualizarDatos()
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        'Dim formMant As New FrmSucursal With {
        '    .intIdSucursal = 0,
        '    .servicioTraslados = servicioTraslados
        '}
        'formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        'Dim formMant As New FrmSucursal With {
        '    .intIdSucursal = dgvDatos.CurrentRow.Cells(0).Value,
        '    .servicioTraslados = servicioTraslados
        '}
        'formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                'servicioTraslados.EliminarSucursal(dgvDatos.CurrentRow.Cells(0).Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ActualizarDatos()
        End If
    End Sub

    Private Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ActualizarDatos()
    End Sub
#End Region
End Class