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
    Private Async Function CargarCombos() As Task
        cboTipoIdentificacion.ValueMember = "Id"
        cboTipoIdentificacion.DisplayMember = "Descripcion"
        cboTipoIdentificacion.DataSource = Await Puntoventa.ObtenerListadoTipoIdentificacion(FrmPrincipal.usuarioGlobal.Token)
        cboVendedor.ValueMember = "Id"
        cboVendedor.DisplayMember = "Descripcion"
        cboVendedor.DataSource = Await Puntoventa.ObtenerListadoVendedores(FrmPrincipal.empresaGlobal.IdEmpresa, "", FrmPrincipal.usuarioGlobal.Token)
        cboIdTipoPrecio.ValueMember = "Id"
        cboIdTipoPrecio.DisplayMember = "Descripcion"
        cboIdTipoPrecio.DataSource = Await Puntoventa.ObtenerListadoTipodePrecio(FrmPrincipal.usuarioGlobal.Token)
        cboTipoImpuesto.ValueMember = "Id"
        cboTipoImpuesto.DisplayMember = "Descripcion"
        cboTipoImpuesto.DataSource = Await Puntoventa.ObtenerListadoTipoImpuesto(FrmPrincipal.usuarioGlobal.Token)
        cboTipoExoneracion.ValueMember = "Id"
        cboTipoExoneracion.DisplayMember = "Descripcion"
        cboTipoExoneracion.DataSource = Await Puntoventa.ObtenerListadoTipoExoneracion(FrmPrincipal.usuarioGlobal.Token)
    End Function
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
            Await CargarCombos()
            If intIdCliente > 0 Then
                datos = Await Puntoventa.ObtenerCliente(intIdCliente, FrmPrincipal.usuarioGlobal.Token)
                If datos Is Nothing Then
                    Throw New Exception("El cliente seleccionado no existe")
                End If
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
                cboTipoImpuesto.SelectedValue = 8
                cboTipoExoneracion.SelectedValue = 1
                txtPorcentajeExoneracion.Text = "0"
                txtFechaExoneracion.Value = Date.ParseExact("01/01/2019", "dd/MM/yyyy", provider)
            End If
            bolInit = False
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
            MessageBox.Show("Existen campos requeridos que no se fueron ingresados. Por favor verifique la información. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
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
        datos.IdVendedor = cboVendedor.SelectedValue
        datos.IdTipoPrecio = cboIdTipoPrecio.SelectedValue
        datos.AplicaTasaDiferenciada = chkExonerado.Checked
        datos.IdImpuesto = cboTipoImpuesto.SelectedValue
        datos.IdTipoExoneracion = cboTipoExoneracion.SelectedValue
        datos.NumDocExoneracion = txtNumDocExoneracion.Text
        datos.NombreInstExoneracion = txtNombreInstExoneracion.Text
        datos.FechaEmisionDoc = txtFechaExoneracion.Value
        datos.PorcentajeExoneracion = txtPorcentajeExoneracion.Text
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
                Dim cliente As Cliente = Nothing
                cliente = Await Puntoventa.ValidaIdentificacionCliente(FrmPrincipal.empresaGlobal.IdEmpresa, txtIdentificacion.Text, FrmPrincipal.usuarioGlobal.Token)
                If cliente IsNot Nothing Then
                    bolInit = True
                    If cliente.IdCliente > 0 Then
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
                        MessageBox.Show("Cliente encontrado en el padrón electoral. Por favor complete la información faltante.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        cboTipoIdentificacion.SelectedValue = 0
                        txtNombre.Text = cliente.Nombre
                    End If
                    bolInit = False
                ElseIf cboTipoIdentificacion.SelectedValue = 0 Then
                    MessageBox.Show("No se encontró la identificación registrada en el sistema o en el padrón electoral. Por favor ingrese la información completa.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                End If
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
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