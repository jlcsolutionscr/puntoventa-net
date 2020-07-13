Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports System.IO
Imports System.Globalization
Imports LeandroSoftware.Core.Utilitario
Imports LeandroSoftware.ClienteWCF

Public Class FrmOrdenServicio
#Region "Variables"
    Private strMotivoRechazo As String
    Private decDescuento, decSubTotal, decExcento, decGravado, decExonerado, decImpuesto, decTotal, decPagoEfectivo, decPagoCliente, decTotalPago, decSaldoPorPagar, decPrecioVenta As Decimal
    Private I, consecDetalle As Short
    Private dtbDatosLocal, dtbDetalleOrdenServicio, dtbDesglosePago As DataTable
    Private dtrRowDetOrdenServicio, dtrRowDesglosePago As DataRow
    Private arrDetalleOrdenServicio As ArrayList
    Private ordenServicio As OrdenServicio
    Private detalleOrdenServicio As DetalleOrdenServicio
    Private desglosePago As DesglosePagoOrdenServicio
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private bolInit As Boolean = True
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
        dtbDetalleOrdenServicio = New DataTable()
        dtbDetalleOrdenServicio.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("CODIGO", GetType(String))
        dtbDetalleOrdenServicio.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleOrdenServicio.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("PRECIOIVA", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("PORCDESCUENTO", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("VALORDESCUENTO", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("ID", GetType(Integer))
        dtbDetalleOrdenServicio.PrimaryKey = {dtbDetalleOrdenServicio.Columns(11)}

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
        grdDetalleOrdenServicio.Columns.Clear()
        grdDetalleOrdenServicio.AutoGenerateColumns = False

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
        dvcIdProducto.Width = 0
        dvcIdProducto.Visible = False
        grdDetalleOrdenServicio.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 110
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcCantidad)

        dvcPorcDescuento.DataPropertyName = "PORCDESCUENTO"
        dvcPorcDescuento.HeaderText = "Desc"
        dvcPorcDescuento.Width = 40
        dvcPorcDescuento.Visible = True
        dvcPorcDescuento.ReadOnly = False
        dvcPorcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcPorcDescuento)

        dvcDescuento.DataPropertyName = "VALORDESCUENTO"
        dvcDescuento.HeaderText = "Desc"
        dvcDescuento.Width = 75
        dvcDescuento.Visible = True
        dvcDescuento.ReadOnly = True
        dvcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcDescuento)

        dvcPrecio.DataPropertyName = "PRECIOIVA"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.Visible = True
        dvcPrecio.ReadOnly = True
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.Visible = True
        dvcExc.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcExc)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        grdDetalleOrdenServicio.Columns.Add(dvcPorcentajeIVA)

        dvcId.DataPropertyName = "ID"
        dvcId.HeaderText = "Id"
        dvcId.Width = 0
        dvcId.Visible = False
        grdDetalleOrdenServicio.Columns.Add(dvcId)

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
        dvcIdConsecutivo.Width = 0
        dvcIdConsecutivo.Visible = False
        grdDesglosePago.Columns.Add(dvcIdConsecutivo)

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
        dvcDescBanco.Width = 240
        dvcDescBanco.Visible = True
        dvcDescBanco.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcTipoTarjeta.DataPropertyName = "TIPOTARJETA"
        dvcTipoTarjeta.HeaderText = "Tipo Tarjeta"
        dvcTipoTarjeta.Width = 100
        dvcTipoTarjeta.Visible = True
        dvcTipoTarjeta.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcTipoTarjeta)

        dvcNroMovimiento.DataPropertyName = "NROMOVIMIENTO"
        dvcNroMovimiento.HeaderText = "Movimiento #"
        dvcNroMovimiento.Width = 100
        dvcNroMovimiento.Visible = True
        dvcNroMovimiento.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcNroMovimiento)

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

    Private Sub CargarDetalleOrdenServicio(ByVal ordenServicio As OrdenServicio)
        dtbDetalleOrdenServicio.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleOrdenServicio In ordenServicio.DetalleOrdenServicio
            consecDetalle += 1
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = detalle.IdProducto
            dtrRowDetOrdenServicio.Item(1) = detalle.Producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = detalle.Descripcion
            dtrRowDetOrdenServicio.Item(3) = detalle.Cantidad
            dtrRowDetOrdenServicio.Item(4) = detalle.PrecioVenta
            dtrRowDetOrdenServicio.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero)
            dtrRowDetOrdenServicio.Item(6) = dtrRowDetOrdenServicio.Item(3) * dtrRowDetOrdenServicio.Item(5)
            dtrRowDetOrdenServicio.Item(7) = detalle.Excento
            dtrRowDetOrdenServicio.Item(8) = detalle.PorcentajeIVA
            dtrRowDetOrdenServicio.Item(9) = detalle.PorcDescuento
            dtrRowDetOrdenServicio.Item(10) = (dtrRowDetOrdenServicio.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetOrdenServicio.Item(5)
            dtrRowDetOrdenServicio.Item(11) = consecDetalle
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        Next
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Sub CargarDesglosePago(ordenServicio As OrdenServicio)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoOrdenServicio In ordenServicio.DesglosePagoOrdenServicio
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = detalle.FormaPago.Descripcion
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

    Private Sub CargarLineaDetalleOrdenServicio(producto As Producto, strDescripcion As String, decCantidad As Decimal, decPrecio As Decimal, decPorcDesc As Decimal)
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.AplicaTasaDiferenciada Then decTasaImpuesto = cliente.ParametroImpuesto.TasaImpuesto
        Dim decPrecioGravado As Decimal = decPrecio
        If decTasaImpuesto > 0 Then
            decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 3, MidpointRounding.AwayFromZero)
        End If
        Dim intIndice As Integer = ObtenerIndice(dtbDetalleOrdenServicio, producto.IdProducto)
        If producto.Tipo = 1 And intIndice >= 0 Then
            Dim decNewCantidad = dtbDetalleOrdenServicio.Rows(intIndice).Item(3) + decCantidad
            dtbDetalleOrdenServicio.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenServicio.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(3) = decNewCantidad
            dtbDetalleOrdenServicio.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleOrdenServicio.Rows(intIndice).Item(5) = decPrecio
            dtbDetalleOrdenServicio.Rows(intIndice).Item(6) = decNewCantidad * decPrecio
            dtbDetalleOrdenServicio.Rows(intIndice).Item(7) = decTasaImpuesto = 0
            dtbDetalleOrdenServicio.Rows(intIndice).Item(8) = decTasaImpuesto
            dtbDetalleOrdenServicio.Rows(intIndice).Item(9) = decPorcDesc
            dtbDetalleOrdenServicio.Rows(intIndice).Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
        Else
            consecDetalle += 1
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = producto.IdProducto
            dtrRowDetOrdenServicio.Item(1) = producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = strDescripcion
            dtrRowDetOrdenServicio.Item(3) = decCantidad
            dtrRowDetOrdenServicio.Item(4) = decPrecioGravado
            dtrRowDetOrdenServicio.Item(5) = decPrecio
            dtrRowDetOrdenServicio.Item(6) = decCantidad * decPrecio
            dtrRowDetOrdenServicio.Item(7) = decTasaImpuesto = 0
            dtrRowDetOrdenServicio.Item(8) = decTasaImpuesto
            dtrRowDetOrdenServicio.Item(9) = decPorcDesc
            dtrRowDetOrdenServicio.Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
            dtrRowDetOrdenServicio.Item(11) = consecDetalle
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        End If
        grdDetalleOrdenServicio.Refresh()
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
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(8)
            decDescuento += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(10) / (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(4) * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = dtbDetalleOrdenServicio.Rows(I).Item(4) * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += Math.Round(decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
                    decExonerado += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4) - decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
                    decImpuestoProducto = decGravadoPorcentual * decTasaImpuesto / 100
                Else
                    decGravado += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
                End If
                decImpuesto += Math.Round(decImpuestoProducto, 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            Else
                decExcento += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            End If
        Next
        decSubTotal = decGravado + decExcento + decExonerado
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExonerado = Math.Round(decExonerado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2, MidpointRounding.AwayFromZero)
        decSaldoPorPagar = decTotal - decTotalPago
        txtSubTotal.Text = FormatNumber(decSubTotal + decDescuento, 2)
        txtDescuento.Text = FormatNumber(decDescuento, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarTotalesPago()
        decTotalPago = 0
        decPagoEfectivo = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            If dtbDesglosePago.Rows(I).Item(0) = StaticFormaPago.Efectivo Then decPagoEfectivo = CDbl(dtbDesglosePago.Rows(I).Item(7))
            decTotalPago = decTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(7))
        Next
        decSaldoPorPagar = decTotal - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = Await Puntoventa.ObtenerListadoFormaPagoCliente(FrmPrincipal.usuarioGlobal.Token)
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = Await Puntoventa.ObtenerListadoTipoMoneda(FrmPrincipal.usuarioGlobal.Token)
    End Function

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
            decPrecioVenta = Math.Round(decPrecioVenta / (1 + (producto.ParametroImpuesto.TasaImpuesto / 100)), 3)
            decPrecioVenta = Math.Round(decPrecioVenta * (1 + (cliente.ParametroImpuesto.TasaImpuesto / 100)), 2)
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
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
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

    Private Function ObtenerLeyendaEntrega(datFecha As Date, strEntrega As String) As String
        Dim strLeyenda = datFecha.ToString().Substring(0, 5) & "-" & strEntrega
        Select Case datFecha.DayOfWeek
            Case DayOfWeek.Monday
                Return "Lunes " & strLeyenda
            Case DayOfWeek.Tuesday
                Return "Martes " & strLeyenda
            Case DayOfWeek.Wednesday
                Return "Miércoles " & strLeyenda
            Case DayOfWeek.Thursday
                Return "Jueves " & strLeyenda
            Case DayOfWeek.Friday
                Return "Viernes " & strLeyenda
            Case DayOfWeek.Saturday
                Return "Sábado " & strLeyenda
            Case DayOfWeek.Sunday
                Return "Domingo " & strLeyenda
            Case Else
                Return ""
        End Select
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmOrdenServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmOrdenServicio_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.F2 Then
            If FrmPrincipal.productoTranstorio IsNot Nothing Then
                Dim formCargar As New FrmCargaProductoTransitorio
                formCargar.ShowDialog()
                If FrmPrincipal.productoTranstorio.PrecioVenta1 > 0 Then
                    CargarLineaDetalleOrdenServicio(FrmPrincipal.productoTranstorio, FrmPrincipal.productoTranstorio.Descripcion, FrmPrincipal.productoTranstorio.Existencias, FrmPrincipal.productoTranstorio.PrecioVenta1, 0)
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

    Private Async Sub FrmOrdenServicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
            txtFechaEntrega.Value = Today()
            cboHoraEntrega.SelectedIndex = 0
            Await CargarListaBancoAdquiriente()
            grdDetalleOrdenServicio.DataSource = dtbDetalleOrdenServicio
            grdDesglosePago.DataSource = dtbDesglosePago
            consecDetalle = 0
            bolInit = False
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
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
            If FrmPrincipal.bolModificaDescripcion Then txtDescripcion.ReadOnly = False
            If FrmPrincipal.bolModificaCliente Then txtPorcDesc.ReadOnly = False
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdOrdenServicio.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        cboTipoMoneda.Enabled = True
        txtFechaEntrega.Value = Today()
        cboHoraEntrega.SelectedIndex = 0
        txtTelefono.Text = ""
        txtDireccion.Text = ""
        txtDescripcionOrden.Text = ""
        txtFechaEntrega.Text = ""
        txtOtrosDetalles.Text = ""
        dtbDetalleOrdenServicio.Rows.Clear()
        grdDetalleOrdenServicio.Refresh()
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
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        decTotal = 0
        decTotalPago = 0
        decPagoEfectivo = 0
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        btnGuardar.Enabled = True
        btnAnular.Enabled = False
        btnImprimir.Enabled = False
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
        bolInit = False
        txtMontoPago.Text = ""
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdOrdenServicio.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularOrdenServicio(ordenServicio.IdOrden, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        formBusqueda.bolIncluyeEstado = True
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
                bolInit = True
                txtIdOrdenServicio.Text = ordenServicio.ConsecOrdenServicio
                cliente = ordenServicio.Cliente
                txtNombreCliente.Text = ordenServicio.NombreCliente
                txtFecha.Text = ordenServicio.Fecha
                cboTipoMoneda.SelectedValue = ordenServicio.IdTipoMoneda
                txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
                txtTelefono.Text = ordenServicio.Telefono
                txtDireccion.Text = ordenServicio.Direccion
                txtDescripcionOrden.Text = ordenServicio.Descripcion
                txtFechaEntrega.Value = ordenServicio.FechaEntrega
                cboHoraEntrega.SelectedIndex = IIf(ordenServicio.HoraEntrega = "Tarde", 1, 0)
                txtOtrosDetalles.Text = ordenServicio.OtrosDetalles
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = cliente.ParametroExoneracion.Descripcion
                    txtNumDocExoneracion.Text = cliente.NumDocExoneracion
                    txtNombreInstExoneracion.Text = cliente.NombreInstExoneracion
                    txtFechaExoneracion.Text = cliente.FechaEmisionDoc
                    txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                End If
                vendedor = ordenServicio.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleOrdenServicio(ordenServicio)
                CargarDesglosePago(ordenServicio)
                CargarTotales()
                CargarTotalesPago()
                decPagoCliente = ordenServicio.MontoPagado
                cboTipoMoneda.Enabled = False
                txtNombreCliente.ReadOnly = True
                btnImprimir.Enabled = True
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnGuardar.Enabled = ordenServicio.Aplicado = False
                btnAnular.Enabled = ordenServicio.Aplicado = False And FrmPrincipal.bolAnularTransacciones
                bolInit = False
            Else
                MessageBox.Show("No existe registro de OrdenServicio asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                txtTelefono.Text = cliente.Telefono & IIf(cliente.Celular <> "", " " & cliente.Celular, "")
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = cliente.ParametroExoneracion.Descripcion
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
        If vendedor Is Nothing Then
            MessageBox.Show("Debe seleccionar el vendedor para poder guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnBuscaVendedor_Click(btnBuscaVendedor, New EventArgs())
            Exit Sub
        ElseIf decTotal = 0 Then
            MessageBox.Show("Debe agregar líneas de detalle para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar = 0 Then
            MessageBox.Show("La orden de servicio no puede ser cancelada en su totalidad.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago de la factura es superior al saldo por pagar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdOrdenServicio.Text = "" Then
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
            ordenServicio = New OrdenServicio With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCliente = cliente.IdCliente,
                .NombreCliente = txtNombreCliente.Text,
                .Fecha = Now(),
                .IdVendedor = vendedor.IdVendedor,
                .Telefono = txtTelefono.Text,
                .Direccion = txtDireccion.Text,
                .Descripcion = txtDescripcionOrden.Text,
                .FechaEntrega = txtFechaEntrega.Value.ToString().Substring(0, 10),
                .HoraEntrega = cboHoraEntrega.Text,
                .OtrosDetalles = txtOtrosDetalles.Text,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = 0,
                .Impuesto = decImpuesto,
                .MontoAdelanto = decTotalPago,
                .MontoPagado = decPagoCliente,
                .Nulo = False
            }
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                detalleOrdenServicio = New DetalleOrdenServicio
                detalleOrdenServicio.IdProducto = dtbDetalleOrdenServicio.Rows(I).Item(0)
                detalleOrdenServicio.Descripcion = dtbDetalleOrdenServicio.Rows(I).Item(2)
                detalleOrdenServicio.Cantidad = dtbDetalleOrdenServicio.Rows(I).Item(3)
                detalleOrdenServicio.PrecioVenta = dtbDetalleOrdenServicio.Rows(I).Item(4)
                detalleOrdenServicio.Excento = dtbDetalleOrdenServicio.Rows(I).Item(7)
                detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(8)
                detalleOrdenServicio.PorcDescuento = dtbDetalleOrdenServicio.Rows(I).Item(9)
                ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
            Next
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoOrdenServicio With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                    .TipoTarjeta = dtbDesglosePago.Rows(I).Item(4),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(5),
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(6),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(7),
                    .TipoDeCambio = dtbDesglosePago.Rows(I).Item(8)
                }
                ordenServicio.DesglosePagoOrdenServicio.Add(desglosePago)
            Next
            Try
                Dim strIdConsec As String = Await Puntoventa.AgregarOrdenServicio(ordenServicio, FrmPrincipal.usuarioGlobal.Token)
                Dim arrIdConsec = strIdConsec.Split("-")
                ordenServicio.IdOrden = arrIdConsec(0)
                ordenServicio.ConsecOrdenServicio = arrIdConsec(1)
                txtIdOrdenServicio.Text = ordenServicio.ConsecOrdenServicio
            Catch ex As Exception
                txtIdOrdenServicio.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If FrmPrincipal.empresaGlobal.IngresaPagoCliente And decPagoEfectivo > 0 Then
                BtnImprimir_Click(btnImprimir, New EventArgs())
                Dim formPagoFactura As New FrmPagoEfectivo()
                formPagoFactura.decTotalEfectivo = decPagoEfectivo
                formPagoFactura.decPagoCliente = decPagoCliente
                formPagoFactura.ShowDialog()
            Else
                MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Else
            ordenServicio.Telefono = txtTelefono.Text
            ordenServicio.Direccion = txtDireccion.Text
            ordenServicio.Descripcion = txtDescripcionOrden.Text
            ordenServicio.FechaEntrega = txtFechaEntrega.Value.ToString().Substring(0, 10)
            ordenServicio.HoraEntrega = cboHoraEntrega.Text
            ordenServicio.OtrosDetalles = txtOtrosDetalles.Text
            ordenServicio.Excento = decExcento
            ordenServicio.Gravado = decGravado
            ordenServicio.Exonerado = decExonerado
            ordenServicio.Descuento = 0
            ordenServicio.Impuesto = CDbl(txtImpuesto.Text)
            ordenServicio.DetalleOrdenServicio.Clear()
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                detalleOrdenServicio = New DetalleOrdenServicio
                detalleOrdenServicio.IdOrden = ordenServicio.IdOrden
                detalleOrdenServicio.IdProducto = dtbDetalleOrdenServicio.Rows(I).Item(0)
                detalleOrdenServicio.Descripcion = dtbDetalleOrdenServicio.Rows(I).Item(2)
                detalleOrdenServicio.Cantidad = dtbDetalleOrdenServicio.Rows(I).Item(3)
                detalleOrdenServicio.PrecioVenta = dtbDetalleOrdenServicio.Rows(I).Item(4)
                detalleOrdenServicio.Excento = dtbDetalleOrdenServicio.Rows(I).Item(7)
                detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(8)
                detalleOrdenServicio.PorcDescuento = dtbDetalleOrdenServicio.Rows(I).Item(9)
                ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
            Next
            Try
                Await Puntoventa.ActualizarOrdenServicio(ordenServicio, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGuardar.Enabled = True
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        cboTipoMoneda.Enabled = False
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Async Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdOrdenServicio.Text <> "" Then
            If ordenServicio.ConsecOrdenServicio = 0 Then
                Try
                    ordenServicio = Await Puntoventa.ObtenerOrdenServicio(txtIdOrdenServicio.Text, FrmPrincipal.usuarioGlobal.Token)
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
                    .strId = ordenServicio.ConsecOrdenServicio,
                    .strFecha = ordenServicio.Fecha.ToString("dd/MM/yyyy hh:mm:ss"),
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strTelefono = txtTelefono.Text,
                    .strDireccion = txtDireccion.Text,
                    .strDescripcion = txtDescripcionOrden.Text,
                    .strDetalle = txtOtrosDetalles.Text,
                    .strDocumento = ObtenerLeyendaEntrega(Date.ParseExact(ordenServicio.FechaEntrega, "dd/MM/yyyy", Nothing), ordenServicio.HoraEntrega),
                    .strSubTotal = txtSubTotal.Text,
                    .strDescuento = txtDescuento.Text,
                    .strImpuesto = txtImpuesto.Text,
                    .strTotal = txtTotal.Text,
                    .strAdelanto = FormatNumber(ordenServicio.MontoAdelanto, 2),
                    .strSaldo = FormatNumber(ordenServicio.Total - ordenServicio.MontoAdelanto, 2),
                    .strPagoCon = FormatNumber(decPagoCliente, 2),
                    .strCambio = FormatNumber(decPagoCliente - decPagoEfectivo, 2)
                }
                arrDetalleOrden = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleOrdenServicio.Rows(I).Item(1) + "-" + dtbDetalleOrdenServicio.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleOrdenServicio.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleOrdenServicio.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleOrdenServicio.Rows(I).Item(3)) * CDbl(dtbDetalleOrdenServicio.Rows(I).Item(4)), 2),
                    .strExcento = IIf(dtbDetalleOrdenServicio.Rows(I).Item(7) = 0, "G", "E")
                }
                    arrDetalleOrden.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleOrden
                arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
                For I = 0 To dtbDesglosePago.Rows.Count - 1
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(1), FormatNumber(dtbDesglosePago.Rows(I).Item(7), 2))
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                comprobanteImpresion.arrDesglosePago = arrDesglosePago
                ModuloImpresion.ImprimirOrdenServicio(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdOrdenServicio.Text <> "" Then
            If ordenServicio.ConsecOrdenServicio = 0 Then
                Try
                    ordenServicio = Await Puntoventa.ObtenerOrdenServicio(txtIdOrdenServicio.Text, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            Dim datos As EstructuraPDF = New EstructuraPDF()
            Try
                Dim poweredByImage As Image = My.Resources.logo
                datos.PoweredByLogotipo = poweredByImage
            Catch ex As Exception
                datos.PoweredByLogotipo = Nothing
            End Try
            Try
                Dim logotipo As Byte() = Await Puntoventa.ObtenerLogotipoEmpresa(ordenServicio.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                Dim logoImage As Image
                Using ms As New MemoryStream(logotipo)
                    logoImage = Image.FromStream(ms)
                End Using
                datos.Logotipo = logoImage
            Catch ex As Exception
                datos.Logotipo = Nothing
            End Try
            datos.TituloDocumento = "ORDEN DE SERVICIO"
            datos.NombreEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
            datos.NombreComercial = FrmPrincipal.empresaGlobal.NombreComercial
            datos.ConsecInterno = ordenServicio.ConsecOrdenServicio
            datos.Consecutivo = Nothing
            datos.Clave = Nothing
            datos.CondicionVenta = "Efectivo"
            datos.PlazoCredito = ""
            datos.Fecha = ordenServicio.Fecha.ToString("dd/MM/yyyy hh:mm:ss")
            datos.MedioPago = ""
            datos.NombreEmisor = FrmPrincipal.empresaGlobal.NombreEmpresa
            datos.NombreComercialEmisor = FrmPrincipal.empresaGlobal.NombreComercial
            datos.IdentificacionEmisor = FrmPrincipal.empresaGlobal.Identificacion
            datos.CorreoElectronicoEmisor = FrmPrincipal.empresaGlobal.CorreoNotificacion
            datos.TelefonoEmisor = FrmPrincipal.empresaGlobal.Telefono1 + IIf(FrmPrincipal.empresaGlobal.Telefono2.Length > 0, " - " + FrmPrincipal.empresaGlobal.Telefono2, "")
            datos.FaxEmisor = ""
            datos.ProvinciaEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Provincia.Descripcion
            datos.CantonEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Descripcion
            datos.DistritoEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Descripcion
            datos.BarrioEmisor = FrmPrincipal.empresaGlobal.Barrio.Descripcion
            datos.DireccionEmisor = FrmPrincipal.empresaGlobal.Direccion
            If ordenServicio.IdCliente > 1 Then
                datos.PoseeReceptor = True
                datos.NombreReceptor = cliente.Nombre
                datos.NombreComercialReceptor = cliente.NombreComercial
                datos.IdentificacionReceptor = cliente.Identificacion
                datos.CorreoElectronicoReceptor = cliente.CorreoElectronico
                datos.TelefonoReceptor = cliente.Telefono
                datos.FaxReceptor = cliente.Fax
            End If
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                Dim decPrecioVenta As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(4)
                Dim decTotalLinea As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(3) * decPrecioVenta
                Dim detalle As EstructuraPDFDetalleServicio = New EstructuraPDFDetalleServicio With {
                .Cantidad = dtbDetalleOrdenServicio.Rows(I).Item(3),
                .Codigo = dtbDetalleOrdenServicio.Rows(I).Item(1),
                .Detalle = dtbDetalleOrdenServicio.Rows(I).Item(2),
                .PrecioUnitario = decPrecioVenta.ToString("N2", CultureInfo.InvariantCulture),
                .TotalLinea = decTotalLinea.ToString("N2", CultureInfo.InvariantCulture)
            }
                datos.DetalleServicio.Add(detalle)
            Next
            datos.TotalGravado = decGravado.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalExonerado = "0.00"
            datos.TotalExento = decExcento.ToString("N2", CultureInfo.InvariantCulture)
            datos.Descuento = "0.00"
            datos.Impuesto = decImpuesto.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalGeneral = decTotal.ToString("N2", CultureInfo.InvariantCulture)
            datos.CodigoMoneda = "CRC"
            datos.TipoDeCambio = 1
            Try
                Dim pdfBytes As Byte() = UtilitarioPDF.GenerarPDF(datos)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ORDENSERVICIO-" + txtIdOrdenServicio.Text + ".pdf"
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
            CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text)
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
        If grdDetalleOrdenServicio.Rows.Count > 0 Then
            Dim intId = grdDetalleOrdenServicio.CurrentRow.Cells(10).Value
            dtbDetalleOrdenServicio.Rows.RemoveAt(dtbDetalleOrdenServicio.Rows.IndexOf(dtbDetalleOrdenServicio.Rows.Find(intId)))
            grdDetalleOrdenServicio.Refresh()
            If dtbDetalleOrdenServicio.Rows.Count = 0 Then consecDetalle = 0
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub CboFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
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

    Private Sub CboTipoMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
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
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text), 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub grdDetalleOrdenServicio_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdDetalleOrdenServicio.EditingControlShowing
        If grdDetalleOrdenServicio.CurrentCell.ColumnIndex = 4 Then
            Dim tb As TextBox = e.Control
            If tb IsNot Nothing Then
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            End If
        End If
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Char.IsDigit(CChar(CStr(e.KeyChar))) = False Then e.Handled = True
    End Sub

    Private Async Sub grdDetalleOrdenServicio_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleOrdenServicio.CellValueChanged
        If e.ColumnIndex = 4 And Not bolAutorizando Then
            bolAutorizando = True
            Dim decPorcDesc As Decimal = 0
            If Not IsDBNull(grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                decPorcDesc = grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            End If
            If decPorcDesc > FrmPrincipal.empresaGlobal.PorcentajeDescMaximo And decPorcDesc > FrmPrincipal.usuarioGlobal.PorcMaxDescuento Then
                Dim strEntidad = "la empresa"
                If decPorcDesc > FrmPrincipal.usuarioGlobal.PorcMaxDescuento Then strEntidad += "el usuario actual"
                If MessageBox.Show("El porcentaje ingresado es mayor al parámetro establecido para " & strEntidad & ". Desea ingresar una autorización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    FrmPrincipal.strCodigoUsuario = ""
                    FrmPrincipal.strContrasena = ""
                    Dim formAutorizacion As New FrmAutorizacionEspecial
                    formAutorizacion.ShowDialog()
                    If FrmPrincipal.strCodigoUsuario <> "" And FrmPrincipal.strContrasena <> "" Then
                        Dim decPorcentaje As Decimal
                        Try
                            decPorcentaje = Await Puntoventa.AutorizacionPorcentaje(FrmPrincipal.strCodigoUsuario, FrmPrincipal.strContrasena, FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            decPorcDesc = 0
                        End Try
                        If decPorcentaje < decPorcDesc Then
                            MessageBox.Show("El usuario ingresado no puede autorizar el porcentaje solicitado.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            decPorcDesc = 0
                        End If
                    Else
                        MessageBox.Show("Proceso de autorización abortado por el usuario.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        decPorcDesc = 0
                    End If
                Else
                    decPorcDesc = 0
                End If
            End If
            grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = decPorcDesc
            Dim decCantidad As Decimal = grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(3).Value
            Dim decTasaImpuesto As Decimal = grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(9).Value
            Dim decPrecio As Decimal = grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(6).Value + grdDetalleOrdenServicio.Rows(e.RowIndex).Cells(5).Value
            Dim decMontoDesc = decPrecio / 100 * decPorcDesc
            decPrecio = decPrecio - decMontoDesc
            Dim decPrecioGravado As Decimal = decPrecio
            If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 3, MidpointRounding.AwayFromZero)
            dtbDetalleOrdenServicio.Rows(e.RowIndex).Item(4) = decPrecioGravado
            dtbDetalleOrdenServicio.Rows(e.RowIndex).Item(5) = decPrecio
            dtbDetalleOrdenServicio.Rows(e.RowIndex).Item(6) = decCantidad * decPrecio
            dtbDetalleOrdenServicio.Rows(e.RowIndex).Item(9) = decPorcDesc
            dtbDetalleOrdenServicio.Rows(e.RowIndex).Item(10) = decMontoDesc
            CargarTotales()
            bolAutorizando = False
        End If
    End Sub

    Private Async Sub txtPorcDesc_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPorcDesc.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtPorcDesc.Text = "" Then txtPorcDesc.Text = "0"
            Dim decPorcDesc As Decimal = CDbl(txtPorcDesc.Text)
            If producto IsNot Nothing Then
                decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                If decPorcDesc > FrmPrincipal.empresaGlobal.PorcentajeDescMaximo And decPorcDesc > FrmPrincipal.usuarioGlobal.PorcMaxDescuento Then
                    Dim strEntidad = "la empresa"
                    If decPorcDesc > FrmPrincipal.usuarioGlobal.PorcMaxDescuento Then strEntidad += "el usuario actual"
                    If MessageBox.Show("El porcentaje ingresado es mayor al parámetro establecido para " & strEntidad & ". Desea ingresar una autorización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        FrmPrincipal.strCodigoUsuario = ""
                        FrmPrincipal.strContrasena = ""
                        Dim formAutorizacion As New FrmAutorizacionEspecial
                        formAutorizacion.ShowDialog()
                        If FrmPrincipal.strCodigoUsuario <> "" And FrmPrincipal.strContrasena <> "" Then
                            Dim decPorcentaje As Decimal
                            Try
                                decPorcentaje = Await Puntoventa.AutorizacionPorcentaje(FrmPrincipal.strCodigoUsuario, FrmPrincipal.strContrasena, FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                decPorcDesc = 0
                            End Try
                            If decPorcentaje < decPorcDesc Then
                                MessageBox.Show("El usuario ingresado no puede autorizar el porcentaje solicitado.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                decPorcDesc = 0
                            End If
                        Else
                            MessageBox.Show("Proceso de autorización abortado por el usuario.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            decPorcDesc = 0
                        End If
                    Else
                        decPorcDesc = 0
                    End If
                End If
                txtPorcDesc.Text = decPorcDesc
                decPorcDesc /= 100
                decPrecioVenta -= (decPrecioVenta * decPorcDesc)
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
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    If producto.Activo Then
                        CargarDatosProducto(producto)
                        txtCantidad.Focus()
                    End If
                Else
                    txtCodigo.Text = ""
                    txtDescripcion.Text = ""
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
        If e.KeyCode = Keys.Enter And txtIdOrdenServicio.Text = "" And txtMontoPago.Text <> "" Then
            If CDbl(txtMontoPago.Text) > decSaldoPorPagar Then
                MessageBox.Show("El monto ingresado no puede sar mayor al saldo por pagar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
            Else
                BtnInsertarPago_Click(btnInsertarPago, New EventArgs())
            End If
        End If
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPorcDesc.KeyPress, txtPrecio.KeyPress, txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class