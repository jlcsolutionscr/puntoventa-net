Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports System.IO
Imports System.Globalization
Imports LeandroSoftware.Core.Utilitario

Public Class FrmOrdenServicio
#Region "Variables"
    Private strMotivoRechazo As String
    Private decSubTotal, decExcento, decGravado, decImpuesto, decTotal, decTotalPago, decSaldoPorPagar, decPrecioVenta As Decimal
    Private I, shtConsecutivoPago As Short
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
        dtbDetalleOrdenServicio.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("PORCDESCUENTO", GetType(Decimal))
        dtbDetalleOrdenServicio.PrimaryKey = {dtbDetalleOrdenServicio.Columns(0)}

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDCONSECUTIVO", GetType(Integer))
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDCUENTABANCO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("TIPOTARJETA", GetType(String))
        dtbDesglosePago.Columns.Add("NROMOVIMIENTO", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCTIPOMONEDA", GetType(String))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("MONTOFORANEO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleOrdenServicio.Columns.Clear()
        grdDetalleOrdenServicio.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPorcDescuento As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Width = 0
        dvcIdProducto.Visible = False
        grdDetalleOrdenServicio.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 205
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 280
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
        dvcPorcDescuento.ReadOnly = True
        dvcPorcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcPorcDescuento)

        dvcPrecio.DataPropertyName = "PRECIO"
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
        Dim dvcDescTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcMontoForaneo As New DataGridViewTextBoxColumn

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
        dvcDescBanco.Width = 150
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
        dvcMontoLocal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcMontoForaneo.DataPropertyName = "MONTOFORANEO"
        dvcMontoForaneo.HeaderText = "Monto Exterior"
        dvcMontoForaneo.Width = 110
        dvcMontoForaneo.Visible = True
        dvcMontoForaneo.ReadOnly = True
        dvcMontoForaneo.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoForaneo)
    End Sub

    Private Sub CargarDetalleOrdenServicio(ByVal ordenServicio As OrdenServicio)
        dtbDetalleOrdenServicio.Rows.Clear()
        For Each detalle As DetalleOrdenServicio In ordenServicio.DetalleOrdenServicio
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = detalle.IdProducto
            dtrRowDetOrdenServicio.Item(1) = detalle.Producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = detalle.Descripcion
            dtrRowDetOrdenServicio.Item(3) = detalle.Cantidad
            dtrRowDetOrdenServicio.Item(4) = detalle.PrecioVenta
            dtrRowDetOrdenServicio.Item(5) = dtrRowDetOrdenServicio.Item(3) * dtrRowDetOrdenServicio.Item(4)
            dtrRowDetOrdenServicio.Item(6) = detalle.Excento
            dtrRowDetOrdenServicio.Item(7) = detalle.PorcentajeIVA
            dtrRowDetOrdenServicio.Item(8) = detalle.PorcDescuento
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        Next
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Async Function CargarDesglosePago(ordenServicio As OrdenServicio) As Task
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoOrdenServicio In ordenServicio.DesglosePagoOrdenServicio
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdConsecutivo
            dtrRowDesglosePago.Item(1) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(2) = detalle.FormaPago.Descripcion
            dtrRowDesglosePago.Item(3) = detalle.IdCuentaBanco
            If detalle.IdFormaPago = StaticFormaPago.Tarjeta Then
                Dim banco As BancoAdquiriente = Await Puntoventa.ObtenerBancoAdquiriente(detalle.IdCuentaBanco, FrmPrincipal.usuarioGlobal.Token)
                dtrRowDesglosePago.Item(4) = banco.Descripcion
            ElseIf detalle.IdFormaPago = StaticFormaPago.Cheque Or detalle.IdFormaPago = StaticFormaPago.TransferenciaDepositoBancario Then
                Dim banco As CuentaBanco = Await Puntoventa.ObtenerCuentaBanco(detalle.IdCuentaBanco, FrmPrincipal.usuarioGlobal.Token)
                dtrRowDesglosePago.Item(4) = banco.Descripcion
            End If
            dtrRowDesglosePago.Item(5) = detalle.TipoTarjeta
            dtrRowDesglosePago.Item(6) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(7) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(8) = detalle.TipoMoneda.Descripcion
            dtrRowDesglosePago.Item(9) = detalle.MontoLocal
            dtrRowDesglosePago.Item(10) = detalle.MontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
    End Function

    Private Sub CargarLineaDetalleOrdenServicio(producto As Producto, strDescripcion As String, decCantidad As Decimal, decPrecio As Decimal, decPorcDesc As Decimal)
        Dim intIndice As Integer = dtbDetalleOrdenServicio.Rows.IndexOf(dtbDetalleOrdenServicio.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        Dim decPrecioGravado As Decimal = decPrecio
        If decTasaImpuesto > 0 Then
            decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 3, MidpointRounding.AwayFromZero)
            If cliente.AplicaTasaDiferenciada Then decTasaImpuesto = cliente.ParametroImpuesto.TasaImpuesto
        End If
        If intIndice >= 0 Then
            dtbDetalleOrdenServicio.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenServicio.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(3) += decCantidad
            dtbDetalleOrdenServicio.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleOrdenServicio.Rows(intIndice).Item(5) = decCantidad * decPrecioGravado
            dtbDetalleOrdenServicio.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleOrdenServicio.Rows(intIndice).Item(7) = decTasaImpuesto
            dtbDetalleOrdenServicio.Rows(intIndice).Item(8) = decPorcDesc
        Else
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = producto.IdProducto
            dtrRowDetOrdenServicio.Item(1) = producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = strDescripcion
            dtrRowDetOrdenServicio.Item(3) = decCantidad
            dtrRowDetOrdenServicio.Item(4) = decPrecioGravado
            dtrRowDetOrdenServicio.Item(5) = decCantidad * decPrecioGravado
            dtrRowDetOrdenServicio.Item(6) = decTasaImpuesto = 0
            dtrRowDetOrdenServicio.Item(7) = decTasaImpuesto
            dtrRowDetOrdenServicio.Item(8) = decPorcDesc
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        End If
        grdDetalleOrdenServicio.Refresh()
        CargarTotales()
    End Sub

    Private Sub CargarLineaDesglosePago()
        Dim dblMontoLocal, dblMontoForaneo As Decimal
        dblMontoForaneo = CDbl(txtMontoPago.Text)
        dblMontoLocal = CDbl(txtMontoPago.Text)
        dtrRowDesglosePago = dtbDesglosePago.NewRow
        shtConsecutivoPago += 1
        dtrRowDesglosePago.Item(0) = shtConsecutivoPago
        dtrRowDesglosePago.Item(1) = cboFormaPago.SelectedValue
        dtrRowDesglosePago.Item(2) = cboFormaPago.Text
        dtrRowDesglosePago.Item(3) = cboTipoBanco.SelectedValue
        dtrRowDesglosePago.Item(4) = cboTipoBanco.Text
        dtrRowDesglosePago.Item(5) = txtTipoTarjeta.Text
        dtrRowDesglosePago.Item(6) = txtAutorizacion.Text
        dtrRowDesglosePago.Item(7) = cboTipoMoneda.SelectedValue
        dtrRowDesglosePago.Item(8) = cboTipoMoneda.Text
        dtrRowDesglosePago.Item(9) = dblMontoLocal
        dtrRowDesglosePago.Item(10) = dblMontoForaneo
        dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotales()
        decSubTotal = 0
        decGravado = 0
        decExcento = 0
        decImpuesto = 0
        For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(7)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(4) * decTasaImpuesto / 100
                decGravado += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
                decImpuesto += Math.Round(decImpuestoProducto, 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            Else
                decExcento += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            End If
        Next
        decSubTotal = decGravado + decExcento
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2, MidpointRounding.AwayFromZero)
        decSaldoPorPagar = decTotal - decTotalPago
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarTotalesPago()
        decTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            decTotalPago = decTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(10))
        Next
        decSaldoPorPagar = decTotal - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = Await Puntoventa.ObtenerListadoFormaPagoFactura(FrmPrincipal.usuarioGlobal.Token)
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = Await Puntoventa.ObtenerListadoTipoMoneda(FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Async Function CargarListaBancoAdquiriente() As Task
        Dim lista As IList = Await Puntoventa.ObtenerListadoBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        If lista.Count() = 0 Then
            Throw New Exception("Debe parametrizar la lista de bancos adquirientes para pagos con tarjeta.")
        Else
            cboTipoBanco.DataSource = lista
            cboTipoBanco.ValueMember = "Id"
            cboTipoBanco.DisplayMember = "Descripcion"
        End If
    End Function

    Private Async Function CargarListaCuentaBanco() As Task
        Dim lista As IList = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
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
        Return decPrecioVenta
    End Function

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecio.Text = FormatNumber(0, 2)
            txtUnidad.Text = ""
            txtCodigo.Focus()
            Exit Sub
        Else
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            txtCodigo.Text = producto.Codigo
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtDescripcion.Text = producto.Descripcion
            decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
            txtPrecio.Text = FormatNumber(decPrecioVenta / (1 + (decTasaImpuesto / 100)), 2)
            txtUnidad.Text = IIf(producto.Tipo = 1, "UND", IIf(producto.Tipo = 2, "SP", "OS"))
        End If
    End Sub

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, 1, 0, True, FrmPrincipal.usuarioGlobal.Token)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            Await CargarListaBancoAdquiriente()
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then Await CargarAutoCompletarProducto()
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            grdDetalleOrdenServicio.DataSource = dtbDetalleOrdenServicio
            grdDesglosePago.DataSource = dtbDesglosePago
            bolInit = False
            txtCantidad.Text = "1"
            txtPorcDesc.Text = "0"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            Try
                cliente = New Cliente With {
                    .IdCliente = 1,
                    .Nombre = "CLIENTE DE CONTADO"
                }
                txtNombreCliente.Text = cliente.Nombre
            Catch ex As Exception
                Throw New Exception("Error al consultar el cliente de contado. Por favor consulte con su proveedor.")
            End Try
            Try
                vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                Throw New Exception("Debe ingresar al menos un vendedor para generar la facturación. . .")
            End Try
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
            shtConsecutivoPago = 0
            If FrmPrincipal.bolModificaDescripcion Then txtDescripcion.ReadOnly = False
            If FrmPrincipal.bolAplicaDescuento Then
                txtPorcDesc.ReadOnly = False
                txtPorcDesc.TabStop = True
            End If
            If FrmPrincipal.bolModificaPrecioVenta Then
                txtPrecio.ReadOnly = False
                txtPrecio.TabStop = True
            End If
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdOrdenServicio.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtTelefono.Text = ""
        txtDescripcionOrden.Text = ""
        txtFechaEntrega.Text = ""
        txtOtrosDetalles.Text = ""
        dtbDetalleOrdenServicio.Rows.Clear()
        grdDetalleOrdenServicio.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPorcDesc.Text = "0"
        txtPrecio.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        decTotal = 0
        decTotalPago = 0
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        btnAnular.Enabled = False
        btnImprimir.Enabled = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        Try
            cliente = New Cliente With {
                .IdCliente = 1,
                .Nombre = "CLIENTE DE CONTADO"
            }
            txtNombreCliente.Text = cliente.Nombre
            txtNombreCliente.ReadOnly = False
        Catch ex As Exception
            MessageBox.Show("Error al consultar el cliente de contado. Por favor consulte con su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
            txtVendedor.Text = vendedor.Nombre
        Catch ex As Exception
            MessageBox.Show("Debe ingresar al menos un vendedor para generar la facturación. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        bolInit = False
        txtMontoPago.Text = ""
        shtConsecutivoPago = 0
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdOrdenServicio.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularOrdenServicio(txtIdOrdenServicio.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ordenServicio = Await Puntoventa.ObtenerOrdenServicio(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenServicio IsNot Nothing Then
                bolInit = True
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
                cliente = ordenServicio.Cliente
                txtNombreCliente.Text = ordenServicio.NombreCliente
                txtFecha.Text = ordenServicio.Fecha
                txtTelefono.Text = ordenServicio.Telefono
                txtDescripcionOrden.Text = ordenServicio.Descripcion
                txtFechaEntrega.Text = ordenServicio.FechaEntrega
                txtOtrosDetalles.Text = ordenServicio.OtrosDetalles
                vendedor = ordenServicio.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleOrdenServicio(ordenServicio)
                Await CargarDesglosePago(ordenServicio)
                CargarTotales()
                CargarTotalesPago()
                txtNombreCliente.ReadOnly = True
                btnImprimir.Enabled = True
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                bolInit = False
            Else
                MessageBox.Show("No existe registro de OrdenServicio asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If vendedor Is Nothing Then
                MessageBox.Show("El vendedor seleccionado no existe. Consulte a su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                CargarTotales()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If cliente Is Nothing Then
                MessageBox.Show("El cliente de contado no se encuentra registrado en el sistema. Consulte a su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                producto = Await Puntoventa.ObtenerProducto(intIdProducto, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show("Error al obtener la información del producto seleccionado. Intente mas tarde.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If decSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago de la factura es superior al saldo por pagar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        If txtIdOrdenServicio.Text = "" Then
            ordenServicio = New OrdenServicio With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario
            }
            ordenServicio.IdCliente = cliente.IdCliente
            ordenServicio.NombreCliente = txtNombreCliente.Text
            ordenServicio.Fecha = FrmPrincipal.ObtenerFechaFormateada(Now())
            ordenServicio.IdVendedor = vendedor.IdVendedor
            ordenServicio.Telefono = txtTelefono.Text
            ordenServicio.Descripcion = txtDescripcionOrden.Text
            ordenServicio.FechaEntrega = txtFechaEntrega.Text
            ordenServicio.OtrosDetalles = txtOtrosDetalles.Text
            ordenServicio.Excento = decExcento
            ordenServicio.Gravado = decGravado
            ordenServicio.Descuento = 0
            ordenServicio.Impuesto = decImpuesto
            ordenServicio.MontoAdelanto = decTotalPago
            If txtIdOrdenServicio.Text <> "" Then
                ordenServicio.DetalleOrdenServicio.Clear()
            End If
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                detalleOrdenServicio = New DetalleOrdenServicio
                If txtIdOrdenServicio.Text <> "" Then
                    detalleOrdenServicio.IdOrden = ordenServicio.IdOrden
                End If
                detalleOrdenServicio.IdProducto = dtbDetalleOrdenServicio.Rows(I).Item(0)
                detalleOrdenServicio.Descripcion = dtbDetalleOrdenServicio.Rows(I).Item(2)
                detalleOrdenServicio.Cantidad = dtbDetalleOrdenServicio.Rows(I).Item(3)
                detalleOrdenServicio.PrecioVenta = dtbDetalleOrdenServicio.Rows(I).Item(4)
                detalleOrdenServicio.Excento = dtbDetalleOrdenServicio.Rows(I).Item(6)
                detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(7)
                detalleOrdenServicio.PorcDescuento = dtbDetalleOrdenServicio.Rows(I).Item(8)
                ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
            Next
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoOrdenServicio With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(1),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(3),
                    .TipoTarjeta = dtbDesglosePago.Rows(I).Item(5),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(6),
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(7),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(9),
                    .MontoForaneo = dtbDesglosePago.Rows(I).Item(10)
                }
                ordenServicio.DesglosePagoOrdenServicio.Add(desglosePago)
            Next
            Try
                txtIdOrdenServicio.Text = Await Puntoventa.AgregarOrdenServicio(ordenServicio, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdOrdenServicio.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            ordenServicio.Telefono = txtTelefono.Text
            ordenServicio.Descripcion = txtDescripcionOrden.Text
            ordenServicio.FechaEntrega = txtFechaEntrega.Text
            ordenServicio.OtrosDetalles = txtOtrosDetalles.Text
            ordenServicio.Excento = decExcento
            ordenServicio.Gravado = decGravado
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
                detalleOrdenServicio.Excento = dtbDetalleOrdenServicio.Rows(I).Item(6)
                detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(7)
                detalleOrdenServicio.PorcDescuento = dtbDetalleOrdenServicio.Rows(I).Item(8)
                ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
            Next
            Try
                Await Puntoventa.ActualizarOrdenServicio(ordenServicio, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGuardar.Enabled = True
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdOrdenServicio.Text <> "" Then
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = txtIdOrdenServicio.Text,
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strDocumento = FrmPrincipal.empresaGlobal.LeyendaOrdenServicio,
                    .strFecha = txtFecha.Text,
                    .strSubTotal = txtSubTotal.Text,
                    .strDescuento = "0.00",
                    .strImpuesto = txtImpuesto.Text,
                    .strTotal = txtTotal.Text
                }
                arrDetalleOrden = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleOrdenServicio.Rows(I).Item(1) + "-" + dtbDetalleOrdenServicio.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleOrdenServicio.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleOrdenServicio.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleOrdenServicio.Rows(I).Item(3)) * CDbl(dtbDetalleOrdenServicio.Rows(I).Item(4)), 2),
                    .strExcento = IIf(dtbDetalleOrdenServicio.Rows(I).Item(6) = 0, "G", "E")
                }
                    arrDetalleOrden.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleOrden
                arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
                For I = 0 To dtbDesglosePago.Rows.Count - 1
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago With {
                    .strDescripcion = dtbDesglosePago.Rows(I).Item(2),
                    .strMonto = FormatNumber(dtbDesglosePago.Rows(I).Item(10)),
                    .strNroDoc = dtbDesglosePago.Rows(I).Item(5)
                }
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                comprobanteImpresion.arrDesglosePago = arrDesglosePago
                ModuloImpresion.ImprimirOrdenServicio(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdOrdenServicio.Text <> "" Then
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
            datos.TelefonoEmisor = FrmPrincipal.empresaGlobal.Telefono
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
                Dim barrio As Barrio = cliente.Barrio
                datos.ProvinciaReceptor = cliente.Barrio.Distrito.Canton.Provincia.Descripcion
                datos.CantonReceptor = cliente.Barrio.Distrito.Canton.Descripcion
                datos.DistritoReceptor = cliente.Barrio.Distrito.Descripcion
                datos.BarrioReceptor = cliente.Barrio.Descripcion
                datos.DireccionReceptor = cliente.Direccion
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
                Dim pdfBytes As Byte() = UtilitarioPDF.GenerarPDFFacturaElectronica(datos)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\ORDENSERVICIO-" + txtIdOrdenServicio.Text + ".pdf"
                File.WriteAllBytes(pdfFilePath, pdfBytes)
                Process.Start(pdfFilePath)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto Is Nothing Then
            If txtCodigo.Text <> "" Then
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                    CargarLineaDetalleOrdenServicio(producto, producto.Descripcion, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text)
                    txtCodigo.Text = ""
                    producto = Nothing
                    txtCodigo.Focus()
                End If
            End If
        Else
            Dim strError As String = ""
            If txtDescripcion.Text = "" Then strError = "La descripción no puede estar en blanco"
            If decPrecioVenta <= 0 Then strError = "El precio del producto no puede ser igual o menor a 0"
            If strError <> "" Then
                MessageBox.Show(strError, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtUnidad.Text = ""
            txtPorcDesc.Text = "0"
            txtPrecio.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleOrdenServicio.Rows.Count > 0 Then
            dtbDetalleOrdenServicio.Rows.Remove(dtbDetalleOrdenServicio.Rows.Find(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value))
            grdDetalleOrdenServicio.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub CboFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoTarjeta.Text = ""
            txtAutorizacion.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario Then
                Try
                    Await CargarListaBancoAdquiriente()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                cboTipoBanco.SelectedIndex = 0
                cboTipoBanco.Width = 123
                lblBanco.Width = 123
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
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                cboTipoBanco.SelectedIndex = 0
                cboTipoBanco.Width = 193
                lblBanco.Width = 193
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
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        End If
    End Sub

    Private Sub BtnInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboTipoBanco.SelectedValue > 0 And decTotal > 0 And txtMontoPago.Text <> "" Then
            If decSaldoPorPagar = 0 Then
                MessageBox.Show("El monto de por cancelar ya se encuentra cubierto. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If cboFormaPago.SelectedValue = StaticFormaPago.Tarjeta Then
                If txtTipoTarjeta.Text = "" Or txtAutorizacion.Text = "" Then
                    MessageBox.Show("Debe ingresar el banco y autorización del movimiento con tarjeta", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            ElseIf cboFormaPago.SelectedValue = StaticFormaPago.Cheque Or cboFormaPago.SelectedValue = StaticFormaPago.TransferenciaDepositoBancario Then
                If txtAutorizacion.Text = "" Then
                    MessageBox.Show("Debe ingresar el número de documento correspondiente al movimiento.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            CargarLineaDesglosePago()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Sub BtnEliminarPago_Click(sender As Object, e As EventArgs) Handles btnEliminarPago.Click
        Dim objPkDesglose(0) As Object
        If dtbDesglosePago.Rows.Count > 0 Then
            objPkDesglose(0) = grdDesglosePago.CurrentRow.Cells(0).Value
            dtbDesglosePago.Rows.Remove(dtbDesglosePago.Rows.Find(objPkDesglose))
            grdDesglosePago.Refresh()
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Sub txtPorcDesc_Validated(sender As Object, e As EventArgs) Handles txtPorcDesc.Validated
        If txtPorcDesc.Text = "" Then txtPorcDesc.Text = "0"
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto) / (1 + (decTasaImpuesto / 100))
            If CDbl(txtPorcDesc.Text) > FrmPrincipal.empresaGlobal.PorcentajeDescMaximo Then
                MessageBox.Show("El porcentaje ingresado es mayor al parametro establecido para la empresa", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtPorcDesc.Text = "0"
                txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
            Else
                Dim decPorcDesc As Decimal = CDbl(txtPorcDesc.Text) / 100
                txtPrecio.Text = FormatNumber(decPrecioVenta - (decPrecioVenta * decPorcDesc), 2)
                If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
            End If
        End If
    End Sub

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio.KeyUp
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    CargarDatosProducto(producto)
                    txtCantidad.Focus()
                Else
                    txtCodigo.Text = ""
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub TxtPrecio_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPrecio.PreviewKeyDown
        If producto IsNot Nothing Then
            If e.KeyCode = Keys.ControlKey Then
                FrmPrincipal.strCodigoUsuario = ""
                FrmPrincipal.strContrasena = ""
                FrmPrincipal.strBusqueda = ""
                Dim formAutorizacion As New FrmAutorizaPrecio
                formAutorizacion.ShowDialog()
                If FrmPrincipal.strCodigoUsuario <> "" And FrmPrincipal.strContrasena <> "" And FrmPrincipal.strBusqueda <> "" Then
                    Dim autorizado As Boolean
                    Try
                        autorizado = Await Puntoventa.AutorizacionPrecioExtraordinario(FrmPrincipal.strCodigoUsuario, FrmPrincipal.strContrasena, FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    If autorizado Then
                        txtPrecio.Text = FormatNumber(FrmPrincipal.strBusqueda)
                        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
                        decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
                    Else
                        MessageBox.Show("Los credenciales ingresados no tienen permisos para modificar el precio de venta.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    End If
                End If
            ElseIf e.KeyCode = Keys.Enter Then
                BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub TxtCantidad_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCantidad.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If CDbl(txtPrecio.Text) > 0 Then
                BtnInsertar_Click(btnInsertar, New EventArgs())
            Else
                txtPrecio.Focus()
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
        Else
            txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
        End If
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPorcDesc.KeyPress, txtPrecio.KeyPress, txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class