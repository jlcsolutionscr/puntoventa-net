Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmEgreso
#Region "Variables"
    Private egreso As Egreso
    Private comprobante As ModuloImpresion.ClsEgreso
#End Region

#Region "M�todos"
    Private Async Function CargarCombos() As Task
        cboCuentaEgreso.ValueMember = "Id"
        cboCuentaEgreso.DisplayMember = "Descripcion"
        cboCuentaEgreso.DataSource = Await Puntoventa.ObtenerListadoCuentasEgreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmEgreso_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmEgreso_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
            BtnBuscar_Click(btnBuscar, New EventArgs())
        ElseIf e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            BtnGuardar_Click(btnGuardar, New EventArgs())
        ElseIf e.KeyCode = Keys.F11 And btnImprimir.Enabled Then
            BtnImprimir_Click(btnImprimir, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Async Sub FrmEgreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdEgreso.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboCuentaEgreso.SelectedIndex = 0
        txtBeneficiario.Text = ""
        txtDetalle.Text = ""
        txtMonto.Text = ""
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdEgreso.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularEgreso(txtIdEgreso.Text, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacci�n procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaEgreso()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                egreso = Await Puntoventa.ObtenerEgreso(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If egreso IsNot Nothing Then
                txtIdEgreso.Text = egreso.IdEgreso
                txtFecha.Text = egreso.Fecha.ToString("dd/MM/yyyy")
                cboCuentaEgreso.SelectedValue = egreso.IdCuenta
                txtBeneficiario.Text = egreso.Beneficiario
                txtDetalle.Text = egreso.Detalle
                txtMonto.Text = FormatNumber(egreso.Monto, 2)
                txtMonto.ReadOnly = True
                btnImprimir.Enabled = True
                btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cboCuentaEgreso.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar el tipo de cuenta por aplicar al egreso.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtFecha.Text = "" Then
            MessageBox.Show("Debe ingresar la fecha del registro de egresos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtDetalle.Text = "" Then
            MessageBox.Show("Debe ingresar el detalle del registro de egresos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtMonto.Text = "" Then
            MessageBox.Show("Debe ingresar el monto del registro de egresos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                .Monto = CDbl(txtMonto.Text),
                .Nulo = False
            }
            Try
                txtIdEgreso.Text = Await Puntoventa.AgregarEgreso(egreso, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdEgreso.Text = ""
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacci�n efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        btnImprimir.Focus()
        btnGuardar.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdEgreso.Text <> "" Then
            comprobante = New ModuloImpresion.ClsEgreso With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdEgreso.Text,
                .strFecha = txtFecha.Text,
                .strBeneficiario = txtBeneficiario.Text,
                .strConcepto = txtDetalle.Text,
                .strMonto = txtMonto.Text
            }
            Try
                ModuloImpresion.ImprimirEgreso(comprobante)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TxtMonto_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtMonto.Validated
        txtMonto.Text = FormatNumber(IIf(txtMonto.Text = "", 0, txtMonto.Text), 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtMonto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class