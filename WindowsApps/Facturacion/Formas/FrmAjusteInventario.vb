Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Constantes
Imports System.Collections.Generic

Public Class FrmAjusteInventario
#Region "Variables"
    Private dtbDetalleAjusteInventario As DataTable
    Private dtrRowDetAjusteInventario As DataRow
    Private ajusteInventario As AjusteInventario
    Private detalleAjusteInventario As DetalleAjusteInventario
    Private producto As Producto
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleAjusteInventario()
        dtbDetalleAjusteInventario = New DataTable()
        dtbDetalleAjusteInventario.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleAjusteInventario.Columns.Add("CODIGO", GetType(String))
        dtbDetalleAjusteInventario.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleAjusteInventario.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleAjusteInventario.PrimaryKey = {dtbDetalleAjusteInventario.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleAjusteInventario.Columns.Clear()
        grdDetalleAjusteInventario.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleAjusteInventario.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 125
        dvcCodigo.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleAjusteInventario.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 360
        dvcDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleAjusteInventario.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 75
        dvcPrecioCosto.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcTotal)
    End Sub

    Private Sub CargarDetalleAjusteInventario(ByVal ajusteInventario As AjusteInventario)
        dtbDetalleAjusteInventario.Rows.Clear()
        For Each detalle As DetalleAjusteInventario In ajusteInventario.DetalleAjusteInventario
            dtrRowDetAjusteInventario = dtbDetalleAjusteInventario.NewRow
            dtrRowDetAjusteInventario.Item(0) = detalle.IdProducto
            dtrRowDetAjusteInventario.Item(1) = detalle.Producto.Codigo
            dtrRowDetAjusteInventario.Item(2) = detalle.Producto.Descripcion
            dtrRowDetAjusteInventario.Item(3) = detalle.Cantidad
            dtrRowDetAjusteInventario.Item(4) = detalle.PrecioCosto
            dtrRowDetAjusteInventario.Item(5) = dtrRowDetAjusteInventario.Item(3) * dtrRowDetAjusteInventario.Item(4)
            dtbDetalleAjusteInventario.Rows.Add(dtrRowDetAjusteInventario)
        Next
        grdDetalleAjusteInventario.Refresh()
    End Sub

    Private Sub CargarLineaDetalleAjusteInventario(ByVal producto As Producto)
        Dim intIndice As Integer = dtbDetalleAjusteInventario.Rows.IndexOf(dtbDetalleAjusteInventario.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleAjusteInventario.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleAjusteInventario.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleAjusteInventario.Rows(intIndice).Item(3) += txtCantidad.Text
            dtbDetalleAjusteInventario.Rows(intIndice).Item(4) = producto.PrecioCosto
            dtbDetalleAjusteInventario.Rows(intIndice).Item(5) = dtbDetalleAjusteInventario.Rows(intIndice).Item(3) * dtbDetalleAjusteInventario.Rows(intIndice).Item(4)
        Else
            dtrRowDetAjusteInventario = dtbDetalleAjusteInventario.NewRow
            dtrRowDetAjusteInventario.Item(0) = producto.IdProducto
            dtrRowDetAjusteInventario.Item(1) = producto.Codigo
            dtrRowDetAjusteInventario.Item(2) = producto.Descripcion
            dtrRowDetAjusteInventario.Item(3) = txtCantidad.Text
            dtrRowDetAjusteInventario.Item(4) = producto.PrecioCosto
            dtrRowDetAjusteInventario.Item(5) = dtrRowDetAjusteInventario.Item(3) * dtrRowDetAjusteInventario.Item(4)
            dtbDetalleAjusteInventario.Rows.Add(dtrRowDetAjusteInventario)
        End If
        grdDetalleAjusteInventario.Refresh()
    End Sub

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtPrecioCosto.Text = FormatNumber(0, 2)
            txtCodigo.Focus()
            Exit Sub
        Else
            txtCodigo.Text = producto.Codigo
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtDescripcion.Text = producto.Descripcion
            txtExistencias.Text = producto.Existencias
            txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
        End If
    End Sub

    Private Sub CargarAutoCompletarProducto()
        Dim source As New AutoCompleteStringCollection()
        For Each p As ProductoDetalle In FrmPrincipal.listaProductos
            source.Add(p.Codigo + ": " + p.Descripcion)
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
    End Sub

    Private Sub CargarCombos()
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        Dim listado As List(Of LlaveDescripcion) = New List(Of LlaveDescripcion)(FrmPrincipal.listaSucursales)
        cboSucursal.DataSource = listado
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.bolSeleccionaSucursal
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmAjusteInventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmAjusteInventario_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.F3 Then
            BtnBuscar_Click(btnBuscar, New EventArgs())
        ElseIf e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            BtnGuardar_Click(btnGuardar, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Async Sub FrmAjusteInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleAjusteInventario()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto Then CargarAutoCompletarProducto()
            btnBusProd.Enabled = True
            grdDetalleAjusteInventario.DataSource = dtbDetalleAjusteInventario
            CargarCombos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdAjuste.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtDescAjuste.Text = ""
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtExistencias.Text = ""
        txtCantidad.Text = "1"
        txtPrecioCosto.Text = ""
        dtbDetalleAjusteInventario.Rows.Clear()
        grdDetalleAjusteInventario.Refresh()
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        txtDescAjuste.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdAjuste.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularAjusteInventario(txtIdAjuste.Text, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaAjusteInventario()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ajusteInventario = Await Puntoventa.ObtenerAjusteInventario(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ajusteInventario IsNot Nothing Then
                txtIdAjuste.Text = ajusteInventario.IdAjuste
                txtFecha.Text = ajusteInventario.Fecha
                txtDescAjuste.Text = ajusteInventario.Descripcion
                CargarDetalleAjusteInventario(ajusteInventario)
                btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If txtFecha.Text = "" Or txtDescAjuste.Text = "" Or grdDetalleAjusteInventario.RowCount = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdAjuste.Text = "" Then
            ajusteInventario = New AjusteInventario With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = cboSucursal.SelectedValue,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = Now(),
                .Descripcion = txtDescAjuste.Text
            }
            ajusteInventario.DetalleAjusteInventario = New List(Of DetalleAjusteInventario)
            For I As Integer = 0 To dtbDetalleAjusteInventario.Rows.Count - 1
                detalleAjusteInventario = New DetalleAjusteInventario With {
                    .IdProducto = dtbDetalleAjusteInventario.Rows(I).Item(0),
                    .Cantidad = dtbDetalleAjusteInventario.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleAjusteInventario.Rows(I).Item(4)
                }
                ajusteInventario.DetalleAjusteInventario.Add(detalleAjusteInventario)
            Next
            Try
                txtIdAjuste.Text = Await Puntoventa.AgregarAjusteInventario(ajusteInventario, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdAjuste.Text = ""
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        btnAgregar.Focus()
        btnGuardar.Enabled = False
    End Sub


    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intIdSucursal = cboSucursal.SelectedValue
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            Dim intIdProducto As Integer = Integer.Parse(FrmPrincipal.strBusqueda)
            Try
                producto = Await Puntoventa.ObtenerProducto(intIdProducto, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show("Error al obtener la información del producto seleccionado. Intente mas tarde.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
        End If
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto IsNot Nothing Then
            CargarLineaDetalleAjusteInventario(producto)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtPrecioCosto.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dtbDetalleAjusteInventario.Rows.Count > 0 Then
            dtbDetalleAjusteInventario.Rows.Remove(dtbDetalleAjusteInventario.Rows.Find(grdDetalleAjusteInventario.CurrentRow.Cells(0).Value))
            grdDetalleAjusteInventario.Refresh()
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtCodigo.Text <> "" Then
                Dim strCodigo As String = txtCodigo.Text.Split(":")(0)
                Try
                    producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, strCodigo, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
                    If producto IsNot Nothing Then
                        If producto.Activo And producto.Tipo = StaticTipoProducto.Producto Then
                            CargarDatosProducto(producto)
                            txtCantidad.Focus()
                        Else
                            MessageBox.Show("El código ingresado no pertenece a un producto o se encuentra inactivo", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            txtCodigo.Text = ""
                            txtDescripcion.Text = ""
                            txtExistencias.Text = ""
                            txtCantidad.Text = ""
                            txtPrecioCosto.Text = ""
                            txtCodigo.Focus()
                        End If
                    Else
                        txtCodigo.Text = ""
                        txtDescripcion.Text = ""
                        txtExistencias.Text = ""
                        txtCantidad.Text = ""
                        txtPrecioCosto.Text = ""
                        txtCodigo.Focus()
                    End If
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
        End If
    End Sub

    Private Sub TxtCantidad_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCantidad.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If txtCantidad.Text <> "" And CDbl(txtPrecioCosto.Text) > 0 Then
                BtnInsertar_Click(btnInsertar, New EventArgs())
            ElseIf txtCantidad.Text <> "" Then
                txtPrecioCosto.Focus()
            End If
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".", True)
    End Sub
#End Region
End Class