Public Class FrmCatalogoContableListado
#Region "Variables"
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcCuentaContable As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcTipoSaldo As New DataGridViewTextBoxColumn
        Dim dvcPermiteMovimiento As New DataGridViewCheckBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdCuenta"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcCuentaContable.HeaderText = "Cuenta Contable"
        dvcCuentaContable.DataPropertyName = "CuentaContable"
        dvcCuentaContable.Width = 110
        dgvDatos.Columns.Add(dvcCuentaContable)
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 350
        dgvDatos.Columns.Add(dvcDescripcion)
        dvcTipoSaldo.HeaderText = "Tipo"
        dvcTipoSaldo.DataPropertyName = "TipoSaldo"
        dvcTipoSaldo.Width = 90
        dgvDatos.Columns.Add(dvcTipoSaldo)
        dvcPermiteMovimiento.HeaderText = "Mov"
        dvcPermiteMovimiento.DataPropertyName = "PermiteMovimiento"
        dvcPermiteMovimiento.Width = 50
        dgvDatos.Columns.Add(dvcPermiteMovimiento)
    End Sub

    Private Sub ActualizarDatos()
        Try
            Dim listado As IList = Nothing 'servicioContabilidad.ObtenerListaCuentasContables(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtDescripcion.Text)
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
            Close()
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCatalogoContableListado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            ActualizarDatos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        'Dim formMant As New FrmCatalogoContable With {
        '    .intIdCuenta = 0,
        '    .servicioContabilidad = servicioContabilidad
        '}
        'formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        'Dim formMant As New FrmCatalogoContable With {
        '    .intIdCuenta = dgvDatos.CurrentRow.Cells(0).Value,
        '    .servicioContabilidad = servicioContabilidad
        '}
        'formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                'servicioContabilidad.EliminarCuentaContable(dgvDatos.CurrentRow.Cells(0).Value)
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