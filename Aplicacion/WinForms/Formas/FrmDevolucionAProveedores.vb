Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmDevolucionAProveedores
#Region "Variables"
    Private decExcento, decGravado, decSubTotal As Decimal
    Private I As Short
    Private dtbDatosLocal, dtbDetalleDevolucion As DataTable
    Private dtrRowDetDevolucion As DataRow
    Private arrDetalleDevolucion As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private devolucion As DevolucionProveedor
    Private detalleDevolucion As DetalleDevolucionProveedor
    Private compra As Compra
    Private proveedor As Proveedor
    Private comprobante As ModuloImpresion.ClsComprobante
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
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
        grdDetalleDevolucion.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 340
        dvcCodigo.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcCodigo.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 75
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcCodigo.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcCodigo.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        dvcExc.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcExc)

        dvcCantDevolucion.DataPropertyName = "CANTDEVOLUCION"
        dvcCantDevolucion.HeaderText = "Cant-Devol"
        dvcCantDevolucion.Width = 60
        dvcCantDevolucion.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcExc.ReadOnly = False
        grdDetalleDevolucion.Columns.Add(dvcCantDevolucion)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
        dvcPorcentajeIVA.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcPorcentajeIVA)
    End Sub

    Private Sub CargarDetalleDevolucion(ByVal devolucion As DevolucionProveedor)
        dtbDetalleDevolucion.Rows.Clear()
        For Each detalle As DetalleDevolucionProveedor In devolucion.DetalleDevolucionProveedor
            dtrRowDetDevolucion = dtbDetalleDevolucion.NewRow
            dtrRowDetDevolucion.Item(0) = detalle.IdProducto
            dtrRowDetDevolucion.Item(1) = detalle.Producto.Codigo
            dtrRowDetDevolucion.Item(2) = detalle.Producto.Descripcion
            dtrRowDetDevolucion.Item(3) = detalle.Cantidad
            dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
            dtrRowDetDevolucion.Item(5) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(4)
            dtrRowDetDevolucion.Item(6) = detalle.Excento
            dtrRowDetDevolucion.Item(7) = detalle.CantDevolucion
            dtrRowDetDevolucion.Item(8) = detalle.PorcentajeIVA
            dtbDetalleDevolucion.Rows.Add(dtrRowDetDevolucion)
        Next
        grdDetalleDevolucion.Refresh()
    End Sub

    Private Sub CargarDetalleCompra(ByVal compra As Compra)
        dtbDetalleDevolucion.Rows.Clear()
        For Each detalle As DetalleCompra In compra.DetalleCompra
            If detalle.Producto.TipoProducto.IdTipoProducto = StaticTipoProducto.Producto Then
                dtrRowDetDevolucion = dtbDetalleDevolucion.NewRow
                dtrRowDetDevolucion.Item(0) = detalle.IdProducto
                dtrRowDetDevolucion.Item(1) = detalle.Producto.Codigo
                dtrRowDetDevolucion.Item(2) = detalle.Producto.Descripcion
                dtrRowDetDevolucion.Item(3) = detalle.Cantidad
                dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
                dtrRowDetDevolucion.Item(5) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(4)
                dtrRowDetDevolucion.Item(6) = detalle.Excento
                dtrRowDetDevolucion.Item(7) = 0
                dtrRowDetDevolucion.Item(8) = detalle.PorcentajeIVA
                dtbDetalleDevolucion.Rows.Add(dtrRowDetDevolucion)
            End If
        Next
        grdDetalleDevolucion.Refresh()
    End Sub

    Private Sub CargarTotales()
        Dim decImpuesto As Decimal = 0
        decExcento = 0
        decGravado = 0
        decSubTotal = 0
        For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
            If dtbDetalleDevolucion.Rows(I).Item(7) > 0 Then
                Dim decTotalPorLinea As Decimal = dtbDetalleDevolucion.Rows(I).Item(4).Value * dtbDetalleDevolucion.Rows(I).Item(7).Value
                If dtbDetalleDevolucion.Rows(I).Item(6) = 0 Then
                    decGravado += decTotalPorLinea
                    decImpuesto += decTotalPorLinea * dtbDetalleDevolucion.Rows(I).Item(8).Value
                Else
                    decExcento += decTotalPorLinea
                End If
            End If
        Next
        decSubTotal = decGravado + decExcento
        If decSubTotal > 0 And compra.Descuento > 0 Then
            decImpuesto = 0
            For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
                Dim decDescuentoPorLinea As Decimal = 0
                Dim decTotalPorLinea As Decimal = dtbDetalleDevolucion.Rows(I).Item(4).Value * dtbDetalleDevolucion.Rows(I).Item(7).Value
                decDescuentoPorLinea = decTotalPorLinea - (compra.Descuento / decSubTotal * decTotalPorLinea)
                decImpuesto += decDescuentoPorLinea * dtbDetalleDevolucion.Rows(I).Item(8).Value
            Next
        End If
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        decExcento = Math.Round(decExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(decExcento + decGravado + txtImpuesto.Text, 2)
    End Sub

    Private Sub CargarCompra(intIdCompra As Integer)
        Try
            'compra = servicioCompras.ObtenerCompra(intIdCompra)
            If compra IsNot Nothing Then
                proveedor = compra.Proveedor
                txtProveedor.Text = proveedor.Nombre
                txtNumFactura.Text = compra.NoDocumento
                CargarDetalleCompra(compra)
                CargarTotales()
            Else
                MessageBox.Show("El número de compra ingresado no existe. Por favor verifique.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmDevolucion_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            IniciaDetalleDevolucion()
            EstablecerPropiedadesDataGridView()
            grdDetalleDevolucion.DataSource = dtbDetalleDevolucion
            bolInit = False
            txtSubTotal.Text = FormatNumber(0, 2)
            txtImpuesto.Text = FormatNumber(0, 2)
            txtTotal.Text = FormatNumber(0, 2)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub grdDetalleDevolucion_CellEndEdit(sender As Object, e As System.Windows.Forms.DataGridViewCellEventArgs) Handles grdDetalleDevolucion.CellEndEdit
        If grdDetalleDevolucion.CurrentCell.Value.ToString() = "" Then
            grdDetalleDevolucion.CurrentCell.Value = 0
        Else
            If grdDetalleDevolucion.CurrentCell.Value > grdDetalleDevolucion.CurrentRow.Cells(3).Value Then
                MessageBox.Show("La cantidad ingresada de artículos por devolver excede la cantidad de artículos de la compra.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                grdDetalleDevolucion.CurrentCell.Value = 0
            End If
        End If
        CargarTotales()
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdDevolucion.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        proveedor = Nothing
        compra = Nothing
        txtProveedor.Text = ""
        txtIdCompra.Text = ""
        dtbDetalleDevolucion.Rows.Clear()
        grdDetalleDevolucion.Refresh()
        grdDetalleDevolucion.ReadOnly = False
        txtSubTotal.Text = FormatNumber(0, 2)
        txtImpuesto.Text = FormatNumber(0, 2)
        txtTotal.Text = FormatNumber(0, 2)
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        txtIdCompra.Focus()
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdDevolucion.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    'servicioCompras.AnularDevolucionProveedor(txtIdDevolucion.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
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
        Dim formBusqueda As New FrmBusquedaDevolucionProveedor()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'devolucion = servicioCompras.ObtenerDevolucionProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If devolucion IsNot Nothing Then
                'compra = servicioCompras.ObtenerCompra(devolucion.IdCompra)
                txtIdDevolucion.Text = devolucion.IdDevolucion
                txtIdCompra.Text = devolucion.IdCompra
                proveedor = devolucion.Proveedor
                txtProveedor.Text = compra.NombreProveedor
                txtFecha.Text = devolucion.Fecha
                CargarDetalleDevolucion(devolucion)
                CargarTotales()
                grdDetalleDevolucion.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnBuscarCompra_Click(sender As Object, e As EventArgs) Handles btnBuscarFactura.Click
        Dim formBusqueda As New FrmBusquedaCompra()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            txtIdCompra.Text = FrmPrincipal.intBusqueda
            CargarCompra(txtIdCompra.Text)
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If proveedor Is Nothing Or txtFecha.Text = "" Or compra Is Nothing Or CDbl(txtTotal.Text) = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
        If txtIdDevolucion.Text = "" Then
            devolucion = New DevolucionProveedor With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .IdProveedor = compra.IdProveedor,
                .IdCompra = compra.IdCompra,
                .Fecha = Now(),
                .Excento = decExcento,
                .Gravado = decGravado,
                .Impuesto = CDbl(txtImpuesto.Text)
            }
            For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
                detalleDevolucion = New DetalleDevolucionProveedor With {
                    .IdProducto = dtbDetalleDevolucion.Rows(I).Item(0),
                    .Cantidad = dtbDetalleDevolucion.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleDevolucion.Rows(I).Item(4),
                    .Excento = dtbDetalleDevolucion.Rows(I).Item(6),
                    .CantDevolucion = dtbDetalleDevolucion.Rows(I).Item(7),
                    .PorcentajeIVA = dtbDetalleDevolucion.Rows(I).Item(8)
                }
                devolucion.DetalleDevolucionProveedor.Add(detalleDevolucion)
            Next
            Try
                'devolucion = servicioCompras.AgregarDevolucionProveedor(devolucion)
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
        CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdDevolucion.Text <> "" Then
            comprobante = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdDevolucion.Text,
                .strDocumento = txtNumFactura.Text,
                .strNombre = txtProveedor.Text,
                .strFecha = txtFecha.Text,
                .strFormaPago = "Contado",
                .strSubTotal = txtSubTotal.Text,
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleDevolucion = New List(Of ModuloImpresion.clsDetalleComprobante)
            For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
                If dtbDetalleDevolucion.Rows(I).Item(7) > 0 Then
                    detalleComprobante = New ModuloImpresion.clsDetalleComprobante With {
                        .strDescripcion = dtbDetalleDevolucion.Rows(I).Item(1) + "-" + dtbDetalleDevolucion.Rows(I).Item(2),
                        .strCantidad = CDbl(dtbDetalleDevolucion.Rows(I).Item(7)),
                        .strPrecio = FormatNumber(dtbDetalleDevolucion.Rows(I).Item(4), 2),
                        .strTotalLinea = FormatNumber(CDbl(dtbDetalleDevolucion.Rows(I).Item(7)) * CDbl(dtbDetalleDevolucion.Rows(I).Item(4)), 2)
                    }
                    arrDetalleDevolucion.Add(detalleComprobante)
                End If
            Next
            comprobante.arrDetalleComprobante = arrDetalleDevolucion
            Try
                ModuloImpresion.ImprimirDevolucionProveedor(comprobante)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub txtIdCompra_Validated(sender As Object, e As EventArgs) Handles txtIdCompra.Validated
        If txtIdCompra.Text <> "" Then
            If Not compra Is Nothing Then
                If txtIdCompra.Text <> compra.IdCompra Or dtbDetalleDevolucion.Rows.Count = 0 Then
                    CargarCompra(txtIdCompra.Text)
                End If
            Else
                CargarCompra(txtIdCompra.Text)
            End If
        End If
    End Sub
#End Region
End Class