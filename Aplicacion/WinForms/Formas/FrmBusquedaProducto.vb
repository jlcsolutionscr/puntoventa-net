Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmBusquedaProducto
#Region "Variables"
    Private producto As Producto
    Private servicioMantenimiento As IMantenimientoService
    Public bolIncluyeServicios As Boolean
    Public intTipoPrecio As Integer
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        FlexProducto.Columns.Clear()
        FlexProducto.AutoGenerateColumns = False
        Dim dgvNumber As System.Windows.Forms.DataGridViewCellStyle
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcPrecioPorMayor As New DataGridViewTextBoxColumn
        dgvNumber = New System.Windows.Forms.DataGridViewCellStyle With {
            .Format = "N2",
            .NullValue = "0",
            .Alignment = DataGridViewContentAlignment.MiddleRight
        }

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "Id"
        dvcIdProducto.Visible = False
        FlexProducto.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 160
        FlexProducto.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        FlexProducto.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 50
        dvcCantidad.DefaultCellStyle = dgvNumber
        FlexProducto.Columns.Add(dvcCantidad)
        If intTipoPrecio = 0 Then
            dvcPrecio.DataPropertyName = "PRECIOVENTA"
        Else
            dvcPrecio.DataPropertyName = "PRECIOCOSTO"
        End If
        dvcPrecio.HeaderText = "Precio"
        dvcPrecio.Width = 90
        dvcPrecio.DefaultCellStyle = dgvNumber
        FlexProducto.Columns.Add(dvcPrecio)
        dvcPrecioPorMayor.DataPropertyName = "PRECIOVENTAPORMAYOR"
        dvcPrecioPorMayor.HeaderText = "Prec. Mayor"
        dvcPrecioPorMayor.Width = 90
        dvcPrecioPorMayor.DefaultCellStyle = dgvNumber
        FlexProducto.Columns.Add(dvcPrecioPorMayor)
    End Sub

    Private Sub CargarComboBox()
        Try
            cboLinea.DataSource = servicioMantenimiento.ObtenerListaLineas(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboLinea.ValueMember = "IdLinea"
            cboLinea.DisplayMember = "Descripcion"
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboLinea.SelectedValue = 0
    End Sub

    Private Sub ActualizarDatos(ByVal intNumeroPagina As Integer)
        Try
            FlexProducto.DataSource = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, cboLinea.SelectedValue, TxtCodigo.Text, TxtDesc.Text)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        FlexProducto.Refresh()
    End Sub

    Private Sub ValidarCantidadEmpresas()
        Try
            intTotalEmpresas = servicioMantenimiento.ObtenerTotalListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, bolIncluyeServicios, cboLinea.SelectedValue, TxtCodigo.Text, TxtDesc.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
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
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub CmdFiltro_Click(sender As Object, e As EventArgs) Handles CmdFiltro.Click
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles FlexProducto.DoubleClick
        If FlexProducto.RowCount > 0 Then
            FrmMenuPrincipal.strBusqueda = FlexProducto.CurrentRow.Cells(1).Value.ToString()
            Close()
        End If
    End Sub

    Private Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        CargarComboBox()
        EstablecerPropiedadesDataGridView()
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class