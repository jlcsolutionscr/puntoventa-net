Imports System.Collections.Generic
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmAjusteInventario
#Region "Variables"
    Private I As Short
    Private dtbDatosLocal, dtbDetalleAjusteInventario As DataTable
    Private dtrRowDetAjusteInventario As DataRow
    Private arrDetalleAjusteInventario As List(Of ModuloImpresion.ClsDetalleComprobante)
    Private ajusteInventario As AjusteInventario
    Private detalleAjusteInventario As DetalleAjusteInventario
    Private comprobante As ModuloImpresion.ClsAjusteInventario
    Private detalleComprobante As ModuloImpresion.ClsDetalleComprobante
    Private bolInit As Boolean = True
    Private producto As Producto
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleAjusteInventario()
        dtbDetalleAjusteInventario = New DataTable()
        dtbDetalleAjusteInventario.Columns.Add("IDPRODUCTO", GetType(Integer))
        dtbDetalleAjusteInventario.Columns.Add("CODIGO", GetType(String))
        dtbDetalleAjusteInventario.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleAjusteInventario.Columns.Add("CANTIDAD", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("PRECIOCOSTO", GetType(Decimal))
        dtbDetalleAjusteInventario.Columns.Add("TOTAL", GetType(Decimal))
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
        dvcDescripcion.Width = 360
        grdDetalleAjusteInventario.Columns.Add(dvcDescripcion)

        dvcCantidad.DataPropertyName = "CANTIDAD"
        dvcCantidad.HeaderText = "Cantidad"
        dvcCantidad.Width = 60
        dvcCantidad.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcCantidad)

        dvcPrecioCosto.DataPropertyName = "PRECIOCOSTO"
        dvcPrecioCosto.HeaderText = "Precio"
        dvcPrecioCosto.Width = 75
        dvcPrecioCosto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcPrecioCosto)

        dvcTotal.DataPropertyName = "TOTAL"
        dvcTotal.HeaderText = "Total"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleAjusteInventario.Columns.Add(dvcTotal)
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
        Else
            dtrRowDetAjusteInventario = dtbDetalleAjusteInventario.NewRow
            dtrRowDetAjusteInventario.Item(0) = producto.IdProducto
            dtrRowDetAjusteInventario.Item(1) = producto.Codigo
            dtrRowDetAjusteInventario.Item(2) = producto.Descripcion
            dtrRowDetAjusteInventario.Item(3) = txtCantidad.Text
            dtrRowDetAjusteInventario.Item(4) = producto.PrecioCosto
            dtrRowDetAjusteInventario.Item(5) = dtrRowDetAjusteInventario.Item(3) * dtrRowDetAjusteInventario.Item(4)
            dtbDetalleAjusteInventario.Rows.Add(dtrRowDetAjusteInventario)
        End If
        grdDetalleAjusteInventario.Refresh()
    End Sub

    Private Sub CargarDatosProducto(producto As Producto)
        If producto Is Nothing Then
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecioCosto.Text = FormatNumber(0, 2)
            txtCodigo.Focus()
            Exit Sub
        Else
            Dim decTasaImpuesto As Decimal = producto.ParametroImpuesto.TasaImpuesto
            txtCodigo.Text = producto.Codigo
            If txtCantidad.Text = "" Then txtCantidad.Text = "1"
            txtDescripcion.Text = producto.Descripcion
            txtPrecioCosto.Text = FormatNumber(producto.PrecioCosto, 2)
            If FrmPrincipal.bolModificaDescripcion = True Then
                txtDescripcion.Focus()
            Else
                txtPrecioCosto.Focus()
            End If
        End If
    End Sub

    Private Async Function CargarAutoCompletarProducto() As Task
        Dim source As AutoCompleteStringCollection = New AutoCompleteStringCollection()
        Dim listOfProducts As IList(Of Producto) = Await Puntoventa.ObtenerListadoProductos(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, 1, 0, True, FrmPrincipal.usuarioGlobal.Token)
        For Each producto As Producto In listOfProducts
            source.Add(String.Concat(producto.Codigo, " ", producto.Descripcion))
        Next
        txtCodigo.AutoCompleteCustomSource = source
        txtCodigo.AutoCompleteSource = AutoCompleteSource.CustomSource
        txtCodigo.AutoCompleteMode = AutoCompleteMode.SuggestAppend
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmAjusteInventario_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        KeyPreview = True
    End Sub

    Private Async Sub FrmAjusteInventario_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
            If FrmPrincipal.empresaGlobal.AutoCompletaProducto = True Then
                Await CargarAutoCompletarProducto()
            End If
            IniciaDetalleAjusteInventario()
            EstablecerPropiedadesDataGridView()
            grdDetalleAjusteInventario.DataSource = dtbDetalleAjusteInventario
            bolInit = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub CmdAgregar_Click(sender As Object, e As EventArgs) Handles CmdAgregar.Click
        txtIdAjuste.Text = ""
        txtFecha.Text = FrmPrincipal.ObtenerFechaFormateada(Now())
        txtDescAjuste.Text = ""
        dtbDetalleAjusteInventario.Rows.Clear()
        grdDetalleAjusteInventario.Refresh()
        CmdAnular.Enabled = False
        CmdGuardar.Enabled = True
        CmdImprimir.Enabled = False
        txtDescAjuste.Focus()
    End Sub

    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If txtIdAjuste.Text <> "" Then
            If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Try
                    Await Puntoventa.AnularAjusteInventario(txtIdAjuste.Text, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
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
        Dim formBusqueda As New FrmBusquedaAjusteInventario()
        FrmPrincipal.intBusqueda = 0
        formBusqueda.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ajusteInventario = Await Puntoventa.ObtenerAjusteInventario(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
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
                CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
                CmdGuardar.Enabled = False
            End If
        End If
    End Sub

    Private Async Sub CmdGuardar_Click(sender As Object, e As EventArgs) Handles CmdGuardar.Click
        If txtFecha.Text = "" Or txtDescAjuste.Text = "" Or grdDetalleAjusteInventario.RowCount = 0 Then
            MessageBox.Show("Información incompleta.  Favor verificar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If txtIdAjuste.Text = "" Then
            ajusteInventario = New AjusteInventario With {
                .IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa,
                .IdSucursal = FrmPrincipal.equipoGlobal.IdSucursal,
                .IdUsuario = FrmPrincipal.usuarioGlobal.IdUsuario,
                .Fecha = Now(),
                .Descripcion = txtDescAjuste.Text
            }
            For I = 0 To dtbDetalleAjusteInventario.Rows.Count - 1
                detalleAjusteInventario = New DetalleAjusteInventario With {
                    .IdProducto = dtbDetalleAjusteInventario.Rows(I).Item(0),
                    .Cantidad = dtbDetalleAjusteInventario.Rows(I).Item(3),
                    .PrecioCosto = dtbDetalleAjusteInventario.Rows(I).Item(4)
                }
                ajusteInventario.DetalleAjusteInventario.Add(detalleAjusteInventario)
            Next
            Try
                txtIdAjuste.Text = Await Puntoventa.AgregarAjusteInventario(ajusteInventario, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                txtIdAjuste.Text = ""
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
        MessageBox.Show("Transacción efectuada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        CmdImprimir.Enabled = True
        CmdAgregar.Enabled = True
        CmdAnular.Enabled = FrmPrincipal.usuarioGlobal.Modifica
        CmdImprimir.Focus()
        CmdGuardar.Enabled = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If txtIdAjuste.Text <> "" Then
            comprobante = New ModuloImpresion.ClsAjusteInventario With {
                .usuario = FrmPrincipal.usuarioGlobal,
                .empresa = FrmPrincipal.empresaGlobal,
                .equipo = FrmPrincipal.equipoGlobal,
                .strId = txtIdAjuste.Text,
                .strFecha = txtFecha.Text,
                .strDescripcion = txtDescAjuste.Text
            }
            arrDetalleAjusteInventario = New List(Of ModuloImpresion.ClsDetalleComprobante)
            For I = 0 To dtbDetalleAjusteInventario.Rows.Count - 1
                If dtbDetalleAjusteInventario.Rows(I).Item(7) > 0 Then
                    detalleComprobante = New ModuloImpresion.ClsDetalleComprobante With {
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

    Private Async Sub BtnBusProd_Click(sender As Object, e As EventArgs) Handles btnBusProd.Click
        Dim formBusProd As New FrmBusquedaProducto With {
            .bolIncluyeServicios = False,
            .intIdSucursal = FrmPrincipal.equipoGlobal.IdSucursal
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

    Private Async Sub BtnInsertar_Click(sender As Object, e As EventArgs) Handles btnInsertar.Click
        If producto Is Nothing Then
            If txtCodigo.Text <> "" Then
                producto = Await Puntoventa.ObtenerProductoPorCodigo(FrmPrincipal.empresaGlobal.IdEmpresa, txtCodigo.Text, FrmPrincipal.usuarioGlobal.Token)
                If producto IsNot Nothing Then
                    CargarLineaDetalleAjusteInventario(producto)
                    txtCodigo.Text = ""
                    producto = Nothing
                    txtCodigo.Focus()
                End If
            End If
        Else
            CargarLineaDetalleAjusteInventario(producto)
            txtCantidad.Text = "1"
            txtCodigo.Text = ""
            txtDescripcion.Text = ""
            txtPrecioCosto.Text = ""
            producto = Nothing
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

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".", True)
    End Sub
#End Region
End Class