Imports System.Collections.Generic
Imports LeandroSoftware.Common.Dominio.Entidades

Public Class FrmAsientoContable
#Region "Variables"
    Private dblTotalDebito, dblTotalCredito As Decimal
    Private datFechaProc As Date
    Private dtbDetalleAsiento As DataTable
    Private dtrRowDetAsiento As DataRow
    Private arrDetalleAsiento As ArrayList
    Private usuario As Usuario
    Private asientoDiario As Asiento
    Private detalleAsiento As DetalleAsiento
    Private cuentaContable As CatalogoContable
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleAsiento()
        dtbDetalleAsiento = New DataTable()
        dtbDetalleAsiento.Columns.Add("IDCUENTA", GetType(Integer))
        dtbDetalleAsiento.Columns.Add("NIVELES", GetType(String))
        dtbDetalleAsiento.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleAsiento.Columns.Add("TIPOSALDO", GetType(String))
        dtbDetalleAsiento.Columns.Add("DEBITO", GetType(Decimal))
        dtbDetalleAsiento.Columns.Add("CREDITO", GetType(Decimal))
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleAsiento.Columns.Clear()
        grdDetalleAsiento.AutoGenerateColumns = False

        Dim dvcIdCuenta As New DataGridViewTextBoxColumn
        Dim dvcNiveles As New DataGridViewTextBoxColumn
        Dim dvcDescCuenta As New DataGridViewTextBoxColumn
        Dim dvcTipoSaldoCuenta As New DataGridViewTextBoxColumn
        Dim dvcDebito As New DataGridViewTextBoxColumn
        Dim dvcCredito As New DataGridViewTextBoxColumn

        dvcIdCuenta.DataPropertyName = "IDCUENTA"
        dvcIdCuenta.HeaderText = "Cuenta"
        dvcIdCuenta.Visible = False
        grdDetalleAsiento.Columns.Add(dvcIdCuenta)

        dvcNiveles.DataPropertyName = "NIVELES"
        dvcNiveles.HeaderText = "Cuenta"
        dvcNiveles.Width = 100
        grdDetalleAsiento.Columns.Add(dvcNiveles)

        dvcDescCuenta.DataPropertyName = "DESCRIPCION"
        dvcDescCuenta.HeaderText = "Descripción"
        dvcDescCuenta.Width = 300
        grdDetalleAsiento.Columns.Add(dvcDescCuenta)

        dvcTipoSaldoCuenta.DataPropertyName = "TIPOSALDO"
        dvcTipoSaldoCuenta.HeaderText = "TipoSaldo"
        dvcTipoSaldoCuenta.Visible = False
        grdDetalleAsiento.Columns.Add(dvcTipoSaldoCuenta)

        dvcDebito.DataPropertyName = "DEBITO"
        dvcDebito.HeaderText = "Débito"
        dvcDebito.Width = 90
        dvcDebito.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAsiento.Columns.Add(dvcDebito)

        dvcCredito.DataPropertyName = "CREDITO"
        dvcCredito.HeaderText = "Crédito"
        dvcCredito.Width = 90
        dvcCredito.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAsiento.Columns.Add(dvcCredito)
    End Sub

    Private Sub CargarDetalleAsiento(ByVal asiento As Asiento)
        dtbDetalleAsiento.Rows.Clear()
        For Each detalleAsiento In asiento.DetalleAsiento
            dtrRowDetAsiento = dtbDetalleAsiento.NewRow
            dtrRowDetAsiento.Item(0) = detalleAsiento.IdCuenta
            dtrRowDetAsiento.Item(1) = detalleAsiento.CatalogoContable.CuentaContable
            dtrRowDetAsiento.Item(2) = detalleAsiento.CatalogoContable.DescripcionCompleta
            dtrRowDetAsiento.Item(3) = detalleAsiento.CatalogoContable.TipoCuentaContable.TipoSaldo
            dtrRowDetAsiento.Item(4) = detalleAsiento.Debito
            dtrRowDetAsiento.Item(5) = detalleAsiento.Credito
            dtbDetalleAsiento.Rows.Add(dtrRowDetAsiento)
        Next
        grdDetalleAsiento.Refresh()
    End Sub

    Private Sub CargarLineaDetalleAsiento(ByVal cuentaContable As CatalogoContable)
        dtrRowDetAsiento = dtbDetalleAsiento.NewRow
        dtrRowDetAsiento.Item(0) = cuentaContable.IdCuenta
        dtrRowDetAsiento.Item(1) = cuentaContable.Nivel_1
        dtrRowDetAsiento.Item(2) = cuentaContable.DescripcionCompleta
        dtrRowDetAsiento.Item(3) = cuentaContable.TipoCuentaContable.TipoSaldo
        dtrRowDetAsiento.Item(4) = FormatNumber(txtDebito.Text, 2)
        dtrRowDetAsiento.Item(5) = FormatNumber(txtCredito.Text, 2)
        dtbDetalleAsiento.Rows.Add(dtrRowDetAsiento)
        grdDetalleAsiento.Refresh()
    End Sub

    Private Sub CargarTotales()
        dblTotalDebito = 0
        dblTotalCredito = 0
        For I As Short = 0 To dtbDetalleAsiento.Rows.Count - 1
            dblTotalDebito = dblTotalDebito + Decimal.Parse(grdDetalleAsiento.Rows(I).Cells(4).Value)
            dblTotalCredito = dblTotalCredito + Decimal.Parse(grdDetalleAsiento.Rows(I).Cells(5).Value)
        Next
        txtTotalDebito.Text = FormatNumber(dblTotalDebito, 2)
        txtTotalCredito.Text = FormatNumber(dblTotalCredito, 2)
    End Sub

    Private Sub CargarComboProducto()
        'cboCuentaContable.DataSource = servicioContabilidad.ObtenerListaCuentasParaMovimientos(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboCuentaContable.ValueMember = "IdCuenta"
        cboCuentaContable.DisplayMember = "DescripcionCompleta"
        cboCuentaContable.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmAsientoContable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdAsiento.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtDetalle.Text = ""
        txtDebito.Text = FormatNumber(0, 2)
        txtCredito.Text = FormatNumber(0, 2)
        dtbDetalleAsiento.Rows.Clear()
        grdDetalleAsiento.Refresh()
        txtTotalDebito.Text = FormatNumber(0, 2)
        txtTotalCredito.Text = FormatNumber(0, 2)
        cboCuentaContable.SelectedValue = 0
        cboCuentaContable.Text = ""
        cboCuentaContable.SelectedValue = 0
        cmdInsertar.Enabled = True
        cmdEliminar.Enabled = True
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        txtDetalle.Focus()
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdAsiento.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioContabilidad.AnularAsiento(txtIdAsiento.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(CmdAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaAsientoContable()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            'asientoDiario = servicioContabilidad.ObtenerAsiento(FrmMenuPrincipal.intBusqueda)
            If asientoDiario.IdAsiento > 0 Then
                txtIdAsiento.Text = asientoDiario.IdAsiento
                txtDetalle.Text = asientoDiario.Detalle
                txtFecha.Text = asientoDiario.Fecha
                txtTotalDebito.Text = FormatNumber(asientoDiario.TotalDebito, 2)
                txtTotalCredito.Text = FormatNumber(asientoDiario.TotalCredito, 2)
                CargarDetalleAsiento(asientoDiario)
                CargarTotales()
                cmdInsertar.Enabled = False
                cmdEliminar.Enabled = False
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.bolAnularTransacciones
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If txtFecha.Text <> "" And txtDetalle.Text <> "" Then
            asientoDiario = New Asiento With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .Detalle = txtDetalle.Text,
                .Fecha = Now(),
                .TotalDebito = 0,
                .TotalCredito = 0
            }
            asientoDiario.DetalleAsiento = New List(Of DetalleAsiento)
            For I As Short = 0 To dtbDetalleAsiento.Rows.Count - 1
                detalleAsiento = New DetalleAsiento With {
                    .Linea = I + 1,
                    .IdCuenta = dtbDetalleAsiento.Rows(I).Item(0),
                    .Debito = Decimal.Parse(dtbDetalleAsiento.Rows(I).Item(4)),
                    .Credito = Decimal.Parse(dtbDetalleAsiento.Rows(I).Item(5))
                }
                asientoDiario.DetalleAsiento.Add(detalleAsiento)
                If Decimal.Parse(dtbDetalleAsiento.Rows(I).Item(4)) > 0 Then
                    asientoDiario.TotalDebito += Decimal.Parse(dtbDetalleAsiento.Rows(I).Item(4))
                Else
                    asientoDiario.TotalCredito += Decimal.Parse(dtbDetalleAsiento.Rows(I).Item(5))
                End If
            Next
            Try
                If txtIdAsiento.Text = "" Then
                    'asientoDiario = servicioContabilidad.AgregarAsiento(asientoDiario)
                    txtIdAsiento.Text = asientoDiario.IdAsiento
                Else
                    'servicioContabilidad.ActualizarAsiento(asientoDiario)
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmdImprimir.Enabled = True
            CmdAgregar.Enabled = True
            CmdAnular.Enabled = FrmPrincipal.bolAnularTransacciones
            CmdImprimir.Focus()
            CmdGuardar.Enabled = False
            cmdInsertar.Enabled = False
            cmdEliminar.Enabled = False
        Else
            MessageBox.Show("La información del asiento no está completa.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        ' TODO Generar reporte del Asiento de Diario
    End Sub

    Private Sub CmdInsertar_Click(sender As Object, e As EventArgs) Handles cmdInsertar.Click
        If cboCuentaContable.SelectedValue <> Nothing And txtDebito.Text <> "" And txtCredito.Text <> "" Then
            If Decimal.Parse(txtDebito.Text) > 0 And Decimal.Parse(txtCredito.Text) > 0 Then
                MessageBox.Show("Debe ingresar el monto de la línea del asiento contable.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Exit Sub
            End If
            Try
                'cuentaContable = servicioContabilidad.ObtenerCuentaContable(cboCuentaContable.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarLineaDetalleAsiento(cuentaContable)
            CargarTotales()
            txtDebito.Text = FormatNumber(0, 2)
            txtCredito.Text = FormatNumber(0, 2)
            cboCuentaContable.SelectedValue = -1
            cboCuentaContable.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles cmdEliminar.Click
        If dtbDetalleAsiento.Rows.Count > 0 Then
            dtbDetalleAsiento.Rows.Remove(dtbDetalleAsiento.Rows.Find(grdDetalleAsiento.CurrentRow.Cells(0).Value))
            grdDetalleAsiento.Refresh()
            CargarTotales()
            cboCuentaContable.SelectedValue = -1
            cboCuentaContable.Focus()
        End If
    End Sub

    Private Sub FrmFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            CargarComboProducto()
            IniciaDetalleAsiento()
            EstablecerPropiedadesDataGridView()
            grdDetalleAsiento.DataSource = dtbDetalleAsiento
            grdDetalleAsiento.Refresh()
            txtDebito.Text = FormatNumber(0, 2)
            txtCredito.Text = FormatNumber(0, 2)
            txtTotalDebito.Text = FormatNumber(0, 2)
            txtTotalCredito.Text = FormatNumber(0, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub txtDebito_Leave(sender As Object, e As EventArgs) Handles txtDebito.Validated
        If txtDebito.Text = "" Then
            txtDebito.Text = FormatNumber(0, 2)
        Else
            txtDebito.Text = FormatNumber(txtDebito.Text, 2)
        End If
    End Sub

    Private Sub txtCredito_Leave(sender As Object, e As EventArgs) Handles txtCredito.Validated
        If txtCredito.Text = "" Then
            txtCredito.Text = FormatNumber(0, 2)
        Else
            txtCredito.Text = FormatNumber(txtCredito.Text, 2)
        End If
    End Sub

    Private Sub cboCodigo_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles cboCuentaContable.Validated
        If cboCuentaContable.Text <> "" And cboCuentaContable.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar la cuenta contable para continuar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboCuentaContable.Text = ""
            txtDebito.Text = FormatNumber(0, 2)
            txtCredito.Text = FormatNumber(0, 2)
            cboCuentaContable.Focus()
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtDebito.KeyPress, txtCredito.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class