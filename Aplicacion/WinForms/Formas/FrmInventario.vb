Imports LeandroSoftware.ClienteWCF

Public Class FrmInventario
#Region "Variables"
    Private intTotalRegistros As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
    Private intIdSucursal As Integer = FrmPrincipal.equipoGlobal.IdSucursal
    Private bolInit As Boolean = True
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
        cboLinea.DataSource = Await Puntoventa.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboLinea.SelectedValue = 0
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
    End Function

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Threading.Tasks.Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, intNumeroPagina, intFilasPorPagina, False, chkFiltrarActivos.Checked, FrmPrincipal.usuarioGlobal.Token, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadRegistros() As Threading.Tasks.Task
        Try
            intTotalRegistros = Await Puntoventa.ObtenerTotalListaProductos(FrmPrincipal.empresaGlobal.IdEmpresa, False, chkFiltrarActivos.Checked, FrmPrincipal.usuarioGlobal.Token, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        Await ValidarCantidadRegistros()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub FrmInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarComboBox()
            EstablecerPropiedadesDataGridView()
            Await ValidarCantidadRegistros()
            bolInit = False
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnReporte_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnReporte.Click
        'Dim reptInventario As New rptInventario
        'Dim formReport As New ReportViewer()
        ''Dim dtbDatos As DataTable
        ''dtbDatos = servicioReportes.ObtenerReporteInventario(FrmMenuPrincipal.empresaGlobal.IdEmpresa, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
        ''reptInventario.SetDataSource(dtbDatos)
        'reptInventario.SetParameterValue(0, FrmPrincipal.usuarioGlobal.CodigoUsuario)
        'reptInventario.SetParameterValue(1, FrmPrincipal.empresaGlobal.NombreEmpresa)
        'formReport.crtViewer.ReportSource = reptInventario
        'formReport.ShowDialog()
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

    Private Async Sub chkFiltrarActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkFiltrarActivos.CheckedChanged
        Await ValidarCantidadRegistros()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub cboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSucursal.SelectedIndexChanged
        If Not bolInit And Not cboSucursal.SelectedValue Is Nothing Then
            intIdSucursal = cboSucursal.SelectedValue
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub

    Private Sub cboLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLinea.SelectedIndexChanged
        If Not bolInit And Not cboLinea.SelectedValue Is Nothing Then
            CmdFiltrar_Click(CmdFiltrar, New EventArgs())
        End If
    End Sub
#End Region
End Class