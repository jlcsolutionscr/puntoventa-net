Imports System.Collections.Generic
Imports LeandroSoftware.Puntoventa.CommonTypes
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmProforma
#Region "Variables"
    Private strMotivoRechazo As String
    Private decExcento, decGrabado, decImpuesto, decTotal As Decimal
    Private I As Short
    Private dtbDatosLocal, dtbDetalleProforma As DataTable
    Private dtrRowDetProforma As DataRow
    Private arrDetalleProforma As ArrayList
    Private proforma As Proforma
    Private detalleProforma As DetalleProforma
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private bolInit As Boolean = True
    Private listOfProducts As List(Of Producto)

    Private formReport As New FrmReportViewer()
    Private dtbDatos As DataTable
    Private strNombreEmpresa, strNombreComercial, strUsuario, strTelefonos, strIdentificacion As String
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleProforma = New DataTable()
        dtbDetalleProforma.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("CODIGO", GetType(String))
        dtbDetalleProforma.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleProforma.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleProforma.PrimaryKey = {dtbDetalleProforma.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleProforma.Columns.Clear()
        grdDetalleProforma.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Width = 0
        dvcIdProducto.Visible = False
        grdDetalleProforma.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 225
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcCantidad)

        dvcPrecio.DataPropertyName = "PRECIO"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.Visible = True
        dvcPrecio.ReadOnly = True
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.Visible = True
        dvcExc.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcExc)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        grdDetalleProforma.Columns.Add(dvcPorcentajeIVA)
    End Sub

    Private Sub CargarDetalleProforma(ByVal proforma As Proforma)
        dtbDetalleProforma.Rows.Clear()
        For Each detalle As DetalleProforma In proforma.DetalleProforma
            dtrRowDetProforma = dtbDetalleProforma.NewRow
            dtrRowDetProforma.Item(0) = detalle.IdProducto
            dtrRowDetProforma.Item(1) = detalle.Producto.Codigo
            dtrRowDetProforma.Item(2) = detalle.Producto.Descripcion
            dtrRowDetProforma.Item(3) = detalle.Cantidad
            dtrRowDetProforma.Item(4) = detalle.PrecioVenta
            dtrRowDetProforma.Item(5) = dtrRowDetProforma.Item(3) * dtrRowDetProforma.Item(4)
            dtrRowDetProforma.Item(6) = detalle.Excento
            dtrRowDetProforma.Item(7) = detalle.PorcentajeIVA
            dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        Next
        grdDetalleProforma.Refresh()
    End Sub

    Private Sub CargarLineaDetalleProforma(ByVal producto As Producto)
        Dim intIndice As Integer = dtbDetalleProforma.Rows.IndexOf(dtbDetalleProforma.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.ExoneradoDeImpuesto Then decTasaImpuesto = 0
        If intIndice >= 0 Then
            dtbDetalleProforma.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleProforma.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleProforma.Rows(intIndice).Item(3) += txtCantidad.Text
            dtbDetalleProforma.Rows(intIndice).Item(4) = txtPrecio.Text
            dtbDetalleProforma.Rows(intIndice).Item(5) = dtbDetalleProforma.Rows(intIndice).Item(3) * dtbDetalleProforma.Rows(intIndice).Item(4)
            dtbDetalleProforma.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleProforma.Rows(intIndice).Item(7) = decTasaImpuesto
        Else
            dtrRowDetProforma = dtbDetalleProforma.NewRow
            dtrRowDetProforma.Item(0) = producto.IdProducto
            dtrRowDetProforma.Item(1) = producto.Codigo
            dtrRowDetProforma.Item(2) = producto.Descripcion
            dtrRowDetProforma.Item(3) = txtCantidad.Text
            dtrRowDetProforma.Item(4) = txtPrecio.Text
            dtrRowDetProforma.Item(5) = dtrRowDetProforma.Item(3) * dtrRowDetProforma.Item(4)
            dtrRowDetProforma.Item(6) = decTasaImpuesto = 0
            dtrRowDetProforma.Item(7) = decTasaImpuesto
            dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        End If
        grdDetalleProforma.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim decSubTotal As Decimal = 0
        decGrabado = 0
        decExcento = 0
        decImpuesto = 0
        For I = 0 To dtbDetalleProforma.Rows.Count - 1
            If dtbDetalleProforma.Rows(I).Item(6) = 0 Then
                decGrabado += dtbDetalleProforma.Rows(I).Item(5)
                decImpuesto += dtbDetalleProforma.Rows(I).Item(5) * dtbDetalleProforma.Rows(I).Item(7) / 100
            Else
                decExcento += dtbDetalleProforma.Rows(I).Item(5)
            End If
        Next
        decSubTotal = decGrabado + decExcento
        If decSubTotal > 0 And txtDescuento.Text > 0 Then
            decImpuesto = 0
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                If dtbDetalleProforma.Rows(I).Item(6) = 0 Then
                    Dim decDescuentoPorLinea As Decimal = 0
                    Dim decTotalPorLinea As Decimal = dtbDetalleProforma.Rows(I).Item(5)
                    decDescuentoPorLinea = decTotalPorLinea - (txtDescuento.Text / decSubTotal * decTotalPorLinea)
                    decImpuesto += decDescuentoPorLinea * dtbDetalleProforma.Rows(I).Item(7) / 100
                End If
            Next
        End If
        decGrabado = Math.Round(decGrabado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decExcento + decGrabado + decImpuesto - txtDescuento.Text, 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
    End Sub

    Private Sub ValidarProducto()
        If Not bolInit Then
            If txtCodigo.Text <> "" Then
                If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If txtCodigo.Text.IndexOf(" ") >= 0 Then
                        txtCodigo.Text = txtCodigo.Text.Substring(0, txtCodigo.Text.IndexOf(" "))
                    End If
                End If
                Try
                    'producto = servicioMantenimiento.ObtenerProductoPorCodigo(txtCodigo.Text)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                If producto Is Nothing Then
                    MessageBox.Show("El código ingresado no existe. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Text = ""
                    txtUnidad.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecio.Text = ""
                    txtCodigo.Focus()
                    Exit Sub
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
                If cliente.IdTipoPrecio = 1 Then
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta1, 2)
                ElseIf cliente.IdTipoPrecio = 2 Then
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta2, 2)
                ElseIf cliente.IdTipoPrecio = 3 Then
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta3, 2)
                ElseIf cliente.IdTipoPrecio = 4 Then
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta4, 2)
                ElseIf cliente.IdTipoPrecio = 5 Then
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta5, 2)
                Else
                    txtPrecio.Text = FormatNumber(producto.PrecioVenta1, 2)
                End If
                txtUnidad.Text = producto.IdTipoUnidad
            End If
        End If
    End Sub

    Private Sub CargarAutoCompletarProducto()
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        'listOfProducts = servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, True)
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
            'cboIdCondicionVenta.DataSource = servicioMantenimiento.ObtenerListaCondicionVenta()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmProforma_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
            CargarAutoCompletarProducto()
        End If
        CargarCombos()
        IniciaTablasDeDetalle()
        EstablecerPropiedadesDataGridView()
        grdDetalleProforma.DataSource = dtbDetalleProforma
        txtCantidad.Text = "1"
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        bolInit = False
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdProforma.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtDocumento.Text = ""
        cboIdCondicionVenta.SelectedValue = StaticCondicionVenta.Contado
        txtPlazoCredito.Text = ""
        dtbDetalleProforma.Rows.Clear()
        grdDetalleProforma.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPrecio.Text = ""
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        txtDescuento.ReadOnly = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        cliente = Nothing
        vendedor = Nothing
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdProforma.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioFacturacion.AnularProforma(txtIdProforma.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                    MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CmdAgregar_Click(CmdAgregar, New EventArgs())
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaProforma()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'proforma = servicioFacturacion.ObtenerProforma(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If proforma IsNot Nothing Then
                txtIdProforma.Text = proforma.IdProforma
                cliente = proforma.Cliente
                txtNombreCliente.Text = proforma.Cliente.Nombre
                txtFecha.Text = proforma.Fecha
                txtDocumento.Text = proforma.NoDocumento
                vendedor = proforma.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                cboIdCondicionVenta.SelectedValue = proforma.IdCondicionVenta
                txtPlazoCredito.Text = proforma.PlazoCredito
                txtDescuento.Text = FormatNumber(proforma.Descuento, 2)
                CargarDetalleProforma(proforma)
                CargarTotales()
                CmdImprimir.Enabled = True
                txtDescuento.ReadOnly = True
                btnBuscaVendedor.Enabled = False
                If proforma.Aplicado Then
                    btnBuscarCliente.Enabled = False
                    CmdAnular.Enabled = False
                    CmdGuardar.Enabled = False
                Else
                    btnBuscarCliente.Enabled = True
                    CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                    CmdGuardar.Enabled = True
                End If
                bolInit = False
            End If
        End If
    End Sub

    Private Sub BtnBuscaVendedor_Click(sender As Object, e As EventArgs) Handles btnBuscaVendedor.Click
        Dim formBusquedaVendedor As New FrmBusquedaVendedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaVendedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'vendedor = servicioMantenimiento.ObtenerVendedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If vendedor Is Nothing Then
                MessageBox.Show("El vendedor seleccionado no existe. Consulte a su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            txtVendedor.Text = vendedor.Nombre
        End If
    End Sub

    Private Sub BtnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'cliente = servicioFacturacion.ObtenerCliente(FrmMenuPrincipal.intBusqueda)
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If cliente Is Nothing Then
                MessageBox.Show("El cliente de contado no se encuentra registrado en el sistema. Consulte a su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            txtNombreCliente.Text = cliente.Nombre
        End If
    End Sub

    Private Sub CmdBusProd_Click(sender As Object, e As EventArgs) Handles cmdBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = True,
            .intTipoPrecio = 0
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmPrincipal.strBusqueda
            ValidarProducto()
        End If
        txtCodigo.Focus()
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdProforma.Text = "" Then
            proforma = New Proforma With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario
            }
        End If
        proforma.IdCliente = cliente.IdCliente
        proforma.Fecha = FrmPrincipal.ObtenerFechaFormateada(Now())
        proforma.NoDocumento = txtDocumento.Text
        proforma.IdVendedor = vendedor.IdVendedor
        proforma.IdCondicionVenta = cboIdCondicionVenta.SelectedValue
        proforma.PlazoCredito = IIf(txtPlazoCredito.Text <> "", txtPlazoCredito.Text, 0)
        proforma.Excento = decExcento
        proforma.Grabado = decGrabado
        proforma.Descuento = CDbl(txtDescuento.Text)
        proforma.Impuesto = CDbl(txtImpuesto.Text)
        proforma.DetalleProforma.Clear()
        For I = 0 To dtbDetalleProforma.Rows.Count - 1
            detalleProforma = New DetalleProforma
            If txtIdProforma.Text <> "" Then
                detalleProforma.IdProforma = proforma.IdProforma
            End If
            detalleProforma.IdProducto = dtbDetalleProforma.Rows(I).Item(0)
            detalleProforma.Cantidad = dtbDetalleProforma.Rows(I).Item(3)
            detalleProforma.PrecioVenta = dtbDetalleProforma.Rows(I).Item(4)
            detalleProforma.Excento = dtbDetalleProforma.Rows(I).Item(6)
            detalleProforma.PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(7)
            proforma.DetalleProforma.Add(detalleProforma)
        Next
        If txtIdProforma.Text = "" Then
            Try
                'proforma = servicioFacturacion.AgregarProforma(proforma)
                txtIdProforma.Text = proforma.IdProforma
            Catch ex As Exception
                txtIdProforma.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            Try
                'servicioFacturacion.ActualizarProforma(proforma)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdProforma.Text <> "" Then
            'Dim reptProforma As New rptProforma
            'Try
            '    'dtbDatos = servicioReportes.ObtenerReporteProforma(txtIdProforma.Text)
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try
            'strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
            'strIdentificacion = FrmPrincipal.empresaGlobal.Identificacion
            'strTelefonos = FrmPrincipal.empresaGlobal.Telefono
            'strNombreEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
            'strNombreComercial = FrmPrincipal.empresaGlobal.NombreComercial
            'reptProforma.SetDataSource(dtbDatos)
            'reptProforma.SetParameterValue(0, strUsuario)
            'reptProforma.SetParameterValue(1, strNombreEmpresa)
            'reptProforma.SetParameterValue(2, strNombreComercial)
            'reptProforma.SetParameterValue(3, strIdentificacion)
            'reptProforma.SetParameterValue(4, strTelefonos)
            'formReport.crtViewer.ReportSource = reptProforma
            'formReport.ShowDialog()
        End If
    End Sub

    Private Sub txtPlazoCredito_TextChanged(sender As Object, e As KeyPressEventArgs) Handles txtPlazoCredito.KeyPress
        If (e.KeyChar >= "0" And e.KeyChar <= "9" Or e.KeyChar = "") Then
            e.Handled = False
        Else
            e.Handled = True
        End If
    End Sub

    Private Sub CmdInsertar_Click(sender As Object, e As EventArgs) Handles cmdInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" And txtPrecio.Text <> "" And txtUnidad.Text <> "" Then
            CargarLineaDetalleProforma(producto)
            CargarTotales()
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtUnidad.Text = ""
            txtPrecio.Text = ""
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles cmdEliminar.Click
        If dtbDetalleProforma.Rows.Count > 0 Then
            dtbDetalleProforma.Rows.Remove(dtbDetalleProforma.Rows.Find(grdDetalleProforma.CurrentRow.Cells(0).Value))
            grdDetalleProforma.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub Precio_Leave(sender As Object, e As EventArgs) Handles txtPrecio.Leave
        If txtPrecio.Text <> "" Then
            txtPrecio.Text = FormatNumber(txtPrecio.Text, 2)
        Else
            txtPrecio.Text = FormatNumber(0, 2)
        End If
    End Sub

    Private Sub CboCodigo_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.Validated
        ValidarProducto()
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
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

    Private Sub TxtDescuento_Validated(sender As Object, e As EventArgs) Handles txtDescuento.Validated
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

    Private Sub ValidaDigitosSinDecimal(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecio.KeyPress, txtDescuento.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class