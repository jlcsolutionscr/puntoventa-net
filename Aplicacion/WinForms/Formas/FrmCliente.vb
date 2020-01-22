Imports System.Threading.Tasks
Imports System.Globalization
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmCliente
#Region "Variables"
    Public intIdCliente As Integer
    Private datos As Cliente
    Private bolInit As Boolean = True
    Private provider As CultureInfo = CultureInfo.InvariantCulture
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

    Private Async Function CargarListadoBarrios(IdProvincia As Integer, IdCanton As Integer, IdDistrito As Integer) As Task
        cboCanton.ValueMember = "Id"
        cboCanton.DisplayMember = "Descripcion"
        cboCanton.DataSource = Await Puntoventa.ObtenerListadoCantones(IdProvincia, FrmPrincipal.usuarioGlobal.Token)
        cboDistrito.ValueMember = "Id"
        cboDistrito.DisplayMember = "Descripcion"
        cboDistrito.DataSource = Await Puntoventa.ObtenerListadoDistritos(IdProvincia, IdCanton, FrmPrincipal.usuarioGlobal.Token)
        cboBarrio.ValueMember = "Id"
        cboBarrio.DisplayMember = "Descripcion"
        cboBarrio.DataSource = Await Puntoventa.ObtenerListadoBarrios(IdProvincia, IdCanton, IdDistrito, FrmPrincipal.usuarioGlobal.Token)
        cboTipoImpuesto.ValueMember = "Id"
        cboTipoImpuesto.DisplayMember = "Descripcion"
        cboTipoImpuesto.DataSource = Await Puntoventa.ObtenerListadoTipoImpuesto(FrmPrincipal.usuarioGlobal.Token)
    End Function

    Private Async Function CargarCombos() As Task
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = Await Puntoventa.ObtenerListadoTipoIdentificacion(FrmPrincipal.usuarioGlobal.Token)
        cboProvincia.ValueMember = "Id"
        cboProvincia.DisplayMember = "Descripcion"
        cboProvincia.DataSource = Await Puntoventa.ObtenerListadoProvincias(FrmPrincipal.usuarioGlobal.Token)
        cboVendedor.ValueMember = "Id"
        cboVendedor.DisplayMember = "Descripcion"
        cboVendedor.DataSource = Await Puntoventa.ObtenerListadoVendedores(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboIdTipoPrecio.ValueMember = "Id"
        cboIdTipoPrecio.DisplayMember = "Descripcion"
        cboIdTipoPrecio.DataSource = Await Puntoventa.ObtenerListadoTipodePrecio(FrmPrincipal.usuarioGlobal.Token)
        cboTipoExoneracion.ValueMember = "Id"
        cboTipoExoneracion.DisplayMember = "Descripcion"
        cboTipoExoneracion.DataSource = Await Puntoventa.ObtenerListadoTipoExoneracion(FrmPrincipal.usuarioGlobal.Token)
    End Function
#End Region

#Region "Eventos Controles"
    Private Async Sub FrmCliente_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            Await CargarCombos()
            If intIdCliente > 0 Then
                datos = Await Puntoventa.ObtenerCliente(intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    Throw New Exception("El cliente seleccionado no existe")
                End If
                Await CargarListadoBarrios(datos.IdProvincia, datos.IdCanton, datos.IdDistrito)
                txtIdCliente.Text = datos.IdCliente
                cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
                txtIdentificacion.Text = datos.Identificacion
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
                chkExonerado.Checked = datos.AplicaTasaDiferenciada
                cboTipoImpuesto.SelectedValue = datos.IdImpuesto
                cboTipoExoneracion.SelectedValue = datos.IdTipoExoneracion
                txtNumDocExoneracion.Text = datos.NumDocExoneracion
                txtNombreInstExoneracion.Text = datos.NombreInstExoneracion
                txtFechaExoneracion.Value = datos.FechaEmisionDoc
                txtPorcentajeExoneracion.Text = datos.PorcentajeExoneracion
                txtIdentificacion.ReadOnly = True
            Else
                datos = New Cliente
                Await CargarListadoBarrios(1, 1, 1)
                cboTipoImpuesto.SelectedValue = 8
                cboTipoExoneracion.SelectedValue = 1
                txtPorcentajeExoneracion.Text = "0"
                txtFechaExoneracion.Value = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
            End If
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
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        If cboTipoIdentificacion.SelectedValue Is Nothing Or txtIdentificacion.Text.Length = 0 Or cboProvincia.SelectedValue Is Nothing Or cboCanton.SelectedValue Is Nothing Or cboDistrito.SelectedValue Is Nothing Or cboBarrio.SelectedValue Is Nothing Or txtDireccion.Text.Length = 0 Or txtNombre.Text.Length = 0 Or txtCorreoElectronico.Text.Length = 0 Then
            MessageBox.Show("Existen campos requeridos que no se fueron ingresados. Por favor verifique la información. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End If
        If datos.IdCliente = 0 Then
            datos.IdEmpresa = FrmPrincipal.empresaGlobal.IdEmpresa
        End If
        datos.IdTipoIdentificacion = cboTipoIdentificacion.SelectedValue
        datos.Identificacion = txtIdentificacion.Text
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
        datos.AplicaTasaDiferenciada = chkExonerado.Checked
        datos.IdImpuesto = cboTipoImpuesto.SelectedValue
        datos.IdTipoExoneracion = cboTipoExoneracion.SelectedValue
        datos.NumDocExoneracion = txtNumDocExoneracion.Text
        datos.NombreInstExoneracion = txtNombreInstExoneracion.Text
        datos.FechaEmisionDoc = txtFechaExoneracion.Value
        datos.PorcentajeExoneracion = txtPorcentajeExoneracion.Text
        datos.Barrio = Nothing
        Try
            If datos.IdCliente = 0 Then
                Await Puntoventa.AgregarCliente(datos, FrmPrincipal.usuarioGlobal.Token)
            Else
                Await Puntoventa.ActualizarCliente(datos, FrmPrincipal.usuarioGlobal.Token)
            End If
        Catch ex As Exception
            btnGuardar.Enabled = True
            btnGuardar.Focus()
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Async Sub Identificacion_Validating(ByVal sender As Object, ByVal e As EventArgs) Handles txtIdentificacion.Validated
        Try
            If txtIdCliente.Text = "" And txtIdentificacion.Text <> "" Then
                Dim cliente As Cliente = Nothing
                cliente = Await Puntoventa.ValidaIdentificacionCliente(FrmPrincipal.empresaGlobal.IdEmpresa, txtIdentificacion.Text, FrmPrincipal.usuarioGlobal.Token)
                If cliente IsNot Nothing Then
                    bolInit = True
                    If cliente.IdCliente > 0 Then
                        MessageBox.Show("La identificación ingresada ya se encuentra registrada en la base de datos de clientes del sistema.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        datos = cliente
                        txtIdCliente.Text = datos.IdCliente
                        cboTipoIdentificacion.SelectedValue = datos.IdTipoIdentificacion
                        txtIdentificacion.Text = datos.Identificacion
                        Await CargarListadoBarrios(datos.IdProvincia, datos.IdCanton, datos.IdDistrito)
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
                        chkExonerado.Checked = datos.AplicaTasaDiferenciada
                        cboTipoImpuesto.SelectedValue = datos.IdImpuesto
                        cboTipoExoneracion.SelectedValue = datos.IdTipoExoneracion
                        txtNumDocExoneracion.Text = datos.NumDocExoneracion
                        txtNombreInstExoneracion.Text = datos.NombreInstExoneracion
                        txtFechaExoneracion.Text = datos.FechaEmisionDoc.ToString()
                        txtPorcentajeExoneracion.Text = datos.PorcentajeExoneracion
                    Else
                        MessageBox.Show("Cliente encontrado en el padrón electoral. Por favor complete la información faltante.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cboTipoIdentificacion.SelectedValue = 0
                        Await CargarListadoBarrios(cliente.IdProvincia, cliente.IdCanton, cliente.IdDistrito)
                        cboProvincia.SelectedValue = cliente.IdProvincia
                        cboCanton.SelectedValue = cliente.IdCanton
                        cboDistrito.SelectedValue = cliente.IdDistrito
                        cboBarrio.SelectedValue = 0
                        txtNombre.Text = cliente.Nombre
                    End If
                    bolInit = False
                ElseIf cboTipoIdentificacion.SelectedValue = 0 Then
                    MessageBox.Show("No se encontró la identificación registrada en el sistema o en el padrón electoral. Por favor ingrese la información completa.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub CboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        If Not bolInit Then
            bolInit = True
            Await CargarListadoBarrios(cboProvincia.SelectedValue, 1, 1)
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

    Private Sub ValidaDigitos(sender As Object, e As KeyPressEventArgs) Handles txtIdentificacion.KeyPress, txtTelefono.KeyPress, txtCelular.KeyPress, txtFax.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 2, ".")
    End Sub

    Private Sub ChkExonerado_CheckedChanged(sender As Object, e As EventArgs) Handles chkExonerado.CheckedChanged
        If chkExonerado.Checked Then
            cboTipoImpuesto.Enabled = True
        Else
            cboTipoImpuesto.Enabled = False
            cboTipoImpuesto.SelectedValue = 8
        End If
    End Sub

    Private Sub txtPorcentajeExoneracion_TextChanged(sender As Object, e As KeyPressEventArgs) Handles txtPorcentajeExoneracion.KeyPress
        FrmPrincipal.ValidaNumero(e, sender, True, 0, ".")
    End Sub
#End Region
End Class