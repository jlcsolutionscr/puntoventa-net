Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes
Imports System.Collections.Generic
Imports LeandroSoftware.Common.DatosComunes

Public Class FrmDevolucionDeClientes
#Region "Variables"
    Private dblExcento, decGravado, decSubTotal As Decimal
    Private dtbDetalleDevolucion As DataTable
    Private dtrRowDetDevolucion As DataRow
    Private arrDetalleDevolucion As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private devolucion As DevolucionCliente
    Private detalleDevolucion As DetalleDevolucionCliente
    Private factura As Factura
    Private cliente As Cliente
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleDevolucion()
        dtbDetalleDevolucion = New DataTable()
        dtbDetalleDevolucion.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleDevolucion.Columns.Add("CODIGO", GetType(String))
        dtbDetalleDevolucion.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleDevolucion.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleDevolucion.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleDevolucion.Columns.Add("PRECIOVENTA", GetType(Decimal))
        dtbDetalleDevolucion.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleDevolucion.Columns.Add("EXCENTO", GetType(Integer))
        dtbDetalleDevolucion.Columns.Add("CANTDEVOLUCION", GetType(Decimal))
        dtbDetalleDevolucion.Columns.Add("PORCENTAJEIVA", GetType(Decimal))
        dtbDetalleDevolucion.PrimaryKey = {dtbDetalleDevolucion.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleDevolucion.Columns.Clear()
        grdDetalleDevolucion.AutoGenerateColumns = False

        Dim dvcIdProducto As New DataGridViewTextBoxColumn
        Dim dvcCodigo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcCantidad As New DataGridViewTextBoxColumn
        Dim dvcPrecioCosto As New DataGridViewTextBoxColumn
        Dim dvcPrecioVenta As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcExc As New DataGridViewCheckBoxColumn
        Dim dvcCantDevolucion As New DataGridViewTextBoxColumn
        Dim dvcPorcentajeIVA As New DataGridViewTextBoxColumn

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 125
        dvcCodigo.ReadOnly = True
        dvcCodigo.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleDevolucion.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 340
        dvcDescripcion.ReadOnly = True
        dvcDescripcion.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleDevolucion.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcCantidad.ReadOnly = True
        dvcCantidad.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleDevolucion.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "PrecioCosto"
        dvcPrecioCosto.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta.DataPropertyName = "PRECIOVENTA"
        dvcPrecioVenta.HeaderText = "Precio"
        dvcPrecioVenta.Width = 75
        dvcPrecioVenta.ReadOnly = True
        dvcPrecioVenta.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcPrecioVenta.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleDevolucion.Columns.Add(dvcPrecioVenta)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.ReadOnly = True
        dvcTotal.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleDevolucion.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.ReadOnly = True
        dvcExc.SortMode = DataGridViewColumnSortMode.NotSortable
        grdDetalleDevolucion.Columns.Add(dvcExc)

        dvcCantDevolucion.DataPropertyName = "CANTDEVOLUCION"
        dvcCantDevolucion.HeaderText = "Cant-Devol"
        dvcCantDevolucion.Width = 60
        dvcCantDevolucion.ReadOnly = False
        dvcCantDevolucion.SortMode = DataGridViewColumnSortMode.NotSortable
        dvcCantDevolucion.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleDevolucion.Columns.Add(dvcCantDevolucion)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcPorcentajeIVA)
    End Sub

    Private Sub CargarDetalleDevolucion(ByVal devolucion As DevolucionCliente)
        dtbDetalleDevolucion.Rows.Clear()
        For Each detalle As DetalleDevolucionCliente In devolucion.DetalleDevolucionCliente
            dtrRowDetDevolucion = dtbDetalleDevolucion.NewRow
            dtrRowDetDevolucion.Item(0) = detalle.IdProducto
            dtrRowDetDevolucion.Item(1) = detalle.Producto.Codigo
            dtrRowDetDevolucion.Item(2) = detalle.Producto.Descripcion
            dtrRowDetDevolucion.Item(3) = 0
            dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
            dtrRowDetDevolucion.Item(5) = detalle.PrecioVenta
            dtrRowDetDevolucion.Item(6) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(5)
            dtrRowDetDevolucion.Item(7) = detalle.Excento
            dtrRowDetDevolucion.Item(8) = detalle.Cantidad
            dtrRowDetDevolucion.Item(9) = detalle.PorcentajeIVA
            dtbDetalleDevolucion.Rows.Add(dtrRowDetDevolucion)
        Next
        grdDetalleDevolucion.Refresh()
    End Sub

    Private Sub CargarDetalleFactura(ByVal factura As Factura)
        dtbDetalleDevolucion.Rows.Clear()
        Dim tipoProducto As New List(Of Integer)(New Integer() {StaticTipoProducto.Producto, StaticTipoProducto.Transitorio})
        For Each detalle As DetalleFactura In factura.DetalleFactura
            If tipoProducto.Contains(detalle.Producto.Tipo) Then
                dtrRowDetDevolucion = dtbDetalleDevolucion.NewRow
                dtrRowDetDevolucion.Item(0) = detalle.IdProducto
                dtrRowDetDevolucion.Item(1) = detalle.Producto.Codigo
                dtrRowDetDevolucion.Item(2) = detalle.Producto.Descripcion
                dtrRowDetDevolucion.Item(3) = detalle.Cantidad - detalle.CantDevuelto
                dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
                dtrRowDetDevolucion.Item(5) = detalle.PrecioVenta
                dtrRowDetDevolucion.Item(6) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(5)
                dtrRowDetDevolucion.Item(7) = detalle.Excento
                dtrRowDetDevolucion.Item(8) = 0
                dtrRowDetDevolucion.Item(9) = detalle.PorcentajeIVA
                dtbDetalleDevolucion.Rows.Add(dtrRowDetDevolucion)
            End If
        Next
        grdDetalleDevolucion.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim decImpuesto As Decimal = 0
        decGravado = 0
        dblExcento = 0
        decSubTotal = 0
        For I As Short = 0 To dtbDetalleDevolucion.Rows.Count - 1
            If dtbDetalleDevolucion.Rows(I).Item(8) > 0 Then
                Dim decTotalPorLinea = dtbDetalleDevolucion.Rows(I).Item(5) * dtbDetalleDevolucion.Rows(I).Item(8)
                If dtbDetalleDevolucion.Rows(I).Item(7) = False Then
                    decGravado += decTotalPorLinea
                    decImpuesto += decTotalPorLinea * dtbDetalleDevolucion.Rows(I).Item(9) / 100
                Else
                    dblExcento += decTotalPorLinea
                End If
            End If
        Next
        decSubTotal = decGravado + dblExcento
        decGravado = Math.Round(decGravado, 2)
        dblExcento = Math.Round(dblExcento, 2)
        decImpuesto = Math.Round(decImpuesto, 2)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(dblExcento + decGravado + decImpuesto, 2)
    End Sub

    Private Async Sub CargarFactura(intIdFactura As Integer)
        Try
            factura = Await Puntoventa.ObtenerFactura(intIdFactura, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        If factura IsNot Nothing Then
            txtIdFactura.Text = factura.ConsecFactura
            cliente = factura.Cliente
            txtCliente.Text = factura.Cliente.Nombre
            CargarDetalleFactura(factura)
            CargarTotales()
        Else
            txtIdFactura.Text = ""
            MessageBox.Show("El número de factura ingresado no existe. Por favor verifique.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmDevolucionDeClientes_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmDevolucionDeClientes_KeyDown(sender As Object, e As KeyEventArgs) Handles MyBase.KeyDown
        If e.KeyCode = Keys.F3 Then
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

    Private Sub FrmDevolucionDeClientes_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            IniciaDetalleDevolucion()
            EstablecerPropiedadesDataGridView()
            grdDetalleDevolucion.DataSource = dtbDetalleDevolucion
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub grdDetalleDevolucion_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleDevolucion.CellEndEdit
        If grdDetalleDevolucion.CurrentCell.Value.ToString() = "" Then
            grdDetalleDevolucion.CurrentCell.Value = 0
        Else
            If grdDetalleDevolucion.CurrentCell.ColumnIndex = 8 Then
                If grdDetalleDevolucion.CurrentCell.Value > grdDetalleDevolucion.CurrentRow.Cells(3).Value Then
                    MessageBox.Show("La cantidad ingresada de artículos por devolver excede la cantidad de artículos de la factura.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    grdDetalleDevolucion.CurrentCell.Value = 0
                End If
            End If
        End If
        CargarTotales()
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        txtIdDevolucion.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        cliente = Nothing
        factura = Nothing
        txtCliente.Text = ""
        txtIdFactura.Text = ""
        txtDetalle.Text = ""
        dtbDetalleDevolucion.Rows.Clear()
        grdDetalleDevolucion.Refresh()
        grdDetalleDevolucion.ReadOnly = False
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        btnAnular.Enabled = False
        btnGuardar.Enabled = True
        btnImprimir.Enabled = False
        btnBuscarFactura.Enabled = True
        txtIdFactura.Focus()
    End Sub

    Private Async Sub BtnAnular_Click(sender As Object, e As EventArgs) Handles btnAnular.Click
        If txtIdDevolucion.Text <> "" Then
            Dim formAnulacion As New FrmMotivoAnulacion()
            formAnulacion.bolConfirmacion = False
            formAnulacion.ShowDialog()
            If formAnulacion.bolConfirmacion Then
                Try
                    Await Puntoventa.AnularDevolucionCliente(txtIdDevolucion.Text, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaDevolucionCliente()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                devolucion = Await Puntoventa.ObtenerDevolucionCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If devolucion IsNot Nothing Then
                txtIdDevolucion.Text = devolucion.IdDevolucion
                txtIdFactura.Text = devolucion.ConsecFactura
                cliente = devolucion.Cliente
                txtCliente.Text = cliente.Nombre
                txtFecha.Text = devolucion.Fecha
                txtDetalle.Text = devolucion.Detalle
                CargarDetalleDevolucion(devolucion)
                CargarTotales()
                grdDetalleDevolucion.ReadOnly = True
                btnImprimir.Enabled = Not devolucion.Nulo
                btnAnular.Enabled = Not devolucion.Nulo And FrmPrincipal.bolAnularTransacciones
                btnGuardar.Enabled = False
                btnBuscarFactura.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnBuscarFactura_Click(sender As Object, e As EventArgs) Handles btnBuscarFactura.Click
        Dim formBusqueda As New FrmBusquedaFactura()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Dim intIdFactura As Integer = FrmPrincipal.intBusqueda
            CargarFactura(intIdFactura)
        End If
    End Sub

    Private Async Sub BtnGuardar_Click(sender As Object, e As EventArgs) Handles btnGuardar.Click
        If cliente Is Nothing Or txtFecha.Text = "" Or factura Is Nothing Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        btnGuardar.Enabled = False
        If txtIdDevolucion.Text = "" Then
            devolucion = New DevolucionCliente With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdCliente = factura.IdCliente,
                .IdFactura = factura.IdFactura,
                .Detalle = txtDetalle.Text,
                .ConsecFactura = factura.ConsecFactura,
                .Fecha = Now(),
                .Excento = dblExcento,
                .Gravado = decGravado,
                .Impuesto = CDbl(txtImpuesto.Text)
            }
            devolucion.DetalleDevolucionCliente = New List(Of DetalleDevolucionCliente)
            For I As Short = 0 To dtbDetalleDevolucion.Rows.Count - 1
                If dtbDetalleDevolucion.Rows(I).Item(8) > 0 Then
                    detalleDevolucion = New DetalleDevolucionCliente With {
                        .IdProducto = dtbDetalleDevolucion.Rows(I).Item(0),
                        .PrecioCosto = dtbDetalleDevolucion.Rows(I).Item(4),
                        .PrecioVenta = dtbDetalleDevolucion.Rows(I).Item(5),
                        .Excento = dtbDetalleDevolucion.Rows(I).Item(7),
                        .Cantidad = dtbDetalleDevolucion.Rows(I).Item(8),
                        .PorcentajeIVA = dtbDetalleDevolucion.Rows(I).Item(9)
                    }
                    devolucion.DetalleDevolucionCliente.Add(detalleDevolucion)
                End If
            Next
            Try
                Dim referencias As ReferenciasEntidad = Await Puntoventa.AgregarDevolucionCliente(devolucion, FrmPrincipal.usuarioGlobal.Token)
                devolucion.IdDevolucion = referencias.Id
                txtIdDevolucion.Text = devolucion.IdDevolucion
            Catch ex As Exception
                txtIdDevolucion.Text = ""
                btnGuardar.Enabled = True
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        grdDetalleDevolucion.ReadOnly = True
        btnImprimir.Enabled = True
        btnAgregar.Enabled = True
        btnAnular.Enabled = FrmPrincipal.bolAnularTransacciones
        btnImprimir.Focus()
        btnBuscarFactura.Enabled = False
    End Sub

    Private Sub BtnImprimir_Click(sender As Object, e As EventArgs) Handles btnImprimir.Click
        If txtIdDevolucion.Text <> "" Then
            comprobante = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdDevolucion.Text,
                .strDocumento = txtIdFactura.Text,
                .strNombre = txtCliente.Text,
                .strFecha = txtFecha.Text,
                .strDetalle = txtDetalle.Text,
                .strFormaPago = "Contado",
                .strSubTotal = txtSubTotal.Text,
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleDevolucion = New List(Of ModuloImpresion.ClsDetalleComprobante)
            For I As Short = 0 To dtbDetalleDevolucion.Rows.Count - 1
                If dtbDetalleDevolucion.Rows(I).Item(8) > 0 Then
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
                        .strDescripcion = dtbDetalleDevolucion.Rows(I).Item(1) + "-" + dtbDetalleDevolucion.Rows(I).Item(2),
                        .strCantidad = CDbl(dtbDetalleDevolucion.Rows(I).Item(8)),
                        .strPrecio = FormatNumber(dtbDetalleDevolucion.Rows(I).Item(5), 2),
                        .strTotalLinea = FormatNumber(CDbl(dtbDetalleDevolucion.Rows(I).Item(8)) * CDbl(dtbDetalleDevolucion.Rows(I).Item(5)), 2)
                    }
                    arrDetalleDevolucion.Add(detalleComprobante)
                End If
            Next
            comprobante.arrDetalleComprobante = arrDetalleDevolucion
            Try
                ModuloImpresion.ImprimirDevolucionCliente(comprobante)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
#End Region
End Class