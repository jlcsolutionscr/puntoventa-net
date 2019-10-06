Imports System.Threading.Tasks
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.Core.ClienteWCF
Imports System.Collections.Generic
Imports LeandroSoftware.Core.Utilities
Imports System.IO

Public Class FrmEmpresa
#Region "Variables"
    Public intIdEmpresa As Integer
    Private datos As Empresa
    Private datosSucursal As List(Of TerminalPorEmpresa)
    Private bolInit As Boolean = True
    Private bolCertificadoModificado As Boolean = False
    Private intIdTerminalEnUso As Integer = 0
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
            cboCanton.ValueMember = "IdCanton"
            cboCanton.DisplayMember = "Descripcion"
            cboCanton.DataSource = Await ClienteFEWCF.ObtenerListaCantones(IdProvincia)
            cboDistrito.ValueMember = "IdDistrito"
            cboDistrito.DisplayMember = "Descripcion"
            cboDistrito.DataSource = Await ClienteFEWCF.ObtenerListaDistritos(IdProvincia, IdCanton)
            cboBarrio.ValueMember = "IdBarrio"
            cboBarrio.DisplayMember = "Descripcion"
            cboBarrio.DataSource = Await ClienteFEWCF.ObtenerListaBarrios(IdProvincia, IdCanton, IdDistrito)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function

    Private Async Function CargarCombos() As Task
        Try
            cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion"
            cboTipoIdentificacion.DisplayMember = "Descripcion"
            cboTipoIdentificacion.DataSource = Await ClienteFEWCF.ObtenerListaTipoIdentificacion()
            cboProvincia.ValueMember = "IdProvincia"
            cboProvincia.DisplayMember = "Descripcion"
            cboProvincia.DataSource = Await ClienteFEWCF.ObtenerListaProvincias()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmCliente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Await CargarCombos()
        Try
            datos = Await ClienteFEWCF.ObtenerEmpresa(FrmPrincipal.empresaGlobal.IdEmpresa)
            datosSucursal = datos.TerminalPorEmpresa
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        If datos Is Nothing Then
            MessageBox.Show("Error al cargar los datos de la empresa.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End If
        Await CargarListadoBarrios(datos.IdProvincia, datos.IdCanton, datos.IdDistrito)
        txtIdEmpresa.Text = datos.IdEmpresa
        cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
        txtIdentificacion.Text = datos.Identificacion
        txtCodigoActividad.Text = datos.CodigoActividad
        cboProvincia.SelectedValue = datos.IdProvincia
        cboCanton.SelectedValue = datos.IdCanton
        cboDistrito.SelectedValue = datos.IdDistrito
        cboBarrio.SelectedValue = datos.IdBarrio
        txtDireccion.Text = datos.Direccion
        txtNombreEmpresa.Text = datos.NombreEmpresa
        txtNombreComercial.Text = datos.NombreComercial
        txtTelefono.Text = datos.Telefono
        txtCorreoNotificacion.Text = datos.CorreoNotificacion
        txtFechaRenovacion.Text = Format(datos.FechaVence, "dd/MM/yyyy")
        txtIdSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).IdSucursal
        txtIdTerminal.Text = datosSucursal.Item(intIdTerminalEnUso).IdTerminal
        txtNombreSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).NombreSucursal
        txtDireccionSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).Direccion
        txtTelefonoSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).Telefono
        txtImpresora.Text = datosSucursal.Item(intIdTerminalEnUso).ImpresoraFactura
        If datosSucursal.Count > 1 Then btnSiguiente.Enabled = True
        bolInit = False
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        If cboTipoIdentificacion.SelectedValue Is Nothing Or txtIdentificacion.Text.Length = 0 Or cboProvincia.SelectedValue Is Nothing Or cboCanton.SelectedValue Is Nothing Or cboDistrito.SelectedValue Is Nothing Or cboBarrio.SelectedValue Is Nothing Or txtDireccion.Text.Length = 0 Or txtNombreEmpresa.Text.Length = 0 Or txtCorreoNotificacion.Text.Length = 0 Then
            MessageBox.Show("Existen campos requeridos que no se fueron ingresados. Por favor verifique la información. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        datos.IdTipoIdentificacion = cboTipoIdentificacion.SelectedValue
        datos.Identificacion = txtIdentificacion.Text
        datos.CodigoActividad = txtCodigoActividad.Text
        datos.IdProvincia = cboProvincia.SelectedValue
        datos.IdCanton = cboCanton.SelectedValue
        datos.IdDistrito = cboDistrito.SelectedValue
        datos.IdBarrio = cboBarrio.SelectedValue
        datos.Direccion = txtDireccion.Text
        datos.NombreEmpresa = txtNombreEmpresa.Text
        datos.NombreComercial = txtNombreComercial.Text
        datos.Telefono = txtTelefono.Text
        datos.CorreoNotificacion = txtCorreoNotificacion.Text
        datos.Barrio = Nothing
        Try
            Await ClienteFEWCF.ActualizarEmpresa(datos)
            For Each terminal As TerminalPorEmpresa In datosSucursal
                Await ClienteFEWCF.ActualizarTerminalPorEmpresa(terminal)
            Next
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
            cboCanton.DataSource = Await ClienteFEWCF.ObtenerListaCantones(cboProvincia.SelectedValue)
            cboDistrito.DataSource = Await ClienteFEWCF.ObtenerListaDistritos(cboProvincia.SelectedValue, 1)
            cboBarrio.DataSource = Await ClienteFEWCF.ObtenerListaBarrios(cboProvincia.SelectedValue, 1, 1)
            bolInit = False
        End If
    End Sub

    Private Async Sub CboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        If Not bolInit Then
            bolInit = True
            cboDistrito.DataSource = Await ClienteFEWCF.ObtenerListaDistritos(cboProvincia.SelectedValue, cboCanton.SelectedValue)
            cboBarrio.DataSource = Await ClienteFEWCF.ObtenerListaBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, 1)
            bolInit = False
        End If
    End Sub

    Private Async Sub CboDistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrito.SelectedIndexChanged
        If Not bolInit Then
            cboBarrio.DataSource = Await ClienteFEWCF.ObtenerListaBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, cboDistrito.SelectedValue)
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs) Handles txtIdentificacion.KeyPress, txtTelefono.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub BtnAnterior_Click(sender As Object, e As EventArgs) Handles btnAnterior.Click
        intIdTerminalEnUso -= 1
        If intIdTerminalEnUso = 0 Then btnAnterior.Enabled = False
        If datosSucursal.Count > intIdTerminalEnUso + 1 Then
            btnSiguiente.Enabled = True
        Else
            btnSiguiente.Enabled = False
        End If
        txtIdSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).IdSucursal
        txtIdTerminal.Text = datosSucursal.Item(intIdTerminalEnUso).IdTerminal
        txtNombreSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).NombreSucursal
        txtDireccionSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).Direccion
        txtTelefonoSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).Telefono
        txtImpresora.Text = datosSucursal.Item(intIdTerminalEnUso).ImpresoraFactura
    End Sub

    Private Sub BtnSiguiente_Click(sender As Object, e As EventArgs) Handles btnSiguiente.Click
        intIdTerminalEnUso += 1
        If intIdTerminalEnUso >= 1 Then btnAnterior.Enabled = True
        If datosSucursal.Count > intIdTerminalEnUso + 1 Then
            btnSiguiente.Enabled = True
        Else
            btnSiguiente.Enabled = False
        End If
        txtIdSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).IdSucursal
        txtIdTerminal.Text = datosSucursal.Item(intIdTerminalEnUso).IdTerminal
        txtNombreSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).NombreSucursal
        txtDireccionSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).Direccion
        txtTelefonoSucursal.Text = datosSucursal.Item(intIdTerminalEnUso).Telefono
        txtImpresora.Text = datosSucursal.Item(intIdTerminalEnUso).ImpresoraFactura
    End Sub

    Private Sub TxtNombreSucursal_Validated(sender As Object, e As EventArgs) Handles txtNombreSucursal.Validated
        datosSucursal.Item(intIdTerminalEnUso).NombreSucursal = txtNombreSucursal.Text
    End Sub

    Private Sub TxtDireccionSucursal_Validated(sender As Object, e As EventArgs) Handles txtDireccionSucursal.Validated
        datosSucursal.Item(intIdTerminalEnUso).Direccion = txtDireccionSucursal.Text
    End Sub

    Private Sub TxtTelefonoSucursal_Validated(sender As Object, e As EventArgs) Handles txtTelefonoSucursal.Validated
        datosSucursal.Item(intIdTerminalEnUso).Telefono = txtTelefonoSucursal.Text
    End Sub

    Private Sub TxtImpresora_Validated(sender As Object, e As EventArgs)
        datosSucursal.Item(intIdTerminalEnUso).ImpresoraFactura = txtImpresora.Text
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