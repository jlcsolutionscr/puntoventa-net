Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmDetalleTraslado
#Region "Variables"
    Private dblTotal As Decimal
    Private dtbDetalleTraslado As DataTable
    Private dtrRowDetTraslado As DataRow
    Private traslado As Traslado
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleTraslado()
        dtbDetalleTraslado = New DataTable()
        dtbDetalleTraslado.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleTraslado.Columns.Add("CODIGO", GetType(String))
        dtbDetalleTraslado.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleTraslado.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleTraslado.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleTraslado.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleTraslado.PrimaryKey = {dtbDetalleTraslado.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleTraslado.Columns.Clear()
        grdDetalleTraslado.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleTraslado.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 200
        dvcCodigo.ReadOnly = True
        grdDetalleTraslado.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 320
        grdDetalleTraslado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 100
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcTotal)
    End Sub

    Private Sub CargarDetalleTraslado(ByVal traslado As Traslado)
        dtbDetalleTraslado.Rows.Clear()
        For Each detalle As DetalleTraslado In traslado.DetalleTraslado
            dtrRowDetTraslado = dtbDetalleTraslado.NewRow
            dtrRowDetTraslado.Item(0) = detalle.IdProducto
            dtrRowDetTraslado.Item(1) = detalle.Producto.Codigo
            dtrRowDetTraslado.Item(2) = detalle.Producto.Descripcion
            dtrRowDetTraslado.Item(3) = detalle.Cantidad
            dtrRowDetTraslado.Item(4) = detalle.PrecioCosto
            dtrRowDetTraslado.Item(5) = dtrRowDetTraslado.Item(3) * dtrRowDetTraslado.Item(4)
            dtbDetalleTraslado.Rows.Add(dtrRowDetTraslado)
        Next
        grdDetalleTraslado.Refresh()
    End Sub

    Private Sub CargarTotales()
        dblTotal = 0
        For I As Short = 0 To dtbDetalleTraslado.Rows.Count - 1
            dblTotal = dblTotal + CDbl(dtbDetalleTraslado.Rows(I).Item(5))
        Next
        txtTotal.Text = FormatNumber(dblTotal, 2)
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmDetalleTraslado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
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

    Private Async Sub FrmDetalleTraslado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleTraslado()
            EstablecerPropiedadesDataGridView()
            grdDetalleTraslado.DataSource = dtbDetalleTraslado
            If FrmPrincipal.intBusqueda > 0 Then
                Try
                    traslado = Await Puntoventa.ObtenerTraslado(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                If traslado IsNot Nothing Then
                    txtIdTraslado.Text = traslado.IdTraslado
                    txtNombreSucursalOrigen.Text = traslado.NombreSucursalOrigen
                    txtNombreSucursalDestino.Text = traslado.NombreSucursalDestino
                    txtFecha.Text = traslado.Fecha
                    txtReferencia.Text = traslado.Referencia
                    If traslado.Aplicado Then btnAplicar.Enabled = False
                    CargarDetalleTraslado(traslado)
                    CargarTotales()
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAplicar_Click(sender As Object, e As EventArgs) Handles btnAplicar.Click
        If MessageBox.Show("Desea aplicar este registro de traslado?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            btnAplicar.Enabled = False
            Try
                Await Puntoventa.AplicarTraslado(traslado.IdTraslado, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                btnAplicar.Enabled = True
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Close()
        End If
    End Sub

#End Region
End Class