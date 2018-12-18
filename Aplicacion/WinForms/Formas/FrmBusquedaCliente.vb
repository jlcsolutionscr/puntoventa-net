Public Class FrmBusquedaCliente
#Region "Variables"
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
#End Region

#Region "M�todos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn
        Dim dvcPermiteCredito As New DataGridViewCheckBoxColumn
        Dim dvcTopeCredito As New DataGridViewTextBoxColumn

        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdCliente"
        dvcId.Width = 50
        dgvListado.Columns.Add(dvcId)
        dvcNombre.HeaderText = "Nombre"
        dvcNombre.DataPropertyName = "Nombre"
        dvcNombre.Width = 400
        dgvListado.Columns.Add(dvcNombre)
        dvcPermiteCredito.HeaderText = "Cr�dito"
        dvcPermiteCredito.DataPropertyName = "PermiteCredito"
        dvcPermiteCredito.Width = 50
        dgvListado.Columns.Add(dvcPermiteCredito)
        dvcTopeCredito.HeaderText = "Tope Cr�dito"
        dvcTopeCredito.DataPropertyName = "TopeCredito"
        dvcTopeCredito.Width = 120
        dvcTopeCredito.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcTopeCredito)
    End Sub

    Private Sub ActualizarDatos(ByVal intNumeroPagina As Integer)
        Try
            'dgvListado.DataSource = servicioFacturacion.ObtenerListaClientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, txtNombre.Text, True)
            lblPagina.Text = "P�gina " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        dgvListado.Refresh()
    End Sub

    Private Sub ValidarCantidadEmpresas()
        Try
            'intTotalEmpresas = servicioFacturacion.ObtenerTotalListaClientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtNombre.Text, True)
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
    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        EstablecerPropiedadesDataGridView()
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        If dgvListado.RowCount > 0 Then
            FrmMenuPrincipal.intBusqueda = dgvListado.CurrentRow.Cells(0).Value
            Close()
        End If
    End Sub

    Private Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class
