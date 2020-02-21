Public Class FrmActualizarPrecioProducto
#Region "Variables"
#End Region

#Region "Metodos"
    Private Sub EstablecerPropiedadesDataGridView()
        GrdDetalle.Columns.Clear()
        GrdDetalle.AutoGenerateColumns = False
        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        dvcIdProducto.DataPropertyName = "IdProducto"
        dvcIdProducto.HeaderText = "PK"
        dvcIdProducto.Visible = False
        GrdDetalle.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "Codigo"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 100
        GrdDetalle.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 230
        GrdDetalle.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "Cantidad"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 48
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        GrdDetalle.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PrecioCosto"
        dvcPrecioCosto.HeaderText = "Precio Costo"
        dvcPrecioCosto.Width = 100
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        GrdDetalle.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta.DataPropertyName = "PrecioVenta"
        dvcPrecioVenta.HeaderText = "Precio Venta"
        dvcPrecioVenta.Width = 100
        dvcPrecioVenta.DefaultCellStyle = FrmPrincipal.dgvDecimal
        GrdDetalle.Columns.Add(dvcPrecioVenta)
    End Sub

    Private Sub CargarComboBox()
        'cboLinea.DataSource = servicioMantenimiento.ObtenerListaLineasDeProducto(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        cboLinea.ValueMember = "IdLinea"
        cboLinea.DisplayMember = "Descripcion"
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmActualizarPrecioProducto_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub CmdFiltrar_Click(sender As Object, e As EventArgs) Handles CmdFiltrar.Click
        Try
            'GrdDetalle.DataSource = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, False, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        cboLinea.SelectedValue = 0
    End Sub

    Private Sub FrmActualizarPrecioProducto_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            CargarComboBox()
            cboLinea.SelectedValue = 0
            'GrdDetalle.DataSource = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, False)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnAplicar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAplicar.Click
        If GrdDetalle.RowCount > 0 And txtPorcentaje.Text <> "" Then
            If MessageBox.Show("Desea proceder con la aplicación de aumento sobre el precio de venta de los productos listados?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                'servicioMantenimiento.ActualizarPrecioVentaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, cboLinea.SelectedValue, txtCodigo.Text, txtDescripcion.Text, Decimal.Parse(txtPorcentaje.Text))
                MessageBox.Show("Debe listar al menos un producto o ingresar el porcentaje de aumento.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Else
            MessageBox.Show("Debe listar al menos un producto o ingresar el porcentaje de aumento.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtPorcentaje.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class