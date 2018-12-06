﻿Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity

Public Class FrmUsuarioListado
#Region "Variables"
    Private servicioMantenimiento As IMantenimientoService
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcModifica As New DataGridViewCheckBoxColumn
        Dim dvcAutoriza As New DataGridViewCheckBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdUsuario"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.DataPropertyName = "CodigoUsuario"
        dvcCodigo.Width = 450
        dgvDatos.Columns.Add(dvcCodigo)
        dvcModifica.HeaderText = "Modifica"
        dvcModifica.DataPropertyName = "Modifica"
        dvcModifica.Width = 75
        dgvDatos.Columns.Add(dvcModifica)
        dvcAutoriza.HeaderText = "Autoriza"
        dvcAutoriza.DataPropertyName = "AutorizaCredito"
        dvcAutoriza.Width = 75
        dgvDatos.Columns.Add(dvcAutoriza)
    End Sub

    Private Sub ActualizarDatos()
        Try
            Dim listado As IList = servicioMantenimiento.ObtenerListaUsuarios(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text)
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
    Private Sub FrmUsuarioListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
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

    Private Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmUsuario With {
            .intIdUsuario = 0,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        ActualizarDatos()
    End Sub

    Private Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmUsuario With {
            .intIdUsuario = dgvDatos.CurrentRow.Cells(0).Value,
            .servicioMantenimiento = servicioMantenimiento
        }
        formMant.ShowDialog()
        dgvDatos.Refresh()
    End Sub

    Private Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                servicioMantenimiento.EliminarUsuario(dgvDatos.CurrentRow.Cells(0).Value)
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