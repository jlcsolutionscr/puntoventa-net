Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmDevolucionDeClientes
#Region "Variables"
    Private dblExcento, decGravado, decSubTotal As Decimal
    Private I As Short
    Private dtbDetalleDevolucion As DataTable
    Private dtrRowDetDevolucion As DataRow
    Private arrDetalleDevolucion As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private devolucion As DevolucionCliente
    Private detalleDevolucion As DetalleDevolucionCliente
    Private factura As Factura
    Private cliente As Cliente
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
        grdDetalleDevolucion.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 340
        dvcDescripcion.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcCantidad.ReadOnly = True
        grdDetalleDevolucion.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "PrecioCosto"
        dvcPrecioCosto.Visible = False
        grdDetalleDevolucion.Columns.Add(dvcPrecioCosto)

        dvcPrecioVenta.DataPropertyName = "PRECIOVENTA"
        dvcPrecioVenta.HeaderText = "Precio"
        dvcPrecioVenta.Width = 75
        dvcPrecioVenta.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcPrecioVenta.ReadOnly = False
        grdDetalleDevolucion.Columns.Add(dvcPrecioVenta)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
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
        dvcCantDevolucion.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dvcExc.ReadOnly = False
        grdDetalleDevolucion.Columns.Add(dvcCantDevolucion)

        dvcPorcentajeIVA.DataPropertyName = "PORCENTAJEIVA"
        dvcPorcentajeIVA.HeaderText = "PorcIVA"
        dvcPorcentajeIVA.Width = 0
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
            dtrRowDetDevolucion.Item(3) = detalle.Cantidad
            dtrRowDetDevolucion.Item(4) = detalle.PrecioCosto
            dtrRowDetDevolucion.Item(5) = detalle.PrecioVenta
            dtrRowDetDevolucion.Item(6) = dtrRowDetDevolucion.Item(3) * dtrRowDetDevolucion.Item(5)
            dtrRowDetDevolucion.Item(7) = detalle.Excento
            dtrRowDetDevolucion.Item(8) = detalle.CantDevolucion
            dtrRowDetDevolucion.Item(9) = detalle.PorcentajeIVA
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
        For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
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
        decGravado = Math.Round(decGravado, 2, MidpointRounding.AwayFromZero)
        dblExcento = Math.Round(dblExcento, 2, MidpointRounding.AwayFromZero)
        decImpuesto = Math.Round(decImpuesto, 2, MidpointRounding.AwayFromZero)
        txtSubTotal.Text = FormatNumber(decSubTotal, 2)
        txtImpuesto.Text = FormatNumber(decImpuesto, 2)
        txtTotal.Text = FormatNumber(dblExcento + decGravado + decImpuesto, 2)
    End Sub

    Private Async Sub CargarFactura(intIdFactura As Integer)
        Try
            factura = Await Puntoventa.ObtenerFactura(intIdFactura, FrmPrincipal.usuarioGlobal.Token)
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

    Private Sub grdDetalleDevolucion_CellEndEdit(sender As Object, e As DataGridViewCellEventArgs) Handles grdDetalleDevolucion.CellEndEdit
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
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
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
        btnBuscarFactura.Enabled = True
        txtIdFactura.Focus()
    End Sub

    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdDevolucion.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularDevolucionCliente(txtIdDevolucion.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                CmdAgregar_Click(CmdAgregar, New EventArgs())
            End If
        End If
    End Sub

    Private Async Sub CmdBuscar_Click(sender As Object, e As EventArgs) Handles CmdBuscar.Click
        Dim formBusqueda As New FrmBusquedaDevolucionCliente()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                devolucion = Await Puntoventa.ObtenerDevolucionCliente(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If devolucion IsNot Nothing Then
                txtIdDevolucion.Text = devolucion.IdDevolucion
                txtIdFactura.Text = devolucion.IdFactura
                cliente = devolucion.Cliente
                txtCliente.Text = cliente.Nombre
                txtFecha.Text = devolucion.Fecha
                CargarDetalleDevolucion(devolucion)
                CargarTotales()
                grdDetalleDevolucion.ReadOnly = True
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
                btnBuscarFactura.Enabled = False
            End If
        End If
    End Sub

    Private Sub btnBuscarFactura_Click(sender As Object, e As EventArgs) Handles btnBuscarFactura.Click
        Dim formBusqueda As New FrmBusquedaFactura()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            txtIdFactura.Text = FrmPrincipal.intBusqueda
            CargarFactura(txtIdFactura.Text)
        End If
    End Sub

    Private Async Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If Not cliente Is Nothing And txtFecha.Text <> "" And Not factura Is Nothing And CDbl(txtTotal.Text) > 0 Then
            CmdGuardar.Enabled = False
            If txtIdDevolucion.Text = "" Then
                devolucion = New DevolucionCliente With {
                    .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                    .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                    .IdCliente = factura.IdCliente,
                    .IdFactura = factura.IdFactura,
                    .Fecha = Now(),
                    .Excento = dblExcento,
                    .Gravado = decGravado,
                    .Impuesto = CDbl(txtImpuesto.Text)
                }
                For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
                    If dtbDetalleDevolucion.Rows(I).Item(8) > 0 Then
                        detalleDevolucion = New DetalleDevolucionCliente With {
                            .IdProducto = dtbDetalleDevolucion.Rows(I).Item(0),
                            .Cantidad = dtbDetalleDevolucion.Rows(I).Item(3),
                            .PrecioCosto = dtbDetalleDevolucion.Rows(I).Item(4),
                            .PrecioVenta = dtbDetalleDevolucion.Rows(I).Item(5),
                            .Excento = dtbDetalleDevolucion.Rows(I).Item(7),
                            .CantDevolucion = dtbDetalleDevolucion.Rows(I).Item(8),
                            .PorcentajeIVA = dtbDetalleDevolucion.Rows(I).Item(9)
                        }
                        devolucion.DetalleDevolucionCliente.Add(detalleDevolucion)
                    End If
                Next
                Try
                    txtIdDevolucion.Text = Await Puntoventa.AgregarDevolucionCliente(devolucion, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    CmdGuardar.Enabled = True
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
            btnBuscarFactura.Enabled = False
        Else
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdDevolucion.Text <> "" Then
            comprobante = New ModuloImpresion.ClsComprobante With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdDevolucion.Text,
                .strDocumento = txtIdFactura.Text,
                .strNombre = txtCliente.Text,
                .strFecha = txtFecha.Text,
                .strFormaPago = "Contado",
                .strSubTotal = txtSubTotal.Text,
                .strImpuesto = txtImpuesto.Text,
                .strTotal = txtTotal.Text
            }
            arrDetalleDevolucion = New List(Of ModuloImpresion.ClsDetalleComprobante)
            For I = 0 To dtbDetalleDevolucion.Rows.Count - 1
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