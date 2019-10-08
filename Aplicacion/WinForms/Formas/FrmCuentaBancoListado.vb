Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmCuentaBancoListado
#Region "Variables"
    Private listado As IList
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "Id"
        dvcId.Width = 50
        dgvListado.Columns.Add(dvcId)
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 600
        dgvListado.Columns.Add(dvcDescripcion)
    End Sub

    Private Async Sub ActualizarDatos()
        Try
            listado = Await ClienteFEWCF.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, txtDescripcion.Text)
            dgvListado.DataSource = listado
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
        dgvListado.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCuentaBancoListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        EstablecerPropiedadesDataGridView()
        ActualizarDatos()
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmCuentaBanco With {
            .intIdCuenta = 0
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmCuentaBanco With {
            .intIdCuenta = dgvListado.CurrentRow.Cells(0).Value
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Async Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await ClienteFEWCF.EliminarCuentaBanco(dgvListado.CurrentRow.Cells(0).Value)
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