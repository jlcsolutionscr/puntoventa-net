Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmProductoListado
#Region "Variables"
    Private intTotalRegistros As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 14
    Private intCantidadDePaginas As Integer
    Private listado As IList
    Private bolReady As Boolean = False
#End Region

#Region "Métodos"
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
        Dim dvcUtilidad As New DataGridViewTextBoxColumn
        Dim dvcActivo As New DataGridViewCheckBoxColumn
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
        dvcDescripcion.Width = 300
        dgvListado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "Cantidad"
        dvcCantidad.HeaderText = "Cant"
        dvcCantidad.Width = 47
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

        dvcUtilidad.DataPropertyName = "Utilidad"
        dvcUtilidad.HeaderText = "Util"
        dvcUtilidad.Width = 50
        dvcUtilidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcUtilidad)

        dvcActivo.DataPropertyName = "ACTIVO"
        dvcActivo.HeaderText = "A"
        dvcActivo.Width = 20
        dvcActivo.Visible = True
        dvcActivo.ReadOnly = True
        dgvListado.Columns.Add(dvcActivo)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            listado = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, intNumeroPagina, intFilasPorPagina, True, chkFiltrarActivos.Checked, False, chkConDescuento.Checked, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text, FrmPrincipal.usuarioGlobal.Token)
            dgvListado.DataSource = listado
            If listado.Count() > 0 Then
                btnEditar.Enabled = True
                btnEliminar.Enabled = True
            Else
                btnEditar.Enabled = False
                btnEliminar.Enabled = False
            End If
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            Throw ex
        End Try
        dgvListado.Refresh()
    End Function

    Private Async Function ValidarCantidadRegistros() As Task
        Try
            intTotalRegistros = Await Puntoventa.ObtenerTotalListaProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, True, chkFiltrarActivos.Checked, False, chkConDescuento.Checked, cboLinea.SelectedValue, txtCodigo.Text, txtCodigoProveedor.Text, txtDescripcion.Text, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            Throw ex
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

    Private Async Function CargarComboBox() As Task
        cboLinea.DataSource = Await Puntoventa.ObtenerListadoLineas(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        cboLinea.ValueMember = "Id"
        cboLinea.DisplayMember = "Descripcion"
        cboLinea.SelectedValue = 0
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmProductoListado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmProductoListado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            Await ValidarCantidadRegistros()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
            Await CargarComboBox()
            bolReady = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnAgregar.Click
        Dim formMant As New FrmProducto With {
            .intIdProducto = 0
        }
        formMant.ShowDialog()
        Await ValidarCantidadRegistros()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnEditar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEditar.Click
        Dim formMant As New FrmProducto With {
            .intIdProducto = dgvListado.CurrentRow.Cells(0).Value
            }
        formMant.ShowDialog()
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnEliminar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminar.Click
        If MessageBox.Show("Desea eliminar el registro actual", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
            Try
                Await Puntoventa.EliminarProducto(dgvListado.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            Await ValidarCantidadRegistros()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnFiltrar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnFiltrar.Click
        If btnFiltrar.Enabled Then
            btnFiltrar.Enabled = False
            btnFirst.Enabled = False
            btnPrevious.Enabled = False
            btnNext.Enabled = False
            btnLast.Enabled = False
            Await ValidarCantidadRegistros()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
            btnFiltrar.Enabled = True
            btnFirst.Enabled = True
            btnPrevious.Enabled = True
            btnNext.Enabled = True
            btnLast.Enabled = True
        End If
    End Sub

    Private Async Sub FlexProducto_DoubleClick(ByVal sender As Object, ByVal e As EventArgs) Handles dgvListado.DoubleClick
        Dim formMant As New FrmProducto With {
            .intIdProducto = dgvListado.CurrentRow.Cells(0).Value
        }
        formMant.ShowDialog()
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub chkFiltrarActivos_CheckedChanged(sender As Object, e As EventArgs) Handles chkFiltrarActivos.CheckedChanged
        btnFiltrar_Click(btnFiltrar, New EventArgs())
    End Sub

    Private Sub cboLinea_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboLinea.SelectedIndexChanged
        If bolReady And cboLinea.SelectedValue IsNot Nothing Then
            BtnFiltrar_Click(btnFiltrar, New EventArgs())
        End If
    End Sub

    Private Sub chkConDescuento_CheckedChanged(sender As Object, e As EventArgs) Handles chkConDescuento.CheckedChanged
        BtnFiltrar_Click(btnFiltrar, New EventArgs())
    End Sub
#End Region
End Class