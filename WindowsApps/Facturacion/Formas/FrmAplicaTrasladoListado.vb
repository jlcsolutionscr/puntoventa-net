Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmAplicaTrasladoListado
#Region "Variables"
    Private dtListaEstado As New DataTable, drListaEstado As DataRow
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
        dvcId.DataPropertyName = "IdTraslado"
        dvcId.Width = 50
        dgvListado.Columns.Add(dvcId)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 70
        dgvListado.Columns.Add(dvcFecha)
        dvcNombre.HeaderText = "Origen del traslado"
        dvcNombre.DataPropertyName = "NombreSucursal"
        dvcNombre.Width = 400
        dgvListado.Columns.Add(dvcNombre)
        dvcTopeCredito.HeaderText = "Total"
        dvcTopeCredito.DataPropertyName = "Total"
        dvcTopeCredito.Width = 100
        dvcTopeCredito.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcTopeCredito)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoTrasladosPorAplicar(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, cboEstado.SelectedValue, intNumeroPagina, intFilasPorPagina, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadRegistros() As Task
        Try
            intTotalRegistros = Await Puntoventa.ObtenerTotalListaTrasladosPorAplicar(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, cboEstado.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
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

    Private Async Function CargarCombos() As Task
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.bolSeleccionaSucursal
        dtListaEstado.Clear()
        dtListaEstado.Columns.Add(New DataColumn("IdEstado", GetType(Boolean)))
        dtListaEstado.Columns.Add(New DataColumn("Descripcion", GetType(String)))
        drListaEstado = dtListaEstado.NewRow()
        drListaEstado(0) = False
        drListaEstado(1) = "Por Aplicar"
        dtListaEstado.Rows.Add(drListaEstado)
        drListaEstado = dtListaEstado.NewRow()
        drListaEstado(0) = True
        drListaEstado(1) = "Aplicado"
        dtListaEstado.Rows.Add(drListaEstado)
        cboEstado.ValueMember = "IdEstado"
        cboEstado.DisplayMember = "Descripcion"
        cboEstado.DataSource = dtListaEstado
        cboEstado.SelectedValue = 0
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmAplicaTrasladoListado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtId.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 0)
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

    Private Async Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            Await CargarCombos()
            Await ValidarCantidadRegistros()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        If dgvListado.RowCount > 0 Then
            FrmPrincipal.intBusqueda = dgvListado.CurrentRow.Cells(0).Value
            Dim FormDetalleTraslado As New FrmDetalleTraslado()
            FormDetalleTraslado.ShowDialog()
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        If txtId.Text <> "" Then
            intId = CInt(txtId.Text)
        Else
            intId = 0
        End If
        Await ValidarCantidadRegistros()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub cboEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEstado.SelectedIndexChanged
        btnFiltrar_Click(btnFiltrar, New EventArgs())
    End Sub

    Private Sub cboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSucursal.SelectedIndexChanged
        btnFiltrar_Click(btnFiltrar, New EventArgs())
    End Sub

    Private Async Sub btnDetalle_Click(sender As Object, e As EventArgs)
        If dgvListado.RowCount > 0 Then
            FrmPrincipal.intBusqueda = dgvListado.CurrentRow.Cells(0).Value
            Dim FormDetalleTraslado As New FrmDetalleTraslado()
            FormDetalleTraslado.ShowDialog()
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub
#End Region
End Class
