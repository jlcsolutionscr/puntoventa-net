﻿Imports System.Threading.Tasks
Imports LeandroSoftware.AccesoDatos.ClienteWCF

Public Class FrmProductoListado
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
        Dim dvcTipo As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcExcento As New DataGridViewCheckBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdProducto"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)

        dvcTipo.HeaderText = "Tipo"
        dvcTipo.DataPropertyName = "TipoProductoDesc"
        dvcTipo.Width = 100
        dgvDatos.Columns.Add(dvcTipo)

        dvcCodigo.HeaderText = "Código"
        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.Width = 120
        dgvDatos.Columns.Add(dvcCodigo)

        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 250
        dgvDatos.Columns.Add(dvcDescripcion)

        dvcPrecioVenta.HeaderText = "Precio Venta"
        dvcPrecioVenta.DataPropertyName = "PrecioVenta"
        dvcPrecioVenta.Width = 100
        dvcPrecioVenta.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dgvDatos.Columns.Add(dvcPrecioVenta)

        dvcExcento.HeaderText = "Excento"
        dvcExcento.DataPropertyName = "Excento"
        dvcExcento.Width = 50
        dgvDatos.Columns.Add(dvcExcento)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            listado = Await PuntoventaWCF.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, True, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
            dgvDatos.DataSource = listado
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
        dgvDatos.Refresh()
    End Function

    Private Async Function ValidarCantidadProductos() As Task
        Try
            intTotalEmpresas = Await PuntoventaWCF.ObtenerTotalListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, True, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
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

    Private Async Function CargarComboBox() As Task
        Try
            cboLinea.DataSource = Await PuntoventaWCF.ObtenerListaLineas(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboLinea.ValueMember = "IdLinea"
            cboLinea.DisplayMember = "Descripcion"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        cboLinea.SelectedValue = 0
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub FrmProductoListado_Shown(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Shown
        Await CargarComboBox()
        EstablecerPropiedadesDataGridView()
        Await ValidarCantidadProductos()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub btnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmProducto With {
            .intIdProducto = 0
        }
        formMant.ShowDialog()
        Await ValidarCantidadProductos()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub btnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmProducto With {
            .intIdProducto = dgvDatos.CurrentRow.Cells(0).Value
            }
        formMant.ShowDialog()
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await PuntoventaWCF.EliminarProducto(dgvDatos.CurrentRow.Cells(0).Value)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Await ValidarCantidadProductos()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        Await ValidarCantidadProductos()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class