﻿Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

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
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcCodigoProveedor As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta1 As New DataGridViewTextBoxColumn
        dvcIdProducto.DataPropertyName = "Id"
        dvcIdProducto.HeaderText = "Id"
        dvcIdProducto.Visible = False
        dgvListado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 100
        dgvListado.Columns.Add(dvcCodigo)

        dvcCodigoProveedor.DataPropertyName = "CodigoProveedor"
        dvcCodigoProveedor.HeaderText = "Código Prov"
        dvcCodigoProveedor.Width = 100
        dgvListado.Columns.Add(dvcCodigoProveedor)

        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 320
        dgvListado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "Cantidad"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 48
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PrecioCosto"
        dvcPrecioCosto.HeaderText = "Precio Costo"
        dvcPrecioCosto.Width = 100
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta1.DataPropertyName = "PrecioVenta1"
        dvcPrecioVenta1.HeaderText = "Precio Venta"
        dvcPrecioVenta1.Width = 100
        dvcPrecioVenta1.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcPrecioVenta1)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            listado = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, intNumeroPagina, intFilasPorPagina, True, FrmPrincipal.usuarioGlobal.Token, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
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
            Throw ex
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadProductos() As Task
        Try
            intTotalEmpresas = Await Puntoventa.ObtenerTotalListaProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, True, FrmPrincipal.usuarioGlobal.Token, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
        Catch ex As Exception
            Throw ex
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
        cboLinea.DataSource = Await Puntoventa.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboLinea.ValueMember = "Id"
        cboLinea.DisplayMember = "Descripcion"
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

    Private Async Sub FrmProductoListado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarComboBox()
            EstablecerPropiedadesDataGridView()
            Await ValidarCantidadProductos()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
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
            .intIdProducto = dgvListado.CurrentRow.Cells(0).Value
            }
        formMant.ShowDialog()
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub btnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await Puntoventa.EliminarProducto(dgvListado.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.Token)
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

    Private Async Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        Dim formMant As New FrmProducto With {
            .intIdProducto = dgvListado.CurrentRow.Cells(0).Value
        }
        formMant.ShowDialog()
        Await ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class