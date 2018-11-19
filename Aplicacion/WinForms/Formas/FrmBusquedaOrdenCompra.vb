Imports LeandroSoftware.Puntoventa.Servicios
Imports Unity

Public Class FrmBusquedaOrdenCompra
#Region "Variables"
    Private servicioCompras As ICompraService
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
    Private intId As Integer = 0
    Private bolIncluyeTodo = True
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn
        Dim dvcTopeCredito As New DataGridViewTextBoxColumn

        FlexProducto.Columns.Clear()
        FlexProducto.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdOrdenCompra"
        dvcId.Width = 50
        FlexProducto.Columns.Add(dvcId)
        dvcNombre.HeaderText = "Proveedor"
        dvcNombre.DataPropertyName = "NombreProveedor"
        dvcNombre.Width = 450
        FlexProducto.Columns.Add(dvcNombre)
        dvcTopeCredito.HeaderText = "Total"
        dvcTopeCredito.DataPropertyName = "Total"
        dvcTopeCredito.Width = 120
        dvcTopeCredito.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        FlexProducto.Columns.Add(dvcTopeCredito)
    End Sub

    Private Sub ActualizarDatos(ByVal intNumeroPagina As Integer)
        Try
            FlexProducto.DataSource = servicioCompras.ObtenerListaOrdenesCompra(FrmMenuPrincipal.empresaGlobal.IdEmpresa, bolIncluyeTodo, intNumeroPagina, intFilasPorPagina, intId, txtNombre.Text)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        FlexProducto.Refresh()
    End Sub

    Private Sub ValidarCantidadEmpresas()
        Try
            intTotalEmpresas = servicioCompras.ObtenerTotalListaOrdenesCompra(FrmMenuPrincipal.empresaGlobal.IdEmpresa, bolIncluyeTodo, intId, txtNombre.Text)
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

    Public Sub ExcluirOrdenesAplicadas()
        bolIncluyeTodo = False
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtId.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 0)
    End Sub

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

    Private Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioCompras = FrmMenuPrincipal.unityContainer.Resolve(Of ICompraService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        EstablecerPropiedadesDataGridView()
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles FlexProducto.DoubleClick
        If FlexProducto.RowCount > 0 Then
            FrmMenuPrincipal.intBusqueda = FlexProducto.CurrentRow.Cells(0).Value
            Close()
        End If
    End Sub

    Private Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        If txtId.Text = "" Then
            intId = 0
        Else
            intId = CInt(txtId.Text)
        End If
        ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class
