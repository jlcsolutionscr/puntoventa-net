Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmBancoAdquirienteListado
#Region "Variables"
    Public servicioMantenimiento As IMantenimientoService
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeRetencion As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeComision As New DataGridViewTextBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdBanco"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.Width = 150
        dgvDatos.Columns.Add(dvcCodigo)
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 250
        dgvDatos.Columns.Add(dvcDescripcion)
        dvcPorcentajeRetencion.HeaderText = "% Retenc."
        dvcPorcentajeRetencion.DataPropertyName = "PorcentajeRetencion"
        dvcPorcentajeRetencion.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcPorcentajeRetencion.Width = 100
        dgvDatos.Columns.Add(dvcPorcentajeRetencion)
        dvcPorcentajeComision.HeaderText = "% Comisión"
        dvcPorcentajeComision.DataPropertyName = "PorcentajeComision"
        dvcPorcentajeComision.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcPorcentajeComision.Width = 100
        dgvDatos.Columns.Add(dvcPorcentajeComision)
    End Sub

    Private Sub ActualizarDatos()
        Try
            Dim listado As IList = servicioMantenimiento.ObtenerListaBancoAdquiriente(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtDescripcion.Text)
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
    Private Sub FrmBancoAdquirienteListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
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

    Private Sub BtnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmBancoAdquiriente With {
            .intIdBanco = 0,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub BtnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmBancoAdquiriente With {
            .intIdBanco = dgvDatos.CurrentRow.Cells(0).Value,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub BtnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                servicioMantenimiento.EliminarBancoAdquiriente(dgvDatos.CurrentRow.Cells(0).Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            ActualizarDatos()
        End If
    End Sub

    Private Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ActualizarDatos()
    End Sub
#End Region
End Class