Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmLineaListado
#Region "Variables"
    Dim listado As IList
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcTipo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdLinea"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcTipo.HeaderText = "Tipo"
        dvcTipo.DataPropertyName = "TipoLineaDesc"
        dvcTipo.Width = 100
        dgvDatos.Columns.Add(dvcTipo)
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 500
        dgvDatos.Columns.Add(dvcDescripcion)
    End Sub

    Private Async Sub ActualizarDatos()
        Try
            listado = Await ClienteFEWCF.ObtenerListaLineas(FrmPrincipal.empresaGlobal.IdEmpresa, txtDescripcion.Text)
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
        dgvDatos.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmLineaListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        EstablecerPropiedadesDataGridView()
        ActualizarDatos()
    End Sub

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmLinea With {
            .intIdLinea = 0
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmLinea With {
        .intIdLinea = dgvDatos.CurrentRow.Cells(0).Value
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Async Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await ClienteFEWCF.EliminarLinea(dgvDatos.CurrentRow.Cells(0).Value)
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