Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF
Imports System.Threading.Tasks

Public Class FrmIngreso
#Region "Variables"
    Private ingreso As Ingreso
    Private comprobante As ModuloImpresion.ClsIngreso
#End Region

#Region "Métodos"
    Private Async Function CargarCombos() As Task
        cboCuentaIngreso.ValueMember = "Id"
        cboCuentaIngreso.DisplayMember = "Descripcion"
        cboCuentaIngreso.DataSource = Await Puntoventa.ObtenerListadoCuentasIngreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmIngreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdIngreso.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboCuentaIngreso.SelectedValue = 0
        txtRecibidoDe.Text = ""
        txtDetalle.Text = ""
        txtMonto.Text = FormatNumber(0, 2)
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
    End Sub

    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdIngreso.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularIngreso(txtIdIngreso.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaIngreso()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ingreso = Await Puntoventa.ObtenerIngreso(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ingreso IsNot Nothing Then
                txtIdIngreso.Text = ingreso.IdIngreso
                txtFecha.Text = ingreso.Fecha
                cboCuentaIngreso.SelectedValue = ingreso.IdCuenta
                txtRecibidoDe.Text = ingreso.RecibidoDe
                txtDetalle.Text = ingreso.Detalle
                txtMonto.Text = FormatNumber(ingreso.Monto, 2)
                txtMonto.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Async Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If cboCuentaIngreso.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar el tipo de cuenta por aplicar al ingreso.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtFecha.Text = "" Then
            MessageBox.Show("Debe ingresar la fecha del registro de ingresos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtDetalle.Text = "" Then
            MessageBox.Show("Debe ingresar el detalle del registro de ingresos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtMonto.Text = "" Then
            MessageBox.Show("Debe ingresar el monto del registro de ingresos", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdIngreso.Text = "" Then
            ingreso = New Ingreso With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = Now(),
                .IdCuenta = cboCuentaIngreso.SelectedValue,
                .RecibidoDe = txtRecibidoDe.Text,
                .Detalle = txtDetalle.Text,
                .Monto = CDbl(txtMonto.Text),
                .Nulo = False
            }
            Try
                txtIdIngreso.Text = Await Puntoventa.AgregarIngreso(ingreso, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdIngreso.Text = ""
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdIngreso.Text <> "" Then
            comprobante = New ModuloImpresion.ClsIngreso With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdIngreso.Text,
                .strFecha = txtFecha.Text,
                .strRecibidoDe = txtRecibidoDe.Text,
                .strConcepto = txtDetalle.Text,
                .strMonto = txtMonto.Text
            }
            Try
                ModuloImpresion.ImprimirIngreso(comprobante)
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