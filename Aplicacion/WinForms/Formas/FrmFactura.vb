Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports System.Xml.Serialization
Imports LeandroSoftware.Core
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.TiposDatos
Imports System.Threading.Tasks

Public Class FrmFactura
#Region "Variables"
    Private strMotivoRechazo As String
    Private dblExcento, dblGrabado, dblImpuesto, dblPorcentajeIVA, dblTotalCosto, dblTotalPago, dblTotal, dblSubTotal, dblSaldoPorPagar, dblCostoPorInstalacion As Decimal
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
    Private tipoMoneda As TipoMoneda
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
        dvcCantidad.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcCantidad)

        dvcPrecio.DataPropertyName = "PRECIO"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.Visible = True
        dvcPrecio.ReadOnly = True
        dvcPrecio.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleFactura.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
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
            dtrRowDetFactura.Item(6) = detalle.Producto.Excento
            dtrRowDetFactura.Item(7) = detalle.PrecioCosto
            dtrRowDetFactura.Item(8) = detalle.CostoInstalacion
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
            dblCostoPorInstalacion += detalle.Cantidad * detalle.CostoInstalacion
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
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
            dblCostoPorInstalacion += detalle.Cantidad * detalle.CostoInstalacion
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
                Dim banco As BancoAdquiriente = Await ClienteWCF.ObtenerBancoAdquiriente(detalle.IdCuentaBanco)
                dtrRowDesglosePago.Item(4) = banco.Descripcion
            ElseIf detalle.IdFormaPago = StaticFormaPago.Cheque Or detalle.IdFormaPago = StaticFormaPago.TransferenciaDepositoBancario Then
                Dim banco As CuentaBanco = Await ClienteWCF.ObtenerCuentaBanco(detalle.IdCuentaBanco)
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
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleFactura.Rows(intIndice).Item(3) += dblCantidad
            dtbDetalleFactura.Rows(intIndice).Item(4) = dblPrecio
            dtbDetalleFactura.Rows(intIndice).Item(5) = dtbDetalleFactura.Rows(intIndice).Item(3) * dtbDetalleFactura.Rows(intIndice).Item(4)
            dtbDetalleFactura.Rows(intIndice).Item(6) = producto.Excento
            dtbDetalleFactura.Rows(intIndice).Item(7) = producto.PrecioCosto
            dtbDetalleFactura.Rows(intIndice).Item(8) = dblCostoInstalacion
        Else
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = producto.IdProducto
            dtrRowDetFactura.Item(1) = producto.Codigo
            dtrRowDetFactura.Item(2) = strDescripcion
            dtrRowDetFactura.Item(3) = dblCantidad
            dtrRowDetFactura.Item(4) = dblPrecio
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = producto.Excento
            dtrRowDetFactura.Item(7) = producto.PrecioCosto
            dtrRowDetFactura.Item(8) = dblCostoInstalacion
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        End If
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarLineaDetalleInstalacion(ByVal producto As Producto, ByVal dblTotal As Double)
        Dim intIndice As Integer = dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleFactura.Rows(intIndice).Item(4) += dblTotal
            dtbDetalleFactura.Rows(intIndice).Item(5) += dblTotal
            dtbDetalleFactura.Rows(intIndice).Item(6) = producto.Excento
            dtbDetalleFactura.Rows(intIndice).Item(7) = producto.PrecioCosto
        Else
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = producto.IdProducto
            dtrRowDetFactura.Item(1) = producto.Codigo
            dtrRowDetFactura.Item(2) = producto.Descripcion
            dtrRowDetFactura.Item(3) = 1
            dtrRowDetFactura.Item(4) = dblTotal
            dtrRowDetFactura.Item(5) = dblTotal
            dtrRowDetFactura.Item(6) = producto.Excento
            dtrRowDetFactura.Item(7) = producto.PrecioCosto
            dtrRowDetFactura.Item(8) = 0
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        End If
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub DescargarLineaDetalleInstalacion(ByVal producto As Producto, ByVal dblTotal As Double)
        Dim intIndice As Integer = dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleFactura.Rows(intIndice).Item(4) -= dblTotal
            dtbDetalleFactura.Rows(intIndice).Item(5) -= dblTotal
            dtbDetalleFactura.Rows(intIndice).Item(6) = producto.Excento
        End If
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarLineaDesglosePago()
        Dim dblMontoLocal, dblMontoForaneo As Decimal
        dblMontoForaneo = CDbl(txtMontoPago.Text)
        dblMontoLocal = txtMontoPago.Text * txtTipoCambio.Text
        If dblMontoLocal > dblSaldoPorPagar Then
            dblMontoLocal = dblSaldoPorPagar
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
        Dim dblSubTotalSinIVA As Decimal = 0
        dblSubTotal = 0
        dblExcento = 0
        dblGrabado = 0
        dblImpuesto = 0
        dblTotalCosto = 0
        For I = 0 To dtbDetalleFactura.Rows.Count - 1
            If dtbDetalleFactura.Rows(I).Item(6) = 0 Then
                dblGrabado += grdDetalleFactura.Rows(I).Cells(5).Value
            Else
                dblExcento += grdDetalleFactura.Rows(I).Cells(5).Value
            End If
            dblTotalCosto += Math.Round(CDbl(grdDetalleFactura.Rows(I).Cells(7).Value), 2, MidpointRounding.AwayFromZero)
        Next
        dblSubTotal = dblGrabado + dblExcento
        If dblSubTotal > 0 Then
            dblSubTotalSinIVA = Math.Round((dblGrabado / (1 + (dblPorcentajeIVA / 100))) + dblExcento, 2, MidpointRounding.AwayFromZero)
            dblExcento = Math.Round(dblExcento - (txtDescuento.Text / dblSubTotal * dblExcento), 2, MidpointRounding.AwayFromZero)
            dblGrabado = Math.Round(dblGrabado - (txtDescuento.Text / dblSubTotal * dblGrabado), 2, MidpointRounding.AwayFromZero)
            dblGrabado = dblGrabado / (1 + (dblPorcentajeIVA / 100))
            dblImpuesto = dblGrabado * (dblPorcentajeIVA / 100)
        End If
        dblGrabado = Math.Round(dblGrabado, 2, MidpointRounding.AwayFromZero)
        dblImpuesto = Math.Round(dblImpuesto, 2, MidpointRounding.AwayFromZero)
        txtImpuesto.Text = FormatNumber(dblImpuesto, 2)
        txtSubTotal.Text = FormatNumber(dblSubTotalSinIVA, 2)
        dblTotal = Math.Round(dblExcento + dblGrabado + dblImpuesto, 2, MidpointRounding.AwayFromZero)
        txtTotal.Text = FormatNumber(dblTotal, 2)
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtMontoPago.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtPagoDelCliente.Text = FormatNumber(dblTotal, 2)
    End Sub

    Private Sub CargarTotalesPago()
        dblTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            dblTotalPago = dblTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(10))
        Next
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtMontoPago.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        Try
            cboCondicionVenta.ValueMember = "IdCondicionVenta"
            cboCondicionVenta.DisplayMember = "Descripcion"
            cboCondicionVenta.DataSource = Await ClienteWCF.ObtenerListaCondicionVenta()
            cboFormaPago.ValueMember = "IdFormaPago"
            cboFormaPago.DisplayMember = "Descripcion"
            cboFormaPago.DataSource = Await ClienteWCF.ObtenerListaFormaPagoFactura()
            cboTipoMoneda.ValueMember = "IdTipoMoneda"
            cboTipoMoneda.DisplayMember = "Descripcion"
            cboTipoMoneda.DataSource = Await ClienteWCF.ObtenerListaTipoMoneda()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarListaBancoAdquiriente() As Task
        Dim lista As IList = Await ClienteWCF.ObtenerListaBancoAdquiriente(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        If lista.Count() = 0 Then
            Throw New Exception("Debe parametrizar la lista de bancos adquirientes para pagos con tarjeta.")
        Else
            cboTipoBanco.DataSource = lista
            cboTipoBanco.ValueMember = "IdBanco"
            cboTipoBanco.DisplayMember = "Descripcion"
        End If
    End Function

    Private Async Function CargarListaCuentaBanco() As Task
        Dim lista As IList = Await ClienteWCF.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
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
                If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If strCodigoProducto.IndexOf(" ") >= 0 Then
                        strCodigoProducto = strCodigoProducto.Substring(0, strCodigoProducto.IndexOf(" "))
                    End If
                End If
                Try
                    producto = Await ClienteWCF.ObtenerProductoPorCodigo(strCodigoProducto)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Function
                End Try
                If producto Is Nothing Then
                    MessageBox.Show("El código ingresado no existe. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Text = ""
                    txtUnidad.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecio.Text = ""
                    txtCodigo.Focus()
                    Exit Function
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
                If cliente IsNot Nothing Then
                    If cliente.IdTipoPrecio = 1 Then
                        txtPrecio.Text = FormatNumber(producto.PrecioVenta1, 2)
                    ElseIf cliente.IdTipoPrecio = 2 Then
                        txtPrecio.Text = FormatNumber(producto.PrecioVenta2, 2)
                    ElseIf cliente.IdTipoPrecio = 3 Then
                        txtPrecio.Text = FormatNumber(producto.PrecioVenta3, 2)
                    ElseIf cliente.IdTipoPrecio = 4 Then
                        txtPrecio.Text = FormatNumber(producto.PrecioVenta4, 2)
                    ElseIf cliente.IdTipoPrecio = 5 Then
                        txtPrecio.Text = FormatNumber(producto.PrecioVenta5, 2)
                    Else
                        txtPrecio.Text = FormatNumber(producto.PrecioVenta1, 2)
                    End If
                Else
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta1, 2)
                End If
                txtUnidad.Text = producto.IdTipoUnidad
                If producto.Tipo = StaticTipoProducto.Servicio Then
                    If FrmMenuPrincipal.empresaGlobal.ModificaDescProducto = True Then
                        txtDescripcion.ReadOnly = False
                        txtDescripcion.Focus()
                    End If
                Else
                    txtDescripcion.ReadOnly = True
                    txtPrecio.Focus()
                End If
            End If
        End If
    End Function

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await ClienteWCF.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, True)
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
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
        Await CargarCombos()
        Try
            Await CargarListaBancoAdquiriente()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
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
            tipoMoneda = New TipoMoneda With {
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .Descripcion = cboTipoMoneda.Text
            }
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
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
            vendedor = Await ClienteWCF.ObtenerVendedor(1)
            txtVendedor.Text = vendedor.Nombre
        Catch ex As Exception
            MessageBox.Show("Debe ingresar al menos un vendedor para generar la facturación. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmMenuPrincipal.decTipoCambioDolar.ToString())
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        shtConsecutivoPago = 0
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdFactura.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
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
        dblCostoPorInstalacion = 0
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPrecio.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
        dblSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        dblTotal = 0
        dblTotalPago = 0
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
            vendedor = Await ClienteWCF.ObtenerVendedor(1)
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
                    Await ClienteWCF.AnularFactura(txtIdFactura.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
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
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                factura = Await ClienteWCF.ObtenerFactura(FrmMenuPrincipal.intBusqueda)
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
                dblPorcentajeIVA = factura.PorcentajeIVA
                dblCostoPorInstalacion = 0
                CargarDetalleFactura(factura)
                Await CargarDesglosePago(factura)
                CargarTotales()
                CargarTotalesPago()
                txtPagoDelCliente.Text = FormatNumber(factura.MontoPagado, 2)
                txtCambio.Text = FormatNumber(txtPagoDelCliente.Text - dblTotal, 2)
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
                btnAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                btnGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                bolInit = False
            Else
                MessageBox.Show("No existe registro de factura asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Sub BtnOrdenServicio_Click(sender As Object, e As EventArgs) Handles btnOrdenServicio.Click
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        formBusqueda.ExcluirOrdenesAplicadas()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
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
                txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
                dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
                txtDocumento.Text = ordenServicio.Placa
                cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
                txtPlazoCredito.Text = ""
                vendedor = ordenServicio.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                txtDescuento.Text = FormatNumber(ordenServicio.Descuento, 2)
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
                dblCostoPorInstalacion = 0
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
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
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
                txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
                dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
                txtDocumento.Text = proforma.NoDocumento
                cboCondicionVenta.SelectedValue = proforma.IdCondicionVenta
                txtPlazoCredito.Text = proforma.PlazoCredito
                vendedor = proforma.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                txtDescuento.Text = FormatNumber(proforma.Descuento, 2)
                txtIdProforma.Text = proforma.IdProforma
                dblCostoPorInstalacion = 0
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
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaVendedor.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                vendedor = Await ClienteWCF.ObtenerVendedor(FrmMenuPrincipal.intBusqueda)
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
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                cliente = Await ClienteWCF.ObtenerCliente(FrmMenuPrincipal.intBusqueda)
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
        FrmMenuPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmMenuPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmMenuPrincipal.strBusqueda
            Await ValidarProducto(txtCodigo.Text)
        End If
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or dblTotal = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtPagoDelCliente.Text = "" Then
            MessageBox.Show("Debe ingresar el monto con el que paga el cliente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado Then
            If dblSaldoPorPagar > 0 Then
                MessageBox.Show("El total del desglose de pago de la factura no es suficiente para cubrir el saldo por pagar actual.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If

            If dblSaldoPorPagar < 0 Then
                MessageBox.Show("El total del desglose de pago de la factura es superior al saldo por pagar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        End If
        If txtIdFactura.Text = "" Then
            factura = New Factura With {
                .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmMenuPrincipal.intSucursal,
                .IdTerminal = FrmMenuPrincipal.intTerminal,
                .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
                .IdCliente = cliente.IdCliente,
                .IdCondicionVenta = cboCondicionVenta.SelectedValue,
                .PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text),
                .Fecha = Now(),
                .NoDocumento = txtDocumento.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = dblExcento,
                .Grabado = dblGrabado,
                .Descuento = CDbl(txtDescuento.Text),
                .PorcentajeIVA = dblPorcentajeIVA,
                .Impuesto = dblImpuesto,
                .MontoPagado = CDbl(txtPagoDelCliente.Text),
                .TotalCosto = dblTotalCosto,
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
                    .CostoInstalacion = dtbDetalleFactura.Rows(I).Item(8)
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
                txtIdFactura.Text = Await ClienteWCF.AgregarFactura(factura)
            Catch ex As Exception
                txtIdFactura.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            factura.NoDocumento = txtDocumento.Text
            Try
                Await ClienteWCF.ActualizarFactura(factura)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnGenerarPDF.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
        btnImprimir.Focus()
        btnGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
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
                    .usuario = FrmMenuPrincipal.usuarioGlobal,
                    .empresa = FrmMenuPrincipal.empresaGlobal,
                    .equipo = FrmMenuPrincipal.equipoGlobal,
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
                    comprobanteImpresion.strClaveNumerica = "09876543212345678909876543212345678909876543212345"
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
                documento = Await ClienteWCF.ObtenerDocumentoElectronico(factura.IdDocElectronico)
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
                Using ms As New MemoryStream(FrmMenuPrincipal.empresaGlobal.Logotipo)
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
            datos.ProvinciaEmisor = FrmMenuPrincipal.empresaGlobal.Barrio.Distrito.Canton.Provincia.Descripcion
            datos.CantonEmisor = FrmMenuPrincipal.empresaGlobal.Barrio.Distrito.Canton.Descripcion
            datos.DistritoEmisor = FrmMenuPrincipal.empresaGlobal.Barrio.Distrito.Descripcion
            datos.BarrioEmisor = FrmMenuPrincipal.empresaGlobal.Barrio.Descripcion
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
                Dim pdfBytes As Byte() = Utilitario.GenerarPDFFacturaElectronica(datos)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\FAC-" + documento.ClaveNumerica + ".pdf"
                File.WriteAllBytes(pdfFilePath, pdfBytes)
                Process.Start(pdfFilePath)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub CmdInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" And txtPrecio.Text <> "" And txtUnidad.Text <> "" Then
            If Not FrmMenuPrincipal.empresaGlobal.IncluyeInsumosEnFactura Then
                If txtPrecio.Text <= 0 Then
                    MessageBox.Show("El precio de venta no puede ser igual o menor a 0.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            If FrmMenuPrincipal.empresaGlobal.DesglosaServicioInst And FrmMenuPrincipal.empresaGlobal.PorcentajeInstalacion > 0 Then
                If producto.Tipo = StaticTipoProducto.Producto Then
                    If MessageBox.Show("Desea desglosar el servicio de instalación?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        Dim precioProducto, precioInstalacion As Double
                        precioInstalacion = txtPrecio.Text * FrmMenuPrincipal.empresaGlobal.PorcentajeInstalacion / 100
                        precioProducto = txtPrecio.Text - precioInstalacion
                        CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, precioProducto, precioInstalacion)
                        producto = Await ClienteWCF.ObtenerProducto(FrmMenuPrincipal.empresaGlobal.CodigoServicioInst)
                        CargarLineaDetalleInstalacion(producto, precioInstalacion * txtCantidad.Text)
                        dblCostoPorInstalacion += precioInstalacion * txtCantidad.Text
                    Else
                        CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, txtPrecio.Text, 0)
                    End If
                ElseIf producto.IdProducto = FrmMenuPrincipal.empresaGlobal.CodigoServicioInst Then
                    CargarLineaDetalleInstalacion(producto, txtCantidad.Text * txtPrecio.Text)
                Else
                    CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, txtPrecio.Text, 0)
                End If
            Else
                CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, txtPrecio.Text, 0)
            End If
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
            If FrmMenuPrincipal.empresaGlobal.DesglosaServicioInst And grdDetalleFactura.CurrentRow.Cells(0).Value = FrmMenuPrincipal.empresaGlobal.CodigoServicioInst And dblCostoPorInstalacion > 0 Then
                MessageBox.Show("La línea seleccionada no puede eliminarse. Debe eliminar los productos relacionados.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            producto = Await ClienteWCF.ObtenerProducto(grdDetalleFactura.CurrentRow.Cells(0).Value)
            If CDbl(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value).Item(8)) > 0 Then
                producto = Await ClienteWCF.ObtenerProducto(FrmMenuPrincipal.empresaGlobal.CodigoServicioInst)
                DescargarLineaDetalleInstalacion(producto, CDbl(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value).Item(8)) * CDbl(grdDetalleFactura.CurrentRow.Cells(3).Value))
                dblCostoPorInstalacion -= CDbl(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value).Item(8)) * CDbl(grdDetalleFactura.CurrentRow.Cells(3).Value)
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
            Try
                tipoMoneda = New TipoMoneda With {
                    .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                    .Descripcion = cboTipoMoneda.Text
                }
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmMenuPrincipal.decTipoCambioDolar.ToString())
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
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboTipoBanco.SelectedValue > 0 And dblTotal > 0 And txtMontoPago.Text <> "" Then
            If dblSaldoPorPagar = 0 Then
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

    Private Sub Precio_Leave(sender As Object, e As EventArgs) Handles txtPrecio.Leave
        If txtPrecio.Text <> "" Then
            txtPrecio.Text = FormatNumber(txtPrecio.Text, 2)
        Else
            txtPrecio.Text = FormatNumber(0, 2)
        End If
    End Sub

    Private Async Sub TxtCodigo_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.Validated
        Await ValidarProducto(txtCodigo.Text)
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
            txtDescuento.Text = FormatNumber(dblSubTotal * txtPorDesc.Text / 100, 2)
        End If
        CargarTotales()
        CargarTotalesPago()
    End Sub

    Private Sub TxtDescuento_Validated(sender As Object, e As EventArgs) Handles txtDescuento.Validated
        If txtDescuento.Text = "" Then
            txtDescuento.Text = FormatNumber(0, 2)
        Else
            If txtDescuento.Text > dblSubTotal Then
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
        txtCambio.Text = FormatNumber(txtPagoDelCliente.Text - dblTotal, 2)
    End Sub

    Private Sub ValidaDigitosSinDecimal(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        FrmMenuPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecio.KeyPress, txtDescuento.KeyPress, txtMontoPago.KeyPress, txtPorDesc.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class