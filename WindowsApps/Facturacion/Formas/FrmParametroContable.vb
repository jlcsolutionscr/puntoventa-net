Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes

Public Class FrmParametroContable
#Region "Variables"
    Public intIdParametro As Integer
    Private datos As ParametroContable
    Private tipoParametro As TipoParametroContable
    Private cuentaContable As CatalogoContable
    Private bolReady As Boolean = False
#End Region

#Region "Métodos"
    Private Function ValidarCampos(ByRef pCampo As String) As Boolean
        If cboTipoParametro.SelectedValue Is Nothing Then
            pCampo = "Tipo Parámetro"
            Return False
        ElseIf cboCuentaContable.SelectedValue Is Nothing Then
            pCampo = "Cuenta Contable"
            Return False
        ElseIf cboProducto.SelectedValue Is Nothing And tipoParametro.MultiCuenta Then
            pCampo = "Producto"
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub CargarCombos()
        cboTipoParametro.ValueMember = "IdTipo"
        cboTipoParametro.DisplayMember = "Descripcion"
        'cboTipoParametro.DataSource = servicioContabilidad.ObtenerTiposParametroContable()
        cboCuentaContable.ValueMember = "IdCuenta"
        cboCuentaContable.DisplayMember = "DescripcionCompleta"
        'cboCuentaContable.DataSource = servicioContabilidad.ObtenerListaCuentasParaMovimientos(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
    End Sub

    Private Sub CargarDatosCuentaContable(ByVal intIdCuenta As Integer)
        Try
            'cuentaContable = servicioContabilidad.ObtenerCuentaContable(intIdCuenta)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub CargarDatosProducto(ByVal tipoParametro As TipoParametroContable)
        If tipoParametro.MultiCuenta Then
            cboProducto.Enabled = True
            Select Case tipoParametro.IdTipo
                Case StaticTipoParametroContable.CuentaDeBancos
                    'cboProducto.DataSource = servicioBancario.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdCuenta"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoParametroContable.CuentaDeEgresos
                    'cboProducto.DataSource = servicioEgresos.ObtenerListaCuentasEgreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdCuenta"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoParametroContable.CuentaDeIngresos
                    'cboProducto.DataSource = servicioIngresos.ObtenerListaCuentasIngreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdCuenta"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoParametroContable.LineaDeProductos
                    'cboProducto.DataSource = servicioMantenimiento.ObtenerListaLineasDeProducto(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdLinea"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoParametroContable.LineaDeServicios
                    'cboProducto.DataSource = servicioMantenimiento.ObtenerListaLineasDeServicio(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdLinea"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoParametroContable.Traslados
                    'cboProducto.DataSource = servicioTraslados.ObtenerListaSucursales(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdSucursal"
                    cboProducto.DisplayMember = "Nombre"
            End Select
        Else
            cboProducto.DataSource = Nothing
            cboProducto.Enabled = False
        End If
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmParametroContable_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Sub FrmParametroContable_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarCombos()
            If intIdParametro > 0 Then
                'datos = servicioContabilidad.ObtenerParametroContable(intIdParametro)
                If datos Is Nothing Then
                    Throw New Exception("El parámetro contable seleccionado no existe")
                End If
                txtIdParametro.Text = datos.IdParametro
                cboTipoParametro.SelectedValue = datos.IdTipo
                cboCuentaContable.SelectedValue = datos.IdCuenta
                CargarDatosCuentaContable(datos.IdCuenta)
                CargarDatosProducto(datos.TipoParametroContable)
                If datos.IdProducto > 0 Then
                    cboProducto.SelectedValue = datos.IdProducto
                End If
                'tipoParametro = servicioContabilidad.ObtenerTipoParametroContable(cboTipoParametro.SelectedValue)
            Else
                datos = New ParametroContable
                If cboCuentaContable.SelectedValue > 0 Then CargarDatosCuentaContable(cboCuentaContable.SelectedValue)
            End If
            bolReady = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        btnCancelar.Focus()
        btnGuardar.Enabled = False
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            btnGuardar.Enabled = True
            Exit Sub
        End If
        datos.IdTipo = cboTipoParametro.SelectedValue
        datos.IdCuenta = cboCuentaContable.SelectedValue
        If tipoParametro.MultiCuenta Then
            datos.IdProducto = cboProducto.SelectedValue
        Else
            datos.IdProducto = 0
        End If
        Try
            If datos.IdParametro = 0 Then
                'datos = servicioContabilidad.AgregarParametroContable(datos)
                txtIdParametro.Text = datos.IdParametro
            Else
                'servicioContabilidad.ActualizarParametroContable(datos)
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

    Private Sub cboTipoParametro_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoParametro.SelectedValueChanged
        If bolReady And cboTipoParametro.SelectedValue IsNot Nothing Then
            Try
                'tipoParametro = servicioContabilidad.ObtenerTipoParametroContable(cboTipoParametro.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(tipoParametro)
        End If
    End Sub

    Private Sub cboCuentaContable_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCuentaContable.SelectedValueChanged
        If bolReady And cboCuentaContable.SelectedValue IsNot Nothing Then
            Try
                'cuentaContable = servicioContabilidad.ObtenerCuentaContable(cboCuentaContable.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
#End Region
End Class