Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmBusquedaProforma
#Region "Variables"
    Private dtListaEstado As New DataTable, drListaEstado As DataRow
    Private intTotalProformas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
    Private intId As Integer = 0
    Private bolCargado As Boolean = False
    Public bolIncluyeEstado As Boolean = False
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcConsecutivo As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcNombreCliente As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.DataPropertyName = "IdFactura"
        dvcId.Width = 0
        dvcId.Visible = False
        dgvListado.Columns.Add(dvcId)
        dvcConsecutivo.HeaderText = "Consec"
        dvcConsecutivo.DataPropertyName = "Consecutivo"
        dvcConsecutivo.Width = 50
        dgvListado.Columns.Add(dvcConsecutivo)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 67
        dgvListado.Columns.Add(dvcFecha)
        dvcNombreCliente.HeaderText = "Cliente"
        dvcNombreCliente.DataPropertyName = "NombreCliente"
        dvcNombreCliente.Width = 400
        dgvListado.Columns.Add(dvcNombreCliente)
        dvcTotal.HeaderText = "Total"
        dvcTotal.DataPropertyName = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcTotal)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerListadoProformas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, cboEstado.SelectedValue, intNumeroPagina, intFilasPorPagina, intId, txtNombre.Text, FrmPrincipal.usuarioGlobal.Token)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadProformas() As Task
        Try
            intTotalProformas = Await Puntoventa.ObtenerTotalListaProformas(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, cboEstado.SelectedValue, intId, txtNombre.Text, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        intCantidadDePaginas = Math.Truncate(intTotalProformas / intFilasPorPagina) + IIf((intTotalProformas Mod intFilasPorPagina) = 0, 0, 1)
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

    Private Sub CargarCombos()
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = FrmPrincipal.ObtenerListadoSucursales()
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
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmBusquedaProforma_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            cboEstado.Visible = bolIncluyeEstado
            lblEstado.Visible = bolIncluyeEstado
            EstablecerPropiedadesDataGridView()
            CargarCombos()
            Await ValidarCantidadProformas()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
            bolCargado = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Async Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        If txtId.Text = "" Then
            intId = 0
        Else
            intId = CInt(txtId.Text)
        End If
        Await ValidarCantidadProformas()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub cboEstado_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboEstado.SelectedIndexChanged
        If bolCargado Then BtnFiltrar_Click(btnFiltrar, New EventArgs())
    End Sub

    Private Sub cboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSucursal.SelectedIndexChanged
        If bolCargado Then BtnFiltrar_Click(btnFiltrar, New EventArgs())
    End Sub
#End Region
End Class
