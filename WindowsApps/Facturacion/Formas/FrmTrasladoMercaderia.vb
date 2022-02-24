Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes
Imports System.Linq

Public Class FrmTrasladoMercaderia
#Region "Variables"
    Private decExcento, decGravado, decImpuesto, decTotal As Decimal
    Private dtbDetalleTraslado As DataTable
    Private dtrRowDetTraslado As DataRow
    Private arrDetalleTraslado As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private traslado As Traslado
    Private detalleTraslado As DetalleTraslado
    Private producto As Producto
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
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
        dtbDetalleTraslado.Columns.Add("PORCDESCUENTO", GetType(Decimal))
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
        Dim dvcTasaIVA As New DataGridViewTextBoxColumn

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

        dvcTasaIVA.DataPropertyName = "PORCDESCUENTO"
        dvcTasaIVA.HeaderText = "PorcDescuento"
        dvcTasaIVA.Visible = False
        dvcTasaIVA.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcTasaIVA)
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
            dtrRowDetTraslado.Item(6) = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
            dtbDetalleTraslado.Rows.Add(dtrRowDetTraslado)
        Next
        grdDetalleTraslado.Refresh()
    End Sub

    Private Sub CargarLineaDetalleTraslado(ByVal producto As Producto)
        Dim intIndice As Integer = dtbDetalleTraslado.Rows.IndexOf(dtbDetalleTraslado.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleTraslado.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleTraslado.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleTraslado.Rows(intIndice).Item(3) += txtCantidad.Text
            dtbDetalleTraslado.Rows(intIndice).Item(4) = producto.PrecioCosto
            dtbDetalleTraslado.Rows(intIndice).Item(5) = dtbDetalleTraslado.Rows(intIndice).Item(3) * dtbDetalleTraslado.Rows(intIndice).Item(4)
            dtbDetalleTraslado.Rows(intIndice).Item(6) = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
        Else
            dtrRowDetTraslado = dtbDetalleTraslado.NewRow
            dtrRowDetTraslado.Item(0) = producto.IdProducto
            dtrRowDetTraslado.Item(1) = producto.Codigo
            dtrRowDetTraslado.Item(2) = producto.Descripcion
            dtrRowDetTraslado.Item(3) = txtCantidad.Text
            dtrRowDetTraslado.Item(4) = producto.PrecioCosto
            dtrRowDetTraslado.Item(5) = dtrRowDetTraslado.Item(3) * dtrRowDetTraslado.Item(4)
            dtrRowDetTraslado.Item(6) = FrmPrincipal.ObtenerTarifaImpuesto(producto.IdImpuesto)
            dtbDetalleTraslado.Rows.Add(dtrRowDetTraslado)
        End If
        grdDetalleTraslado.Refresh()
        CargarTotales()
    End Sub

    Private Sub CargarTotales()
        decExcento = 0
        decGravado = 0
        decImpuesto = 0
        decTotal = 0
        For I As Short = 0 To dtbDetalleTraslado.Rows.Count - 1
            If dtbDetalleTraslado.Rows(I).Item(6) > 0 Then
                decGravado += CDbl(dtbDetalleTraslado.Rows(I).Item(5))
                decImpuesto += CDbl(dtbDetalleTraslado.Rows(I).Item(5)) * dtbDetalleTraslado.Rows(I).Item(6) / 100
            Else
                decExcento += CDbl(dtbDetalleTraslado.Rows(I).Item(5))
            End If
        Next
        decTotal = decGravado + decExcento + decImpuesto
        txtExcento.Text = FormatNumber(decExcento, 2)
        txtGravado.Text = FormatNumber(decGravado, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
    End Sub

    Private Async Function CargarCombos() As Task
        cboIdSucursalDestino.ValueMember = "Id"
        cboIdSucursalDestino.DisplayMember = "Descripcion"
        cboIdSucursalDestino.DataSource = Await Puntoventa.ObtenerListadoSucursalDestino(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtCantidad.Text = ""
            txtPrecioCosto.Text = FormatNumber(0, 2)
            txtUnidad.Text = ""
            txtCodigo.Focus()
            Exit Sub
        Else
            txtCodigo.Text = producto.Codigo
            txtDescripcion.Text = producto.Descripcion
            txtExistencias.Text = producto.Existencias
            txtCantidad.Text = ""
            txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
            txtUnidad.Text = IIf(producto.Tipo = 1, "UND", IIf(producto.Tipo = 2, "SP", "OS"))
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
#End Region

#Region "Eventos Controles"
    Private Sub FrmTrasladoMercaderia_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmTrasladoMercaderia_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.F3 Then
            BtnBuscar_Click(btnBuscar, New EventArgs())
        ElseIf e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            btnGuardar_Click(btnGuardar, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Async Sub FrmTrasladoMercaderia_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleTraslado()
            EstablecerPropiedadesDataGridView()
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            txtNombreSucursalOrigen.Text = FrmPrincipal.equipoGlobal.NombreSucursal
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto Then CargarAutoCompletarProducto()
            grdDetalleTraslado.DataSource = dtbDetalleTraslado
            txtCantidad.Text = ""
            txtTotal.Text = FormatNumber(0, 2)
            Await CargarCombos()
            If cboIdSucursalDestino.Items.Count = 0 Then
                MessageBox.Show("La empresa no posee sucursales adicionales.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
                Exit Sub
            End If
            cboIdSucursalDestino.SelectedIndex = 0
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdTraslado.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtNombreSucursalOrigen.Text = FrmPrincipal.equipoGlobal.NombreSucursal
        txtReferencia.Text = ""
        dtbDetalleTraslado.Rows.Clear()
        grdDetalleTraslado.Refresh()
        txtExcento.Text = FormatNumber(0, 2)
        txtGravado.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtDescripcion.Text = ""
        txtUnidad.Text = ""
        txtExistencias.Text = ""
        txtCantidad.Text = ""
        txtPrecioCosto.Text = ""
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnBusProd.Enabled = True
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        cboIdSucursalDestino.Focus()
    End Sub

    Private Async Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdTraslado.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularTraslado(txtIdTraslado.Text, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaTraslado()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                traslado = Await Puntoventa.ObtenerTraslado(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If traslado IsNot Nothing Then
                txtIdTraslado.Text = traslado.IdTraslado
                txtNombreSucursalOrigen.Text = FrmPrincipal.equipoGlobal.NombreSucursal
                cboIdSucursalDestino.SelectedValue = traslado.IdSucursalDestino
                txtFecha.Text = traslado.Fecha
                txtReferencia.Text = traslado.Referencia
                CargarDetalleTraslado(traslado)
                CargarTotales()
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnBusProd.Enabled = False
                btnImprimir.Enabled = True
                btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intIdSucursal = FrmPrincipal.equipoGlobal.IdSucursal
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            Dim intIdProducto As Integer = Integer.Parse(FrmPrincipal.strBusqueda)
            Try
                producto = Await Puntoventa.ObtenerProducto(intIdProducto, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show("Error al obtener la información del producto seleccionado. Intente mas tarde.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
            txtCantidad.Focus()
        End If
    End Sub

    Private Async Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cboIdSucursalDestino.SelectedValue Is Nothing Then
            MessageBox.Show("Debe seleccionar la sucursal destino para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            cboIdSucursalDestino.Focus()
            Exit Sub
        ElseIf CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Debe agregar líneas de detalle para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        If txtIdTraslado.Text = "" Then
            traslado = New Traslado With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdSucursalOrigen = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdSucursalDestino = cboIdSucursalDestino.SelectedValue,
                .Fecha = FrmPrincipal.ObtenerFechaFormateada(Now()),
                .Referencia = txtReferencia.Text,
                .Total = decTotal
            }
            traslado.DetalleTraslado = New List(Of DetalleTraslado)
            For I As Short = 0 To dtbDetalleTraslado.Rows.Count - 1
                detalleTraslado = New DetalleTraslado With {
                    .IdProducto = dtbDetalleTraslado.Rows(I).Item(0),
                    .Cantidad = dtbDetalleTraslado.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleTraslado.Rows(I).Item(4)
                }
                traslado.DetalleTraslado.Add(detalleTraslado)
            Next
            Try
                txtIdTraslado.Text = Await Puntoventa.AgregarTraslado(traslado, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdTraslado.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnBusProd.Enabled = False
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdTraslado.Text <> "" Then
            comprobante = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdTraslado.Text,
                .strNombre = txtNombreSucursalOrigen.Text,
                .strFecha = txtFecha.Text,
                .strFormaPago = cboIdSucursalDestino.Text,
                .strEnviadoPor = FrmPrincipal.usuarioGlobal.CodigoUsuario,
                .strSubTotal = FormatNumber(decExcento + decGravado, 2),
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleTraslado = New List(Of ModuloImpresion.ClsDetalleComprobante)
            For I As Short = 0 To dtbDetalleTraslado.Rows.Count - 1
                detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleTraslado.Rows(I).Item(1) + "-" + dtbDetalleTraslado.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleTraslado.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleTraslado.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleTraslado.Rows(I).Item(3)) * CDbl(dtbDetalleTraslado.Rows(I).Item(4)), 2)
                }
                arrDetalleTraslado.Add(detalleComprobante)
            Next
            comprobante.arrDetalleComprobante = arrDetalleTraslado
            Try
                ModuloImpresion.ImprimirTraslado(comprobante)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto IsNot Nothing Then
            CargarLineaDetalleTraslado(producto)
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtCantidad.Text = ""
            txtUnidad.Text = ""
            txtPrecioCosto.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleTraslado.Rows.Count > 0 Then
            dtbDetalleTraslado.Rows.Remove(dtbDetalleTraslado.Rows.Find(grdDetalleTraslado.CurrentRow.Cells(0).Value))
            grdDetalleTraslado.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtCodigo.Text <> "" Then
                Dim strCodigo As String = txtCodigo.Text.Split(":")(0)
                Try
                    producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, strCodigo, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
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
                        txtUnidad.Text = ""
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

    Private Sub txtCantidad_TextChanged(sender As Object, e As EventArgs) Handles txtCantidad.TextChanged
        If txtCantidad.Text <> "" Then
            If CDbl(txtCantidad.Text) > producto.Existencias Then
                MessageBox.Show("La cantidad por trasladar no puede ser mayor a las existencias del producto.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtCantidad.Text = ""
            End If
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecioCosto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class