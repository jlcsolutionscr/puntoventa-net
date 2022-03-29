Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Constantes
Imports System.Collections.Generic
Imports System.Threading.Tasks

Public Class FrmApartado
#Region "Variables"
    Private decDescuento, decExcento, decGravado, decExonerado, decImpuesto, decTotal, decSubTotal, decPrecioVenta, decPagoEfectivo, decPagoCliente, decTotalPago, decSaldoPorPagar As Decimal
    Private consecDetalle As Short
    Private dtbDetalleApartado, dtbDesglosePago As DataTable
    Private dtrRowDetApartado, dtrRowDesglosePago As DataRow
    Private apartado As Apartado
    Private detalleApartado As DetalleApartado
    Private desglosePago As DesglosePagoApartado
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
        dtbDetalleApartado = New DataTable()
        dtbDetalleApartado.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleApartado.Columns.Add("CODIGO", GetType(String))
        dtbDetalleApartado.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleApartado.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("PRECIOIVA", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleApartado.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("PORCDESCUENTO", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("VALORDESCUENTO", GetType(Decimal))
        dtbDetalleApartado.Columns.Add("ID", GetType(Integer))
        dtbDetalleApartado.PrimaryKey = {dtbDetalleApartado.Columns(11)}

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
        grdDetalleApartado.Columns.Clear()
        grdDetalleApartado.AutoGenerateColumns = False

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
        grdDetalleApartado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 110
        dvcCodigo.ReadOnly = True
        dvcCodigo.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleApartado.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.ReadOnly = True
        dvcDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleApartado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.ReadOnly = True
        dvcCantidad.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleApartado.Columns.Add(dvcCantidad)

        dvcPorcDescuento.DataPropertyName = "PORCDESCUENTO"
        dvcPorcDescuento.HeaderText = "Des%"
        dvcPorcDescuento.Width = 40
        dvcPorcDescuento.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPorcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleApartado.Columns.Add(dvcPorcDescuento)

        dvcDescuento.DataPropertyName = "VALORDESCUENTO"
        dvcDescuento.HeaderText = "Desc/U"
        dvcDescuento.Width = 75
        dvcDescuento.ReadOnly = True
        dvcDescuento.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleApartado.Columns.Add(dvcDescuento)

        dvcPrecio.DataPropertyName = "PRECIOIVA"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.ReadOnly = True
        dvcPrecio.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleApartado.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.ReadOnly = True
        dvcTotal.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleApartado.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.ReadOnly = True
        dvcExc.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleApartado.Columns.Add(dvcExc)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Visible = False
        grdDetalleApartado.Columns.Add(dvcPorcentajeIVA)

        dvcId.DataPropertyName = "ID"
        dvcId.HeaderText = "Id"
        dvcId.Visible = False
        grdDetalleApartado.Columns.Add(dvcId)

        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdFormaPago As New DataGridViewTextBoxColumn
        Dim dvcDescFormaPago As New DataGridViewTextBoxColumn
        Dim dvcIdCuentaBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcTipoTarjeta As New DataGridViewTextBoxColumn
        Dim dvcNroMovimiento As New DataGridViewTextBoxColumn
        Dim dvcPlazo As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcDescTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcTipoCambio As New DataGridViewTextBoxColumn

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

    Private Sub CargarDetalleApartado(apartado As Apartado)
        dtbDetalleApartado.Rows.Clear()
        consecDetalle = 0
        For Each detalle As DetalleApartado In apartado.DetalleApartado
            consecDetalle += 1
            dtrRowDetApartado = dtbDetalleApartado.NewRow
            dtrRowDetApartado.Item(0) = detalle.IdProducto
            dtrRowDetApartado.Item(1) = detalle.Producto.Codigo
            dtrRowDetApartado.Item(2) = detalle.Descripcion
            dtrRowDetApartado.Item(3) = detalle.Cantidad
            dtrRowDetApartado.Item(4) = detalle.PrecioVenta
            dtrRowDetApartado.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2)
            dtrRowDetApartado.Item(6) = dtrRowDetApartado.Item(3) * dtrRowDetApartado.Item(5)
            dtrRowDetApartado.Item(7) = detalle.Excento
            dtrRowDetApartado.Item(8) = detalle.PorcentajeIVA
            dtrRowDetApartado.Item(9) = detalle.PorcDescuento
            dtrRowDetApartado.Item(10) = (dtrRowDetApartado.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetApartado.Item(5)
            dtrRowDetApartado.Item(11) = consecDetalle
            dtbDetalleApartado.Rows.Add(dtrRowDetApartado)
        Next
        grdDetalleApartado.Refresh()
    End Sub

    Private Sub CargarDesglosePago(apartado As Apartado)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoApartado In apartado.DesglosePagoApartado
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

    Private Sub CargarLineaDetalleApartado(producto As Producto, strDescripcion As String, decCantidad As Decimal, decPrecio As Decimal, decPorcDesc As Decimal)
        Dim decTasaImpuesto As Decimal = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
        If cliente.AplicaTasaDiferenciada Then
            decTasaImpuesto = FrmPrincipal.ObtenerTarifaImpuesto(cliente.IdImpuesto)
        End If
        Dim decPrecioGravado As Decimal = decPrecio
        If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 5)
        Dim intIndice As Integer = ObtenerIndice(dtbDetalleApartado, producto.IdProducto)
        If producto.Tipo = 1 And intIndice >= 0 Then
            Dim decNewCantidad = dtbDetalleApartado.Rows(intIndice).Item(3) + decCantidad
            dtbDetalleApartado.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleApartado.Rows(intIndice).Item(3) = decNewCantidad
            dtbDetalleApartado.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleApartado.Rows(intIndice).Item(5) = decPrecio
            dtbDetalleApartado.Rows(intIndice).Item(6) = decNewCantidad * decPrecio
            dtbDetalleApartado.Rows(intIndice).Item(7) = decTasaImpuesto = 0
            dtbDetalleApartado.Rows(intIndice).Item(8) = decTasaImpuesto
            dtbDetalleApartado.Rows(intIndice).Item(9) = decPorcDesc
            dtbDetalleApartado.Rows(intIndice).Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
        Else
            consecDetalle += 1
            dtrRowDetApartado = dtbDetalleApartado.NewRow
            dtrRowDetApartado.Item(0) = producto.IdProducto
            dtrRowDetApartado.Item(1) = producto.Codigo
            dtrRowDetApartado.Item(2) = strDescripcion
            dtrRowDetApartado.Item(3) = decCantidad
            dtrRowDetApartado.Item(4) = decPrecioGravado
            dtrRowDetApartado.Item(5) = decPrecio
            dtrRowDetApartado.Item(6) = decCantidad * decPrecio
            dtrRowDetApartado.Item(7) = decTasaImpuesto = 0
            dtrRowDetApartado.Item(8) = decTasaImpuesto
            dtrRowDetApartado.Item(9) = decPorcDesc
            dtrRowDetApartado.Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
            dtrRowDetApartado.Item(11) = consecDetalle
            dtbDetalleApartado.Rows.Add(dtrRowDetApartado)
        End If
        grdDetalleApartado.Refresh()
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
        For I As Short = 0 To dtbDetalleApartado.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleApartado.Rows(I).Item(8)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleApartado.Rows(I).Item(4) * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = dtbDetalleApartado.Rows(I).Item(4) * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += Math.Round(decGravadoPorcentual * dtbDetalleApartado.Rows(I).Item(3), 2)
                    decExonerado += Math.Round((dtbDetalleApartado.Rows(I).Item(4) - decGravadoPorcentual) * dtbDetalleApartado.Rows(I).Item(3), 2)
                    decImpuestoProducto = decGravadoPorcentual * decTasaImpuesto / 100
                Else
                    decGravado += Math.Round(dtbDetalleApartado.Rows(I).Item(3) * dtbDetalleApartado.Rows(I).Item(4), 2)
                End If
                decImpuesto += Math.Round(decImpuestoProducto * dtbDetalleApartado.Rows(I).Item(3), 2)
            Else
                decExcento += Math.Round(dtbDetalleApartado.Rows(I).Item(4) * dtbDetalleApartado.Rows(I).Item(3), 2)
            End If
            decDescuento += dtbDetalleApartado.Rows(I).Item(10) * dtbDetalleApartado.Rows(I).Item(3)
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
        decSaldoPorPagar = decTotal - decTotalPago
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
        decSaldoPorPagar = decTotal - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = FrmPrincipal.ObtenerListadoFormaPagoCliente()
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = FrmPrincipal.ObtenerListadoTipoMoneda()
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
    Private Sub FrmApartado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmApartado_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
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

    Private Async Sub FrmApartado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto Then CargarAutoCompletarProducto()
            grdDetalleApartado.DataSource = dtbDetalleApartado
            grdDesglosePago.DataSource = dtbDesglosePago
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
            decSaldoPorPagar = 0
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
            If FrmPrincipal.bolModificaDescripcion Then txtDescripcion.ReadOnly = False
            If FrmPrincipal.bolModificaCliente Then txtPorcDesc.ReadOnly = False
            txtCodigo.Focus()
            CargarCombos()
            Await CargarListaBancoAdquiriente()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
            bolReady = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdApartado.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        cboTipoMoneda.Enabled = True
        txtTelefono.Text = ""
        txtDocumento.Text = ""
        txtTipoExoneracion.Text = ""
        txtNumDocExoneracion.Text = ""
        txtNombreInstExoneracion.Text = ""
        txtFechaExoneracion.Text = ""
        txtPorcentajeExoneracion.Text = ""
        dtbDetalleApartado.Rows.Clear()
        grdDetalleApartado.Refresh()
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
        txtTipoCambio.Text = "1"
        txtMontoPago.Text = ""
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdApartado.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularApartado(apartado.IdApartado, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaApartado()
        formBusqueda.bolIncluyeEstado = True
        formBusqueda.bolIncluyeNulos = True
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
                bolReady = False
                txtIdApartado.Text = apartado.ConsecApartado
                cliente = apartado.Cliente
                txtNombreCliente.Text = apartado.NombreCliente
                txtFecha.Text = apartado.Fecha
                cboTipoMoneda.SelectedValue = apartado.IdTipoMoneda
                txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
                txtTelefono.Text = apartado.Telefono
                txtDocumento.Text = apartado.TextoAdicional
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = FrmPrincipal.ObtenerDescripcionTipoExoneracion(cliente.IdTipoExoneracion)
                    txtNumDocExoneracion.Text = cliente.NumDocExoneracion
                    txtNombreInstExoneracion.Text = cliente.NombreInstExoneracion
                    txtFechaExoneracion.Text = cliente.FechaEmisionDoc
                    txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                End If
                vendedor = apartado.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleApartado(apartado)
                CargarDesglosePago(apartado)
                CargarTotales()
                CargarTotalesPago()
                decPagoCliente = apartado.MontoPagado
                cboTipoMoneda.Enabled = False
                txtNombreCliente.ReadOnly = True
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnBusProd.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnImprimir.Enabled = Not apartado.Nulo
                btnGenerarPDF.Enabled = Not apartado.Nulo
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnAnular.Enabled = Not apartado.Nulo And Not apartado.Aplicado And FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
                bolReady = True
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
                txtTelefono.Text = cliente.Telefono
                txtNombreCliente.ReadOnly = True
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
            .bolIncluyeServicios = False,
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
            MessageBox.Show("El apartado no puede ser cancelado en su totalidad.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago de la factura es superior al saldo por pagar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdApartado.Text = "" Then
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
            apartado = New Apartado With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCliente = cliente.IdCliente,
                .NombreCliente = txtNombreCliente.Text,
                .Fecha = Now(),
                .Telefono = txtTelefono.Text,
                .TextoAdicional = txtDocumento.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = decDescuento,
                .Impuesto = decImpuesto,
                .MontoAdelanto = decTotalPago,
                .MontoPagado = decPagoCliente,
                .Nulo = False
            }
            apartado.DetalleApartado = New List(Of DetalleApartado)
            For I As Short = 0 To dtbDetalleApartado.Rows.Count - 1
                detalleApartado = New DetalleApartado With {
                    .IdProducto = dtbDetalleApartado.Rows(I).Item(0),
                    .Descripcion = dtbDetalleApartado.Rows(I).Item(2),
                    .Cantidad = dtbDetalleApartado.Rows(I).Item(3),
                    .PrecioVenta = dtbDetalleApartado.Rows(I).Item(4),
                    .Excento = dtbDetalleApartado.Rows(I).Item(7),
                    .PorcentajeIVA = dtbDetalleApartado.Rows(I).Item(8),
                    .PorcDescuento = dtbDetalleApartado.Rows(I).Item(9)
                }
                apartado.DetalleApartado.Add(detalleApartado)
            Next
            apartado.DesglosePagoApartado = New List(Of DesglosePagoApartado)
            For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoApartado With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                    .TipoTarjeta = dtbDesglosePago.Rows(I).Item(4),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(5),
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(6),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(7),
                    .TipoDeCambio = dtbDesglosePago.Rows(I).Item(8)
                }
                apartado.DesglosePagoApartado.Add(desglosePago)
            Next
            Try
                Dim strIdConsec As String = Await Puntoventa.AgregarApartado(apartado, FrmPrincipal.usuarioGlobal.Token)
                Dim arrIdConsec = strIdConsec.Split("-")
                apartado.IdApartado = arrIdConsec(0)
                apartado.ConsecApartado = arrIdConsec(1)
                txtIdApartado.Text = apartado.ConsecApartado
            Catch ex As Exception
                txtIdApartado.Text = ""
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
        End If
        btnImprimir.Enabled = True
        btnGenerarPDF.Enabled = True
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        btnBusProd.Enabled = False
        cboTipoMoneda.Enabled = False
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Async Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdApartado.Text <> "" Then
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = apartado.ConsecApartado,
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strTelefono = apartado.Telefono,
                    .strDetalle = apartado.TextoAdicional,
                    .strFecha = apartado.Fecha.ToString("dd/MM/yyyy hh:mm:ss"),
                    .strSubTotal = txtSubTotal.Text,
                    .strDescuento = txtDescuento.Text,
                    .strImpuesto = txtImpuesto.Text,
                    .strTotal = txtTotal.Text,
                    .strAdelanto = FormatNumber(apartado.MontoAdelanto, 2),
                    .strSaldo = FormatNumber(apartado.Total - apartado.MontoAdelanto, 2),
                    .strPagoCon = FormatNumber(decPagoCliente, 2),
                    .strCambio = FormatNumber(decPagoCliente - decPagoEfectivo, 2)
                }
                arrDetalleOrden = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For I As Short = 0 To dtbDetalleApartado.Rows.Count - 1
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleApartado.Rows(I).Item(1) + "-" + dtbDetalleApartado.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleApartado.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleApartado.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleApartado.Rows(I).Item(3)) * CDbl(dtbDetalleApartado.Rows(I).Item(4)), 2),
                    .strExcento = IIf(dtbDetalleApartado.Rows(I).Item(7) = 0, "G", "E")
                }
                    arrDetalleOrden.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleOrden
                arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
                For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(1), FormatNumber(dtbDesglosePago.Rows(I).Item(7), 2))
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                comprobanteImpresion.arrDesglosePago = arrDesglosePago
                ModuloImpresion.ImprimirApartado(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdApartado.Text <> "" Then
            Try
                Dim pdfBytes As Byte() = Await Puntoventa.ObtenerApartadoPDF(apartado.IdApartado, FrmPrincipal.usuarioGlobal.Token)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\APARTADO-" + apartado.ConsecApartado.ToString() + ".pdf"
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
            CargarLineaDetalleApartado(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text)
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
        If grdDetalleApartado.Rows.Count > 0 Then
            Dim intId = grdDetalleApartado.CurrentRow.Cells(10).Value
            dtbDetalleApartado.Rows.RemoveAt(dtbDetalleApartado.Rows.IndexOf(dtbDetalleApartado.Rows.Find(intId)))
            grdDetalleApartado.Refresh()
            If dtbDetalleApartado.Rows.Count = 0 Then consecDetalle = 0
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

    Private Sub CboTipoMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoMoneda.SelectedValueChanged
        If bolReady And cboTipoMoneda.SelectedValue IsNot Nothing Then
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
            If txtPrecio.Text <> "" And e.KeyCode <> Keys.Tab And e.KeyCode <> Keys.Enter And e.KeyCode <> Keys.ShiftKey Then
                decPrecioVenta = Math.Round(CDbl(txtPrecio.Text), 2)
                txtPorcDesc.Text = "0"
            End If
        End If
    End Sub

    Private Sub grdDetalleApartado_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdDetalleApartado.EditingControlShowing
        If grdDetalleApartado.CurrentCell.ColumnIndex = 4 Then
            Dim tb As TextBox = e.Control
            If tb IsNot Nothing Then
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            End If
        End If
    End Sub

    Private Sub TextBox_keyPress(sender As Object, e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, True, 2)
    End Sub

    Private Sub grdDetalleApartado_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleApartado.CellValueChanged
        If e.ColumnIndex = 4 And Not bolAutorizando Then
            bolAutorizando = True
            Dim bolPrecioAutorizado As Boolean = False
            Dim decPorcDesc As Decimal = 0
            Dim decPrecioTotal As Decimal = dtbDetalleApartado.Rows(e.RowIndex).Item(5) + dtbDetalleApartado.Rows(e.RowIndex).Item(10)
            If Not IsDBNull(grdDetalleApartado.Rows(e.RowIndex).Cells(4).Value) Then
                decPorcDesc = grdDetalleApartado.Rows(e.RowIndex).Cells(4).Value
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
            grdDetalleApartado.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = decPorcDesc
            Dim decCantidad As Decimal = grdDetalleApartado.Rows(e.RowIndex).Cells(3).Value
            Dim decTasaImpuesto As Decimal = grdDetalleApartado.Rows(e.RowIndex).Cells(9).Value
            Dim decPrecioConDescuento As Decimal = decPrecioTotal - (decPrecioTotal * decPorcDesc / 100)
            If decPorcDesc > 0 And Not bolPrecioAutorizado And FrmPrincipal.empresaGlobal.MontoRedondeoDescuento > 0 Then
                decPrecioConDescuento = Puntoventa.ObtenerPrecioRedondeado(FrmPrincipal.empresaGlobal.MontoRedondeoDescuento, decPrecioConDescuento)
                decPorcDesc = (decPrecioTotal - decPrecioConDescuento) / decPrecioTotal * 100
            End If
            Dim decMontoDesc = decPrecioTotal - decPrecioConDescuento
            Dim decPrecioGravado As Decimal = decPrecioConDescuento
            If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecioConDescuento / (1 + (decTasaImpuesto / 100)), 5)
            dtbDetalleApartado.Rows(e.RowIndex).Item(4) = decPrecioGravado
            dtbDetalleApartado.Rows(e.RowIndex).Item(5) = decPrecioConDescuento
            dtbDetalleApartado.Rows(e.RowIndex).Item(6) = decCantidad * decPrecioConDescuento
            dtbDetalleApartado.Rows(e.RowIndex).Item(9) = decPorcDesc
            dtbDetalleApartado.Rows(e.RowIndex).Item(10) = decMontoDesc
            grdDetalleApartado.Refresh()
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
                        End If
                    Else
                        txtPorcDesc.Text = 0
                    End If
                End If
                txtPorcDesc.Text = decPorcDesc
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
                        If producto.Activo And producto.Tipo = StaticTipoProducto.Producto Then
                            CargarDatosProducto(producto)
                            txtCantidad.Focus()
                        Else
                            MessageBox.Show("El código ingresado no pertenece a un producto o se encuentra inactivo", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) Handles txtPrecio.MouseDown, txtDescripcion.MouseDown, txtCodigo.MouseDown, txtCantidad.MouseDown
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
        If e.KeyCode = Keys.Enter And txtIdApartado.Text = "" And txtMontoPago.Text <> "" Then
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

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtPrecio.KeyPress, txtPorcDesc.KeyPress, txtMontoPago.KeyPress, txtCantidad.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class