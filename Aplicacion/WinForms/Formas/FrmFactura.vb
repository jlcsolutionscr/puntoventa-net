Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Xml.Serialization
Imports LeandroSoftware.AccesoDatos.ClienteWCF
Imports LeandroSoftware.Puntoventa.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.TiposDatos
Imports System.Threading.Tasks
Imports LeandroSoftware.Puntoventa.Utilitario

Public Class FrmFactura
#Region "Variables"
    Private strMotivoRechazo As String
    Private decExcento, decGrabado, decImpuesto, decTotalCosto, decTotalPago, decTotal, decSubTotal, decSaldoPorPagar, decCostoPorInstalacion, decPrecioVenta As Decimal
    Private I, shtConsecutivoPago As Short
    Private dtbDetalleFactura, dtbDesglosePago As DataTable
    Private dtrRowDetFactura, dtrRowDesglosePago As DataRow
    Private arrDetalleFactura As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
    Private factura As Factura
    Private detalleFactura As DetalleFactura
    Private ordenServicio As OrdenServicio
    Private proforma As Proforma
    Private desglosePago As DesglosePagoFactura
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleFactura = New DataTable()
        dtbDetalleFactura.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleFactura.Columns.Add("CODIGO", GetType(String))
        dtbDetalleFactura.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleFactura.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleFactura.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("COSTOINSTALACION", GetType(Decimal))
        dtbDetalleFactura.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleFactura.PrimaryKey = {dtbDetalleFactura.Columns(0)}

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
        grdDetalleFactura.Columns.Clear()
        grdDetalleFactura.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Width = 0
        dvcIdProducto.Visible = False
        grdDetalleFactura.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 225
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleFactura.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDetalleFactura.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcCantidad)

        dvcPrecio.DataPropertyName = "PRECIO"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.Visible = True
        dvcPrecio.ReadOnly = True
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.Visible = True
        dvcExc.ReadOnly = True
        grdDetalleFactura.Columns.Add(dvcExc)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "PrecioCosto"
        dvcPrecioCosto.Width = 0
        dvcPrecioCosto.Visible = False
        grdDetalleFactura.Columns.Add(dvcPrecioCosto)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        grdDetalleFactura.Columns.Add(dvcPorcentajeIVA)

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

    Private Sub CargarDetalleFactura(ByVal factura As Factura)
        dtbDetalleFactura.Rows.Clear()
        For Each detalle As DetalleFactura In factura.DetalleFactura
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = detalle.Excento
            dtrRowDetFactura.Item(7) = detalle.PrecioCosto
            dtrRowDetFactura.Item(8) = detalle.CostoInstalacion
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
            decCostoPorInstalacion += detalle.Cantidad * detalle.CostoInstalacion
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleOrdenServicio(ByVal ordenServicio As OrdenServicio)
        dtbDetalleFactura.Rows.Clear()
        For Each detalle As DetalleOrdenServicio In ordenServicio.DetalleOrdenServicio
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = detalle.Excento
            dtrRowDetFactura.Item(7) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(8) = detalle.CostoInstalacion
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
            decCostoPorInstalacion += detalle.Cantidad * detalle.CostoInstalacion
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleProforma(ByVal proforma As Proforma)
        dtbDetalleFactura.Rows.Clear()
        For Each detalle As DetalleProforma In proforma.DetalleProforma
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Producto.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = detalle.PrecioVenta
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = detalle.Excento
            dtrRowDetFactura.Item(7) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(8) = 0
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Async Function CargarDesglosePago(ByVal factura As Factura) As Task
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoFactura In factura.DesglosePagoFactura
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdConsecutivo
            dtrRowDesglosePago.Item(1) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(2) = detalle.FormaPago.Descripcion
            dtrRowDesglosePago.Item(3) = detalle.IdCuentaBanco
            If detalle.IdFormaPago = StaticFormaPago.Tarjeta Then
                Dim banco As BancoAdquiriente = Await PuntoventaWCF.ObtenerBancoAdquiriente(detalle.IdCuentaBanco)
                dtrRowDesglosePago.Item(4) = banco.Descripcion
            ElseIf detalle.IdFormaPago = StaticFormaPago.Cheque Or detalle.IdFormaPago = StaticFormaPago.TransferenciaDepositoBancario Then
                Dim banco As CuentaBanco = Await PuntoventaWCF.ObtenerCuentaBanco(detalle.IdCuentaBanco)
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

    Private Sub CargarLineaDetalleFactura(ByVal producto As Producto, ByVal strDescripcion As String, ByVal dblCantidad As Double, ByVal dblPrecio As Double, ByVal dblCostoInstalacion As Double)
        Dim intIndice As Integer = dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.ExoneradoDeImpuesto Then decTasaImpuesto = 0
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleFactura.Rows(intIndice).Item(3) += dblCantidad
            dtbDetalleFactura.Rows(intIndice).Item(4) = dblPrecio
            dtbDetalleFactura.Rows(intIndice).Item(5) = dtbDetalleFactura.Rows(intIndice).Item(3) * dtbDetalleFactura.Rows(intIndice).Item(4)
            dtbDetalleFactura.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleFactura.Rows(intIndice).Item(7) = producto.PrecioCosto
            dtbDetalleFactura.Rows(intIndice).Item(8) = dblCostoInstalacion
            dtbDetalleFactura.Rows(intIndice).Item(9) = decTasaImpuesto
        Else
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = producto.IdProducto
            dtrRowDetFactura.Item(1) = producto.Codigo
            dtrRowDetFactura.Item(2) = strDescripcion
            dtrRowDetFactura.Item(3) = dblCantidad
            dtrRowDetFactura.Item(4) = dblPrecio
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = decTasaImpuesto = 0
            dtrRowDetFactura.Item(7) = producto.PrecioCosto
            dtrRowDetFactura.Item(8) = dblCostoInstalacion
            dtrRowDetFactura.Item(9) = decTasaImpuesto
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        End If
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarLineaDetalleInstalacion(ByVal producto As Producto, ByVal decTotal As Decimal)
        Dim intIndice As Integer = dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.ExoneradoDeImpuesto Then decTasaImpuesto = 0
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleFactura.Rows(intIndice).Item(4) += decTotal
            dtbDetalleFactura.Rows(intIndice).Item(5) += decTotal
            dtbDetalleFactura.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleFactura.Rows(intIndice).Item(7) = producto.PrecioCosto
            dtbDetalleFactura.Rows(intIndice).Item(9) = decTasaImpuesto
        Else
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = producto.IdProducto
            dtrRowDetFactura.Item(1) = producto.Codigo
            dtrRowDetFactura.Item(2) = producto.Descripcion
            dtrRowDetFactura.Item(3) = 1
            dtrRowDetFactura.Item(4) = decTotal
            dtrRowDetFactura.Item(5) = decTotal
            dtrRowDetFactura.Item(6) = decTasaImpuesto = 0
            dtrRowDetFactura.Item(7) = producto.PrecioCosto
            dtrRowDetFactura.Item(8) = 0
            dtrRowDetFactura.Item(9) = decTasaImpuesto
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        End If
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub DescargarLineaDetalleInstalacion(ByVal producto As Producto, ByVal decTotal As Decimal)
        Dim intIndice As Integer = dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleFactura.Rows(intIndice).Item(4) -= decTotal
            dtbDetalleFactura.Rows(intIndice).Item(5) -= decTotal
        End If
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarLineaDesglosePago()
        Dim dblMontoLocal, dblMontoForaneo As Decimal
        dblMontoForaneo = CDbl(txtMontoPago.Text)
        dblMontoLocal = txtMontoPago.Text * txtTipoCambio.Text
        If dblMontoLocal > decSaldoPorPagar Then
            dblMontoLocal = decSaldoPorPagar
            dblMontoForaneo = dblMontoLocal / txtTipoCambio.Text
        End If
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
        decGrabado = 0
        decExcento = 0
        decImpuesto = 0
        decTotalCosto = 0
        For I = 0 To dtbDetalleFactura.Rows.Count - 1
            If dtbDetalleFactura.Rows(I).Item(6) = 0 Then
                decGrabado += dtbDetalleFactura.Rows(I).Item(5)
                decImpuesto += dtbDetalleFactura.Rows(I).Item(5) * dtbDetalleFactura.Rows(I).Item(9) / 100
            Else
                decExcento += dtbDetalleFactura.Rows(I).Item(5)
            End If
            decTotalCosto += dtbDetalleFactura.Rows(I).Item(7)
        Next
        decSubTotal = decGrabado + decExcento
        If decSubTotal > 0 And txtDescuento.Text > 0 Then
            decImpuesto = 0
            For I = 0 To dtbDetalleFactura.Rows.Count - 1
                If dtbDetalleFactura.Rows(I).Item(6) = 0 Then
                    Dim decTotalPorLinea As Decimal = dtbDetalleFactura.Rows(I).Item(5)
                    Dim decDescuentoPorLinea As Decimal = txtDescuento.Text / decSubTotal * decTotalPorLinea
                    decTotalPorLinea = decTotalPorLinea - decDescuentoPorLinea
                    decImpuesto += decTotalPorLinea * dtbDetalleFactura.Rows(I).Item(9) / 100
                End If
            Next
        End If
        decGrabado = Math.Round(decGrabado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decExcento + decGrabado + decImpuesto - txtDescuento.Text, 2, MidpointRounding.AwayFromZero)
        decTotalCosto = Math.Round(decTotalCosto, 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
        decSaldoPorPagar = decTotal - decTotalPago
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        txtPagoDelCliente.Text = FormatNumber(decTotal, 2)
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
        Try
            cboCondicionVenta.ValueMember = "IdCondicionVenta"
            cboCondicionVenta.DisplayMember = "Descripcion"
            cboCondicionVenta.DataSource = Await PuntoventaWCF.ObtenerListaCondicionVenta()
            cboFormaPago.ValueMember = "IdFormaPago"
            cboFormaPago.DisplayMember = "Descripcion"
            cboFormaPago.DataSource = Await PuntoventaWCF.ObtenerListaFormaPagoFactura()
            cboTipoMoneda.ValueMember = "IdTipoMoneda"
            cboTipoMoneda.DisplayMember = "Descripcion"
            cboTipoMoneda.DataSource = Await PuntoventaWCF.ObtenerListaTipoMoneda()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarListaBancoAdquiriente() As Task
        Dim lista As IList = Await PuntoventaWCF.ObtenerListaBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa)
        If lista.Count() = 0 Then
            Throw New Exception("Debe parametrizar la lista de bancos adquirientes para pagos con tarjeta.")
        Else
            cboTipoBanco.DataSource = lista
            cboTipoBanco.ValueMember = "IdBanco"
            cboTipoBanco.DisplayMember = "Descripcion"
        End If
    End Function

    Private Async Function CargarListaCuentaBanco() As Task
        Dim lista As IList = Await PuntoventaWCF.ObtenerListaCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa)
        If lista.Count() = 0 Then
            Throw New Exception("Debe parametrizar la lista de bancos para registrar movimientos.")
        Else
            cboTipoBanco.DataSource = lista
            cboTipoBanco.ValueMember = "IdCuenta"
            cboTipoBanco.DisplayMember = "Descripcion"
        End If
    End Function

    Private Async Function ValidarProducto(ByVal strCodigoProducto As String) As Task
        If Not bolInit Then
            If strCodigoProducto <> "" Then
                txtDescripcion.Text = ""
                txtUnidad.Text = ""
                txtCantidad.Text = "1"
                txtPrecio.Text = ""
                If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If strCodigoProducto.IndexOf(" ") >= 0 Then
                        strCodigoProducto = strCodigoProducto.Substring(0, strCodigoProducto.IndexOf(" "))
                    End If
                End If
                Try
                    producto = Await PuntoventaWCF.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, strCodigoProducto)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End Try
                If producto Is Nothing Then
                    MessageBox.Show("El código ingresado no existe. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Text = ""
                    txtCodigo.Focus()
                    Exit Function
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
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
                If Not cliente.ExoneradoDeImpuesto Then
                    txtPrecio.Text = FormatNumber(Math.Round(decPrecioVenta * (1 + (producto.ParametroImpuesto.TasaImpuesto / 100)), 2), 2)
                Else
                    txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
                End If
                txtUnidad.Text = producto.IdTipoUnidad
            End If
        End If
    End Function

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await PuntoventaWCF.ObtenerListaProductos(FrmPrincipal.empresaGlobal.IdEmpresa, 1, 0, True)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmFactura_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Me.KeyPreview = True
    End Sub

    Private Async Sub FrmFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        Await CargarCombos()
        Try
            Await CargarListaBancoAdquiriente()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
            Await CargarAutoCompletarProducto()
        End If
        IniciaTablasDeDetalle()
        EstablecerPropiedadesDataGridView()
        grdDetalleFactura.DataSource = dtbDetalleFactura
        grdDesglosePago.DataSource = dtbDesglosePago
        bolInit = False
        txtCantidad.Text = "1"
        txtSubTotal.Text = FormatNumber(0, 2)
        txtPorDesc.Text = FormatNumber(0, 0)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtPagoDelCliente.Text = FormatNumber(0, 2)
        txtCambio.Text = FormatNumber(0, 2)
        txtIdOrdenServicio.Text = "0"
        txtIdProforma.Text = "0"
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        Try
            cliente = New Cliente With {
                .IdCliente = 1,
                .Nombre = "CLIENTE DE CONTADO"
            }
            txtNombreCliente.Text = cliente.Nombre
        Catch ex As Exception
            MessageBox.Show("Error al consultar el cliente de contado. Por favor consulte con su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            vendedor = Await PuntoventaWCF.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa)
            txtVendedor.Text = vendedor.Nombre
        Catch ex As Exception
            MessageBox.Show("Debe ingresar al menos un vendedor para generar la facturación. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        shtConsecutivoPago = 0
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdFactura.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtDocumento.Text = ""
        txtIdOrdenServicio.Text = "0"
        txtIdProforma.Text = "0"
        cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        txtPlazoCredito.Text = ""
        dtbDetalleFactura.Rows.Clear()
        grdDetalleFactura.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtPorDesc.Text = FormatNumber(0, 0)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtPagoDelCliente.Text = FormatNumber(0, 2)
        txtCambio.Text = FormatNumber(0, 2)
        decCostoPorInstalacion = 0
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPrecio.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        decTotal = 0
        decTotalPago = 0
        txtDescuento.ReadOnly = False
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
        btnProforma.Enabled = True
        Try
            cliente = New Cliente With {
                .IdCliente = 1,
                .Nombre = "CLIENTE DE CONTADO"
            }
            txtNombreCliente.Text = cliente.Nombre
        Catch ex As Exception
            MessageBox.Show("Error al consultar el cliente de contado. Por favor consulte con su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            vendedor = Await PuntoventaWCF.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa)
            txtVendedor.Text = vendedor.Nombre
        Catch ex As Exception
            MessageBox.Show("Debe ingresar al menos un vendedor para generar la facturación. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        bolInit = False
        txtMontoPago.Text = ""
        shtConsecutivoPago = 0
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdFactura.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await PuntoventaWCF.AnularFactura(txtIdFactura.Text, FrmPrincipal.usuarioGlobal.IdUsuario)
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
        Dim formBusqueda As New FrmBusquedaFactura()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                factura = Await PuntoventaWCF.ObtenerFactura(FrmPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If factura IsNot Nothing Then
                bolInit = True
                txtIdFactura.Text = factura.IdFactura
                cliente = factura.Cliente
                txtNombreCliente.Text = factura.Cliente.Nombre
                txtFecha.Text = factura.Fecha
                txtDocumento.Text = factura.NoDocumento
                txtIdOrdenServicio.Text = factura.IdOrdenServicio
                txtIdProforma.Text = factura.IdProforma
                cboCondicionVenta.SelectedValue = factura.IdCondicionVenta
                txtPlazoCredito.Text = factura.PlazoCredito
                vendedor = factura.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                txtDescuento.Text = FormatNumber(factura.Descuento, 2)
                decCostoPorInstalacion = 0
                CargarDetalleFactura(factura)
                Await CargarDesglosePago(factura)
                CargarTotales()
                CargarTotalesPago()
                txtPagoDelCliente.Text = FormatNumber(factura.MontoPagado, 2)
                txtCambio.Text = FormatNumber(txtPagoDelCliente.Text - decTotal, 2)
                txtDescuento.ReadOnly = True
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnBusProd.Enabled = False
                btnImprimir.Enabled = True
                btnGenerarPDF.Enabled = True
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnOrdenServicio.Enabled = False
                btnProforma.Enabled = False
                btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                btnGuardar.Enabled = False
                bolInit = False
            Else
                MessageBox.Show("No existe registro de factura asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnOrdenServicio_Click(sender As Object, e As EventArgs) Handles btnOrdenServicio.Click
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        formBusqueda.ExcluirOrdenesAplicadas()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'ordenServicio = servicioFacturacion.ObtenerOrdenServicio(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenServicio IsNot Nothing Then
                bolInit = True
                txtIdFactura.Text = ""
                cliente = ordenServicio.Cliente
                txtNombreCliente.Text = ordenServicio.Cliente.Nombre
                txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
                txtDocumento.Text = ordenServicio.Placa
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                txtPlazoCredito.Text = ""
                vendedor = ordenServicio.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                txtDescuento.Text = FormatNumber(ordenServicio.Descuento, 2)
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
                decCostoPorInstalacion = 0
                CargarDetalleOrdenServicio(ordenServicio)
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
                btnGenerarPDF.Enabled = False
                btnBuscarCliente.Enabled = True
                bolInit = False
            Else
                MessageBox.Show("No existe registro de orden de servicio asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnProforma_Click(sender As Object, e As EventArgs) Handles btnProforma.Click
        Dim formBusqueda As New FrmBusquedaProforma()
        formBusqueda.ExcluirOrdenesFacturadas()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'proforma = servicioFacturacion.ObtenerProforma(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If proforma IsNot Nothing Then
                bolInit = True
                txtIdFactura.Text = ""
                cliente = proforma.Cliente
                txtNombreCliente.Text = proforma.Cliente.Nombre
                txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
                txtDocumento.Text = proforma.NoDocumento
                cboCondicionVenta.SelectedValue = proforma.IdCondicionVenta
                txtPlazoCredito.Text = proforma.PlazoCredito
                vendedor = proforma.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                txtDescuento.Text = FormatNumber(proforma.Descuento, 2)
                txtIdProforma.Text = proforma.IdProforma
                decCostoPorInstalacion = 0
                CargarDetalleProforma(proforma)
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
                btnGenerarPDF.Enabled = False
                btnBuscarCliente.Enabled = True
                bolInit = False
            Else
                MessageBox.Show("No existe registro de orden de servicio asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnBuscaVendedor_Click(sender As Object, e As EventArgs) Handles btnBuscaVendedor.Click
        Dim formBusquedaVendedor As New FrmBusquedaVendedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaVendedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                vendedor = Await PuntoventaWCF.ObtenerVendedor(FrmPrincipal.intBusqueda)
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

    Private Async Sub BtnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                cliente = Await PuntoventaWCF.ObtenerCliente(FrmPrincipal.intBusqueda)
                txtNombreCliente.Text = cliente.Nombre
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
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
            .intTipoPrecio = 0
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmPrincipal.strBusqueda
            Await ValidarProducto(txtCodigo.Text)
        End If
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or decTotal = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtPagoDelCliente.Text = "" Then
            MessageBox.Show("Debe ingresar el monto con el que paga el cliente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado Then
            If decSaldoPorPagar > 0 Then
                MessageBox.Show("El total del desglose de pago de la factura no es suficiente para cubrir el saldo por pagar actual.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If decSaldoPorPagar < 0 Then
                MessageBox.Show("El total del desglose de pago de la factura es superior al saldo por pagar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
        If txtIdFactura.Text = "" Then
            btnGuardar.Enabled = False
            factura = New Factura With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdTerminal = FrmPrincipal.equipoGlobal.IdTerminal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdCliente = cliente.IdCliente,
                .IdCondicionVenta = cboCondicionVenta.SelectedValue,
                .PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text),
                .Fecha = Now(),
                .NoDocumento = txtDocumento.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento,
                .Grabado = decGrabado,
                .Descuento = CDbl(txtDescuento.Text),
                .Impuesto = decImpuesto,
                .MontoPagado = CDbl(txtPagoDelCliente.Text),
                .TotalCosto = decTotalCosto,
                .Nulo = False,
                .IdOrdenServicio = IIf(txtIdOrdenServicio.Text <> "", txtIdOrdenServicio.Text, 0),
                .IdProforma = IIf(txtIdProforma.Text <> "", txtIdProforma.Text, 0)
            }
            For I = 0 To dtbDetalleFactura.Rows.Count - 1
                detalleFactura = New DetalleFactura With {
                    .IdProducto = dtbDetalleFactura.Rows(I).Item(0),
                    .Descripcion = dtbDetalleFactura.Rows(I).Item(2),
                    .Cantidad = dtbDetalleFactura.Rows(I).Item(3),
                    .PrecioVenta = dtbDetalleFactura.Rows(I).Item(4),
                    .Excento = dtbDetalleFactura.Rows(I).Item(6),
                    .PrecioCosto = dtbDetalleFactura.Rows(I).Item(7),
                    .CostoInstalacion = dtbDetalleFactura.Rows(I).Item(8),
                    .PorcentajeIVA = dtbDetalleFactura.Rows(I).Item(9)
                }
                factura.DetalleFactura.Add(detalleFactura)
            Next
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoFactura With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(1),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(3),
                    .TipoTarjeta = dtbDesglosePago.Rows(I).Item(5),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(6),
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(7),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(9),
                    .MontoForaneo = dtbDesglosePago.Rows(I).Item(10)
                }
                factura.DesglosePagoFactura.Add(desglosePago)
            Next
            Try
                factura = Await PuntoventaWCF.AgregarFactura(factura)
                txtIdFactura.Text = factura.IdFactura
            Catch ex As Exception
                txtIdFactura.Text = ""
                btnGuardar.Enabled = True
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGenerarPDF.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        btnBusProd.Enabled = False
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
        btnOrdenServicio.Enabled = False
        btnProforma.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdFactura.Text <> "" Then
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = txtIdFactura.Text,
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strDocumento = txtDocumento.Text,
                    .strFecha = txtFecha.Text,
                    .strSubTotal = txtSubTotal.Text,
                    .strDescuento = txtDescuento.Text,
                    .strImpuesto = txtImpuesto.Text,
                    .strTotal = txtTotal.Text,
                    .strPagoCon = txtPagoDelCliente.Text,
                    .strCambio = txtCambio.Text
                }
                If factura.IdDocElectronico IsNot Nothing Then
                    comprobanteImpresion.strClaveNumerica = factura.IdDocElectronico
                Else
                    comprobanteImpresion.strClaveNumerica = ""
                End If
                arrDetalleFactura = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For I = 0 To dtbDetalleFactura.Rows.Count - 1
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleFactura.Rows(I).Item(1) + "-" + dtbDetalleFactura.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleFactura.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleFactura.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleFactura.Rows(I).Item(3)) * CDbl(dtbDetalleFactura.Rows(I).Item(4)), 2),
                    .strExcento = IIf(dtbDetalleFactura.Rows(I).Item(6) = 0, "G", "E")
                }
                    arrDetalleFactura.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleFactura
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
                ModuloImpresion.ImprimirFactura(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdFactura.Text <> "" Then
            Dim documento As DocumentoElectronico
            Try
                documento = Await PuntoventaWCF.ObtenerDocumentoElectronicoPorClave(factura.IdDocElectronico)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Dim datos As EstructuraPDF = New EstructuraPDF()
            Dim facturaElectronica As FacturaElectronica = Nothing
            Dim serializer As New XmlSerializer(GetType(FacturaElectronica))
            Using memStream As MemoryStream = New MemoryStream(documento.DatosDocumento)
                facturaElectronica = serializer.Deserialize(memStream)
            End Using
            Try
                Dim logoImage As Image
                Using ms As New MemoryStream(FrmPrincipal.empresaGlobal.Logotipo)
                    logoImage = Image.FromStream(ms)
                End Using
                datos.Logotipo = logoImage
            Catch ex As Exception
                Dim noImage As Image = My.Resources.emptyImage
                datos.Logotipo = noImage
            End Try
            datos.TituloDocumento = "FACTURA ELECTRONICA"
            datos.NombreEmpresa = IIf(facturaElectronica.Emisor.NombreComercial IsNot Nothing, facturaElectronica.Emisor.NombreComercial, facturaElectronica.Emisor.Nombre)
            datos.Consecutivo = facturaElectronica.NumeroConsecutivo
            datos.PlazoCredito = IIf(facturaElectronica.PlazoCredito IsNot Nothing, facturaElectronica.PlazoCredito, "")
            datos.Clave = facturaElectronica.Clave
            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(Integer.Parse(facturaElectronica.CondicionVenta.ToString().Substring(5)))
            datos.Fecha = facturaElectronica.FechaEmision.ToString("dd/MM/yyyy hh,mm,ss")
            datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(Integer.Parse(facturaElectronica.MedioPago(0).ToString().Substring(5)))
            datos.NombreEmisor = facturaElectronica.Emisor.Nombre
            datos.NombreComercialEmisor = facturaElectronica.Emisor.NombreComercial
            datos.IdentificacionEmisor = facturaElectronica.Emisor.Identificacion.Numero
            datos.CorreoElectronicoEmisor = facturaElectronica.Emisor.CorreoElectronico
            If facturaElectronica.Emisor.Telefono IsNot Nothing Then
                datos.TelefonoEmisor = facturaElectronica.Emisor.Telefono.NumTelefono
            Else
                datos.TelefonoEmisor = ""
            End If
            If facturaElectronica.Emisor.Fax IsNot Nothing Then
                datos.FaxEmisor = facturaElectronica.Emisor.Fax.NumTelefono
            Else
                datos.FaxEmisor = ""
            End If
            datos.ProvinciaEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Provincia.Descripcion
            datos.CantonEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Descripcion
            datos.DistritoEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Descripcion
            datos.BarrioEmisor = FrmPrincipal.empresaGlobal.Barrio.Descripcion
            datos.DireccionEmisor = facturaElectronica.Emisor.Ubicacion.OtrasSenas
            If facturaElectronica.Receptor IsNot Nothing Then
                datos.PoseeReceptor = True
                datos.NombreReceptor = facturaElectronica.Receptor.Nombre
                datos.NombreComercialReceptor = IIf(facturaElectronica.Receptor.NombreComercial IsNot Nothing, facturaElectronica.Receptor.NombreComercial, "")
                datos.IdentificacionReceptor = facturaElectronica.Receptor.Identificacion.Numero
                datos.CorreoElectronicoReceptor = facturaElectronica.Receptor.CorreoElectronico
                If facturaElectronica.Receptor.Telefono IsNot Nothing Then
                    datos.TelefonoReceptor = facturaElectronica.Receptor.Telefono.NumTelefono
                Else
                    datos.TelefonoReceptor = ""
                End If
                If facturaElectronica.Receptor.Fax IsNot Nothing Then
                    datos.FaxReceptor = facturaElectronica.Receptor.Fax.NumTelefono
                Else
                    datos.FaxReceptor = ""
                End If
                Dim barrio As Barrio = cliente.Barrio
                datos.ProvinciaReceptor = cliente.Barrio.Distrito.Canton.Provincia.Descripcion
                datos.CantonReceptor = cliente.Barrio.Distrito.Canton.Descripcion
                datos.DistritoReceptor = cliente.Barrio.Distrito.Descripcion
                datos.BarrioReceptor = cliente.Barrio.Descripcion
                datos.DireccionReceptor = facturaElectronica.Receptor.Ubicacion.OtrasSenas
            End If
            For Each linea As FacturaElectronicaLineaDetalle In facturaElectronica.DetalleServicio
                Dim detalle As EstructuraPDFDetalleServicio = New EstructuraPDFDetalleServicio With {
                    .NumeroLinea = linea.NumeroLinea,
                    .Codigo = linea.Codigo(0).Codigo,
                    .Detalle = linea.Detalle,
                    .PrecioUnitario = linea.PrecioUnitario.ToString("N5", CultureInfo.InvariantCulture),
                    .TotalLinea = linea.MontoTotalLinea.ToString("N5", CultureInfo.InvariantCulture)
                }
                datos.DetalleServicio.Add(detalle)
            Next
            datos.SubTotal = facturaElectronica.ResumenFactura.TotalVenta.ToString("N5", CultureInfo.InvariantCulture)
            datos.Descuento = IIf(facturaElectronica.ResumenFactura.TotalDescuentosSpecified, facturaElectronica.ResumenFactura.TotalDescuentos.ToString("N5", CultureInfo.InvariantCulture), "0.00000")
            datos.Impuesto = IIf(facturaElectronica.ResumenFactura.TotalImpuestoSpecified, facturaElectronica.ResumenFactura.TotalImpuesto.ToString("N5", CultureInfo.InvariantCulture), "0.00000")
            datos.TotalGeneral = facturaElectronica.ResumenFactura.TotalComprobante.ToString("N5", CultureInfo.InvariantCulture)
            datos.CodigoMoneda = IIf(facturaElectronica.ResumenFactura.CodigoMonedaSpecified, facturaElectronica.ResumenFactura.CodigoMoneda.ToString(), "")
            datos.TipoDeCambio = IIf(facturaElectronica.ResumenFactura.CodigoMonedaSpecified, facturaElectronica.ResumenFactura.TipoCambio.ToString(), "")
            Try
                Dim poweredByImage As Image = My.Resources.poweredByImage
                datos.PoweredByLogotipo = poweredByImage
            Catch ex As Exception
                datos.PoweredByLogotipo = Nothing
            End Try
            Try
                Dim pdfBytes As Byte() = UtilitarioPDF.GenerarPDFFacturaElectronica(datos)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FAC-" + documento.ClaveNumerica + ".pdf"
                File.WriteAllBytes(pdfFilePath, pdfBytes)
                Process.Start(pdfFilePath)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCodigo.Text <> "" And txtDescripcion.Text = "" Then
            Await ValidarProducto(txtCodigo.Text)
        End If
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" And txtPrecio.Text <> "" And txtUnidad.Text <> "" Then
            If txtPrecio.Text <= 0 Then
                MessageBox.Show("El precio de venta no puede ser igual o menor a 0.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, 0)
            CargarTotales()
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtUnidad.Text = ""
            txtPrecio.Text = ""
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleFactura.Rows.Count > 0 Then
            If FrmPrincipal.empresaGlobal.DesglosaServicioInst And grdDetalleFactura.CurrentRow.Cells(0).Value = FrmPrincipal.empresaGlobal.CodigoServicioInst And decCostoPorInstalacion > 0 Then
                MessageBox.Show("La línea seleccionada no puede eliminarse. Debe eliminar los productos relacionados.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            producto = Await PuntoventaWCF.ObtenerProducto(grdDetalleFactura.CurrentRow.Cells(0).Value)
            If CDbl(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value).Item(8)) > 0 Then
                producto = Await PuntoventaWCF.ObtenerProducto(FrmPrincipal.empresaGlobal.CodigoServicioInst)
                DescargarLineaDetalleInstalacion(producto, CDbl(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value).Item(8)) * CDbl(grdDetalleFactura.CurrentRow.Cells(3).Value))
                decCostoPorInstalacion -= CDbl(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value).Item(8)) * CDbl(grdDetalleFactura.CurrentRow.Cells(3).Value)
            End If
            dtbDetalleFactura.Rows.Remove(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value))
            grdDetalleFactura.Refresh()
            CargarTotales()
            txtMontoPago.Text = txtTotal.Text
            txtPagoDelCliente.Text = txtTotal.Text
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub CboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
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
                If cboFormaPago.SelectedValue = StaticFormaPago.Efectivo Then
                    cboTipoMoneda.Enabled = True
                Else
                    cboTipoMoneda.Enabled = False
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
                cboTipoMoneda.Enabled = False
            End If
        End If
    End Sub

    Private Sub CboTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoMoneda.SelectedValueChanged
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
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
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

    Private Sub Precio_Validated(sender As Object, e As EventArgs) Handles txtPrecio.Validated
        If Not producto Is Nothing And txtPrecio.Text <> "" Then
            If Not cliente.ExoneradoDeImpuesto And producto.ParametroImpuesto.TasaImpuesto > 0 Then
                decPrecioVenta = Math.Round(txtPrecio.Text / (1 + (producto.ParametroImpuesto.TasaImpuesto / 100)), 2)
            Else
                decPrecioVenta = Math.Round(CDbl(txtPrecio.Text), 2)
            End If
            txtPrecio.Text = FormatNumber(txtPrecio.Text, 2)
        Else
            txtPrecio.Text = FormatNumber(0, 2)
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(ByVal sender As Object, ByVal e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            Await ValidarProducto(txtCodigo.Text)
            If Not producto Is Nothing Then
                If FrmPrincipal.empresaGlobal.ModificaDescProducto = True Then
                    txtDescripcion.ReadOnly = False
                    txtDescripcion.Focus()
                    txtDescripcion.SelectAll()
                End If
            Else
                txtDescripcion.ReadOnly = True
                txtPrecio.Focus()
                txtPrecio.SelectAll()
            End If
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub TxtPorDesc_Validated(sender As Object, e As EventArgs) Handles txtPorDesc.Validated
        If txtPorDesc.Text = "" Then
            txtDescuento.Text = FormatNumber(0, 2)
        ElseIf txtPorDesc.Text > 100 Then
            MessageBox.Show("El descuento debe ser menor al SubTotal. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            txtPorDesc.Text = FormatNumber(0, 0)
            txtDescuento.Text = FormatNumber(0, 2)
        Else
            txtDescuento.Text = FormatNumber(decSubTotal * txtPorDesc.Text / 100, 2)
        End If
        CargarTotales()
        CargarTotalesPago()
    End Sub

    Private Sub TxtDescuento_Validated(sender As Object, e As EventArgs) Handles txtDescuento.Validated
        If txtDescuento.Text = "" Then
            txtDescuento.Text = FormatNumber(0, 2)
        Else
            If txtDescuento.Text > decSubTotal Then
                MessageBox.Show("El descuento debe ser menor al SubTotal. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescuento.Text = 0
            End If
            txtDescuento.Text = FormatNumber(txtDescuento.Text, 2)
        End If
        CargarTotales()
        CargarTotalesPago()
    End Sub

    Private Sub TxtMonto_Validated(sender As Object, e As EventArgs) Handles txtMontoPago.Validated
        If txtMontoPago.Text <> "" Then txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
    End Sub

    Private Sub TxtMontoPagado_Validated(sender As Object, e As EventArgs) Handles txtPagoDelCliente.Validated
        If txtPagoDelCliente.Text = "" Then
            txtPagoDelCliente.Text = FormatNumber(0, 2)
        ElseIf txtPagoDelCliente.Text < CDbl(txtTotal.Text) Then
            txtPagoDelCliente.Text = FormatNumber(0, 2)
            MessageBox.Show("El monto con el que el cliente paga no puede ser menor al total de la factura. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        txtPagoDelCliente.Text = FormatNumber(txtPagoDelCliente.Text, 2)
        txtCambio.Text = FormatNumber(txtPagoDelCliente.Text - decTotal, 2)
    End Sub

    Private Sub ValidaDigitosSinDecimal(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecio.KeyPress, txtDescuento.KeyPress, txtMontoPago.KeyPress, txtPorDesc.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class