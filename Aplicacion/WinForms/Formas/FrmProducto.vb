Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.AccesoDatos.ClienteWCF
Imports System.IO
Imports System.Threading.Tasks

Public Class FrmProducto
#Region "Variables"
    Public intIdProducto As Integer
    Private datos As Producto
    Private bolInit As Boolean = True
    Private proveedor As Proveedor
    Private parametroImpuesto As ParametroImpuesto
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If cboTipoProducto.Text = "" Then
            pCampo = "Tipo de producto"
            Return False
        ElseIf cboLinea.Text = "" Then
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

    Private Async Function CargarCombos() As Task
        Try
            cboTipoProducto.ValueMember = "IdTipoProducto"
            cboTipoProducto.DisplayMember = "Descripcion"
            cboTipoProducto.DataSource = Await PuntoventaWCF.ObtenerListaTipoProducto()
            cboTipoImpuesto.ValueMember = "IdImpuesto"
            cboTipoImpuesto.DisplayMember = "Descripcion"
            cboTipoImpuesto.DataSource = Await PuntoventaWCF.ObtenerListaTipoImpuesto()
            cboUnidad.ValueMember = "IdTipoUnidad"
            cboUnidad.DisplayMember = "Descripcion"
            cboUnidad.DataSource = Await PuntoventaWCF.ObtenerListaTipoUnidad()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarComboLinea() As Task
        Try
            If cboTipoProducto.SelectedValue = 1 Then
                cboLinea.DataSource = Await PuntoventaWCF.ObtenerListaLineasDeProducto(FrmPrincipal.empresaGlobal.IdEmpresa)
            Else
                cboLinea.DataSource = Await PuntoventaWCF.ObtenerListaLineasDeServicio(FrmPrincipal.empresaGlobal.IdEmpresa)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Function
        End Try
        cboLinea.ValueMember = "IdLinea"
        cboLinea.DisplayMember = "Descripcion"
    End Function

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

    Private Function FormatoPrecio(strInput As String, intDecimales As Integer) As String
        Dim decPrecio As Decimal = Decimal.Round(Decimal.Parse(strInput), intDecimales)
        Return FormatNumber(decPrecio, intDecimales)
    End Function

    Private Async Function CalcularPrecioConImpuesto(intIdImpuesto As Integer) As Task
        parametroImpuesto = Await PuntoventaWCF.ObtenerParametroImpuesto(intIdImpuesto)
        If txtPrecioVenta1.Text <> "" Then txtPrecioImpuesto1.Text = FormatoPrecio(txtPrecioVenta1.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
        If txtPrecioVenta2.Text <> "" Then txtPrecioImpuesto2.Text = FormatoPrecio(txtPrecioVenta2.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
        If txtPrecioVenta3.Text <> "" Then txtPrecioImpuesto3.Text = FormatoPrecio(txtPrecioVenta3.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
        If txtPrecioVenta4.Text <> "" Then txtPrecioImpuesto4.Text = FormatoPrecio(txtPrecioVenta4.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
        If txtPrecioVenta5.Text <> "" Then txtPrecioImpuesto5.Text = FormatoPrecio(txtPrecioVenta5.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmProducto_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await CargarCombos()
        If intIdProducto > 0 Then
            Try
                datos = Await PuntoventaWCF.ObtenerProducto(intIdProducto)
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
            Await CargarComboLinea()
            cboLinea.SelectedValue = datos.IdLinea
            txtCodigo.Text = datos.Codigo
            proveedor = datos.Proveedor
            txtProveedor.Text = proveedor.Nombre
            txtDescripcion.Text = datos.Descripcion
            txtCantidad.Text = FormatoPrecio(datos.Cantidad, 2)
            txtPrecioCosto.Text = FormatoPrecio(datos.PrecioCosto, 2)
            txtPrecioVenta1.Text = FormatoPrecio(datos.PrecioVenta1, 2)
            txtPrecioVenta2.Text = FormatoPrecio(datos.PrecioVenta2, 2)
            txtPrecioVenta3.Text = FormatoPrecio(datos.PrecioVenta3, 2)
            txtPrecioVenta4.Text = FormatoPrecio(datos.PrecioVenta4, 2)
            txtPrecioVenta5.Text = FormatoPrecio(datos.PrecioVenta5, 2)
            cboTipoImpuesto.SelectedValue = datos.IdImpuesto
            txtIndExistencia.Text = FormatoPrecio(datos.IndExistencia, 2)
            cboUnidad.SelectedValue = datos.IdTipoUnidad
            If datos.Imagen IsNot Nothing Then
                ptbImagen.Image = Bytes_Imagen(datos.Imagen)
            End If
            Await CalcularPrecioConImpuesto(datos.IdImpuesto)
        Else
            datos = New Producto
            Await CargarComboLinea()
            parametroImpuesto = Await PuntoventaWCF.ObtenerParametroImpuesto(1)
            txtCantidad.Text = FormatoPrecio(0, 2)
            txtPrecioCosto.Text = FormatoPrecio(0, 2)
            txtPrecioVenta1.Text = FormatoPrecio(0, 2)
            txtPrecioVenta2.Text = FormatoPrecio(0, 2)
            txtPrecioVenta3.Text = FormatoPrecio(0, 2)
            txtPrecioVenta4.Text = FormatoPrecio(0, 2)
            txtPrecioVenta5.Text = FormatoPrecio(0, 2)
            txtPrecioImpuesto1.Text = FormatoPrecio(0, 2)
            txtPrecioImpuesto2.Text = FormatoPrecio(0, 2)
            txtPrecioImpuesto3.Text = FormatoPrecio(0, 2)
            txtPrecioImpuesto4.Text = FormatoPrecio(0, 2)
            txtPrecioImpuesto5.Text = FormatoPrecio(0, 2)
            txtIndExistencia.Text = FormatoPrecio(0, 2)
        End If
        bolInit = False
    End Sub

    Private Async Sub cboTipoProducto_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoProducto.SelectedValueChanged
        If Not bolInit And cboTipoProducto.SelectedValue IsNot Nothing Then
            Await CargarComboLinea()
        End If
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        If datos.IdProducto = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.IdLinea = cboLinea.SelectedValue
        datos.Codigo = txtCodigo.Text
        datos.IdProveedor = proveedor.IdProveedor
        datos.Descripcion = txtDescripcion.Text
        datos.Cantidad = txtCantidad.Text
        datos.PrecioCosto = txtPrecioCosto.Text
        datos.PrecioVenta1 = txtPrecioVenta1.Text
        datos.PrecioVenta2 = txtPrecioVenta2.Text
        datos.PrecioVenta3 = txtPrecioVenta3.Text
        datos.PrecioVenta4 = txtPrecioVenta4.Text
        datos.PrecioVenta5 = txtPrecioVenta5.Text
        datos.Tipo = cboTipoProducto.SelectedValue
        datos.IdImpuesto = cboTipoImpuesto.SelectedValue
        datos.IndExistencia = txtIndExistencia.Text
        datos.IdTipoUnidad = cboUnidad.SelectedValue
        If ptbImagen.Image IsNot Nothing Then
            datos.Imagen = Imagen_Bytes(ptbImagen.Image)
        Else
            datos.Imagen = Nothing
        End If
        Try
            If datos.IdProducto = 0 Then
                Dim strIdProducto As String = Await PuntoventaWCF.AgregarProducto(datos)
                txtIdProducto.Text = strIdProducto
            Else
                Await PuntoventaWCF.ActualizarProducto(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Async Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                proveedor = Await PuntoventaWCF.ObtenerProveedor(FrmPrincipal.intBusqueda)
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
        txtCantidad.Text = FormatoPrecio(txtCantidad.Text, 2)
    End Sub

    Private Sub PrecioCosto_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioCosto.Validated
        If txtPrecioCosto.Text = "" Then txtPrecioCosto.Text = "0"
        txtPrecioCosto.Text = FormatoPrecio(txtPrecioCosto.Text, 2)
    End Sub

    Private Sub PrecioVenta1_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta1.Validated
        If txtPrecioVenta1.Text = "" Then txtPrecioVenta1.Text = "0"
        txtPrecioVenta1.Text = FormatoPrecio(txtPrecioVenta1.Text, 2)
        txtPrecioImpuesto1.Text = FormatoPrecio(txtPrecioVenta1.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub txtPrecioImpuesto1_Validating(sender As Object, e As EventArgs) Handles txtPrecioImpuesto1.Validated
        If txtPrecioImpuesto1.Text = "" Then txtPrecioImpuesto1.Text = "0"
        txtPrecioImpuesto1.Text = FormatoPrecio(txtPrecioImpuesto1.Text, 2)
        txtPrecioVenta1.Text = FormatoPrecio(txtPrecioImpuesto1.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub PrecioVenta2_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta2.Validated
        If txtPrecioVenta2.Text = "" Then txtPrecioVenta2.Text = "0"
        txtPrecioVenta2.Text = FormatoPrecio(txtPrecioVenta2.Text, 2)
        txtPrecioImpuesto2.Text = FormatoPrecio(txtPrecioVenta2.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub txtPrecioImpuesto2_Validating(sender As Object, e As EventArgs) Handles txtPrecioImpuesto2.Validated
        If txtPrecioImpuesto2.Text = "" Then txtPrecioImpuesto2.Text = "0"
        txtPrecioImpuesto2.Text = FormatoPrecio(txtPrecioImpuesto2.Text, 2)
        txtPrecioVenta2.Text = FormatoPrecio(txtPrecioImpuesto2.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub PrecioVenta3_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta3.Validated
        If txtPrecioVenta3.Text = "" Then txtPrecioVenta3.Text = "0"
        txtPrecioVenta3.Text = FormatoPrecio(txtPrecioVenta3.Text, 2)
        txtPrecioImpuesto3.Text = FormatoPrecio(txtPrecioVenta3.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub txtPrecioImpuesto3_Validating(sender As Object, e As EventArgs) Handles txtPrecioImpuesto3.Validated
        If txtPrecioImpuesto3.Text = "" Then txtPrecioImpuesto3.Text = "0"
        txtPrecioImpuesto3.Text = FormatoPrecio(txtPrecioImpuesto3.Text, 2)
        txtPrecioVenta3.Text = FormatoPrecio(txtPrecioImpuesto3.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub PrecioVenta4_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta4.Validated
        If txtPrecioVenta4.Text = "" Then txtPrecioVenta4.Text = "0"
        txtPrecioVenta4.Text = FormatoPrecio(txtPrecioVenta4.Text, 2)
        txtPrecioImpuesto4.Text = FormatoPrecio(txtPrecioVenta4.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub txtPrecioImpuesto4_Validating(sender As Object, e As EventArgs) Handles txtPrecioImpuesto4.Validated
        If txtPrecioImpuesto4.Text = "" Then txtPrecioImpuesto4.Text = "0"
        txtPrecioImpuesto4.Text = FormatoPrecio(txtPrecioImpuesto4.Text, 2)
        txtPrecioVenta4.Text = FormatoPrecio(txtPrecioImpuesto4.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub PrecioVenta5_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtPrecioVenta5.Validated
        If txtPrecioVenta5.Text = "" Then txtPrecioVenta5.Text = "0"
        txtPrecioVenta5.Text = FormatoPrecio(txtPrecioVenta5.Text, 2)
        txtPrecioImpuesto5.Text = FormatoPrecio(txtPrecioVenta5.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub txtPrecioImpuesto5_Validating(sender As Object, e As EventArgs) Handles txtPrecioImpuesto5.Validated
        If txtPrecioImpuesto5.Text = "" Then txtPrecioImpuesto5.Text = "0"
        txtPrecioImpuesto5.Text = FormatoPrecio(txtPrecioImpuesto5.Text, 2)
        txtPrecioVenta5.Text = FormatoPrecio(txtPrecioImpuesto5.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub IndExistencia_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtIndExistencia.Validated
        If txtIndExistencia.Text = "" Then txtIndExistencia.Text = "0"
        txtIndExistencia.Text = FormatoPrecio(txtIndExistencia.Text, 2)
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtCantidad.KeyPress, txtPrecioCosto.KeyPress, txtPrecioVenta1.KeyPress, txtPrecioVenta2.KeyPress, txtPrecioVenta3.KeyPress, txtPrecioVenta4.KeyPress, txtPrecioVenta5.KeyPress, txtIndExistencia.KeyPress, txtPrecioImpuesto1.KeyPress, txtPrecioImpuesto2.KeyPress, txtPrecioImpuesto3.KeyPress, txtPrecioImpuesto4.KeyPress, txtPrecioImpuesto5.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Async Sub cboTipoImpuesto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoImpuesto.SelectedIndexChanged
        If Not bolInit And cboTipoImpuesto.SelectedValue IsNot Nothing Then
            Await CalcularPrecioConImpuesto(cboTipoImpuesto.SelectedValue)
        End If
    End Sub
#End Region
End Class