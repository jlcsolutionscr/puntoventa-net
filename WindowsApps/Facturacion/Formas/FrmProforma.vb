Imports System.Collections.Generic
Imports System.Globalization
Imports System.IO
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Utilitario

Public Class FrmProforma
#Region "Variables"
    Private decExcento, decGravado, decExonerado, decImpuesto, decTotal, decSubTotal, decPrecioVenta As Decimal
    Private I As Short
    Private dtbDetalleProforma As DataTable
    Private dtrRowDetProforma As DataRow
    Private proforma As Proforma
    Private detalleProforma As DetalleProforma
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private bolInit As Boolean = True
    Private bolAutorizando As Boolean = False
    Private provider As CultureInfo = CultureInfo.InvariantCulture
    'Impresion de tiquete
    Private comprobanteImpresion As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private arrDetalleOrden As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleProforma = New DataTable()
        dtbDetalleProforma.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("CODIGO", GetType(String))
        dtbDetalleProforma.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleProforma.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PRECIOIVA", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleProforma.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("PORCDESCUENTO", GetType(Decimal))
        dtbDetalleProforma.Columns.Add("VALORDESCUENTO", GetType(Decimal))
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleProforma.Columns.Clear()
        grdDetalleProforma.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPorcDescuento As New DataGridViewTextBoxColumn
        Dim dvcDescuento As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Width = 0
        dvcIdProducto.Visible = False
        grdDetalleProforma.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 110
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

        dvcPorcDescuento.DataPropertyName = "PORCDESCUENTO"
        dvcPorcDescuento.HeaderText = "Desc"
        dvcPorcDescuento.Width = 40
        dvcPorcDescuento.Visible = True
        dvcPorcDescuento.ReadOnly = False
        dvcPorcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcPorcDescuento)

        dvcDescuento.DataPropertyName = "VALORDESCUENTO"
        dvcDescuento.HeaderText = "Desc"
        dvcDescuento.Width = 75
        dvcDescuento.Visible = True
        dvcDescuento.ReadOnly = True
        dvcDescuento.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleProforma.Columns.Add(dvcDescuento)

        dvcPrecio.DataPropertyName = "PRECIOIVA"
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

    Private Sub CargarDetalleProforma(proforma As Proforma)
        dtbDetalleProforma.Rows.Clear()
        For Each detalle As DetalleProforma In proforma.DetalleProforma
            dtrRowDetProforma = dtbDetalleProforma.NewRow
            dtrRowDetProforma.Item(0) = detalle.IdProducto
            dtrRowDetProforma.Item(1) = detalle.Producto.Codigo
            dtrRowDetProforma.Item(2) = detalle.Descripcion
            dtrRowDetProforma.Item(3) = detalle.Cantidad
            dtrRowDetProforma.Item(4) = detalle.PrecioVenta
            dtrRowDetProforma.Item(5) = Math.Round(detalle.PrecioVenta * (1 + (detalle.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero)
            dtrRowDetProforma.Item(6) = dtrRowDetProforma.Item(3) * dtrRowDetProforma.Item(5)
            dtrRowDetProforma.Item(7) = detalle.Excento
            dtrRowDetProforma.Item(8) = detalle.PorcentajeIVA
            dtrRowDetProforma.Item(9) = detalle.PorcDescuento
            dtrRowDetProforma.Item(10) = (dtrRowDetProforma.Item(5) * 100 / (100 - detalle.PorcDescuento)) - dtrRowDetProforma.Item(5)
            dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        Next
        grdDetalleProforma.Refresh()
    End Sub

    Private Sub CargarLineaDetalleProforma(producto As Producto, strDescripcion As String, decCantidad As Decimal, decPrecio As Decimal, decPorcDesc As Decimal)
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.AplicaTasaDiferenciada Then decTasaImpuesto = cliente.ParametroImpuesto.TasaImpuesto
        Dim decPrecioGravado As Decimal = decPrecio
        If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 3, MidpointRounding.AwayFromZero)
        Dim intIndice As Integer = ObtenerIndice(dtbDetalleProforma, producto.IdProducto)
        If producto.Tipo = 1 And intIndice >= 0 Then
            Dim decNewCantidad = dtbDetalleProforma.Rows(intIndice).Item(3) + decCantidad
            dtbDetalleProforma.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleProforma.Rows(intIndice).Item(3) = decNewCantidad
            dtbDetalleProforma.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleProforma.Rows(intIndice).Item(5) = decPrecio
            dtbDetalleProforma.Rows(intIndice).Item(6) = decNewCantidad * decPrecio
            dtbDetalleProforma.Rows(intIndice).Item(7) = decTasaImpuesto = 0
            dtbDetalleProforma.Rows(intIndice).Item(8) = decTasaImpuesto
            dtbDetalleProforma.Rows(intIndice).Item(9) = decPorcDesc
            dtbDetalleProforma.Rows(intIndice).Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
        Else
            dtrRowDetProforma = dtbDetalleProforma.NewRow
            dtrRowDetProforma.Item(0) = producto.IdProducto
            dtrRowDetProforma.Item(1) = producto.Codigo
            dtrRowDetProforma.Item(2) = strDescripcion
            dtrRowDetProforma.Item(3) = decCantidad
            dtrRowDetProforma.Item(4) = decPrecioGravado
            dtrRowDetProforma.Item(5) = decPrecio
            dtrRowDetProforma.Item(6) = decCantidad * decPrecio
            dtrRowDetProforma.Item(7) = decTasaImpuesto = 0
            dtrRowDetProforma.Item(8) = decTasaImpuesto
            dtrRowDetProforma.Item(9) = decPorcDesc
            dtrRowDetProforma.Item(10) = (decPrecio * 100 / (100 - decPorcDesc)) - decPrecio
            dtbDetalleProforma.Rows.Add(dtrRowDetProforma)
        End If
        grdDetalleProforma.Refresh()
        CargarTotales()
    End Sub

    Private Function ObtenerIndice(table As DataTable, intValor As Integer) As Integer
        Dim intIndice As Integer = -1
        Dim intPosicion As Integer = 0
        For Each row As DataRow In table.Rows
            If row(0) = intValor Then intIndice = intPosicion
            intPosicion += 1
        Next
        Return intIndice
    End Function

    Private Sub CargarTotales()
        decSubTotal = 0
        decGravado = 0
        decExonerado = 0
        decExcento = 0
        decImpuesto = 0
        Dim intPorcentajeExoneracion As Integer = 0
        If txtPorcentajeExoneracion.Text <> "" Then intPorcentajeExoneracion = CInt(txtPorcentajeExoneracion.Text)
        For I = 0 To dtbDetalleProforma.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleProforma.Rows(I).Item(8)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleProforma.Rows(I).Item(4) * decTasaImpuesto / 100
                If intPorcentajeExoneracion > 0 Then
                    Dim decGravadoPorcentual = dtbDetalleProforma.Rows(I).Item(4) * (1 - (intPorcentajeExoneracion / 100))
                    decGravado += Math.Round(decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(3)
                    decExonerado += Math.Round(dtbDetalleProforma.Rows(I).Item(4) - decGravadoPorcentual, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(3)
                    decImpuestoProducto = decGravadoPorcentual * decTasaImpuesto / 100
                Else
                    decGravado += Math.Round(dtbDetalleProforma.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(3)
                End If
                decImpuesto += Math.Round(decImpuestoProducto, 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(3)
            Else
                decExcento += Math.Round(dtbDetalleProforma.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleProforma.Rows(I).Item(3)
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

    Private Function ObtenerPrecioVentaPorCliente(cliente As Cliente, producto As Producto)
        Dim decPrecioVenta As Decimal
        If cliente IsNot Nothing Then
            If cliente.IdTipoPrecio = 1 Then
                decPrecioVenta = producto.PrecioVenta1
            ElseIf cliente.IdTipoPrecio = 2 Then
                decPrecioVenta = producto.PrecioVenta2
            ElseIf cliente.IdTipoPrecio = 3 Then
                decPrecioVenta = producto.PrecioVenta3
            ElseIf cliente.IdTipoPrecio = 4 Then
                decPrecioVenta = producto.PrecioVenta4
            ElseIf cliente.IdTipoPrecio = 5 Then
                decPrecioVenta = producto.PrecioVenta5
            Else
                decPrecioVenta = producto.PrecioVenta1
            End If
        Else
            decPrecioVenta = producto.PrecioVenta1
        End If
        If cliente.AplicaTasaDiferenciada Then
            decPrecioVenta = Math.Round(decPrecioVenta / (1 + (producto.ParametroImpuesto.TasaImpuesto / 100)), 3)
            decPrecioVenta = Math.Round(decPrecioVenta * (1 + (cliente.ParametroImpuesto.TasaImpuesto / 100)), 2)
        End If
        Return decPrecioVenta
    End Function

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtPrecio.Text = FormatNumber(0, 2)
            txtUnidad.Text = ""
            txtCodigo.Focus()
            Exit Sub
        Else
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            txtCodigo.Text = producto.Codigo
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtDescripcion.Text = producto.Descripcion
            txtExistencias.Text = producto.Existencias
            decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
            txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
            txtUnidad.Text = IIf(producto.Tipo = 1, "UND", IIf(producto.Tipo = 2, "SP", "OS"))
        End If
    End Sub

    Private Async Function CargarCombos() As Task
        cboTipoMoneda.ValueMember = "Id"
        cboTipoMoneda.DisplayMember = "Descripcion"
        cboTipoMoneda.DataSource = Await Puntoventa.ObtenerListadoTipoMoneda(FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmProforma_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmProforma_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F1 Then
            BtnBusProd_Click(btnBusProd, New EventArgs())
        ElseIf e.KeyCode = Keys.F2 Then
            If FrmPrincipal.productoTranstorio IsNot Nothing Then
                Dim formCargar As New FrmCargaProductoTransitorio
                formCargar.ShowDialog()
                If FrmPrincipal.productoTranstorio.PrecioVenta1 > 0 Then
                    CargarLineaDetalleProforma(FrmPrincipal.productoTranstorio, FrmPrincipal.productoTranstorio.Descripcion, FrmPrincipal.productoTranstorio.Existencias, FrmPrincipal.productoTranstorio.PrecioVenta1, 0)
                End If
            End If
        ElseIf e.KeyCode = Keys.F3 Then
            BtnBuscar_Click(btnBuscar, New EventArgs())
        ElseIf e.KeyCode = Keys.F4 Then
            BtnAgregar_Click(btnAgregar, New EventArgs())
        ElseIf e.KeyCode = Keys.F10 And btnGuardar.Enabled Then
            BtnGuardar_Click(btnGuardar, New EventArgs())
        ElseIf e.KeyCode = Keys.F11 And btnImprimir.Enabled Then
            BtnImprimir_Click(btnImprimir, New EventArgs())
        End If
        e.Handled = False
    End Sub

    Private Async Sub FrmProforma_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            Await CargarCombos()
            cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            grdDetalleProforma.DataSource = dtbDetalleProforma
            bolInit = False
            txtCantidad.Text = "1"
            txtPorcDesc.Text = "0"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            cliente = New Cliente With {
                .IdCliente = 1,
                .Nombre = "CLIENTE DE CONTADO",
                .Telefono = "",
                .IdTipoExoneracion = 1,
                .PorcentajeExoneracion = 0,
                .NombreInstExoneracion = "",
                .NumDocExoneracion = "",
                .FechaEmisionDoc = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
            }
            txtNombreCliente.Text = cliente.Nombre
            If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
                Try
                    vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                    txtVendedor.Text = vendedor.Nombre
                Catch ex As Exception
                    Throw New Exception("Debe agregar al menos un vendedor al catalogo de vendedores para poder continuar.")
                End Try
            End If
            If FrmPrincipal.bolModificaDescripcion Then txtDescripcion.ReadOnly = False
            If FrmPrincipal.bolAplicaDescuento Then txtPorcDesc.ReadOnly = False
            If FrmPrincipal.bolModificaPrecioVenta Then txtPrecio.ReadOnly = False
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdProforma.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cboTipoMoneda.SelectedValue = FrmPrincipal.empresaGlobal.IdTipoMoneda
        cboTipoMoneda.Enabled = True
        txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        txtTextoAdicional.Text = ""
        txtTelefono.Text = ""
        txtTipoExoneracion.Text = ""
        txtNumDocExoneracion.Text = ""
        txtNombreInstExoneracion.Text = ""
        txtFechaExoneracion.Text = ""
        txtPorcentajeExoneracion.Text = ""
        dtbDetalleProforma.Rows.Clear()
        grdDetalleProforma.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtExistencias.Text = ""
        txtPorcDesc.Text = "0"
        txtPrecio.Text = ""
        decTotal = 0
        btnGuardar.Enabled = True
        btnAnular.Enabled = False
        btnImprimir.Enabled = False
        btnGenerarPDF.Enabled = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        cliente = New Cliente With {
            .IdCliente = 1,
            .Nombre = "CLIENTE DE CONTADO",
            .Telefono = "",
            .IdTipoExoneracion = 1,
            .PorcentajeExoneracion = 0,
            .NombreInstExoneracion = "",
            .NumDocExoneracion = "",
            .FechaEmisionDoc = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
        }
        txtNombreCliente.Text = cliente.Nombre
        txtNombreCliente.ReadOnly = False
        If FrmPrincipal.empresaGlobal.AsignaVendedorPorDefecto Then
            Try
                vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                MessageBox.Show("Debe agregar al menos un vendedor al catalogo de vendedores para poder continuar.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
        Else
            vendedor = Nothing
            txtVendedor.Text = ""
        End If
        bolInit = False
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdProforma.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularProforma(proforma.IdProforma, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaProforma()
        formBusqueda.bolIncluyeEstado = True
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                proforma = Await Puntoventa.ObtenerProforma(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If proforma IsNot Nothing Then
                bolInit = True
                txtIdProforma.Text = proforma.ConsecProforma
                cliente = proforma.Cliente
                txtNombreCliente.Text = proforma.NombreCliente
                txtFecha.Text = proforma.Fecha
                cboTipoMoneda.SelectedValue = proforma.IdTipoMoneda
                txtTextoAdicional.Text = proforma.TextoAdicional
                txtTelefono.Text = proforma.Telefono
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = cliente.ParametroExoneracion.Descripcion
                    txtNumDocExoneracion.Text = cliente.NumDocExoneracion
                    txtNombreInstExoneracion.Text = cliente.NombreInstExoneracion
                    txtFechaExoneracion.Text = cliente.FechaEmisionDoc
                    txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                End If
                vendedor = proforma.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleProforma(proforma)
                CargarTotales()
                cboTipoMoneda.Enabled = False
                txtNombreCliente.ReadOnly = True
                btnImprimir.Enabled = True
                btnGenerarPDF.Enabled = True
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnGuardar.Enabled = proforma.Aplicado = False
                btnAnular.Enabled = proforma.Aplicado = False And FrmPrincipal.bolAnularTransacciones
                bolInit = False
            Else
                MessageBox.Show("No existe registro de proforma asociado al identificador seleccionado", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        End If
    End Sub

    Private Async Sub BtnBuscaVendedor_Click(sender As Object, e As EventArgs) Handles btnBuscaVendedor.Click
        Dim formBusquedaVendedor As New FrmBusquedaVendedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaVendedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                vendedor = Await Puntoventa.ObtenerVendedor(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If vendedor Is Nothing Then
                MessageBox.Show("El vendedor seleccionado no existe. Consulte a su proveedor.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
    End Sub

    Private Async Sub BtnBuscarCliente_Click(sender As Object, e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                cliente = Await Puntoventa.ObtenerCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
                txtNombreCliente.Text = cliente.Nombre
                txtNombreCliente.ReadOnly = True
                txtTelefono.Text = cliente.Telefono
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
                If cliente.PorcentajeExoneracion > 0 Then
                    txtTipoExoneracion.Text = cliente.ParametroExoneracion.Descripcion
                    txtNumDocExoneracion.Text = cliente.NumDocExoneracion
                    txtNombreInstExoneracion.Text = cliente.NombreInstExoneracion
                    txtFechaExoneracion.Text = cliente.FechaEmisionDoc
                    txtPorcentajeExoneracion.Text = cliente.PorcentajeExoneracion
                Else
                    txtTipoExoneracion.Text = ""
                    txtNumDocExoneracion.Text = ""
                    txtNombreInstExoneracion.Text = ""
                    txtFechaExoneracion.Text = ""
                    txtPorcentajeExoneracion.Text = ""
                End If
                CargarTotales()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If cliente Is Nothing Then
                MessageBox.Show("El cliente de contado no se encuentra registrado en el sistema. Consulte a su proveedor.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
    End Sub

    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = True
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

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If vendedor Is Nothing Then
            MessageBox.Show("Debe seleccionar el vendedor para poder guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            BtnBuscaVendedor_Click(btnBuscaVendedor, New EventArgs())
            Exit Sub
        ElseIf decTotal = 0 Then
            MessageBox.Show("Debe agregar líneas de detalle para guardar el registro.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        If txtIdProforma.Text = "" Then
            proforma = New Proforma With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdTipoMoneda = cboTipoMoneda.SelectedValue,
                .IdCliente = cliente.IdCliente,
                .NombreCliente = txtNombreCliente.Text,
                .Fecha = Now(),
                .TextoAdicional = txtTextoAdicional.Text,
                .Telefono = txtTelefono.Text,
                .IdVendedor = vendedor.IdVendedor,
                .Excento = decExcento,
                .Gravado = decGravado,
                .Exonerado = decExonerado,
                .Descuento = 0,
                .Impuesto = decImpuesto,
                .Nulo = False
            }
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleProforma = New DetalleProforma With {
                    .IdProducto = dtbDetalleProforma.Rows(I).Item(0),
                    .Descripcion = dtbDetalleProforma.Rows(I).Item(2),
                    .Cantidad = dtbDetalleProforma.Rows(I).Item(3),
                    .PrecioVenta = dtbDetalleProforma.Rows(I).Item(4),
                    .Excento = dtbDetalleProforma.Rows(I).Item(7),
                    .PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(8),
                    .PorcDescuento = dtbDetalleProforma.Rows(I).Item(9)
                }
                proforma.DetalleProforma.Add(detalleProforma)
            Next
            Try
                Dim strIdConsec As String = Await Puntoventa.AgregarProforma(proforma, FrmPrincipal.usuarioGlobal.Token)
                Dim arrIdConsec = strIdConsec.Split("-")
                proforma.IdProforma = arrIdConsec(0)
                proforma.ConsecProforma = arrIdConsec(1)
                txtIdProforma.Text = proforma.ConsecProforma
            Catch ex As Exception
                txtIdProforma.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            proforma.TextoAdicional = txtTextoAdicional.Text
            proforma.Excento = decExcento
            proforma.Gravado = decGravado
            proforma.Descuento = 0
            proforma.Impuesto = CDbl(txtImpuesto.Text)
            proforma.DetalleProforma.Clear()
            For I = 0 To dtbDetalleProforma.Rows.Count - 1
                detalleProforma = New DetalleProforma
                detalleProforma.IdProforma = proforma.IdProforma
                detalleProforma.IdProducto = dtbDetalleProforma.Rows(I).Item(0)
                detalleProforma.Descripcion = dtbDetalleProforma.Rows(I).Item(2)
                detalleProforma.Cantidad = dtbDetalleProforma.Rows(I).Item(3)
                detalleProforma.PrecioVenta = dtbDetalleProforma.Rows(I).Item(4)
                detalleProforma.Excento = dtbDetalleProforma.Rows(I).Item(7)
                detalleProforma.PorcentajeIVA = dtbDetalleProforma.Rows(I).Item(8)
                detalleProforma.PorcDescuento = dtbDetalleProforma.Rows(I).Item(9)
                proforma.DetalleProforma.Add(detalleProforma)
            Next
            Try
                Await Puntoventa.ActualizarProforma(proforma, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        btnImprimir.Enabled = True
        btnImprimir.Focus()
        btnGenerarPDF.Enabled = True
        btnGuardar.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        cboTipoMoneda.Enabled = False
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Async Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdProforma.Text <> "" Then
            If proforma.ConsecProforma = 0 Then
                Try
                    proforma = Await Puntoventa.ObtenerProforma(txtIdProforma.Text, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            Try
                comprobanteImpresion = New ModuloImpresion.ClsComprobante With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strId = proforma.ConsecProforma,
                    .strVendedor = txtVendedor.Text,
                    .strNombre = txtNombreCliente.Text,
                    .strTelefono = txtTelefono.Text,
                    .strDocumento = "",
                    .strFecha = proforma.Fecha.ToString("dd/MM/yyyy hh:mm:ss"),
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
                    .strExcento = IIf(dtbDetalleProforma.Rows(I).Item(7) = 0, "G", "E")
                }
                    arrDetalleOrden.Add(detalleComprobante)
                Next
                comprobanteImpresion.arrDetalleComprobante = arrDetalleOrden
                ModuloImpresion.ImprimirProforma(comprobanteImpresion)
            Catch ex As Exception
                MessageBox.Show("Error al tratar de imprimir: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Async Sub BtnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If txtIdProforma.Text <> "" Then
            If proforma.ConsecProforma = 0 Then
                Try
                    proforma = Await Puntoventa.ObtenerProforma(txtIdProforma.Text, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            Dim datos As EstructuraPDF = New EstructuraPDF()
            Try
                Dim poweredByImage As Image = My.Resources.logo
                datos.PoweredByLogotipo = poweredByImage
            Catch ex As Exception
                datos.PoweredByLogotipo = Nothing
            End Try
            Try
                Dim logotipo As Byte() = Await Puntoventa.ObtenerLogotipoEmpresa(proforma.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
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
            datos.ConsecInterno = proforma.ConsecProforma
            datos.Consecutivo = Nothing
            datos.Clave = Nothing
            datos.CondicionVenta = "Proforma"
            datos.PlazoCredito = ""
            datos.Fecha = proforma.Fecha.ToString("dd/MM/yyyy hh:mm:ss")
            datos.MedioPago = ""
            datos.NombreEmisor = FrmPrincipal.empresaGlobal.NombreEmpresa
            datos.NombreComercialEmisor = FrmPrincipal.empresaGlobal.NombreComercial
            datos.IdentificacionEmisor = FrmPrincipal.empresaGlobal.Identificacion
            datos.CorreoElectronicoEmisor = FrmPrincipal.empresaGlobal.CorreoNotificacion
            datos.TelefonoEmisor = FrmPrincipal.empresaGlobal.Telefono1 + IIf(FrmPrincipal.empresaGlobal.Telefono2.Length > 0, " - " + FrmPrincipal.empresaGlobal.Telefono2, "")
            datos.FaxEmisor = ""
            datos.ProvinciaEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Provincia.Descripcion
            datos.CantonEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Canton.Descripcion
            datos.DistritoEmisor = FrmPrincipal.empresaGlobal.Barrio.Distrito.Descripcion
            datos.BarrioEmisor = FrmPrincipal.empresaGlobal.Barrio.Descripcion
            datos.DireccionEmisor = FrmPrincipal.empresaGlobal.Direccion
            If proforma.IdCliente > 1 Then
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
            If (proforma.TextoAdicional IsNot Nothing) Then datos.OtrosTextos = proforma.TextoAdicional
            datos.TotalGravado = decGravado.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalExonerado = decExonerado.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalExento = decExcento.ToString("N2", CultureInfo.InvariantCulture)
            datos.Descuento = "0.00"
            datos.Impuesto = decImpuesto.ToString("N2", CultureInfo.InvariantCulture)
            datos.TotalGeneral = decTotal.ToString("N2", CultureInfo.InvariantCulture)
            datos.CodigoMoneda = IIf(proforma.IdTipoMoneda = 1, "CRC", "USD")
            datos.TipoDeCambio = 1
            Try
                Dim pdfBytes As Byte() = UtilitarioPDF.GenerarPDF(datos)
                Dim pdfFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\PROFORMA-" + txtIdProforma.Text + ".pdf"
                File.WriteAllBytes(pdfFilePath, pdfBytes)
                Process.Start(pdfFilePath)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto IsNot Nothing Then
            Dim strError As String = ""
            If txtDescripcion.Text = "" Then strError = "La descripción no puede estar en blanco"
            If decPrecioVenta <= 0 Then strError = "El precio del producto no puede ser igual o menor a 0"
            If strError <> "" Then
                MessageBox.Show(strError, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            CargarLineaDetalleProforma(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta, txtPorcDesc.Text)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtExistencias.Text = ""
            txtUnidad.Text = ""
            txtPorcDesc.Text = "0"
            txtPrecio.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleProforma.Rows.Count > 0 Then
            dtbDetalleProforma.Rows.RemoveAt(grdDetalleProforma.CurrentRow.Index)
            grdDetalleProforma.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio.KeyUp
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text), 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Sub cboTipoMoneda_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoMoneda.SelectedIndexChanged
        If Not bolInit And Not cboTipoMoneda.SelectedValue Is Nothing Then
            txtTipoCambio.Text = IIf(cboTipoMoneda.SelectedValue = 1, 1, FrmPrincipal.decTipoCambioDolar.ToString())
        End If
    End Sub

    Private Sub grdDetalleProforma_EditingControlShowing(sender As Object, e As DataGridViewEditingControlShowingEventArgs) Handles grdDetalleProforma.EditingControlShowing
        If grdDetalleProforma.CurrentCell.ColumnIndex = 4 Then
            Dim tb As TextBox = e.Control
            If tb IsNot Nothing Then
                AddHandler CType(e.Control, TextBox).KeyPress, AddressOf TextBox_keyPress
            End If
        End If
    End Sub

    Private Sub TextBox_keyPress(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        If Char.IsDigit(CChar(CStr(e.KeyChar))) = False Then e.Handled = True
    End Sub

    Private Async Sub grdDetalleProforma_CellValueChanged(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleProforma.CellValueChanged
        If e.ColumnIndex = 4 And Not bolAutorizando Then
            bolAutorizando = True
            Dim decPorcDesc As Decimal = 0
            If Not IsDBNull(grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value) Then
                decPorcDesc = grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value
            End If
            If Not FrmPrincipal.bolAplicaDescuento And decPorcDesc > FrmPrincipal.empresaGlobal.PorcentajeDescMaximo Then
                If MessageBox.Show("El porcentaje ingresado es mayor al parámetro establecido para la empresa. Desea ingresar una autorización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    FrmPrincipal.strCodigoUsuario = ""
                    FrmPrincipal.strContrasena = ""
                    FrmPrincipal.strBusqueda = ""
                    Dim formAutorizacion As New FrmAutorizaPrecio
                    formAutorizacion.ShowDialog()
                    If FrmPrincipal.strCodigoUsuario <> "" And FrmPrincipal.strContrasena <> "" And FrmPrincipal.strBusqueda <> "" Then
                        Dim autorizado As Boolean
                        Try
                            autorizado = Await Puntoventa.AutorizacionPrecioExtraordinario(FrmPrincipal.strCodigoUsuario, FrmPrincipal.strContrasena, FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                        Catch ex As Exception
                            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                            Exit Sub
                        End Try
                        If autorizado Then
                            decPorcDesc = FrmPrincipal.strBusqueda
                            grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = FrmPrincipal.strBusqueda
                        Else
                            MessageBox.Show("Los credenciales ingresados son incorrectos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                            decPorcDesc = 0
                            grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                        End If
                    End If
                Else
                    decPorcDesc = 0
                    grdDetalleProforma.Rows(e.RowIndex).Cells(e.ColumnIndex).Value = 0
                End If
            End If
            Dim decCantidad As Decimal = grdDetalleProforma.Rows(e.RowIndex).Cells(3).Value
            Dim decTasaImpuesto As Decimal = grdDetalleProforma.Rows(e.RowIndex).Cells(9).Value
            Dim decPrecio As Decimal = grdDetalleProforma.Rows(e.RowIndex).Cells(6).Value + grdDetalleProforma.Rows(e.RowIndex).Cells(5).Value
            Dim decMontoDesc = decPrecio / 100 * decPorcDesc
            decPrecio = decPrecio - decMontoDesc
            Dim decPrecioGravado As Decimal = decPrecio
            If decTasaImpuesto > 0 Then decPrecioGravado = Math.Round(decPrecio / (1 + (decTasaImpuesto / 100)), 3, MidpointRounding.AwayFromZero)
            dtbDetalleProforma.Rows(e.RowIndex).Item(4) = decPrecioGravado
            dtbDetalleProforma.Rows(e.RowIndex).Item(5) = decPrecio
            dtbDetalleProforma.Rows(e.RowIndex).Item(6) = decCantidad * decPrecio
            dtbDetalleProforma.Rows(e.RowIndex).Item(9) = decPorcDesc
            dtbDetalleProforma.Rows(e.RowIndex).Item(10) = decMontoDesc
            CargarTotales()
            bolAutorizando = False
        End If
    End Sub

    Private Async Sub txtPorcDesc_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPorcDesc.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            If txtPorcDesc.Text = "" Then txtPorcDesc.Text = "0"
            If producto IsNot Nothing Then
                decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                If Not FrmPrincipal.bolAplicaDescuento And CDbl(txtPorcDesc.Text) > FrmPrincipal.empresaGlobal.PorcentajeDescMaximo Then
                    If MessageBox.Show("El porcentaje ingresado es mayor al parámetro establecido para la empresa. Desea ingresar una autorización?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        FrmPrincipal.strCodigoUsuario = ""
                        FrmPrincipal.strContrasena = ""
                        FrmPrincipal.strBusqueda = ""
                        Dim formAutorizacion As New FrmAutorizaPrecio
                        formAutorizacion.ShowDialog()
                        If FrmPrincipal.strCodigoUsuario <> "" And FrmPrincipal.strContrasena <> "" And FrmPrincipal.strBusqueda <> "" Then
                            Dim autorizado As Boolean
                            Try
                                autorizado = Await Puntoventa.AutorizacionPrecioExtraordinario(FrmPrincipal.strCodigoUsuario, FrmPrincipal.strContrasena, FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                            Catch ex As Exception
                                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                                Exit Sub
                            End Try
                            If autorizado Then
                                txtPorcDesc.Text = FrmPrincipal.strBusqueda
                            Else
                                MessageBox.Show("Los credenciales ingresados son incorrectos.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                                txtPorcDesc.Text = 0
                            End If
                        End If
                    Else
                        txtPorcDesc.Text = 0
                    End If
                End If
                Dim decPorcDesc As Decimal = CDbl(txtPorcDesc.Text) / 100
                decPrecioVenta -= (decPrecioVenta * decPorcDesc)
                txtPrecio.Text = FormatNumber(decPrecioVenta, 2)
                If e.KeyCode = Keys.Enter Then BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub TxtPrecio_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtPrecio.PreviewKeyDown
        If producto IsNot Nothing Then
            If e.KeyCode = Keys.Enter Then
                BtnInsertar_Click(btnInsertar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
        If e.KeyCode = Keys.Enter Or e.KeyCode = Keys.Tab Then
            Try
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    If producto.Activo Then
                        CargarDatosProducto(producto)
                        txtCantidad.Focus()
                    End If
                Else
                    txtCodigo.Text = ""
                    txtDescripcion.Text = ""
                    txtExistencias.Text = ""
                    txtCantidad.Text = "1"
                    txtUnidad.Text = ""
                    txtPorcDesc.Text = "0"
                    txtPrecio.Text = ""
                    txtCodigo.Focus()
                End If
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub TxtCantidad_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCantidad.PreviewKeyDown
        If e.KeyCode = Keys.Enter Then
            If producto IsNot Nothing Then
                If CDbl(txtPrecio.Text) > 0 Then
                    BtnInsertar_Click(btnInsertar, New EventArgs())
                Else
                    txtPrecio.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) Handles txtCantidad.MouseDown, txtCodigo.MouseDown, txtDescripcion.MouseDown, txtPrecio.MouseDown
        sender.SelectAll()
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs) Handles txtTelefono.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPorcDesc.KeyPress, txtPrecio.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class