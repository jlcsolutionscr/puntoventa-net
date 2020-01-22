Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmEgreso
#Region "Variables"
    Private egreso As Egreso
    Private comprobante As ModuloImpresion.ClsEgreso
#End Region

#Region "Métodos"
    Private Async Function CargarCombos() As Task
        cboCuentaEgreso.ValueMember = "Id"
        cboCuentaEgreso.DisplayMember = "Descripcion"
        cboCuentaEgreso.DataSource = Await Puntoventa.ObtenerListadoCuentasEgreso(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmEgreso_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
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
        txtMonto.Text = ""
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
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
                txtIdEgreso.Text = egreso.IdEgreso
                txtFecha.Text = egreso.Fecha.ToString("dd/MM/yyyy")
                cboCuentaEgreso.SelectedValue = egreso.IdCuenta
                txtBeneficiario.Text = egreso.Beneficiario
                txtDetalle.Text = egreso.Detalle
                txtMonto.Text = FormatNumber(egreso.Monto, 2)
                txtMonto.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
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
        If txtMonto.Text = "" Then
            MessageBox.Show("Debe ingresar el monto del registro de egresos.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = False
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
                .strMonto = txtMonto.Text
            }
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

    Private Sub TxtMonto_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtMonto.Validated
        txtMonto.Text = FormatNumber(IIf(txtMonto.Text = "", 0, txtMonto.Text), 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtMonto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class