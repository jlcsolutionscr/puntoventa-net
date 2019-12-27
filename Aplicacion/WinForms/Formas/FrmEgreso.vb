Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmEgreso
#Region "Variables"
    Private dblTotal As Decimal = 0
    Private dblSaldoPorPagar As Decimal = 0
    Private dblTotalPago As Decimal = 0
    Private I As Short
    Private dtbDesglosePago As DataTable
    Private dtrRowDesglosePago As DataRow
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
    Private egreso As Egreso
    Private desglosePago As DesglosePagoEgreso
    Private comprobante As ModuloImpresion.ClsEgreso
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Sub IniciaDetallePago()
        dtbDesglosePago = New DataTable()
        dtbDesglosePago.Columns.Add("IDFORMAPAGO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCFORMAPAGO", GetType(String))
        dtbDesglosePago.Columns.Add("IDCUENTABANCO", GetType(Integer))
        dtbDesglosePago.Columns.Add("DESCBANCO", GetType(String))
        dtbDesglosePago.Columns.Add("NROCHEQUE", GetType(String))
        dtbDesglosePago.Columns.Add("IDTIPOMONEDA", GetType(Integer))
        dtbDesglosePago.Columns.Add("MONTOLOCAL", GetType(Decimal))
        dtbDesglosePago.Columns.Add("TIPODECAMBIO", GetType(Decimal))
        dtbDesglosePago.PrimaryKey = {dtbDesglosePago.Columns(0), dtbDesglosePago.Columns(2)}
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
        Dim dvcMontoLocal As New DataGridViewTextBoxColumn
        Dim dvcTipoCambio As New DataGridViewTextBoxColumn

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
        dvcDescBanco.Width = 240
        dvcDescBanco.Visible = True
        dvcDescBanco.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcDescBanco)

        dvcNroCheque.DataPropertyName = "NROCHEQUE"
        dvcNroCheque.HeaderText = "Referencia"
        dvcNroCheque.Width = 100
        dvcNroCheque.Visible = True
        dvcNroCheque.ReadOnly = True
        grdDesglosePago.Columns.Add(dvcNroCheque)

        dvcIdTipoMoneda.DataPropertyName = "IDTIPOMONEDA"
        dvcIdTipoMoneda.HeaderText = "TipoMoneda"
        dvcIdTipoMoneda.Width = 0
        dvcIdTipoMoneda.Visible = False
        grdDesglosePago.Columns.Add(dvcIdTipoMoneda)

        dvcMontoLocal.DataPropertyName = "MONTOLOCAL"
        dvcMontoLocal.HeaderText = "Monto Local"
        dvcMontoLocal.Width = 110
        dvcMontoLocal.Visible = True
        dvcMontoLocal.ReadOnly = True
        dvcMontoLocal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcMontoLocal)

        dvcTipoCambio.DataPropertyName = "TIPODECAMBIO"
        dvcTipoCambio.HeaderText = "Tipo Cambio"
        dvcTipoCambio.Width = 110
        dvcTipoCambio.Visible = True
        dvcTipoCambio.ReadOnly = True
        dvcTipoCambio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDesglosePago.Columns.Add(dvcTipoCambio)
    End Sub

    Private Sub CargarDesglosePago(ByVal egreso As Egreso)
        dtbDesglosePago.Rows.Clear()
        For Each detalle As DesglosePagoEgreso In egreso.DesglosePagoEgreso
            dtrRowDesglosePago = dtbDesglosePago.NewRow
            dtrRowDesglosePago.Item(0) = detalle.IdFormaPago
            dtrRowDesglosePago.Item(1) = detalle.FormaPago.Descripcion
            dtrRowDesglosePago.Item(2) = detalle.IdCuentaBanco
            dtrRowDesglosePago.Item(3) = detalle.DescripcionCuenta
            dtrRowDesglosePago.Item(4) = detalle.NroMovimiento
            dtrRowDesglosePago.Item(5) = detalle.IdTipoMoneda
            dtrRowDesglosePago.Item(6) = detalle.MontoLocal
            dtrRowDesglosePago.Item(7) = detalle.TipoDeCambio
            dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        Next
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarLineaDesglosePago()
        Dim objPkDesglose(2) As Object
        objPkDesglose(0) = cboFormaPago.SelectedValue
        objPkDesglose(1) = cboCuentaBanco.SelectedValue
        If dtbDesglosePago.Rows.Contains(objPkDesglose) Then
            MessageBox.Show("La forma de pago seleccionada ya fue agregada al detalle de pago.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        Dim decMontoPago, decTipoCambio As Decimal
        decMontoPago = CDbl(txtMontoPago.Text)
        decTipoCambio = IIf(FrmPrincipal.empresaGlobal.IdTipoMoneda = 1, 1, FrmPrincipal.decTipoCambioDolar)
        dtrRowDesglosePago = dtbDesglosePago.NewRow
        dtrRowDesglosePago.Item(0) = cboFormaPago.SelectedValue
        dtrRowDesglosePago.Item(1) = cboFormaPago.Text
        dtrRowDesglosePago.Item(2) = cboCuentaBanco.SelectedValue
        dtrRowDesglosePago.Item(3) = cboCuentaBanco.Text
        dtrRowDesglosePago.Item(4) = txtDocumento.Text
        dtrRowDesglosePago.Item(5) = FrmPrincipal.empresaGlobal.IdTipoMoneda
        dtrRowDesglosePago.Item(6) = decMontoPago
        dtrRowDesglosePago.Item(7) = decTipoCambio
        dtbDesglosePago.Rows.Add(dtrRowDesglosePago)
        grdDesglosePago.Refresh()
    End Sub

    Private Sub CargarTotalesPago()
        dblTotalPago = 0
        For I = 0 To dtbDesglosePago.Rows.Count - 1
            dblTotalPago = dblTotalPago + CDbl(dtbDesglosePago.Rows(I).Item(6))
        Next
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtMontoPago.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        cboCuentaEgreso.ValueMember = "Id"
        cboCuentaEgreso.DisplayMember = "Descripcion"
        cboCuentaEgreso.DataSource = Await Puntoventa.ObtenerListadoCuentasEgreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboFormaPago.ValueMember = "Id"
        cboFormaPago.DisplayMember = "Descripcion"
        cboFormaPago.DataSource = Await Puntoventa.ObtenerListadoFormaPagoEgreso(FrmPrincipal.usuarioGlobal.Token)
        cboCuentaBanco.ValueMember = "Id"
        cboCuentaBanco.DisplayMember = "Descripcion"
        cboCuentaBanco.DataSource = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmEgreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            IniciaDetallePago()
            EstablecerPropiedadesDataGridView()
            grdDesglosePago.DataSource = dtbDesglosePago
            cboFormaPago.SelectedValue = StaticFormaPago.Efectivo
            bolInit = False
            txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdEgreso.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboCuentaEgreso.SelectedIndex = 0
        txtBeneficiario.Text = ""
        txtDetalle.Text = ""
        txtTotal.Text = ""
        dtbDesglosePago.Rows.Clear()
        grdDesglosePago.Refresh()
        txtMontoPago.Text = ""
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
        txtMontoPago.Text = ""
    End Sub

    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdEgreso.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularEgreso(txtIdEgreso.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(CmdAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaEgreso()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                egreso = Await Puntoventa.ObtenerEgreso(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If egreso IsNot Nothing Then
                bolInit = True
                txtIdEgreso.Text = egreso.IdEgreso
                txtFecha.Text = egreso.Fecha.ToString("dd/MM/yyyy")
                cboCuentaEgreso.SelectedValue = egreso.IdCuenta
                txtBeneficiario.Text = egreso.Beneficiario
                txtDetalle.Text = egreso.Detalle
                txtTotal.Text = FormatNumber(egreso.Monto, 2)
                dblTotal = CDbl(txtTotal.Text)
                CargarDesglosePago(egreso)
                CargarTotalesPago()
                txtTotal.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                bolInit = False
            End If
        End If
    End Sub

    Private Async Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
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
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = Now(),
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
                    .Fecha = Now(),
                    .MontoLocal = dtbDesglosePago.Rows(I).Item(6),
                    .TipoDeCambio = dtbDesglosePago.Rows(I).Item(7)
                }
                egreso.DesglosePagoEgreso.Add(desglosePago)
            Next
            Try
                txtIdEgreso.Text = Await Puntoventa.AgregarEgreso(egreso, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdEgreso.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnInsertarPago.Enabled = False
        btnEliminarPago.Enabled = False
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = FrmPrincipal.usuarioGlobal.Modifica
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdEgreso.Text <> "" Then
            comprobante = New ModuloImpresion.ClsEgreso With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdEgreso.Text,
                .strFecha = txtFecha.Text,
                .strBeneficiario = txtBeneficiario.Text,
                .strConcepto = txtDetalle.Text,
                .strMonto = txtTotal.Text
            }
            arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)
            For I = 0 To dtbDesglosePago.Rows.Count - 1
                desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago With {
                    .strDescripcion = dtbDesglosePago.Rows(I).Item(1) & IIf(dtbDesglosePago.Rows(I).Item(4).Equals(String.Empty), String.Empty, " - " & dtbDesglosePago.Rows(I).Item(4)),
                    .strMonto = FormatNumber(dtbDesglosePago.Rows(I).Item(6)),
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
            'Else
            '    Dim strUsuario, strEmpresa As String
            '    Dim dtbDatos As DataTable
            '    Dim formReport As New frmRptViewer
            '    Dim reptVentas As New rptEgreso
            '    Try
            '        strUsuario = FrmMenuPrincipal.usuarioGlobal.CodigoUsuario
            '        strEmpresa = FrmMenuPrincipal.empresaGlobal.NombreEmpresa
            '        'dtbDatos = servicioReportes.ObtenerReporteEgreso(txtIdEgreso.Text)
            '    Catch ex As Exception
            '        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '        Exit Sub
            '    End Try
            '    reptVentas.SetDataSource(dtbDatos)
            '    reptVentas.SetParameterValue(0, strUsuario)
            '    reptVentas.SetParameterValue(1, strEmpresa)
            '    formReport.crtViewer.ReportSource = reptVentas
            '    formReport.ShowDialog()
        End If
    End Sub

    Private Sub CboFormaPago_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboFormaPago.SelectedValueChanged
        If Not bolInit And Not cboFormaPago.SelectedValue Is Nothing Then
            cboCuentaBanco.SelectedIndex = 0
            txtDocumento.Text = ""
            If cboFormaPago.SelectedValue <> StaticFormaPago.TransferenciaDepositoBancario And cboFormaPago.SelectedValue <> StaticFormaPago.Cheque And cboFormaPago.SelectedValue <> StaticFormaPago.Tarjeta Then
                cboCuentaBanco.Enabled = False
                txtDocumento.ReadOnly = True
                txtDocumento.Text = ""
            Else
                cboCuentaBanco.Enabled = True
                txtDocumento.ReadOnly = False
            End If
        End If
    End Sub

    Private Sub BtnInsertarPago_Click(sender As Object, e As EventArgs) Handles btnInsertarPago.Click
        If cboFormaPago.SelectedValue > 0 And cboCuentaBanco.SelectedValue > 0 And dblTotal > 0 And txtMontoPago.Text <> "" Then
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

    Private Sub TxtMonto_Validated(sender As Object, e As EventArgs) Handles txtMontoPago.Validated
        If txtMontoPago.Text <> "" Then txtMontoPago.Text = FormatNumber(txtMontoPago.Text, 2)
    End Sub

    Private Sub TxtTotal_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtTotal.Validated
        txtTotal.Text = FormatNumber(IIf(txtTotal.Text = "", 0, txtTotal.Text), 2)
        dblTotal = CDbl(txtTotal.Text)
        dblSaldoPorPagar = dblTotal - dblTotalPago
        txtMontoPago.Text = FormatNumber(dblSaldoPorPagar, 2)
        txtSaldoPorPagar.Text = FormatNumber(dblSaldoPorPagar, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtTotal.KeyPress, txtMontoPago.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class