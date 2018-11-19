Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity
Imports LeandroSoftware.PuntoVenta.Core.CommonTypes

Public Class FrmParametroContable
#Region "Variables"
    Public servicioContabilidad As IContabilidadService
    Public intIdParametro As Integer
    Private datos As ParametroContable
    Private tipoParametro As TipoParametroContable
    Private cuentaContable As CatalogoContable
    Private bolInit As Boolean = True
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
        Try
            cboTipoParametro.ValueMember = "IdTipo"
            cboTipoParametro.DisplayMember = "Descripcion"
            cboTipoParametro.DataSource = servicioContabilidad.ObtenerTiposParametroContable()
            cboCuentaContable.ValueMember = "IdCuenta"
            cboCuentaContable.DisplayMember = "DescripcionCompleta"
            cboCuentaContable.DataSource = servicioContabilidad.ObtenerListaCuentasParaMovimientos(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub CargarDatosCuentaContable(ByVal intIdCuenta As Integer)
        Try
            cuentaContable = servicioContabilidad.ObtenerCuentaContable(intIdCuenta)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub CargarDatosProducto(ByVal tipoParametro As TipoParametroContable)
        If tipoParametro.MultiCuenta Then
            cboProducto.Enabled = True
            Select Case tipoParametro.IdTipo
                Case StaticTipoCuentaContable.CuentaDeBancos
                    Dim servicioBancario As IBancaService = FrmMenuPrincipal.unityContainer.Resolve(Of IBancaService)()
                    cboProducto.DataSource = servicioBancario.ObtenerListaCuentasBanco(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdCuenta"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoCuentaContable.CuentaDeEgresos
                    Dim servicioEgresos As IEgresoService = FrmMenuPrincipal.unityContainer.Resolve(Of IEgresoService)()
                    cboProducto.DataSource = servicioEgresos.ObtenerListaCuentasEgreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdCuenta"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoCuentaContable.CuentaDeIngresos
                    Dim servicioIngresos As IIngresoService = FrmMenuPrincipal.unityContainer.Resolve(Of IIngresoService)()
                    cboProducto.DataSource = servicioIngresos.ObtenerListaCuentasIngreso(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdCuenta"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoCuentaContable.LineaDeProductos
                    Dim servicioMantenimiento As IMantenimientoService = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
                    cboProducto.DataSource = servicioMantenimiento.ObtenerListaLineasDeProducto(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdLinea"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoCuentaContable.LineaDeServicios
                    Dim servicioMantenimiento As IMantenimientoService = FrmMenuPrincipal.unityContainer.Resolve(Of IMantenimientoService)()
                    cboProducto.DataSource = servicioMantenimiento.ObtenerListaLineasDeServicio(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
                    cboProducto.ValueMember = "IdLinea"
                    cboProducto.DisplayMember = "Descripcion"
                Case StaticTipoCuentaContable.Traslados
                    Dim servicioTraslados As ITrasladoService = FrmMenuPrincipal.unityContainer.Resolve(Of ITrasladoService)()
                    cboProducto.DataSource = servicioTraslados.ObtenerListaSucursales(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
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
    Private Sub FrmParametroContable_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        CargarCombos()
        If intIdParametro > 0 Then
            Try
                datos = servicioContabilidad.ObtenerParametroContable(intIdParametro)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End Try
            If datos Is Nothing Then
                MessageBox.Show("El parámetro contable seleccionado no existe", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            txtIdParametro.Text = datos.IdParametro
            cboTipoParametro.SelectedValue = datos.IdTipo
            cboCuentaContable.SelectedValue = datos.IdCuenta
            CargarDatosCuentaContable(datos.IdCuenta)
            CargarDatosProducto(datos.TipoParametroContable)
            If datos.IdProducto > 0 Then
                cboProducto.SelectedValue = datos.IdProducto
            End If
            Try
                tipoParametro = servicioContabilidad.ObtenerTipoParametroContable(cboTipoParametro.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        Else
            datos = New ParametroContable
            If cboCuentaContable.SelectedValue > 0 Then CargarDatosCuentaContable(cboCuentaContable.SelectedValue)
        End If
        bolInit = False
    End Sub

    Private Sub btnCancelar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnCancelar.Click
        Close()
    End Sub

    Private Sub btnGuardar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnGuardar.Click
        Dim strCampo As String = ""
        If Not ValidarCampos(strCampo) Then
            MessageBox.Show("El campo " & strCampo & " es requerido", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                datos = servicioContabilidad.AgregarParametroContable(datos)
                txtIdParametro.Text = datos.IdParametro
            Else
                servicioContabilidad.ActualizarParametroContable(datos)
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        MessageBox.Show("Registro guardado satisfactoriamente", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub cboTipoParametro_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboTipoParametro.SelectedValueChanged
        If Not bolInit And cboTipoParametro.SelectedValue IsNot Nothing Then
            Try
                tipoParametro = servicioContabilidad.ObtenerTipoParametroContable(cboTipoParametro.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            CargarDatosProducto(tipoParametro)
        End If
    End Sub

    Private Sub cboCuentaContable_SelectedValueChanged(ByVal sender As Object, ByVal e As EventArgs) Handles cboCuentaContable.SelectedValueChanged
        If Not bolInit And cboCuentaContable.SelectedValue IsNot Nothing Then
            Try
                cuentaContable = servicioContabilidad.ObtenerCuentaContable(cboCuentaContable.SelectedValue)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
        End If
    End Sub
#End Region
End Class