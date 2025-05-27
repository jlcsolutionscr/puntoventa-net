Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes
Imports LeandroSoftware.Common.DatosComunes
Imports System.Collections.Generic
Imports System.Threading.Tasks

Public Class FrmFactura
#Region "Variables"
    Private decDescuento, decExcento, decGravado, decExonerado, decImpuesto, decTotalCosto, decTotalPago, decPagoEfectivo, decPagoCliente, decTotal, decSubTotal, decSaldoPorPagar, decPrecioVenta, decMontoAdelanto As Decimal
    Private consecDetalle As Short
    Private intIdProforma, intIdOrdenServicio, intIdApartado As Integer
    Private dtbDetalleFactura, dtbDesglosePago As DataTable
    Private dtrRowDetFactura, dtrRowDesglosePago As DataRow
    Private factura As Factura
    Private detalleFactura As DetalleFactura
    Private ordenServicio As OrdenServicio
    Private proforma As Proforma
    Private apartado As Apartado
    Private desglosePago As DesglosePagoFactura
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private bolReady As Boolean = False
    Private bolAutorizando As Boolean = False
    Private bolImpuestoCargado As Boolean = False
    Private provider As CultureInfo = CultureInfo.InvariantCulture
    'Impresion de tiquete
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private arrDetalleFactura As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
#End Region

#Region "Metodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleFactura = New DataTable()
        dtbDetalleFactura.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleFactura.Columns.Add("CODIGO", GetType(String))
        dtbDetalleFactura.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleFactura.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("PRECIOIVA", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleFactura.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("PORCDESCUENTO", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("VALORDESCUENTO", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("ID", GetType(Integer))
        dtbDetalleFactura.Columns.Add("IMPSER", GetType(Integer))
        dtbDetalleFactura.PrimaryKey = {dtbDetalleFactura.Columns(12)}

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDCUENTABANCO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("TIPOTARJETA", GetType(String))
        dtbDesglosePago.Columns.Add("NROMOVIMIENTO", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Integer))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("TIPODECAMBIO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleFactura.Columns.Clear()
        grdDetalleFactura.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPorcDescuento As New DataGridViewTextBoxColumn
        Dim dvcDescuento As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn
        Dim dvcId As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleFactura.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 110
        dvcCodigo.ReadOnly = True
        dvcCodigo.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleFactura.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.ReadOnly = True
        dvcDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleFactura.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 50
        dvcCantidad.ReadOnly = True
        dvcCantidad.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcCantidad)

        dvcPorcDescuento.DataPropertyName = "PORCDESCUENTO"
        dvcPorcDescuento.HeaderText = "Des%"
        dvcPorcDescuento.Width = 50
        dvcPorcDescuento.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPorcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcPorcDescuento)

        dvcDescuento.DataPropertyName = "VALORDESCUENTO"
        dvcDescuento.HeaderText = "Desc/U"
        dvcDescuento.Width = 75
        dvcDescuento.ReadOnly = True
        dvcDescuento.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcDescuento)

        dvcPrecio.DataPropertyName = "PRECIOIVA"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.ReadOnly = True
        dvcPrecio.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.ReadOnly = True
        dvcTotal.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.ReadOnly = True
        dvcExc.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleFactura.Columns.Add(dvcExc)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "PrecioCosto"
        dvcPrecioCosto.Visible = False
        grdDetalleFactura.Columns.Add(dvcPrecioCosto)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Visible = False
        grdDetalleFactura.Columns.Add(dvcPorcentajeIVA)

        dvcId.DataPropertyName = "ID"
        dvcId.HeaderText = "Id"
        dvcId.Visible = False
        grdDetalleFactura.Columns.Add(dvcId)

        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdConsecutivo As New DataGridViewTextBoxColumn
        Dim dvcIdFormaPago As New DataGridViewTextBoxColumn
        Dim dvcDescFormaPago As New DataGridViewTextBoxColumn
        Dim dvcIdCuentaBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcTipoTarjeta As New DataGridViewTextBoxColumn
        Dim dvcNroMovimiento As New DataGridViewTextBoxColumn
        Dim dvcPlazo As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcTipoCambio As New DataGridViewTextBoxColumn

        dvcIdConsecutivo.DataPropertyName = "IDCONSECUTIVO"
        dvcIdConsecutivo.HeaderText = "IdConsecutivo"
        dvcIdConsecutivo.Visible = False
        grdDesglosePago.Columns.Add(dvcIdConsecutivo)

        dvcIdFormaPago.DataPropertyName = "IDFORMAPAGO"
        dvcIdFormaPago.HeaderText = "Id"
        dvcIdFormaPago.Visible = False
        grdDesglosePago.Columns.Add(dvcIdFormaPago)

        dvcDescFormaPago.DataPropertyName = "DESCFORMAPAGO"
        dvcDescFormaPago.HeaderText = "Forma de Pago"
        dvcDescFormaPago.Width = 120
        dvcDescFormaPago.ReadOnly = True
        dvcDescFormaPago.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDesglosePago.Columns.Add(dvcDescFormaPago)

        dvcIdCuentaBanco.DataPropertyName = "IDCUENTABANCO"
        dvcIdCuentaBanco.HeaderText = "IdCuentaBanco"
        dvcIdCuentaBanco.Visible = False
        grdDesglosePago.Columns.Add(dvcIdCuentaBanco)

        dvcDescBanco.DataPropertyName = "DESCBANCO"
        dvcDescBanco.HeaderText = "Banco"
        dvcDescBanco.Width = 240
        dvcDescBanco.ReadOnly = True
        dvcDescBanco.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcTipoTarjeta.DataPropertyName = "TIPOTARJETA"
        dvcTipoTarjeta.HeaderText = "Tipo Tarjeta"
        dvcTipoTarjeta.Width = 100
        dvcTipoTarjeta.ReadOnly = True
        dvcTipoTarjeta.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDesglosePago.Columns.Add(dvcTipoTarjeta)

        dvcNroMovimiento.DataPropertyName = "NROMOVIMIENTO"
        dvcNroMovimiento.HeaderText = "Movimiento #"
        dvcNroMovimiento.Width = 100
        dvcNroMovimiento.ReadOnly = True
        dvcNroMovimiento.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDesglosePago.Columns.Add(dvcNroMovimiento)

        dvcIdTipoMoneda.DataPropertyName = "IDTIPOMONEDA"
        dvcIdTipoMoneda.HeaderText = "TipoMoneda"
        dvcIdTipoMoneda.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoMoneda)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 110
        dvcMontoLocal.ReadOnly = True
        dvcMontoLocal.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcMontoLocal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcTipoCambio.DataPropertyName = "TIPODECAMBIO"
        dvcTipoCambio.HeaderText = "Tipo Cambio"
        dvcTipoCambio.Width = 110
        dvcTipoCambio.ReadOnly = True
        dvcTipoCambio.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcTipoCambio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcTipoCambio)
    End Sub

    Private Sub CargarDetalleFactura(factura As Factura)
        dtbDetalleFactura.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleFactura In factura.DetalleFactura
            consecDetalle += 1
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2)
            dtrRowDetFactura.Item(6) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(7) = detalle.Excento
            dtrRowDetFactura.Item(8) = detalle.PrecioCosto
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PorcDescuento
            dtrRowDetFactura.Item(11) = (dtrRowDetFactura.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(12) = consecDetalle
            dtrRowDetFactura.Item(13) = False
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleOrdenServicio(ordenServicio As OrdenServicio)
        dtbDetalleFactura.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleOrdenServicio In ordenServicio.DetalleOrdenServicio
            consecDetalle += 1
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2)
            dtrRowDetFactura.Item(6) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(7) = detalle.Excento
            dtrRowDetFactura.Item(8) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PorcDescuento
            dtrRowDetFactura.Item(11) = (dtrRowDetFactura.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(12) = consecDetalle
            dtrRowDetFactura.Item(13) = False
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleApartado(apartado As Apartado)
        dtbDetalleFactura.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleApartado In apartado.DetalleApartado
            consecDetalle += 1
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2)
            dtrRowDetFactura.Item(6) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(7) = detalle.Excento
            dtrRowDetFactura.Item(8) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PorcDescuento
            dtrRowDetFactura.Item(11) = (dtrRowDetFactura.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(12) = consecDetalle
            dtrRowDetFactura.Item(13) = False
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleProforma(proforma As Proforma)
        dtbDetalleFactura.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleProforma In proforma.DetalleProforma
            consecDetalle += 1
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2)
            dtrRowDetFactura.Item(6) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(7) = detalle.Excento
            dtrRowDetFactura.Item(8) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PorcDescuento
            dtrRowDetFactura.Item(11) = (dtrRowDetFactura.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetFactura.Item(5)
            dtrRowDetFactura.Item(12) = consecDetalle
            dtrRowDetFactura.Item(13) = False
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDesglosePago(factura As Factura)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoFactura In factura.DesglosePagoFactura
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = FrmPrincipal.ObtenerDescripcionFormaPagoCliente(detalle.IdFormaPago)
            dtrRowDesglosePago.Item(2) = detalle.IdCuentaBanco
            dtrRowDesglosePago.Item(3) = detalle.DescripcionCuenta
            dtrRowDesglosePago.Item(4) = detalle.TipoTarjeta
            dtrRowDesglosePago.Item(5) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(6) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(7) = detalle.MontoLocal
            dtrRowDesglosePago.Item(8) = detalle.TipoDeCambio
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarLineaDetalleFactura(producto As Producto, strDescripcion As String, decCantidad As Decimal, decPrecio As Decimal, decPorcDesc As Decimal, bolImpServicio As Boolean)
        Dim decTasaImpuesto As Decimal = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
        Dim decPrecioGravado As Decimal = decPrecio
        If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 5)
        Dim intIndice As Integer = ObtenerIndice(dtbDetalleFactura, producto.IdProducto)
        If producto.Tipo = 1 And intIndice >= 0 Then
            Dim decNewCantidad = dtbDetalleFactura.Rows(intIndice).Item(3) + decCantidad
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleFactura.Rows(intIndice).Item(3) = decNewCantidad
            dtbDetalleFactura.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleFactura.Rows(intIndice).Item(5) = decPrecio
            dtbDetalleFactura.Rows(intIndice).Item(6) = decNewCantidad * decPrecio
            dtbDetalleFactura.Rows(intIndice).Item(7) = decTasaImpuesto = 0
            dtbDetalleFactura.Rows(intIndice).Item(8) = producto.PrecioCosto
            dtbDetalleFactura.Rows(intIndice).Item(9) = decTasaImpuesto
            dtbDetalleFactura.Rows(intIndice).Item(10) = decPorcDesc
            dtbDetalleFactura.Rows(intIndice).Item(11) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
        Else
            consecDetalle += 1
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = producto.IdProducto
            dtrRowDetFactura.Item(1) = producto.Codigo
            dtrRowDetFactura.Item(2) = strDescripcion
            dtrRowDetFactura.Item(3) = decCantidad
            dtrRowDetFactura.Item(4) = decPrecioGravado
            dtrRowDetFactura.Item(5) = decPrecio
            dtrRowDetFactura.Item(6) = decCantidad * decPrecio
            dtrRowDetFactura.Item(7) = decTasaImpuesto = 0
            dtrRowDetFactura.Item(8) = producto.PrecioCosto
            dtrRowDetFactura.Item(9) = decTasaImpuesto
            dtrRowDetFactura.Item(10) = decPorcDesc
            dtrRowDetFactura.Item(11) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
            dtrRowDetFactura.Item(12) = consecDetalle
            dtrRowDetFactura.Item(13) = bolImpServicio
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        End If
        grdDetalleFactura.Refresh()
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
        objPkDesglose(1) = cboTipoBanco.SelectedValue
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
        dtrRowDesglosePago.Item(2) = cboTipoBanco.SelectedValue
        dtrRowDesglosePago.Item(3) = cboTipoBanco.Text
        dtrRowDesglosePago.Item(4) = txtTipoTarjeta.Text
        dtrRowDesglosePago.Item(5) = txtAutorizacion.Text
        dtrRowDesglosePago.Item(6) = cboTipoMoneda.SelectedValue
        dtrRowDesglosePago.Item(7) = decMontoPago
        dtrRowDesglosePago.Item(8) = decTipoCambio
        dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotales()
        decSubTotal = 0
        decDescuento = 0
        decGravado = 0
        decExonerado = 0
        decExcento = 0
        decImpuesto = 0
        decTotalCosto = 0
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I As Short = 0 To dtbDetalleFactura.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleFactura.Rows(I).Item(9)
            Dim decPrecio As Decimal = dtbDetalleFactura.Rows(I).Item(4)
            Dim decPrecioCosto As Decimal = dtbDetalleFactura.Rows(I).Item(7)
            Dim decCantidad As Decimal = dtbDetalleFactura.Rows(I).Item(3)
            Dim decDescuentoLinea As Decimal = dtbDetalleFactura.Rows(I).Item(11)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = decPrecio * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decTasaGravado = Math.Max(decTasaImpuesto - intPorcentajeExoneracion, 0)
                    Dim decTasaExonerado = decTasaImpuesto - decTasaGravado
                    decImpuestoProducto = decPrecio * decTasaGravado / 100
                    decGravado += Math.Round(decPrecio * (decTasaGravado * 100 / decTasaImpuesto) / 100 * decCantidad, 2)
                    decExonerado += Math.Round(decPrecio * (decTasaExonerado * 100 / decTasaImpuesto) / 100 * decCantidad, 2)
                Else
                    decGravado += Math.Round(decCantidad * decPrecio, 2)
                End If
                decImpuesto += Math.Round(decCantidad * decImpuestoProducto, 2)
            Else
                decExcento += Math.Round(decCantidad * decPrecio, 2)
            End If
            decDescuento += decDescuentoLinea * decCantidad
            decTotalCosto += decPrecioCosto * decCantidad
        Next
        decSubTotal = decGravado + decExcento + decExonerado
        decDescuento = Math.Round(decDescuento, 2)
        decGravado = Math.Round(decGravado, 2)
        decExonerado = Math.Round(decExonerado, 2)
        decExcento = Math.Round(decExcento, 2)
        decImpuesto = Math.Round(decImpuesto, 2)
        decTotalCosto = Math.Round(decTotalCosto, 2)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2)
        txtSubTotal.Text = FormatNumber(decSubTotal + decDescuento, 2)
        txtDescuento.Text = FormatNumber(decDescuento, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
        decSaldoPorPagar = decTotal - decMontoAdelanto - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarTotalesPago()
        decTotalPago = 0
        decPagoEfectivo = 0
        For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
            If dtbDesglosePago.Rows(I).Item(0) = StaticFormaPago.Efectivo Then decPagoEfectivo = CDbl(dtbDesglosePago.Rows(I).Item(7))
            decTotalPago = decTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(7))
        Next
        decSaldoPorPagar = decTotal - decMontoAdelanto - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        cboCondicionVenta.ValueMember = "Id"
        cboCondicionVenta.DisplayMember = "Descripcion"
        cboCondicionVenta.DataSource = FrmPrincipal.ObtenerListadoCondicionVenta()
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = FrmPrincipal.ObtenerListadoFormaPagoCliente()
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = FrmPrincipal.ObtenerListadoTipoMoneda()
        cboActividadEconomica.ValueMember = "Id"
        cboActividadEconomica.DisplayMember = "Descripcion"
        cboActividadEconomica.DataSource = FrmPrincipal.ObtenerListadoActividadEconomica()
    End Sub

    Private Async Function CargarListaBancoAdquiriente() As Task
        Dim lista As IList = Await Puntoventa.ObtenerListadoBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        If lista.Count() = 0 Then
            Throw New Exception("Debe parametrizar la lista de bancos adquirientes para pagos con tarjeta.")
        Else
            cboTipoBanco.DataSource = lista
            cboTipoBanco.ValueMember = "Id"
            cboTipoBanco.DisplayMember = "Descripcion"
        End If
    End Function

    Private Sub CargarAutoCompletarProducto()
        Dim source As New AutoCompleteStringCollection()
        For Each p As ProductoDetalle In FrmPrincipal.listaProductos
            source.Add(p.Codigo + ": " + p.Descripcion)
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Private Async Function CargarListaCuentaBanco() As Task
        Dim lista As IList = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        If lista.Count() = 0 Then
            Throw New Exception("Debe parametrizar la lista de bancos para registrar movimientos.")
        Else
            cboTipoBanco.DataSource = lista
            cboTipoBanco.ValueMember = "Id"
            cboTipoBanco.DisplayMember = "Descripcion"
        End If
    End Function

    Private Function ObtenerPrecioVentaPorCliente(cliente As Cliente, producto As Producto)
        Dim decPrecioVenta As Decimal = producto.PrecioVenta1
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
            End If
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
#End Region

#Region "Eventos Controles"
    Private Sub FrmFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmFactura_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.F2 Then
            If FrmPrincipal.productoTranstorio IsNot Nothing Then
                Dim formCargar As New FrmCargaProductoTransitorio
                formCargar.ShowDialog()
                If FrmPrincipal.productoTranstorio.PrecioVenta1 > 0 Then
                    CargarLineaDetalleFactura(FrmPrincipal.productoTranstorio, FrmPrincipal.productoTranstorio.Descripcion, FrmPrincipal.productoTranstorio.Existencias, FrmPrincipal.productoTranstorio.PrecioVenta1, 0, False)
                    FrmPrincipal.productoTranstorio.PrecioVenta1 = 0
                End If
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            BtnBuscar_Click(btnBuscar, New EventArgs())
        ElseIf e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F5 Then
            If FrmPrincipal.empresaGlobal.Modalidad = 2 Then
                If FrmPrincipal.productoImpuestoServicio Is Nothing Then
                    MessageBox.Show("El impuesto de servicio no se encuentra configurado. Contacte con su proveedor del sistema. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Else
                    If bolImpuestoCargado Then
                        MessageBox.Show("El impuesto de servicio ya se encuentra cargado. Debe eliminarlo si desea recalcularlo. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Else
                        Dim decMontoImpuesto As Decimal = (decExonerado + decExcento + decGravado) * 0.1
                        If decMontoImpuesto > 0 Then
                            CargarLineaDetalleFactura(FrmPrincipal.productoImpuestoServicio, FrmPrincipal.productoImpuestoServicio.Descripcion, 1, decMontoImpuesto, 0, True)
                            bolImpuestoCargado = True
                        End If
                    End If
                End If
            End If
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            BtnGuardar_Click(btnGuardar, New EventArgs())
        ElseIf e.KeyCode = Keys.F11 And btnImprimir.Enabled Then
            BtnImprimir_Click(btnImprimir, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Async Sub FrmFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada()
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto Then CargarAutoCompletarProducto()
            grdDetalleFactura.DataSource = dtbDetalleFactura
            grdDesglosePago.DataSource = dtbDesglosePago
            consecDetalle = 0
            txtCantidad.Text = "1"
            txtPorcDesc.Text = "0"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            decPagoCliente = 0
            intIdProforma = 0
            intIdOrdenServicio = 0
            intIdApartado = 0
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
            txtPorcentajeExoneracion.Text = "0"
            If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
                Try
                    vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                    txtVendedor.Text = vendedor.Nombre
                Catch ex As Exception
                    Throw New Exception("Debe agregar al menos un vendedor al catálogo de vendedores para poder continuar.")
                End Try
            End If
            decMontoAdelanto = 0
            txtMontoAdelanto.Text = FormatNumber(decMontoAdelanto, 2)
            decSaldoPorPagar = 0
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
            If FrmPrincipal.bolModificaDescripcion Then txtDescripcion.ReadOnly = False
            txtCodigo.Focus()
            CargarCombos()
            Await CargarListaBancoAdquiriente()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, Await FrmPrincipal.ObtenerTipoDeCambioDolar())
            bolReady = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdFactura.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada()
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, Await FrmPrincipal.ObtenerTipoDeCambioDolar())
        txtTelefono.Text = ""
        txtReferencia.Text = ""
        txtObservaciones.Text = ""
        cboTipoMoneda.Enabled = True
        If cboActividadEconomica.Items.Count > 0 Then cboActividadEconomica.SelectedIndex = 0
        cboCondicionVenta.Enabled = False
        cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        txtPlazoCredito.Text = ""
        txtPorcentajeExoneracion.Text = "0"
        dtbDetalleFactura.Rows.Clear()
        grdDetalleFactura.Refresh()
        consecDetalle = 0
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        decPagoCliente = 0
        intIdProforma = 0
        intIdOrdenServicio = 0
        intIdApartado = 0
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtExistencias.Text = ""
        txtPorcDesc.Text = "0"
        txtPrecio.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
        decMontoAdelanto = 0
        txtMontoAdelanto.Text = FormatNumber(decMontoAdelanto, 2)
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        decTotal = 0
        decTotalPago = 0
        decPagoEfectivo = 0
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        btnBusProd.Enabled = True
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        btnGenerarPDF.Enabled = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        btnOrdenServicio.Enabled = True
        btnApartado.Enabled = True
        btnProforma.Enabled = True
        Try
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
        Catch ex As Exception
            MessageBox.Show("Error al consultar el cliente de contado. Por favor consulte con su proveedor.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
            Try
                vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                MessageBox.Show("Debe agregar al menos un vendedor al catálogo de vendedores para poder continuar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
        Else
            vendedor = Nothing
            txtVendedor.Text = ""
        End If
        txtTipoCambio.Text = "1"
        txtMontoPago.Text = ""
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdFactura.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularFactura(factura.IdFactura, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaFactura()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.bolIncluyeNulos = True
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                factura = Await Puntoventa.ObtenerFactura(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If factura IsNot Nothing Then
                txtIdFactura.Text = factura.ConsecFactura
                cliente = factura.Cliente
                txtNombreCliente.Text = factura.NombreCliente
                txtFecha.Text = factura.Fecha
                txtTelefono.Text = factura.Telefono
                txtObservaciones.Text = factura.TextoAdicional
                intIdProforma = factura.IdProforma
                intIdOrdenServicio = factura.IdOrdenServicio
                intIdApartado = factura.IdApartado
                If factura.IdProforma > 0 Then
                    proforma = Await Puntoventa.ObtenerProforma(factura.IdProforma, FrmPrincipal.usuarioGlobal.Token)
                    txtReferencia.Text = "Proforma nro. " & proforma.ConsecProforma
                End If
                If factura.IdOrdenServicio > 0 Then
                    ordenServicio = Await Puntoventa.ObtenerOrdenServicio(factura.IdOrdenServicio, FrmPrincipal.usuarioGlobal.Token)
                    txtReferencia.Text = "Orden de servicio nro. " & ordenServicio.ConsecOrdenServicio
                End If
                If factura.IdApartado > 0 Then
                    apartado = Await Puntoventa.ObtenerApartado(factura.IdApartado, FrmPrincipal.usuarioGlobal.Token)
                    txtReferencia.Text = "Apartado nro. " & apartado.ConsecApartado
                End If
                cboActividadEconomica.SelectedValue = factura.CodigoActividad
                cboCondicionVenta.SelectedValue = factura.IdCondicionVenta
                txtPlazoCredito.Text = factura.PlazoCredito
                txtPorcentajeExoneracion.Text = factura.PorcentajeExoneracion
                vendedor = factura.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                decMontoAdelanto = factura.MontoAdelanto
                CargarDetalleFactura(factura)
                CargarDesglosePago(factura)
                CargarTotales()
                CargarTotalesPago()
                decPagoCliente = factura.MontoPagado
                txtMontoAdelanto.Text = FormatNumber(decMontoAdelanto, 2)
                cboTipoMoneda.Enabled = False
                cboCondicionVenta.Enabled = False
                txtNombreCliente.ReadOnly = True
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnBusProd.Enabled = False
                btnImprimir.Enabled = Not factura.Nulo
                btnGenerarPDF.Enabled = Not factura.Nulo
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnOrdenServicio.Enabled = False
                btnApartado.Enabled = False
                btnProforma.Enabled = False
                btnAnular.Enabled = Not factura.Nulo And FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
            Else
                MessageBox.Show("No existe registro de factura asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnOrdenServicio_Click(sender As Object, e As EventArgs) Handles btnOrdenServicio.Click
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ordenServicio = Await Puntoventa.ObtenerOrdenServicio(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenServicio IsNot Nothing Then
                txtIdFactura.Text = ""
                cliente = ordenServicio.Cliente
                cboTipoMoneda.SelectedValue = ordenServicio.IdTipoMoneda
                txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, Await FrmPrincipal.ObtenerTipoDeCambioDolar())
                txtReferencia.Text = "Orden de servicio nro. " & ordenServicio.ConsecOrdenServicio
                txtNombreCliente.Text = ordenServicio.NombreCliente
                txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada()
                txtTelefono.Text = ordenServicio.Telefono
                txtObservaciones.Text = ordenServicio.OtrosDetalles
                cboActividadEconomica.SelectedIndex = 0
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                cboCondicionVenta.Enabled = cliente.PermiteCredito
                txtPlazoCredito.Text = ""
                txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
                    vendedor = ordenServicio.Vendedor
                    txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                Else
                    vendedor = Nothing
                    txtVendedor.Text = ""
                End If
                intIdProforma = 0
                intIdOrdenServicio = ordenServicio.IdOrden
                intIdApartado = 0
                CargarDetalleOrdenServicio(ordenServicio)
                dtbDesglosePago.Rows.Clear()
                grdDesglosePago.Refresh()
                decMontoAdelanto = ordenServicio.MontoAdelanto
                txtMontoAdelanto.Text = FormatNumber(decMontoAdelanto, 2)
                decSaldoPorPagar = 0
                decPagoCliente = 0
                txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
                CargarTotales()
                CargarTotalesPago()
                cboTipoMoneda.Enabled = False
                btnInsertar.Enabled = True
                btnEliminar.Enabled = True
                btnInsertarPago.Enabled = True
                btnEliminarPago.Enabled = True
                btnBusProd.Enabled = True
                btnAnular.Enabled = False
                btnGuardar.Enabled = True
                btnImprimir.Enabled = False
                btnGenerarPDF.Enabled = False
                btnBuscarCliente.Enabled = True
                txtMontoPago.Focus()
                txtMontoPago.SelectAll()
            Else
                MessageBox.Show("No existe registro de orden de servicio asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnApartado_Click(sender As Object, e As EventArgs) Handles btnApartado.Click
        Dim formBusqueda As New FrmBusquedaApartado()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                apartado = Await Puntoventa.ObtenerApartado(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If apartado IsNot Nothing Then
                txtIdFactura.Text = ""
                cliente = apartado.Cliente
                cboTipoMoneda.SelectedValue = apartado.IdTipoMoneda
                txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, Await FrmPrincipal.ObtenerTipoDeCambioDolar())
                txtReferencia.Text = "Apartado nro. " & apartado.ConsecApartado
                txtNombreCliente.Text = apartado.NombreCliente
                txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada()
                txtTelefono.Text = apartado.Telefono
                txtObservaciones.Text = apartado.TextoAdicional
                cboActividadEconomica.SelectedIndex = 0
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                cboCondicionVenta.Enabled = cliente.PermiteCredito
                txtPlazoCredito.Text = ""
                txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
                    vendedor = apartado.Vendedor
                    txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                Else
                    vendedor = Nothing
                    txtVendedor.Text = ""
                End If
                intIdProforma = 0
                intIdOrdenServicio = 0
                intIdApartado = apartado.IdApartado
                CargarDetalleApartado(apartado)
                dtbDesglosePago.Rows.Clear()
                grdDesglosePago.Refresh()
                decMontoAdelanto = apartado.MontoAdelanto
                txtMontoAdelanto.Text = FormatNumber(decMontoAdelanto, 2)
                decSaldoPorPagar = 0
                decPagoCliente = 0
                txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
                CargarTotales()
                CargarTotalesPago()
                cboTipoMoneda.Enabled = False
                btnInsertar.Enabled = True
                btnEliminar.Enabled = True
                btnInsertarPago.Enabled = True
                btnEliminarPago.Enabled = True
                btnBusProd.Enabled = True
                btnAnular.Enabled = False
                btnGuardar.Enabled = True
                btnImprimir.Enabled = False
                btnGenerarPDF.Enabled = False
                btnBuscarCliente.Enabled = True
                txtMontoPago.Focus()
                txtMontoPago.SelectAll()
            Else
                MessageBox.Show("No existe registro de orden de servicio asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnProforma_Click(sender As Object, e As EventArgs) Handles btnProforma.Click
        Dim formBusqueda As New FrmBusquedaProforma()
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
                txtIdFactura.Text = ""
                cliente = proforma.Cliente
                cboTipoMoneda.SelectedValue = proforma.IdTipoMoneda
                txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, Await FrmPrincipal.ObtenerTipoDeCambioDolar())
                txtReferencia.Text = "Proforma nro. " & proforma.ConsecProforma
                txtNombreCliente.Text = proforma.NombreCliente
                txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada()
                txtTelefono.Text = proforma.Telefono
                txtObservaciones.Text = proforma.TextoAdicional
                cboActividadEconomica.SelectedIndex = 0
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                cboCondicionVenta.Enabled = cliente.PermiteCredito
                txtPlazoCredito.Text = ""
                txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
                    vendedor = proforma.Vendedor
                    txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                Else
                    vendedor = Nothing
                    txtVendedor.Text = ""
                End If
                intIdProforma = proforma.IdProforma
                intIdOrdenServicio = 0
                intIdApartado = 0
                CargarDetalleProforma(proforma)
                dtbDesglosePago.Rows.Clear()
                grdDesglosePago.Refresh()
                decMontoAdelanto = 0
                txtMontoAdelanto.Text = FormatNumber(decMontoAdelanto, 2)
                decSaldoPorPagar = 0
                decPagoCliente = 0
                txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
                CargarTotales()
                CargarTotalesPago()
                cboTipoMoneda.Enabled = False
                btnInsertar.Enabled = True
                btnEliminar.Enabled = True
                btnInsertarPago.Enabled = True
                btnEliminarPago.Enabled = True
                btnBusProd.Enabled = True
                btnAnular.Enabled = False
                btnGuardar.Enabled = True
                btnImprimir.Enabled = False
                btnGenerarPDF.Enabled = False
                btnBuscarCliente.Enabled = True
                txtMontoPago.Focus()
                txtMontoPago.SelectAll()
            Else
                MessageBox.Show("No existe registro de orden de servicio asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                cboCondicionVenta.Enabled = cliente.PermiteCredito
                txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
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
            .intIdSucursal = FrmPrincipal.equipoGlobal.IdSucursal
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            Dim intIdProducto As Integer = Integer.Parse(FrmPrincipal.strBusqueda)
            Try
                producto = Await Puntoventa.ObtenerProducto(intIdProducto, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show("Error al obtener la información del producto seleccionado. Intente mas tarde.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
            txtCantidad.Focus()
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        Dim decTipoDeCambioDolar = 1
        If vendedor Is Nothing Then
            MessageBox.Show("Debe seleccionar el vendedor para poder guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnBuscaVendedor_Click(btnBuscaVendedor, New EventArgs())
            Exit Sub
        ElseIf decTotal = 0 Then
            MessageBox.Show("Debe agregar líneas de detalle para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado And decSaldoPorPagar > 0 Then
            MessageBox.Show("El total del desglose de pago no es suficiente para cubrir el saldo por pagar actual.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado And decSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago del movimiento es superior al saldo por pagar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf cboCondicionVenta.SelectedValue = StaticCondicionVenta.Credito And (txtPlazoCredito.Text = "" Or txtPlazoCredito.Text = "0") Then
            MessageBox.Show("El valor del campo plazo no puede ser 0 o nulo para una factura de crédito.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPlazoCredito.Focus()
            Exit Sub
        End If
        If (cboTipoMoneda.SelectedValue = 2) Then
            Try
                decTipoDeCambioDolar = Await FrmPrincipal.ObtenerTipoDeCambioDolar()
            Catch ex As Exception
                MessageBox.Show("Ocurrió un error al consultar el tipo de cambio del dólar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        If txtIdFactura.Text = "" Then
            If FrmPrincipal.empresaGlobal.IngresaPagoCliente And decPagoEfectivo > 0 Then
                Dim formPagoFactura As New FrmPagoEfectivo()
                formPagoFactura.decTotalEfectivo = decPagoEfectivo
                formPagoFactura.decPagoCliente = 0
                FrmPrincipal.intBusqueda = 0
                formPagoFactura.ShowDialog()
                If FrmPrincipal.intBusqueda > 0 Then
                    decPagoCliente = FrmPrincipal.intBusqueda
                Else
                    MessageBox.Show("Proceso cancelado por el usuario. Intente guardar de nuevo.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Exit Sub
                End If
            Else
                decPagoCliente = decPagoEfectivo
            End If
            btnImprimir.Focus()
            btnGuardar.Enabled = False
            factura = New Factura With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdTerminal = FrmPrincipal.equipoGlobal.IdTerminal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCliente = cliente.IdCliente,
                .NombreCliente = txtNombreCliente.Text,
                .CodigoActividad = cboActividadEconomica.SelectedValue,
                .IdCondicionVenta = cboCondicionVenta.SelectedValue,
                .PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text),
                .Fecha = Now(),
                .Telefono = txtTelefono.Text,
                .TextoAdicional = txtObservaciones.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = decDescuento,
                .Impuesto = decImpuesto,
                .MontoPagado = decPagoCliente,
                .MontoAdelanto = CDbl(txtMontoAdelanto.Text),
                .TotalCosto = decTotalCosto,
                .Nulo = False,
                .IdOrdenServicio = intIdOrdenServicio,
                .IdProforma = intIdProforma,
                .IdApartado = intIdApartado,
                .IdTipoExoneracion = cliente.IdTipoExoneracion,
                .NumDocExoneracion = cliente.NumDocExoneracion,
                .NombreInstExoneracion = cliente.NombreInstExoneracion,
                .FechaEmisionDoc = cliente.FechaEmisionDoc,
                .PorcentajeExoneracion = cliente.PorcentajeExoneracion,
                .TipoDeCambioDolar = decTipoDeCambioDolar
            }
            factura.DetalleFactura = New List(Of DetalleFactura)
            For I As Short = 0 To dtbDetalleFactura.Rows.Count - 1
                detalleFactura = New DetalleFactura With {
                    .IdProducto = dtbDetalleFactura.Rows(I).Item(0),
                    .Descripcion = dtbDetalleFactura.Rows(I).Item(2),
                    .Cantidad = dtbDetalleFactura.Rows(I).Item(3),
                    .PrecioVenta = dtbDetalleFactura.Rows(I).Item(4),
                    .Excento = dtbDetalleFactura.Rows(I).Item(7),
                    .PrecioCosto = dtbDetalleFactura.Rows(I).Item(8),
                    .PorcentajeIVA = dtbDetalleFactura.Rows(I).Item(9),
                    .PorcDescuento = dtbDetalleFactura.Rows(I).Item(10)
                }
                factura.DetalleFactura.Add(detalleFactura)
            Next
            factura.DesglosePagoFactura = New List(Of DesglosePagoFactura)
            For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoFactura With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                    .TipoTarjeta = dtbDesglosePago.Rows(I).Item(4),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(5),
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(6),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(7),
                    .TipoDeCambio = decTipoDeCambioDolar
                }
                factura.DesglosePagoFactura.Add(desglosePago)
            Next
            Try
                Dim strIdConsec As String = Await Puntoventa.AgregarFactura(factura, FrmPrincipal.usuarioGlobal.Token)
                Dim arrIdConsec = strIdConsec.Split("-")
                factura.IdFactura = arrIdConsec(0)
                factura.ConsecFactura = arrIdConsec(1)
                txtIdFactura.Text = factura.ConsecFactura
            Catch ex As Exception
                txtIdFactura.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        If FrmPrincipal.empresaGlobal.IngresaPagoCliente And decPagoEfectivo > 0 Then
            BtnImprimir_Click(btnImprimir, New EventArgs())
            Dim formPagoFactura As New FrmPagoEfectivo()
            formPagoFactura.decTotalEfectivo = decPagoEfectivo
            formPagoFactura.decPagoCliente = decPagoCliente
            formPagoFactura.ShowDialog()
        Else
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        cboTipoMoneda.Enabled = False
        cboCondicionVenta.Enabled = False
        btnGuardar.Enabled = False
        btnGenerarPDF.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        btnBusProd.Enabled = False
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
        btnOrdenServicio.Enabled = False
        btnApartado.Enabled = False
        btnProforma.Enabled = False
    End Sub

    Private Async Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdFactura.Text <> "" Then
            If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then
                Try
                    factura = Await Puntoventa.ObtenerFactura(factura.IdFactura, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = factura.ConsecFactura,
                    .strFecha = factura.Fecha.ToString("dd/MM/yyyy hh:mm:ss"),
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strDocumento = txtReferencia.Text,
                    .strTelefono = txtTelefono.Text,
                    .strDetalle = txtObservaciones.Text,
                    .strSubTotal = txtSubTotal.Text,
                    .strDescuento = txtDescuento.Text,
                    .strImpuesto = txtImpuesto.Text,
                    .strTotal = txtTotal.Text,
                    .strPagoCon = FormatNumber(decPagoCliente, 2),
                    .strCambio = FormatNumber(decPagoCliente - decPagoEfectivo, 2)
                }
                If factura.IdDocElectronico IsNot Nothing Then
                    comprobanteImpresion.strTipoDocumento = IIf(cliente.IdCliente > 1, "FACTURA ELECTRONICA", "TIQUETE ELECTRONICO")
                    comprobanteImpresion.strClaveNumerica = factura.IdDocElectronico
                Else
                    comprobanteImpresion.strClaveNumerica = ""
                End If
                arrDetalleFactura = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For I As Short = 0 To dtbDetalleFactura.Rows.Count - 1
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleFactura.Rows(I).Item(1) + "-" + dtbDetalleFactura.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleFactura.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleFactura.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleFactura.Rows(I).Item(3)) * CDbl(dtbDetalleFactura.Rows(I).Item(4)), 2),
                    .strExcento = IIf(dtbDetalleFactura.Rows(I).Item(7) = 0, "G", "E")
                }
                    arrDetalleFactura.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleFactura
                arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
                For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(1), FormatNumber(dtbDesglosePago.Rows(I).Item(7), 2))
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                comprobanteImpresion.arrDesglosePago = arrDesglosePago
                ModuloImpresion.ImprimirFactura(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdFactura.Text <> "" Then
            If Not FrmPrincipal.empresaGlobal.RegimenSimplificado And factura IsNot Nothing Then
                Try
                    Dim pdfBytes As Byte() = Await Puntoventa.ObtenerFacturaPDF(factura.IdFactura, FrmPrincipal.usuarioGlobal.Token)
                    Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FAC-" + factura.ConsecFactura.ToString() + ".pdf"
                    File.WriteAllBytes(pdfFilePath, pdfBytes)
                    Process.Start(pdfFilePath)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
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
            CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text, False)
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

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleFactura.Rows.Count > 0 Then
            Dim intId = grdDetalleFactura.CurrentRow.Cells(11).Value
            If bolImpuestoCargado Then
                If dtbDetalleFactura.Rows.Find(intId).Item(13) = 1 Then bolImpuestoCargado = False
            End If
            dtbDetalleFactura.Rows.RemoveAt(dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(intId)))
            grdDetalleFactura.Refresh()
            If dtbDetalleFactura.Rows.Count = 0 Then consecDetalle = 0
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub CboFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If bolReady And cboFormaPago.SelectedValue IsNot Nothing Then
            txtTipoTarjeta.Text = ""
            txtAutorizacion.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario Then
                Try
                    Await CargarListaBancoAdquiriente()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                cboTipoBanco.SelectedIndex = 0
                cboTipoBanco.Width = 325
                lblBanco.Width = 325
                lblBanco.Text = "Banco Adquiriente"
                lblAutorizacion.Text = "Autorización"
                txtTipoTarjeta.Visible = True
                lblTipoTarjeta.Visible = True
                If cboFormaPago.SelectedValue = StaticFormaPago.Tarjeta Then
                    cboTipoBanco.Enabled = True
                    txtTipoTarjeta.ReadOnly = False
                    txtAutorizacion.ReadOnly = False
                Else
                    cboTipoBanco.Enabled = False
                    txtTipoTarjeta.ReadOnly = True
                    txtAutorizacion.ReadOnly = True
                End If
            Else
                Try
                    Await CargarListaCuentaBanco()
                Catch ex As Exception
                    cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                cboTipoBanco.SelectedIndex = 0
                cboTipoBanco.Width = 395
                lblBanco.Width = 395
                lblBanco.Text = "Cuenta Bancaria"
                lblAutorizacion.Text = "Nro. Mov"
                cboTipoBanco.Enabled = True
                txtTipoTarjeta.ReadOnly = True
                txtTipoTarjeta.Visible = False
                lblTipoTarjeta.Visible = False
                txtAutorizacion.ReadOnly = False
            End If
        End If
    End Sub

    Private Async Sub CboTipoMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If bolReady And cboTipoMoneda.SelectedValue IsNot Nothing Then
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, Await FrmPrincipal.ObtenerTipoDeCambioDolar())
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
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboTipoBanco.SelectedValue > 0 And decTotal > 0 And txtMontoPago.Text <> "" Then
            If decSaldoPorPagar = 0 Then
                MessageBox.Show("El monto de por cancelar ya se encuentra cubierto. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDesglosePago()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
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

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio.KeyUp
        If producto IsNot Nothing Then
            If txtPrecio.Text <> "" And e.KeyCode <> Keys.Tab And e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.ShiftKey Then
                decPrecioVenta = Math.Round(CDbl(txtPrecio.Text), 2)
                txtPorcDesc.Text = "0"
            End If
        End If
    End Sub

    Private Sub grdDetalleFactura_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdDetalleFactura.EditingControlShowing
        If grdDetalleFactura.CurrentCell.ColumnIndex = 4 Then
            Dim tb As TextBox = e.Control
            If tb IsNot Nothing Then
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            End If
        End If
    End Sub

    Private Sub TextBox_keyPress(sender As Object, e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, True, 2)
    End Sub

    Private Sub grdDetalleFactura_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleFactura.CellValueChanged
        If e.ColumnIndex = 4 And Not bolAutorizando Then
            bolAutorizando = True
            Dim bolPrecioAutorizado As Boolean = False
            Dim decPorcDesc As Decimal = 0
            Dim decPrecioTotal As Decimal = dtbDetalleFactura.Rows(e.RowIndex).Item(5) + dtbDetalleFactura.Rows(e.RowIndex).Item(11)
            If dtbDetalleFactura.Rows(e.RowIndex).Item(13) = 1 Then
                grdDetalleFactura.Rows(e.RowIndex).Cells(4).Value = 0
            Else
                If Not IsDBNull(grdDetalleFactura.Rows(e.RowIndex).Cells(4).Value) Then
                    decPorcDesc = grdDetalleFactura.Rows(e.RowIndex).Cells(4).Value
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
                grdDetalleFactura.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = decPorcDesc
                Dim decCantidad As Decimal = grdDetalleFactura.Rows(e.RowIndex).Cells(3).Value
                Dim decTasaImpuesto As Decimal = grdDetalleFactura.Rows(e.RowIndex).Cells(10).Value
                Dim decPrecioConDescuento As Decimal = decPrecioTotal - (decPrecioTotal * decPorcDesc / 100)
                If decPorcDesc > 0 And Not bolPrecioAutorizado And FrmPrincipal.empresaGlobal.MontoRedondeoDescuento > 0 Then
                    decPrecioConDescuento = Puntoventa.ObtenerPrecioRedondeado(FrmPrincipal.empresaGlobal.MontoRedondeoDescuento, decPrecioConDescuento)
                    decPorcDesc = (decPrecioTotal - decPrecioConDescuento) / decPrecioTotal * 100
                End If
                Dim decMontoDesc = decPrecioTotal - decPrecioConDescuento
                Dim decPrecioGravado As Decimal = decPrecioConDescuento
                If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecioConDescuento / (1 + (decTasaImpuesto / 100)), 5)
                dtbDetalleFactura.Rows(e.RowIndex).Item(4) = decPrecioGravado
                dtbDetalleFactura.Rows(e.RowIndex).Item(5) = decPrecioConDescuento
                dtbDetalleFactura.Rows(e.RowIndex).Item(6) = decCantidad * decPrecioConDescuento
                dtbDetalleFactura.Rows(e.RowIndex).Item(10) = decPorcDesc
                dtbDetalleFactura.Rows(e.RowIndex).Item(11) = decMontoDesc
                grdDetalleFactura.Refresh()
                CargarTotales()
            End If
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
                    producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, strCodigo, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
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

    Private Sub TxtMontoPago_Validated(sender As Object, e As EventArgs) Handles txtMontoPago.Validated
        If txtMontoPago.Text = "" Then
            txtMontoPago.Text = "0.00"
        ElseIf CDbl(txtMontoPago.Text) > decSaldoPorPagar Then
            MessageBox.Show("El monto ingresado no puede sar mayor al saldo por pagar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        Else
            txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
        End If
    End Sub

    Private Sub TxtMontoPago_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtMontoPago.PreviewKeyDown
        If e.KeyCode = Keys.Enter And txtMontoPago.Text <> "" Then
            If CDbl(txtMontoPago.Text) > decSaldoPorPagar Then
                MessageBox.Show("El monto ingresado no puede sar mayor al saldo por pagar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
            Else
                BtnInsertarPago_Click(btnInsertarPago, New EventArgs())
            End If
        End If
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress, txtPlazoCredito.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPorcDesc.KeyPress, txtPrecio.KeyPress, txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class