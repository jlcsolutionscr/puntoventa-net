Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmCatalogoContable
#Region "Variables"
    Public intIdCuenta As Integer
    Private datos As CatalogoContable
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtNivel_1.Text = "" Then
            pCampo = "Nivel 1"
            Return False
        ElseIf txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        ElseIf cboTipoSaldo.Text = "" Then
            pCampo = "Tipo de Saldo"
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub CargarComboBox()
        'cboTipoSaldo.DataSource = servicioContabilidad.ObtenerTiposCuentaContable()
        cboTipoSaldo.ValueMember = "IdTipoCuenta"
        cboTipoSaldo.DisplayMember = "Descripcion"
        'cboCuentaGrupo.DataSource = servicioContabilidad.ObtenerListaCuentasPrimerOrden(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboCuentaGrupo.ValueMember = "IdCuenta"
        cboCuentaGrupo.DisplayMember = "Descripcion"
        'cboClaseCuenta.DataSource = servicioContabilidad.ObtenerClaseCuentaContable()
        cboClaseCuenta.ValueMember = "IdClaseCuenta"
        cboClaseCuenta.DisplayMember = "Descripcion"
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCatalogoContable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmCatalogoContable_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        If intIdCuenta > 0 Then
            Try
                CargarComboBox()
                'datos = servicioContabilidad.ObtenerCuentaContable(intIdCuenta)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("La cuenta contable seleccionada no existe", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdCuenta.Text = datos.IdCuenta
            txtNivel_1.Text = datos.Nivel_1
            txtNivel_2.Text = datos.Nivel_2
            txtNivel_3.Text = datos.Nivel_3
            txtNivel_4.Text = datos.Nivel_4
            txtNivel_5.Text = datos.Nivel_5
            txtNivel_6.Text = datos.Nivel_6
            txtNivel_7.Text = datos.Nivel_7
            cboCuentaGrupo.SelectedValue = IIf(datos.IdCuentaGrupo Is Nothing, 0, datos.IdCuentaGrupo)
            chkCuentaBalance.Checked = datos.EsCuentaBalance
            txtDescripcion.Text = datos.Descripcion
            cboTipoSaldo.SelectedValue = datos.IdTipoCuenta
            chkPermiteMovimiento.Checked = datos.PermiteMovimiento
            chkPermiteSobrejiro.Checked = datos.PermiteSobrejiro
            txtSaldoActual.Text = FormatNumber(datos.SaldoActual, 2)
            cboClaseCuenta.SelectedValue = datos.IdClaseCuenta
        Else
            datos = New CatalogoContable
            txtSaldoActual.Text = FormatNumber(0, 2)
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            btnGuardar.Enabled = True
            Exit Sub
        End If
        If datos.IdCuenta = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.Nivel_1 = txtNivel_1.Text
        datos.Nivel_2 = txtNivel_2.Text
        datos.Nivel_3 = txtNivel_3.Text
        datos.Nivel_4 = txtNivel_4.Text
        datos.Nivel_5 = txtNivel_5.Text
        datos.Nivel_6 = txtNivel_6.Text
        datos.Nivel_7 = txtNivel_7.Text
        datos.IdCuentaGrupo = cboCuentaGrupo.SelectedValue
        datos.EsCuentaBalance = chkCuentaBalance.Checked
        datos.Descripcion = txtDescripcion.Text
        datos.IdTipoCuenta = cboTipoSaldo.SelectedValue
        datos.PermiteMovimiento = chkPermiteMovimiento.Checked
        datos.PermiteSobrejiro = chkPermiteSobrejiro.Checked
        datos.SaldoActual = txtSaldoActual.Text
        datos.IdClaseCuenta = cboClaseCuenta.SelectedValue
        Try
            If datos.IdCuenta = 0 Then
                'servicioContabilidad.AgregarCuentaContable(datos)
            Else
                'servicioContabilidad.ActualizarCuentaContable(datos)
            End If
            MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        Close()
    End Sub

    Private Sub SaldoActual_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtSaldoActual.Validated
        If txtSaldoActual.Text = "" Then txtSaldoActual.Text = "0"
        txtSaldoActual.Text = FormatNumber(txtSaldoActual.Text, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtSaldoActual.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class