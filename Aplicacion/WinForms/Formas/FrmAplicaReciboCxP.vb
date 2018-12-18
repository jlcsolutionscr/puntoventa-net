Imports System.Collections.Generic
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmAplicaReciboCxP
#Region "Variables"
    Private I As Integer
    Private dblTotal As Decimal = 0
    Private dblTotalPago As Decimal = 0
    Private dblSaldoPorPagar As Decimal = 0
    Private dtbDesglosePago, dtbDesgloseCuenta As DataTable
    Private dtrRowDesglosePago, dtrRowDesgloseCuenta As DataRow
    Private bolInit As Boolean = True
    Private cuentaPorPagar As CuentaPorPagar
    Private proveedor As Proveedor
    Private movimiento As MovimientoCuentaPorPagar
    Private tipoMoneda As TipoMoneda
    Private desgloseMovimiento As DesgloseMovimientoCuentaPorPagar
    Private desglosePagoMovimiento As DesglosePagoMovimientoCuentaPorPagar
    Private reciboComprobante As ModuloImpresion.ClsRecibo
    Private desglosePagoImpresion As ModuloImpresion.clsDesgloseFormaPago
    Private arrDesglosePago, arrDesgloseMov As List(Of ModuloImpresion.clsDesgloseFormaPago)
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleMovimiento()
        dtbDesgloseCuenta = New DataTable()
        dtbDesgloseCuenta.Columns.Add("IDCXP", GetType(Int32))
        dtbDesgloseCuenta.Columns.Add("DESCRIPCION", GetType(String))
        dtbDesgloseCuenta.Columns.Add("MONTO", GetType(Decimal))
        dtbDesgloseCuenta.Columns.Add("DOCORIGINAL", GetType(Int32))
        dtbDesgloseCuenta.PrimaryKey = {dtbDesgloseCuenta.Columns(0)}

        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDBANCO", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("DOCUMENTO", GetType(String))
        dtbDesglosePago.Columns.Add("BENEFICIARIO", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCTIPOMONEDA", GetType(String))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("MONTOFORANEO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2), dtbDesglosePago.Columns(6)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDesgloseCuenta.Columns.Clear()
        grdDesgloseCuenta.AutoGenerateColumns = False

        Dim dvcIdCxC As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcMonto As New DataGridViewTextBoxColumn
        Dim dvcDocOriginal As New DataGridViewTextBoxColumn

        dvcIdCxC.DataPropertyName = "IDCXP"
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
        dvcMonto.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
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
        Dim dvcDocumento As New DataGridViewTextBoxColumn
        Dim dvcBeneficiario As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcDescTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcMontoForaneo As New DataGridViewTextBoxColumn

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
        dvcDescBanco.Width = 140
        dvcDescBanco.Visible = True
        dvcDescBanco.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcDocumento.DataPropertyName = "DOCUMENTO"
        dvcDocumento.HeaderText = "Doc."
        dvcDocumento.Width = 50
        dvcDocumento.Visible = True
        dvcDocumento.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDocumento)

        dvcBeneficiario.DataPropertyName = "BENEFICIARIO"
        dvcBeneficiario.HeaderText = "Beneficiario"
        dvcBeneficiario.Width = 150
        dvcBeneficiario.Visible = True
        dvcBeneficiario.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcBeneficiario)

        dvcIdTipoMoneda.DataPropertyName = "IDTIPOMONEDA"
        dvcIdTipoMoneda.HeaderText = "TipoMoneda"
        dvcIdTipoMoneda.Width = 0
        dvcIdTipoMoneda.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoMoneda)

        dvcDescTipoMoneda.DataPropertyName = "DESCTIPOMONEDA"
        dvcDescTipoMoneda.HeaderText = "Moneda"
        dvcDescTipoMoneda.Width = 70
        dvcDescTipoMoneda.Visible = True
        dvcDescTipoMoneda.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescTipoMoneda)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 100
        dvcMontoLocal.Visible = True
        dvcMontoLocal.ReadOnly = True
        dvcMontoLocal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcMontoForaneo.DataPropertyName = "MONTOFORANEO"
        dvcMontoForaneo.HeaderText = "Monto Exterior"
        dvcMontoForaneo.Width = 100
        dvcMontoForaneo.Visible = True
        dvcMontoForaneo.ReadOnly = True
        dvcMontoForaneo.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoForaneo)
    End Sub

    Private Sub CargarLineaDesgloseCuenta()
        Dim intIndice As Integer
        Dim objPkDesglose(0) As Object
        objPkDesglose(0) = cboCuentaPorPagar.SelectedValue
        If dtbDesgloseCuenta.Rows.Contains(objPkDesglose) Then
            intIndice = dtbDesgloseCuenta.Rows.IndexOf(dtbDesgloseCuenta.Rows.Find(objPkDesglose))
            dtbDesgloseCuenta.Rows(intIndice).Item(2) = CDbl(txtMontoAbono.Text)
        Else
            dtrRowDesgloseCuenta = dtbDesgloseCuenta.NewRow
            dtrRowDesgloseCuenta.Item(0) = cboCuentaPorPagar.SelectedValue
            dtrRowDesgloseCuenta.Item(1) = cboCuentaPorPagar.Text
            dtrRowDesgloseCuenta.Item(2) = CDbl(txtMontoAbono.Text)
            dtrRowDesgloseCuenta.Item(3) = cuentaPorPagar.NroDocOrig
            dtbDesgloseCuenta.Rows.Add(dtrRowDesgloseCuenta)
        End If
        grdDesgloseCuenta.Refresh()
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
            dtbDesglosePago.Rows(intIndice).Item(5) = txtNombreProveedor.Text
            dtbDesglosePago.Rows(intIndice).Item(8) = dblMontoLocal
            dtbDesglosePago.Rows(intIndice).Item(9) = dblMontoForaneo
        Else
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
            dtrRowDesglosePago.Item(1) = cboFormaPago.Text
            dtrRowDesglosePago.Item(2) = cboCuentaBanco.SelectedValue
            dtrRowDesglosePago.Item(3) = cboCuentaBanco.Text
            dtrRowDesglosePago.Item(4) = txtDocumento.Text
            dtrRowDesglosePago.Item(5) = txtNombreProveedor.Text
            dtrRowDesglosePago.Item(6) = cboTipoMoneda.SelectedValue
            dtrRowDesglosePago.Item(7) = cboTipoMoneda.Text
            dtrRowDesglosePago.Item(8) = dblMontoLocal
            dtrRowDesglosePago.Item(9) = dblMontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        End If
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotalesPago()
        dblTotal = 0
        dblTotalPago = 0
        For I = 0 To dtbDesgloseCuenta.Rows.Count - 1
            dblTotal = dblTotal + CDbl(dtbDesgloseCuenta.Rows(I).Item(2))
        Next
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            dblTotalPago = dblTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(8))
        Next
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        Try
            cboFormaPago.ValueMember = "IdFormaPago"
            cboFormaPago.DisplayMember = "Descripcion"
            'cboFormaPago.DataSource = servicioMantenimiento.ObtenerListaFormaPagoMovimientoCxP()
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
#End Region

#Region "Eventos Controles"
    Private Sub FrmAplicaReciboCxP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        CargarCombos()
        IniciaDetalleMovimiento()
        EstablecerPropiedadesDataGridView()
        grdDesgloseCuenta.DataSource = dtbDesgloseCuenta
        grdDesglosePago.DataSource = dtbDesglosePago
        bolInit = False
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        Try
            'tipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        txtMontoAbono.Text = FormatNumber(0, 2)
        txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioCompra, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        proveedor = Nothing
        txtNombreProveedor.Text = ""
        cboCuentaPorPagar.DataSource = Nothing
        Recibo.Text = ""
        txtDescripcion.Text = ""
        dtbDesgloseCuenta.Rows.Clear()
        grdDesgloseCuenta.Refresh()
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        dblTotal = 0
        txtMontoOriginal.Text = ""
        txtTotalAbonado.Text = ""
        txtMontoAbono.Text = FormatNumber(dblTotal, 2)
        txtMonto.Text = ""
        dblTotalPago = 0
        dblSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtMontoAbono.ReadOnly = False
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        CmdGuardar.Enabled = True
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        txtMonto.Text = ""
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If proveedor Is Nothing Or txtFecha.Text = "" Or Recibo.Text = "" Or txtDescripcion.Text = "" Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblTotal = 0 Then
            MessageBox.Show("Debe ingresar el monto del abono para guardar el movimiento.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblSaldoPorPagar > 0 Then
            MessageBox.Show("El total del desglose de pago del movimiento no es suficiente para cubrir el saldo por pagar actual.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago del movimiento es superior al saldo por pagar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If

        movimiento = New MovimientoCuentaPorPagar With {
            .IdEmpresa = cuentaPorPagar.IdEmpresa,
            .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
            .IdPropietario = proveedor.IdProveedor,
            .Tipo = StaticTipoAbono.AbonoEfectivo,
            .TipoPropietario = StaticTipoCuentaPorPagar.Proveedores,
            .Recibo = Recibo.Text,
            .Descripcion = txtDescripcion.Text,
            .Monto = dblTotal,
            .Fecha = Now()
        }
        For I = 0 To grdDesgloseCuenta.Rows.Count - 1
            desgloseMovimiento = New DesgloseMovimientoCuentaPorPagar With {
                .IdCxP = dtbDesgloseCuenta.Rows(I).Item(0),
                .Monto = dtbDesgloseCuenta.Rows(I).Item(2)
            }
            movimiento.DesgloseMovimientoCuentaPorPagar.Add(desgloseMovimiento)
        Next
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            desglosePagoMovimiento = New DesglosePagoMovimientoCuentaPorPagar With {
                .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                .NroMovimiento = dtbDesglosePago.Rows(I).Item(4),
                .Beneficiario = dtbDesglosePago.Rows(I).Item(5),
                .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(6),
                .MontoLocal = dtbDesglosePago.Rows(I).Item(8),
                .MontoForaneo = dtbDesglosePago.Rows(I).Item(9)
            }
            movimiento.DesglosePagoMovimientoCuentaPorPagar.Add(desglosePagoMovimiento)
        Next
        Try
            'servicioCuentaPorPagar.AplicarMovimientoCxP(movimiento)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CmdAgregar.Enabled = True
        CmdImprimir.Enabled = True
        CmdImprimir.Focus()
        txtMontoAbono.ReadOnly = True
        CmdGuardar.Enabled = False
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdImprimir.Click
        reciboComprobante = New ModuloImpresion.ClsRecibo With {
            .usuario = FrmMenuPrincipal.usuarioGlobal,
            .empresa = FrmMenuPrincipal.empresaGlobal,
            .equipo = FrmMenuPrincipal.equipoGlobal,
            .strConsecutivo = movimiento.IdMovCxP,
            .strRecibo = txtDocumento.Text,
            .strNombre = txtNombreProveedor.Text,
            .strFechaAbono = txtFecha.Text,
            .strTotalAbono = FormatNumber(dblTotal, 2)
        }
        arrDesgloseMov = New List(Of ModuloImpresion.clsDesgloseFormaPago)()
        For I = 0 To dtbDesgloseCuenta.Rows.Count - 1
            desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                .strDescripcion = dtbDesgloseCuenta.Rows(I).Item(3),
                .strMonto = FormatNumber(dtbDesgloseCuenta.Rows(I).Item(2), 2)
            }
            arrDesgloseMov.Add(desglosePagoImpresion)
        Next
        reciboComprobante.arrDesgloseMov = arrDesgloseMov
        arrDesglosePago = New List(Of ModuloImpresion.clsDesgloseFormaPago)()
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                .strDescripcion = dtbDesglosePago.Rows(I).Item(1),
                .strMonto = FormatNumber(dtbDesglosePago.Rows(I).Item(9), 2),
                .strNroDoc = dtbDesglosePago.Rows(I).Item(4)
            }
            arrDesglosePago.Add(desglosePagoImpresion)
        Next
        reciboComprobante.arrDesglosePago = arrDesglosePago
        Try
            ModuloImpresion.ImprimirReciboCxP(reciboComprobante)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub cboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboCuentaBanco.SelectedIndex = 0
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario And cboFormaPago.SelectedValue <> StaticFormaPago.Cheque Then
                cboCuentaBanco.Enabled = False
                txtDocumento.ReadOnly = True
                txtDocumento.Text = ""
                cboTipoMoneda.Enabled = True
            Else
                cboCuentaBanco.Enabled = True
                txtDocumento.ReadOnly = False
                cboTipoMoneda.Enabled = False
            End If
        End If
    End Sub

    Private Sub cboTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoMoneda.SelectedValueChanged
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            Dim tipoMoneda As TipoMoneda = Nothing
            Try
                'tipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioCompra, 2)
        End If
    End Sub

    Private Sub CmdInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If cboCuentaPorPagar.SelectedValue > 0 And txtMontoAbono.Text <> "" Then
            If CDbl(txtMontoAbono.Text) <= 0 Then
                MessageBox.Show("El monto del abono debe ser mayor a cero.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDesgloseCuenta()
            txtMontoAbono.Text = ""
            CargarTotalesPago()
            cboCuentaPorPagar.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        Dim objPkDesglose(0) As Object
        If dtbDesgloseCuenta.Rows.Count > 0 Then
            objPkDesglose(0) = grdDesgloseCuenta.CurrentRow.Cells(0).Value
            dtbDesgloseCuenta.Rows.Remove(dtbDesgloseCuenta.Rows.Find(objPkDesglose))
            grdDesgloseCuenta.Refresh()
            CargarTotalesPago()
            cboCuentaPorPagar.Focus()
        End If
    End Sub

    Private Sub CmdInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboCuentaBanco.SelectedValue > 0 And dblTotal > 0 And txtMonto.Text <> "" Then
            If dblSaldoPorPagar = 0 Then
                MessageBox.Show("El monto por cancelar ya se encuentra cubierto. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If cboFormaPago.SelectedValue = StaticFormaPago.Cheque Or cboFormaPago.SelectedValue = StaticFormaPago.TransferenciaDepositoBancario Then
                If txtDocumento.Text = "" Then
                    MessageBox.Show("Debe ingresar el nombre del beneficiario para esta forma de pago.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub CmdEliminarPago_Click(sender As Object, e As EventArgs) Handles btnEliminarPago.Click
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

    Private Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
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
            txtNombreProveedor.Text = proveedor.Nombre
            bolInit = True
            Try
                'cboCuentaPorPagar.DataSource = servicioCuentaPorPagar.ObtenerListaCuentasPorPagarPorPropietario(StaticTipoCuentaPorPagar.Proveedores, proveedor.IdProveedor)
                cboCuentaPorPagar.ValueMember = "IdCxP"
                cboCuentaPorPagar.DisplayMember = "DescReferencia"
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            cboCuentaPorPagar.SelectedValue = 0
            bolInit = False
            txtMontoOriginal.Text = ""
            txtTotalAbonado.Text = ""
            txtSaldoActual.Text = ""
            dtbDesglosePago.Rows.Clear()
            grdDesglosePago.Refresh()
            dblTotal = 0
            dblTotalPago = 0
            dblSaldoPorPagar = 0
            txtMontoAbono.Text = FormatNumber(dblTotal, 2)
            txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        End If
    End Sub

    Private Sub cboCuentaPorPagar_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCuentaPorPagar.SelectedValueChanged
        If Not bolInit And cboCuentaPorPagar.SelectedValue IsNot Nothing Then
            Try
                'cuentaPorPagar = servicioCuentaPorPagar.ObtenerCuentaPorPagar(cboCuentaPorPagar.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtMontoOriginal.Text = FormatNumber(cuentaPorPagar.Total, 2)
            txtTotalAbonado.Text = FormatNumber(cuentaPorPagar.Total - cuentaPorPagar.Saldo, 2)
            txtSaldoActual.Text = FormatNumber(cuentaPorPagar.Saldo, 2)
        End If
    End Sub

    Private Sub txtMonto_Validated(sender As Object, e As EventArgs) Handles txtMonto.Validated
        If txtMonto.Text <> "" Then txtMonto.Text = FormatNumber(txtMonto.Text, 2)
    End Sub

    Private Sub txtMontoAbono_Validated(sender As Object, e As EventArgs) Handles txtMontoAbono.Validated
        If txtMontoAbono.Text <> "" Then txtMontoAbono.Text = FormatNumber(txtMontoAbono.Text, 2)
    End Sub

    Private Sub txtMontoAbono_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMontoAbono.KeyPress, txtMonto.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class