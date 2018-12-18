Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmCliente
#Region "Variables"
    Public intIdCliente As Integer
    Private datos As Cliente
    Private bolInit As Boolean = True
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If txtNombre.Text = "" Then
            pCampo = "Nombre"
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub CargarListadoBarrios(IdProvincia As Integer, IdCanton As Integer, IdDistrito As Integer)
        Try
            cboCanton.ValueMember = "IdCanton"
            cboCanton.DisplayMember = "Descripcion"
            'cboCanton.DataSource = servicioMantenimiento.ObtenerListaCantones(IdProvincia)
            cboDistrito.ValueMember = "IdDistrito"
            cboDistrito.DisplayMember = "Descripcion"
            'cboDistrito.DataSource = servicioMantenimiento.ObtenerListaDistritos(IdProvincia, IdCanton)
            cboBarrio.ValueMember = "IdBarrio"
            cboBarrio.DisplayMember = "Descripcion"
            'cboBarrio.DataSource = servicioMantenimiento.ObtenerListaBarrios(IdProvincia, IdCanton, IdDistrito)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub CargarCombos()
        Try
            cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion"
            cboTipoIdentificacion.DisplayMember = "Descripcion"
            'cboTipoIdentificacion.DataSource = servicioMantenimiento.ObtenerListaTipoIdentificacion()
            cboProvincia.ValueMember = "IdProvincia"
            cboProvincia.DisplayMember = "Descripcion"
            'cboProvincia.DataSource = servicioMantenimiento.ObtenerListaProvincias()
            cboVendedor.ValueMember = "IdVendedor"
            cboVendedor.DisplayMember = "Nombre"
            'cboVendedor.DataSource = servicioMantenimiento.ObtenerListaVendedores(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            cboIdTipoPrecio.ValueMember = "IdTipoPrecio"
            cboIdTipoPrecio.DisplayMember = "Descripcion"
            'cboIdTipoPrecio.DataSource = servicioMantenimiento.ObtenerListaTipodePrecio()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmCliente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
        If intIdCliente > 0 Then
            Try
                'datos = servicioFacturacion.ObtenerCliente(intIdCliente)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El cliente seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            CargarListadoBarrios(datos.IdProvincia, datos.IdCanton, datos.IdDistrito)
            txtIdCliente.Text = datos.IdCliente
            cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
            txtIdentificacion.Text = datos.Identificacion
            txtIdentificacionExtranjero.Text = datos.IdentificacionExtranjero
            cboProvincia.SelectedValue = datos.IdProvincia
            cboCanton.SelectedValue = datos.IdCanton
            cboDistrito.SelectedValue = datos.IdDistrito
            cboBarrio.SelectedValue = datos.IdBarrio
            txtDireccion.Text = datos.Direccion
            txtNombre.Text = datos.Nombre
            txtNombreComercial.Text = datos.NombreComercial
            txtTelefono.Text = datos.Telefono
            txtCelular.Text = datos.Celular
            txtFax.Text = datos.Fax
            txtCorreoElectronico.Text = datos.CorreoElectronico
            If datos.IdVendedor IsNot Nothing Then cboVendedor.SelectedValue = datos.IdVendedor
            cboIdTipoPrecio.SelectedValue = datos.IdTipoPrecio
            txtIdentificacion.ReadOnly = True
        Else
            datos = New Cliente
            CargarListadoBarrios(1, 1, 1)
        End If
        bolInit = False
    End Sub

    Private Sub BtnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub BtnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        If cboTipoIdentificacion.SelectedValue Is Nothing Or txtIdentificacion.Text.Length = 0 Or cboProvincia.SelectedValue Is Nothing Or cboCanton.SelectedValue Is Nothing Or cboDistrito.SelectedValue Is Nothing Or cboBarrio.SelectedValue Is Nothing Or txtDireccion.Text.Length = 0 Or txtNombre.Text.Length = 0 Or txtCorreoElectronico.Text.Length = 0 Then
            MessageBox.Show("Existen campos requeridos que no se fueron ingresados. Por favor verifique la información. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If datos.IdCliente = 0 Then
            datos.IdEmpresa = FrmMenuPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.IdTipoIdentificacion = cboTipoIdentificacion.SelectedValue
        datos.Identificacion = txtIdentificacion.Text
        datos.IdentificacionExtranjero = txtIdentificacionExtranjero.Text
        datos.IdProvincia = cboProvincia.SelectedValue
        datos.IdCanton = cboCanton.SelectedValue
        datos.IdDistrito = cboDistrito.SelectedValue
        datos.IdBarrio = cboBarrio.SelectedValue
        datos.Direccion = txtDireccion.Text
        datos.Nombre = txtNombre.Text
        datos.NombreComercial = txtNombreComercial.Text
        datos.Telefono = txtTelefono.Text
        datos.Celular = txtCelular.Text
        datos.Fax = txtFax.Text
        datos.CorreoElectronico = txtCorreoElectronico.Text
        datos.IdVendedor = cboVendedor.SelectedValue
        datos.IdTipoPrecio = cboIdTipoPrecio.SelectedValue
        datos.Barrio = Nothing
        Try
            If datos.IdCliente = 0 Then
                'servicioFacturacion.AgregarCliente(datos)
                txtIdCliente.Text = datos.IdCliente
            Else
                'servicioFacturacion.ActualizarCliente(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub Identificacion_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtIdentificacion.Validated
        If txtIdCliente.Text = "" And txtIdentificacion.Text <> "" Then
            Dim cliente As Cliente = Nothing
            Try
                'cliente = servicioFacturacion.ValidaIdentificacionCliente(FrmMenuPrincipal.empresaGlobal.IdEmpresa, txtIdentificacion.Text)
            Catch ex As Exception
            End Try
            If (cliente IsNot Nothing) Then
                If (cliente.IdCliente > 0) Then
                    MessageBox.Show("La identificación ingresada ya se encuentra registrada en la base de datos de clientes del sistema.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    datos = cliente
                    txtIdCliente.Text = datos.IdCliente
                    cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
                    txtIdentificacion.Text = datos.Identificacion
                    txtIdentificacionExtranjero.Text = datos.IdentificacionExtranjero
                    cboProvincia.SelectedValue = datos.IdProvincia
                    cboCanton.SelectedValue = datos.IdCanton
                    cboDistrito.SelectedValue = datos.IdDistrito
                    cboBarrio.SelectedValue = datos.IdBarrio
                    txtDireccion.Text = datos.Direccion
                    txtNombre.Text = datos.Nombre
                    txtNombreComercial.Text = datos.NombreComercial
                    txtTelefono.Text = datos.Telefono
                    txtCelular.Text = datos.Celular
                    txtFax.Text = datos.Fax
                    txtCorreoElectronico.Text = datos.CorreoElectronico
                    If datos.IdVendedor IsNot Nothing Then cboVendedor.SelectedValue = datos.IdVendedor
                    cboIdTipoPrecio.SelectedValue = datos.IdTipoPrecio
                Else
                    MessageBox.Show("Cliente encontrado en el padrón electoral. Por favor complete la información faltante.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    cboTipoIdentificacion.SelectedValue = 0
                    txtIdentificacion.Text = cliente.Identificacion
                    cboProvincia.SelectedValue = cliente.IdProvincia
                    cboCanton.SelectedValue = cliente.IdCanton
                    cboDistrito.SelectedValue = cliente.IdDistrito
                    cboBarrio.SelectedValue = 0
                    txtNombre.Text = cliente.Nombre
                End If
            Else
                MessageBox.Show("No se encontró la identificación registrada en el sistema o en el padrón electoral. Por favor ingrese la información completa.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        End If
    End Sub

    Private Sub ValidaDigitos(ByVal sender As Object, ByVal e As KeyPressEventArgs)
        FrmMenuPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub CboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        If Not bolInit Then
            'cboCanton.DataSource = servicioMantenimiento.ObtenerListaCantones(cboProvincia.SelectedValue)
            'cboDistrito.DataSource = servicioMantenimiento.ObtenerListaDistritos(cboProvincia.SelectedValue, 1)
            'cboBarrio.DataSource = servicioMantenimiento.ObtenerListaBarrios(cboProvincia.SelectedValue, 1, 1)
        End If
    End Sub

    Private Sub CboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        If Not bolInit Then
            'cboDistrito.DataSource = servicioMantenimiento.ObtenerListaDistritos(cboProvincia.SelectedValue, cboCanton.SelectedValue)
            'cboBarrio.DataSource = servicioMantenimiento.ObtenerListaBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, 1)
        End If
    End Sub

    Private Sub CboDistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrito.SelectedIndexChanged
        If Not bolInit Then
            'cboBarrio.DataSource = servicioMantenimiento.ObtenerListaBarrios(cboProvincia.SelectedValue, cboCanton.SelectedValue, cboDistrito.SelectedValue)
        End If
    End Sub
#End Region
End Class