Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmBusquedaProducto
#Region "Variables"
    Private producto As Producto
    Public bolIncluyeServicios As Boolean
    Public intTipoPrecio As Integer
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
#End Region

#Region "M�todos"
    Private Sub EstablecerPropiedadesDataGridView()
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dgvNumber As DataGridViewCellStyle
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcPrecioPorMayor As New DataGridViewTextBoxColumn
        dgvNumber = New DataGridViewCellStyle With {
            .Format = "N2",
            .NullValue = "0",
            .Alignment = DataGridViewContentAlignment.MiddleRight
        }

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "Id"
        dvcIdProducto.Visible = False
        dgvListado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "C�digo"
        dvcCodigo.Width = 160
        dgvListado.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripci�n"
        dvcDescripcion.Width = 300
        dgvListado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 50
        dvcCantidad.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcCantidad)
        If intTipoPrecio = 0 Then
            dvcPrecio.DataPropertyName = "PRECIOVENTA"
        Else
            dvcPrecio.DataPropertyName = "PRECIOCOSTO"
        End If
        dvcPrecio.HeaderText = "Precio"
        dvcPrecio.Width = 90
        dvcPrecio.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcPrecio)
        dvcPrecioPorMayor.DataPropertyName = "PRECIOVENTAPORMAYOR"
        dvcPrecioPorMayor.HeaderText = "Prec. Mayor"
        dvcPrecioPorMayor.Width = 90
        dvcPrecioPorMayor.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcPrecioPorMayor)
    End Sub

    Private Sub CargarComboBox()
        Try
            'cboLinea.DataSource = servicioMantenimiento.ObtenerListaLineas(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
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
            'dgvListado.DataSource = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, cboLinea.SelectedValue, TxtCodigo.Text, TxtDesc.Text)
            lblPagina.Text = "P�gina " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        dgvListado.Refresh()
    End Sub

    Private Sub ValidarCantidadEmpresas()
        Try
            'intTotalEmpresas = servicioMantenimiento.ObtenerTotalListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, bolIncluyeServicios, cboLinea.SelectedValue, TxtCodigo.Text, TxtDesc.Text)
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

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        If dgvListado.RowCount > 0 Then
            FrmMenuPrincipal.strBusqueda = dgvListado.CurrentRow.Cells(1).Value.ToString()
            Close()
        End If
    End Sub

    Private Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarComboBox()
        EstablecerPropiedadesDataGridView()
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class