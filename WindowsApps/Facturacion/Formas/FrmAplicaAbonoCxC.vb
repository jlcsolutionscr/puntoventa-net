Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmAplicaAbonoCxC
#Region "Variables"
    Private I As Integer
    Private decTotal As Decimal = 0
    Private decTotalPago As Decimal = 0
    Private decSaldoPorPagar As Decimal = 0
    Private decPagoEfectivo, decPagoCliente As Decimal
    Private dtbDesglosePago, dtbDesgloseCuenta As DataTable
    Private dtrRowDesglosePago, dtrRowDesgloseCuenta As DataRow
    Private bolInit As Boolean = True
    Private cuentaPorCobrar As CuentaPorCobrar
    Private cliente As Cliente
    Private movimiento As MovimientoCuentaPorCobrar
    Private desgloseMovimiento As DesgloseMovimientoCuentaPorCobrar
    Private desglosePagoMovimiento As DesglosePagoMovimientoCuentaPorCobrar
    Private reciboComprobante As ModuloImpresion.ClsRecibo
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private arrDesglosePago, arrDesgloseMov As List(Of ModuloImpresion.ClsDesgloseFormaPago)
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleMovimiento()
        dtbDesgloseCuenta = New DataTable()
        dtbDesgloseCuenta.Columns.Add("IDCXC", GetType(Integer))
        dtbDesgloseCuenta.Columns.Add("DESCRIPCION", GetType(String))
        dtbDesgloseCuenta.Columns.Add("MONTO", GetType(Decimal))
        dtbDesgloseCuenta.Columns.Add("DOCORIGINAL", GetType(Integer))
        dtbDesgloseCuenta.PrimaryKey = {dtbDesgloseCuenta.Columns(0)}

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDBANCO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("TIPOTARJETA", GetType(String))
        dtbDesglosePago.Columns.Add("AUTORIZACION", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Integer))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("TIPODECAMBIO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDesgloseCuenta.Columns.Clear()
        grdDesgloseCuenta.AutoGenerateColumns = False

        Dim dvcIdCxC As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcMonto As New DataGridViewTextBoxColumn
        Dim dvcDocOriginal As New DataGridViewTextBoxColumn

        dvcIdCxC.DataPropertyName = "IDCXC"
        dvcIdCxC.HeaderText = "Id"
        dvcIdCxC.Width = 70
        dvcIdCxC.Visible = True
        dvcIdCxC.ReadOnly = True
        grdDesgloseCuenta.Columns.Add(dvcIdCxC)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Desripción"
        dvcDescripcion.Width = 560
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDesgloseCuenta.Columns.Add(dvcDescripcion)

        dvcMonto.DataPropertyName = "MONTO"
        dvcMonto.HeaderText = "Monto Abono"
        dvcMonto.Width = 100
        dvcMonto.Visible = True
        dvcMonto.ReadOnly = True
        dvcMonto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesgloseCuenta.Columns.Add(dvcMonto)

        dvcDocOriginal.DataPropertyName = "DOCORIGINAL"
        dvcDocOriginal.HeaderText = "DocOriginal"
        dvcDocOriginal.Width = 0
        dvcDocOriginal.Visible = False
        dvcDocOriginal.ReadOnly = True
        grdDesgloseCuenta.Columns.Add(dvcDocOriginal)

        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdTipoPago As New DataGridViewTextBoxColumn
        Dim dvcDescTipoPago As New DataGridViewTextBoxColumn
        Dim dvcIdBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcTipoTarjeta As New DataGridViewTextBoxColumn
        Dim dvcAutorizacion As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcDescTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcTipoCambio As New DataGridViewTextBoxColumn

        dvcIdTipoPago.DataPropertyName = "IDFORMAPAGO"
        dvcIdTipoPago.HeaderText = "Id"
        dvcIdTipoPago.Width = 0
        dvcIdTipoPago.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoPago)

        dvcDescTipoPago.DataPropertyName = "DESCFORMAPAGO"
        dvcDescTipoPago.HeaderText = "Forma de Pago"
        dvcDescTipoPago.Width = 120
        dvcDescTipoPago.Visible = True
        dvcDescTipoPago.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescTipoPago)

        dvcIdBanco.DataPropertyName = "IDBANCO"
        dvcIdBanco.HeaderText = "IdBanco"
        dvcIdBanco.Width = 0
        dvcIdBanco.Visible = False
        grdDesglosePago.Columns.Add(dvcIdBanco)

        dvcDescBanco.DataPropertyName = "DESCBANCO"
        dvcDescBanco.HeaderText = "Banco"
        dvcDescBanco.Width = 210
        dvcDescBanco.Visible = True
        dvcDescBanco.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcTipoTarjeta.DataPropertyName = "TIPOTARJETA"
        dvcTipoTarjeta.HeaderText = "Tipo Tarjeta"
        dvcTipoTarjeta.Width = 100
        dvcTipoTarjeta.Visible = True
        dvcTipoTarjeta.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcTipoTarjeta)

        dvcAutorizacion.DataPropertyName = "AUTORIZACION"
        dvcAutorizacion.HeaderText = "Movimiento #"
        dvcAutorizacion.Width = 100
        dvcAutorizacion.Visible = True
        dvcAutorizacion.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcAutorizacion)

        dvcIdTipoMoneda.DataPropertyName = "IDTIPOMONEDA"
        dvcIdTipoMoneda.HeaderText = "TipoMoneda"
        dvcIdTipoMoneda.Width = 0
        dvcIdTipoMoneda.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoMoneda)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 100
        dvcMontoLocal.Visible = True
        dvcMontoLocal.ReadOnly = True
        dvcMontoLocal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcTipoCambio.DataPropertyName = "TIPODECAMBIO"
        dvcTipoCambio.HeaderText = "Tipo Cambio"
        dvcTipoCambio.Width = 100
        dvcTipoCambio.Visible = True
        dvcTipoCambio.ReadOnly = True
        dvcTipoCambio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcTipoCambio)
    End Sub

    Private Sub CargarLineaDesgloseCuenta()
        Dim intIndice As Integer
        Dim objPkDesglose(0) As Object
        objPkDesglose(0) = cboCuentaPorCobrar.SelectedValue
        If dtbDesgloseCuenta.Rows.Contains(objPkDesglose) Then
            intIndice = dtbDesgloseCuenta.Rows.IndexOf(dtbDesgloseCuenta.Rows.Find(objPkDesglose))
            dtbDesgloseCuenta.Rows(intIndice).Item(2) = CDbl(txtMontoAbono.Text)
        Else
            dtrRowDesgloseCuenta = dtbDesgloseCuenta.NewRow
            dtrRowDesgloseCuenta.Item(0) = cboCuentaPorCobrar.SelectedValue
            dtrRowDesgloseCuenta.Item(1) = cboCuentaPorCobrar.Text
            dtrRowDesgloseCuenta.Item(2) = CDbl(txtMontoAbono.Text)
            dtrRowDesgloseCuenta.Item(3) = cuentaPorCobrar.NroDocOrig
            dtbDesgloseCuenta.Rows.Add(dtrRowDesgloseCuenta)
        End If
        grdDesgloseCuenta.Refresh()
    End Sub

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
        decTipoCambio = IIf(cuentaPorCobrar.IdTipoMoneda = 1, 1, FrmPrincipal.decTipoCambioDolar)
        dtrRowDesglosePago = dtbDesglosePago.NewRow
        dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
        dtrRowDesglosePago.Item(1) = cboFormaPago.Text
        dtrRowDesglosePago.Item(2) = cboTipoBanco.SelectedValue
        dtrRowDesglosePago.Item(3) = cboTipoBanco.Text
        dtrRowDesglosePago.Item(4) = txtTipoTarjeta.Text
        dtrRowDesglosePago.Item(5) = txtDocumento.Text
        dtrRowDesglosePago.Item(6) = cuentaPorCobrar.IdTipoMoneda
        dtrRowDesglosePago.Item(7) = decMontoPago
        dtrRowDesglosePago.Item(8) = decTipoCambio
        dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotalesPago()
        decTotal = 0
        decTotalPago = 0
        decPagoEfectivo = 0
        For I = 0 To dtbDesgloseCuenta.Rows.Count - 1
            decTotal = decTotal + CDbl(dtbDesgloseCuenta.Rows(I).Item(2))
        Next
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            If dtbDesglosePago.Rows(I).Item(0) = StaticFormaPago.Efectivo Then decPagoEfectivo = CDbl(dtbDesglosePago.Rows(I).Item(7))
            decTotalPago = decTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(7))
        Next
        decSaldoPorPagar = decTotal - decTotalPago
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        txtMontoPago.Text = FormatNumber(decSaldoPorPagar, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = Await Puntoventa.ObtenerListadoFormaPagoCliente(FrmPrincipal.usuarioGlobal.Token)
        cboTipoBanco.ValueMember = "Id"
        cboTipoBanco.DisplayMember = "Descripcion"
        cboTipoBanco.DataSource = Await Puntoventa.ObtenerListadoBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Async Function CargarListaBancoAdquiriente() As Task
        cboTipoBanco.DataSource = Await Puntoventa.ObtenerListadoBancoAdquiriente(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Async Function CargarListaCuentaBanco() As Task
        cboTipoBanco.DataSource = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmAplicaAbonoCxC_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
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

    Private Async Sub FrmAplicaAbonoCxC_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleMovimiento()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            grdDesgloseCuenta.DataSource = dtbDesgloseCuenta
            grdDesglosePago.DataSource = dtbDesglosePago
            bolInit = False
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            txtMontoAbono.Text = FormatNumber(0, 2)
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        Await CargarListaBancoAdquiriente()
        cliente = Nothing
        txtNombreCliente.Text = ""
        cboCuentaPorCobrar.DataSource = Nothing
        txtDescripcion.Text = ""
        dtbDesgloseCuenta.Rows.Clear()
        grdDesgloseCuenta.Refresh()
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        decTotal = 0
        txtMontoOriginal.Text = ""
        txtTotalAbonado.Text = ""
        txtMontoAbono.Text = FormatNumber(decTotal, 2)
        txtMontoPago.Text = ""
        decTotalPago = 0
        decSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        txtMontoAbono.ReadOnly = False
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        txtMontoPago.Text = ""
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Then
            MessageBox.Show("Debe seleccionar el cliente para poder guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decTotal = 0 Then
            MessageBox.Show("Debe ingresar el monto del abono para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar > 0 Then
            MessageBox.Show("El total del desglose de pago no es suficiente para cubrir el saldo por pagar actual.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        ElseIf decSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago del movimiento es superior al saldo por pagar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
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
        movimiento = New MovimientoCuentaPorCobrar With {
            .IdEmpresa = cuentaPorCobrar.IdEmpresa,
            .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
            .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
            .IdPropietario = cliente.IdCliente,
            .Tipo = StaticTipoAbono.AbonoEfectivo,
            .Descripcion = txtDescripcion.Text,
            .Monto = decTotal,
            .Fecha = Now()
        }
        For I = 0 To grdDesgloseCuenta.Rows.Count - 1
            desgloseMovimiento = New DesgloseMovimientoCuentaPorCobrar With {
                .IdCxC = dtbDesgloseCuenta.Rows(I).Item(0),
                .Monto = dtbDesgloseCuenta.Rows(I).Item(2)
            }
            movimiento.DesgloseMovimientoCuentaPorCobrar.Add(desgloseMovimiento)
        Next
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            desglosePagoMovimiento = New DesglosePagoMovimientoCuentaPorCobrar With {
                .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                .TipoTarjeta = dtbDesglosePago.Rows(I).Item(4),
                .NroMovimiento = dtbDesglosePago.Rows(I).Item(5),
                .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(6),
                .MontoLocal = dtbDesglosePago.Rows(I).Item(7),
                .TipoDeCambio = dtbDesglosePago.Rows(I).Item(8)
            }
            movimiento.DesglosePagoMovimientoCuentaPorCobrar.Add(desglosePagoMovimiento)
        Next
        Try
            Await Puntoventa.AplicarMovimientoCxC(movimiento, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
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
        btnAgregar.Enabled = True
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        txtMontoAbono.ReadOnly = True
        btnGuardar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnImprimir.Click
        reciboComprobante = New ModuloImpresion.ClsRecibo With {
            .usuario = FrmPrincipal.usuarioGlobal,
            .empresa = FrmPrincipal.empresaGlobal,
            .equipo = FrmPrincipal.equipoGlobal,
            .strConsecutivo = movimiento.IdMovCxC,
            .strNombre = txtNombreCliente.Text,
            .strFechaAbono = txtFecha.Text,
            .strTotalAbono = FormatNumber(decTotal, 2),
            .strPagoCon = FormatNumber(decPagoCliente, 2),
            .strCambio = FormatNumber(decPagoCliente - decPagoEfectivo, 2)
        }
        arrDesgloseMov = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
        For I = 0 To dtbDesgloseCuenta.Rows.Count - 1
            desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(3), FormatNumber(dtbDesglosePago.Rows(I).Item(2), 2))
            arrDesgloseMov.Add(desglosePagoImpresion)
        Next
        reciboComprobante.arrDesgloseMov = arrDesgloseMov
        arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(dtbDesglosePago.Rows(I).Item(1), FormatNumber(dtbDesglosePago.Rows(I).Item(7), 2))
            arrDesglosePago.Add(desglosePagoImpresion)
        Next
        reciboComprobante.arrDesglosePago = arrDesglosePago
        Try
            ModuloImpresion.ImprimirReciboCxC(reciboComprobante)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Async Sub cboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedIndexChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboTipoBanco.SelectedIndex = 0
            txtTipoTarjeta.Text = ""
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario Then
                Try
                    Await CargarListaBancoAdquiriente()
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                cboTipoBanco.Width = 371
                lblBanco.Width = 371
                lblBanco.Text = "Banco Adquiriente"
                lblAutorizacion.Text = "Autorización"
                txtTipoTarjeta.Visible = True
                lblTipoTarjeta.Visible = True
                If cboFormaPago.SelectedValue = StaticFormaPago.Tarjeta Then
                    cboTipoBanco.Enabled = True
                    txtTipoTarjeta.ReadOnly = False
                    txtDocumento.ReadOnly = False
                Else
                    cboTipoBanco.Enabled = False
                    txtTipoTarjeta.ReadOnly = True
                    txtDocumento.ReadOnly = True
                End If
            Else
                Try
                    Await CargarListaCuentaBanco()
                Catch ex As Exception
                    cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
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

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If cboCuentaPorCobrar.SelectedValue > 0 And txtMontoAbono.Text <> "" Then
            If CDbl(txtMontoAbono.Text) < 1 Then
                MessageBox.Show("El monto del abono debe ser mayor a cero.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDesgloseCuenta()
            txtMontoAbono.Text = ""
            CargarTotalesPago()
            cboCuentaPorCobrar.Focus()
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim objPkDesglose(0) As Object
        If dtbDesgloseCuenta.Rows.Count > 0 Then
            objPkDesglose(0) = grdDesgloseCuenta.CurrentRow.Cells(0).Value
            dtbDesgloseCuenta.Rows.Remove(dtbDesgloseCuenta.Rows.Find(objPkDesglose))
            grdDesgloseCuenta.Refresh()
            CargarTotalesPago()
            cboCuentaPorCobrar.Focus()
        End If
    End Sub

    Private Sub BtnInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboTipoBanco.SelectedValue > 0 And decTotal > 0 And txtMontoPago.Text <> "" Then
            If decSaldoPorPagar = 0 Then
                MessageBox.Show("El monto por cancelar ya se encuentra cubierto. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDesglosePago()
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
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

    Private Async Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            If FrmPrincipal.intBusqueda = StaticValoresPorDefecto.ClienteContado Then
                MessageBox.Show("El cliente indicado no corresponde a un cliente de crédito", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Try
                cliente = Await Puntoventa.ObtenerCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtNombreCliente.Text = cliente.Nombre
            bolInit = True
            Try
                cboCuentaPorCobrar.ValueMember = "Id"
                cboCuentaPorCobrar.DisplayMember = "Descripcion"
                cboCuentaPorCobrar.DataSource = Await Puntoventa.ObtenerListadoCuentasPorCobrar(FrmPrincipal.empresaGlobal.IdEmpresa, StaticTipoCuentaPorCobrar.Clientes, cliente.IdCliente, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            cboCuentaPorCobrar.SelectedValue = 0
            bolInit = False
            txtMontoOriginal.Text = ""
            txtTotalAbonado.Text = ""
            txtSaldoActual.Text = ""
            dtbDesglosePago.Rows.Clear()
            grdDesglosePago.Refresh()
            decTotal = 0
            decTotalPago = 0
            decSaldoPorPagar = 0
            txtMontoAbono.Text = FormatNumber(decTotal, 2)
            txtSaldoPorPagar.Text = FormatNumber(decSaldoPorPagar, 2)
        End If
    End Sub

    Private Async Sub cboCuentaPorCobrar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCuentaPorCobrar.SelectedIndexChanged
        If Not bolInit And cboCuentaPorCobrar.SelectedValue IsNot Nothing Then
            Try
                cuentaPorCobrar = Await Puntoventa.ObtenerCuentaPorCobrar(cboCuentaPorCobrar.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtMontoOriginal.Text = FormatNumber(cuentaPorCobrar.Total, 2)
            txtTotalAbonado.Text = FormatNumber(cuentaPorCobrar.Total - cuentaPorCobrar.Saldo, 2)
            txtSaldoActual.Text = FormatNumber(cuentaPorCobrar.Saldo, 2)
        End If
    End Sub

    Private Sub TxtMontoPago_Validated(sender As Object, e As EventArgs) Handles txtMontoPago.Validated
        If txtMontoPago.Text <> "" Then txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
    End Sub

    Private Sub TxtMontoPago_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtMontoPago.PreviewKeyDown
        If e.KeyCode = Keys.Enter And txtMontoPago.Text <> "" Then
            BtnInsertarPago_Click(btnInsertarPago, New EventArgs())
        End If
    End Sub

    Private Sub txtMontoAbono_Validated(sender As Object, e As EventArgs) Handles txtMontoAbono.Validated
        If txtMontoAbono.Text <> "" Then txtMontoAbono.Text = FormatNumber(txtMontoAbono.Text, 2)
    End Sub

    Private Sub txtMontoAbono_KeyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtMontoAbono.KeyPress, txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class