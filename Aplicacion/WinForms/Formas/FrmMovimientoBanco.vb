Imports System.Collections.Generic
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmMovimientoBanco
#Region "Variables"
    Private movimiento As MovimientoBanco
    Private listaMovimientos As IEnumerable(Of MovimientoBanco)
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        Try
            cboIdCuenta.ValueMember = "IdCuenta"
            cboIdCuenta.DisplayMember = "Descripcion"
            'cboIdCuenta.DataSource = servicioAuxiliarBancario.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboIdTipo.ValueMember = "IdTipoMov"
            cboIdTipo.DisplayMember = "Descripcion"
            'cboIdTipo.DataSource = servicioAuxiliarBancario.ObtenerTipoMovimientoBanco()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        cboIdCuenta.SelectedValue = 0
        cboIdTipo.SelectedValue = 0
    End Sub
#End Region

#Region "Eventos Controles"
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

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdMov.Text <> "" And cboIdCuenta.SelectedValue <> Nothing And cboIdTipo.SelectedValue <> Nothing Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioAuxiliarBancario.AnularMovimientoBanco(txtIdMov.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
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
        Dim formBusqueda As New FrmBusquedaMovimientoBanco()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'movimiento = servicioAuxiliarBancario.ObtenerMovimientoBanco(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                txtMonto.ReadOnly = True
                cboIdCuenta.Enabled = False
                cboIdTipo.Enabled = False
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = FrmPrincipal.usuarioGlobal.Modifica
            End If
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If txtNumero.Text <> "" And txtFecha.Text <> "" And cboIdCuenta.SelectedValue <> Nothing And cboIdTipo.SelectedValue <> Nothing And txtBeneficiario.Text <> "" And CDbl(txtMonto.Text) > 0 And txtDescripcion.Text <> "" Then
            If txtIdMov.Text = "" Then
                movimiento = New MovimientoBanco With {
                    .IdCuenta = cboIdCuenta.SelectedValue,
                    .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                    .Fecha = FrmPrincipal.ObtenerFechaFormateada(Now()),
                    .IdTipo = cboIdTipo.SelectedValue,
                    .Numero = txtNumero.Text,
                    .Beneficiario = txtBeneficiario.Text,
                    .Monto = CDbl(txtMonto.Text),
                    .Descripcion = txtDescripcion.Text
                }
                Try
                    'movimiento = servicioAuxiliarBancario.AgregarMovimientoBanco(movimiento)
                    txtIdMov.Text = movimiento.IdMov
                Catch ex As Exception
                    txtIdMov.Text = ""
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                movimiento.Numero = txtNumero.Text
                movimiento.Beneficiario = txtBeneficiario.Text
                movimiento.Descripcion = txtDescripcion.Text
                Try
                    'servicioAuxiliarBancario.ActualizarMovimientoBanco(movimiento)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            CmdImprimir.Enabled = True
            CmdAgregar.Enabled = True
            CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
            CmdImprimir.Focus()
            CmdGuardar.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        Else
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdMov.Text <> "" Then
        End If
    End Sub

    Private Sub FrmMantDebCred_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        CargarCombos()
        txtMonto.Text = FormatNumber(0, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtMonto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub txtMonto_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtMonto.Validated
        txtMonto.Text = FormatNumber(IIf(txtMonto.Text <> "", txtMonto.Text, 0), 2)
    End Sub
#End Region
End Class