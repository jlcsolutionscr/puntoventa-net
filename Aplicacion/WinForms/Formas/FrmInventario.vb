Public Class FrmInventario
#Region "Variables"
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
#End Region

#Region "Metodos"
    Private Sub EstablecerPropiedadesDataGridView()
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dgvNumber As DataGridViewCellStyle
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcPrecioPorMayor As New DataGridViewTextBoxColumn
        dgvNumber = New DataGridViewCellStyle With {
            .Format = "N2",
            .NullValue = "0",
            .Alignment = DataGridViewContentAlignment.MiddleRight
        }
        dvcIdProducto.DataPropertyName = "IdProducto"
        dvcIdProducto.HeaderText = "PK"
        dvcIdProducto.Visible = False
        dgvListado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 120
        dgvListado.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 230
        dgvListado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "Cantidad"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 48
        dvcCantidad.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PrecioCosto"
        dvcPrecioCosto.HeaderText = "Precio Costo"
        dvcPrecioCosto.Width = 100
        dvcPrecioCosto.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta.DataPropertyName = "PrecioVenta"
        dvcPrecioVenta.HeaderText = "Precio Venta"
        dvcPrecioVenta.Width = 100
        dvcPrecioVenta.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcPrecioVenta)

        dvcPrecioPorMayor.DataPropertyName = "PrecioVentaPorMayor"
        dvcPrecioPorMayor.HeaderText = "Prec. x Mayor"
        dvcPrecioPorMayor.Width = 100
        dvcPrecioPorMayor.DefaultCellStyle = dgvNumber
        dgvListado.Columns.Add(dvcPrecioPorMayor)
    End Sub

    Private Sub CargarComboBox()
        'cboLinea.DataSource = servicioMantenimiento.ObtenerListaLineasDeProducto(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboLinea.ValueMember = "IdLinea"
        cboLinea.DisplayMember = "Descripcion"
        cboLinea.SelectedValue = 0
    End Sub

    Private Sub ActualizarDatos(ByVal intNumeroPagina As Integer)
        Try
            'dgvListado.DataSource = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, False, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        dgvListado.Refresh()
    End Sub

    Private Sub ValidarCantidadEmpresas()
        Try
            'intTotalEmpresas = servicioMantenimiento.ObtenerTotalListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, False, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        intCantidadDePaginas = Math.Truncate(intTotalEmpresas / intFilasPorPagina) + IIf((intTotalEmpresas Mod intFilasPorPagina) = 0, 0, 1)

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
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub btnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub btnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub btnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub CmdFiltrar_Click(sender As Object, e As EventArgs) Handles CmdFiltrar.Click
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FrmInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarComboBox()
            EstablecerPropiedadesDataGridView()
            ValidarCantidadEmpresas()
            intIndiceDePagina = 1
            ActualizarDatos(intIndiceDePagina)
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
                    .intIdProducto = dgvListado.CurrentRow.Cells(0).Value
                }
                movimiento.ShowDialog()
            End If
        End If
    End Sub
#End Region
End Class