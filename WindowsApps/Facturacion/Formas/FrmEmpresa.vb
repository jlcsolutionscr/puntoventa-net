Imports System.Threading.Tasks
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.IO
Imports LeandroSoftware.ClienteWCF
Imports System.Collections.Generic

Public Class FrmEmpresa
#Region "Variables"
    Public intIdEmpresa As Integer
    Private datos As Empresa
    Private credenciales As CredencialesHacienda
    Private datosSucursal As SucursalPorEmpresa
    Private datosTerminal As TerminalPorSucursal
    Private bolReady As Boolean = False
    Private bolSucursalActualizada As Boolean = False
    Private bolTerminalActualizada As Boolean = False
    Private bolCertificadoModificado As Boolean = False
    Private bolLogoModificado As Boolean = False
    Private strRutaCertificado As String
#End Region

#Region "Métodos"
    Private Async Function CargarListadoBarrios(IdProvincia As Integer, IdCanton As Integer, IdDistrito As Integer) As Task
        Try
            cboCanton.ValueMember = "Id"
            cboCanton.DisplayMember = "Descripcion"
            cboCanton.DataSource = Await Puntoventa.ObtenerListadoCantones(IdProvincia, FrmPrincipal.usuarioGlobal.Token)
            cboDistrito.ValueMember = "Id"
            cboDistrito.DisplayMember = "Descripcion"
            cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(IdProvincia, IdCanton, FrmPrincipal.usuarioGlobal.Token)
            cboBarrio.ValueMember = "Id"
            cboBarrio.DisplayMember = "Descripcion"
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(IdProvincia, IdCanton, IdDistrito, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarCombos() As Task
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = Await Puntoventa.ObtenerListadoTipoIdentificacion(FrmPrincipal.usuarioGlobal.Token)
        cboProvincia.ValueMember = "Id"
        cboProvincia.DisplayMember = "Descripcion"
        cboProvincia.DataSource = Await Puntoventa.ObtenerListadoProvincias(FrmPrincipal.usuarioGlobal.Token)

        Dim comboSource As New Dictionary(Of Integer, String)()
        comboSource.Add(80, "80MM THERMAL RECEIPT PRINTER")
        comboSource.Add(52, "58MM THERMAL RECEIPT PRINTER")
        cboTipoImpresora.DataSource = New BindingSource(comboSource, Nothing)
        cboTipoImpresora.ValueMember = "Key"
        cboTipoImpresora.DisplayMember = "Value"
    End Function
#End Region

#Region "Eventos Controles"
    Private Sub FrmEmpresa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub FrmCliente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarCombos()
            datos = Await Puntoventa.ObtenerEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
            Dim logotipo As Byte() = Await Puntoventa.ObtenerLogotipoEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
            credenciales = Await Puntoventa.ObtenerCredencialesHacienda(FrmPrincipal.empresaGlobal.Identificacion, FrmPrincipal.usuarioGlobal.Token)
            datosSucursal = Await Puntoventa.ObtenerSucursalPorEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
            datosTerminal = Await Puntoventa.ObtenerTerminalPorSucursal(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.equipoGlobal.IdTerminal, FrmPrincipal.usuarioGlobal.Token)
            If datos Is Nothing Then
                MessageBox.Show("Error al cargar los datos de la empresa.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            Await CargarListadoBarrios(datos.IdProvincia, datos.IdCanton, datos.IdDistrito)
            txtIdEmpresa.Text = datos.IdEmpresa
            txtNombreEmpresa.Text = datos.NombreEmpresa
            txtNombreComercial.Text = datos.NombreComercial
            cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
            txtIdentificacion.Text = datos.Identificacion
            txtCodigoActividad.Text = datos.CodigoActividad
            cboProvincia.SelectedValue = datos.IdProvincia
            cboCanton.SelectedValue = datos.IdCanton
            cboDistrito.SelectedValue = datos.IdDistrito
            cboBarrio.SelectedValue = datos.IdBarrio
            txtDireccion.Text = datos.Direccion
            txtTelefono1.Text = datos.Telefono1
            txtTelefono2.Text = datos.Telefono2
            txtCorreoNotificacion.Text = datos.CorreoNotificacion
            txtLeyendaFactura.Text = datos.LeyendaFactura
            txtLeyendaOrdenServicio.Text = datos.LeyendaOrdenServicio
            txtLeyendaApartado.Text = datos.LeyendaApartado
            txtLeyendaProforma.Text = datos.LeyendaProforma
            txtFechaRenovacion.Text = Format(datos.FechaVence, "dd/MM/yyyy")
            chkPrecioVentaIncluyeIVA.Checked = datos.PrecioVentaIncluyeIVA
            txtMontoRedondeoDescuento.Text = datos.MontoRedondeoDescuento
            txtNombreCertificado.Text = datos.NombreCertificado
            txtPinCertificado.Text = datos.PinCertificado
            txtUsuarioATV.Text = datos.UsuarioHacienda
            txtClaveATV.Text = datos.ClaveHacienda
            txtIdSucursal.Text = datosSucursal.IdSucursal
            txtNombreSucursal.Text = datosSucursal.NombreSucursal
            txtDireccionSucursal.Text = datosSucursal.Direccion
            txtTelefonoSucursal.Text = datosSucursal.Telefono
            txtConsecFactura.Text = datosSucursal.ConsecFactura
            txtConsecProforma.Text = datosSucursal.ConsecProforma
            txtConsecOrdenServicio.Text = datosSucursal.ConsecOrdenServicio
            txtConsecApartado.Text = datosSucursal.ConsecApartado
            txtIdTerminal.Text = datosTerminal.IdTerminal
            txtNombreImpresora.Text = datosTerminal.ImpresoraFactura
            cboTipoImpresora.SelectedValue = datosTerminal.AnchoLinea
            txtUltimoFE.Text = datosTerminal.UltimoDocFE
            txtUltimoND.Text = datosTerminal.UltimoDocND
            txtUltimoNC.Text = datosTerminal.UltimoDocNC
            txtUltimoTE.Text = datosTerminal.UltimoDocTE
            txtUltimoMR.Text = datosTerminal.UltimoDocMR
            txtUltimoFEC.Text = datosTerminal.UltimoDocFEC
            chkCierre.Checked = datosSucursal.CierreEnEjecucion
            If logotipo IsNot Nothing Then
                Dim logoImage As Image
                Using ms As New MemoryStream(logotipo)
                    logoImage = Image.FromStream(ms)
                End Using
                picLogo.Image = logoImage
            End If
            bolReady = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        If txtNombreEmpresa.Text.Length = 0 Or
            cboTipoIdentificacion.SelectedValue Is Nothing Or
            txtIdentificacion.Text.Length = 0 Or
            cboProvincia.SelectedValue Is Nothing Or
            cboCanton.SelectedValue Is Nothing Or
            cboDistrito.SelectedValue Is Nothing Or
            cboBarrio.SelectedValue Is Nothing Or
            txtDireccion.Text.Length = 0 Or
            txtTelefono1.Text.Length = 0 Or
            txtCorreoNotificacion.Text.Length = 0 Or
            txtNombreSucursal.Text.Length = 0 Or
            txtDireccionSucursal.Text.Length = 0 Or
            txtTelefonoSucursal.Text.Length = 0 Or
            txtConsecFactura.Text.Length = 0 Or
            txtConsecProforma.Text.Length = 0 Or
            txtConsecOrdenServicio.Text.Length = 0 Or
            txtConsecApartado.Text.Length = 0 Then
            MessageBox.Show("Existen campos requeridos que no fueron ingresados. Por favor verifique la información. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then
            If txtNombreCertificado.Text.Length = 0 Or
                txtPinCertificado.Text.Length = 0 Or
                txtUsuarioATV.Text.Length = 0 Or
                txtClaveATV.Text.Length = 0 Or
                txtCodigoActividad.Text.Length = 0 Then
                MessageBox.Show("Los datos para generar los documentos electrónicos son requeridos. Por favor verifique la información. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If txtUltimoFE.Text.Length = 0 Or
                txtUltimoND.Text.Length = 0 Or
                txtUltimoNC.Text.Length = 0 Or
                txtUltimoTE.Text.Length = 0 Or
                txtUltimoMR.Text.Length = 0 Then
                MessageBox.Show("La numeración de los últimos documentos electrónicos es requerida. Por favor verifique la información. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Dim usuarioValido As Boolean = Await Puntoventa.ValidarUsuarioHacienda(txtUsuarioATV.Text, txtClaveATV.Text, FrmPrincipal.usuarioGlobal.Token)
            If usuarioValido = False Then
                MessageBox.Show("Los credenciales para validar el usuario en el ATV son incorrectos. Por favor verifique la información. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        datos.NombreEmpresa = txtNombreEmpresa.Text
        datos.NombreComercial = txtNombreComercial.Text
        datos.IdTipoIdentificacion = cboTipoIdentificacion.SelectedValue
        datos.Identificacion = txtIdentificacion.Text
        datos.CodigoActividad = txtCodigoActividad.Text
        datos.IdProvincia = cboProvincia.SelectedValue
        datos.IdCanton = cboCanton.SelectedValue
        datos.IdDistrito = cboDistrito.SelectedValue
        datos.IdBarrio = cboBarrio.SelectedValue
        datos.Direccion = txtDireccion.Text
        datos.Telefono1 = txtTelefono1.Text
        datos.Telefono2 = txtTelefono2.Text
        datos.CorreoNotificacion = txtCorreoNotificacion.Text
        datos.LeyendaFactura = txtLeyendaFactura.Text
        datos.LeyendaOrdenServicio = txtLeyendaOrdenServicio.Text
        datos.LeyendaApartado = txtLeyendaApartado.Text
        datos.LeyendaProforma = txtLeyendaProforma.Text
        datos.NombreCertificado = txtNombreCertificado.Text
        datos.PinCertificado = txtPinCertificado.Text
        datos.UsuarioHacienda = txtUsuarioATV.Text
        datos.ClaveHacienda = txtClaveATV.Text
        datos.PrecioVentaIncluyeIVA = chkPrecioVentaIncluyeIVA.Checked
        datos.MontoRedondeoDescuento = txtMontoRedondeoDescuento.Text
        datos.Barrio = Nothing
        Try
            btnCancelar.Focus()
            btnGuardar.Enabled = False
            Await Puntoventa.ActualizarEmpresa(datos, FrmPrincipal.usuarioGlobal.Token)
            FrmPrincipal.empresaGlobal.PrecioVentaIncluyeIVA = datos.PrecioVentaIncluyeIVA
            FrmPrincipal.empresaGlobal.MontoRedondeoDescuento = datos.MontoRedondeoDescuento
            If bolCertificadoModificado And txtNombreCertificado.Text.Length > 0 Then
                Dim bytCertificado As Byte() = File.ReadAllBytes(strRutaCertificado)
                Await Puntoventa.ActualizarCredencialesHacienda(txtIdentificacion.Text, txtUsuarioATV.Text, txtClaveATV.Text, txtNombreCertificado.Text, txtPinCertificado.Text, Convert.ToBase64String(bytCertificado), FrmPrincipal.usuarioGlobal.Token)
            End If
            If bolLogoModificado Then
                If picLogo.Image IsNot Nothing Then
                    Dim bytLogotipo As Byte()
                    Using stream As MemoryStream = New MemoryStream()
                        picLogo.Image.Save(stream, Imaging.ImageFormat.Png)
                        bytLogotipo = stream.ToArray()
                    End Using
                    Await Puntoventa.ActualizarLogoEmpresa(txtIdEmpresa.Text, Convert.ToBase64String(bytLogotipo), FrmPrincipal.usuarioGlobal.Token)
                Else
                    Await Puntoventa.RemoverLogoEmpresa(txtIdEmpresa.Text, FrmPrincipal.usuarioGlobal.Token)
                End If
            End If
            If bolSucursalActualizada Then
                datosSucursal.NombreSucursal = txtNombreSucursal.Text
                datosSucursal.Direccion = txtDireccionSucursal.Text
                datosSucursal.Telefono = txtTelefonoSucursal.Text
                datosSucursal.ConsecFactura = txtConsecFactura.Text
                datosSucursal.ConsecProforma = txtConsecProforma.Text
                datosSucursal.ConsecOrdenServicio = txtConsecOrdenServicio.Text
                datosSucursal.ConsecApartado = txtConsecApartado.Text
                datosSucursal.CierreEnEjecucion = chkCierre.Checked
                Await Puntoventa.ActualizarSucursalPorEmpresa(datosSucursal, FrmPrincipal.usuarioGlobal.Token)
                FrmPrincipal.equipoGlobal.NombreSucursal = txtNombreSucursal.Text
                FrmPrincipal.equipoGlobal.DireccionSucursal = txtDireccionSucursal.Text
                FrmPrincipal.equipoGlobal.TelefonoSucursal = txtTelefonoSucursal.Text
            End If
            If bolTerminalActualizada Then
                datosTerminal.ImpresoraFactura = txtNombreImpresora.Text
                datosTerminal.AnchoLinea = DirectCast(cboTipoImpresora.SelectedItem, KeyValuePair(Of Integer, String)).Key
                datosTerminal.UltimoDocFE = txtUltimoFE.Text
                datosTerminal.UltimoDocND = txtUltimoND.Text
                datosTerminal.UltimoDocNC = txtUltimoNC.Text
                datosTerminal.UltimoDocTE = txtUltimoTE.Text
                datosTerminal.UltimoDocMR = txtUltimoMR.Text
                datosTerminal.UltimoDocFEC = txtUltimoFEC.Text
                Await Puntoventa.ActualizarTerminalPorSucursal(datosTerminal, FrmPrincipal.usuarioGlobal.Token)
                FrmPrincipal.equipoGlobal.ImpresoraFactura = txtNombreImpresora.Text
                FrmPrincipal.equipoGlobal.AnchoLinea = datosTerminal.AnchoLinea
            End If
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Async Sub CboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        If bolReady Then
            bolReady = False
            cboCanton.DataSource = Await Puntoventa.ObtenerListadoCantones(cboProvincia.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(cboProvincia.SelectedValue, 1, FrmPrincipal.usuarioGlobal.Token)
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, 1, 1, FrmPrincipal.usuarioGlobal.Token)
            bolReady = True
        End If
    End Sub

    Private Async Sub CboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        If bolReady Then
            bolReady = False
            cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(cboProvincia.SelectedValue, cboCanton.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, 1, FrmPrincipal.usuarioGlobal.Token)
            bolReady = True
        End If
    End Sub

    Private Async Sub CboDistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrito.SelectedIndexChanged
        If bolReady Then
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, cboDistrito.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
        End If
    End Sub

    Private Sub ValidaDigitosSinDecimal(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtIdentificacion.KeyPress, txtTelefono1.KeyPress, txtTelefono2.KeyPress, txtMontoRedondeoDescuento.KeyPress, txtConsecFactura.KeyPress, txtConsecProforma.KeyPress, txtConsecOrdenServicio.KeyPress, txtConsecApartado.KeyPress, txtUltimoFE.KeyPress, txtUltimoND.KeyPress, txtUltimoNC.KeyPress, txtUltimoTE.KeyPress, txtUltimoMR.KeyPress, txtUltimoFEC.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 0, ".")
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub TextFieldSucursal_Validated(sender As Object, e As EventArgs) Handles txtNombreSucursal.Validated, txtDireccionSucursal.Validated, txtTelefonoSucursal.Validated
        bolSucursalActualizada = True
    End Sub

    Private Sub chkCierre_CheckedChanged(sender As Object, e As EventArgs) Handles chkCierre.CheckedChanged
        bolSucursalActualizada = True
    End Sub

    Private Sub TextFieldTerminal_Validated(sender As Object, e As EventArgs) Handles txtNombreImpresora.Validated, txtUltimoFE.Validated, txtUltimoND.Validated, txtUltimoNC.Validated, txtUltimoTE.Validated, txtUltimoMR.Validated, txtUltimoFEC.Validated
        bolTerminalActualizada = True
    End Sub

    Private Sub BtnCargarCertificado_Click(sender As Object, e As EventArgs) Handles btnCargarCertificado.Click
        ofdAbrirDocumento.DefaultExt = "p12"
        ofdAbrirDocumento.Filter = "Certificate Files|*.p12;"
        Dim result As DialogResult = ofdAbrirDocumento.ShowDialog()
        If result = DialogResult.OK Then
            Try
                strRutaCertificado = ofdAbrirDocumento.FileName
                txtNombreCertificado.Text = Path.GetFileName(strRutaCertificado)
                bolCertificadoModificado = True
            Catch ex As Exception
                MessageBox.Show("Error al intentar cargar el certificado. Verifique que sea un archivo .p12 válido. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnCargarLogo_Click(sender As Object, e As EventArgs) Handles btnCargarLogo.Click
        ofdAbrirDocumento.DefaultExt = "png"
        ofdAbrirDocumento.Filter = "PNG Image Files|*.png;"
        Dim result As DialogResult = ofdAbrirDocumento.ShowDialog()
        If result = DialogResult.OK Then
            Try
                picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName)
                bolLogoModificado = True
            Catch ex As Exception
                MessageBox.Show("Error al intentar cargar el certificado. Verifique que sea un archivo .PNG válido. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub btnLimpiarLogo_Click(sender As Object, e As EventArgs) Handles btnLimpiarLogo.Click
        Dim dialogResult As DialogResult = MessageBox.Show("Desea eliminar el logotipo de la empresa?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
        If dialogResult = dialogResult.Yes Then
            picLogo.Image = Nothing
            bolLogoModificado = True
        End If
    End Sub

    Private Sub cboTipoImpresora_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboTipoImpresora.SelectedIndexChanged
        bolTerminalActualizada = True
    End Sub

    Private Sub txtMontoRedondeoDescuento_Validated(sender As Object, e As EventArgs) Handles txtMontoRedondeoDescuento.Validated
        If txtMontoRedondeoDescuento.Text > 100 Then
            MessageBox.Show("El monto de redondeo para los descuentos no puede ser mayor a 100", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Warning)
            txtMontoRedondeoDescuento.Text = "100"
        End If
    End Sub
#End Region
End Class