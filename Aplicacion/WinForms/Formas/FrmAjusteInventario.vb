Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmAjusteInventario
#Region "Variables"
    Private I As Short
    Private dtbDatosLocal, dtbDetalleAjusteInventario As DataTable
    Private dtrRowDetAjusteInventario As DataRow
    Private arrDetalleAjusteInventario As List(Of ModuloImpresion.clsDetalleComprobante)
    Private servicioMantenimiento As IMantenimientoService
    Private ajusteInventario As AjusteInventario
    Private detalleAjusteInventario As DetalleAjusteInventario
    Private comprobante As ModuloImpresion.clsAjusteInventario
    Private detalleComprobante As ModuloImpresion.clsDetalleComprobante
    Private bolInit As Boolean = True
    Private producto As Producto
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleAjusteInventario()
        dtbDetalleAjusteInventario = New DataTable()
        dtbDetalleAjusteInventario.Columns.Add("IDPRODUCTO", GetType(Int32))
        dtbDetalleAjusteInventario.Columns.Add("CODIGO", GetType(String))
        dtbDetalleAjusteInventario.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleAjusteInventario.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("EXCENTO", GetType(Int32))
        dtbDetalleAjusteInventario.PrimaryKey = {dtbDetalleAjusteInventario.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleAjusteInventario.Columns.Clear()
        grdDetalleAjusteInventario.AutoGenerateColumns = False

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
        grdDetalleAjusteInventario.Columns.Add(dvcIdProducto)

        dvcCodigo.DataPropertyName = "CODIGO"
        dvcCodigo.HeaderText = "Código"
        dvcCodigo.Width = 125
        grdDetalleAjusteInventario.Columns.Add(dvcCodigo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 340
        grdDetalleAjusteInventario.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 75
        dvcPrecioCosto.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcTotal)

        dvcExc.DataPropertyName = "EXCENTO"
        dvcExc.HeaderText = "Exc"
        dvcExc.Width = 20
        grdDetalleAjusteInventario.Columns.Add(dvcExc)
    End Sub

    Private Sub CargarDetalleAjusteInventario(ByVal ajusteInventario As AjusteInventario)
        dtbDetalleAjusteInventario.Rows.Clear()
        For Each detalle As DetalleAjusteInventario In ajusteInventario.DetalleAjusteInventario
            dtrRowDetAjusteInventario = dtbDetalleAjusteInventario.NewRow
            dtrRowDetAjusteInventario.Item(0) = detalle.IdProducto
            dtrRowDetAjusteInventario.Item(1) = detalle.Producto.Codigo
            dtrRowDetAjusteInventario.Item(2) = detalle.Producto.Descripcion
            dtrRowDetAjusteInventario.Item(3) = detalle.Cantidad
            dtrRowDetAjusteInventario.Item(4) = detalle.PrecioCosto
            dtrRowDetAjusteInventario.Item(5) = dtrRowDetAjusteInventario.Item(3) * dtrRowDetAjusteInventario.Item(4)
            dtrRowDetAjusteInventario.Item(6) = detalle.Excento
            dtbDetalleAjusteInventario.Rows.Add(dtrRowDetAjusteInventario)
        Next
        grdDetalleAjusteInventario.Refresh()
    End Sub

    Private Sub CargarLineaDetalleAjusteInventario(ByVal producto As Producto)
        Dim intIndice As Integer = dtbDetalleAjusteInventario.Rows.IndexOf(dtbDetalleAjusteInventario.Rows.Find(producto.IdProducto))
        If intIndice >= 0 Then
            dtbDetalleAjusteInventario.Rows(intIndice).Item(1) = producto.Codigo
            dtbDetalleAjusteInventario.Rows(intIndice).Item(2) = producto.Descripcion
            dtbDetalleAjusteInventario.Rows(intIndice).Item(3) += txtCantidad.Text
            dtbDetalleAjusteInventario.Rows(intIndice).Item(4) = producto.PrecioCosto
            dtbDetalleAjusteInventario.Rows(intIndice).Item(5) = dtbDetalleAjusteInventario.Rows(intIndice).Item(3) * dtbDetalleAjusteInventario.Rows(intIndice).Item(4)
            dtbDetalleAjusteInventario.Rows(intIndice).Item(6) = producto.Excento
        Else
            dtrRowDetAjusteInventario = dtbDetalleAjusteInventario.NewRow
            dtrRowDetAjusteInventario.Item(0) = producto.IdProducto
            dtrRowDetAjusteInventario.Item(1) = producto.Codigo
            dtrRowDetAjusteInventario.Item(2) = producto.Descripcion
            dtrRowDetAjusteInventario.Item(3) = txtCantidad.Text
            dtrRowDetAjusteInventario.Item(4) = producto.PrecioCosto
            dtrRowDetAjusteInventario.Item(5) = dtrRowDetAjusteInventario.Item(3) * dtrRowDetAjusteInventario.Item(4)
            dtrRowDetAjusteInventario.Item(6) = producto.Excento
            dtbDetalleAjusteInventario.Rows.Add(dtrRowDetAjusteInventario)
        End If
        grdDetalleAjusteInventario.Refresh()
    End Sub

    Private Sub ValidarProducto()
        If Not bolInit Then
            If txtCodigo.Text <> "" Then
                If FrmMenuPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                    If txtCodigo.Text.IndexOf(" ") >= 0 Then
                        txtCodigo.Text = txtCodigo.Text.Substring(0, txtCodigo.Text.IndexOf(" "))
                    End If
                End If
                Try
                    producto = servicioMantenimiento.ObtenerProductoPorCodigo(txtCodigo.Text)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                If producto Is Nothing Then
                    MessageBox.Show("El código ingresado no existe. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    txtCodigo.Text = ""
                    txtCantidad.Text = "1"
                    txtPrecioCosto.Text = ""
                    txtCodigo.Focus()
                    Exit Sub
                End If
                If txtCantidad.Text = "" Then txtCantidad.Text = "1"
                txtDescripcion.Text = producto.Descripcion
                txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
            End If
        End If
    End Sub

    Public Sub ValidaNumero(ByVal e As KeyPressEventArgs, ByVal oText As TextBox, Optional ByVal pbConPuntoDec As Boolean = True, Optional ByVal pnNumDecimal As Integer = 2, Optional ByVal psSimbolo As String = ".")
        Dim nDig As Integer
        Dim sTexto As String = String.Concat(oText.Text, e.KeyChar)
        If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = Keys.Return Or Asc(e.KeyChar) = 45 Then
            e.Handled = False
            Exit Sub
        End If
        If pbConPuntoDec Then
            If Char.IsDigit(e.KeyChar) Or e.KeyChar = psSimbolo Then
                e.Handled = False
            ElseIf Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
            nDig = sTexto.Length
            If nDig = 1 And e.KeyChar = psSimbolo Then
                e.Handled = True
            End If
            If oText.SelectedText.Length = 0 Then
                If sTexto.IndexOf(psSimbolo) >= 0 And (nDig - (sTexto.IndexOf(psSimbolo) + 1)) > pnNumDecimal Then
                    e.Handled = True
                End If
            End If
        Else
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            ElseIf Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmAjusteInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioMantenimiento = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        IniciaDetalleAjusteInventario()
        EstablecerPropiedadesDataGridView()
        grdDetalleAjusteInventario.DataSource = dtbDetalleAjusteInventario
        bolInit = False
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdAjuste.Text = ""
        txtFecha.Text = FrmMenuPrincipal.ObtenerFechaFormateada(Now())
        txtDescAjuste.Text = ""
        dtbDetalleAjusteInventario.Rows.Clear()
        grdDetalleAjusteInventario.Refresh()
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        txtDescAjuste.Focus()
    End Sub

    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdAjuste.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    servicioMantenimiento.AnularAjusteInventario(txtIdAjuste.Text, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
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
        Dim formBusqueda As New FrmBusquedaAjusteInventario()
        FrmMenuPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                ajusteInventario = servicioMantenimiento.ObtenerAjusteInventario(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If ajusteInventario IsNot Nothing Then
                txtIdAjuste.Text = ajusteInventario.IdAjuste
                txtFecha.Text = ajusteInventario.Fecha
                txtDescAjuste.Text = ajusteInventario.Descripcion
                CargarDetalleAjusteInventario(ajusteInventario)
                CmdImprimir.Enabled = True
                CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If txtFecha.Text = "" Or txtDescAjuste.Text = "" Or grdDetalleAjusteInventario.RowCount = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdAjuste.Text = "" Then
            ajusteInventario = New AjusteInventario With {
                .IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa,
                .IdUsuario = FrmMenuPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = Now(),
                .Descripcion = txtDescAjuste.Text
            }
            For I = 0 To dtbDetalleAjusteInventario.Rows.Count - 1
                detalleAjusteInventario = New DetalleAjusteInventario With {
                    .IdProducto = dtbDetalleAjusteInventario.Rows(I).Item(0),
                    .Cantidad = dtbDetalleAjusteInventario.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleAjusteInventario.Rows(I).Item(4),
                    .Excento = dtbDetalleAjusteInventario.Rows(I).Item(6)
                }
                ajusteInventario.DetalleAjusteInventario.Add(detalleAjusteInventario)
            Next
            Try
                ajusteInventario = servicioMantenimiento.AgregarAjusteInventario(ajusteInventario)
                txtIdAjuste.Text = ajusteInventario.IdAjuste
            Catch ex As Exception
                txtIdAjuste.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmMenuPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdAjuste.Text <> "" Then
            comprobante = New ModuloImpresion.clsAjusteInventario With {
                .usuario = FrmMenuPrincipal.usuarioGlobal,
                .empresa = FrmMenuPrincipal.empresaGlobal,
                .equipo = FrmMenuPrincipal.equipoGlobal,
                .strId = txtIdAjuste.Text,
                .strFecha = txtFecha.Text,
                .strDescripcion = txtDescAjuste.Text
            }
            arrDetalleAjusteInventario = New List(Of ModuloImpresion.clsDetalleComprobante)
            For I = 0 To dtbDetalleAjusteInventario.Rows.Count - 1
                If dtbDetalleAjusteInventario.Rows(I).Item(7) > 0 Then
                    detalleComprobante = New ModuloImpresion.clsDetalleComprobante With {
                        .strDescripcion = dtbDetalleAjusteInventario.Rows(I).Item(1) + "-" + dtbDetalleAjusteInventario.Rows(I).Item(2),
                        .strCantidad = CDbl(dtbDetalleAjusteInventario.Rows(I).Item(7)),
                        .strPrecio = FormatNumber(dtbDetalleAjusteInventario.Rows(I).Item(4), 2),
                        .strTotalLinea = FormatNumber(CDbl(dtbDetalleAjusteInventario.Rows(I).Item(7)) * CDbl(dtbDetalleAjusteInventario.Rows(I).Item(4)), 2)
                    }
                    arrDetalleAjusteInventario.Add(detalleComprobante)
                End If
            Next
            comprobante.arrDetalleComprobante = arrDetalleAjusteInventario
            Try
                ModuloImpresion.ImprimirAjusteInventario(comprobante)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intTipoPrecio = 1
        }
        FrmMenuPrincipal.strBusqueda = ""
        formBusProd.ShowDialog()
        If Not FrmMenuPrincipal.strBusqueda.Equals("") Then
            txtCodigo.Text = FrmMenuPrincipal.strBusqueda
            ValidarProducto()
        End If
        txtCodigo.Focus()
    End Sub

    Private Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If txtCodigo.Text <> "" And txtCantidad.Text <> "" Then
            CargarLineaDetalleAjusteInventario(producto)
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtCantidad.Text = "1"
            txtPrecioCosto.Text = ""
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub BtnEliminar_Click(sender As Object, e As EventArgs) Handles btnEliminar.Click
        If dtbDetalleAjusteInventario.Rows.Count > 0 Then
            dtbDetalleAjusteInventario.Rows.Remove(dtbDetalleAjusteInventario.Rows.Find(grdDetalleAjusteInventario.CurrentRow.Cells(0).Value))
            grdDetalleAjusteInventario.Refresh()
            txtCodigo.Focus()
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class