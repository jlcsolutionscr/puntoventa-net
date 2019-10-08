﻿Imports System.Threading.Tasks
Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmClienteListado
#Region "Variables"
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 16
    Private intCantidadDePaginas As Integer
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

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            listado = Await ClienteFEWCF.ObtenerListadoClientes(FrmPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, txtNombre.Text)
            dgvListado.DataSource = listado
            If listado.Count() > 0 Then
                btnEditar.Enabled = True
                btnEliminar.Enabled = True
            Else
                btnEditar.Enabled = False
                btnEliminar.Enabled = False
            End If
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadClientes() As Task
        Try
            intTotalEmpresas = Await ClienteFEWCF.ObtenerTotalListaClientes(FrmPrincipal.empresaGlobal.IdEmpresa, txtNombre.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        intCantidadDePaginas = Math.Truncate(intTotalEmpresas / intFilasPorPagina) + IIf((intTotalEmpresas Mod intFilasPorPagina) = 0, 0, 1)

        If intCantidadDePaginas > 1 Then
            btnLast.Enabled = True
            btnNext.Enabled = True
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
        Else
            btnLast.Enabled = False
            btnNext.Enabled = False
            btnPrevious.Enabled = False
            btnFirst.Enabled = False
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub FrmClienteListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        EstablecerPropiedadesDataGridView()
        Await ValidarCantidadClientes()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmCliente With {
        .intIdCliente = 0
        }
        formMant.ShowDialog()
        Await ValidarCantidadClientes()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmCliente With {
        .intIdCliente = dgvListado.CurrentRow.Cells(0).Value
        }
        formMant.ShowDialog()
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await ClienteFEWCF.EliminarCliente(dgvListado.CurrentRow.Cells(0).Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Await ValidarCantidadClientes()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        Await ValidarCantidadClientes()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class