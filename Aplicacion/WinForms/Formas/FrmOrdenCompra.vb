Imports System.Collections.Generic
Imports LeandroSoftware.Puntoventa.Core.CommonTypes
Imports LeandroSoftware.Puntoventa.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmOrdenCompra
#Region "Variables"
    Private dblExcento, dblGrabado, dblSubTotal, dblPorcentajeIVA As Decimal
    Private I As Integer
    Private dtbDatosLocal, dtbDetalleOrdenCompra As DataTable
    Private objRowDetOrdenCompra As DataRow
    Private servicioCompras As ICompraService
    Private servicioMantenimiento As IMantenimientoService
    Private servicioReportes As IReporteService
    Private ordenCompra As OrdenCompra
    Private proveedor As Proveedor
    Private detalleOrdenCompra As DetalleOrdenCompra
    Private producto As Producto
    Private bolInit As Boolean = True

    Private formReport As New frmRptViewer()
    Private dtbDatos As DataTable
    Private strEmpresa, strUsuario, strTelefonos As String
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleOrdenCompra()
        dtbDetalleOrdenCompra = New DataTable()
        dtbDetalleOrdenCompra.Columns.Add("IDPRODUCTO", GetType(Int32))
        dtbDetalleOrdenCompra.Columns.Add("CODIGO", GetType(String))
        dtbDetalleOrdenCompra.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleOrdenCompra.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleOrdenCompra.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleOrdenCompra.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleOrdenCompra.Columns.Add("EXCENTO", GetType(Int32))
        dtbDetalleOrdenCompra.PrimaryKey = {dtbDetalleOrdenCompra.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleOrdenCompra.Columns.Clear()
        grdDetalleOrdenCompra.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleOrdenCompra.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 190
        grdDetalleOrdenCompra.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 350
        grdDetalleOrdenCompra.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleOrdenCompra.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 80
        dvcPrecioCosto.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleOrdenCompra.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleOrdenCompra.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 0
        dvcExc.Visible = False
        grdDetalleOrdenCompra.Columns.Add(dvcExc)
    End Sub

    Private Sub CargarDetalleOrdenCompra(ByVal OrdenCompra As OrdenCompra)
        dtbDetalleOrdenCompra.Rows.Clear()
        For Each detalle As DetalleOrdenCompra In OrdenCompra.DetalleOrdenCompra
            objRowDetOrdenCompra = dtbDetalleOrdenCompra.NewRow
            objRowDetOrdenCompra.Item(0) = detalle.IdProducto
            objRowDetOrdenCompra.Item(1) = detalle.Producto.Codigo
            objRowDetOrdenCompra.Item(2) = detalle.Producto.Descripcion
            objRowDetOrdenCompra.Item(3) = detalle.Cantidad
            objRowDetOrdenCompra.Item(4) = detalle.PrecioCosto
            objRowDetOrdenCompra.Item(5) = objRowDetOrdenCompra.Item(3) * objRowDetOrdenCompra.Item(4)
            objRowDetOrdenCompra.Item(6) = detalle.Producto.Excento
            dtbDetalleOrdenCompra.Rows.Add(objRowDetOrdenCompra)
        Next
        grdDetalleOrdenCompra.Refresh()
    End Sub

    Private Sub CargarLineaDetalleOrdenCompra(ByVal producto As Producto)
        Dim intIndice As Integer = dtbDetalleOrdenCompra.Rows.IndexOf(dtbDetalleOrdenCompra.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleOrdenCompra.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenCompra.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleOrdenCompra.Rows(intIndice).Item(3) += txtCantidad.Text
            dtbDetalleOrdenCompra.Rows(intIndice).Item(4) = txtPrecioCosto.Text
            dtbDetalleOrdenCompra.Rows(intIndice).Item(5) = dtbDetalleOrdenCompra.Rows(intIndice).Item(3) * dtbDetalleOrdenCompra.Rows(intIndice).Item(4)
            dtbDetalleOrdenCompra.Rows(intIndice).Item(6) = producto.Excento
        Else
            objRowDetOrdenCompra = dtbDetalleOrdenCompra.NewRow
            objRowDetOrdenCompra.Item(0) = producto.IdProducto
            objRowDetOrdenCompra.Item(1) = producto.Codigo
            objRowDetOrdenCompra.Item(2) = producto.Descripcion
            objRowDetOrdenCompra.Item(3) = txtCantidad.Text
            objRowDetOrdenCompra.Item(4) = txtPrecioCosto.Text
            objRowDetOrdenCompra.Item(5) = objRowDetOrdenCompra.Item(3) * objRowDetOrdenCompra.Item(4)
            objRowDetOrdenCompra.Item(6) = producto.Excento
            dtbDetalleOrdenCompra.Rows.Add(objRowDetOrdenCompra)
        End If
        grdDetalleOrdenCompra.Refresh()
    End Sub

    Private Sub CargarTotales()
        dblExcento = 0
        dblGrabado = 0
        dblSubTotal = 0
        For I = 0 To dtbDetalleOrdenCompra.Rows.Count - 1
            If dtbDetalleOrdenCompra.Rows(I).Item(6) = 0 Then
                dblGrabado = dblGrabado + CDbl(dtbDetalleOrdenCompra.Rows(I).Item(5))
            Else
                dblExcento = dblExcento + CDbl(dtbDetalleOrdenCompra.Rows(I).Item(5))
            End If
        Next
        dblSubTotal = dblGrabado + dblExcento
        If dblSubTotal > 0 Then
            dblExcento = FormatNumber(dblExcento - (CDbl(txtDescuento.Text) / dblSubTotal * dblExcento), 2)
            dblGrabado = FormatNumber(dblGrabado - (CDbl(txtDescuento.Text) / dblSubTotal * dblGrabado), 2)
        End If
        txtImpuesto.Text = FormatNumber(dblGrabado * (dblPorcentajeIVA / 100), 2)
        txtSubTotal.Text = FormatNumber(dblSubTotal, 2)
        txtTotal.Text = FormatNumber(dblExcento + dblGrabado + CDbl(txtImpuesto.Text), 2)
    End Sub

    Private Sub ValidarProducto()
        If Not bolInit Then
            If txtCodigo.Text <> "" Then
                If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If txtCodigo.Text.IndexOf(" ") >= 0 Then
                        txtCodigo.Text = txtCodigo.Text.Substring(0, txtCodigo.Text.IndexOf(" "))
                    End If
                End If
                Try
                    producto = servicioMantenimiento.ObtenerProductoPorCodigo(txtCodigo.Text)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                If producto Is Nothing Then
                    MessageBox.Show("El código ingresado no existe. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecioCosto.Text = ""
                    txtCodigo.Focus()
                    Exit Sub
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
                txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
            End If
        End If
    End Sub

    Private Sub CargarAutoCompletarProducto()
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As ICollection(Of Producto) = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, True)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub

    Private Sub CargarCombos()
        Try
            cboIdCondicionVenta.ValueMember = "IdCondicionVenta"
            cboIdCondicionVenta.DisplayMember = "Descripcion"
            cboIdCondicionVenta.DataSource = servicioMantenimiento.ObtenerListaCondicionVenta()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmOrdenCompra_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioCompras = FrmMenuPrincipal.unityContainer.Resolve(Of ICompraService)()
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
            servicioReportes = FrmMenuPrincipal.unityContainer.Resolve(Of IReporteService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
        If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
            CargarAutoCompletarProducto()
        End If
        CargarCombos()
        IniciaDetalleOrdenCompra()
        EstablecerPropiedadesDataGridView()
        grdDetalleOrdenCompra.DataSource = dtbDetalleOrdenCompra
        bolInit = False
        txtCantidad.Text = "1"
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdOrdenCompra.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        dblPorcentajeIVA = FrmMenuPrincipal.empresaGlobal.PorcentajeIVA
        proveedor = Nothing
        txtProveedor.Text = ""
        txtReferencia.Text = ""
        cboIdCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        txtPlazoCredito.Text = ""
        dtbDetalleOrdenCompra.Rows.Clear()
        grdDetalleOrdenCompra.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtCantidad.Text = ""
        txtDescripcion.Text = ""
        txtPrecioCosto.Text = ""
        txtDescuento.ReadOnly = False
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnBusProd.Enabled = True
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        btnBuscarProveedor.Enabled = True
        txtProveedor.Focus()
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdOrdenCompra.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    servicioCompras.AnularOrdenCompra(txtIdOrdenCompra.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaOrdenCompra()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                ordenCompra = servicioCompras.ObtenerOrdenCompra(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenCompra IsNot Nothing Then
                txtIdOrdenCompra.Text = ordenCompra.IdOrdenCompra
                proveedor = ordenCompra.Proveedor
                txtProveedor.Text = proveedor.Nombre
                txtFecha.Text = ordenCompra.Fecha
                txtReferencia.Text = ordenCompra.NoDocumento
                cboIdCondicionVenta.SelectedValue = ordenCompra.IdCondicionVenta
                txtPlazoCredito.Text = ordenCompra.PlazoCredito
                txtDescuento.Text = FormatNumber(ordenCompra.Descuento, 2)
                dblPorcentajeIVA = ordenCompra.PorcentajeIVA
                CargarDetalleOrdenCompra(ordenCompra)
                CargarTotales()
                txtDescuento.ReadOnly = True
                btnImprimir.Enabled = True
                If ordenCompra.Aplicado Then
                    btnBuscarProveedor.Enabled = False
                    btnAnular.Enabled = False
                    btnGuardar.Enabled = False
                Else
                    btnBuscarProveedor.Enabled = True
                    btnAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                    btnGuardar.Enabled = True
                End If
            End If
        End If
    End Sub

    Private Sub BtnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                proveedor = servicioCompras.ObtenerProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtProveedor.Text = proveedor.Nombre
        End If
    End Sub

    Private Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intTipoPrecio = 1
        }
        FrmMenuPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmMenuPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmMenuPrincipal.strBusqueda
            ValidarProducto()
        End If
        txtCodigo.Focus()
    End Sub

    Private Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If proveedor Is Nothing Or txtFecha.Text = "" Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdOrdenCompra.Text = "" Then
            ordenCompra = New OrdenCompra With {
                .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario
            }
        End If
        ordenCompra.IdProveedor = proveedor.IdProveedor
        ordenCompra.Fecha = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        ordenCompra.IdCondicionVenta = cboIdCondicionVenta.SelectedValue
        ordenCompra.PlazoCredito = IIf(txtPlazoCredito.Text = "", 0, txtPlazoCredito.Text)
        ordenCompra.NoDocumento = txtReferencia.Text
        ordenCompra.Excento = dblExcento
        ordenCompra.Grabado = dblGrabado
        ordenCompra.Descuento = CDbl(txtDescuento.Text)
        ordenCompra.PorcentajeIVA = dblPorcentajeIVA
        ordenCompra.Impuesto = CDbl(txtImpuesto.Text)
        If txtIdOrdenCompra.Text <> "" Then
            ordenCompra.DetalleOrdenCompra.Clear()
        End If
        For I = 0 To dtbDetalleOrdenCompra.Rows.Count - 1
            detalleOrdenCompra = New DetalleOrdenCompra
            If txtIdOrdenCompra.Text <> "" Then
                detalleOrdenCompra.IdOrdenCompra = ordenCompra.IdOrdenCompra
            End If
            detalleOrdenCompra.IdProducto = dtbDetalleOrdenCompra.Rows(I).Item(0)
            detalleOrdenCompra.Cantidad = dtbDetalleOrdenCompra.Rows(I).Item(3)
            detalleOrdenCompra.PrecioCosto = dtbDetalleOrdenCompra.Rows(I).Item(4)
            detalleOrdenCompra.Excento = dtbDetalleOrdenCompra.Rows(I).Item(6)
            ordenCompra.DetalleOrdenCompra.Add(detalleOrdenCompra)
        Next
        If txtIdOrdenCompra.Text = "" Then
            Try
                ordenCompra = servicioCompras.AgregarOrdenCompra(ordenCompra)
                txtIdOrdenCompra.Text = ordenCompra.IdOrdenCompra
            Catch ex As Exception
                txtIdOrdenCompra.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            Try
                servicioCompras.ActualizarOrdenCompra(ordenCompra)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
        btnImprimir.Focus()
        btnGuardar.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
        btnInsertar.Enabled = False
        btnEliminar.Enabled = False
        btnBusProd.Enabled = False
        btnBuscarProveedor.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdOrdenCompra.Text <> "" Then
            Dim reptOrdenCompra As New rptOrdenCompra
            Try
                dtbDatos = servicioReportes.ObtenerReporteOrdenCompra(txtIdOrdenCompra.Text)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            strUsuario = FrmMenuPrincipal.usuarioGlobal.CodigoUsuario
            strEmpresa = FrmMenuPrincipal.empresaGlobal.NombreEmpresa
            strTelefonos = FrmMenuPrincipal.empresaGlobal.Telefono
            reptOrdenCompra.SetDataSource(dtbDatos)
            reptOrdenCompra.SetParameterValue(0, strUsuario)
            reptOrdenCompra.SetParameterValue(1, strEmpresa)
            reptOrdenCompra.SetParameterValue(2, strTelefonos)
            formReport.crtViewer.ReportSource = reptOrdenCompra
            formReport.ShowDialog()
        End If
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" And txtPrecioCosto.Text <> "" Then
            CargarLineaDetalleOrdenCompra(producto)
            CargarTotales()
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecioCosto.Text = ""
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dtbDetalleOrdenCompra.Rows.Count > 0 Then
            dtbDetalleOrdenCompra.Rows.Remove(dtbDetalleOrdenCompra.Rows.Find(grdDetalleOrdenCompra.CurrentRow.Cells(0).Value))
            grdDetalleOrdenCompra.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub Descuento_Leave(sender As Object, e As EventArgs) Handles txtDescuento.Validated
        If txtDescuento.Text = "" Then
            txtDescuento.Text = FormatNumber(0, 2)
        Else
            If InStr(txtDescuento.Text, "%") Then
                txtDescuento.Text = CDbl(Mid(txtDescuento.Text, 1, Len(txtDescuento.Text) - 1)) / 100 * CDbl(txtSubTotal.Text)
            End If
            If CDbl(txtDescuento.Text) > CDbl(txtSubTotal.Text) Then
                MessageBox.Show("El descuento debe ser menor al SubTotal. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                txtDescuento.Text = 0
            End If
            txtDescuento.Text = FormatNumber(txtDescuento.Text, 2)
        End If
        CargarTotales()
    End Sub

    Private Sub PrecioCosto_Validating(ByVal sender As Object, ByVal e As EventArgs)
        If txtPrecioCosto.Text <> "" Then txtPrecioCosto.Text = FormatNumber(txtPrecioCosto.Text, 2)
    End Sub

    Private Sub CboCodigo_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.Validated
        ValidarProducto()
    End Sub

    Private Sub CboIdCondicionVenta_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboIdCondicionVenta.SelectedIndexChanged
        If cboIdCondicionVenta.SelectedValue = StaticCondicionVenta.Credito Then
            txtPlazoCredito.Enabled = True
            txtPlazoCredito.Text = "30"
        Else
            txtPlazoCredito.Enabled = False
            txtPlazoCredito.Text = ""
        End If
    End Sub

    Private Sub TxtPlazo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        FrmMenuPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescuento.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class