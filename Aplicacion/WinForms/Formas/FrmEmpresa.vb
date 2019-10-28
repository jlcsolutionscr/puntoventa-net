Imports System.Threading.Tasks
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF
Imports System.IO

Public Class FrmEmpresa
#Region "Variables"
    Public intIdEmpresa As Integer
    Private datos As Empresa
    Private datosSucursal As SucursalPorEmpresa
    Private datosTerminal As TerminalPorSucursal
    Private bolInit As Boolean = True
    Private bolSucursalActualizada As Boolean = False
    Private bolTerminalActualizada As Boolean = False
    Private bolCertificadoModificado As Boolean = False
    Private strRutaCertificado As String
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtNombreEmpresa.Text = "" Then
            pCampo = "Nombre"
            Return False
        Else
            Return True
        End If
    End Function

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
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarCombos() As Task
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = Await Puntoventa.ObtenerListadoTipoIdentificacion(FrmPrincipal.usuarioGlobal.Token)
        cboProvincia.ValueMember = "Id"
        cboProvincia.DisplayMember = "Descripcion"
        cboProvincia.DataSource = Await Puntoventa.ObtenerListadoProvincias(FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmCliente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarCombos()
            datos = Await Puntoventa.ObtenerEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
            datosSucursal = Await Puntoventa.ObtenerSucursalPorEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.usuarioGlobal.Token)
            datosTerminal = Await Puntoventa.ObtenerTerminalPorSucursal(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, FrmPrincipal.equipoGlobal.IdTerminal, FrmPrincipal.usuarioGlobal.Token)
            If datos Is Nothing Then
                MessageBox.Show("Error al cargar los datos de la empresa.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
            txtTelefono.Text = datos.Telefono
            txtCorreoNotificacion.Text = datos.CorreoNotificacion
            txtFechaRenovacion.Text = Format(datos.FechaVence, "dd/MM/yyyy")
            txtNombreCertificado.Text = datos.NombreCertificado
            txtPinCertificado.Text = datos.PinCertificado
            txtUsuarioATV.Text = datos.UsuarioHacienda
            txtClaveATV.Text = datos.ClaveHacienda
            txtIdSucursal.Text = datosSucursal.IdSucursal
            txtNombreSucursal.Text = datosSucursal.NombreSucursal
            txtDireccionSucursal.Text = datosSucursal.Direccion
            txtTelefonoSucursal.Text = datosSucursal.Telefono
            txtIdTerminal.Text = datosTerminal.IdTerminal
            txtNombreImpresora.Text = datosTerminal.ImpresoraFactura
            txtUltimoFE.Text = datosTerminal.UltimoDocFE
            txtUltimoND.Text = datosTerminal.UltimoDocND
            txtUltimoNC.Text = datosTerminal.UltimoDocNC
            txtUltimoTE.Text = datosTerminal.UltimoDocTE
            txtUltimoMR.Text = datosTerminal.UltimoDocMR
            bolInit = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
            txtTelefono.Text.Length = 0 Or
            txtCorreoNotificacion.Text.Length = 0 Or
            txtNombreSucursal.Text.Length = 0 Or
            txtDireccionSucursal.Text.Length = 0 Or
            txtTelefonoSucursal.Text.Length = 0 Then
            MessageBox.Show("Existen campos requeridos que no fueron ingresados. Por favor verifique la información. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then
            If txtNombreCertificado.Text.Length = 0 Or
                txtPinCertificado.Text.Length = 0 Or
                txtUsuarioATV.Text.Length = 0 Or
                txtClaveATV.Text.Length = 0 Or
                txtCodigoActividad.Text.Length = 0 Then
                MessageBox.Show("Los datos para generar los documentos electrónicos son requeridos. Por favor verifique la información. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            If txtUltimoFE.Text.Length = 0 Or
                txtUltimoND.Text.Length = 0 Or
                txtUltimoNC.Text.Length = 0 Or
                txtUltimoTE.Text.Length = 0 Or
                txtUltimoMR.Text.Length = 0 Then
                MessageBox.Show("La numeración de los últimos documentos electrónicos es requerida. Por favor verifique la información. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
        datos.Telefono = txtTelefono.Text
        datos.CorreoNotificacion = txtCorreoNotificacion.Text
        datos.NombreCertificado = txtNombreCertificado.Text
        datos.PinCertificado = txtPinCertificado.Text
        datos.UsuarioHacienda = txtUsuarioATV.Text
        datos.ClaveHacienda = txtClaveATV.Text
        datos.Barrio = Nothing
        Try
            Await Puntoventa.ActualizarEmpresa(datos, FrmPrincipal.usuarioGlobal.Token)
            If bolCertificadoModificado And txtNombreCertificado.Text.Length > 0 Then
                Dim bytCertificado As Byte() = File.ReadAllBytes(strRutaCertificado)
                Await Puntoventa.ActualizarCertificadoEmpresa(txtIdEmpresa.Text, Convert.ToBase64String(bytCertificado), FrmPrincipal.usuarioGlobal.Token)
            End If
            If bolSucursalActualizada Then
                datosSucursal.NombreSucursal = txtNombreSucursal.Text
                datosSucursal.Direccion = txtDireccionSucursal.Text
                datosSucursal.Telefono = txtTelefonoSucursal.Text
                Await Puntoventa.ActualizarSucursalPorEmpresa(datosSucursal, FrmPrincipal.usuarioGlobal.Token)
            End If
            If bolTerminalActualizada Then
                datosTerminal.ImpresoraFactura = txtNombreImpresora.Text
                datosTerminal.UltimoDocFE = txtUltimoFE.Text
                datosTerminal.UltimoDocND = txtUltimoND.Text
                datosTerminal.UltimoDocNC = txtUltimoNC.Text
                datosTerminal.UltimoDocTE = txtUltimoTE.Text
                datosTerminal.UltimoDocMR = txtUltimoMR.Text
                Await Puntoventa.ActualizarTerminalPorSucursal(datosTerminal, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Async Sub CboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        If Not bolInit Then
            bolInit = True
            cboCanton.DataSource = Await Puntoventa.ObtenerListadoCantones(cboProvincia.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(cboProvincia.SelectedValue, 1, FrmPrincipal.usuarioGlobal.Token)
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, 1, 1, FrmPrincipal.usuarioGlobal.Token)
            bolInit = False
        End If
    End Sub

    Private Async Sub CboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        If Not bolInit Then
            bolInit = True
            cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(cboProvincia.SelectedValue, cboCanton.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, 1, FrmPrincipal.usuarioGlobal.Token)
            bolInit = False
        End If
    End Sub

    Private Async Sub CboDistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrito.SelectedIndexChanged
        If Not bolInit Then
            cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, cboDistrito.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtIdentificacion.KeyPress, txtTelefono.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub TextFieldSucursal_Validated(sender As Object, e As EventArgs) Handles txtNombreSucursal.Validated, txtDireccionSucursal.Validated, txtTelefonoSucursal.Validated
        bolSucursalActualizada = True
    End Sub

    Private Sub TextFieldTerminal_Validated(sender As Object, e As EventArgs) Handles txtNombreImpresora.Validated, txtUltimoFE.Validated, txtUltimoND.Validated, txtUltimoNC.Validated, txtUltimoTE.Validated, txtUltimoMR.Validated
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
                MessageBox.Show("Error al intentar cargar el certificado. Verifique que sea un archivo .p12 válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub
#End Region
End Class