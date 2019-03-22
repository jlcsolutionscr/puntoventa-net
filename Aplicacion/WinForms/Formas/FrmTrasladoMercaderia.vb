Imports System.Collections.Generic
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports System.Xml

Public Class FrmTrasladoMercaderia
#Region "Variables"
    Private dblTotal As Decimal
    Private I As Short
    Private dtbDatosLocal, dtbDetalleTraslado As DataTable
    Private dtrRowDetTraslado As DataRow
    Private arrDetalleTraslado As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private traslado As Traslado
    Private detalleTraslado As DetalleTraslado
    Private producto As Producto
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private listOfProducts As IList(Of Producto)
    Private bolInit As Boolean = True
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
        dtbDetalleTraslado.Columns.Add("EXCENTO", GetType(Integer))
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
        dvcCodigo.Width = 225
        dvcCodigo.ReadOnly = True
        grdDetalleTraslado.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        grdDetalleTraslado.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 75
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleTraslado.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.Visible = True
        dvcExc.ReadOnly = True
        grdDetalleTraslado.Columns.Add(dvcExc)
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
            dtrRowDetTraslado.Item(6) = detalle.Producto.ParametroImpuesto.TasaImpuesto = 0
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
            dtbDetalleTraslado.Rows(intIndice).Item(6) = producto.ParametroImpuesto.TasaImpuesto = 0
        Else
            dtrRowDetTraslado = dtbDetalleTraslado.NewRow
            dtrRowDetTraslado.Item(0) = producto.IdProducto
            dtrRowDetTraslado.Item(1) = producto.Codigo
            dtrRowDetTraslado.Item(2) = producto.Descripcion
            dtrRowDetTraslado.Item(3) = txtCantidad.Text
            dtrRowDetTraslado.Item(4) = producto.PrecioCosto
            dtrRowDetTraslado.Item(5) = dtrRowDetTraslado.Item(3) * dtrRowDetTraslado.Item(4)
            dtrRowDetTraslado.Item(6) = producto.ParametroImpuesto.TasaImpuesto = 0
            dtbDetalleTraslado.Rows.Add(dtrRowDetTraslado)
        End If
        grdDetalleTraslado.Refresh()
    End Sub

    Private Sub CargarTotales()
        dblTotal = 0
        For I = 0 To dtbDetalleTraslado.Rows.Count - 1
            dblTotal = dblTotal + CDbl(dtbDetalleTraslado.Rows(I).Item(5))
        Next
        txtTotal.Text = FormatNumber(dblTotal, 2)
    End Sub

    Private Sub CargarCombos()
        Try
            cboIdSucursal.ValueMember = "IdSucursal"
            cboIdSucursal.DisplayMember = "Nombre"
            'cboIdSucursal.DataSource = servicioTraslados.ObtenerListaSucursales(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        cboIdSucursal.SelectedValue = 0
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
                    txtPrecioCosto.Text = ""
                    txtCodigo.Focus()
                    Exit Sub
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
                txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
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
#End Region

#Region "Eventos Controles"
    Private Sub FrmTraslado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        CargarCombos()
        If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
            CargarAutoCompletarProducto()
        End If
        IniciaDetalleTraslado()
        EstablecerPropiedadesDataGridView()
        grdDetalleTraslado.DataSource = dtbDetalleTraslado
        bolInit = False
        txtCantidad.Text = "1"
        txtTotal.Text = FormatNumber(0, 2)
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdTraslado.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboIdSucursal.SelectedValue = 0
        txtDocumento.Text = ""
        rbEntrante.Checked = True
        dtbDetalleTraslado.Rows.Clear()
        grdDetalleTraslado.Refresh()
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPrecioCosto.Text = ""
        btnInsertar.Enabled = True
        btnEliminar.Enabled = True
        btnBusProd.Enabled = True
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImportar.Enabled = True
        btnExportar.Enabled = False
        btnImprimir.Enabled = False
        gbTipo.Enabled = True
        cboIdSucursal.Focus()
    End Sub

    Private Sub btnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdTraslado.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioTraslados.AnularTraslado(txtIdTraslado.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub btnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaTraslado()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'traslado = servicioTraslados.ObtenerTraslado(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If traslado IsNot Nothing Then
                txtIdTraslado.Text = traslado.IdTraslado
                cboIdSucursal.SelectedValue = traslado.IdSucursal
                txtFecha.Text = traslado.Fecha
                txtDocumento.Text = traslado.NoDocumento
                If traslado.Tipo = 0 Then
                    rbEntrante.Checked = True
                Else
                    rbSaliente.Checked = True
                End If
                CargarDetalleTraslado(traslado)
                CargarTotales()
                btnInsertar.Enabled = False
                btnEliminar.Enabled = False
                btnBusProd.Enabled = False
                btnImportar.Enabled = False
                btnExportar.Enabled = rbSaliente.Checked
                btnImprimir.Enabled = True
                btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                btnGuardar.Enabled = False
                gbTipo.Enabled = False
            End If
            End If
    End Sub

    Private Sub btnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intTipoPrecio = 1
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmPrincipal.strBusqueda
            ValidarProducto()
        End If
        txtCodigo.Focus()
    End Sub

    Private Sub btnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If Not cboIdSucursal.SelectedValue Is Nothing And txtFecha.Text <> "" And txtDocumento.Text <> "" And CDbl(txtTotal.Text) > 0 And (rbEntrante.Checked Or rbSaliente.Checked) Then
            If txtIdTraslado.Text = "" Then
                traslado = New Traslado With {
                    .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                    .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                    .IdSucursal = cboIdSucursal.SelectedValue,
                    .Fecha = FrmPrincipal.ObtenerFechaFormateada(Now()),
                    .Tipo = If(rbEntrante.Checked = True, 0, 1),
                    .NoDocumento = txtDocumento.Text,
                    .Total = dblTotal
                }
                For I = 0 To dtbDetalleTraslado.Rows.Count - 1
                    detalleTraslado = New DetalleTraslado With {
                        .IdProducto = dtbDetalleTraslado.Rows(I).Item(0),
                        .Cantidad = dtbDetalleTraslado.Rows(I).Item(3),
                        .PrecioCosto = dtbDetalleTraslado.Rows(I).Item(4),
                        .Excento = dtbDetalleTraslado.Rows(I).Item(6)
                    }
                    traslado.DetalleTraslado.Add(detalleTraslado)
                Next
                Try
                    'traslado = servicioTraslados.AgregarTraslado(traslado)
                    txtIdTraslado.Text = traslado.IdTraslado
                Catch ex As Exception
                    txtIdTraslado.Text = ""
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                traslado.NoDocumento = txtDocumento.Text
                Try
                    'servicioTraslados.ActualizarTraslado(traslado)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            btnImportar.Enabled = False
            btnExportar.Enabled = rbSaliente.Checked
            btnImprimir.Enabled = True
            btnAgregar.Enabled = True
            btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
            btnImprimir.Focus()
            btnGuardar.Enabled = False
            btnInsertar.Enabled = False
            btnEliminar.Enabled = False
            btnBusProd.Enabled = False
            gbTipo.Enabled = False
        Else
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdTraslado.Text <> "" Then
            comprobante = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdTraslado.Text,
                .strNombre = cboIdSucursal.Text,
                .strFecha = txtFecha.Text,
                .strFormaPago = If(rbEntrante.Checked = True, "Entrante", "Saliente"),
                .strEnviadoPor = If(rbEntrante.Checked = True, cboIdSucursal.Text, FrmPrincipal.empresaGlobal.NombreEmpresa),
                .strTotal = txtTotal.Text
            }
            arrDetalleTraslado = New List(Of ModuloImpresion.clsDetalleComprobante)
            For I = 0 To dtbDetalleTraslado.Rows.Count - 1
                detalleComprobante = New ModuloImpresion.clsDetalleComprobante With {
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
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btnImportar_Click(sender As Object, e As EventArgs) Handles btnImportar.Click
        Dim xmlFileOpenDialog As New OpenFileDialog With {
            .Filter = "Extensible Markup Language (*.xml)|*.xml",
            .Title = "Select the XML file to be imported"
        }
        If xmlFileOpenDialog.ShowDialog() = System.Windows.Forms.DialogResult.OK Then
            Dim xmlDoc As New XmlDocument()
            xmlDoc.Load(xmlFileOpenDialog.OpenFile())
            Dim trasladoXmlNodo As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Traslado")
            txtDocumento.Text = trasladoXmlNodo.Item(0).SelectSingleNode("DocumentoNro").InnerText
            rbEntrante.Checked = True
            dtbDetalleTraslado.Rows.Clear()
            Dim detalleTrasladoXmlNodo As XmlNodeList = xmlDoc.DocumentElement.SelectNodes("/Traslado/DetalleTraslado/Producto")
            For Each lineaDetalle As XmlNode In detalleTrasladoXmlNodo
                dtrRowDetTraslado = dtbDetalleTraslado.NewRow
                dtrRowDetTraslado.Item(0) = lineaDetalle.SelectSingleNode("IdProducto").InnerText
                dtrRowDetTraslado.Item(1) = lineaDetalle.SelectSingleNode("Código").InnerText
                dtrRowDetTraslado.Item(2) = lineaDetalle.SelectSingleNode("Descripción").InnerText
                dtrRowDetTraslado.Item(3) = CDbl(lineaDetalle.SelectSingleNode("Cantidad").InnerText)
                dtrRowDetTraslado.Item(4) = CDbl(lineaDetalle.SelectSingleNode("PrecioCosto").InnerText)
                dtrRowDetTraslado.Item(5) = dtrRowDetTraslado.Item(3) * dtrRowDetTraslado.Item(4)
                dtrRowDetTraslado.Item(6) = IIf(lineaDetalle.SelectSingleNode("Excento").InnerText = "true", True, False)
                dtbDetalleTraslado.Rows.Add(dtrRowDetTraslado)
            Next
            CargarTotales()
        End If


    End Sub

    Private Sub btnExportar_Click(sender As Object, e As EventArgs) Handles btnExportar.Click
        Dim xmlFileSaveDialog As New SaveFileDialog With {
            .Filter = "Extensible Markup Language (*.xml)|*.xml",
            .Title = "Enter the XML file name to be created"
        }
        If xmlFileSaveDialog.ShowDialog = DialogResult.OK Then
            Dim strPath As String = xmlFileSaveDialog.FileName
            Dim writer As New XmlTextWriter(strPath, System.Text.Encoding.UTF8)
            With writer
                .WriteStartDocument(True)
                .Formatting = Formatting.Indented
                .Indentation = 2
                .WriteStartElement("Traslado")
                .WriteStartElement("IdTraslado")
                .WriteString(txtIdTraslado.Text)
                .WriteEndElement()
                .WriteStartElement("DocumentoNro")
                .WriteString(txtDocumento.Text)
                .WriteEndElement()
                .WriteStartElement("Fecha")
                .WriteString(txtFecha.Text)
                .WriteEndElement()
                .WriteStartElement("Envia")
                .WriteString(FrmPrincipal.empresaGlobal.NombreEmpresa)
                .WriteEndElement()
                .WriteStartElement("Total")
                .WriteString(txtTotal.Text)
                .WriteEndElement()
                .WriteStartElement("DetalleTraslado")
                For I = 0 To dtbDetalleTraslado.Rows.Count - 1
                    .WriteStartElement("Producto")
                    .WriteStartElement("IdProducto")
                    .WriteString(dtbDetalleTraslado.Rows(I).Item(0))
                    .WriteEndElement()
                    .WriteStartElement("Código")
                    .WriteString(dtbDetalleTraslado.Rows(I).Item(1))
                    .WriteEndElement()
                    .WriteStartElement("Descripción")
                    .WriteString(dtbDetalleTraslado.Rows(I).Item(2))
                    .WriteEndElement()
                    .WriteStartElement("Cantidad")
                    .WriteString(dtbDetalleTraslado.Rows(I).Item(3))
                    .WriteEndElement()
                    .WriteStartElement("PrecioCosto")
                    .WriteString(dtbDetalleTraslado.Rows(I).Item(4))
                    .WriteEndElement()
                    .WriteStartElement("Excento")
                    .WriteString(dtbDetalleTraslado.Rows(I).Item(6))
                    .WriteEndElement()
                    .WriteEndElement()
                Next
                .WriteEndElement()
                .WriteEndElement()
                .WriteEndDocument()
                .Close()
            End With
        End If
    End Sub

    Private Sub btnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" Then
            CargarLineaDetalleTraslado(producto)
            CargarTotales()
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtCantidad.Text = "1"
            txtPrecioCosto.Text = ""
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub btnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dtbDetalleTraslado.Rows.Count > 0 Then
            dtbDetalleTraslado.Rows.Remove(dtbDetalleTraslado.Rows.Find(grdDetalleTraslado.CurrentRow.Cells(0).Value))
            grdDetalleTraslado.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub PrecioCosto_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioCosto.Validated
        If txtPrecioCosto.Text = "" Then txtPrecioCosto.Text = "0"
        txtPrecioCosto.Text = FormatNumber(txtPrecioCosto.Text, 2)
    End Sub

    Private Sub txtCodigo_Validated(ByVal sender As Object, ByVal e As EventArgs) Handles txtCodigo.Validated
        ValidarProducto()
    End Sub

    Private Sub txtPlazo_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtPrecioCosto.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class