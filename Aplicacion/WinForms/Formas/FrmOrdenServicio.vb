Imports System.Collections.Generic
Imports LeandroSoftware.Puntoventa.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmOrdenServicio
#Region "Variables"
    Private strMotivoRechazo As String
    Private decExcento, decGrabado, decImpuesto, decTotal, decCostoPorInstalacion As Decimal
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

    Private formReport As New frmRptViewer()
    Private dtbDatos As DataTable
    Private strEmpresa, strUsuario, strTelefonos As String
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleOrdenServicio()
        dtbDetalleOrdenServicio = New DataTable()
        dtbDetalleOrdenServicio.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("CODIGO", GetType(String))
        dtbDetalleOrdenServicio.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleOrdenServicio.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("PRECIO", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleOrdenServicio.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleOrdenServicio.Columns.Add("COSTOINSTALACION", GetType(Decimal))
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
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
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
            dtrRowDetOrdenServicio.Item(7) = detalle.CostoInstalacion
            dtrRowDetOrdenServicio.Item(8) = detalle.PorcentajeIVA
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
            decCostoPorInstalacion += detalle.Cantidad * detalle.CostoInstalacion
        Next
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Sub CargarLineaDetalleOrdenServicio(ByVal producto As Producto, ByVal strDescripcion As String, ByVal intCantidad As Integer, ByVal dblPrecio As Double, ByVal dblCostoInstalacion As Double)
        Dim intIndice As Integer = dtbDetalleOrdenServicio.Rows.IndexOf(dtbDetalleOrdenServicio.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.ExoneradoDeImpuesto Then decTasaImpuesto = 0
        If intIndice >= 0 Then
            dtbDetalleOrdenServicio.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenServicio.Rows(intIndice).Item(2) = strDescripcion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(3) += intCantidad
            dtbDetalleOrdenServicio.Rows(intIndice).Item(4) = dblPrecio
            dtbDetalleOrdenServicio.Rows(intIndice).Item(5) = dtbDetalleOrdenServicio.Rows(intIndice).Item(3) * dtbDetalleOrdenServicio.Rows(intIndice).Item(4)
            dtbDetalleOrdenServicio.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleOrdenServicio.Rows(intIndice).Item(7) = dblCostoInstalacion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(8) = decTasaImpuesto
        Else
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = producto.IdProducto
            dtrRowDetOrdenServicio.Item(1) = producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = strDescripcion
            dtrRowDetOrdenServicio.Item(3) = intCantidad
            dtrRowDetOrdenServicio.Item(4) = dblPrecio
            dtrRowDetOrdenServicio.Item(5) = dtrRowDetOrdenServicio.Item(3) * dtrRowDetOrdenServicio.Item(4)
            dtrRowDetOrdenServicio.Item(6) = decTasaImpuesto = 0
            dtrRowDetOrdenServicio.Item(7) = dblCostoInstalacion
            dtrRowDetOrdenServicio.Item(8) = decTasaImpuesto
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        End If
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Sub CargarLineaDetalleInstalacion(ByVal producto As Producto, ByVal dblTotal As Double)
        Dim intIndice As Integer = dtbDetalleOrdenServicio.Rows.IndexOf(dtbDetalleOrdenServicio.Rows.Find(producto.IdProducto))
        Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
        If cliente.ExoneradoDeImpuesto Then decTasaImpuesto = 0
        If intIndice >= 0 Then
            dtbDetalleOrdenServicio.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenServicio.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(4) += dblTotal
            dtbDetalleOrdenServicio.Rows(intIndice).Item(5) += dblTotal
            dtbDetalleOrdenServicio.Rows(intIndice).Item(6) = decTasaImpuesto = 0
            dtbDetalleOrdenServicio.Rows(intIndice).Item(7) = 0
            dtbDetalleOrdenServicio.Rows(intIndice).Item(8) = decTasaImpuesto
        Else
            dtrRowDetOrdenServicio = dtbDetalleOrdenServicio.NewRow
            dtrRowDetOrdenServicio.Item(0) = producto.IdProducto
            dtrRowDetOrdenServicio.Item(1) = producto.Codigo
            dtrRowDetOrdenServicio.Item(2) = producto.Descripcion
            dtrRowDetOrdenServicio.Item(3) = 1
            dtrRowDetOrdenServicio.Item(4) = dblTotal
            dtrRowDetOrdenServicio.Item(5) = dblTotal
            dtrRowDetOrdenServicio.Item(6) = decTasaImpuesto = 0
            dtrRowDetOrdenServicio.Item(7) = 0
            dtrRowDetOrdenServicio.Item(8) = decTasaImpuesto
            dtbDetalleOrdenServicio.Rows.Add(dtrRowDetOrdenServicio)
        End If
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Sub DescargarLineaDetalleInstalacion(ByVal producto As Producto, ByVal dblTotal As Double)
        Dim intIndice As Integer = dtbDetalleOrdenServicio.Rows.IndexOf(dtbDetalleOrdenServicio.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleOrdenServicio.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleOrdenServicio.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleOrdenServicio.Rows(intIndice).Item(4) -= dblTotal
            dtbDetalleOrdenServicio.Rows(intIndice).Item(5) -= dblTotal
        End If
        grdDetalleOrdenServicio.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim decSubTotal As Decimal = 0
        decGrabado = 0
        decExcento = 0
        decImpuesto = 0
        For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
            If dtbDetalleOrdenServicio.Rows(I).Item(6) = 0 Then
                decGrabado += dtbDetalleOrdenServicio.Rows(I).Item(5)
                decImpuesto += dtbDetalleOrdenServicio.Rows(I).Item(5) * dtbDetalleOrdenServicio.Rows(I).Item(8) / 100
            Else
                decExcento += dtbDetalleOrdenServicio.Rows(I).Item(5)
            End If
        Next
        decSubTotal = decGrabado + decExcento
        If decSubTotal > 0 And txtDescuento.Text > 0 Then
            decImpuesto = 0
            For I = 0 To dtbDetalleOrdenServicio.Rows.Count - 1
                If dtbDetalleOrdenServicio.Rows(I).Item(6) = 0 Then
                    Dim decDescuentoPorLinea As Decimal = 0
                    Dim decTotalPorLinea As Decimal = dtbDetalleOrdenServicio.Rows(I).Item(5)
                    decDescuentoPorLinea = decTotalPorLinea - (txtDescuento.Text / decSubTotal * decTotalPorLinea)
                    decImpuesto += decDescuentoPorLinea * dtbDetalleOrdenServicio.Rows(I).Item(8) / 100
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

    Private Sub ValidarProducto(ByVal strCodigoProducto As String)
        If Not bolInit Then
            If strCodigoProducto <> "" Then
                If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If strCodigoProducto.IndexOf(" ") >= 0 Then
                        strCodigoProducto = strCodigoProducto.Substring(0, strCodigoProducto.IndexOf(" "))
                    End If
                End If
                Try
                    'producto = servicioMantenimiento.ObtenerProductoPorCodigo(strCodigoProducto)
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
                If producto.Tipo = StaticTipoProducto.Servicio Then
                    If FrmPrincipal.empresaGlobal.ModificaDescProducto = True Then
                        txtDescripcion.ReadOnly = False
                        txtDescripcion.Focus()
                    End If
                Else
                    txtDescripcion.ReadOnly = True
                    txtPrecio.Focus()
                End If
            End If
        End If
    End Sub

    Private Sub CargarAutoCompletarProducto()
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As ICollection(Of Producto) = Nothing 'servicioMantenimiento.ObtenerListaProductos(FrmMenuPrincipal.empresaGlobal.IdEmpresa, 1, 0, True)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmOrdenServicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
            CargarAutoCompletarProducto()
        End If
        IniciaDetalleOrdenServicio()
        EstablecerPropiedadesDataGridView()
        grdDetalleOrdenServicio.DataSource = dtbDetalleOrdenServicio
        txtCantidad.Text = "1"
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        bolInit = False
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdOrdenServicio.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtOperarios.Text = ""
        txtHoraEntrada.Text = ""
        txtHoraSalida.Text = ""
        txtMarca.Text = ""
        txtModelo.Text = ""
        txtPlaca.Text = ""
        txtColor.Text = ""
        txtEstadoActual.Text = ""
        dtbDetalleOrdenServicio.Rows.Clear()
        grdDetalleOrdenServicio.Refresh()
        txtSubTotal.Text = FormatNumber(0, 2)
        txtDescuento.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        decCostoPorInstalacion = 0
        txtCodigo.Text = ""
        txtUnidad.Text = ""
        txtCantidad.Text = "1"
        txtDescripcion.Text = ""
        txtPrecio.Text = ""
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        btnBuscaVendedor.Enabled = True
        btnBuscarCliente.Enabled = True
        cliente = Nothing
        vendedor = Nothing
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdOrdenServicio.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioFacturacion.AnularOrdenServicio(txtIdOrdenServicio.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                    MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CmdAgregar_Click(CmdAgregar, New EventArgs())
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaOrdenServicio()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'ordenServicio = servicioFacturacion.ObtenerOrdenServicio(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ordenServicio IsNot Nothing Then
                cliente = ordenServicio.Cliente
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
                cliente = ordenServicio.Cliente
                txtNombreCliente.Text = ordenServicio.Cliente.Nombre
                txtFecha.Text = ordenServicio.Fecha
                txtOperarios.Text = ordenServicio.Operarios
                txtHoraEntrada.Text = ordenServicio.HoraEntrada
                txtHoraSalida.Text = ordenServicio.HoraSalida
                txtMarca.Text = ordenServicio.Marca
                txtModelo.Text = ordenServicio.Modelo
                txtPlaca.Text = ordenServicio.Placa
                txtColor.Text = ordenServicio.Color
                txtEstadoActual.Text = ordenServicio.EstadoActual
                vendedor = ordenServicio.Vendedor
                txtVendedor.Text = vendedor.Nombre
                txtDescuento.Text = FormatNumber(ordenServicio.Descuento, 2)
                decCostoPorInstalacion = 0
                CargarDetalleOrdenServicio(ordenServicio)
                CargarTotales()
                CmdImprimir.Enabled = True
                btnBuscaVendedor.Enabled = False
                If ordenServicio.Aplicado Then
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

    Private Sub BtnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'cliente = servicioFacturacion.ObtenerCliente(FrmMenuPrincipal.intBusqueda)
                txtNombreCliente.Text = cliente.Nombre
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
            ValidarProducto(txtCodigo.Text)
        End If
        txtCodigo.Focus()
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If cliente Is Nothing Or vendedor Is Nothing Or txtFecha.Text = "" Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdOrdenServicio.Text = "" Then
            ordenServicio = New OrdenServicio With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario
            }
        End If
        ordenServicio.IdCliente = cliente.IdCliente
        ordenServicio.Fecha = FrmPrincipal.ObtenerFechaFormateada(Now())
        ordenServicio.IdVendedor = vendedor.IdVendedor
        ordenServicio.Operarios = txtOperarios.Text
        ordenServicio.HoraEntrada = txtHoraEntrada.Text
        ordenServicio.HoraSalida = txtHoraSalida.Text
        ordenServicio.Marca = txtMarca.Text
        ordenServicio.Modelo = txtModelo.Text
        ordenServicio.Placa = txtPlaca.Text
        ordenServicio.Color = txtColor.Text
        ordenServicio.EstadoActual = txtEstadoActual.Text
        ordenServicio.Excento = decExcento
        ordenServicio.Grabado = decGrabado
        ordenServicio.Descuento = CDbl(txtDescuento.Text)
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
            detalleOrdenServicio.CostoInstalacion = dtbDetalleOrdenServicio.Rows(I).Item(7)
            detalleOrdenServicio.PorcentajeIVA = dtbDetalleOrdenServicio.Rows(I).Item(8)
            ordenServicio.DetalleOrdenServicio.Add(detalleOrdenServicio)
        Next
        If txtIdOrdenServicio.Text = "" Then
            Try
                'ordenServicio = servicioFacturacion.AgregarOrdenServicio(ordenServicio)
                txtIdOrdenServicio.Text = ordenServicio.IdOrden
            Catch ex As Exception
                txtIdOrdenServicio.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            Try
                'servicioFacturacion.ActualizarOrdenServicio(ordenServicio)
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
        CmdGuardar.Enabled = True
        btnBuscaVendedor.Enabled = False
        btnBuscarCliente.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdOrdenServicio.Text <> "" Then
            Dim reptOrdenServicio As New rptOrdenServicio
            Try
                'dtbDatos = servicioReportes.ObtenerReporteOrdenServicio(txtIdOrdenServicio.Text)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            strUsuario = FrmPrincipal.usuarioGlobal.CodigoUsuario
            strEmpresa = FrmPrincipal.empresaGlobal.NombreEmpresa
            strTelefonos = FrmPrincipal.empresaGlobal.Telefono
            reptOrdenServicio.SetDataSource(dtbDatos)
            reptOrdenServicio.SetParameterValue(0, strUsuario)
            reptOrdenServicio.SetParameterValue(1, strEmpresa)
            reptOrdenServicio.SetParameterValue(2, strTelefonos)
            formReport.crtViewer.ReportSource = reptOrdenServicio
            formReport.ShowDialog()
        End If
    End Sub

    Private Sub CmdInsertar_Click(sender As Object, e As EventArgs) Handles cmdInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" And txtPrecio.Text <> "" And txtUnidad.Text <> "" Then
            If Not FrmPrincipal.empresaGlobal.IncluyeInsumosEnFactura Then
                If CDbl(txtPrecio.Text) <= 0 Then
                    MessageBox.Show("El precio de venta no puede ser igual o menor a 0.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End If
            End If
            If FrmPrincipal.empresaGlobal.DesglosaServicioInst And FrmPrincipal.empresaGlobal.PorcentajeInstalacion > 0 Then
                If producto.Tipo = StaticTipoProducto.Producto And CDbl(txtPrecio.Text) > 0 Then
                    If MessageBox.Show("Desea desglosar el servicio de instalación?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        Dim precioProducto, precioInstalacion As Double
                        precioInstalacion = CDbl(txtPrecio.Text) * FrmPrincipal.empresaGlobal.PorcentajeInstalacion / 100
                        precioProducto = CDbl(txtPrecio.Text) - precioInstalacion
                        CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, precioProducto, precioInstalacion)
                        'producto = servicioMantenimiento.ObtenerProducto(FrmMenuPrincipal.empresaGlobal.CodigoServicioInst)
                        CargarLineaDetalleInstalacion(producto, precioInstalacion * CDbl(txtCantidad.Text))
                        decCostoPorInstalacion += precioInstalacion * CDbl(txtCantidad.Text)
                    Else
                        CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, txtPrecio.Text, 0)
                    End If
                ElseIf producto.IdProducto = FrmPrincipal.empresaGlobal.CodigoServicioInst Then
                    CargarLineaDetalleInstalacion(producto, CDbl(txtCantidad.Text) * CDbl(txtPrecio.Text))
                Else
                    CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, txtPrecio.Text, 0)
                End If
            Else
                CargarLineaDetalleOrdenServicio(producto, txtDescripcion.Text, txtCantidad.Text, txtPrecio.Text, 0)
            End If
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
        If grdDetalleOrdenServicio.Rows.Count > 0 Then
            If FrmPrincipal.empresaGlobal.DesglosaServicioInst And grdDetalleOrdenServicio.CurrentRow.Cells(0).Value = FrmPrincipal.empresaGlobal.CodigoServicioInst And decCostoPorInstalacion > 0 Then
                MessageBox.Show("La línea seleccionada no puede eliminarse. Debe eliminar los productos relacionados.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            'producto = servicioMantenimiento.ObtenerProducto(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value)
            If CDbl(dtbDetalleOrdenServicio.Rows.Find(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value).Item(7)) > 0 Then
                'producto = servicioMantenimiento.ObtenerProducto(FrmMenuPrincipal.empresaGlobal.CodigoServicioInst)
                DescargarLineaDetalleInstalacion(producto, CDbl(dtbDetalleOrdenServicio.Rows.Find(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value).Item(7)) * CDbl(grdDetalleOrdenServicio.CurrentRow.Cells(3).Value))
                decCostoPorInstalacion -= CDbl(dtbDetalleOrdenServicio.Rows.Find(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value).Item(7)) * CDbl(grdDetalleOrdenServicio.CurrentRow.Cells(3).Value)
            End If
            dtbDetalleOrdenServicio.Rows.Remove(dtbDetalleOrdenServicio.Rows.Find(grdDetalleOrdenServicio.CurrentRow.Cells(0).Value))
            grdDetalleOrdenServicio.Refresh()
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
        ValidarProducto(txtCodigo.Text)
    End Sub

    Private Sub TxtCantidad_Validated(sender As Object, e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "1"
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

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtDescuento.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class