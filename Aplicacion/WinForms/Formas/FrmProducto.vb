Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.Servicios
Imports Unity
Imports System.IO

Public Class FrmProducto
#Region "Variables"
    Public servicioMantenimiento As IMantenimientoService
    Public intIdProducto As Integer
    Private servicioCompras As ICompraService
    Private datos As Producto
    Private bolInit As Boolean = True
    Private proveedor As Proveedor
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If cboTipoProducto.Text = "" Then
            pCampo = "Tipo de producto"
            Return False
        ElseIf CboLinea.Text = "" Then
            pCampo = "Línea del Producto"
            Return False
        ElseIf txtCodigo.Text = "" Then
            pCampo = "Código"
            Return False
        ElseIf txtProveedor.Text = "" Then
            pCampo = "Proveedor"
            Return False
        ElseIf txtDescripcion.Text = "" Then
            pCampo = "Descripción"
            Return False
        ElseIf txtCantidad.Text = "" Then
            pCampo = "Cantidad"
            Return False
        ElseIf txtPrecioCosto.Text = "" Then
            pCampo = "Precio de costo"
            Return False
        ElseIf txtPrecioVenta1.Text = "" Then
            pCampo = "Precio de Venta"
            Return False
        ElseIf txtIndExistencia.Text = "" Then
            pCampo = "Punto de reorden"
            Return False
        ElseIf cboUnidad.Text = "" Then
            pCampo = "Unidad"
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub CargarCombos()
        Try
            cboTipoProducto.ValueMember = "IdTipoProducto"
            cboTipoProducto.DisplayMember = "Descripcion"
            cboTipoProducto.DataSource = servicioMantenimiento.ObtenerTiposProducto()
            cboUnidad.ValueMember = "IdTipoUnidad"
            cboUnidad.DisplayMember = "Descripcion"
            cboUnidad.DataSource = servicioMantenimiento.ObtenerTiposUnidad()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub CargarComboLinea()
        Try
            If cboTipoProducto.SelectedValue = 1 Then
                CboLinea.DataSource = servicioMantenimiento.ObtenerListaLineasDeProducto(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            Else
                CboLinea.DataSource = servicioMantenimiento.ObtenerListaLineasDeServicio(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        CboLinea.ValueMember = "IdLinea"
        CboLinea.DisplayMember = "Descripcion"
    End Sub

    Private Function Bytes_Imagen(ByVal Imagen As Byte()) As Image
        Try
            If Not Imagen Is Nothing Then
                Dim Bin As New MemoryStream(Imagen)
                Dim Resultado As Image = Image.FromStream(Bin)
                Return Resultado
            Else
                Return Nothing
            End If
        Catch ex As Exception
            Return Nothing
        End Try
    End Function

    Private Function Imagen_Bytes(ByVal Imagen As Image) As Byte()
        If Not Imagen Is Nothing Then
            Dim Bin As New MemoryStream
            Imagen.Save(Bin, Imaging.ImageFormat.Jpeg)
            Return Bin.GetBuffer
        Else
            Return Nothing
        End If
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmProducto_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioCompras = FrmMenuPrincipal.unityContainer.Resolve(Of ICompraService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        CargarCombos()
        If intIdProducto > 0 Then
            Try
                datos = servicioMantenimiento.ObtenerProducto(intIdProducto)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El producto seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdProducto.Text = datos.IdProducto
            cboTipoProducto.SelectedValue = datos.Tipo
            CargarComboLinea()
            CboLinea.SelectedValue = datos.IdLinea
            txtCodigo.Text = datos.Codigo
            proveedor = datos.Proveedor
            txtProveedor.Text = proveedor.Nombre
            txtDescripcion.Text = datos.Descripcion
            txtCantidad.Text = FormatNumber(datos.Cantidad, 2)
            txtPrecioCosto.Text = FormatNumber(datos.PrecioCosto, 2)
            txtPrecioVenta1.Text = FormatNumber(datos.PrecioVenta1, 2)
            txtPrecioVenta2.Text = FormatNumber(datos.PrecioVenta2, 2)
            txtPrecioVenta3.Text = FormatNumber(datos.PrecioVenta3, 2)
            txtPrecioVenta4.Text = FormatNumber(datos.PrecioVenta4, 2)
            txtPrecioVenta5.Text = FormatNumber(datos.PrecioVenta5, 2)
            chkExcento.Checked = datos.Excento
            txtIndExistencia.Text = FormatNumber(datos.IndExistencia, 2)
            cboUnidad.SelectedValue = datos.IdTipoUnidad
            If datos.Imagen IsNot Nothing Then
                ptbImagen.Image = Bytes_Imagen(datos.Imagen)
            End If
        Else
            datos = New Producto
            CargarComboLinea()
            txtCantidad.Text = FormatNumber(0, 2)
            txtPrecioCosto.Text = FormatNumber(0, 2)
            txtPrecioVenta1.Text = FormatNumber(0, 2)
            txtPrecioVenta2.Text = FormatNumber(0, 2)
            txtPrecioVenta3.Text = FormatNumber(0, 2)
            txtPrecioVenta4.Text = FormatNumber(0, 2)
            txtPrecioVenta5.Text = FormatNumber(0, 2)
            txtIndExistencia.Text = FormatNumber(0, 2)
        End If
        bolInit = False
    End Sub

    Private Sub cboTipoProducto_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoProducto.SelectedValueChanged
        If Not bolInit And cboTipoProducto.SelectedValue IsNot Nothing Then
            CargarComboLinea()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If datos.IdProducto = 0 Then
            datos.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.IdLinea = CboLinea.SelectedValue
        datos.Codigo = txtCodigo.Text
        datos.IdProveedor = proveedor.IdProveedor
        datos.Descripcion = txtDescripcion.Text
        datos.Cantidad = txtCantidad.Text
        datos.PrecioCosto = txtPrecioCosto.Text
        datos.PrecioVenta1 = txtPrecioVenta1.Text
        datos.PrecioVenta2 = IIf(txtPrecioVenta2.Text = "", 0, txtPrecioVenta2.Text)
        datos.PrecioVenta3 = IIf(txtPrecioVenta3.Text = "", 0, txtPrecioVenta3.Text)
        datos.PrecioVenta4 = IIf(txtPrecioVenta4.Text = "", 0, txtPrecioVenta4.Text)
        datos.PrecioVenta5 = IIf(txtPrecioVenta5.Text = "", 0, txtPrecioVenta5.Text)
        datos.Tipo = cboTipoProducto.SelectedValue
        datos.Excento = chkExcento.Checked
        datos.IndExistencia = txtIndExistencia.Text
        datos.IdTipoUnidad = cboUnidad.SelectedValue
        If ptbImagen.Image IsNot Nothing Then
            datos.Imagen = Imagen_Bytes(ptbImagen.Image)
        Else
            datos.Imagen = Nothing
        End If
        Try
            If datos.IdProducto = 0 Then
                servicioMantenimiento.AgregarProducto(datos)
            Else
                servicioMantenimiento.ActualizarProducto(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                proveedor = servicioCompras.ObtenerProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtProveedor.Text = proveedor.Nombre
        End If
    End Sub

    Private Sub CmdImprimir_Click(ByVal sender As Object, ByVal e As EventArgs)
        'Dim reptEtiquetaProducto As New rptEtiquetaProducto()
        'Dim formReport As New frmRptViewer()
        'If txtIdProducto.Text <> "" Then
        '    dtDatos = FrmMenuPrincipal.objGenericoCN.ObtenerTabla("SELECT concat('*',Codigo,'*') As Codigo, PrecioVenta, Descripcion FROM Producto WHERE IdProducto = " & txtIdProducto.Text, "EtiquetaProducto")
        '    reptEtiquetaProducto.SetDataSource(dtDatos)
        '    formReport.crtViewer.ReportSource = reptEtiquetaProducto
        '    formReport.ShowDialog()
        'End If
    End Sub

    Private Sub btnCargarImagen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCargarImagen.Click
        If ofdAbrirImagen.ShowDialog() = DialogResult.OK Then
            Try
                ptbImagen.Image = Image.FromFile(ofdAbrirImagen.FileName)
            Catch ex As Exception
                MessageBox.Show("No se logró cargar el archivo seleccionado. Por favor intente de nuevo.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub

    Private Sub btnEliminarImagen_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarImagen.Click
        ptbImagen.Image = Nothing
    End Sub

    Private Sub Cantidad_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtCantidad.Validated
        If txtCantidad.Text = "" Then txtCantidad.Text = "0"
        txtCantidad.Text = FormatNumber(txtCantidad.Text, 2)
    End Sub

    Private Sub PrecioCosto_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioCosto.Validated
        If txtPrecioCosto.Text = "" Then txtPrecioCosto.Text = "0"
        txtPrecioCosto.Text = FormatNumber(txtPrecioCosto.Text, 2)
    End Sub

    Private Sub PrecioVenta1_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta1.Validated
        If txtPrecioVenta1.Text = "" Then txtPrecioVenta1.Text = "0"
        txtPrecioVenta1.Text = FormatNumber(txtPrecioVenta1.Text, 2)
        If txtPrecioVenta2.Text = "" Then
            txtPrecioVenta2.Text = txtPrecioVenta1.Text
        End If
    End Sub

    Private Sub PrecioVenta2_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta2.Validated
        If txtPrecioVenta2.Text = "" Then txtPrecioVenta2.Text = "0"
        txtPrecioVenta2.Text = FormatNumber(txtPrecioVenta2.Text, 2)
    End Sub

    Private Sub PrecioVenta3_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta3.Validated
        If txtPrecioVenta3.Text = "" Then txtPrecioVenta3.Text = "0"
        txtPrecioVenta3.Text = FormatNumber(txtPrecioVenta3.Text, 2)
    End Sub

    Private Sub PrecioVenta4_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta4.Validated
        If txtPrecioVenta4.Text = "" Then txtPrecioVenta4.Text = "0"
        txtPrecioVenta4.Text = FormatNumber(txtPrecioVenta4.Text, 2)
    End Sub

    Private Sub PrecioVenta5_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta5.Validated
        If txtPrecioVenta5.Text = "" Then txtPrecioVenta5.Text = "0"
        txtPrecioVenta5.Text = FormatNumber(txtPrecioVenta5.Text, 2)
    End Sub

    Private Sub IndExistencia_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtIndExistencia.Validated
        If txtIndExistencia.Text = "" Then txtIndExistencia.Text = "0"
        txtIndExistencia.Text = FormatNumber(txtIndExistencia.Text, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecioCosto.KeyPress, txtPrecioVenta1.KeyPress, txtPrecioVenta2.KeyPress, txtPrecioVenta3.KeyPress, txtPrecioVenta4.KeyPress, txtPrecioVenta5.KeyPress, txtIndExistencia.KeyPress
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub
#End Region
End Class