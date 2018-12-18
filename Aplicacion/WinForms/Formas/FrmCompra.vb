Imports System.Collections.Generic
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmCompra
#Region "Variables"
    Private dblExcento, dblGrabado, dblPorcentajeIVA As Decimal
    Private dblTotalPago As Decimal = 0
    Private dblTotal As Decimal = 0
    Private dblSaldoPorPagar As Decimal = 0
    Private I As Short
    Private dtbDatosLocal, dtbDetalleCompra, dtbDesglosePago As DataTable
    Private dtrRowDetCompra, dtrRowDesglosePago As DataRow
    Private arrDetalleCompra As List(Of ModuloImpresion.clsDetalleComprobante)
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
    Private compra As Compra
    Private ordenCompra As OrdenCompra
    Private proveedor As Proveedor
    Private detalleCompra As DetalleCompra
    Private desglosePago As DesglosePagoCompra
    Private producto As Producto
    Private tipoMoneda As TipoMoneda
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.clsDetalleComprobante
    Private desglosePagoImpresion As ModuloImpresion.clsDesgloseFormaPago
    Private bolInit As Boolean = True
#End Region

#Region "M�todos"
    Private Sub IniciaDetalleCompra()
        dtbDetalleCompra = New DataTable()
        dtbDetalleCompra.Columns.Add("IDPRODUCTO", GetType(Int32))
        dtbDetalleCompra.Columns.Add("CODIGO", GetType(String))
        dtbDetalleCompra.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleCompra.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleCompra.Columns.Add("EXCENTO", GetType(Int32))
        dtbDetalleCompra.PrimaryKey = {dtbDetalleCompra.Columns(0)}

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDCUENTABANCO", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("NROCHEQUE", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCTIPOMONEDA", GetType(String))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("MONTOFORANEO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2), dtbDesglosePago.Columns(6)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleCompra.Columns.Clear()
        grdDetalleCompra.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleCompra.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "C�digo"
        dvcCodigo.Width = 190
        grdDetalleCompra.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripci�n"
        dvcDescripcion.Width = 350
        grdDetalleCompra.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 80
        dvcPrecioCosto.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleCompra.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 0
        dvcExc.Visible = False
        grdDetalleCompra.Columns.Add(dvcExc)

        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdFormaPago As New DataGridViewTextBoxColumn
        Dim dvcDescFormaPago As New DataGridViewTextBoxColumn
        Dim dvcIdCuentaBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcPlazo As New DataGridViewTextBoxColumn
        Dim dvcNroCheque As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcDescTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcMontoForaneo As New DataGridViewTextBoxColumn

        dvcIdFormaPago.DataPropertyName = "IDFORMAPAGO"
        dvcIdFormaPago.HeaderText = "Id"
        dvcIdFormaPago.Width = 0
        dvcIdFormaPago.Visible = False
        grdDesglosePago.Columns.Add(dvcIdFormaPago)

        dvcDescFormaPago.DataPropertyName = "DESCFORMAPAGO"
        dvcDescFormaPago.HeaderText = "Forma de Pago"
        dvcDescFormaPago.Width = 120
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
        dvcDescBanco.Width = 200
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

        dvcDescTipoMoneda.DataPropertyName = "DESCTIPOMONEDA"
        dvcDescTipoMoneda.HeaderText = "Moneda"
        dvcDescTipoMoneda.Width = 90
        dvcDescTipoMoneda.Visible = True
        dvcDescTipoMoneda.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescTipoMoneda)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 110
        dvcMontoLocal.Visible = True
        dvcMontoLocal.ReadOnly = True
        dvcMontoLocal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcMontoForaneo.DataPropertyName = "MONTOFORANEO"
        dvcMontoForaneo.HeaderText = "Monto Exterior"
        dvcMontoForaneo.Width = 110
        dvcMontoForaneo.Visible = True
        dvcMontoForaneo.ReadOnly = True
        dvcMontoForaneo.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoForaneo)
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
            dtrRowDetCompra.Item(6) = detalle.Producto.Excento
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
            dtrRowDetCompra.Item(6) = detalle.Producto.Excento
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
            dtrRowDesglosePago.Item(3) = detalle.CuentaBanco.Descripcion
            dtrRowDesglosePago.Item(4) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(5) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(6) = detalle.TipoMoneda.Descripcion
            dtrRowDesglosePago.Item(7) = detalle.MontoLocal
            dtrRowDesglosePago.Item(8) = detalle.MontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarLineaDetalleCompra(ByVal producto As Producto)
        Dim intIndice As Integer = dtbDetalleCompra.Rows.IndexOf(dtbDetalleCompra.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleCompra.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleCompra.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleCompra.Rows(intIndice).Item(3) += txtCantidad.Text
            dtbDetalleCompra.Rows(intIndice).Item(4) = txtPrecioCosto.Text
            dtbDetalleCompra.Rows(intIndice).Item(5) = dtbDetalleCompra.Rows(intIndice).Item(3) * dtbDetalleCompra.Rows(intIndice).Item(4)
            dtbDetalleCompra.Rows(intIndice).Item(6) = producto.Excento
        Else
            dtrRowDetCompra = dtbDetalleCompra.NewRow
            dtrRowDetCompra.Item(0) = producto.IdProducto
            dtrRowDetCompra.Item(1) = producto.Codigo
            dtrRowDetCompra.Item(2) = producto.Descripcion
            dtrRowDetCompra.Item(3) = txtCantidad.Text
            dtrRowDetCompra.Item(4) = CDbl(txtPrecioCosto.Text)
            dtrRowDetCompra.Item(5) = dtrRowDetCompra.Item(3) * dtrRowDetCompra.Item(4)
            dtrRowDetCompra.Item(6) = producto.Excento
            dtbDetalleCompra.Rows.Add(dtrRowDetCompra)
        End If
        grdDetalleCompra.Refresh()
    End Sub

    Private Sub CargarLineaDesglosePago()
        Dim intIndice As Integer
        Dim objPkDesglose(2) As Object
        Dim dblMontoLocal, dblMontoForaneo As Decimal
        objPkDesglose(0) = cboFormaPago.SelectedValue
        objPkDesglose(1) = cboCuentaBanco.SelectedValue
        objPkDesglose(2) = cboTipoMoneda.SelectedValue
        dblMontoForaneo = CDbl(txtMonto.Text)
        dblMontoLocal = CDbl(txtMonto.Text) * CDbl(txtTipoCambio.Text)
        If dblMontoLocal > dblSaldoPorPagar Then
            dblMontoLocal = dblSaldoPorPagar
            dblMontoForaneo = dblMontoLocal / CDbl(txtTipoCambio.Text)
        End If
        If dtbDesglosePago.Rows.Contains(objPkDesglose) Then
            intIndice = dtbDesglosePago.Rows.IndexOf(dtbDesglosePago.Rows.Find(objPkDesglose))
            dtbDesglosePago.Rows(intIndice).Item(4) = txtDocumento.Text
            dtbDesglosePago.Rows(intIndice).Item(7) = dblMontoLocal
            dtbDesglosePago.Rows(intIndice).Item(8) = dblMontoForaneo
        Else
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
            dtrRowDesglosePago.Item(1) = cboFormaPago.Text
            dtrRowDesglosePago.Item(2) = cboCuentaBanco.SelectedValue
            dtrRowDesglosePago.Item(3) = cboCuentaBanco.Text
            dtrRowDesglosePago.Item(4) = txtDocumento.Text
            dtrRowDesglosePago.Item(5) = cboTipoMoneda.SelectedValue
            dtrRowDesglosePago.Item(6) = cboTipoMoneda.Text
            dtrRowDesglosePago.Item(7) = dblMontoLocal
            dtrRowDesglosePago.Item(8) = dblMontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        End If
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim dblSubTotal As Decimal = 0
        dblExcento = 0
        dblGrabado = 0
        For I = 0 To dtbDetalleCompra.Rows.Count - 1
            If dtbDetalleCompra.Rows(I).Item(6) = 0 Then
                dblGrabado = dblGrabado + CDbl(dtbDetalleCompra.Rows(I).Item(5))
            Else
                dblExcento = dblExcento + CDbl(dtbDetalleCompra.Rows(I).Item(5))
            End If
        Next
        dblSubTotal = dblGrabado + dblExcento
        If dblSubTotal > 0 Then
            dblExcento = Math.Round(dblExcento - (CDbl(txtDescuento.Text) / dblSubTotal * dblExcento), 2, MidpointRounding.AwayFromZero)
            dblGrabado = Math.Round(dblGrabado - (CDbl(txtDescuento.Text) / dblSubTotal * dblGrabado), 2, MidpointRounding.AwayFromZero)
        End If
        txtImpuesto.Text = FormatNumber(dblGrabado * (dblPorcentajeIVA / 100), 2)
        txtSubTotal.Text = FormatNumber(dblSubTotal, 2)
        dblTotal = Math.Round(dblExcento + dblGrabado + CDbl(txtImpuesto.Text), 2, MidpointRounding.AwayFromZero)
        txtTotal.Text = FormatNumber(dblTotal, 2)
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtMonto.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CargarTotalesPago()
        dblTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            dblTotalPago = dblTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(8))
        Next
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtMonto.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        Try
            cboCondicionVenta.ValueMember = "IdCondicionVenta"
            cboCondicionVenta.DisplayMember = "Descripcion"
            'cboCondicionVenta.DataSource = servicioMantenimiento.ObtenerListaCondicionVenta()
            cboFormaPago.ValueMember = "IdFormaPago"
            cboFormaPago.DisplayMember = "Descripcion"
            'cboFormaPago.DataSource = servicioMantenimiento.ObtenerListaFormaPagoCompra()
            cboCuentaBanco.ValueMember = "IdCuenta"
            cboCuentaBanco.DisplayMember = "Descripcion"
            'cboCuentaBanco.DataSource = servicioAuxiliarBancario.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboTipoMoneda.ValueMember = "IdTipoMoneda"
            cboTipoMoneda.DisplayMember = "Descripcion"
            'cboTipoMoneda.DataSource = servicioMantenimiento.ObtenerListaTipoMoneda()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub ValidarProducto()
        If Not bolInit Then
            If txtCodigo.Text <> "" Then
                If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If txtCodigo.Text.IndexOf(" ") >= 0 Then
                        txtCodigo.Text = txtCodigo.Text.Substring(0, txtCodigo.Text.IndexOf(" "))
                    End If
                End If
                Try
                    'producto = servicioMantenimiento.ObtenerProductoPorCodigo(txtCodigo.Text)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                If producto Is Nothing Then
                    MessageBox.Show("El c�digo ingresado no existe. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecioCosto.Text = ""
                    txtCodigo.Focus()
                    Exit Sub
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
                txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
            End If
        End If
    End Sub

    Private Sub CargarAutoCompletarProducto()
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As ICollection(Of Producto) = Nothing 'servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, True)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCompra_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        txtIdOrdenCompra.Text = "0"
        dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
        CargarCombos()
        If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
            CargarAutoCompletarProducto()
        End If
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
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        Try
            'tipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioCompra, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdCompra.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
        proveedor = Nothing
        txtProveedor.Text = ""
        txtFactura.Text = ""
        txtIdOrdenCompra.Text = "0"
        cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        txtPlazoCredito.Text = ""
        dtbDetalleCompra.Rows.Clear()
        grdDetalleCompra.Refresh()
        dblSaldoPorPagar = 0
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCantidad.Text = "1"
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtPrecioCosto.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMonto.Text = ""
        dblSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        dblTotal = 0
        dblTotalPago = 0
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
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        txtMonto.Text = ""
        txtProveedor.Focus()
    End Sub

    Private Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdCompra.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioCompras.AnularCompra(compra.IdCompra, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacci�n procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaCompra()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                'compra = servicioCompras.ObtenerCompra(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                dblPorcentajeIVA = compra.PorcentajeIVA
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
                btnAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                btnGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
            End If
        End If
    End Sub

    Private Sub BtnOrdenCompra_Click(sender As Object, e As EventArgs) Handles btnOrdenCompra.Click
        Dim formBusqueda As New FrmBusquedaOrdenCompra()
        formBusqueda.ExcluirOrdenesAplicadas()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                'ordenCompra = servicioCompras.ObtenerOrdenCompra(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenCompra IsNot Nothing Then
                txtIdCompra.Text = ""
                proveedor = ordenCompra.Proveedor
                txtProveedor.Text = proveedor.Nombre
                txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
                dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
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
                MessageBox.Show("No existe registro de orden de compra asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                'proveedor = servicioCompras.ObtenerProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtProveedor.Text = proveedor.Nombre
        End If
    End Sub

    Private Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intTipoPrecio = 1
        }
        FrmMenuPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmMenuPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmMenuPrincipal.strBusqueda
            ValidarProducto()
        End If
        txtCodigo.Focus()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If proveedor Is Nothing Or txtFecha.Text = "" Or dblTotal = 0 Then
            MessageBox.Show("Informaci�n incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtFactura.Text = "" Then
            MessageBox.Show("Debe ingresar la referencia del n�mero de factura de la compra para guardar el registro.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado Then
            If dblSaldoPorPagar > 0 Then
                MessageBox.Show("El total del desglose de pago de la compra no es suficiente para cubrir el saldo por pagar actual.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            If dblSaldoPorPagar < 0 Then
                MessageBox.Show("El total del desglose de pago de la compra es superior al saldo por pagar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
        If txtIdCompra.Text = "" Then
            compra = New Compra With {
                .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
                .IdProveedor = proveedor.IdProveedor,
                .Fecha = Now(),
                .NoDocumento = txtFactura.Text,
                .IdCondicionVenta = cboCondicionVenta.SelectedValue,
                .PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text),
                .Excento = dblExcento,
                .Grabado = dblGrabado,
                .Descuento = CDbl(txtDescuento.Text),
                .PorcentajeIVA = dblPorcentajeIVA,
                .Impuesto = CDbl(txtImpuesto.Text),
                .IdOrdenCompra = txtIdOrdenCompra.Text,
                .Nulo = False
            }
            For I = 0 To dtbDetalleCompra.Rows.Count - 1
                detalleCompra = New DetalleCompra With {
                    .IdProducto = dtbDetalleCompra.Rows(I).Item(0),
                    .Cantidad = dtbDetalleCompra.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleCompra.Rows(I).Item(4),
                    .Excento = dtbDetalleCompra.Rows(I).Item(6)
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
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(7),
                    .MontoForaneo = dtbDesglosePago.Rows(I).Item(8)
                }
                compra.DesglosePagoCompra.Add(desglosePago)
            Next
            Try
                'compra = servicioCompras.AgregarCompra(compra)
                txtIdCompra.Text = compra.IdCompra
            Catch ex As Exception
                txtIdCompra.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            compra.NoDocumento = txtFactura.Text
            Try
                'servicioCompras.ActualizarCompra(compra)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacci�n efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
        btnImprimir.Focus()
        btnGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
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
                .usuario = FrmMenuPrincipal.usuarioGlobal,
                .empresa = FrmMenuPrincipal.empresaGlobal,
                .equipo = FrmMenuPrincipal.equipoGlobal,
                .strId = txtIdCompra.Text,
                .strNombre = txtProveedor.Text,
                .strFecha = txtFecha.Text,
                .strSubTotal = txtSubTotal.Text,
                .strDescuento = txtDescuento.Text,
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleCompra = New List(Of ModuloImpresion.clsDetalleComprobante)
            For I = 0 To dtbDetalleCompra.Rows.Count - 1
                detalleComprobante = New ModuloImpresion.clsDetalleComprobante With {
                    .strDescripcion = dtbDetalleCompra.Rows(I).Item(1) + "-" + dtbDetalleCompra.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleCompra.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleCompra.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleCompra.Rows(I).Item(3)) * CDbl(dtbDetalleCompra.Rows(I).Item(4)), 2)
                }
                arrDetalleCompra.Add(detalleComprobante)
            Next
            comprobanteImpresion.arrDetalleComprobante = arrDetalleCompra
            arrDesglosePago = New List(Of ModuloImpresion.clsDesgloseFormaPago)
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                    .strDescripcion = dtbDesglosePago.Rows(I).Item(1),
                    .strMonto = FormatNumber(dtbDesglosePago.Rows(I).Item(8)),
                    .strNroDoc = dtbDesglosePago.Rows(I).Item(5)
                }
                arrDesglosePago.Add(desglosePagoImpresion)
            Next
            comprobanteImpresion.arrDesglosePago = arrDesglosePago
            Try
                ModuloImpresion.ImprimirCompra(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" Then
            CargarLineaDetalleCompra(producto)
            CargarTotales()
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecioCosto.Text = ""
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dtbDetalleCompra.Rows.Count > 0 Then
            dtbDetalleCompra.Rows.Remove(dtbDetalleCompra.Rows.Find(grdDetalleCompra.CurrentRow.Cells(0).Value))
            grdDetalleCompra.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboCuentaBanco.SelectedIndex = 0
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario And cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.Tarjeta Then
                cboCuentaBanco.Enabled = False
                txtDocumento.ReadOnly = True
                If cboFormaPago.SelectedValue = StaticFormaPago.Efectivo Then
                    cboTipoMoneda.Enabled = True
                Else
                    cboTipoMoneda.Enabled = False
                End If
            Else
                cboCuentaBanco.Enabled = True
                txtDocumento.ReadOnly = False
                cboTipoMoneda.Enabled = False
            End If
        End If
    End Sub

    Private Sub CboTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            Try
                'tipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioVenta, 2)
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
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboCuentaBanco.SelectedValue > 0 And dblTotal > 0 And txtMonto.Text <> "" Then
            If dblSaldoPorPagar = 0 Then
                MessageBox.Show("El monto de por cancelar ya se encuentra cubierto. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If cboFormaPago.SelectedValue = StaticFormaPago.Cheque Then
                If txtDocumento.Text = "" Then
                    MessageBox.Show("Debe ingresar el n�mero de cheque.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            CargarLineaDesglosePago()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
            txtMonto.Text = ""
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Sub BtnEliminarPago_Click(sender As Object, e As EventArgs) Handles btnEliminarPago.Click
        Dim objPkDesglose(2) As Object
        If dtbDesglosePago.Rows.Count > 0 Then
            objPkDesglose(0) = grdDesglosePago.CurrentRow.Cells(0).Value
            objPkDesglose(1) = grdDesglosePago.CurrentRow.Cells(2).Value
            objPkDesglose(2) = grdDesglosePago.CurrentRow.Cells(6).Value
            dtbDesglosePago.Rows.Remove(dtbDesglosePago.Rows.Find(objPkDesglose))
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
            If CDbl(txtDescuento.Text) > CDbl(txtSubTotal.Text) Then
                MessageBox.Show("El descuento debe ser menor al SubTotal. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescuento.Text = 0
            End If
            txtDescuento.Text = FormatNumber(txtDescuento.Text, 2)
        End If
        txtTotal.Text = FormatNumber(CDbl(txtSubTotal.Text) + CDbl(txtImpuesto.Text) - CDbl(txtDescuento.Text), 2)
        dblTotal = CDbl(txtTotal.Text)
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub TxtImpuesto_Leave(sender As Object, e As EventArgs) Handles txtImpuesto.Validated
        If txtImpuesto.Text = "" Then txtImpuesto.Text = "0"
        txtImpuesto.Text = FormatNumber(txtImpuesto.Text, 2)
        txtTotal.Text = FormatNumber(CDbl(txtSubTotal.Text) + CDbl(txtImpuesto.Text) - CDbl(txtDescuento.Text), 2)
        dblTotal = CDbl(txtTotal.Text)
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub PrecioCosto_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioCosto.Validated
        If txtPrecioCosto.Text = "" Then txtPrecioCosto.Text = "0"
        txtPrecioCosto.Text = FormatNumber(txtPrecioCosto.Text, 2)
    End Sub

    Private Sub CboCodigo_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.Validated
        ValidarProducto()
    End Sub

    Private Sub TxtMonto_Validated(sender As Object, e As EventArgs) Handles txtMonto.Validated
        If txtMonto.Text <> "" Then txtMonto.Text = FormatNumber(txtMonto.Text, 2)
    End Sub

    Private Sub TxtPlazo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        FrmMenuPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecioCosto.KeyPress, txtDescuento.KeyPress, txtMonto.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class