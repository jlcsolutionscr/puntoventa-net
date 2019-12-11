Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Utilitario

Public Class FrmFacturaCompra
#Region "Variables"
    Private decExcento, decGravado, decExonerado, decImpuesto, decTotal, decSubTotal, decPrecioVenta As Decimal
    Private I As Short
    Private dtbDetalleProforma As DataTable
    Private dtrRowDetProforma As DataRow
    Private facturaCompra As FacturaCompra
    Private detalleFacturaCompra As DetalleFacturaCompra
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleProforma = New DataTable()
        dtbDetalleProforma.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("CODIGO", GetType(String))
        dtbDetalleProforma.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleProforma.Columns.Add("IDIMPUESTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("UNIDADMEDIDA", GetType(String))
        dtbDetalleProforma.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleProforma.PrimaryKey = {dtbDetalleProforma.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleProforma.Columns.Clear()
        grdDetalleProforma.AutoGenerateColumns = False

        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcTipoImpuesto As New DataGridViewCheckBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn
        Dim dvcUnidadMedida As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcCantidad)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 155
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 295
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcDescripcion)

        dvcTipoImpuesto.DataPropertyName = "IDIMPUESTO"
        dvcTipoImpuesto.HeaderText = "Exc"
        dvcTipoImpuesto.Width = 0
        dvcTipoImpuesto.Visible = False
        dvcTipoImpuesto.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcTipoImpuesto)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        dvcTipoImpuesto.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcPorcentajeIVA)

        dvcUnidadMedida.DataPropertyName = "UNIDADMEDIDA"
        dvcUnidadMedida.HeaderText = "Unid"
        dvcUnidadMedida.Width = 20
        dvcUnidadMedida.Visible = True
        dvcUnidadMedida.ReadOnly = True
        grdDetalleProforma.Columns.Add(dvcUnidadMedida)

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
    End Sub

    Private Sub CargarLineaDetalleFacturaCompra(decCantidad As Decimal, strCodigo As String, strDescripcion As String, intIdImpuesto As Integer, intPorcetajeIVA As Integer, strUnidadMedida As String, decPrecio As Decimal)
        dtrRowDetProforma = dtbDetalleProforma.NewRow
        dtrRowDetProforma.Item(0) = decCantidad
        dtrRowDetProforma.Item(1) = strCodigo
        dtrRowDetProforma.Item(2) = strDescripcion
        dtrRowDetProforma.Item(3) = intIdImpuesto
        dtrRowDetProforma.Item(4) = intPorcetajeIVA
        dtrRowDetProforma.Item(5) = strUnidadMedida
        dtrRowDetProforma.Item(6) = decPrecio
        dtrRowDetProforma.Item(7) = decCantidad * decPrecio
        dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        grdDetalleProforma.Refresh()
        CargarTotales()
    End Sub

    Private Sub CargarTotales()
        decSubTotal = 0
        decGravado = 0
        decExonerado = 0
        decExcento = 0
        decImpuesto = 0
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I = 0 To dtbDetalleProforma.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleProforma.Rows(I).Item(4)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleProforma.Rows(I).Item(6) * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = dtbDetalleProforma.Rows(I).Item(6) * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += Math.Round(decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
                    decExonerado += Math.Round(dtbDetalleProforma.Rows(I).Item(6) - decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
                    decImpuestoProducto = decGravadoPorcentual * decTasaImpuesto / 100
                Else
                    decGravado += Math.Round(dtbDetalleProforma.Rows(I).Item(6), 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
                End If
                decImpuesto += Math.Round(decImpuestoProducto, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
            Else
                decExcento += Math.Round(dtbDetalleProforma.Rows(I).Item(6), 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(0)
            End If
        Next
        decSubTotal = decGravado + decExcento + decExonerado
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExonerado = Math.Round(decExonerado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        decTotal = Math.Round(decSubTotal + decImpuesto, 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decTotal, 2)
    End Sub

    Private Async Function CargarListadoBarrios(IdProvincia As Integer, IdCanton As Integer, IdDistrito As Integer) As Task
        cboCanton.ValueMember = "Id"
        cboCanton.DisplayMember = "Descripcion"
        cboCanton.DataSource = Await Puntoventa.ObtenerListadoCantones(IdProvincia, FrmPrincipal.usuarioGlobal.Token)
        cboDistrito.ValueMember = "Id"
        cboDistrito.DisplayMember = "Descripcion"
        cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(IdProvincia, IdCanton, FrmPrincipal.usuarioGlobal.Token)
        cboBarrio.ValueMember = "Id"
        cboBarrio.DisplayMember = "Descripcion"
        cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(IdProvincia, IdCanton, IdDistrito, FrmPrincipal.usuarioGlobal.Token)
        cboTipoImpuesto.ValueMember = "Id"
        cboTipoImpuesto.DisplayMember = "Descripcion"
        cboTipoImpuesto.DataSource = Await Puntoventa.ObtenerListadoTipoImpuesto(FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Async Function CargarCombos() As Task
        Dim columns() As String = {"Und", "Sp", "Spe", "St", "Os"}
        cboUnidadMedida.MaxDropDownItems = columns.Length
        For i As Integer = 0 To (columns.Length - 1)
            cboUnidadMedida.Items.Add(columns(i))
        Next
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = Await Puntoventa.ObtenerListadoTipoIdentificacion(FrmPrincipal.usuarioGlobal.Token)
        cboProvincia.ValueMember = "Id"
        cboProvincia.DisplayMember = "Descripcion"
        cboProvincia.DataSource = Await Puntoventa.ObtenerListadoProvincias(FrmPrincipal.usuarioGlobal.Token)
        cboTipoExoneracion.ValueMember = "Id"
        cboTipoExoneracion.DisplayMember = "Descripcion"
        cboTipoExoneracion.DataSource = Await Puntoventa.ObtenerListadoTipoExoneracion(FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmFacturaCompra_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub

    Private Async Sub FrmFacturaCompra_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            grdDetalleProforma.DataSource = dtbDetalleProforma
            txtFechaExoneracion.Text = "01/01/2019"
            txtPorcentajeExoneracion.Text = "0"
            bolInit = False
            txtCantidad.Text = "1"
            txtPrecio.Text = "0.00"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            cboTipoIdentificacion.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdProforma.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtIdentificacion.Text = ""
        txtDireccion.Text = ""
        txtNombre.Text = ""
        txtNombreComercial.Text = ""
        txtTelefono.Text = ""
        txtCorreoElectronico.Text = ""
        cboTipoIdentificacion.SelectedIndex = 0
        Await CargarListadoBarrios(1, 1, 1)
        cboTipoExoneracion.SelectedIndex = 0
        txtNumDocExoneracion.Text = ""
        txtNombreInstExoneracion.Text = ""
        txtFechaExoneracion.Text = "01/01/2019"
        txtPorcentajeExoneracion.Text = "0"
        dtbDetalleProforma.Rows.Clear()
        grdDetalleProforma.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCantidad.Text = "1"
        txtCodigo.Text = ""
        cboUnidadMedida.SelectedIndex = 0
        txtDescripcion.Text = ""
        txtPrecio.Text = "0.00"
        txtTextoAdicional.Text = ""
        decTotal = 0
        bolInit = False
        cboTipoIdentificacion.Focus()
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or decTotal = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnAgregar.Focus()
        btnGuardar.Enabled = False
        If txtIdProforma.Text = "" Then
            facturaCompra = New Proforma With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = 1,
                .IdCliente = cliente.IdCliente,
                .IdTipoExoneracion = IIf(cliente.PorcentajeExoneracion > 0, cliente.IdTipoExoneracion, 1),
                .NumDocExoneracion = txtNumDocExoneracion.Text,
                .NombreInstExoneracion = txtNombreInstExoneracion.Text,
                .FechaEmisionDoc = IIf(cliente.PorcentajeExoneracion > 0, cliente.FechaEmisionDoc, Now()),
                .PorcentajeExoneracion = cliente.PorcentajeExoneracion,
                .Fecha = Now(),
                .TextoAdicional = txtTextoAdicional.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = 0,
                .Impuesto = decImpuesto,
                .Nulo = False
            }
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleFacturaCompra = New DetalleProforma With {
                    .IdProducto = dtbDetalleProforma.Rows(I).Item(0),
                    .Descripcion = dtbDetalleProforma.Rows(I).Item(2),
                    .Cantidad = dtbDetalleProforma.Rows(I).Item(3),
                    .PrecioVenta = dtbDetalleProforma.Rows(I).Item(4),
                    .Excento = dtbDetalleProforma.Rows(I).Item(6),
                    .PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(7),
                    .PorcDescuento = dtbDetalleProforma.Rows(I).Item(8)
                }
                facturaCompra.DetalleProforma.Add(detalleFacturaCompra)
            Next
            Try
                txtIdProforma.Text = Await Puntoventa.AgregarProforma(facturaCompra, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdProforma.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            facturaCompra.TextoAdicional = txtTextoAdicional.Text
            facturaCompra.Excento = decExcento
            facturaCompra.Gravado = decGravado
            facturaCompra.Descuento = 0
            facturaCompra.Impuesto = CDbl(txtImpuesto.Text)
            facturaCompra.DetalleProforma.Clear()
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleFacturaCompra = New DetalleProforma
                detalleFacturaCompra.IdProforma = facturaCompra.IdProforma
                detalleFacturaCompra.IdProducto = dtbDetalleProforma.Rows(I).Item(0)
                detalleFacturaCompra.Descripcion = dtbDetalleProforma.Rows(I).Item(2)
                detalleFacturaCompra.Cantidad = dtbDetalleProforma.Rows(I).Item(3)
                detalleFacturaCompra.PrecioVenta = dtbDetalleProforma.Rows(I).Item(4)
                detalleFacturaCompra.Excento = dtbDetalleProforma.Rows(I).Item(6)
                detalleFacturaCompra.PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(7)
                detalleFacturaCompra.PorcDescuento = dtbDetalleProforma.Rows(I).Item(8)
                facturaCompra.DetalleProforma.Add(detalleFacturaCompra)
            Next
            Try
                Await Puntoventa.ActualizarProforma(facturaCompra, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGuardar.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs)
        If txtIdProforma.Text <> "" Then
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = txtIdProforma.Text,
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strDocumento = "",
                    .strFecha = txtFecha.Text,
                    .strSubTotal = txtSubTotal.Text,
                    .strDescuento = "0.00",
                    .strImpuesto = txtImpuesto.Text,
                    .strTotal = txtTotal.Text
                }
                arrDetalleOrden = New List(Of ModuloImpresion.ClsDetalleComprobante)
                For I = 0 To dtbDetalleProforma.Rows.Count - 1
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                    .strDescripcion = dtbDetalleProforma.Rows(I).Item(1) + "-" + dtbDetalleProforma.Rows(I).Item(2),
                    .strCantidad = CDbl(dtbDetalleProforma.Rows(I).Item(3)),
                    .strPrecio = FormatNumber(dtbDetalleProforma.Rows(I).Item(4), 2),
                    .strTotalLinea = FormatNumber(CDbl(dtbDetalleProforma.Rows(I).Item(3)) * CDbl(dtbDetalleProforma.Rows(I).Item(4)), 2),
                    .strExcento = IIf(dtbDetalleProforma.Rows(I).Item(6) = 0, "G", "E")
                }
                    arrDetalleOrden.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleOrden
                ModuloImpresion.ImprimirProforma(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnGenerarPDF_Click(sender As Object, e As EventArgs)
        If txtIdProforma.Text <> "" Then
            Dim datos As EstructuraPDF = New EstructuraPDF()
            Try
                Dim poweredByImage As Image = My.Resources.logo
                datos.PoweredByLogotipo = poweredByImage
            Catch ex As Exception
                datos.PoweredByLogotipo = Nothing
            End Try
            Try
                Dim logotipo As Byte() = Await Puntoventa.ObtenerLogotipoEmpresa(facturaCompra.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                Dim logoImage As Image
                Using ms As New MemoryStream(logotipo)
                    logoImage = Image.FromStream(ms)
                End Using
                datos.Logotipo = logoImage
            Catch ex As Exception
                datos.Logotipo = Nothing
            End Try
            datos.TituloDocumento = "FACTURA PROFORMA"
            datos.NombreEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
            datos.NombreComercial = FrmPrincipal.empresaGlobal.NombreComercial
            datos.Consecutivo = Nothing
            datos.Clave = Nothing
            datos.CondicionVenta = "Proforma"
            datos.PlazoCredito = ""
            datos.Fecha = facturaCompra.Fecha.ToString("dd/MM/yyyy hh:mm:ss")
            datos.MedioPago = ""
            datos.NombreEmisor = FrmPrincipal.empresaGlobal.NombreEmpresa
            datos.NombreComercialEmisor = FrmPrincipal.empresaGlobal.NombreComercial
            datos.IdentificacionEmisor = FrmPrincipal.empresaGlobal.Identificacion
            datos.CorreoElectronicoEmisor = FrmPrincipal.empresaGlobal.CorreoNotificacion
            datos.TelefonoEmisor = FrmPrincipal.empresaGlobal.Telefono
            datos.FaxEmisor = ""
            datos.ProvinciaEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Provincia.Descripcion
            datos.CantonEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Descripcion
            datos.DistritoEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Descripcion
            datos.BarrioEmisor = FrmPrincipal.empresaGlobal.Barrio.Descripcion
            datos.DireccionEmisor = FrmPrincipal.empresaGlobal.Direccion
            If facturaCompra.IdCliente > 1 Then
                datos.PoseeReceptor = True
                datos.NombreReceptor = cliente.Nombre
                datos.NombreComercialReceptor = cliente.NombreComercial
                datos.IdentificacionReceptor = cliente.Identificacion
                datos.CorreoElectronicoReceptor = cliente.CorreoElectronico
                datos.TelefonoReceptor = cliente.Telefono
                datos.FaxReceptor = cliente.Fax
                Dim barrio As Barrio = cliente.Barrio
                datos.ProvinciaReceptor = cliente.Barrio.Distrito.Canton.Provincia.Descripcion
                datos.CantonReceptor = cliente.Barrio.Distrito.Canton.Descripcion
                datos.DistritoReceptor = cliente.Barrio.Distrito.Descripcion
                datos.BarrioReceptor = cliente.Barrio.Descripcion
                datos.DireccionReceptor = cliente.Direccion
            End If
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                Dim decPrecioVenta As Decimal = dtbDetalleProforma.Rows(I).Item(4)
                Dim decTotalLinea As Decimal = dtbDetalleProforma.Rows(I).Item(3) * decPrecioVenta
                Dim detalle As EstructuraPDFDetalleServicio = New EstructuraPDFDetalleServicio With {
                    .Cantidad = dtbDetalleProforma.Rows(I).Item(3),
                    .Codigo = dtbDetalleProforma.Rows(I).Item(1),
                    .Detalle = dtbDetalleProforma.Rows(I).Item(2),
                    .PrecioUnitario = decPrecioVenta.ToString("N2", CultureInfo.InvariantCulture),
                    .TotalLinea = decTotalLinea.ToString("N2", CultureInfo.InvariantCulture)
                }
                datos.DetalleServicio.Add(detalle)
            Next
            If (facturaCompra.TextoAdicional IsNot Nothing) Then datos.OtrosTextos = facturaCompra.TextoAdicional
            datos.TotalGravado = decGravado.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalExonerado = decExonerado.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalExento = decExcento.ToString("N2", CultureInfo.InvariantCulture)
            datos.Descuento = "0.00"
            datos.Impuesto = decImpuesto.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalGeneral = decTotal.ToString("N2", CultureInfo.InvariantCulture)
            datos.CodigoMoneda = IIf(facturaCompra.IdTipoMoneda = 1, "CRC", "USD")
            datos.TipoDeCambio = 1
            Try
                Dim pdfBytes As Byte() = UtilitarioPDF.GenerarPDFFacturaElectronica(datos)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PROFORMA-" + txtIdProforma.Text + ".pdf"
                File.WriteAllBytes(pdfFilePath, pdfBytes)
                Process.Start(pdfFilePath)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnInsertar_Click(sender As Object, e As EventArgs) 
        If producto Is Nothing Then
            If txtCodigo.Text <> "" Then
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                    CargarLineaDetalleProforma(producto, producto.Descripcion, txtCantidad.Text, decPrecioVenta, txtDescuento.Text)
                    txtCodigo.Text = ""
                    producto = Nothing
                    txtCodigo.Focus()
                End If
            End If
        Else
            Dim strError As String = ""
            If txtDescripcion.Text = "" Then strError = "La descripción no puede estar en blanco"
            If decPrecioVenta <= 0 Then strError = "El precio del producto no puede ser igual o menor a 0"
            If strError <> "" Then
                MessageBox.Show(strError, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDetalleProforma(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtDescuento.Text)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtUnidad.Text = ""
            txtDescuento.Text = "0"
            txtPrecio.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) 
        If grdDetalleProforma.Rows.Count > 0 Then
            dtbDetalleProforma.Rows.Remove(dtbDetalleProforma.Rows.Find(grdDetalleProforma.CurrentRow.Cells(0).Value))
            grdDetalleProforma.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub txtPorcDesc_Validated(sender As Object, e As EventArgs) 
        If txtDescuento.Text = "" Then txtDescuento.Text = "0"
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto) / (1 + (decTasaImpuesto / 100))
            If CDbl(txtDescuento.Text) > FrmPrincipal.empresaGlobal.PorcentajeDescMaximo Then
                MessageBox.Show("El porcentaje ingresado es mayor al parametro establecido para la empresa", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                txtDescuento.Text = "0"
                txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
            Else
                Dim decPorcDesc As Decimal = CDbl(txtDescuento.Text) / 100
                txtPrecio.Text = FormatNumber(decPrecioVenta - (decPrecioVenta * decPorcDesc), 2)
                If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
            End If
        End If
    End Sub

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) 
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) 
        If e.KeyCode = Keys.Tab Then
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) 
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) 
        sender.SelectAll()
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) 
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class