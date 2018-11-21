Imports System.Collections.Generic
Imports LeandroSoftware.Core.CommonTypes
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmEgreso
#Region "Variables"
    Private strUsuario, Valida, strEmpresa As String
    Private dtbDatos As DataTable
    Private servicioReportes As IReporteService
    Private dblTotal As Decimal = 0
    Private dblSaldoPorPagar As Decimal = 0
    Private dblTotalPago As Decimal = 0
    Private I As Short
    Private dtbDesglosePago As DataTable
    Private servicioEgresos As IEgresoService
    Private dtrRowDesglosePago As DataRow
    Private arrDesglosePago As List(Of ModuloImpresion.clsDesgloseFormaPago)
    Private servicioAuxiliarBancario As IBancaService
    Private servicioMantenimiento As IMantenimientoService
    Private egreso As Egreso
    Private tipoMoneda As TipoMoneda
    Private desglosePago As DesglosePagoEgreso
    Private cuentaEgreso As CuentaEgreso
    Private comprobante As ModuloImpresion.clsEgreso
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
        dtbDesglosePago.Columns.Add("NROCHEQUE", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Int32))
        dtbDesglosePago.Columns.Add("DESCTIPOMONEDA", GetType(String))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("MONTOFORANEO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2), dtbDesglosePago.Columns(5)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDesglosePago.Columns.Clear()
        grdDesglosePago.AutoGenerateColumns = False

        Dim dvcIdFormaPago As New DataGridViewTextBoxColumn
        Dim dvcDescFormaPago As New DataGridViewTextBoxColumn
        Dim dvcIdCuentaBanco As New DataGridViewTextBoxColumn
        Dim dvcDescBanco As New DataGridViewTextBoxColumn
        Dim dvcNroCheque As New DataGridViewTextBoxColumn
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
        dvcDescBanco.Width = 150
        dvcDescBanco.Visible = True
        dvcDescBanco.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcNroCheque.DataPropertyName = "NROCHEQUE"
        dvcNroCheque.HeaderText = "Nro. Cheque"
        dvcNroCheque.Width = 100
        dvcNroCheque.Visible = True
        dvcNroCheque.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcNroCheque)

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

    Private Sub CargarDesglosePago(ByVal egreso As Egreso)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoEgreso In egreso.DesglosePagoEgreso
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = detalle.FormaPago.Descripcion
            dtrRowDesglosePago.Item(2) = detalle.IdCuentaBanco
            dtrRowDesglosePago.Item(3) = detalle.CuentaBanco.Descripcion
            dtrRowDesglosePago.Item(4) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(5) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(6) = detalle.TipoMoneda.Descripcion
            dtrRowDesglosePago.Item(7) = detalle.MontoLocal
            dtrRowDesglosePago.Item(8) = detalle.MontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
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
            dtbDesglosePago.Rows(intIndice).Item(7) = dblMontoLocal
            dtbDesglosePago.Rows(intIndice).Item(8) = dblMontoForaneo
        Else
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
            dtrRowDesglosePago.Item(1) = cboFormaPago.Text
            dtrRowDesglosePago.Item(2) = cboCuentaBanco.SelectedValue
            dtrRowDesglosePago.Item(3) = cboCuentaBanco.Text
            dtrRowDesglosePago.Item(4) = txtDocumento.Text
            dtrRowDesglosePago.Item(5) = cboTipoMoneda.SelectedValue
            dtrRowDesglosePago.Item(6) = cboTipoMoneda.Text
            dtrRowDesglosePago.Item(7) = dblMontoLocal
            dtrRowDesglosePago.Item(8) = dblMontoForaneo
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        End If
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotalesPago()
        dblTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            dblTotalPago = dblTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(7))
        Next
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CargarCombos()
        Try
            cboCuentaEgreso.ValueMember = "IdCuenta"
            cboCuentaEgreso.DisplayMember = "Descripcion"
            cboCuentaEgreso.DataSource = servicioEgresos.ObtenerListaCuentasEgreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboFormaPago.ValueMember = "IdFormaPago"
            cboFormaPago.DisplayMember = "Descripcion"
            cboFormaPago.DataSource = servicioMantenimiento.ObtenerListaFormaPagoEgreso()
            cboCuentaBanco.ValueMember = "IdCuenta"
            cboCuentaBanco.DisplayMember = "Descripcion"
            cboCuentaBanco.DataSource = servicioAuxiliarBancario.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboTipoMoneda.ValueMember = "IdTipoMoneda"
            cboTipoMoneda.DisplayMember = "Descripcion"
            cboTipoMoneda.DataSource = servicioMantenimiento.ObtenerListaTipoMoneda()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        cboCuentaEgreso.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmEgreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioEgresos = FrmMenuPrincipal.unityContainer.Resolve(Of IEgresoService)()
            servicioAuxiliarBancario = FrmMenuPrincipal.unityContainer.Resolve(Of IBancaService)()
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
            servicioReportes = FrmMenuPrincipal.unityContainer.Resolve(Of IReporteService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        CargarCombos()
        IniciaDetallePago()
        EstablecerPropiedadesDataGridView()
        grdDesglosePago.DataSource = dtbDesglosePago
        bolInit = False
        cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
        cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
        Try
            tipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioVenta, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdEgreso.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        cboCuentaEgreso.SelectedValue = 0
        txtBeneficiario.Text = ""
        txtDetalle.Text = ""
        txtTotal.Text = ""
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
        If txtIdEgreso.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    servicioEgresos.AnularEgreso(txtIdEgreso.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
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
        Dim formBusqueda As New FrmBusquedaEgreso()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                egreso = servicioEgresos.ObtenerEgreso(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If egreso IsNot Nothing Then
                txtIdEgreso.Text = egreso.IdEgreso
                txtFecha.Text = egreso.Fecha
                cboCuentaEgreso.SelectedValue = egreso.IdCuenta
                txtBeneficiario.Text = egreso.Beneficiario
                txtDetalle.Text = egreso.Detalle
                txtTotal.Text = FormatNumber(egreso.Monto, 2)
                dblTotal = CDbl(txtTotal.Text)
                CargarDesglosePago(egreso)
                CargarTotalesPago()
                txtTotal.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
            End If
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If cboCuentaEgreso.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar el tipo de cuenta por aplicar al egreso.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtFecha.Text = "" Then
            MessageBox.Show("Debe ingresar la fecha del registro de egresos.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtDetalle.Text = "" Then
            MessageBox.Show("Debe ingresar el detalle del registro de egresos.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblTotal = 0 Then
            MessageBox.Show("Debe ingresar el monto del registro de egresos.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblSaldoPorPagar > 0 Then
            MessageBox.Show("El total del desglose de pago del egreso no es suficiente para cubrir el saldo por pagar actual.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If dblSaldoPorPagar < 0 Then
            MessageBox.Show("El total del desglose de pago del egreso es superior al saldo por pagar.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdEgreso.Text = "" Then
            egreso = New Egreso With {
                .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = txtFecha.Text,
                .IdCuenta = cboCuentaEgreso.SelectedValue,
                .Beneficiario = txtBeneficiario.Text,
                .Detalle = txtDetalle.Text,
                .Monto = CDbl(txtTotal.Text),
                .Nulo = False
            }
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePago = New DesglosePagoEgreso With {
                    .IdFormaPago = dtbDesglosePago.Rows(I).Item(0),
                    .IdCuentaBanco = dtbDesglosePago.Rows(I).Item(2),
                    .NroMovimiento = dtbDesglosePago.Rows(I).Item(4),
                    .Beneficiario = txtBeneficiario.Text,
                    .IdTipoMoneda = dtbDesglosePago.Rows(I).Item(5),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(7),
                    .MontoForaneo = dtbDesglosePago.Rows(I).Item(8)
                }
                egreso.DesglosePagoEgreso.Add(desglosePago)
            Next
            Try
                egreso = servicioEgresos.AgregarEgreso(egreso)
                txtIdEgreso.Text = egreso.IdEgreso
            Catch ex As Exception
                txtIdEgreso.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            egreso.Detalle = txtDetalle.Text
            Try
                servicioEgresos.ActualizarEgreso(egreso)
            Catch ex As Exception
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
        CmdGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdEgreso.Text <> "" Then
            If FrmMenuPrincipal.equipoGlobal.UsaImpresoraImpacto Then
                comprobante = New ModuloImpresion.clsEgreso With {
                    .usuario = FrmMenuPrincipal.usuarioGlobal,
                    .empresa = FrmMenuPrincipal.empresaGlobal,
                    .equipo = FrmMenuPrincipal.equipoGlobal,
                    .strId = txtIdEgreso.Text,
                    .strFecha = txtFecha.Text,
                    .strBeneficiario = txtBeneficiario.Text,
                    .strConcepto = txtDetalle.Text,
                    .strMonto = txtTotal.Text
                }
                arrDesglosePago = New List(Of ModuloImpresion.clsDesgloseFormaPago)
                For I = 0 To dtbDesglosePago.Rows.Count - 1
                    desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                        .strDescripcion = dtbDesglosePago.Rows(I).Item(1) & IIf(dtbDesglosePago.Rows(I).Item(4).Equals(String.Empty), String.Empty, " - " & dtbDesglosePago.Rows(I).Item(4)),
                        .strMonto = FormatNumber(dtbDesglosePago.Rows(I).Item(7)),
                        .strNroDoc = dtbDesglosePago.Rows(I).Item(4)
                    }
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                comprobante.arrDesglosePago = arrDesglosePago
                Try
                    ModuloImpresion.ImprimirEgreso(comprobante)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                Dim strUsuario, strEmpresa As String
                Dim dtbDatos As DataTable
                Dim formReport As New frmRptViewer
                Dim reptVentas As New rptEgreso
                Try
                    strUsuario = FrmMenuPrincipal.usuarioGlobal.CodigoUsuario
                    strEmpresa = FrmMenuPrincipal.empresaGlobal.NombreEmpresa
                    dtbDatos = servicioReportes.ObtenerReporteEgreso(txtIdEgreso.Text)
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
            cboCuentaBanco.SelectedIndex = 0
            cboTipoMoneda.SelectedValue = StaticValoresPorDefecto.MonedaDelSistema
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario And cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.Tarjeta Then
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
            Try
                TipoMoneda = servicioMantenimiento.ObtenerTipoMoneda(cboTipoMoneda.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtTipoCambio.Text = FormatNumber(tipoMoneda.TipoCambioVenta, 2)
        End If
    End Sub

    Private Sub btnInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboTipoMoneda.SelectedValue > 0 And cboCuentaBanco.SelectedValue > 0 And dblTotal > 0 And txtMonto.Text <> "" Then
            If dblSaldoPorPagar = 0 Then
                MessageBox.Show("El monto de por cancelar ya se encuentra cubierto. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If cboFormaPago.SelectedValue = StaticFormaPago.Cheque Then
                If txtDocumento.Text = "" Then
                    MessageBox.Show("Debe ingresar el número de cheque.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Sub btnEliminarPago_Click(sender As Object, e As EventArgs) Handles btnEliminarPago.Click
        Dim objPkDesglose(2) As Object
        If dtbDesglosePago.Rows.Count > 0 Then
            objPkDesglose(0) = grdDesglosePago.CurrentRow.Cells(0).Value
            objPkDesglose(1) = grdDesglosePago.CurrentRow.Cells(2).Value
            objPkDesglose(2) = grdDesglosePago.CurrentRow.Cells(5).Value
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

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtTotal.KeyPress, txtMonto.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class