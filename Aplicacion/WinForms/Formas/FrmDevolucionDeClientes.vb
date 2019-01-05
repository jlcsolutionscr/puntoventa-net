Imports System.Collections.Generic
Imports LeandroSoftware.Puntoventa.CommonTypes
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmDevolucionDeClientes
#Region "Variables"
    Private dblExcento, dblGrabado, dblSubTotal As Decimal
    Private I As Short
    Private dtbDatosLocal, dtbDetalleDevolucion As DataTable
    Private dtrRowDetDevolucion As DataRow
    Private arrDetalleDevolucion As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private devolucion As DevolucionCliente
    Private detalleDevolucion As DetalleDevolucionCliente
    Private factura As Factura
    Private cliente As Cliente
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.clsDetalleComprobante
    Private bolInit As Boolean = True
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

        dvcIdProducto.DataPropertyName = "IDPRODUCTO"
        dvcIdProducto.HeaderText = "IdP"
        dvcIdProducto.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 125
        dvcCodigo.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 340
        dvcDescripcion.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcCantidad.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "PrecioCosto"
        dvcPrecioCosto.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta.DataPropertyName = "PRECIOVENTA"
        dvcPrecioVenta.HeaderText = "Precio"
        dvcPrecioVenta.Width = 75
        dvcPrecioVenta.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcPrecioVenta.ReadOnly = False
        grdDetalleDevolucion.Columns.Add(dvcPrecioVenta)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcTotal.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcExc)

        dvcCantDevolucion.DataPropertyName = "CANTDEVOLUCION"
        dvcCantDevolucion.HeaderText = "Cant-Devol"
        dvcCantDevolucion.Width = 60
        dvcCantDevolucion.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        dvcExc.ReadOnly = False
        grdDetalleDevolucion.Columns.Add(dvcCantDevolucion)
    End Sub

    Private Sub CargarDetalleDevolucion(ByVal devolucion As DevolucionCliente)
        dtbDetalleDevolucion.Rows.Clear()
        For Each detalle As DetalleDevolucionCliente In devolucion.DetalleDevolucionCliente
            dtrRowDetDevolucion = dtbDetalleDevolucion.NewRow
            dtrRowDetDevolucion.Item(0) = detalle.IdProducto
            dtrRowDetDevolucion.Item(1) = detalle.Producto.Codigo
            dtrRowDetDevolucion.Item(2) = detalle.Producto.Descripcion
            dtrRowDetDevolucion.Item(3) = detalle.Cantidad
            dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
            dtrRowDetDevolucion.Item(5) = detalle.PrecioVenta
            dtrRowDetDevolucion.Item(6) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(5)
            dtrRowDetDevolucion.Item(7) = detalle.Excento
            dtrRowDetDevolucion.Item(8) = detalle.CantDevolucion
            dtbDetalleDevolucion.Rows.Add(dtrRowDetDevolucion)
        Next
        grdDetalleDevolucion.Refresh()
    End Sub

    Private Sub CargarDetalleFactura(ByVal factura As Factura)
        dtbDetalleDevolucion.Rows.Clear()
        For Each detalle As DetalleFactura In factura.DetalleFactura
            If detalle.Producto.TipoProducto.IdTipoProducto = StaticTipoProducto.Producto Then
                dtrRowDetDevolucion = dtbDetalleDevolucion.NewRow
                dtrRowDetDevolucion.Item(0) = detalle.IdProducto
                dtrRowDetDevolucion.Item(1) = detalle.Producto.Codigo
                dtrRowDetDevolucion.Item(2) = detalle.Producto.Descripcion
                dtrRowDetDevolucion.Item(3) = detalle.Cantidad
                dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
                dtrRowDetDevolucion.Item(5) = detalle.PrecioVenta
                dtrRowDetDevolucion.Item(6) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(5)
                dtrRowDetDevolucion.Item(7) = detalle.Producto.Excento
                dtrRowDetDevolucion.Item(8) = 0
                dtbDetalleDevolucion.Rows.Add(dtrRowDetDevolucion)
            End If
        Next
        grdDetalleDevolucion.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim dblSubTotal As Decimal = 0
        Dim dblSubTotalSinIVA As Decimal = 0
        Dim dblPorcentajeIVA As Decimal = 0
        Dim dblImpuesto As Decimal = 0
        Dim dblTotal As Decimal = 0
        Dim dblDescuento As Decimal = 0
        dblGrabado = 0
        dblExcento = 0
        For Each detalle As DetalleFactura In factura.DetalleFactura
            If Not detalle.Excento Then
                dblGrabado += detalle.Cantidad * detalle.PrecioVenta
            Else
                dblExcento += detalle.Cantidad * detalle.PrecioVenta
            End If
        Next
        dblSubTotal += dblGrabado + dblExcento
        dblGrabado = 0
        dblExcento = 0
        For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
            If dtbDetalleDevolucion.Rows(I).Item(8) > 0 Then
                dblGrabado = dblGrabado + (CDbl(dtbDetalleDevolucion.Rows(I).Item(5)) * CDbl(dtbDetalleDevolucion.Rows(I).Item(8)))
            End If
        Next
        If dblSubTotal > 0 Then
            dblSubTotalSinIVA = dblGrabado
            dblGrabado = Math.Round(dblGrabado / (1 + (factura.PorcentajeIVA / 100)), 2, MidpointRounding.AwayFromZero)
            dblDescuento = factura.Descuento / dblSubTotal * dblGrabado
            dblGrabado = dblGrabado - dblDescuento
            dblImpuesto = dblGrabado * (factura.PorcentajeIVA / 100)
        End If
        dblGrabado = Math.Round(dblGrabado, 2, MidpointRounding.AwayFromZero)
        dblImpuesto = Math.Round(dblImpuesto, 2, MidpointRounding.AwayFromZero)
        dblDescuento = Math.Round(dblDescuento, 2, MidpointRounding.AwayFromZero)
        txtImpuesto.Text = FormatNumber(dblImpuesto, 2)
        txtSubTotal.Text = FormatNumber(dblGrabado, 2)
        txtDescuento.Text = FormatNumber(dblDescuento, 2)
        dblTotal = Math.Round(dblExcento + dblGrabado + dblImpuesto, 2, MidpointRounding.AwayFromZero)
        txtTotal.Text = FormatNumber(dblTotal, 2)
    End Sub

    Private Sub CargarFactura(intIdFactura As Integer)
        Try
            'factura = servicioFacturacion.ObtenerFactura(intIdFactura)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        If factura IsNot Nothing Then
            cliente = factura.Cliente
            txtCliente.Text = factura.Cliente.Nombre
            CargarDetalleFactura(factura)
            CargarTotales()
        Else
            MessageBox.Show("El número de factura ingresado no existe. Por favor verifique.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmDevolucion_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        IniciaDetalleDevolucion()
        EstablecerPropiedadesDataGridView()
        grdDetalleDevolucion.DataSource = dtbDetalleDevolucion
        bolInit = False
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
    End Sub

    Private Sub grdDetalleDevolucion_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDetalleDevolucion.CellEndEdit
        If grdDetalleDevolucion.CurrentCell.Value.ToString() = "" Then
            grdDetalleDevolucion.CurrentCell.Value = 0
        Else
            If grdDetalleDevolucion.CurrentCell.ColumnIndex = 5 Then
                grdDetalleDevolucion.CurrentRow.Cells(6).Value = grdDetalleDevolucion.CurrentRow.Cells(3).Value * grdDetalleDevolucion.CurrentCell.Value
            ElseIf grdDetalleDevolucion.CurrentCell.ColumnIndex = 8 Then
                If grdDetalleDevolucion.CurrentCell.Value > grdDetalleDevolucion.CurrentRow.Cells(3).Value Then
                    MessageBox.Show("La cantidad ingresada de artículos por devolver excede la cantidad de artículos de la factura.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    grdDetalleDevolucion.CurrentCell.Value = 0
                End If
            End If
        End If
        CargarTotales()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdDevolucion.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        cliente = Nothing
        factura = Nothing
        txtCliente.Text = ""
        txtIdFactura.Text = ""
        dtbDetalleDevolucion.Rows.Clear()
        grdDetalleDevolucion.Refresh()
        grdDetalleDevolucion.ReadOnly = False
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        txtIdFactura.Focus()
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdDevolucion.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioFacturacion.AnularDevolucionCliente(txtIdDevolucion.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(CmdAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaDevolucionCliente()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                'devolucion = servicioFacturacion.ObtenerDevolucionCliente(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If devolucion IsNot Nothing Then
                'factura = servicioFacturacion.ObtenerFactura(devolucion.IdFactura)
                txtIdDevolucion.Text = devolucion.IdDevolucion
                txtIdFactura.Text = devolucion.IdFactura
                cliente = devolucion.Cliente
                txtCliente.Text = factura.Cliente.Nombre
                txtFecha.Text = devolucion.Fecha
                CargarDetalleDevolucion(devolucion)
                CargarTotales()
                grdDetalleDevolucion.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnBuscarFactura_Click(sender As Object, e As EventArgs) Handles btnBuscarFactura.Click
        Dim formBusqueda As New FrmBusquedaFactura()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            txtIdFactura.Text = FrmMenuPrincipal.intBusqueda
            CargarFactura(txtIdFactura.Text)
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Not cliente Is Nothing And txtFecha.Text <> "" And Not factura Is Nothing And CDbl(txtTotal.Text) > 0 Then
            If txtIdDevolucion.Text = "" Then
                devolucion = New DevolucionCliente With {
                    .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                    .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
                    .IdCliente = factura.IdCliente,
                    .IdFactura = factura.IdFactura,
                    .Fecha = Now(),
                    .Excento = dblExcento,
                    .Grabado = dblGrabado,
                    .PorcentajeIVA = factura.PorcentajeIVA,
                    .Impuesto = CDbl(txtImpuesto.Text)
                }
                For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
                    detalleDevolucion = New DetalleDevolucionCliente With {
                        .IdProducto = dtbDetalleDevolucion.Rows(I).Item(0),
                        .Cantidad = dtbDetalleDevolucion.Rows(I).Item(3),
                        .PrecioCosto = dtbDetalleDevolucion.Rows(I).Item(4),
                        .PrecioVenta = dtbDetalleDevolucion.Rows(I).Item(5),
                        .Excento = dtbDetalleDevolucion.Rows(I).Item(7),
                        .CantDevolucion = dtbDetalleDevolucion.Rows(I).Item(8)
                    }
                    devolucion.DetalleDevolucionCliente.Add(detalleDevolucion)
                Next
                Try
                    'devolucion = servicioFacturacion.AgregarDevolucionCliente(devolucion)
                    txtIdDevolucion.Text = devolucion.IdDevolucion
                Catch ex As Exception
                    txtIdDevolucion.Text = ""
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            grdDetalleDevolucion.ReadOnly = True
            CmdImprimir.Enabled = True
            CmdAgregar.Enabled = True
            CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
            CmdImprimir.Focus()
            CmdGuardar.Enabled = False
        Else
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdDevolucion.Text <> "" Then
            comprobante = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmMenuPrincipal.usuarioGlobal,
                .empresa = FrmMenuPrincipal.empresaGlobal,
                .equipo = FrmMenuPrincipal.equipoGlobal,
                .strId = txtIdDevolucion.Text,
                .strDocumento = txtIdFactura.Text,
                .strNombre = txtCliente.Text,
                .strFecha = txtFecha.Text,
                .strFormaPago = "Contado",
                .strSubTotal = txtSubTotal.Text,
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleDevolucion = New List(Of ModuloImpresion.clsDetalleComprobante)
            For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
                If dtbDetalleDevolucion.Rows(I).Item(8) > 0 Then
                    detalleComprobante = New ModuloImpresion.clsDetalleComprobante With {
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
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub txtIdFactura_Validated(sender As Object, e As EventArgs) Handles txtIdFactura.Validated
        If txtIdFactura.Text <> "" Then
            If Not factura Is Nothing Then
                If txtIdFactura.Text <> factura.IdFactura Or dtbDetalleDevolucion.Rows.Count = 0 Then
                    CargarFactura(txtIdFactura.Text)
                End If
            Else
                CargarFactura(txtIdFactura.Text)
            End If
        End If
    End Sub
#End Region
End Class