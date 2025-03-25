Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmMovimientoBanco
#Region "Variables"
    Private movimiento As MovimientoBanco
#End Region

#Region "Métodos"
    Private Async Sub CargarCombos()
        cboIdCuenta.ValueMember = "Id"
        cboIdCuenta.DisplayMember = "Descripcion"
        cboIdCuenta.DataSource = Await Puntoventa.ObtenerListadoCuentasBanco(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        cboIdTipo.ValueMember = "Id"
        cboIdTipo.DisplayMember = "Descripcion"
        cboIdTipo.DataSource = Await Puntoventa.ObtenerListadoTipoMovimientoBanco(FrmPrincipal.usuarioGlobal.Token)
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmMovimientoBanco_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
        txtIdMov.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtNumero.Text = ""
        cboIdCuenta.Text = ""
        cboIdCuenta.SelectedValue = 0
        cboIdTipo.Text = ""
        txtBeneficiario.Text = ""
        txtMonto.Text = FormatNumber(0, 2)
        txtDescripcion.Text = ""
        txtMonto.ReadOnly = False
        cboIdCuenta.Enabled = True
        cboIdTipo.Enabled = True
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
    End Sub

    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdMov.Text <> "" And cboIdCuenta.SelectedValue <> Nothing And cboIdTipo.SelectedValue <> Nothing Then
            If MessageBox.Show("Desea anular este registro?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.ReversarMovimientoBanco(txtIdMov.Text, FrmPrincipal.usuarioGlobal.IdUsuario, "", FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(CmdAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaMovimientoBanco()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                movimiento = Await Puntoventa.ObtenerMovimientoBanco(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If movimiento IsNot Nothing Then
                txtIdMov.Text = movimiento.IdMov
                txtNumero.Text = movimiento.Numero
                txtFecha.Text = movimiento.Fecha
                cboIdCuenta.SelectedValue = movimiento.IdCuenta
                cboIdTipo.SelectedValue = movimiento.IdTipo
                txtBeneficiario.Text = movimiento.Beneficiario
                txtMonto.Text = FormatNumber(movimiento.Monto, 2)
                txtDescripcion.Text = movimiento.Descripcion
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.bolAnularTransacciones
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Async Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If txtNumero.Text <> "" And txtFecha.Text <> "" And cboIdCuenta.SelectedValue <> Nothing And cboIdTipo.SelectedValue <> Nothing And txtBeneficiario.Text <> "" And Decimal.Parse(txtMonto.Text) > 0 And txtDescripcion.Text <> "" Then
            movimiento = New MovimientoBanco With {
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdCuenta = cboIdCuenta.SelectedValue,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = FrmPrincipal.ObtenerFechaFormateada(Now()),
                .IdTipo = cboIdTipo.SelectedValue,
                .Numero = txtNumero.Text,
                .Beneficiario = txtBeneficiario.Text,
                .Monto = Decimal.Parse(txtMonto.Text),
                .Descripcion = txtDescripcion.Text
            }
            Try
                Dim strId As String = Await Puntoventa.AgregarMovimientoBanco(movimiento, FrmPrincipal.usuarioGlobal.Token)
                txtIdMov.Text = strId
            Catch ex As Exception
                txtIdMov.Text = ""
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmdImprimir.Enabled = True
            CmdAgregar.Enabled = True
            CmdAnular.Enabled = FrmPrincipal.bolAnularTransacciones
            CmdImprimir.Focus()
            CmdGuardar.Enabled = False
        Else
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdMov.Text <> "" Then
        End If
    End Sub

    Private Sub FrmMantDebCred_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            CargarCombos()
            txtMonto.Text = FormatNumber(0, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtMonto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub txtMonto_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtMonto.Validated
        txtMonto.Text = FormatNumber(IIf(txtMonto.Text <> "", txtMonto.Text, 0), 2)
    End Sub
#End Region
End Class