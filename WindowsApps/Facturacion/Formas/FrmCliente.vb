Imports System.Collections.Generic
Imports System.Globalization
Imports System.Text.RegularExpressions
Imports System.Threading.Tasks
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.Constantes
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades

Public Class FrmCliente
#Region "Variables"
    Public intIdCliente As Integer
    Private datos As Cliente
    Private provider As CultureInfo = CultureInfo.InvariantCulture
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = FrmPrincipal.ObtenerListadoTipoIdentificacion()
        cboIdTipoPrecio.ValueMember = "Id"
        cboIdTipoPrecio.DisplayMember = "Descripcion"
        cboIdTipoPrecio.DataSource = FrmPrincipal.ObtenerListadoTipoPrecio()
        cboTipoExoneracion.ValueMember = "Id"
        cboTipoExoneracion.DisplayMember = "Descripcion"
        cboTipoExoneracion.DataSource = FrmPrincipal.ObtenerListadoTipoExoneracion()
        cboInstExoneracion.ValueMember = "Id"
        cboInstExoneracion.DisplayMember = "Descripcion"
        cboInstExoneracion.DataSource = FrmPrincipal.ObtenerListadoNombreInstExoneracion()
        cboActividadEconomica.ValueMember = "Id"
        cboActividadEconomica.DisplayMember = "Descripcion"
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCliente_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
            If FrmPrincipal.bolModificaCliente Then chkPermiteCredito.Enabled = True
            CargarCombos()
            If intIdCliente > 0 Then
                datos = Await Puntoventa.ObtenerCliente(intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    Throw New Exception("El cliente seleccionado no existe")
                End If
                Dim contribuyente As ContribuyenteHacienda = Await Puntoventa.ObtenerInformacionContribuyente(datos.Identificacion)
                If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then cboActividadEconomica.DataSource = contribuyente.ActividadesEconomicas
                txtIdCliente.Text = datos.IdCliente
                cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
                txtIdentificacion.Text = datos.Identificacion
                txtDireccion.Text = datos.Direccion
                txtNombre.Text = datos.Nombre
                txtNombreComercial.Text = datos.NombreComercial
                txtTelefono.Text = datos.Telefono
                txtCelular.Text = datos.Celular
                txtFax.Text = datos.Fax
                txtCorreoElectronico.Text = datos.CorreoElectronico
                chkPermiteCredito.Checked = datos.PermiteCredito
                cboIdTipoPrecio.SelectedValue = datos.IdTipoPrecio
                cboTipoExoneracion.SelectedValue = datos.IdTipoExoneracion
                cboInstExoneracion.SelectedValue = datos.IdNombreInstExoneracion
                txtNumDocExoneracion.Text = datos.NumDocExoneracion
                txtArticulo.Text = datos.ArticuloExoneracion
                txtInciso.Text = datos.IncisoExoneracion
                txtFechaExoneracion.Value = datos.FechaEmisionDoc
                txtPorcentajeExoneracion.Text = datos.PorcentajeExoneracion
                If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then
                    If datos.CodigoActividad <> "" Then
                        cboActividadEconomica.SelectedValue = Integer.Parse(datos.CodigoActividad)
                    Else
                        cboActividadEconomica.SelectedIndex = -1
                    End If
                End If
            Else
                datos = New Cliente
                cboTipoExoneracion.SelectedValue = StaticValoresPorDefecto.TipoExoneracion
                cboInstExoneracion.SelectedValue = StaticValoresPorDefecto.IdNombreInstExoneracion
                txtPorcentajeExoneracion.Text = "0"
                txtFechaExoneracion.Value = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Async Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        If cboTipoIdentificacion.SelectedValue Is Nothing Or txtIdentificacion.Text.Length = 0 Or txtDireccion.Text.Length = 0 Or txtNombre.Text.Length = 0 Or txtCorreoElectronico.Text.Length = 0 Then
            MessageBox.Show("Existen campos requeridos que no se fueron ingresados. Por favor verifique la información.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If txtPorcentajeExoneracion.Text <> "" Then
            If Integer.Parse(txtPorcentajeExoneracion.Text) > 13 Then
                MessageBox.Show("El porcentaje de exoneración no puede ser mayor a 13%. Por favor verifique la información.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
        End If
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        If datos.IdCliente = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.IdTipoIdentificacion = cboTipoIdentificacion.SelectedValue
        datos.Identificacion = txtIdentificacion.Text
        datos.Direccion = txtDireccion.Text
        datos.Nombre = txtNombre.Text
        datos.NombreComercial = txtNombreComercial.Text
        datos.Telefono = txtTelefono.Text
        datos.Celular = txtCelular.Text
        datos.Fax = txtFax.Text
        datos.CorreoElectronico = txtCorreoElectronico.Text
        datos.PermiteCredito = chkPermiteCredito.Checked
        datos.IdTipoPrecio = cboIdTipoPrecio.SelectedValue
        datos.IdTipoExoneracion = cboTipoExoneracion.SelectedValue
        datos.IdNombreInstExoneracion = cboInstExoneracion.SelectedValue
        datos.NumDocExoneracion = txtNumDocExoneracion.Text
        datos.ArticuloExoneracion = txtArticulo.Text
        datos.IncisoExoneracion = txtInciso.Text
        datos.FechaEmisionDoc = txtFechaExoneracion.Value
        datos.PorcentajeExoneracion = txtPorcentajeExoneracion.Text
        datos.CodigoActividad = ""
        If cboActividadEconomica.SelectedValue <> Nothing Then datos.CodigoActividad = cboActividadEconomica.SelectedValue
        Try
            If datos.IdCliente = 0 Then
                Await Puntoventa.AgregarCliente(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarCliente(datos, FrmPrincipal.usuarioGlobal.Token)
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

    Private Async Sub Identificacion_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtIdentificacion.Validated
        Try
            If txtIdCliente.Text = "" And txtIdentificacion.Text <> "" Then
                Dim contribuyente = Await Puntoventa.ObtenerInformacionContribuyente(txtIdentificacion.Text)
                If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then cboActividadEconomica.DataSource = contribuyente.ActividadesEconomicas
                Dim cliente As Cliente = Nothing
                cliente = Await Puntoventa.ValidaIdentificacionCliente(FrmPrincipal.empresaGlobal.IdEmpresa, txtIdentificacion.Text, FrmPrincipal.usuarioGlobal.Token)
                If cliente IsNot Nothing Then
                    MessageBox.Show("La identificación ingresada ya se encuentra registrada en la base de datos de clientes del sistema.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    datos = cliente
                    txtIdCliente.Text = datos.IdCliente
                    cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
                    txtIdentificacion.Text = datos.Identificacion
                    txtDireccion.Text = datos.Direccion
                    txtNombre.Text = datos.Nombre
                    txtNombreComercial.Text = datos.NombreComercial
                    txtTelefono.Text = datos.Telefono
                    txtCelular.Text = datos.Celular
                    txtFax.Text = datos.Fax
                    txtCorreoElectronico.Text = datos.CorreoElectronico
                    cboIdTipoPrecio.SelectedValue = datos.IdTipoPrecio
                    cboTipoExoneracion.SelectedValue = datos.IdTipoExoneracion
                    txtNumDocExoneracion.Text = datos.NumDocExoneracion
                    txtArticulo.Text = datos.ArticuloExoneracion
                    txtInciso.Text = datos.IncisoExoneracion
                    cboInstExoneracion.SelectedValue = datos.IdNombreInstExoneracion
                    txtFechaExoneracion.Text = datos.FechaEmisionDoc.ToString()
                    txtPorcentajeExoneracion.Text = datos.PorcentajeExoneracion
                    If Not FrmPrincipal.empresaGlobal.RegimenSimplificado Then cboActividadEconomica.SelectedValue = Integer.Parse(datos.CodigoActividad)
                Else
                    txtNombre.Text = contribuyente.Nombre
                    If Not FrmPrincipal.empresaGlobal.RegimenSimplificado And cboActividadEconomica.Items.Count > 0 Then cboActividadEconomica.SelectedIndex = 0
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtIdentificacion.KeyPress, txtTelefono.KeyPress, txtCelular.KeyPress, txtFax.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub txtPorcentajeExoneracion_TextChanged(sender As Object, e As KeyPressEventArgs) Handles txtPorcentajeExoneracion.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 0, ".")
    End Sub

    Private Sub textJustNumbers_TextChanged(sender As Object, e As EventArgs) Handles txtIdentificacion.TextChanged, txtTelefono.TextChanged, txtCelular.TextChanged, txtFax.TextChanged, txtNumDocExoneracion.TextChanged, txtPorcentajeExoneracion.TextChanged
        Dim origin As TextBox = DirectCast(sender, TextBox)
        Dim texto As String = origin.Text
        texto = Regex.Replace(texto, "[^0-9]", "")
        origin.Text = texto
    End Sub
#End Region
End Class