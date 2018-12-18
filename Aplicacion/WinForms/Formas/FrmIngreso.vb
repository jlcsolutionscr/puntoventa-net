Imports System.Collections.Generic
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmIngreso
#Region "Variables"
    Private strUsuario, Valida, strEmpresa As String
    Private dtbDatos As DataTable
    Private dblTotal As Decimal = 0
    Private dblSaldoPorPagar As Decimal = 0
    Private dblTotalPago As Decimal = 0
    Private I As Short
    Private dtbDesglosePago As DataTable
    Private dtrRowDesglosePago As DataRow
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
    Private ingreso As Ingreso
    Private tipoMoneda As TipoMoneda
    Private desglosePago As DesglosePagoIngreso
    Private cuentaIngreso As CuentaIngreso
    Private comprobante As ModuloImpresion.ClsIngreso
    Private desglosePagoImpresion As ModuloImpresion.clsDesgloseFormaPago
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Sub IniciaDetallePago()
        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDCUENTABANCO", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("TIPOTARJETA", GetType(String))
        dtbDesglosePago.Columns.Add("NROMOVIMIENTO", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCTIPOMONEDA", GetType(String))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("MONTOFORANEO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2), dtbDesglosePago.Columns(6)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdFormaPago As New DataGridViewTextBoxColumn
        Dim dvcDescFormaPago As New DataGridViewTextBoxColumn
        Dim dvcIdCuentaBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcTipoTarjeta As New DataGridViewTextBoxColumn
        Dim dvcNroMovimiento As New DataGridViewTextBoxColumn
        Dim dvcIdTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcDescTipoMoneda As New DataGridViewTextBoxColumn
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcMontoForaneo As New DataGridViewTextBoxColumn

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
        dvcDescBanco.Width = 140
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

    Private Sub CargarDesglosePago(ByVal egreso As Ingreso)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoIngreso In egreso.DesglosePagoIngreso
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = detalle.FormaPago.Descripcion
            dtrRowDesglosePago.Item(2) = detalle.IdCuentaBanco
            If detalle.IdFormaPago = StaticFormaPago.Tarjeta Then
                Dim banco As BancoAdquiriente = Nothing 'servicioMantenimiento.ObtenerBancoAdquiriente(detalle.IdCuentaBanco)
                dtrRowDesglosePago.Item(3) = banco.Descripcion
            Else
                Dim banco As CuentaBanco = Nothing 'servicioAuxiliarBancario.ObtenerCuentaBanco(detalle.IdCuentaBanco)
                dtrRowDesglosePago.Item(3) = banco.Descripcion
            End If
            dtrRowDesglosePago.Item(4) = detalle.TipoTarjeta
            dtrRowDesglosePago.Item(5) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(6) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(7) = detalle.TipoMoneda.Descripcion
            dtrRowDesglosePago.Item(8) = detalle.MontoLocal
            dtrRowDesglosePago.Item(9) = detalle.MontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarLineaDesglosePago()
        Dim intIndice As Integer
        Dim objPkDesglose(2) As Object
        Dim dblMontoLocal, dblMontoForaneo As Decimal
        objPkDesglose(0) = cboFormaPago.SelectedValue
        objPkDesglose(1) = cboTipoBanco.SelectedValue
        objPkDesglose(2) = cboTipoMoneda.SelectedValue
        dblMontoForaneo = CDbl(txtMonto.Text)
        dblMontoLocal = CDbl(txtMonto.Text) * CDbl(txtTipoCambio.Text)
        If dblMontoLocal > dblSaldoPorPagar Then
            dblMontoLocal = dblSaldoPorPagar
            dblMontoForaneo = dblMontoLocal / CDbl(txtTipoCambio.Text)
        End If
        If dtbDesglosePago.Rows.Contains(objPkDesglose) Then
            intIndice = dtbDesglosePago.Rows.IndexOf(dtbDesglosePago.Rows.Find(objPkDesglose))
            dtbDesglosePago.Rows(intIndice).Item(4) = txtTipoTarjeta.Text
            dtbDesglosePago.Rows(intIndice).Item(5) = txtDocumento.Text
            dtbDesglosePago.Rows(intIndice).Item(8) = dblMontoLocal
            dtbDesglosePago.Rows(intIndice).Item(9) = dblMontoForaneo
        Else
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
            dtrRowDesglosePago.Item(1) = cboFormaPago.Text
            dtrRowDesglosePago.Item(2) = cboTipoBanco.SelectedValue
            dtrRowDesglosePago.Item(3) = cboTipoBanco.Text
            dtrRowDesglosePago.Item(4) = txtTipoTarjeta.Text
            dtrRowDesglosePago.Item(5) = txtDocumento.Text
            dtrRowDesglosePago.Item(6) = cboTipoMoneda.SelectedValue
            dtrRowDesglosePago.Item(7) = cboTipoMoneda.Text
            dtrRowDesglosePago.Item(8) = dblMontoLocal
            dtrRowDesglosePago.Item(9) = dblMontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        End If
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotalesPago()
        dblTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            dblTotalPago = dblTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(8))
        Next
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        Try
            cboCuentaIngreso.ValueMember = "IdCuenta"
            cboCuentaIngreso.DisplayMember = "Descripcion"
            'cboCuentaIngreso.DataSource = servicioIngresos.ObtenerListaCuentasIngreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboFormaPago.ValueMember = "IdFormaPago"
            cboFormaPago.DisplayMember = "Descripcion"
            'cboFormaPago.DataSource = servicioMantenimiento.ObtenerListaFormaPagoIngreso()
            cboTipoMoneda.ValueMember = "IdTipoMoneda"
            cboTipoMoneda.DisplayMember = "Descripcion"
            'cboTipoMoneda.DataSource = servicioMantenimiento.ObtenerListaTipoMoneda()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        cboCuentaIngreso.SelectedValue = 0
    End Sub

    Private Sub CargarListaBancoAdquiriente()
        'cboTipoBanco.DataSource = servicioMantenimiento.ObtenerListaBancoAdquiriente(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboTipoBanco.ValueMember = "IdBanco"
        cboTipoBanco.DisplayMember = "Codigo"
    End Sub

    Private Sub CargarListaCuentaBanco()
        'cboTipoBanco.DataSource = servicioAuxiliarBancario.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboTipoBanco.ValueMember = "IdCuenta"
        cboTipoBanco.DisplayMember = "Descripcion"
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmIngreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        CargarCombos()
        CargarListaBancoAdquiriente()
        IniciaDetallePago()
        EstablecerPropiedadesDataGridView()
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
        txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioVenta, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdIngreso.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        cboCuentaIngreso.SelectedValue = 0
        txtRecibidoDe.Text = ""
        txtDetalle.Text = ""
        txtTotal.Text = FormatNumber(0, 2)
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMonto.Text = ""
        dblSaldoPorPagar = 0
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        dblTotal = 0
        dblTotalPago = 0
        txtTotal.ReadOnly = False
        btnInsertarPago.Enabled = True
        btnEliminarPago.Enabled = True
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        txtMonto.Text = ""
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdIngreso.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioIngresos.AnularIngreso(txtIdIngreso.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(CmdAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaIngreso()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                'ingreso = servicioIngresos.ObtenerIngreso(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ingreso IsNot Nothing Then
                txtIdIngreso.Text = ingreso.IdIngreso
                txtFecha.Text = ingreso.Fecha
                cboCuentaIngreso.SelectedValue = ingreso.IdCuenta
                txtRecibidoDe.Text = ingreso.RecibidoDe
                txtDetalle.Text = ingreso.Detalle
                txtTotal.Text = FormatNumber(ingreso.Monto, 2)
                dblTotal = CDbl(txtTotal.Text)
                CargarDesglosePago(ingreso)
                CargarTotalesPago()
                txtTotal.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
            End If
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If cboCuentaIngreso.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar el tipo de cuenta por aplicar al ingreso.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtFecha.Text = "" Then
            MessageBox.Show("Debe ingresar la fecha del registro de ingresos.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtDetalle.Text = "" Then
            MessageBox.Show("Debe ingresar el detalle del registro de ingresos.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblTotal = 0 Then
            MessageBox.Show("Debe ingresar el monto del registro de ingresos", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        If txtIdIngreso.Text = "" Then
            ingreso = New Ingreso With {
                .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = txtFecha.Text,
                .IdCuenta = cboCuentaIngreso.SelectedValue,
                .RecibidoDe = txtRecibidoDe.Text,
                .Detalle = txtDetalle.Text,
                .Monto = CDbl(txtTotal.Text),
                .Nulo = False
            }
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoIngreso With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                    .TipoTarjeta = dtbDesglosePago.Rows(I).Item(4),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(5),
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(6),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(8),
                    .MontoForaneo = dtbDesglosePago.Rows(I).Item(9)
                }
                ingreso.DesglosePagoIngreso.Add(desglosePago)
            Next
            Try
                'ingreso = servicioIngresos.AgregarIngreso(ingreso)
                txtIdIngreso.Text = ingreso.IdIngreso
            Catch ex As Exception
                txtIdIngreso.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdIngreso.Text <> "" Then
            If FrmMenuPrincipal.equipoGlobal.UsaImpresoraImpacto Then
                comprobante = New ModuloImpresion.ClsIngreso With {
                    .usuario = FrmMenuPrincipal.usuarioGlobal,
                    .empresa = FrmMenuPrincipal.empresaGlobal,
                    .equipo = FrmMenuPrincipal.equipoGlobal,
                    .strId = txtIdIngreso.Text,
                    .strFecha = txtFecha.Text,
                    .strRecibidoDe = txtRecibidoDe.Text,
                    .strConcepto = txtDetalle.Text,
                    .strMonto = txtTotal.Text
                }
                arrDesglosePago = New List(Of ModuloImpresion.clsDesgloseFormaPago)
                For I = 0 To dtbDesglosePago.Rows.Count - 1
                    desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                        .strDescripcion = dtbDesglosePago.Rows(I).Item(1),
                        .strMonto = FormatNumber(dtbDesglosePago.Rows(I).Item(8)),
                        .strNroDoc = dtbDesglosePago.Rows(I).Item(5)
                    }
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                comprobante.arrDesglosePago = arrDesglosePago
                Try
                    ModuloImpresion.ImprimirIngreso(comprobante)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                Dim strUsuario, strEmpresa As String
                Dim dtbDatos As DataTable
                Dim formReport As New frmRptViewer
                Dim reptVentas As New rptIngreso
                Try
                    strUsuario = FrmMenuPrincipal.usuarioGlobal.CodigoUsuario
                    strEmpresa = FrmMenuPrincipal.empresaGlobal.NombreEmpresa
                    'dtbDatos = servicioReportes.ObtenerReporteIngreso(txtIdIngreso.Text)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                reptVentas.SetDataSource(dtbDatos)
                reptVentas.SetParameterValue(0, strUsuario)
                reptVentas.SetParameterValue(1, strEmpresa)
                formReport.crtViewer.ReportSource = reptVentas
                formReport.ShowDialog()
            End If
        End If
    End Sub

    Private Sub cboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboTipoBanco.SelectedIndex = 0
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
            txtTipoTarjeta.Text = ""
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario Then
                CargarListaBancoAdquiriente()
                cboTipoBanco.Width = 194
                lblBanco.Width = 194
                lblBanco.Text = "Banco Adquiriente"
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
                If cboFormaPago.SelectedValue = StaticFormaPago.Efectivo Then
                    cboTipoMoneda.Enabled = True
                Else
                    cboTipoMoneda.Enabled = False
                End If
            Else
                CargarListaCuentaBanco()
                cboTipoBanco.Width = 264
                lblBanco.Width = 264
                lblBanco.Text = "Cuenta Bancaria"
                cboTipoBanco.Enabled = True
                txtTipoTarjeta.ReadOnly = True
                txtTipoTarjeta.Visible = False
                lblTipoTarjeta.Visible = False
                txtDocumento.ReadOnly = False
                cboTipoMoneda.Enabled = False
            End If
        End If
    End Sub

    Private Sub cboTipoMoneda_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            Try
                'tipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioVenta, 2)
        End If
    End Sub

    Private Sub CmdInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboTipoBanco.SelectedValue > 0 And dblTotal > 0 And txtMonto.Text <> "" Then
            If dblSaldoPorPagar = 0 Then
                MessageBox.Show("El monto por cancelar ya se encuentra cubierto. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If cboFormaPago.SelectedValue = StaticFormaPago.Tarjeta Then
                If txtTipoTarjeta.Text = "" Or txtDocumento.Text = "" Then
                    MessageBox.Show("Debe ingresar el banco dueño de la tarjeta y el número de autorización del movimiento.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            ElseIf cboFormaPago.SelectedValue = StaticFormaPago.Cheque Or cboFormaPago.SelectedValue = StaticFormaPago.TransferenciaDepositoBancario Then
                If txtDocumento.Text = "" Then
                    MessageBox.Show("Debe ingresar el número de documento correspondiente al movimiento.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub txtMonto_Validated(sender As Object, e As EventArgs) Handles txtMonto.Validated
        If txtMonto.Text <> "" Then txtMonto.Text = FormatNumber(txtMonto.Text, 2)
    End Sub

    Private Sub txtTotal_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtTotal.Validated
        txtTotal.Text = FormatNumber(IIf(txtTotal.Text = "", 0, txtTotal.Text), 2)
        dblTotal = CDbl(txtTotal.Text)
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotal.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class