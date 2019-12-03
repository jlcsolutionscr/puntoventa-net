Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmMovimientoProducto
#Region "Variables"
    Private intTotalEmpresas As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 13
    Private intCantidadDePaginas As Integer
    Private producto As Producto
    Public intIdProducto As Integer
#End Region

#Region "Metodos"
    Private Sub EstablecerPropiedadesDataGridView()
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcTipo As New DataGridViewTextBoxColumn
        Dim dvcOrigen As New DataGridViewTextBoxColumn
        Dim dvcReferencia As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn

        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.Width = 125
        dgvListado.Columns.Add(dvcFecha)

        dvcTipo.DataPropertyName = "Tipo"
        dvcTipo.HeaderText = "Tipo"
        dvcTipo.Width = 50
        dgvListado.Columns.Add(dvcTipo)

        dvcOrigen.DataPropertyName = "Origen"
        dvcOrigen.HeaderText = "Origen"
        dvcOrigen.Width = 200
        dgvListado.Columns.Add(dvcOrigen)

        dvcReferencia.DataPropertyName = "Referencia"
        dvcReferencia.HeaderText = "Referencia"
        dvcReferencia.Width = 100
        dgvListado.Columns.Add(dvcReferencia)

        dvcCantidad.DataPropertyName = "Cantidad"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 48
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PrecioCosto"
        dvcPrecioCosto.HeaderText = "Precio Costo"
        dvcPrecioCosto.Width = 75
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcPrecioCosto)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Threading.Tasks.Task
        Try
            dgvListado.DataSource = Await Puntoventa.ObtenerMovimientosPorProducto(intIdProducto, intNumeroPagina, intFilasPorPagina, FechaInicio.Value.ToString("dd/MM/yyyy"), FechaFinal.Value.ToString("dd/MM/yyyy"), FrmPrincipal.usuarioGlobal.Token)
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadEmpresas() As Threading.Tasks.Task
        Try
            intTotalEmpresas = Await Puntoventa.ObtenerTotalMovimientosPorProducto(intIdProducto, FechaInicio.Value.ToString("dd/MM/yyyy"), FechaFinal.Value.ToString("dd/MM/yyyy"), FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
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
        Await ValidarCantidadEmpresas()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub FrmInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            FechaInicio.Text = "01/" & Date.Now.Month & "/" & Date.Now.Year
            FechaFinal.Text = Date.DaysInMonth(Date.Now.Year, Date.Now.Month) & "/" & Date.Now.Month & "/" & Date.Now.Year
            Await ValidarCantidadEmpresas()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub
#End Region
End Class