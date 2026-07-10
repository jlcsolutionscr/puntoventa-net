Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Constantes
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades

Public Class FrmAplicaAbonoCxC
#Region "Variables"
    Private decTotal As Decimal = 0
    Private decTotalPago As Decimal = 0
    Private decSaldoPorPagar As Decimal = 0
    Private decPagoEfectivo, decPagoCliente As Decimal
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetalleMovimiento As DataRow
    Private dtbDesglosePago As DataTable
    Private dtrRowDesglosePago As DataRow
    Private bolReady As Boolean = False
    Private cuentaPorCobrar As CuentaPorCobrar
    Private cliente As Cliente
    Private movimiento As MovimientoCuentaPorCobrar
    Private detalleMovimiento As DetalleMovimientoCuentaPorCobrar
    Private desglosePagoMovimiento As DesglosePagoMovimientoCuentaPorCobrar
    Private reciboComprobante As ModuloImpresion.ClsRecibo
    Private detalleMovImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleMovimiento()
        dtbDetalleMovimiento = New DataTable()
        dtbDetalleMovimiento.Columns.Add("IDCXC", GetType(Integer))
        dtbDetalleMovimiento.Columns.Add("PROPIETARIO", GetType(String))
        dtbDetalleMovimiento.Columns.Add("MONTOORIGINAL", GetType(Decimal))
        dtbDetalleMovimiento.Columns.Add("SALDOACTUAL", GetType(Decimal))
        dtbDetalleMovimiento.Columns.Add("MONTOABONO", GetType(Decimal))
        dtbDetalleMovimiento.PrimaryKey = {dtbDetalleMovimiento.Columns(0)}

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDBANCO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("TIPOTARJETA", GetType(String))
        dtbDesglosePago.Columns.Add("AUTORIZACION", GetType(String))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleMovimiento.Columns.Clear()
        grdDetalleMovimiento.AutoGenerateColumns = False

        Dim dvcIdCxC As New DataGridViewTextBoxColumn
        Dim dvcPropietario As New DataGridViewTextBoxColumn
        Dim dvcMontoOriginal As New DataGridViewTextBoxColumn
        Dim dvcSaldoActual As New DataGridViewTextBoxColumn
        Dim dvcMontoAbono As New DataGridViewTextBoxColumn
        Dim dvcSaldoPosterior As New DataGridViewTextBoxColumn

        dvcIdCxC.DataPropertyName = "IDCXC"
        dvcIdCxC.HeaderText = "Id CxC"
        dvcIdCxC.Width = 50
        dvcIdCxC.Visible = True
        grdDetalleMovimiento.Columns.Add(dvcIdCxC)

        dvcPropietario.DataPropertyName = "PROPIETARIO"
        dvcPropietario.HeaderText = "Propietario"
        dvcPropietario.Width = 320
        dvcPropietario.Visible = True
        grdDetalleMovimiento.Columns.Add(dvcPropietario)

        dvcMontoOriginal.DataPropertyName = "MONTOORIGINAL"
        dvcMontoOriginal.HeaderText = "Monto orig."
        dvcMontoOriginal.Width = 120
        dvcMontoOriginal.Visible = True
        dvcMontoOriginal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleMovimiento.Columns.Add(dvcMontoOriginal)

        dvcSaldoActual.DataPropertyName = "SALDOACTUAL"
        dvcSaldoActual.HeaderText = "Saldo act."
        dvcSaldoActual.Width = 120
        dvcSaldoActual.Visible = True
        dvcSaldoActual.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleMovimiento.Columns.Add(dvcSaldoActual)

        dvcMontoAbono.DataPropertyName = "MONTOABONO"
        dvcMontoAbono.HeaderText = "Abono"
        dvcMontoAbono.Width = 120
        dvcMontoAbono.Visible = True
        dvcMontoAbono.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleMovimiento.Columns.Add(dvcMontoAbono)

        For i As Integer = 0 To grdDetalleMovimiento.Columns.Count - 1
            grdDetalleMovimiento.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next

        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdTipoPago As New DataGridViewTextBoxColumn
        Dim dvcDescTipoPago As New DataGridViewTextBoxColumn
        Dim dvcIdBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcTipoTarjeta As New DataGridViewTextBoxColumn
        Dim dvcAutorizacion As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn

        dvcIdTipoPago.DataPropertyName = "IDFORMAPAGO"
        dvcIdTipoPago.HeaderText = "Id"
        dvcIdTipoPago.Width = 0
        dvcIdTipoPago.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoPago)

        dvcDescTipoPago.DataPropertyName = "DESCFORMAPAGO"
        dvcDescTipoPago.HeaderText = "Forma de Pago"
        dvcDescTipoPago.Width = 120
        dvcDescTipoPago.Visible = True
        grdDesglosePago.Columns.Add(dvcDescTipoPago)

        dvcIdBanco.DataPropertyName = "IDBANCO"
        dvcIdBanco.HeaderText = "IdBanco"
        dvcIdBanco.Width = 0
        dvcIdBanco.Visible = False
        grdDesglosePago.Columns.Add(dvcIdBanco)

        dvcDescBanco.DataPropertyName = "DESCBANCO"
        dvcDescBanco.HeaderText = "Banco"
        dvcDescBanco.Width = 310
        dvcDescBanco.Visible = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcTipoTarjeta.DataPropertyName = "TIPOTARJETA"
        dvcTipoTarjeta.HeaderText = "Tipo Tarjeta"
        dvcTipoTarjeta.Width = 100
        dvcTipoTarjeta.Visible = True
        grdDesglosePago.Columns.Add(dvcTipoTarjeta)

        dvcAutorizacion.DataPropertyName = "AUTORIZACION"
        dvcAutorizacion.HeaderText = "Movimiento #"
        dvcAutorizacion.Width = 100
        dvcAutorizacion.Visible = True
        grdDesglosePago.Columns.Add(dvcAutorizacion)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 100
        dvcMontoLocal.Visible = True
        dvcMontoLocal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        For i As Integer = 0 To grdDesglosePago.Columns.Count - 1
            grdDesglosePago.Columns(i).SortMode = DataGridViewColumnSortMode.NotSortable
        Next
    End Sub

    Private Async Function CargarLineasDeDetalle(movimiento As MovimientoCuentaPorCobrar) As Task
        decTotal = 0
        dtbDetalleMovimiento.Rows.Clear()
        For Each detalle As DetalleMovimientoCuentaPorCobrar In movimiento.DetalleMovimientoCuentaPorCobrar
            cliente = Await Puntoventa.ObtenerCliente(detalle.CuentaPorCobrar.IdPropietario, FrmPrincipal.usuarioGlobal.Token)
            decTotal = decTotal + detalle.Monto
            dtrRowDetalleMovimiento = dtbDetalleMovimiento.NewRow
            dtrRowDetalleMovimiento.Item(0) = detalle.IdCxC
            dtrRowDetalleMovimiento.Item(1) = cliente.Nombre
            dtrRowDetalleMovimiento.Item(2) = detalle.CuentaPorCobrar.Total
            dtrRowDetalleMovimiento.Item(3) = detalle.SaldoActual
            dtrRowDetalleMovimiento.Item(4) = detalle.Monto
            dtbDetalleMovimiento.Rows.Add(dtrRowDetalleMovimiento)
        Next
        grdDetalleMovimiento.Refresh()
    End Function

    Private Sub CargarDesglosePago(movimiento As MovimientoCuentaPorCobrar)
        decTotalPago = 0
        decPagoEfectivo = 0
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoMovimientoCuentaPorCobrar In movimiento.DesglosePagoMovimientoCuentaPorCobrar
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = FrmPrincipal.ObtenerDescripcionFormaPagoCliente(detalle.IdFormaPago)
            dtrRowDesglosePago.Item(2) = detalle.IdReferencia
            dtrRowDesglosePago.Item(3) = detalle.DescripcionCuenta
            dtrRowDesglosePago.Item(4) = detalle.TipoTarjeta
            dtrRowDesglosePago.Item(5) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(6) = detalle.MontoLocal
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
            If detalle.IdFormaPago = StaticFormaPago.Efectivo Then decPagoEfectivo = detalle.MontoLocal
            decTotalPago = decTotalPago + detalle.MontoLocal
        Next
        grdDesglosePago.Refresh()
    End Sub

    Private Sub AgregarLineaDetalle()
        If cuentaPorCobrar IsNot Nothing Then
            Dim objPkDesglose(0) As Object
            objPkDesglose(0) = cuentaPorCobrar.IdCxC
            If dtbDetalleMovimiento.Rows.Contains(objPkDesglose) Then
                MessageBox.Show("La cuenta por cobrar ya fue agregada al detalle del movimiento.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim decMontoAbono = CDec(txtMonto.Text)
            dtrRowDetalleMovimiento = dtbDetalleMovimiento.NewRow
            dtrRowDetalleMovimiento.Item(0) = cuentaPorCobrar.IdCxC
            dtrRowDetalleMovimiento.Item(1) = txtNombreCliente.Text
            dtrRowDetalleMovimiento.Item(2) = cuentaPorCobrar.Total
            dtrRowDetalleMovimiento.Item(3) = cuentaPorCobrar.Saldo
            dtrRowDetalleMovimiento.Item(4) = decMontoAbono
            dtbDetalleMovimiento.Rows.Add(dtrRowDetalleMovimiento)
            grdDetalleMovimiento.Refresh()
            cuentaPorCobrar = Nothing
            cliente = Nothing
            txtMontoOriginal.Text = ""
            txtSaldoActual.Text = ""
            txtMonto.Text = ""
            txtSaldoPosterior.Text = ""
            txtMontoTotal.Text = ""
            txtNombreCliente.Text = ""
        End If
    End Sub

    Private Sub AgregarLineaDesglosePago()
        Dim objPkDesglose(1) As Object
        Dim intTipoBanco As Integer = IIf(cboFormaPago.SelectedValue = StaticFormaPago.Efectivo, 0, cboTipoBanco.SelectedValue)
        objPkDesglose(0) = cboFormaPago.SelectedValue
        objPkDesglose(1) = intTipoBanco
        If dtbDesglosePago.Rows.Contains(objPkDesglose) Then
            MessageBox.Show("La forma de pago seleccionada ya fue agregada al detalle de pago.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim decMontoPago = CDbl(txtMontoPago.Text)
        dtrRowDesglosePago = dtbDesglosePago.NewRow
        dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
        dtrRowDesglosePago.Item(1) = cboFormaPago.Text
        dtrRowDesglosePago.Item(2) = intTipoBanco
        dtrRowDesglosePago.Item(3) = cboTipoBanco.Text
        dtrRowDesglosePago.Item(4) = txtTipoTarjeta.Text
        dtrRowDesglosePago.Item(5) = txtDocumento.Text
        dtrRowDesglosePago.Item(6) = decMontoPago
        dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotalesMovimiento()
        decTotal = 0
        For I As Short = 0 To dtbDetalleMovimiento.Rows.Count - 1
            decTotal = decTotal + CDbl(dtbDetalleMovimiento.Rows(I).Item(4))
        Next
        txtMontoTotal.Text = FormatNumber(decTotal, 2)
        decSaldoPorPagar = decTotal - decTotalPago
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarTotalesPago()
        decTotalPago = 0
        decPagoEfectivo = 0
        For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
            If dtbDesglosePago.Rows(I).Item(0) = StaticFormaPago.Efectivo Then decPagoEfectivo = CDbl(dtbDesglosePago.Rows(I).Item(6))
            decTotalPago = decTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(6))
        Next
        decSaldoPorPagar = decTotal - decTotalPago
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = FrmPrincipal.ObtenerListadoFormaPagoCliente()
        cboTipoBanco.ValueMember = "Id"
        cboTipoBanco.DisplayMember = "Descripcion"
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmAplicaAbonoCxC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmAplicaAbonoCxC_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            BtnGuardar_Click(btnGuardar, New EventArgs())
        ElseIf e.KeyCode = Keys.F11 And btnImprimir.Enabled Then
            BtnImprimir_Click(btnImprimir, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Sub FrmAplicaAbonoCxC_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleMovimiento()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaCostaRica()
            CargarCombos()
            decTotal = 0
            grdDetalleMovimiento.DataSource = dtbDetalleMovimiento
            grdDesglosePago.DataSource = dtbDesglosePago
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            bolReady = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        cuentaPorCobrar = Nothing
        cliente = Nothing
        txtIdMovCxC.Text = ""
        txtMontoOriginal.Text = ""
        txtSaldoActual.Text = ""
        txtMonto.Text = ""
        txtSaldoPosterior.Text = ""
        txtMontoTotal.Text = ""
        txtObservaciones.Text = ""
        decTotal = 0
        decTotalPago = 0
        decPagoEfectivo = 0
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(0, 2)
        txtMonto.ReadOnly = False
        txtObservaciones.ReadOnly = False
        txtMontoPago.ReadOnly = False
        btnBuscarCxC.Enabled = True
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        btnAnular.Enabled = False
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoBanco.DataSource = New List(Of LlaveDescripcion)
        cboTipoBanco.Width = 371
        lblBanco.Width = 371
        lblBanco.Text = "Banco Adquiriente"
        lblAutorizacion.Text = "Autorización"
        txtDocumento.Text = ""
        txtTipoTarjeta.Text = ""
        txtMontoPago.Text = ""
        dtbDetalleMovimiento.Rows.Clear()
        grdDetalleMovimiento.Refresh()
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If decTotal = 0 Then
            MessageBox.Show("Debe ingresar el monto del movimiento por aplicar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar > 0 Then
            MessageBox.Show("El total del desglose de pago no es suficiente para cubrir el saldo por pagar actual.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago del movimiento es superior al saldo por pagar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnGuardar.Enabled = False
        If FrmPrincipal.empresaGlobal.IngresaPagoCliente And decPagoEfectivo > 0 Then
            Dim formPagoEfectivo As New FrmPagoEfectivo()
            formPagoEfectivo.decTotalEfectivo = Puntoventa.ObtenerTotalRedondeado(FrmPrincipal.empresaGlobal.MontoRedondeoFactura, decPagoEfectivo)
            formPagoEfectivo.decPagoCliente = 0
            FrmPrincipal.intBusqueda = 0
            formPagoEfectivo.ShowDialog()
            If FrmPrincipal.intBusqueda > 0 Then
                decPagoCliente = FrmPrincipal.intBusqueda
            Else
                MessageBox.Show("Proceso cancelado por el usuario. Intente guardar de nuevo.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
        Else
            decPagoCliente = decPagoEfectivo
        End If
        movimiento = New MovimientoCuentaPorCobrar With {
            .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
            .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
            .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
            .Observaciones = txtObservaciones.Text,
            .Fecha = FrmPrincipal.ObtenerFechaCostaRica()
        }
        movimiento.DetalleMovimientoCuentaPorCobrar = New List(Of DetalleMovimientoCuentaPorCobrar)
        For I As Short = 0 To dtbDetalleMovimiento.Rows.Count - 1
            detalleMovimiento = New DetalleMovimientoCuentaPorCobrar With {
                .IdCxC = dtbDetalleMovimiento.Rows(I).Item(0),
                .SaldoActual = dtbDetalleMovimiento.Rows(I).Item(3),
                .Monto = dtbDetalleMovimiento.Rows(I).Item(4)
            }
            movimiento.DetalleMovimientoCuentaPorCobrar.Add(detalleMovimiento)
        Next
        movimiento.DesglosePagoMovimientoCuentaPorCobrar = New List(Of DesglosePagoMovimientoCuentaPorCobrar)
        For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
            desglosePagoMovimiento = New DesglosePagoMovimientoCuentaPorCobrar With {
                .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                .IdReferencia = IIf(dtbDesglosePago.Rows(I).Item(0) = StaticFormaPago.Efectivo, 0, dtbDesglosePago.Rows(I).Item(2)),
                .TipoTarjeta = dtbDesglosePago.Rows(I).Item(4),
                .NroMovimiento = dtbDesglosePago.Rows(I).Item(5),
                .MontoLocal = dtbDesglosePago.Rows(I).Item(6)
            }
            movimiento.DesglosePagoMovimientoCuentaPorCobrar.Add(desglosePagoMovimiento)
        Next
        Try
            txtIdMovCxC.Text = Await Puntoventa.AplicarMovimientoCxC(movimiento, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            btnGuardar.Enabled = True
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Transacción efectuada satisfactoriamente.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnAgregar.Enabled = True
        btnImprimir.Enabled = True
        btnAnular.Enabled = True
        btnImprimir.Focus()
        txtMonto.ReadOnly = True
        txtObservaciones.ReadOnly = True
        txtMontoPago.ReadOnly = True
        btnBuscarCxC.Enabled = False
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImprimir.Click
        reciboComprobante = New ModuloImpresion.ClsRecibo With {
            .usuario = FrmPrincipal.usuarioGlobal,
            .empresa = FrmPrincipal.empresaGlobal,
            .equipo = FrmPrincipal.equipoGlobal,
            .strConsecutivo = movimiento.IdMovCxC,
            .strFechaAbono = txtFecha.Text,
            .strTotalAbono = FormatNumber(decTotal, 2),
            .strPagoCon = FormatNumber(decPagoCliente, 2),
            .strCambio = FormatNumber(decPagoCliente - decPagoEfectivo, 2)
        }
        reciboComprobante.arrDesgloseMov = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
        For I As Short = 0 To dtbDetalleMovimiento.Rows.Count - 1
            detalleMovImpresion = New ModuloImpresion.ClsDesgloseFormaPago("Abono a CXC " & dtbDetalleMovimiento.Rows(I).Item(0), FormatNumber(dtbDetalleMovimiento.Rows(I).Item(4), 2))
            reciboComprobante.arrDesgloseMov.Add(detalleMovImpresion)
        Next
        reciboComprobante.arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
        For I As Short = 0 To dtbDesglosePago.Rows.Count - 1
            desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(1), FormatNumber(dtbDesglosePago.Rows(I).Item(6), 2))
            reciboComprobante.arrDesglosePago.Add(desglosePagoImpresion)
        Next
        Try
            ModuloImpresion.ImprimirReciboCxC(reciboComprobante)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Async Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaMovimientoCuentaPorCobrar()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                movimiento = Await Puntoventa.ObtenerMovimientoCxC(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If movimiento IsNot Nothing Then
                bolReady = False
                txtIdMovCxC.Text = movimiento.IdMovCxC
                txtFecha.Text = movimiento.Fecha
                txtObservaciones.Text = movimiento.Observaciones
                Await CargarLineasDeDetalle(movimiento)
                CargarDesglosePago(movimiento)
                decSaldoPorPagar = decTotal - decTotalPago
                txtMontoTotal.Text = FormatNumber(decTotal, 2)
                txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
                txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
                txtObservaciones.ReadOnly = True
                btnBuscarCxC.Enabled = False
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnInsertarPago.Enabled = False
                btnEliminarPago.Enabled = False
                btnImprimir.Enabled = True
                btnAnular.Enabled = Not movimiento.Nulo
                btnBuscarCxC.Enabled = False
                btnAnular.Enabled = Not movimiento.Nulo And FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
                bolReady = True
            Else
                MessageBox.Show("No existe registro de proforma asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub CboFormaPago_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If bolReady And cboFormaPago.SelectedValue IsNot Nothing Then
            txtTipoTarjeta.Text = ""
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue = StaticFormaPago.Efectivo Or cboFormaPago.SelectedValue = StaticFormaPago.Tarjeta Then
                If cboFormaPago.SelectedValue = StaticFormaPago.Tarjeta Then
                    Try
                        btnInsertarPago.Enabled = False
                        cboTipoBanco.DataSource = Await FrmPrincipal.CargarListaBancoAdquiriente()
                        cboTipoBanco.SelectedIndex = 0
                        btnInsertarPago.Enabled = True
                        cboTipoBanco.Enabled = True
                        txtTipoTarjeta.ReadOnly = False
                        txtDocumento.ReadOnly = False
                    Catch ex As Exception
                        btnInsertarPago.Enabled = True
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                Else
                    cboTipoBanco.DataSource = New List(Of LlaveDescripcion)
                    cboTipoBanco.Enabled = False
                    txtTipoTarjeta.ReadOnly = True
                    txtDocumento.ReadOnly = True
                End If
                cboTipoBanco.Width = 371
                lblBanco.Width = 371
                lblBanco.Text = "Banco Adquiriente"
                lblAutorizacion.Text = "Autorización"
                txtTipoTarjeta.Visible = True
                lblTipoTarjeta.Visible = True
            Else
                Try
                    btnInsertarPago.Enabled = False
                    cboTipoBanco.DataSource = Await FrmPrincipal.CargarListaCuentaBanco()
                Catch ex As Exception
                    btnInsertarPago.Enabled = True
                    cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                cboTipoBanco.SelectedIndex = 0
                btnInsertarPago.Enabled = True
                cboTipoBanco.Width = 441
                lblBanco.Width = 441
                lblBanco.Text = "Cuenta Bancaria"
                lblAutorizacion.Text = "Nro. Mov"
                cboTipoBanco.Enabled = True
                txtTipoTarjeta.ReadOnly = True
                txtTipoTarjeta.Visible = False
                lblTipoTarjeta.Visible = False
                txtDocumento.ReadOnly = False
            End If
        End If
    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If cuentaPorCobrar IsNot Nothing Then
            If txtMonto.Text = "" Then
                MessageBox.Show("Debe ingresar el monto del abono para la cuenta seleccionada", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If CDec(txtMonto.Text) > cuentaPorCobrar.Saldo Then
                MessageBox.Show("El monto del abono no puede ser mayor al saldo", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            AgregarLineaDetalle()
            CargarTotalesMovimiento()
            btnBuscarCxC.Focus()
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleMovimiento.CurrentRow IsNot Nothing And dtbDetalleMovimiento.Rows.Count > 0 Then
            dtbDetalleMovimiento.Rows.RemoveAt(grdDetalleMovimiento.CurrentRow.Index)
            grdDetalleMovimiento.Refresh()
            CargarTotalesMovimiento()
            btnBuscarCxC.Focus()
        End If
    End Sub

    Private Sub BtnInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If decTotal = 0 Then
            MessageBox.Show("Debe ingresar el monto del abono por aplicar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf cboFormaPago.SelectedValue <> StaticFormaPago.Efectivo And cboTipoBanco.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar la cuenta bancaria o tipo de tarjeta para esta forma de pago.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf decSaldoPorPagar = 0 Then
            MessageBox.Show("El monto por cancelar ya se encuentra cubierto en el detalle de pago.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        ElseIf txtMontoPago.Text = FormatNumber(0, 2) Then
            MessageBox.Show("El monto de pago debe ser mayor a 0.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        Else
            AgregarLineaDesglosePago()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            txtMontoPago.Text = ""
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Sub BtnEliminarPago_Click(sender As Object, e As EventArgs) Handles btnEliminarPago.Click
        If dtbDesglosePago.Rows.Count > 0 And grdDesglosePago.CurrentRow IsNot Nothing Then
            dtbDesglosePago.Rows.RemoveAt(grdDesglosePago.CurrentRow.Index)
            grdDesglosePago.Refresh()
            CargarTotalesPago()
            cboFormaPago.Focus()
        End If
    End Sub

    Private Async Sub BtnBuscarCxC_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCxC.Click
        Dim formBusqueda As New FrmBusquedaCuentaPorCobrar()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                cuentaPorCobrar = Await Puntoventa.ObtenerCuentaPorCobrar(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                cliente = Await Puntoventa.ObtenerCliente(cuentaPorCobrar.IdPropietario, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtMontoOriginal.Text = FormatNumber(cuentaPorCobrar.Total, 2)
            txtNombreCliente.Text = cliente.Nombre
            txtSaldoActual.Text = FormatNumber(cuentaPorCobrar.Saldo, 2)
            txtMonto.Text = FormatNumber(cuentaPorCobrar.Saldo, 2)
            txtSaldoPosterior.Text = FormatNumber(0, 2)
            txtMonto.Focus()
        End If
    End Sub

    Private Sub TxtMontoPago_Validated(sender As Object, e As EventArgs) Handles txtMontoPago.Validated
        If txtMontoPago.Text <> "" Then
            If CDbl(txtMontoPago.Text) > decSaldoPorPagar Then
                MessageBox.Show("El monto ingresado no puede sar mayor al saldo por pagar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
            Else
                txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
            End If
        Else
            txtMontoPago.Text = FormatNumber(0, 2)
        End If
    End Sub

    Private Sub TxtMontoPago_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtMontoPago.PreviewKeyDown
        If e.KeyCode = Keys.Enter And txtMontoPago.Text <> "" Then
            BtnInsertarPago_Click(btnInsertarPago, New EventArgs())
        End If
    End Sub

    Private Sub TxtMonto_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtMonto.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtMonto.Text = "" Then txtMonto.Text = FormatNumber(0, 2)
            If cuentaPorCobrar Is Nothing Then
                MessageBox.Show("Debe seleccionar el registro de la cuenta por cobrar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf txtMonto.Text = FormatNumber(0, 2) Then
                MessageBox.Show("El monto del abono debe ser mayor a 0", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            ElseIf CDec(txtMonto.Text) > cuentaPorCobrar.Saldo Then
                MessageBox.Show("El monto del abono no puede ser mayor al saldo", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtMonto.Text = FormatNumber(cuentaPorCobrar.Saldo, 2)
            End If
            txtSaldoPosterior.Text = FormatNumber(cuentaPorCobrar.Saldo - CDec(txtMonto.Text), 2)
        End If
    End Sub

    Private Async Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdMovCxC.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularMovimientoCxC(txtIdMovCxC.Text, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub Valida_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class