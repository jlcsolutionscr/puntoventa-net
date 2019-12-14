Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmBusquedaDevolucionCliente
#Region "Variables"
    Private intTotalRegistros As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
    Private intId As Integer = 0
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcNombre As New DataGridViewTextBoxColumn
        Dim dvcTopeCredito As New DataGridViewTextBoxColumn

        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdFactura"
        dvcId.Width = 50
        dgvListado.Columns.Add(dvcId)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 70
        dgvListado.Columns.Add(dvcFecha)
        dvcNombre.HeaderText = "Cliente"
        dvcNombre.DataPropertyName = "NombreCliente"
        dvcNombre.Width = 380
        dgvListado.Columns.Add(dvcNombre)
        dvcTopeCredito.HeaderText = "Total"
        dvcTopeCredito.DataPropertyName = "Total"
        dvcTopeCredito.Width = 120
        dvcTopeCredito.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcTopeCredito)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Threading.Tasks.Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoDevolucionesPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina, FrmPrincipal.usuarioGlobal.Token, intId, txtNombre.Text)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadRegistros() As Task
        Try
            intTotalRegistros = Await Puntoventa.ObtenerTotalListaDevolucionesPorCliente(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token, intId, txtNombre.Text)
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
    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtId.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 0)
    End Sub

    Private Sub btnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
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

    Private Async Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            Await ValidarCantidadRegistros()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        If dgvListado.RowCount > 0 Then
            FrmPrincipal.intBusqueda = dgvListado.CurrentRow.Cells(0).Value
            Close()
        End If
    End Sub

    Private Async Sub btnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        If txtId.Text = "" Then
            intId = 0
        Else
            intId = CInt(txtId.Text)
        End If
        Await ValidarCantidadRegistros()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub
#End Region
End Class
