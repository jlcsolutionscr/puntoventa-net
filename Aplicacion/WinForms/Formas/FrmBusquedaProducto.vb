Imports System.Threading.Tasks
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

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
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta1 As New DataGridViewTextBoxColumn
        dvcIdProducto.DataPropertyName = "Id"
        dvcIdProducto.HeaderText = "Id"
        dvcIdProducto.Visible = False
        dgvListado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.HeaderText = "C�digo"
        dvcCodigo.Width = 100
        dgvListado.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.HeaderText = "Descripci�n"
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

    Private Async Function CargarComboBox() As Task
        cboLinea.DataSource = Await Puntoventa.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboLinea.ValueMember = "Id"
        cboLinea.DisplayMember = "Descripcion"
        cboLinea.SelectedValue = 0
    End Function

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, bolIncluyeServicios, FrmPrincipal.usuarioGlobal.Token, cboLinea.SelectedValue, TxtCodigo.Text, TxtDesc.Text)
            lblPagina.Text = "P�gina " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadProductos() As Task
        Try
            intTotalEmpresas = Await Puntoventa.ObtenerTotalListaProductos(FrmPrincipal.empresaGlobal.IdEmpresa, bolIncluyeServicios, FrmPrincipal.usuarioGlobal.Token, cboLinea.SelectedValue, TxtCodigo.Text, TxtDesc.Text)
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

    Private Async Sub CmdFiltro_Click(sender As Object, e As EventArgs) Handles CmdFiltro.Click
        Await ValidarCantidadProductos()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        If dgvListado.RowCount > 0 Then
            FrmPrincipal.strBusqueda = dgvListado.CurrentRow.Cells(0).Value.ToString()
            Close()
        End If
    End Sub

    Private Async Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarComboBox()
            EstablecerPropiedadesDataGridView()
            Await ValidarCantidadProductos()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub
#End Region
End Class