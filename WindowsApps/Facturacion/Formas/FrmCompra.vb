Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF
Imports System.Threading.Tasks

Public Class FrmCompra
#Region "Variables"
    Private decExcento, decGravado, decSaldoPorPagar As Decimal
    Private decTotalPago As Decimal = 0
    Private decTotal As Decimal = 0
    Private I As Short
    Private dtbDetalleCompra, dtbDesglosePago As DataTable
    Private dtrRowDetCompra, dtrRowDesglosePago As DataRow
    Private arrDetalleCompra As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
    Private compra As Compra
    Private ordenCompra As OrdenCompra
    Private proveedor As Proveedor
    Private detalleCompra As DetalleCompra
    Private desglosePago As DesglosePagoCompra
    Private producto As Producto
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleCompra()
        dtbDetalleCompra = New DataTable()
        dtbDetalleCompra.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleCompra.Columns.Add("CODIGO", GetType(String))
        dtbDetalleCompra.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleCompra.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleCompra.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("PRECIOVENTA", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("UTILIDAD", GetType(Decimal))

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDCUENTABANCO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("NROCHEQUE", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Integer))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("TIPODECAMBIO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleCompra.Columns.Clear()
        grdDetalleCompra.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcUtilidad As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleCompra.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 190
        grdDetalleCompra.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 350
        grdDetalleCompra.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "P. Costo"
        dvcPrecioCosto.Width = 80
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 0
        dvcExc.Visible = False
        grdDetalleCompra.Columns.Add(dvcExc)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        grdDetalleCompra.Columns.Add(dvcPorcentajeIVA)

        dvcPrecioVenta.DataPropertyName = "PRECIOVENTA"
        dvcPrecioVenta.HeaderText = "P. Venta"
        dvcPrecioVenta.Width = 80
        dvcPrecioVenta.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcPrecioVenta)

        dvcUtilidad.DataPropertyName = "UTILIDAD"
        dvcUtilidad.HeaderText = "Utilidad"
        dvcUtilidad.Width = 54
        dvcUtilidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcUtilidad)

        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdFormaPago As New DataGridViewTextBoxColumn
        Dim dvcDescFormaPago As New DataGridViewTextBoxColumn
        Dim dvcIdCuentaBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcPlazo As New DataGridViewTextBoxColumn
        Dim dvcNroCheque As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcTipoCambio As New DataGridViewTextBoxColumn

        dvcIdFormaPago.DataPropertyName = "IDFORMAPAGO"
        dvcIdFormaPago.HeaderText = "Id"
        dvcIdFormaPago.Width = 0
        dvcIdFormaPago.Visible = False
        grdDesglosePago.Columns.Add(dvcIdFormaPago)

        dvcDescFormaPago.DataPropertyName = "DESCFORMAPAGO"
        dvcDescFormaPago.HeaderText = "Forma de Pago"
        dvcDescFormaPago.Width = 154
        dvcDescFormaPago.Visible = True
        dvcDescFormaPago.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescFormaPago)

        dvcIdCuentaBanco.DataPropertyName = "IDCUENTABANCO"
        dvcIdCuentaBanco.HeaderText = "IdCuentaBanco"
        dvcIdCuentaBanco.Width = 0
        dvcIdCuentaBanco.Visible = False
        grdDesglosePago.Columns.Add(dvcIdCuentaBanco)

        dvcDescBanco.DataPropertyName = "DESCBANCO"
        dvcDescBanco.HeaderText = "Banco"
        dvcDescBanco.Width = 390
        dvcDescBanco.Visible = True
        dvcDescBanco.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcNroCheque.DataPropertyName = "NROCHEQUE"
        dvcNroCheque.HeaderText = "Nro. Cheque"
        dvcNroCheque.Width = 150
        dvcNroCheque.Visible = True
        dvcNroCheque.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcNroCheque)

        dvcIdTipoMoneda.DataPropertyName = "IDTIPOMONEDA"
        dvcIdTipoMoneda.HeaderText = "TipoMoneda"
        dvcIdTipoMoneda.Width = 0
        dvcIdTipoMoneda.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoMoneda)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 110
        dvcMontoLocal.Visible = True
        dvcMontoLocal.ReadOnly = True
        dvcMontoLocal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcTipoCambio.DataPropertyName = "TIPODECAMBIO"
        dvcTipoCambio.HeaderText = "Tipo Cambio"
        dvcTipoCambio.Width = 110
        dvcTipoCambio.Visible = True
        dvcTipoCambio.ReadOnly = True
        dvcTipoCambio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcTipoCambio)
    End Sub

    Private Sub CargarDetalleCompra(ByVal compra As Compra)
        dtbDetalleCompra.Rows.Clear()
        For Each detalle As DetalleCompra In compra.DetalleCompra
            dtrRowDetCompra = dtbDetalleCompra.NewRow
            dtrRowDetCompra.Item(0) = detalle.IdProducto
            dtrRowDetCompra.Item(1) = detalle.Producto.Codigo
            dtrRowDetCompra.Item(2) = detalle.Producto.Descripcion
            dtrRowDetCompra.Item(3) = detalle.Cantidad
            dtrRowDetCompra.Item(4) = detalle.PrecioCosto
            dtrRowDetCompra.Item(5) = dtrRowDetCompra.Item(3) * dtrRowDetCompra.Item(4)
            dtrRowDetCompra.Item(6) = detalle.PorcentajeIVA = 0
            dtrRowDetCompra.Item(7) = detalle.PorcentajeIVA
            dtrRowDetCompra.Item(8) = detalle.PrecioVenta
            dtrRowDetCompra.Item(9) = (detalle.PrecioVenta * 100 / detalle.PrecioCosto) - 100
            dtbDetalleCompra.Rows.Add(dtrRowDetCompra)
        Next
        grdDetalleCompra.Refresh()
    End Sub

    Private Sub CargarDetalleOrdenCompra(ByVal ordenCompra As OrdenCompra)
        dtbDetalleCompra.Rows.Clear()
        For Each detalle As DetalleOrdenCompra In ordenCompra.DetalleOrdenCompra
            dtrRowDetCompra = dtbDetalleCompra.NewRow
            dtrRowDetCompra.Item(0) = detalle.IdProducto
            dtrRowDetCompra.Item(1) = detalle.Producto.Codigo
            dtrRowDetCompra.Item(2) = detalle.Producto.Descripcion
            dtrRowDetCompra.Item(3) = detalle.Cantidad
            dtrRowDetCompra.Item(4) = detalle.PrecioCosto
            dtrRowDetCompra.Item(5) = dtrRowDetCompra.Item(3) * dtrRowDetCompra.Item(4)
            dtrRowDetCompra.Item(6) = detalle.PorcentajeIVA = 0
            dtrRowDetCompra.Item(7) = detalle.PorcentajeIVA
            dtrRowDetCompra.Item(8) = detalle.PrecioVenta
            dtrRowDetCompra.Item(9) = (detalle.PrecioVenta * 100 / detalle.PrecioCosto) - 100
            dtbDetalleCompra.Rows.Add(dtrRowDetCompra)
        Next
        grdDetalleCompra.Refresh()
    End Sub

    Private Sub CargarDesglosePago(ByVal compra As Compra)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoCompra In compra.DesglosePagoCompra
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = detalle.FormaPago.Descripcion
            dtrRowDesglosePago.Item(2) = detalle.IdCuentaBanco
            dtrRowDesglosePago.Item(3) = detalle.DescripcionCuenta
            dtrRowDesglosePago.Item(4) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(5) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(6) = detalle.MontoLocal
            dtrRowDesglosePago.Item(7) = detalle.TipoDeCambio
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarLineaDetalleCompra(ByVal producto As Producto, intCantidad As Integer, dblPrecioCosto As Decimal)
        Dim intIndice As Integer = ObtenerIndice(dtbDetalleCompra, producto.IdProducto)
        If producto.Tipo = 1 And intIndice >= 0 Then
            dtbDetalleCompra.Rows(intIndice).Item(1) = producto.CodigoProveedor
            dtbDetalleCompra.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleCompra.Rows(intIndice).Item(3) += intCantidad
            dtbDetalleCompra.Rows(intIndice).Item(4) = dblPrecioCosto
            dtbDetalleCompra.Rows(intIndice).Item(5) = dtbDetalleCompra.Rows(intIndice).Item(3) * dtbDetalleCompra.Rows(intIndice).Item(4)
            dtbDetalleCompra.Rows(intIndice).Item(6) = producto.ParametroImpuesto.TasaImpuesto = 0
            dtbDetalleCompra.Rows(intIndice).Item(7) = producto.ParametroImpuesto.TasaImpuesto
            dtbDetalleCompra.Rows(intIndice).Item(8) = producto.PrecioVenta1
            dtbDetalleCompra.Rows(intIndice).Item(9) = (producto.PrecioVenta1 * 100 / dblPrecioCosto) - 100
        Else
            dtrRowDetCompra = dtbDetalleCompra.NewRow
            dtrRowDetCompra.Item(0) = producto.IdProducto
            dtrRowDetCompra.Item(1) = producto.CodigoProveedor
            dtrRowDetCompra.Item(2) = producto.Descripcion
            dtrRowDetCompra.Item(3) = intCantidad
            dtrRowDetCompra.Item(4) = dblPrecioCosto
            dtrRowDetCompra.Item(5) = dtrRowDetCompra.Item(3) * dtrRowDetCompra.Item(4)
            dtrRowDetCompra.Item(6) = producto.ParametroImpuesto.TasaImpuesto = 0
            dtrRowDetCompra.Item(7) = producto.ParametroImpuesto.TasaImpuesto
            dtrRowDetCompra.Item(8) = producto.PrecioVenta1
            dtrRowDetCompra.Item(9) = (producto.PrecioVenta1 * 100 / dblPrecioCosto) - 100
            dtbDetalleCompra.Rows.Add(dtrRowDetCompra)
        End If
        grdDetalleCompra.Refresh()
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

    Private Sub CargarLineaDesglosePago()
        Dim objPkDesglose(1) As Object
        objPkDesglose(0) = cboFormaPago.SelectedValue
        objPkDesglose(1) = cboCuentaBanco.SelectedValue
        If dtbDesglosePago.Rows.Contains(objPkDesglose) Then
            MessageBox.Show("La forma de pago seleccionada ya fue agregada al detalle de pago.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim decMontoPago, decTipoCambio As Decimal
        decMontoPago = CDbl(txtMontoPago.Text)
        decTipoCambio = CDbl(txtTipoCambio.Text)
        dtrRowDesglosePago = dtbDesglosePago.NewRow
        dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
        dtrRowDesglosePago.Item(1) = cboFormaPago.Text
        dtrRowDesglosePago.Item(2) = cboCuentaBanco.SelectedValue
        dtrRowDesglosePago.Item(3) = cboCuentaBanco.Text
        dtrRowDesglosePago.Item(4) = txtReferencia.Text
        dtrRowDesglosePago.Item(5) = cboTipoMoneda.SelectedValue
        dtrRowDesglosePago.Item(6) = decMontoPago
        dtrRowDesglosePago.Item(7) = decTipoCambio
        dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim decSubTotal As Decimal = 0
        decExcento = 0
        decGravado = 0
        For I = 0 To dtbDetalleCompra.Rows.Count - 1
            If dtbDetalleCompra.Rows(I).Item(6) = 0 Then
                decGravado += dtbDetalleCompra.Rows(I).Item(5)
            Else
                decExcento += dtbDetalleCompra.Rows(I).Item(5)
            End If
        Next
        decSubTotal = decGravado + decExcento
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decExcento + decGravado + CDbl(txtImpuesto.Text) - CDbl(txtDescuento.Text), 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(txtImpuesto.Text, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
        decSaldoPorPagar = decTotal - decTotalPago
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarTotalesPago()
        decTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            decTotalPago = decTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(6))
        Next
        decSaldoPorPagar = decTotal - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        cboCondicionVenta.ValueMember = "Id"
        cboCondicionVenta.DisplayMember = "Descripcion"
        cboCondicionVenta.DataSource = Await Puntoventa.ObtenerListadoCondicionVenta(FrmPrincipal.usuarioGlobal.Token)
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = Await Puntoventa.ObtenerListadoFormaPagoEmpresa(FrmPrincipal.usuarioGlobal.Token)
        cboCuentaBanco.ValueMember = "Id"
        cboCuentaBanco.DisplayMember = "Descripcion"
        cboCuentaBanco.DataSource = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = Await Puntoventa.ObtenerListadoTipoMoneda(FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.usuarioGlobal.Modifica
    End Function

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigoProveedor.Text = ""
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecioCosto.Text = FormatNumber(0, 2)
            txtPrecioVenta.Text = ""
            txtUtilidad.Text = ""
            txtCodigoProveedor.Focus()
            Exit Sub
        Else
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            txtCodigoProveedor.Text = producto.CodigoProveedor
            txtCodigo.Text = producto.Codigo
            txtDescripcion.Text = producto.Descripcion
            txtExistencias.Text = producto.Existencias
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
            txtPrecioVenta.Text = FormatNumber(producto.PrecioVenta1, 2)
            txtUtilidad.Text = IIf(producto.PrecioCosto > 0, FormatNumber((producto.PrecioVenta1 * 100 / producto.PrecioCosto) - 100, 2), "0.00")
            txtCantidad.Focus()
        End If
    End Sub

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, 1, 0, True, True, FrmPrincipal.usuarioGlobal.Token)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigoProveedor.AutoCompleteCustomSource = source
        txtCodigoProveedor.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigoProveedor.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmCompra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub

    Private Async Sub FrmCompra_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            txtIdOrdenCompra.Text = "0"
            Await CargarCombos()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                Await CargarAutoCompletarProducto()
            End If
            btnBusProd.Enabled = True
            IniciaDetalleCompra()
            EstablecerPropiedadesDataGridView()
            grdDetalleCompra.DataSource = dtbDetalleCompra
            grdDesglosePago.DataSource = dtbDesglosePago
            bolInit = False
            txtCantidad.Text = "1"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtDescuento.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdCompra.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        proveedor = Nothing
        txtProveedor.Text = ""
        txtFactura.Text = ""
        txtIdOrdenCompra.Text = "0"
        cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        txtPlazoCredito.Text = ""
        dtbDetalleCompra.Rows.Clear()
        grdDetalleCompra.Refresh()
        decSaldoPorPagar = 0
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCantidad.Text = "1"
        txtCodigoProveedor.Text = ""
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtPrecioCosto.Text = ""
        txtPrecioVenta.Text = ""
        txtUtilidad.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        decTotal = 0
        decTotalPago = 0
        txtDescuento.ReadOnly = False
        txtImpuesto.ReadOnly = False
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        btnBusProd.Enabled = True
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        btnBuscarProveedor.Enabled = True
        txtMontoPago.Text = ""
        txtProveedor.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdCompra.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularCompra(txtIdCompra.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaCompra()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                compra = Await Puntoventa.ObtenerCompra(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If compra IsNot Nothing Then
                txtIdCompra.Text = compra.IdCompra
                proveedor = compra.Proveedor
                txtProveedor.Text = proveedor.Nombre
                txtFecha.Text = compra.Fecha
                txtFactura.Text = compra.NoDocumento
                cboCondicionVenta.SelectedValue = compra.IdCondicionVenta
                txtPlazoCredito.Text = compra.PlazoCredito
                txtDescuento.Text = FormatNumber(compra.Descuento, 2)
                txtImpuesto.Text = FormatNumber(compra.Impuesto, 2)
                CargarDetalleCompra(compra)
                CargarDesglosePago(compra)
                CargarTotales()
                CargarTotalesPago()
                txtDescuento.ReadOnly = True
                txtImpuesto.ReadOnly = True
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnBusProd.Enabled = False
                btnImprimir.Enabled = True
                btnBuscarProveedor.Enabled = False
                btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                btnGuardar.Enabled = FrmPrincipal.usuarioGlobal.Modifica
            End If
        End If
    End Sub

    Private Sub BtnOrdenCompra_Click(sender As Object, e As EventArgs) Handles btnOrdenCompra.Click
        Dim formBusqueda As New FrmBusquedaOrdenCompra()
        formBusqueda.ExcluirOrdenesAplicadas()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'ordenCompra = servicioCompras.ObtenerOrdenCompra(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenCompra IsNot Nothing Then
                txtIdCompra.Text = ""
                proveedor = ordenCompra.Proveedor
                txtProveedor.Text = proveedor.Nombre
                txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
                txtFactura.Text = ""
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                txtPlazoCredito.Text = ""
                txtDescuento.Text = FormatNumber(ordenCompra.Descuento, 2)
                txtIdOrdenCompra.Text = ordenCompra.IdOrdenCompra
                CargarDetalleOrdenCompra(ordenCompra)
                dtbDesglosePago.Rows.Clear()
                grdDesglosePago.Refresh()
                CargarTotales()
                CargarTotalesPago()
                txtDescuento.ReadOnly = False
                btnInsertar.Enabled = True
                btnEliminar.Enabled = True
                btnInsertarPago.Enabled = True
                btnEliminarPago.Enabled = True
                btnBusProd.Enabled = True
                btnAnular.Enabled = False
                btnGuardar.Enabled = True
                btnImprimir.Enabled = False
                btnBuscarProveedor.Enabled = True
            Else
                MessageBox.Show("No existe registro de orden de compra asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                proveedor = Await Puntoventa.ObtenerProveedor(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtProveedor.Text = proveedor.Nombre
        End If
    End Sub

    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
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
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If proveedor Is Nothing Or txtFecha.Text = "" Or decTotal = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtFactura.Text = "" Then
            MessageBox.Show("Debe ingresar la referencia del número de factura de la compra para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado Then
            If decSaldoPorPagar > 0 Then
                MessageBox.Show("El total del desglose de pago de la compra no es suficiente para cubrir el saldo por pagar actual.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If decSaldoPorPagar < 0 Then
                MessageBox.Show("El total del desglose de pago de la compra es superior al saldo por pagar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
        If txtIdCompra.Text = "" Then
            btnImprimir.Focus()
            btnGuardar.Enabled = False
            compra = New Compra With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdProveedor = proveedor.IdProveedor,
                .Fecha = Now(),
                .NoDocumento = txtFactura.Text,
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCondicionVenta = cboCondicionVenta.SelectedValue,
                .PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text),
                .Excento = decExcento,
                .Gravado = decGravado,
                .Descuento = CDbl(txtDescuento.Text),
                .Impuesto = CDbl(txtImpuesto.Text),
                .IdOrdenCompra = txtIdOrdenCompra.Text,
                .Nulo = False
            }
            For I = 0 To dtbDetalleCompra.Rows.Count - 1
                detalleCompra = New DetalleCompra With {
                    .IdProducto = dtbDetalleCompra.Rows(I).Item(0),
                    .Cantidad = dtbDetalleCompra.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleCompra.Rows(I).Item(4),
                    .Excento = dtbDetalleCompra.Rows(I).Item(6),
                    .PorcentajeIVA = dtbDetalleCompra.Rows(I).Item(7)
                }
                compra.DetalleCompra.Add(detalleCompra)
            Next
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoCompra With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(4),
                    .Beneficiario = txtProveedor.Text,
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(5),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(6),
                    .TipoDeCambio = dtbDesglosePago.Rows(I).Item(7)
                }
                compra.DesglosePagoCompra.Add(desglosePago)
            Next
            Try
                txtIdCompra.Text = Await Puntoventa.AgregarCompra(compra, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdCompra.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        btnBusProd.Enabled = False
        btnBuscarProveedor.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdCompra.Text <> "" Then
            comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdCompra.Text,
                .strNombre = txtProveedor.Text,
                .strFecha = txtFecha.Text,
                .strSubTotal = txtSubTotal.Text,
                .strDescuento = txtDescuento.Text,
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleCompra = New List(Of ModuloImpresion.ClsDetalleComprobante)
            For I = 0 To dtbDetalleCompra.Rows.Count - 1
                detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleCompra.Rows(I).Item(1) + "-" + dtbDetalleCompra.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleCompra.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleCompra.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleCompra.Rows(I).Item(3)) * CDbl(dtbDetalleCompra.Rows(I).Item(4)), 2)
                }
                arrDetalleCompra.Add(detalleComprobante)
            Next
            comprobanteImpresion.arrDetalleComprobante = arrDetalleCompra
            arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(1), FormatNumber(dtbDesglosePago.Rows(I).Item(6), 2))
                arrDesglosePago.Add(desglosePagoImpresion)
            Next
            comprobanteImpresion.arrDesglosePago = arrDesglosePago
            Try
                ModuloImpresion.ImprimirCompra(comprobanteImpresion)
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
            If txtPrecioCosto.Text <= "" Then strError = "El precio de costo del producto no puede estar en blanco"
            If strError <> "" Then
                MessageBox.Show(strError, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDetalleCompra(producto, txtCantidad.Text, txtPrecioCosto.Text)
            txtCodigoProveedor.Text = ""
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtCantidad.Text = "1"
            txtPrecioCosto.Text = ""
            txtPrecioVenta.Text = ""
            txtUtilidad.Text = ""
            producto = Nothing
            txtCodigoProveedor.Focus()
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dtbDetalleCompra.Rows.Count > 0 Then
            dtbDetalleCompra.Rows.RemoveAt(grdDetalleCompra.CurrentRow.Index)
            grdDetalleCompra.Refresh()
            CargarTotales()
            txtCodigoProveedor.Focus()
        End If
    End Sub

    Private Sub CboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboCuentaBanco.SelectedIndex = 0
            txtReferencia.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario And cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.Tarjeta Then
                cboCuentaBanco.Enabled = False
                txtReferencia.ReadOnly = True
                If cboFormaPago.SelectedValue = StaticFormaPago.Efectivo Then
                    cboTipoMoneda.Enabled = True
                Else
                    cboTipoMoneda.Enabled = False
                End If
            Else
                cboCuentaBanco.Enabled = True
                txtReferencia.ReadOnly = False
                cboTipoMoneda.Enabled = False
            End If
        End If
    End Sub

    Private Sub CboTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        End If
    End Sub

    Private Sub CboIdCondicionVenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCondicionVenta.SelectedIndexChanged
        If cboCondicionVenta.SelectedValue = StaticCondicionVenta.Credito Then
            txtPlazoCredito.Enabled = True
            txtPlazoCredito.Text = "30"
        Else
            txtPlazoCredito.Enabled = False
            txtPlazoCredito.Text = ""
        End If
        If cboCondicionVenta.SelectedValue <> StaticCondicionVenta.Contado Then
            btnInsertarPago.Enabled = False
            btnEliminarPago.Enabled = False
        Else
            btnInsertarPago.Enabled = True
            btnEliminarPago.Enabled = True
        End If
    End Sub

    Private Sub BtnInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboCuentaBanco.SelectedValue > 0 And decTotal > 0 And txtMontoPago.Text <> "" Then
            If decSaldoPorPagar = 0 Then
                MessageBox.Show("El monto de por cancelar ya se encuentra cubierto. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDesglosePago()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
            txtMontoPago.Text = ""
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Sub BtnEliminarPago_Click(sender As Object, e As EventArgs) Handles btnEliminarPago.Click
        If dtbDesglosePago.Rows.Count > 0 Then
            dtbDesglosePago.Rows.RemoveAt(grdDesglosePago.CurrentRow.Index)
            grdDesglosePago.Refresh()
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Sub Descuento_Leave(sender As Object, e As EventArgs) Handles txtDescuento.Validated
        If txtDescuento.Text = "" Then
            txtDescuento.Text = FormatNumber(0, 2)
        Else
            If InStr(txtDescuento.Text, "%") Then
                txtDescuento.Text = CDbl(Mid(txtDescuento.Text, 1, Len(txtDescuento.Text) - 1)) / 100 * CDbl(txtSubTotal.Text)
            End If
            If txtDescuento.Text > CDbl(txtSubTotal.Text) Then
                MessageBox.Show("El descuento debe ser menor al SubTotal. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescuento.Text = 0
            End If
            txtDescuento.Text = FormatNumber(txtDescuento.Text, 2)
        End If
        txtTotal.Text = FormatNumber(CDbl(txtSubTotal.Text) + CDbl(txtImpuesto.Text) - CDbl(txtDescuento.Text), 2)
        decTotal = CDbl(txtTotal.Text)
        CargarTotalesPago()
    End Sub

    Private Sub TxtImpuesto_Leave(sender As Object, e As EventArgs) Handles txtImpuesto.Validated
        If txtImpuesto.Text = "" Then txtImpuesto.Text = "0"
        txtImpuesto.Text = FormatNumber(txtImpuesto.Text, 2)
        txtTotal.Text = FormatNumber(CDbl(txtSubTotal.Text) + CDbl(txtImpuesto.Text) - CDbl(txtDescuento.Text), 2)
        decTotal = CDbl(txtTotal.Text)
        CargarTotalesPago()
    End Sub

    Private Async Sub txtCodigoProveedor_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigoProveedor.PreviewKeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.ControlKey Then
            If FrmPrincipal.productoTranstorio IsNot Nothing Then
                Dim formCargar As New FrmCargaProductoTransitorio
                formCargar.ShowDialog()
                If FrmPrincipal.productoTranstorio.PrecioVenta1 > 0 Then
                    CargarLineaDetalleCompra(FrmPrincipal.productoTranstorio, FrmPrincipal.productoTranstorio.Existencias, FrmPrincipal.productoTranstorio.PrecioVenta1)
                End If
            End If
        ElseIf e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigoProveedor(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigoProveedor.Text, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    If producto.Activo Then
                        CargarDatosProducto(producto)
                        txtCantidad.Focus()
                    End If
                Else
                    txtCodigo.Text = ""
                    txtDescripcion.Text = ""
                    txtExistencias.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecioCosto.Text = ""
                    txtCodigo.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub txtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    If producto.Activo Then
                        CargarDatosProducto(producto)
                        txtCantidad.Focus()
                    End If
                Else
                    txtCodigo.Text = ""
                    txtDescripcion.Text = ""
                    txtExistencias.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecioCosto.Text = ""
                    txtCodigo.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub txtPrecioCosto_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPrecioCosto.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If producto IsNot Nothing Then
                If txtPrecioCosto.Text = "" Then txtPrecioCosto.Text = "0"
                txtPrecioCosto.Text = FormatNumber(txtPrecioCosto.Text, 2)
                txtUtilidad.Text = FormatNumber((producto.PrecioVenta1 * 100 / CDbl(txtPrecioCosto.Text)) - 100, 2)
                BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub txtCantidad_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCantidad.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If producto IsNot Nothing Then
                BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub TxtMonto_Validated(sender As Object, e As EventArgs) Handles txtMontoPago.Validated
        If txtMontoPago.Text <> "" Then txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
    End Sub

    Private Sub TxtPlazo_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtPlazoCredito.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecioCosto.KeyPress, txtDescuento.KeyPress, txtMontoPago.KeyPress, txtDescuento.KeyPress, txtImpuesto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class