Imports System.Collections.Generic
Imports Microsoft.Reporting.WinForms
Imports System.Reflection
Imports System.IO
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes

Public Class FrmInventario
#Region "Variables"
    Private intTotalRegistros As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 16
    Private intCantidadDePaginas As Integer
    Private intIdSucursal As Integer = FrmPrincipal.equipoGlobal.IdSucursal
    Private bolInizializado As Boolean = False
    Private newFormReport As FrmReportViewer
    Private assembly As Assembly = Assembly.LoadFrom("Common.dll")
    Private strEmpresa As String = IIf(FrmPrincipal.empresaGlobal.NombreComercial = "", FrmPrincipal.empresaGlobal.NombreEmpresa, FrmPrincipal.empresaGlobal.NombreComercial)
#End Region

#Region "Metodos"
    Private Sub EstablecerPropiedadesDataGridView()
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcCodigoProveedor As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta1 As New DataGridViewTextBoxColumn
        Dim dvcActivo As New DataGridViewCheckBoxColumn
        Dim dvcObservacion As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "Id"
        dvcIdProducto.HeaderText = "Id"
        dvcIdProducto.Visible = False
        dgvListado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 100
        dgvListado.Columns.Add(dvcCodigo)

        dvcCodigoProveedor.DataPropertyName = "CodigoProveedor"
        dvcCodigoProveedor.HeaderText = "Código Prov"
        dvcCodigoProveedor.Width = 100
        dgvListado.Columns.Add(dvcCodigoProveedor)

        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 330
        dgvListado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "Cantidad"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 50
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PrecioCosto"
        dvcPrecioCosto.HeaderText = "Precio Costo"
        dvcPrecioCosto.Width = 100
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta1.DataPropertyName = "PrecioVenta1"
        dvcPrecioVenta1.HeaderText = "Precio Venta"
        dvcPrecioVenta1.Width = 100
        dvcPrecioVenta1.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcPrecioVenta1)

        dvcActivo.DataPropertyName = "ACTIVO"
        dvcActivo.HeaderText = "A"
        dvcActivo.Width = 20
        dvcActivo.Visible = True
        dvcActivo.ReadOnly = True
        dgvListado.Columns.Add(dvcActivo)

        dvcObservacion.DataPropertyName = "Observacion"
        dvcObservacion.HeaderText = "Observaciones"
        dvcObservacion.Width = 200
        dgvListado.Columns.Add(dvcObservacion)
    End Sub

    Private Async Function CargarComboBox() As Threading.Tasks.Task
        cboLinea.ValueMember = "Id"
        cboLinea.DisplayMember = "Descripcion"
        cboLinea.DataSource = Await Puntoventa.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        cboLinea.SelectedValue = 0
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = FrmPrincipal.listaSucursales
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
    End Function

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Threading.Tasks.Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, intNumeroPagina, intFilasPorPagina, chkIncluyeServicios.Checked, chkFiltrarActivos.Checked, chkFiltrarExistencias.Checked, False, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text, FrmPrincipal.usuarioGlobal.Token)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadRegistros() As Threading.Tasks.Task
        Try
            intTotalRegistros = Await Puntoventa.ObtenerTotalListaProductos(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, chkIncluyeServicios.Checked, chkFiltrarActivos.Checked, chkFiltrarExistencias.Checked, False, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        intCantidadDePaginas = Math.Truncate(intTotalRegistros / intFilasPorPagina) + IIf((intTotalRegistros Mod intFilasPorPagina) = 0, 0, 1)
        If intCantidadDePaginas > 1 Then
            btnLast.Enabled = True
            btnNext.Enabled = True
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
        Else
            btnLast.Enabled = False
            btnNext.Enabled = False
            btnPrevious.Enabled = False
            btnFirst.Enabled = False
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmInventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub CmdFiltrar_Click(sender As Object, e As EventArgs) Handles CmdFiltrar.Click
        Try
            If CmdFiltrar.Enabled Then
                CmdFiltrar.Enabled = False
                btnFirst.Enabled = False
                btnPrevious.Enabled = False
                btnNext.Enabled = False
                btnLast.Enabled = False
                Await ValidarCantidadRegistros()
                intIndiceDePagina = 1
                Await ActualizarDatos(intIndiceDePagina)
                CmdFiltrar.Enabled = True
                btnFirst.Enabled = True
                btnPrevious.Enabled = True
                btnNext.Enabled = True
                btnLast.Enabled = True
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub FrmInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            Await CargarComboBox()
            Await ValidarCantidadRegistros()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
            bolInizializado = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub btnReporte_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReporte.Click
        btnReporte.Enabled = False
        Dim datosReporte As List(Of ReporteInventario)
        Try
            datosReporte = Await Puntoventa.ObtenerReporteInventario(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, chkFiltrarActivos.Checked, chkFiltrarExistencias.Checked, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            btnReporte.Enabled = True
            Exit Sub
        End Try
        Dim strFecha As String = Now().ToString("dd/MM/yyyy hh:mm:ss")
        Dim rds As ReportDataSource = New ReportDataSource("dstDatos", datosReporte)
        newFormReport = New FrmReportViewer
        newFormReport.Visible = False
        newFormReport.repReportViewer.LocalReport.DataSources.Clear()
        newFormReport.repReportViewer.LocalReport.DataSources.Add(rds)
        newFormReport.repReportViewer.ProcessingMode = ProcessingMode.Local
        Dim stream As Stream = assembly.GetManifestResourceStream("LeandroSoftware.Common.PlantillaReportes.rptInventario.rdlc")
        newFormReport.repReportViewer.LocalReport.LoadReportDefinition(stream)
        Dim parameters(3) As ReportParameter
        parameters(0) = New ReportParameter("pUsuario", FrmPrincipal.usuarioGlobal.CodigoUsuario)
        parameters(1) = New ReportParameter("pEmpresa", strEmpresa)
        parameters(2) = New ReportParameter("pSucursal", cboSucursal.Text)
        parameters(3) = New ReportParameter("pFecha", strFecha)
        Try
            newFormReport.repReportViewer.LocalReport.SetParameters(parameters)
            newFormReport.ShowDialog()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
        btnReporte.Enabled = True
    End Sub

    Private Sub btnCardex_Click(sender As Object, e As EventArgs) Handles btnCardex.Click
        If dgvListado.Rows.Count > 0 Then
            If dgvListado.CurrentRow.Cells(0).Value.ToString <> "" Then
                Dim movimiento As New FrmMovimientoProducto With {
                    .intIdProducto = dgvListado.CurrentRow.Cells(0).Value,
                    .intIdSucursal = intIdSucursal
                }
                movimiento.ShowDialog()
            End If
        End If
    End Sub

    Private Sub chkFiltrarActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkFiltrarActivos.CheckedChanged
        If bolInizializado And Not cboSucursal.SelectedValue Is Nothing Then
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub

    Private Sub cboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSucursal.SelectedIndexChanged
        If bolInizializado And Not cboSucursal.SelectedValue Is Nothing Then
            intIdSucursal = cboSucursal.SelectedValue
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub

    Private Sub cboLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLinea.SelectedIndexChanged
        If bolInizializado And Not cboSucursal.SelectedValue Is Nothing Then
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub

    Private Sub chkFiltrarExistencias_CheckedChanged(sender As Object, e As EventArgs) Handles chkFiltrarExistencias.CheckedChanged
        If bolInizializado And Not cboSucursal.SelectedValue Is Nothing Then
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub

    Private Sub chkIncluyeServicios_CheckedChanged(sender As Object, e As EventArgs) Handles chkIncluyeServicios.CheckedChanged
        If bolInizializado And Not cboSucursal.SelectedValue Is Nothing Then
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub
#End Region
End Class