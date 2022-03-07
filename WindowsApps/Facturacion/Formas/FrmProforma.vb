Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes

Public Class FrmProforma
#Region "Variables"
    Private decDescuento, decExcento, decGravado, decExonerado, decImpuesto, decTotal, decSubTotal, decPrecioVenta As Decimal
    Private consecDetalle As Short
    Private dtbDetalleProforma As DataTable
    Private dtrRowDetProforma As DataRow
    Private proforma As Proforma
    Private detalleProforma As DetalleProforma
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private bolReady As Boolean = False
    Private bolAutorizando As Boolean = False
    Private provider As CultureInfo = CultureInfo.InvariantCulture
    'Impresion de tiquete
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private arrDetalleOrden As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleProforma = New DataTable()
        dtbDetalleProforma.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("CODIGO", GetType(String))
        dtbDetalleProforma.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleProforma.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PRECIOIVA", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PORCDESCUENTO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("VALORDESCUENTO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("ID", GetType(Integer))
        dtbDetalleProforma.PrimaryKey = {dtbDetalleProforma.Columns(11)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleProforma.Columns.Clear()
        grdDetalleProforma.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPorcDescuento As New DataGridViewTextBoxColumn
        Dim dvcDescuento As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn
        Dim dvcId As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleProforma.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 110
        dvcCodigo.ReadOnly = True
        dvcCodigo.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleProforma.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.ReadOnly = True
        dvcDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleProforma.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.ReadOnly = True
        dvcCantidad.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcCantidad)

        dvcPorcDescuento.DataPropertyName = "PORCDESCUENTO"
        dvcPorcDescuento.HeaderText = "Des%"
        dvcPorcDescuento.Width = 40
        dvcPorcDescuento.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPorcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcPorcDescuento)

        dvcDescuento.DataPropertyName = "VALORDESCUENTO"
        dvcDescuento.HeaderText = "Desc/U"
        dvcDescuento.Width = 75
        dvcDescuento.ReadOnly = True
        dvcDescuento.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcDescuento)

        dvcPrecio.DataPropertyName = "PRECIOIVA"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.ReadOnly = True
        dvcPrecio.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.ReadOnly = True
        dvcTotal.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.ReadOnly = True
        dvcExc.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleProforma.Columns.Add(dvcExc)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Visible = False
        grdDetalleProforma.Columns.Add(dvcPorcentajeIVA)

        dvcId.DataPropertyName = "ID"
        dvcId.HeaderText = "Id"
        dvcId.Visible = False
        grdDetalleProforma.Columns.Add(dvcId)
    End Sub

    Private Sub CargarDetalleProforma(proforma As Proforma)
        dtbDetalleProforma.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleProforma In proforma.DetalleProforma
            consecDetalle += 1
            dtrRowDetProforma = dtbDetalleProforma.NewRow
            dtrRowDetProforma.Item(0) = detalle.IdProducto
            dtrRowDetProforma.Item(1) = detalle.Producto.Codigo
            dtrRowDetProforma.Item(2) = detalle.Descripcion
            dtrRowDetProforma.Item(3) = detalle.Cantidad
            dtrRowDetProforma.Item(4) = detalle.PrecioVenta
            dtrRowDetProforma.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2)
            dtrRowDetProforma.Item(6) = dtrRowDetProforma.Item(3) * dtrRowDetProforma.Item(5)
            dtrRowDetProforma.Item(7) = detalle.Excento
            dtrRowDetProforma.Item(8) = detalle.PorcentajeIVA
            dtrRowDetProforma.Item(9) = detalle.PorcDescuento
            dtrRowDetProforma.Item(10) = (dtrRowDetProforma.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetProforma.Item(5)
            dtrRowDetProforma.Item(11) = consecDetalle
            dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        Next
        grdDetalleProforma.Refresh()
    End Sub

    Private Sub CargarLineaDetalleProforma(producto As Producto, strDescripcion As String, decCantidad As Decimal, decPrecio As Decimal, decPorcDesc As Decimal)
        Dim decTasaImpuesto As Decimal = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
        If cliente.AplicaTasaDiferenciada Then
            decTasaImpuesto = FrmPrincipal.ObtenerTarifaImpuesto(cliente.IdImpuesto)
        End If
        Dim decPrecioGravado As Decimal = decPrecio
        If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 5)
        Dim intIndice As Integer = ObtenerIndice(dtbDetalleProforma, producto.IdProducto)
        If producto.Tipo = 1 And intIndice >= 0 Then
            Dim decNewCantidad = dtbDetalleProforma.Rows(intIndice).Item(3) + decCantidad
            dtbDetalleProforma.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleProforma.Rows(intIndice).Item(3) = decNewCantidad
            dtbDetalleProforma.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleProforma.Rows(intIndice).Item(5) = decPrecio
            dtbDetalleProforma.Rows(intIndice).Item(6) = decNewCantidad * decPrecio
            dtbDetalleProforma.Rows(intIndice).Item(7) = decTasaImpuesto = 0
            dtbDetalleProforma.Rows(intIndice).Item(8) = decTasaImpuesto
            dtbDetalleProforma.Rows(intIndice).Item(9) = decPorcDesc
            dtbDetalleProforma.Rows(intIndice).Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
        Else
            consecDetalle += 1
            dtrRowDetProforma = dtbDetalleProforma.NewRow
            dtrRowDetProforma.Item(0) = producto.IdProducto
            dtrRowDetProforma.Item(1) = producto.Codigo
            dtrRowDetProforma.Item(2) = strDescripcion
            dtrRowDetProforma.Item(3) = decCantidad
            dtrRowDetProforma.Item(4) = decPrecioGravado
            dtrRowDetProforma.Item(5) = decPrecio
            dtrRowDetProforma.Item(6) = decCantidad * decPrecio
            dtrRowDetProforma.Item(7) = decTasaImpuesto = 0
            dtrRowDetProforma.Item(8) = decTasaImpuesto
            dtrRowDetProforma.Item(9) = decPorcDesc
            dtrRowDetProforma.Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
            dtrRowDetProforma.Item(11) = consecDetalle
            dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        End If
        grdDetalleProforma.Refresh()
        CargarTotales()
    End Sub

    Private Function ObtenerIndice(table As DataTable, intValor As Integer) As Integer
        Dim intIndice As Integer = -1
        Dim intPosicion As Integer = 0
        For Each row As DataRow In table.Rows
            If row(0) = intValor Then intIndice = intPosicion
            intPosicion += 1
        Next
        Return intIndice
    End Function

    Private Sub CargarTotales()
        decSubTotal = 0
        decDescuento = 0
        decGravado = 0
        decExonerado = 0
        decExcento = 0
        decImpuesto = 0
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I As Short = 0 To dtbDetalleProforma.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleProforma.Rows(I).Item(8)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleProforma.Rows(I).Item(4) * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = dtbDetalleProforma.Rows(I).Item(4) * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += Math.Round(decGravadoPorcentual * dtbDetalleProforma.Rows(I).Item(3), 2)
                    decExonerado += Math.Round((dtbDetalleProforma.Rows(I).Item(4) - decGravadoPorcentual) * dtbDetalleProforma.Rows(I).Item(3), 2)
                    decImpuestoProducto = decGravadoPorcentual * decTasaImpuesto / 100
                Else
                    decGravado += Math.Round(dtbDetalleProforma.Rows(I).Item(3) * dtbDetalleProforma.Rows(I).Item(4), 2)
                End If
                decImpuesto += Math.Round(decImpuestoProducto * dtbDetalleProforma.Rows(I).Item(3), 2)
            Else
                decExcento += Math.Round(dtbDetalleProforma.Rows(I).Item(4) * dtbDetalleProforma.Rows(I).Item(3), 2)
            End If
            decDescuento += dtbDetalleProforma.Rows(I).Item(10) * dtbDetalleProforma.Rows(I).Item(3)
        Next
        decSubTotal = decGravado + decExcento + decExonerado
        decDescuento = Math.Round(decDescuento, 2)
        decGravado = Math.Round(decGravado, 2)
        decExonerado = Math.Round(decExonerado, 2)
        decExcento = Math.Round(decExcento, 2)
        decImpuesto = Math.Round(decImpuesto, 2)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2)
        txtSubTotal.Text = FormatNumber(decSubTotal + decDescuento, 2)
        txtDescuento.Text = FormatNumber(decDescuento, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
        txtSubTotal.Text = FormatNumber(decSubTotal + decDescuento, 2)
        txtDescuento.Text = FormatNumber(decDescuento, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
    End Sub

    Private Function ObtenerPrecioVentaPorCliente(cliente As Cliente, producto As Producto)
        Dim decPrecioVenta As Decimal
        If cliente IsNot Nothing Then
            If cliente.IdTipoPrecio = 1 Then
                decPrecioVenta = producto.PrecioVenta1
            ElseIf cliente.IdTipoPrecio = 2 Then
                decPrecioVenta = producto.PrecioVenta2
            ElseIf cliente.IdTipoPrecio = 3 Then
                decPrecioVenta = producto.PrecioVenta3
            ElseIf cliente.IdTipoPrecio = 4 Then
                decPrecioVenta = producto.PrecioVenta4
            ElseIf cliente.IdTipoPrecio = 5 Then
                decPrecioVenta = producto.PrecioVenta5
            Else
                decPrecioVenta = producto.PrecioVenta1
            End If
        Else
            decPrecioVenta = producto.PrecioVenta1
        End If
        If cliente.AplicaTasaDiferenciada Then
            Dim decTasaImpuestoCliente = FrmPrincipal.ObtenerTarifaImpuesto(cliente.IdImpuesto)
            Dim decTasaImpuestoProducto = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
            decPrecioVenta = Math.Round(decPrecioVenta / (1 + (decTasaImpuestoProducto / 100)), 5)
            decPrecioVenta = Math.Round(decPrecioVenta * (1 + (decTasaImpuestoCliente / 100)), 2)
        End If
        Return decPrecioVenta
    End Function

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtPrecio.Text = FormatNumber(0, 2)
            txtUnidad.Text = ""
            txtCodigo.Focus()
            Exit Sub
        Else
            txtCodigo.Text = producto.Codigo
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtDescripcion.Text = producto.Descripcion
            txtExistencias.Text = producto.Existencias
            txtUnidad.Text = IIf(producto.Tipo = 1, "UND", IIf(producto.Tipo = 2, "SP", "OS"))
            txtPrecio.ReadOnly = Not FrmPrincipal.bolModificaPrecioVenta And Not producto.ModificaPrecio
            txtPorcDesc.Text = FormatNumber(producto.PorcDescuento, 2)
            decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
            If producto.PorcDescuento > 0 Then
                Dim decPorcDesc As Decimal = producto.PorcDescuento / 100
                decPrecioVenta -= (decPrecioVenta * decPorcDesc)
            End If
            txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
        End If
    End Sub

    Private Sub CargarCombos()
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = FrmPrincipal.ObtenerListadoFormaPagoCliente()
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = FrmPrincipal.ObtenerListadoSucursales()
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.bolSeleccionaSucursal
    End Sub

    Private Sub CargarAutoCompletarProducto()
        Dim source As New AutoCompleteStringCollection()
        For Each p As ProductoDetalle In FrmPrincipal.listaProductos
            source.Add(p.Codigo + ": " + p.Descripcion)
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmProforma_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub

    Private Sub FrmProforma_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.F2 Then
            If FrmPrincipal.productoTranstorio IsNot Nothing Then
                Dim formCargar As New FrmCargaProductoTransitorio
                formCargar.ShowDialog()
                If FrmPrincipal.productoTranstorio.PrecioVenta1 > 0 Then
                    CargarLineaDetalleProforma(FrmPrincipal.productoTranstorio, FrmPrincipal.productoTranstorio.Descripcion, FrmPrincipal.productoTranstorio.Existencias, FrmPrincipal.productoTranstorio.PrecioVenta1, 0)
                    FrmPrincipal.productoTranstorio.PrecioVenta1 = 0
                End If
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            BtnBuscar_Click(btnBuscar, New EventArgs())
        ElseIf e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            BtnGuardar_Click(btnGuardar, New EventArgs())
        ElseIf e.KeyCode = Keys.F11 And btnImprimir.Enabled Then
            BtnImprimir_Click(btnImprimir, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Async Sub FrmProforma_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto Then CargarAutoCompletarProducto()
            grdDetalleProforma.DataSource = dtbDetalleProforma
            consecDetalle = 0
            txtCantidad.Text = "1"
            txtPorcDesc.Text = "0"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            cliente = New Cliente With {
                .IdCliente = 1,
                .Nombre = "CLIENTE DE CONTADO",
                .Telefono = "",
                .IdTipoExoneracion = 1,
                .PorcentajeExoneracion = 0,
                .NombreInstExoneracion = "",
                .NumDocExoneracion = "",
                .FechaEmisionDoc = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
            }
            txtNombreCliente.Text = cliente.Nombre
            If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
                Try
                    vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                    txtVendedor.Text = vendedor.Nombre
                Catch ex As Exception
                    Throw New Exception("Debe agregar al menos un vendedor al catalogo de vendedores para poder continuar.")
                End Try
            End If
            If FrmPrincipal.bolModificaDescripcion Then txtDescripcion.ReadOnly = False
            If FrmPrincipal.bolModificaCliente Then txtPorcDesc.ReadOnly = False
            txtCodigo.Focus()
            CargarCombos()
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
            bolReady = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdProforma.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        cboTipoMoneda.Enabled = True
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        txtTextoAdicional.Text = ""
        txtTelefono.Text = ""
        txtTipoExoneracion.Text = ""
        txtNumDocExoneracion.Text = ""
        txtNombreInstExoneracion.Text = ""
        txtFechaExoneracion.Text = ""
        txtPorcentajeExoneracion.Text = ""
        dtbDetalleProforma.Rows.Clear()
        grdDetalleProforma.Refresh()
        consecDetalle = 0
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtExistencias.Text = ""
        txtPorcDesc.Text = "0"
        txtPrecio.Text = ""
        decTotal = 0
        btnGuardar.Enabled = True
        btnAnular.Enabled = False
        btnImprimir.Enabled = False
        btnGenerarPDF.Enabled = False
        btnEnviar.Enabled = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        cliente = New Cliente With {
            .IdCliente = 1,
            .Nombre = "CLIENTE DE CONTADO",
            .Telefono = "",
            .IdTipoExoneracion = 1,
            .PorcentajeExoneracion = 0,
            .NombreInstExoneracion = "",
            .NumDocExoneracion = "",
            .FechaEmisionDoc = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
        }
        txtNombreCliente.Text = cliente.Nombre
        txtNombreCliente.ReadOnly = False
        If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
            Try
                vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                MessageBox.Show("Debe agregar al menos un vendedor al catalogo de vendedores para poder continuar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
        Else
            vendedor = Nothing
            txtVendedor.Text = ""
        End If
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdProforma.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularProforma(proforma.IdProforma, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaProforma()
        formBusqueda.bolIncluyeEstado = True
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                proforma = Await Puntoventa.ObtenerProforma(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If proforma IsNot Nothing Then
                txtIdProforma.Text = proforma.ConsecProforma
                cliente = proforma.Cliente
                txtNombreCliente.Text = proforma.NombreCliente
                txtFecha.Text = proforma.Fecha
                cboSucursal.SelectedValue = proforma.IdSucursal
                cboTipoMoneda.SelectedValue = proforma.IdTipoMoneda
                txtTextoAdicional.Text = proforma.TextoAdicional
                txtTelefono.Text = proforma.Telefono
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = FrmPrincipal.ObtenerDescripcionTipoExoneracion(cliente.IdTipoExoneracion)
                    txtNumDocExoneracion.Text = cliente.NumDocExoneracion
                    txtNombreInstExoneracion.Text = cliente.NombreInstExoneracion
                    txtFechaExoneracion.Text = cliente.FechaEmisionDoc
                    txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                End If
                vendedor = proforma.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleProforma(proforma)
                CargarTotales()
                cboTipoMoneda.Enabled = False
                txtNombreCliente.ReadOnly = IIf(proforma.IdCliente = 1, False, True)
                btnImprimir.Enabled = True
                btnGenerarPDF.Enabled = True
                btnEnviar.Enabled = True
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnGuardar.Enabled = proforma.Aplicado = False
                btnAnular.Enabled = proforma.Aplicado = False And FrmPrincipal.bolAnularTransacciones
            Else
                MessageBox.Show("No existe registro de proforma asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnBuscaVendedor_Click(sender As Object, e As EventArgs) Handles btnBuscaVendedor.Click
        Dim formBusquedaVendedor As New FrmBusquedaVendedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaVendedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                vendedor = Await Puntoventa.ObtenerVendedor(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If vendedor Is Nothing Then
                MessageBox.Show("El vendedor seleccionado no existe. Consulte a su proveedor.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
    End Sub

    Private Async Sub BtnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                cliente = Await Puntoventa.ObtenerCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtNombreCliente.Text = cliente.Nombre
                txtNombreCliente.ReadOnly = True
                txtTelefono.Text = cliente.Telefono
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = FrmPrincipal.ObtenerDescripcionTipoExoneracion(cliente.IdTipoExoneracion)
                    txtNumDocExoneracion.Text = cliente.NumDocExoneracion
                    txtNombreInstExoneracion.Text = cliente.NombreInstExoneracion
                    txtFechaExoneracion.Text = cliente.FechaEmisionDoc
                    txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                Else
                    txtTipoExoneracion.Text = ""
                    txtNumDocExoneracion.Text = ""
                    txtNombreInstExoneracion.Text = ""
                    txtFechaExoneracion.Text = ""
                    txtPorcentajeExoneracion.Text = ""
                End If
                CargarTotales()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If cliente Is Nothing Then
                MessageBox.Show("El cliente de contado no se encuentra registrado en el sistema. Consulte a su proveedor.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
    End Sub

    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = True,
            .intIdSucursal = cboSucursal.SelectedValue
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            Dim intIdProducto As Integer = Integer.Parse(FrmPrincipal.strBusqueda)
            Try
                producto = Await Puntoventa.ObtenerProducto(intIdProducto, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show("Error al obtener la información del producto seleccionado. Intente mas tarde.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
            txtCantidad.Focus()
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If vendedor Is Nothing Then
            MessageBox.Show("Debe seleccionar el vendedor para poder guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnBuscaVendedor_Click(btnBuscaVendedor, New EventArgs())
            Exit Sub
        ElseIf decTotal = 0 Then
            MessageBox.Show("Debe agregar líneas de detalle para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        If txtIdProforma.Text = "" Then
            proforma = New Proforma With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = cboSucursal.SelectedValue,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCliente = cliente.IdCliente,
                .NombreCliente = txtNombreCliente.Text,
                .Fecha = Now(),
                .TextoAdicional = txtTextoAdicional.Text,
                .Telefono = txtTelefono.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = decDescuento,
                .Impuesto = decImpuesto,
                .Nulo = False
            }
            proforma.DetalleProforma = New List(Of DetalleProforma)
            For I As Short = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleProforma = New DetalleProforma With {
                    .IdProducto = dtbDetalleProforma.Rows(I).Item(0),
                    .Codigo = dtbDetalleProforma.Rows(I).Item(1),
                    .Descripcion = dtbDetalleProforma.Rows(I).Item(2),
                    .Cantidad = dtbDetalleProforma.Rows(I).Item(3),
                    .PrecioVenta = dtbDetalleProforma.Rows(I).Item(4),
                    .Excento = dtbDetalleProforma.Rows(I).Item(7),
                    .PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(8),
                    .PorcDescuento = dtbDetalleProforma.Rows(I).Item(9)
                }
                proforma.DetalleProforma.Add(detalleProforma)
            Next
            Try
                Dim strIdConsec As String = Await Puntoventa.AgregarProforma(proforma, FrmPrincipal.usuarioGlobal.Token)
                Dim arrIdConsec = strIdConsec.Split("-")
                proforma.IdProforma = arrIdConsec(0)
                proforma.ConsecProforma = arrIdConsec(1)
                txtIdProforma.Text = proforma.ConsecProforma
            Catch ex As Exception
                txtIdProforma.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            proforma.NombreCliente = txtNombreCliente.Text
            proforma.TextoAdicional = txtTextoAdicional.Text
            proforma.Excento = decExcento
            proforma.Gravado = decGravado
            proforma.Descuento = decDescuento
            proforma.Impuesto = CDbl(txtImpuesto.Text)
            proforma.DetalleProforma.Clear()
            For I As Short = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleProforma = New DetalleProforma
                detalleProforma.IdProforma = proforma.IdProforma
                detalleProforma.IdProducto = dtbDetalleProforma.Rows(I).Item(0)
                detalleProforma.Codigo = dtbDetalleProforma.Rows(I).Item(1)
                detalleProforma.Descripcion = dtbDetalleProforma.Rows(I).Item(2)
                detalleProforma.Cantidad = dtbDetalleProforma.Rows(I).Item(3)
                detalleProforma.PrecioVenta = dtbDetalleProforma.Rows(I).Item(4)
                detalleProforma.Excento = dtbDetalleProforma.Rows(I).Item(7)
                detalleProforma.PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(8)
                detalleProforma.PorcDescuento = dtbDetalleProforma.Rows(I).Item(9)
                proforma.DetalleProforma.Add(detalleProforma)
            Next
            Try
                Await Puntoventa.ActualizarProforma(proforma, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGenerarPDF.Enabled = True
        btnEnviar.Enabled = True
        btnGuardar.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        cboTipoMoneda.Enabled = False
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdProforma.Text <> "" Then
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = proforma.ConsecProforma,
                    .strVendedor = txtVendedor.Text,
                    .strNombre = proforma.NombreCliente,
                    .strTelefono = proforma.Telefono,
                    .strDocumento = proforma.TextoAdicional,
                    .strFecha = proforma.Fecha.ToString("dd/MM/yyyy hh:mm:ss"),
                    .strSubTotal = FormatNumber(proforma.Excento + proforma.Gravado + proforma.Exonerado + proforma.Descuento, 2),
                    .strDescuento = FormatNumber(proforma.Descuento, 2),
                    .strImpuesto = FormatNumber(proforma.Impuesto, 2),
                    .strTotal = FormatNumber(proforma.Total, 2)
                }
                arrDetalleOrden = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For Each item As DetalleProforma In proforma.DetalleProforma
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                        .strDescripcion = item.Codigo + "-" + item.Descripcion,
                        .strCantidad = item.Cantidad,
                        .strPrecio = FormatNumber(item.PrecioVenta, 2),
                        .strTotalLinea = FormatNumber(item.PrecioVenta * item.Cantidad, 2),
                        .strExcento = IIf(item.Excento, "E", "G")
                    }
                    arrDetalleOrden.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleOrden
                ModuloImpresion.ImprimirProforma(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdProforma.Text <> "" Then
            Try
                Dim pdfBytes As Byte() = Await Puntoventa.ObtenerProformaPDF(proforma.IdProforma, FrmPrincipal.usuarioGlobal.Token)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PROFORMA-" + proforma.IdProforma.ToString() + ".pdf"
                File.WriteAllBytes(pdfFilePath, pdfBytes)
                Process.Start(pdfFilePath)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto IsNot Nothing Then
            Dim strError As String = ""
            If txtDescripcion.Text = "" Then strError = "La descripción no puede estar en blanco"
            If decPrecioVenta <= 0 Then strError = "El precio del producto no puede ser igual o menor a 0"
            If strError <> "" Then
                MessageBox.Show(strError, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDetalleProforma(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtUnidad.Text = ""
            txtPorcDesc.Text = "0"
            txtPrecio.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleProforma.Rows.Count > 0 Then
            Dim intId = grdDetalleProforma.CurrentRow.Cells(10).Value
            dtbDetalleProforma.Rows.RemoveAt(dtbDetalleProforma.Rows.IndexOf(dtbDetalleProforma.Rows.Find(intId)))
            grdDetalleProforma.Refresh()
            If dtbDetalleProforma.Rows.Count = 0 Then consecDetalle = 0
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio.KeyUp
        If producto IsNot Nothing Then
            If txtPrecio.Text <> "" And e.KeyCode <> Keys.Tab And e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.ShiftKey Then
                decPrecioVenta = Math.Round(CDbl(txtPrecio.Text), 2)
                txtPorcDesc.Text = "0"
            End If
        End If
    End Sub

    Private Sub cboTipoMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If bolReady And cboTipoMoneda.SelectedValue IsNot Nothing Then
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        End If
    End Sub

    Private Sub grdDetalleProforma_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdDetalleProforma.EditingControlShowing
        If grdDetalleProforma.CurrentCell.ColumnIndex = 4 Then
            Dim tb As TextBox = e.Control
            If tb IsNot Nothing Then
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            End If
        End If
    End Sub

    Private Sub TextBox_keyPress(sender As Object, e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, True, 2)
    End Sub

    Private Sub grdDetalleProforma_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleProforma.CellValueChanged
        If e.ColumnIndex = 4 And Not bolAutorizando Then
            bolAutorizando = True
            Dim bolPrecioAutorizado As Boolean = False
            Dim decPorcDesc As Decimal = 0
            Dim decPrecioTotal As Decimal = dtbDetalleProforma.Rows(e.RowIndex).Item(5) + dtbDetalleProforma.Rows(e.RowIndex).Item(10)
            If Not IsDBNull(grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                decPorcDesc = grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            End If
            If decPorcDesc > FrmPrincipal.usuarioGlobal.PorcMaxDescuento Then
                If MessageBox.Show("El porcentaje ingresado es mayor al parámetro establecido para el usuario actual. Desea ingresar una autorización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    Dim formAutorizacion As New FrmAutorizacionEspecial
                    formAutorizacion.decPorcentaje = decPorcDesc
                    formAutorizacion.decPrecioVenta = decPrecioTotal
                    formAutorizacion.ShowDialog()
                    If FrmPrincipal.decDescAutorizado > 0 Then
                        bolPrecioAutorizado = True
                        decPorcDesc = FrmPrincipal.decDescAutorizado
                    Else
                        MessageBox.Show("No se logró obtener la autorización solicitada.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        decPorcDesc = 0
                    End If
                Else
                    decPorcDesc = 0
                End If
            End If
            grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = decPorcDesc
            Dim decCantidad As Decimal = grdDetalleProforma.Rows(e.RowIndex).Cells(3).Value
            Dim decTasaImpuesto As Decimal = grdDetalleProforma.Rows(e.RowIndex).Cells(9).Value
            Dim decPrecioConDescuento As Decimal = decPrecioTotal - (decPrecioTotal * decPorcDesc / 100)
            If decPorcDesc > 0 And Not bolPrecioAutorizado And FrmPrincipal.empresaGlobal.MontoRedondeoDescuento > 0 Then
                decPrecioConDescuento = Puntoventa.ObtenerPrecioRedondeado(FrmPrincipal.empresaGlobal.MontoRedondeoDescuento, decPrecioConDescuento)
                decPorcDesc = (decPrecioTotal - decPrecioConDescuento) / decPrecioTotal * 100
            End If
            Dim decMontoDesc = decPrecioTotal - decPrecioConDescuento
            Dim decPrecioGravado As Decimal = decPrecioConDescuento
            If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecioConDescuento / (1 + (decTasaImpuesto / 100)), 5)
            dtbDetalleProforma.Rows(e.RowIndex).Item(4) = decPrecioGravado
            dtbDetalleProforma.Rows(e.RowIndex).Item(5) = decPrecioConDescuento
            dtbDetalleProforma.Rows(e.RowIndex).Item(6) = decCantidad * decPrecioConDescuento
            dtbDetalleProforma.Rows(e.RowIndex).Item(9) = decPorcDesc
            dtbDetalleProforma.Rows(e.RowIndex).Item(10) = decMontoDesc
            grdDetalleProforma.Refresh()
            CargarTotales()
            bolAutorizando = False
        End If
    End Sub

    Private Sub txtPorcDesc_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPorcDesc.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Dim bolPrecioAutorizado As Boolean = False
            If txtPorcDesc.Text = "" Then txtPorcDesc.Text = "0"
            Dim decPorcDesc As Decimal = CDbl(txtPorcDesc.Text)
            If producto IsNot Nothing Then
                decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                If decPorcDesc > FrmPrincipal.usuarioGlobal.PorcMaxDescuento Then
                    If MessageBox.Show("El porcentaje ingresado es mayor al parámetro establecido para el usuario actual. Desea ingresar una autorización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        Dim formAutorizacion As New FrmAutorizacionEspecial
                        formAutorizacion.decPorcentaje = decPorcDesc
                        formAutorizacion.decPrecioVenta = decPrecioVenta
                        formAutorizacion.ShowDialog()
                        If FrmPrincipal.decDescAutorizado > 0 Then
                            bolPrecioAutorizado = True
                            decPorcDesc = FrmPrincipal.decDescAutorizado
                        Else
                            MessageBox.Show("No se logró obtener la autorización solicitada.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            txtPorcDesc.Text = 0
                            Exit Sub
                        End If
                    Else
                        txtPorcDesc.Text = 0
                        Exit Sub
                    End If
                End If
                txtPorcDesc.Text = decPorcDesc
                If decPorcDesc > 0 Then
                    Dim decPrecioConDescuento As Decimal = decPrecioVenta - (decPrecioVenta * decPorcDesc / 100)
                    If Not bolPrecioAutorizado And FrmPrincipal.empresaGlobal.MontoRedondeoDescuento > 0 Then
                        decPrecioConDescuento = Puntoventa.ObtenerPrecioRedondeado(FrmPrincipal.empresaGlobal.MontoRedondeoDescuento, decPrecioConDescuento)
                        decPorcDesc = (decPrecioVenta - decPrecioConDescuento) / decPrecioVenta * 100
                        txtPorcDesc.Text = decPorcDesc
                    End If
                    decPrecioVenta = decPrecioConDescuento
                End If
                txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
                If e.KeyCode = Keys.Enter Then BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        If txtIdProforma.Text <> "" Then
            Dim strCorreoReceptor = ""
            If cliente.IdCliente > 1 Then
                If MessageBox.Show("Desea utilizar la dirección(es) de correo electrónico registrada(s) en el cliente?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                    strCorreoReceptor = cliente.CorreoElectronico
                Else
                    strCorreoReceptor = InputBox("Ingrese la(s) dirección(es) de correo electrónico donde se enviará el comprobante, separados por punto y coma:")
                End If
            Else
                strCorreoReceptor = InputBox("Ingrese la(s) dirección(es) de correo electrónico donde se enviará el comprobante, separados por punto y coma:")
            End If
            If strCorreoReceptor <> "" Then
                btnGuardar.Enabled = False
                Try
                    Await Puntoventa.GenerarNotificacionProforma(proforma.IdProforma, strCorreoReceptor, FrmPrincipal.usuarioGlobal.Token)
                    MessageBox.Show("Documento enviado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
                btnEnviar.Enabled = True
            End If
        End If
    End Sub

    Private Sub TxtPrecio_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPrecio.PreviewKeyDown
        If producto IsNot Nothing Then
            If e.KeyCode = Keys.Enter Then
                BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtCodigo.Text <> "" Then
                Dim strCodigo As String = txtCodigo.Text.Split(":")(0)
                Try
                    producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, strCodigo, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
                    If producto IsNot Nothing Then
                        If producto.Activo And producto.Tipo <> StaticTipoProducto.Transitorio Then
                            CargarDatosProducto(producto)
                            txtCantidad.Focus()
                        Else
                            If producto.Activo = False Then MessageBox.Show("El código ingresado pertenece a un producto que se encuentra inactivo", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            txtCodigo.Text = ""
                            txtDescripcion.Text = ""
                            txtExistencias.Text = ""
                            txtCantidad.Text = "1"
                            txtUnidad.Text = ""
                            txtPorcDesc.Text = "0"
                            txtPrecio.Text = ""
                            txtCodigo.Focus()
                        End If
                    Else
                        txtCodigo.Text = ""
                        txtDescripcion.Text = ""
                        txtExistencias.Text = ""
                        txtCantidad.Text = "1"
                        txtUnidad.Text = ""
                        txtPorcDesc.Text = "0"
                        txtPrecio.Text = ""
                        txtCodigo.Focus()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Private Sub TxtCantidad_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCantidad.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If producto IsNot Nothing Then
                If CDbl(txtPrecio.Text) > 0 Then
                    BtnInsertar_Click(btnInsertar, New EventArgs())
                Else
                    txtPrecio.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) Handles txtCantidad.MouseDown, txtCodigo.MouseDown, txtDescripcion.MouseDown, txtPrecio.MouseDown
        sender.SelectAll()
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPorcDesc.KeyPress, txtPrecio.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class