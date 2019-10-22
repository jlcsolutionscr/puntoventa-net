Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Utilities

Public Class FrmFactura
#Region "Variables"
    Private decExcento, decGravado, decExonerado, decImpuesto, decTotalCosto, decTotalPago, decTotal, decSubTotal, decSaldoPorPagar, decPrecioVenta As Decimal
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
        dtbDetalleFactura.Columns.Add("PRECIOTOTAL", GetType(Decimal))
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
        Dim dvcPrecioTotal As New DataGridViewTextBoxColumn

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

        dvcPrecioTotal.DataPropertyName = "PORCENTAJEIVA"
        dvcPrecioTotal.HeaderText = "PorcIVA"
        dvcPrecioTotal.Width = 0
        dvcPrecioTotal.Visible = False
        grdDetalleFactura.Columns.Add(dvcPrecioTotal)

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

    Private Sub CargarDetalleFactura(factura As Factura)
        dtbDetalleFactura.Rows.Clear()
        For Each detalle As DetalleFactura In factura.DetalleFactura
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = Math.Round(detalle.PrecioVenta / (1 + (detalle.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero)
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = detalle.Excento
            dtrRowDetFactura.Item(7) = detalle.PrecioCosto
            dtrRowDetFactura.Item(8) = detalle.CostoInstalacion
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PrecioVenta
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleOrdenServicio(ordenServicio As OrdenServicio)
        dtbDetalleFactura.Rows.Clear()
        For Each detalle As DetalleOrdenServicio In ordenServicio.DetalleOrdenServicio
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = Math.Round(detalle.PrecioVenta / (1 + (detalle.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero)
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = detalle.Excento
            dtrRowDetFactura.Item(7) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(8) = detalle.CostoInstalacion
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PrecioVenta
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Sub CargarDetalleProforma(proforma As Proforma)
        dtbDetalleFactura.Rows.Clear()
        For Each detalle As DetalleProforma In proforma.DetalleProforma
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = detalle.IdProducto
            dtrRowDetFactura.Item(1) = detalle.Producto.Codigo
            dtrRowDetFactura.Item(2) = detalle.Producto.Descripcion
            dtrRowDetFactura.Item(3) = detalle.Cantidad
            dtrRowDetFactura.Item(4) = Math.Round(detalle.PrecioVenta / (1 + (detalle.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero)
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = detalle.Excento
            dtrRowDetFactura.Item(7) = detalle.Producto.PrecioCosto
            dtrRowDetFactura.Item(8) = 0
            dtrRowDetFactura.Item(9) = detalle.PorcentajeIVA
            dtrRowDetFactura.Item(10) = detalle.PrecioVenta
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        Next
        grdDetalleFactura.Refresh()
    End Sub

    Private Async Function CargarDesglosePago(factura As Factura) As Task
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoFactura In factura.DesglosePagoFactura
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

    Private Sub CargarLineaDetalleFactura(producto As Producto, strDescripcion As String, dblCantidad As Double, dblPrecio As Double, dblCostoInstalacion As Double)
        Dim intIndice As Integer = dtbDetalleFactura.Rows.IndexOf(dtbDetalleFactura.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If intIndice >= 0 Then
            dtbDetalleFactura.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleFactura.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleFactura.Rows(intIndice).Item(3) += dblCantidad
            dtbDetalleFactura.Rows(intIndice).Item(4) = Math.Round(dblPrecio / (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
            dtbDetalleFactura.Rows(intIndice).Item(5) = dtbDetalleFactura.Rows(intIndice).Item(3) * dtbDetalleFactura.Rows(intIndice).Item(4)
            dtbDetalleFactura.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleFactura.Rows(intIndice).Item(7) = producto.PrecioCosto
            dtbDetalleFactura.Rows(intIndice).Item(8) = dblCostoInstalacion
            dtbDetalleFactura.Rows(intIndice).Item(9) = decTasaImpuesto
            dtbDetalleFactura.Rows(intIndice).Item(10) = dblPrecio
        Else
            dtrRowDetFactura = dtbDetalleFactura.NewRow
            dtrRowDetFactura.Item(0) = producto.IdProducto
            dtrRowDetFactura.Item(1) = producto.Codigo
            dtrRowDetFactura.Item(2) = strDescripcion
            dtrRowDetFactura.Item(3) = dblCantidad
            dtrRowDetFactura.Item(4) = Math.Round(dblPrecio / (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
            dtrRowDetFactura.Item(5) = dtrRowDetFactura.Item(3) * dtrRowDetFactura.Item(4)
            dtrRowDetFactura.Item(6) = decTasaImpuesto = 0
            dtrRowDetFactura.Item(7) = producto.PrecioCosto
            dtrRowDetFactura.Item(8) = dblCostoInstalacion
            dtrRowDetFactura.Item(9) = decTasaImpuesto
            dtrRowDetFactura.Item(10) = dblPrecio
            dtbDetalleFactura.Rows.Add(dtrRowDetFactura)
        End If
        grdDetalleFactura.Refresh()
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
        decExonerado = 0
        decExcento = 0
        decImpuesto = 0
        decTotalCosto = 0
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I = 0 To dtbDetalleFactura.Rows.Count - 1
            If dtbDetalleFactura.Rows(I).Item(6) = 0 Then
                Dim decTasaImpuesto As Decimal = dtbDetalleFactura.Rows(I).Item(9)
                If cliente.AplicaTasaDiferenciada Then decTasaImpuesto = cliente.ParametroImpuesto.TasaImpuesto
                Dim decPrecioGravado As Decimal = Math.Round(dtbDetalleFactura.Rows(I).Item(10) / (1 + (dtbDetalleFactura.Rows(I).Item(9) / 100)), 2, MidpointRounding.AwayFromZero)
                Dim decImpuestoProducto As Decimal = Math.Round(dtbDetalleFactura.Rows(I).Item(10) - decPrecioGravado, 2, MidpointRounding.AwayFromZero)
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = decPrecioGravado * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += decGravadoPorcentual * dtbDetalleFactura.Rows(I).Item(3)
                    decExonerado += (decPrecioGravado - decGravadoPorcentual) * dtbDetalleFactura.Rows(I).Item(3)
                    If Not cliente.AplicaTasaDiferenciada Then
                        decImpuesto += decImpuestoProducto * dtbDetalleFactura.Rows(I).Item(3)
                    Else
                        decImpuesto += Math.Round(decGravadoPorcentual * decTasaImpuesto / 100, 2, MidpointRounding.AwayFromZero) * dtbDetalleFactura.Rows(I).Item(3)
                    End If
                Else
                    decGravado += decPrecioGravado * dtbDetalleFactura.Rows(I).Item(3)
                    If Not cliente.AplicaTasaDiferenciada Then
                        decImpuesto += decImpuestoProducto * dtbDetalleFactura.Rows(I).Item(3)
                    Else
                        decImpuesto += Math.Round(decPrecioGravado * decTasaImpuesto / 100, 2, MidpointRounding.AwayFromZero) * dtbDetalleFactura.Rows(I).Item(3)
                    End If
                End If
            Else
                decExcento += Math.Round(dtbDetalleFactura.Rows(I).Item(5), 2, MidpointRounding.AwayFromZero)
            End If
            decTotalCosto += dtbDetalleFactura.Rows(I).Item(7)
        Next
        decSubTotal = decGravado + decExcento + decExonerado
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExonerado = Math.Round(decExonerado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotalCosto = Math.Round(decTotalCosto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2, MidpointRounding.AwayFromZero)
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
        cboCondicionVenta.ValueMember = "Id"
        cboCondicionVenta.DisplayMember = "Descripcion"
        cboCondicionVenta.DataSource = Await Puntoventa.ObtenerListadoCondicionVenta(FrmPrincipal.usuarioGlobal.Token)
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = Await Puntoventa.ObtenerListadoFormaPagoFactura(FrmPrincipal.usuarioGlobal.Token)
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = Await Puntoventa.ObtenerListadoTipoMoneda(FrmPrincipal.usuarioGlobal.Token)
        cboTipoExoneracion.ValueMember = "Id"
        cboTipoExoneracion.DisplayMember = "Descripcion"
        cboTipoExoneracion.DataSource = Await Puntoventa.ObtenerListadoTipoExoneracion(FrmPrincipal.usuarioGlobal.Token)
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
            txtUnidad.Text = IIf(producto.Tipo = 1, "UND", "SP")
            If FrmPrincipal.empresaGlobal.ModificaDescProducto = True Then
                txtDescripcion.Focus()
            Else
                txtPrecio.Focus()
            End If
        End If
    End Sub

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, 1, 0, True, FrmPrincipal.usuarioGlobal.Token)
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
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            txtFechaExoneracion.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            txtPorcentajeExoneracion.Text = "0"
            Await CargarCombos()
            Await CargarListaBancoAdquiriente()
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then Await CargarAutoCompletarProducto()
            If FrmPrincipal.empresaGlobal.ModificaDescProducto = True Then txtDescripcion.ReadOnly = False
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            grdDetalleFactura.DataSource = dtbDetalleFactura
            grdDesglosePago.DataSource = dtbDesglosePago
            bolInit = False
            txtCantidad.Text = "1"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            txtPagoDelCliente.Text = FormatNumber(0, 2)
            txtCambio.Text = FormatNumber(0, 2)
            txtIdOrdenServicio.Text = "0"
            txtIdProforma.Text = "0"
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
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
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
            shtConsecutivoPago = 0
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
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
        cboTipoExoneracion.SelectedIndex = 0
        txtNumDocExoneracion.Text = ""
        txtNombreInstExoneracion.Text = ""
        txtFechaExoneracion.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtPorcentajeExoneracion.Text = "0"
        dtbDetalleFactura.Rows.Clear()
        grdDetalleFactura.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtPagoDelCliente.Text = FormatNumber(0, 2)
        txtCambio.Text = FormatNumber(0, 2)
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
            gpbExoneracion.Enabled = False
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
        cboCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        txtTipoCambio.Text = "1"
        bolInit = False
        txtMontoPago.Text = ""
        shtConsecutivoPago = 0
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdFactura.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularFactura(txtIdFactura.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
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
                factura = Await Puntoventa.ObtenerFactura(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
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
                txtDocumento.Text = factura.TextoAdicional
                txtIdOrdenServicio.Text = factura.IdOrdenServicio
                txtIdProforma.Text = factura.IdProforma
                cboCondicionVenta.SelectedValue = factura.IdCondicionVenta
                txtPlazoCredito.Text = factura.PlazoCredito
                cboTipoExoneracion.SelectedValue = factura.IdTipoExoneracion
                txtNumDocExoneracion.Text = factura.NumDocExoneracion
                txtNombreInstExoneracion.Text = factura.NombreInstExoneracion
                txtFechaExoneracion.Text = factura.FechaEmisionDoc
                txtPorcentajeExoneracion.Text = factura.PorcentajeExoneracion
                vendedor = factura.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleFactura(factura)
                Await CargarDesglosePago(factura)
                CargarTotales()
                CargarTotalesPago()
                txtPagoDelCliente.Text = FormatNumber(factura.MontoPagado, 2)
                txtCambio.Text = FormatNumber(txtPagoDelCliente.Text - decTotal, 2)
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
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
                CargarDetalleOrdenServicio(ordenServicio)
                dtbDesglosePago.Rows.Clear()
                grdDesglosePago.Refresh()
                CargarTotales()
                CargarTotalesPago()
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
                txtIdProforma.Text = proforma.IdProforma
                CargarDetalleProforma(proforma)
                dtbDesglosePago.Rows.Clear()
                grdDesglosePago.Refresh()
                CargarTotales()
                CargarTotalesPago()
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
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
                gpbExoneracion.Enabled = True
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
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or decTotal = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtPagoDelCliente.Text = "" Then
            MessageBox.Show("Debe ingresar el monto con el que paga el cliente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtNumDocExoneracion.Text <> "" Or txtNombreInstExoneracion.Text <> "" Or CInt(txtPorcentajeExoneracion.Text) > 0 Then
            If txtNumDocExoneracion.Text = "" Or txtNombreInstExoneracion.Text = "" Or CInt(txtPorcentajeExoneracion.Text) = 0 Then
                MessageBox.Show("La información para la exoneración se encuentra incompleta. Por favor verifique los datos suministrados. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
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
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCliente = cliente.IdCliente,
                .IdCondicionVenta = cboCondicionVenta.SelectedValue,
                .PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text),
                .IdTipoExoneracion = cboTipoExoneracion.SelectedValue,
                .NumDocExoneracion = txtNumDocExoneracion.Text,
                .NombreInstExoneracion = txtNombreInstExoneracion.Text,
                .FechaEmisionDoc = txtFechaExoneracion.Text,
                .PorcentajeExoneracion = txtPorcentajeExoneracion.Text,
                .Fecha = Now(),
                .TextoAdicional = txtDocumento.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento + decExonerado,
                .Gravado = decGravado,
                .Descuento = 0,
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
                txtIdFactura.Text = Await Puntoventa.AgregarFactura(factura, FrmPrincipal.usuarioGlobal.Token)
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
                    .strDescuento = "0.00",
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
                documento = Await Puntoventa.ObtenerDocumentoElectronicoPorClave(factura.IdDocElectronico, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Dim datos As EstructuraPDF = New EstructuraPDF()
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
            datos.NombreEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
            datos.NombreComercial = FrmPrincipal.empresaGlobal.NombreComercial
            datos.Consecutivo = documento.Consecutivo
            datos.PlazoCredito = IIf(factura.PlazoCredito > 0, factura.PlazoCredito.ToString(), "")
            datos.Clave = documento.ClaveNumerica
            datos.CondicionVenta = ObtenerValoresCodificados.ObtenerCondicionDeVenta(factura.IdCondicionVenta)
            datos.Fecha = factura.Fecha.ToString("dd/MM/yyyy hh:mm:ss")
            Dim listaDesglosePago As IList(Of DesglosePagoFactura) = factura.DesglosePagoFactura
            datos.MedioPago = ObtenerValoresCodificados.ObtenerMedioDePago(listaDesglosePago(0).IdFormaPago)
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
            If factura.IdCliente > 1 Then
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
            For Each linea As DetalleFactura In factura.DetalleFactura
                Dim decTotalLinea As Decimal = linea.Cantidad * linea.PrecioVenta
                Dim detalle As EstructuraPDFDetalleServicio = New EstructuraPDFDetalleServicio With {
                    .Cantidad = linea.Cantidad,
                    .Codigo = linea.Producto.Codigo,
                    .Detalle = linea.Descripcion,
                    .PrecioUnitario = linea.PrecioVenta.ToString("N5", CultureInfo.InvariantCulture),
                    .TotalLinea = decTotalLinea.ToString("N5", CultureInfo.InvariantCulture)
                }
                datos.DetalleServicio.Add(detalle)
            Next
            If (factura.TextoAdicional IsNot Nothing) Then datos.OtrosTextos = factura.TextoAdicional
            datos.TotalGravado = decGravado.ToString("N5", CultureInfo.InvariantCulture)
            datos.TotalExonerado = decExonerado.ToString("N5", CultureInfo.InvariantCulture)
            datos.TotalExento = decExcento.ToString("N5", CultureInfo.InvariantCulture)
            datos.Descuento = "0.00000"
            datos.Impuesto = decImpuesto.ToString("N5", CultureInfo.InvariantCulture)

            datos.TotalGeneral = decTotal.ToString("N5", CultureInfo.InvariantCulture)
            datos.CodigoMoneda = IIf(factura.IdTipoMoneda = 1, "CRC", "USD")
            datos.TipoDeCambio = factura.TipoDeCambioDolar.ToString("N5", CultureInfo.InvariantCulture)
            Try
                Dim poweredByImage As Image = My.Resources.logo
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
        If producto Is Nothing Then
            If txtCodigo.Text <> "" Then
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                    CargarLineaDetalleFactura(producto, producto.Descripcion, txtCantidad.Text, decPrecioVenta, 0)
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
            CargarLineaDetalleFactura(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, 0)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtUnidad.Text = ""
            txtPrecio.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleFactura.Rows.Count > 0 Then
            dtbDetalleFactura.Rows.Remove(dtbDetalleFactura.Rows.Find(grdDetalleFactura.CurrentRow.Cells(0).Value))
            grdDetalleFactura.Refresh()
            CargarTotales()
            txtMontoPago.Text = txtTotal.Text
            txtPagoDelCliente.Text = txtTotal.Text
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub txtPorcentajeExoneracion_Validated(sender As Object, e As EventArgs) Handles txtPorcentajeExoneracion.Validated
        CargarTotales()
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

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio.KeyUp
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2)
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Tab Then
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) Handles txtCantidad.MouseDown, txtCodigo.MouseDown, txtDescripcion.MouseDown, txtPrecio.MouseDown
        sender.SelectAll()
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

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtPorcentajeExoneracion.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class