Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF

Public Class FrmCargaProductoTransitorio
#Region "Variables"
    Private datos As Producto
    Private parametroImpuesto As ParametroImpuesto
    Private bolReady As Boolean = False
#End Region

#Region "Métodos"
    Private Async Function CargarCombos() As Task
        cboTipoImpuesto.ValueMember = "Id"
        cboTipoImpuesto.DisplayMember = "Descripcion"
        cboTipoImpuesto.DataSource = Await Puntoventa.ObtenerListadoTipoImpuesto(FrmPrincipal.usuarioGlobal.Token)
        cboTipoImpuesto.SelectedValue = 8
    End Function

    Private Function FormatoPrecio(strInput As String, intDecimales As Integer) As String
        Dim decPrecio As Decimal = Decimal.Round(Decimal.Parse(strInput), intDecimales)
        Return FormatNumber(decPrecio, intDecimales)
    End Function

    Private Async Function CalcularPrecioSinImpuesto(intIdImpuesto As Integer) As Task
        parametroImpuesto = Await Puntoventa.ObtenerParametroImpuesto(intIdImpuesto, FrmPrincipal.usuarioGlobal.Token)
        If txtPrecioImpuesto1.Text <> "" Then txtPrecioVenta1.Text = FormatoPrecio(txtPrecioImpuesto1.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmCargaProductoTransitorio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmProducto_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarCombos()
            datos = FrmPrincipal.productoTranstorio
            parametroImpuesto = Await Puntoventa.ObtenerParametroImpuesto(8, FrmPrincipal.usuarioGlobal.Token)
            bolReady = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnCancelar_Click(sender As Object, e As EventArgs) Handles btnCancelar.Click
        FrmPrincipal.productoTranstorio.PrecioVenta1 = 0
        Close()
    End Sub

    Private Sub BtnAgregar_Click(sender As Object, e As EventArgs) Handles btnAgregar.Click
        If txtDescripcion.Text = "" Or CDbl(txtPrecioVenta1.Text) = 0 Or cboTipoImpuesto.SelectedValue Is Nothing Or txtCantidad.Text = "" Then
            MessageBox.Show("Información incompleta. Por favor verifique. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Exit Sub
        End If
        datos.Descripcion = txtDescripcion.Text
        datos.PrecioVenta1 = txtPrecioImpuesto1.Text
        datos.ParametroImpuesto = parametroImpuesto
        datos.Existencias = txtCantidad.Text
        FrmPrincipal.productoTranstorio = datos
        Close()
    End Sub

    Private Sub PrecioVenta1_Validated(sender As Object, e As EventArgs) Handles txtPrecioVenta1.Validated
        If txtPrecioVenta1.Text = "" Then txtPrecioVenta1.Text = "0"
        txtPrecioVenta1.Text = FormatoPrecio(txtPrecioVenta1.Text, 2)
        txtPrecioImpuesto1.Text = FormatoPrecio(txtPrecioVenta1.Text * (1 + (parametroImpuesto.TasaImpuesto / 100)), 2)
    End Sub

    Private Sub txtPrecioImpuesto1_Validated(sender As Object, e As EventArgs) Handles txtPrecioImpuesto1.Validated
        If txtPrecioImpuesto1.Text = "" Then txtPrecioImpuesto1.Text = "0"
        txtPrecioImpuesto1.Text = FormatoPrecio(txtPrecioImpuesto1.Text, 2)
        txtPrecioVenta1.Text = FormatoPrecio(Math.Round(txtPrecioImpuesto1.Text / (1 + (parametroImpuesto.TasaImpuesto / 100)), 3), 2)
    End Sub

    Private Sub SelectionAll_MouseDown(sender As Object, e As MouseEventArgs) Handles txtPrecioVenta1.MouseDown, txtPrecioImpuesto1.MouseDown
        sender.SelectAll()
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtPrecioVenta1.KeyPress, txtPrecioImpuesto1.KeyPress, txtCantidad.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 3, ".")
    End Sub

    Private Async Sub cboTipoImpuesto_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoImpuesto.SelectedIndexChanged
        If bolReady And cboTipoImpuesto.SelectedValue IsNot Nothing Then
            Await CalcularPrecioSinImpuesto(cboTipoImpuesto.SelectedValue)
        End If
    End Sub
#End Region
End Class