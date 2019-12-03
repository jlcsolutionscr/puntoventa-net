Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmOrdenServicio
#Region "Variables"
    Private strMotivoRechazo As String
    Private decSubTotal, decExcento, decGravado, decImpuesto, decTotal, decPrecioVenta As Decimal
    Private I As Short
    Private dtbDatosLocal, dtbDetalleOrdenServicio As DataTable
    Private dtrRowDetOrdenServicio As DataRow
    Private arrDetalleOrdenServicio As ArrayList
    Private ordenServicio As OrdenServicio
    Private detalleOrdenServicio As DetalleOrdenServicio
    Private producto As Producto
    Private cliente As Cliente
    Private vendedor As Vendedor
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private bolInit As Boolean = True

    Private formReport As New FrmReportViewer()
    Private dtbDatos As DataTable
    Private strEmpresa, strUsuario, strTelefonos As String
#End Region

#Region "Métodos"
    Private Sub IniciaTablasDeDetalle()
        dtbDetalleOrdenServicio = New DataTable()
        dtbDetalleOrdenServicio.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("CODIGO", GetType(String))
        dtbDetalleOrdenServicio.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleOrdenServicio.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleOrdenServicio.PrimaryKey = {dtbDetalleOrdenServicio.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleOrdenServicio.Columns.Clear()
        grdDetalleOrdenServicio.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecio As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Width = 0
        dvcIdProducto.Visible = False
        grdDetalleOrdenServicio.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 225
        dvcCodigo.Visible = True
        dvcCodigo.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 300
        dvcDescripcion.Visible = True
        dvcDescripcion.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.Visible = True
        dvcCantidad.ReadOnly = True
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcCantidad)

        dvcPrecio.DataPropertyName = "PRECIO"
        dvcPrecio.HeaderText = "Precio/U"
        dvcPrecio.Width = 75
        dvcPrecio.Visible = True
        dvcPrecio.ReadOnly = True
        dvcPrecio.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcPrecio)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.Visible = True
        dvcTotal.ReadOnly = True
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleOrdenServicio.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.Visible = True
        dvcExc.ReadOnly = True
        grdDetalleOrdenServicio.Columns.Add(dvcExc)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        grdDetalleOrdenServicio.Columns.Add(dvcPorcentajeIVA)
    End Sub

    Private Sub CargarDetalleOrdenServicio(ByVal ordenServicio As OrdenServicio)
        dtbDetalleOrdenServicio.Rows.Clear()
        For Each detalle As DetalleOrdenServicio In ordenServicio.DetalleOrdenServicio
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = detalle.IdProducto
            dtrRowDetOrdenServicio.Item(1) = detalle.Producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = detalle.Descripcion
            dtrRowDetOrdenServicio.Item(3) = detalle.Cantidad
            dtrRowDetOrdenServicio.Item(4) = detalle.PrecioVenta
            dtrRowDetOrdenServicio.Item(5) = dtrRowDetOrdenServicio.Item(3) * dtrRowDetOrdenServicio.Item(4)
            dtrRowDetOrdenServicio.Item(6) = detalle.Excento
            dtrRowDetOrdenServicio.Item(7) = detalle.PorcentajeIVA
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        Next
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Sub CargarLineaDetalleOrdenServicio(producto As Producto, strDescripcion As String, dblCantidad As Double, dblPrecio As Double)
        Dim intIndice As Integer = dtbDetalleOrdenServicio.Rows.IndexOf(dtbDetalleOrdenServicio.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        Dim decPrecioGravado As Decimal = dblPrecio
        If decTasaImpuesto > 0 Then
            decPrecioGravado = Math.Round(dblPrecio / (1 + (decTasaImpuesto / 100)), 3, MidpointRounding.AwayFromZero)
            If cliente.AplicaTasaDiferenciada Then decTasaImpuesto = cliente.ParametroImpuesto.TasaImpuesto
        End If
        If intIndice >= 0 Then
            dtbDetalleOrdenServicio.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenServicio.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(3) += dblCantidad
            dtbDetalleOrdenServicio.Rows(intIndice).Item(4) = decPrecioGravado
            dtbDetalleOrdenServicio.Rows(intIndice).Item(5) = dblCantidad * decPrecioGravado
            dtbDetalleOrdenServicio.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleOrdenServicio.Rows(intIndice).Item(7) = decTasaImpuesto
        Else
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = producto.IdProducto
            dtrRowDetOrdenServicio.Item(1) = producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = strDescripcion
            dtrRowDetOrdenServicio.Item(3) = dblCantidad
            dtrRowDetOrdenServicio.Item(4) = decPrecioGravado
            dtrRowDetOrdenServicio.Item(5) = dblCantidad * decPrecioGravado
            dtrRowDetOrdenServicio.Item(6) = decTasaImpuesto = 0
            dtrRowDetOrdenServicio.Item(7) = decTasaImpuesto
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        End If
        grdDetalleOrdenServicio.Refresh()
        CargarTotales()
    End Sub

    Private Sub CargarTotales()
        decSubTotal = 0
        decGravado = 0
        decExcento = 0
        decImpuesto = 0
        For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
            Dim decTasaImpuesto As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(7)
            If decTasaImpuesto > 0 Then
                Dim decImpuestoProducto As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(4) * decTasaImpuesto / 100
                decGravado += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
                decImpuesto += Math.Round(decImpuestoProducto, 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            Else
                decExcento += Math.Round(dtbDetalleOrdenServicio.Rows(I).Item(4), 2, MidpointRounding.AwayFromZero) * dtbDetalleOrdenServicio.Rows(I).Item(3)
            End If
        Next
        decSubTotal = decGravado + decExcento
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
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
        Return decPrecioVenta
    End Function

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecio.Text = FormatNumber(0, 2)
            txtUnidad.Text = ""
            txtCodigo.Focus()
            Exit Sub
        Else
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            txtCodigo.Text = producto.Codigo
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtDescripcion.Text = producto.Descripcion
            decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
            txtPrecio.Text = FormatNumber(decPrecioVenta / (1 + (decTasaImpuesto / 100)), 2)
            txtUnidad.Text = IIf(producto.Tipo = 1, "UND", "SP")
            If FrmPrincipal.empresaGlobal.ModificaDescProducto = True Then
                txtDescripcion.Focus()
            Else
                txtPrecio.Focus()
            End If
        End If
    End Sub

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, 1, 0, True, FrmPrincipal.usuarioGlobal.Token)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmFactura_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then Await CargarAutoCompletarProducto()
            If FrmPrincipal.empresaGlobal.ModificaDescProducto = True Then txtDescripcion.ReadOnly = False
            IniciaTablasDeDetalle()
            EstablecerPropiedadesDataGridView()
            grdDetalleOrdenServicio.DataSource = dtbDetalleOrdenServicio
            bolInit = False
            txtCantidad.Text = "1"
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
            Try
                cliente = New Cliente With {
                    .IdCliente = 1,
                    .Nombre = "CLIENTE DE CONTADO"
                }
                txtNombreCliente.Text = cliente.Nombre
            Catch ex As Exception
                Throw New Exception("Error al consultar el cliente de contado. Por favor consulte con su proveedor.")
            End Try
            Try
                vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
                txtVendedor.Text = vendedor.Nombre
            Catch ex As Exception
                Throw New Exception("Debe ingresar al menos un vendedor para generar la facturación. . .")
            End Try
            txtCodigo.Focus()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        bolInit = True
        txtIdOrdenServicio.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtObservaciones1.Text = ""
        txtObservaciones2.Text = ""
        txtObservaciones3.Text = ""
        txtObservaciones4.Text = ""
        txtObservaciones5.Text = ""
        dtbDetalleOrdenServicio.Rows.Clear()
        grdDetalleOrdenServicio.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPrecio.Text = ""
        decTotal = 0
        btnAnular.Enabled = False
        btnImprimir.Enabled = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        Try
            cliente = New Cliente With {
                .IdCliente = 1,
                .Nombre = "CLIENTE DE CONTADO"
            }
            txtNombreCliente.Text = cliente.Nombre
        Catch ex As Exception
            MessageBox.Show("Error al consultar el cliente de contado. Por favor consulte con su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            vendedor = Await Puntoventa.ObtenerVendedorPorDefecto(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
            txtVendedor.Text = vendedor.Nombre
        Catch ex As Exception
            MessageBox.Show("Debe ingresar al menos un vendedor para generar la facturación. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        bolInit = False
        txtCodigo.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdOrdenServicio.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularOrdenServicio(txtIdOrdenServicio.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                BtnAgregar_Click(btnAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub BtnBuscar_Click(sender As Object, e As EventArgs) Handles btnBuscar.Click
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ordenServicio = Await Puntoventa.ObtenerOrdenServicio(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenServicio IsNot Nothing Then
                bolInit = True
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
                cliente = ordenServicio.Cliente
                txtNombreCliente.Text = ordenServicio.Cliente.Nombre
                txtFecha.Text = ordenServicio.Fecha
                txtObservaciones1.Text = ordenServicio.Observaciones1
                txtObservaciones2.Text = ordenServicio.Observaciones2
                txtObservaciones3.Text = ordenServicio.Observaciones3
                txtObservaciones4.Text = ordenServicio.Observaciones4
                txtObservaciones5.Text = ordenServicio.Observaciones5
                vendedor = ordenServicio.Vendedor
                txtVendedor.Text = IIf(vendedor IsNot Nothing, vendedor.Nombre, "")
                CargarDetalleOrdenServicio(ordenServicio)
                CargarTotales()
                btnImprimir.Enabled = True
                btnBuscaVendedor.Enabled = False
                btnBuscarCliente.Enabled = False
                btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                bolInit = False
            Else
                MessageBox.Show("No existe registro de OrdenServicio asociado al identificador seleccionado", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If vendedor Is Nothing Then
                MessageBox.Show("El vendedor seleccionado no existe. Consulte a su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                If cliente.Vendedor IsNot Nothing Then
                    vendedor = cliente.Vendedor
                    txtVendedor.Text = vendedor.Nombre
                End If
                CargarTotales()
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If cliente Is Nothing Then
                MessageBox.Show("El cliente de contado no se encuentra registrado en el sistema. Consulte a su proveedor.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
    End Sub

    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = True,
            .intTipoPrecio = 0
        }
        FrmPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmPrincipal.strBusqueda.Equals("") Then
            Dim intIdProducto As Integer = Integer.Parse(FrmPrincipal.strBusqueda)
            Try
                producto = Await Puntoventa.ObtenerProducto(intIdProducto, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show("Error al obtener la información del producto seleccionado. Intente mas tarde.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(producto)
        End If
    End Sub

    Private Async Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnImprimir.Focus()
        btnGuardar.Enabled = False
        If txtIdOrdenServicio.Text = "" Then
            ordenServicio = New OrdenServicio With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario
            }
            ordenServicio.IdCliente = cliente.IdCliente
            ordenServicio.Fecha = FrmPrincipal.ObtenerFechaFormateada(Now())
            ordenServicio.IdVendedor = vendedor.IdVendedor
            ordenServicio.Observaciones1 = txtObservaciones1.Text
            ordenServicio.Observaciones2 = txtObservaciones2.Text
            ordenServicio.Observaciones3 = txtObservaciones3.Text
            ordenServicio.Observaciones4 = txtObservaciones4.Text
            ordenServicio.Observaciones5 = txtObservaciones5.Text
            ordenServicio.Excento = decExcento
            ordenServicio.Gravado = decGravado
            ordenServicio.Descuento = 0
            ordenServicio.Impuesto = CDbl(txtImpuesto.Text)
            If txtIdOrdenServicio.Text <> "" Then
                ordenServicio.DetalleOrdenServicio.Clear()
            End If
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                detalleOrdenServicio = New DetalleOrdenServicio
                If txtIdOrdenServicio.Text <> "" Then
                    detalleOrdenServicio.IdOrden = ordenServicio.IdOrden
                End If
                detalleOrdenServicio.IdProducto = dtbDetalleOrdenServicio.Rows(I).Item(0)
                detalleOrdenServicio.Descripcion = dtbDetalleOrdenServicio.Rows(I).Item(2)
                detalleOrdenServicio.Cantidad = dtbDetalleOrdenServicio.Rows(I).Item(3)
                detalleOrdenServicio.PrecioVenta = dtbDetalleOrdenServicio.Rows(I).Item(4)
                detalleOrdenServicio.Excento = dtbDetalleOrdenServicio.Rows(I).Item(6)
                detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(7)
                ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
            Next
            Try
                txtIdOrdenServicio.Text = Await Puntoventa.AgregarOrdenServicio(ordenServicio, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdOrdenServicio.Text = ""
                btnGuardar.Enabled = True
                btnGuardar.Focus()
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            ordenServicio.Observaciones1 = txtObservaciones1.Text
            ordenServicio.Observaciones2 = txtObservaciones2.Text
            ordenServicio.Observaciones3 = txtObservaciones3.Text
            ordenServicio.Observaciones4 = txtObservaciones4.Text
            ordenServicio.Observaciones5 = txtObservaciones5.Text
            ordenServicio.Excento = decExcento
            ordenServicio.Gravado = decGravado
            ordenServicio.Descuento = 0
            ordenServicio.Impuesto = CDbl(txtImpuesto.Text)
            ordenServicio.DetalleOrdenServicio.Clear()
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                detalleOrdenServicio = New DetalleOrdenServicio
                detalleOrdenServicio.IdOrden = ordenServicio.IdOrden
                detalleOrdenServicio.IdProducto = dtbDetalleOrdenServicio.Rows(I).Item(0)
                detalleOrdenServicio.Descripcion = dtbDetalleOrdenServicio.Rows(I).Item(2)
                detalleOrdenServicio.Cantidad = dtbDetalleOrdenServicio.Rows(I).Item(3)
                detalleOrdenServicio.PrecioVenta = dtbDetalleOrdenServicio.Rows(I).Item(4)
                detalleOrdenServicio.Excento = dtbDetalleOrdenServicio.Rows(I).Item(6)
                detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(7)
                ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
            Next
            Try
                Await Puntoventa.ActualizarOrdenServicio(ordenServicio, FrmPrincipal.usuarioGlobal.Token)
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
        btnAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdOrdenServicio.Text <> "" Then
            'Dim reptOrdenServicio As New rptOrdenServicio
            'Try
            '    'dtbDatos = servicioReportes.ObtenerReporteOrdenServicio(txtIdOrdenServicio.Text)
            'Catch ex As Exception
            '    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            '    Exit Sub
            'End Try
            'strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
            'strEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
            'strTelefonos = FrmPrincipal.empresaGlobal.Telefono
            'reptOrdenServicio.SetDataSource(dtbDatos)
            'reptOrdenServicio.SetParameterValue(0, strUsuario)
            'reptOrdenServicio.SetParameterValue(1, strEmpresa)
            'reptOrdenServicio.SetParameterValue(2, strTelefonos)
            'formReport.crtViewer.ReportSource = reptOrdenServicio
            'formReport.ShowDialog()
        End If
    End Sub

    Private Async Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto Is Nothing Then
            If txtCodigo.Text <> "" Then
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    decPrecioVenta = ObtenerPrecioVentaPorCliente(cliente, producto)
                    CargarLineaDetalleOrdenServicio(producto, producto.Descripcion, txtCantidad.Text, decPrecioVenta)
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
            CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, decPrecioVenta)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtUnidad.Text = ""
            txtPrecio.Text = ""
            producto = Nothing
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub CmdEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If grdDetalleOrdenServicio.Rows.Count > 0 Then
            dtbDetalleOrdenServicio.Rows.Remove(dtbDetalleOrdenServicio.Rows.Find(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value))
            grdDetalleOrdenServicio.Refresh()
            CargarTotales()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub Precio_KeyUp(sender As Object, e As KeyEventArgs) Handles txtPrecio.KeyUp
        If producto IsNot Nothing Then
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            If txtPrecio.Text <> "" Then decPrecioVenta = Math.Round(CDbl(txtPrecio.Text) * (1 + (decTasaImpuesto / 100)), 2, MidpointRounding.AwayFromZero)
        End If
    End Sub

    Private Async Sub TxtCodigo_KeyPress(sender As Object, e As PreviewKeyDownEventArgs) Handles txtCodigo.PreviewKeyDown
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

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
    End Sub

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) Handles txtCantidad.MouseDown, txtCodigo.MouseDown, txtDescripcion.MouseDown, txtPrecio.MouseDown
        sender.SelectAll()
    End Sub

    Private Sub ValidaDigitosSinDecimal(sender As Object, e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, False, 0)
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class