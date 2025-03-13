Imports System.Threading
Imports System.Configuration
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Seguridad
Imports System.Collections.Generic
Imports System.Linq

Public Class FrmPrincipal
#Region "Variables"
    Private objMenu As ToolStripMenuItem
    Private appSettings As Specialized.NameValueCollection
    Private bolEsAdministrador As Boolean = False
    Public usuarioGlobal As Usuario
    Public empresaGlobal As Empresa
    Public equipoGlobal As EquipoRegistrado
    Public intBusqueda As Integer
    Public strBusqueda As String
    Public dgvDecimal As DataGridViewCellStyle
    Public dgvInteger As DataGridViewCellStyle
    Public lstListaReportes As New List(Of String)
    Public listaEmpresas As New List(Of LlaveDescripcion)
    Public decTipoCambioDolar As Decimal
    Public strCodigoUsuario As String
    Public strContrasena As String
    Public strIdEmpresa As String
    Public bolSalir As Boolean = False
    Public bolSeleccionaSucursal As Boolean = False
    Public bolAnularTransacciones As Boolean = False
    Public bolModificaDescripcion As Boolean = False
    Public bolModificaCliente As Boolean = False
    Public bolModificaPrecioVenta As Boolean = False
    Public productoTranstorio As Producto
    Public productoImpuestoServicio As Producto
    Public bolDescargaCancelada As Boolean = False
    Public decDescAutorizado As Decimal
    Public listaProductos As New List(Of ProductoDetalle)
    'Parametros generales
    Private listaTipoIdentificacion As List(Of LlaveDescripcion)
    Private listaFormaPagoCliente As List(Of LlaveDescripcion)
    Private listaFormaPagoEmpresa As List(Of LlaveDescripcion)
    Private listaTipoProducto As List(Of LlaveDescripcion)
    Private listaTipoImpuesto As List(Of LlaveDescripcionValor)
    Private listaTipoMoneda As List(Of LlaveDescripcion)
    Private listaCondicionVenta As List(Of LlaveDescripcion)
    Private listaTipoExoneracion As List(Of LlaveDescripcion)
    Private listaNombreInstExoneracion As List(Of LlaveDescripcion)
    Private listaSucursales As List(Of LlaveDescripcion)
    Private listaTipoPrecio As List(Of LlaveDescripcion)
    Private listaActividadEconomica As List(Of LlaveDescripcion)

#End Region

#Region "Métodos"
    Public Function ObtenerListadoFormaPagoEmpresa() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaFormaPagoEmpresa)
    End Function

    Public Function ObtenerListadoFormaPagoCliente() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaFormaPagoCliente)
    End Function

    Public Function ObtenerListadoCondicionVenta() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaCondicionVenta)
    End Function

    Public Function ObtenerListadoTipoMoneda() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaTipoMoneda)
    End Function

    Public Function ObtenerListadoTipoImpuesto() As List(Of LlaveDescripcionValor)
        Return New List(Of LlaveDescripcionValor)(listaTipoImpuesto)
    End Function

    Public Function ObtenerListadoTipoIdentificacion() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaTipoIdentificacion)
    End Function

    Public Function ObtenerListadoTipoExoneracion() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaTipoExoneracion)
    End Function

    Public Function ObtenerListadoNombreInstExoneracion() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaNombreInstExoneracion)
    End Function

    Public Function ObtenerListadoTipoProducto() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaTipoProducto)
    End Function

    Public Function ObtenerListadoEmpresas() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaEmpresas)
    End Function

    Public Function ObtenerListadoSucursales() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaSucursales)
    End Function

    Public Function ObtenerListadoTipoPrecio() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaTipoPrecio)
    End Function

    Public Function ObtenerListadoActividadEconomica() As List(Of LlaveDescripcion)
        Return New List(Of LlaveDescripcion)(listaActividadEconomica)
    End Function

    Public Function ObtenerDescripcionTipoExoneracion(intIdTipo As Integer) As String
        Dim tipo As LlaveDescripcion = listaTipoExoneracion.FirstOrDefault(Function(x) x.Id = intIdTipo)
        If tipo.Descripcion <> Nothing Then
            Return tipo.Descripcion
        Else
            Return ""
        End If
    End Function

        Public Function ObtenerDescripcionNombreInstExoneracion(intIdTipo As Integer) As String
        Dim tipo As LlaveDescripcion = listaNombreInstExoneracion.FirstOrDefault(Function(x) x.Id = intIdTipo)
        If tipo.Descripcion <> Nothing Then
            Return tipo.Descripcion
        Else
            Return ""
        End If
    End Function

    Public Function ObtenerDescripcionFormaPagoEmpresa(intIdTipo As Integer) As String
        Dim tipo As LlaveDescripcion = listaFormaPagoEmpresa.FirstOrDefault(Function(x) x.Id = intIdTipo)
        If tipo.Descripcion <> Nothing Then
            Return tipo.Descripcion
        Else
            Return ""
        End If
    End Function

    Public Function ObtenerDescripcionFormaPagoCliente(intIdTipo) As String
        Dim tipo As LlaveDescripcion = listaFormaPagoCliente.FirstOrDefault(Function(x) x.Id = intIdTipo)
        If tipo.Descripcion <> Nothing Then
            Return tipo.Descripcion
        Else
            Return ""
        End If
    End Function

    Public Function ObtenerTarifaImpuesto(intIdTipo As Integer) As Decimal
        Dim tipo As LlaveDescripcionValor = listaTipoImpuesto.FirstOrDefault(Function(x) x.Id = intIdTipo)
        If tipo Is Nothing Then Return 0
        If tipo.Valor <> Nothing Then
            Return tipo.Valor
        Else
            Return 0
        End If
    End Function

    Public Function ObtenerFechaFormateada(fecha As Date) As Date
        Return FormatDateTime(fecha, DateFormat.ShortDate)
    End Function

    Public Function RequiereActualizacion(actual As String, servidor As String) As Boolean
        Dim intVersionActual = Integer.Parse(actual.Replace(".", ""))
        Dim intUltimaVersion = Integer.Parse(servidor.Replace(".", ""))
        Return intVersionActual < intUltimaVersion
    End Function

    Public Sub ValidaNumero(ByVal e As KeyPressEventArgs, ByVal oText As TextBox, Optional ByVal pbConPuntoDec As Boolean = True, Optional ByVal pnNumDecimal As Integer = 2, Optional ByVal psSimbolo As String = ".", Optional ByVal bolNegativo As Boolean = False)
        Dim nDig As Integer
        Dim sTexto As String = String.Concat(oText.Text, e.KeyChar)
        If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = Keys.Return Then
            e.Handled = False
            Exit Sub
        End If
        If pbConPuntoDec Then
            If bolNegativo And Asc(e.KeyChar) = 45 Then
                e.Handled = False
            ElseIf Char.IsDigit(e.KeyChar) Or e.KeyChar = psSimbolo Then
                e.Handled = False
            ElseIf Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
            nDig = sTexto.Length
            If nDig = 1 And e.KeyChar = psSimbolo Then
                e.Handled = True
            End If
            If oText.SelectedText.Length = 0 Then
                If sTexto.IndexOf(psSimbolo) >= 0 And (nDig - (sTexto.IndexOf(psSimbolo) + 1)) > pnNumDecimal Then
                    e.Handled = True
                End If
            End If
        Else
            If bolNegativo And Asc(e.KeyChar) = Keys.Subtract Then
                e.Handled = False
            ElseIf Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            ElseIf Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Public Function ValidarEmpresa(empresa As Empresa, credenciales As CredencialesHacienda)
        If empresa.NombreEmpresa.Length = 0 Or
            empresa.IdTipoIdentificacion < 0 Or
            empresa.Identificacion.Length = 0 Or
            empresa.IdProvincia < 0 Or
            empresa.IdCanton < 0 Or
            empresa.IdDistrito < 0 Or
            empresa.IdBarrio < 0 Or
            empresa.Direccion.Length = 0 Or
            empresa.Telefono1.Length = 0 Or
            empresa.CorreoNotificacion.Length = 0 Or
            empresa.EquipoRegistrado.NombreSucursal.Length = 0 Or
            empresa.EquipoRegistrado.DireccionSucursal.Length = 0 Or
            empresa.EquipoRegistrado.TelefonoSucursal.Length = 0 Then
            Return False
        End If
        If Not empresa.RegimenSimplificado Then
            If credenciales Is Nothing Then
                Return False
            ElseIf credenciales.NombreCertificado.Length = 0 Or
                credenciales.PinCertificado.Length = 0 Or
                credenciales.UsuarioHacienda.Length = 0 Or
                credenciales.ClaveHacienda.Length = 0 Then
                Return False
            End If
        End If
        Return True
    End Function
#End Region

#Region "Eventos del Menu"
    Public Sub MnuArchivoCierre_Click(sender As Object, e As EventArgs) Handles mnuArchivoCierre.Click
        Dim formCierre As New FrmCierreDeCaja With {
            .MdiParent = Me
        }
        formCierre.Show()
    End Sub

    Private Sub mnuArchivoConsultaCierre_Click(sender As Object, e As EventArgs) Handles mnuArchivoConsultaCierre.Click
        Dim formBusquedaCierreCaja As New FrmCierreDeCajaListado With {
            .MdiParent = Me
        }
        formBusquedaCierreCaja.Show()
    End Sub

    Public Sub MnuArchivoReporte_Click(sender As Object, e As EventArgs) Handles MnuArchivoReporte.Click
        Dim formRptMenu As New FrmMenuReportes With {
            .MdiParent = Me
        }
        formRptMenu.Show()
    End Sub

    Public Sub MnuArchivoCambio_Click(sender As Object, e As EventArgs) Handles mnuArchivoCambio.Click
        Dim formActualizarClave As New FrmActualizarClave With {
            .MdiParent = Me
        }
        formActualizarClave.Show()
    End Sub

    Public Sub MnuArchivoSalir_Click(sender As Object, e As EventArgs) Handles MnuArchivoSalir.Click
        If MessageBox.Show("Desea salir del sistema", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then Close()
    End Sub

    Private Sub MnuParamPC_Click(sender As Object, e As EventArgs) Handles MnuParamPC.Click
        Dim formParametroContableListado As New FrmParametroContableListado With {
            .MdiParent = Me
        }
        formParametroContableListado.Show()
    End Sub

    Private Sub MnuParamBA_Click(sender As Object, e As EventArgs) Handles MnuParamBA.Click
        Dim formBancoAdquirienteListado As New FrmBancoAdquirienteListado With {
            .MdiParent = Me
        }
        formBancoAdquirienteListado.Show()
    End Sub

    Private Sub MnuParamCB_Click(sender As Object, e As EventArgs) Handles MnuParamCB.Click
        Dim formCuentaBancoListado As New FrmCuentaBancoListado With {
            .MdiParent = Me
        }
        formCuentaBancoListado.Show()
    End Sub

    Private Sub MnuParamEmpresa_Click(sender As Object, e As EventArgs) Handles MnuParamEmpresa.Click
        Dim formEmpresa As New FrmEmpresa
        formEmpresa.ShowDialog()
    End Sub

    Private Sub MnuParamRegistro_Click(sender As Object, e As EventArgs) Handles MnuParamRegistro.Click
        Dim formRegistro As New FrmRegistro()
        formRegistro.ShowDialog()
    End Sub

    Public Sub MnuMantCliente_Click(sender As Object, e As EventArgs) Handles MnuMantCliente.Click
        Dim formClienteListado As New FrmClienteListado With {
            .MdiParent = Me
        }
        formClienteListado.Show()
    End Sub

    Public Sub MnuMantLinea_Click(sender As Object, e As EventArgs) Handles MnuMantLinea.Click
        Dim formLineaListado As New FrmLineaListado With {
            .MdiParent = Me
        }
        formLineaListado.Show()
    End Sub

    Public Sub MnuMantProveedor_Click(sender As Object, e As EventArgs) Handles MnuMantProveedor.Click
        Dim formProveedorListado As New FrmProveedorListado With {
            .MdiParent = Me
        }
        formProveedorListado.Show()
    End Sub

    Public Sub MnuMantProducto_Click(sender As Object, e As EventArgs) Handles MnuMantProducto.Click
        Dim formProductoListado As New FrmProductoListado With {
            .MdiParent = Me
        }
        formProductoListado.Show()
    End Sub

    Public Sub MnuMantUsuario_Click(sender As Object, e As EventArgs) Handles MnuMantUsuario.Click
        Dim formUsuarioListado As New FrmUsuarioListado With {
            .MdiParent = Me
        }
        formUsuarioListado.Show()
    End Sub

    Private Sub MnuMantCE_Click(sender As Object, e As EventArgs) Handles MnuMantCE.Click
        Dim formCuentaEgresoListado As New FrmCuentaEgresoListado With {
            .MdiParent = Me
        }
        formCuentaEgresoListado.Show()
    End Sub

    Private Sub MnuMantCI_Click(sender As Object, e As EventArgs) Handles MnuMantCI.Click
        Dim formCuentaIngresoListado As New FrmCuentaIngresoListado With {
            .MdiParent = Me
        }
        formCuentaIngresoListado.Show()
    End Sub

    Private Sub MnuMantCB_Click(sender As Object, e As EventArgs)
        Dim formCuentaBancoListado As New FrmCuentaBancoListado With {
            .MdiParent = Me
        }
        formCuentaBancoListado.Show()
    End Sub

    Private Sub MnuMantCC_Click(sender As Object, e As EventArgs) Handles MnuMantCC.Click
        Dim formCatalogoContableListado As New FrmCatalogoContableListado With {
            .MdiParent = Me
        }
        formCatalogoContableListado.Show()
    End Sub

    Public Sub MnuMantInv_Click(sender As Object, e As EventArgs) Handles MnuMantInv.Click
        Dim formInventario As New FrmInventario With {
            .MdiParent = Me
        }
        formInventario.Show()
    End Sub

    Public Sub MnuMantVend_Click(sender As Object, e As EventArgs) Handles MnuMantVend.Click
        Dim formVendedorListado As New FrmVendedorListado With {
            .MdiParent = Me
        }
        formVendedorListado.Show()
    End Sub

    Private Sub MnuMantPuntServicio_Click(sender As Object, e As EventArgs) Handles MnuMantPuntServicio.Click
        Dim formPuntoDeServicioListado As New FrmPuntoDeServicioListado With {
            .MdiParent = Me
        }
        formPuntoDeServicioListado.Show()
    End Sub

    Public Sub MnuCapturaFactura_Click(sender As Object, e As EventArgs) Handles MnuCapturaFactura.Click
        Dim formFactura As New FrmFactura With {
            .MdiParent = Me
        }
        formFactura.Show()
    End Sub

    Public Sub MnuCapturaOrdenServicio_Click(sender As Object, e As EventArgs) Handles MnuCapturaOrdenServicio.Click
        Dim formOrdenServicio As New FrmOrdenServicio With {
            .MdiParent = Me
        }
        formOrdenServicio.Show()
    End Sub

    Private Sub MnuCapturaApartado_Click(sender As Object, e As EventArgs) Handles MnuCapturaApartado.Click
        Dim formApartado As New FrmApartado With {
            .MdiParent = Me
        }
        formApartado.Show()
    End Sub

    Public Sub MnuCapturaProforma_Click(sender As Object, e As EventArgs) Handles MnuCapturaProforma.Click
        Dim formProforma As New FrmProforma With {
            .MdiParent = Me
        }
        formProforma.Show()
    End Sub

    Public Sub MnuCapturaOrden_Click(sender As Object, e As EventArgs)
        Dim formOrdenCompra As New FrmOrdenCompra With {
            .MdiParent = Me
        }
        formOrdenCompra.Show()
    End Sub

    Private Sub MnuCapturaDevolucionProveedor_Click(sender As Object, e As EventArgs)
        Dim formDevolucionProveedores As New FrmDevolucionAProveedores With {
            .MdiParent = Me
        }
        formDevolucionProveedores.Show()
    End Sub

    Private Sub MnuCapturaDevolucionCliente_Click(sender As Object, e As EventArgs) Handles MnuDevolucionCliente.Click
        Dim formDevolucionClientes As New FrmDevolucionDeClientes With {
            .MdiParent = Me
        }
        formDevolucionClientes.Show()
    End Sub

    Private Sub MnuCapturaReimprimirTiquete_Click(sender As Object, e As EventArgs) Handles MnuCapturaReimprimirTiquete.Click
        Dim formListadoTiqueteOrdenServicio As New FrmListadoTiqueteOrdenServicio With {
            .MdiParent = Me
        }
        formListadoTiqueteOrdenServicio.Show()
    End Sub

    Public Sub MnuApRCxC_Click(sender As Object, e As EventArgs) Handles MnuApRCxC.Click
        Dim formReciboCxC As New FrmAplicaAbonoCxC With {
            .MdiParent = Me
        }
        formReciboCxC.Show()
    End Sub

    Public Sub MnuApRCxP_Click(sender As Object, e As EventArgs) Handles MnuApRCxP.Click
        Dim formReciboCxP As New FrmAplicaAbonoCxP With {
            .MdiParent = Me
        }
        formReciboCxP.Show()
    End Sub

    Private Sub MnuApRApartado_Click(sender As Object, e As EventArgs) Handles MnuApRApartado.Click
        Dim formReciboApartado As New FrmAplicaAbonoApartado With {
            .MdiParent = Me
        }
        formReciboApartado.Show()
    End Sub

    Private Sub MnuApROrdenServicio_Click(sender As Object, e As EventArgs) Handles MnuApROrdenServicio.Click
        Dim formReciboOrdenServicio As New FrmAplicaAbonoOrdenServicio With {
            .MdiParent = Me
        }
        formReciboOrdenServicio.Show()
    End Sub

    Public Sub MnuAnRCxC_Click(sender As Object, e As EventArgs) Handles MnuAnRCxC.Click
        Dim formAnulaRecCxC As New FrmGestionAbonoCxC With {
            .MdiParent = Me
        }
        formAnulaRecCxC.Show()
    End Sub

    Public Sub MnuAnRCxP_Click(sender As Object, e As EventArgs) Handles MnuAnRCxP.Click
        Dim formAnulaRecCxP As New FrmGestionAbonoCxP With {
            .MdiParent = Me
        }
        formAnulaRecCxP.Show()
    End Sub

    Private Sub MnuAnRApartado_Click(sender As Object, e As EventArgs) Handles MnuAnRApartado.Click
        Dim formAnulaRecApartado As New FrmGestionAbonoApartado With {
            .MdiParent = Me
        }
        formAnulaRecApartado.Show()
    End Sub

    Private Sub MnuAnROrdenServicio_Click(sender As Object, e As EventArgs) Handles MnuAnROrdenServicio.Click
        Dim formAnulaRecOrdenServicio As New FrmGestionAbonoOrdenServicio With {
            .MdiParent = Me
        }
        formAnulaRecOrdenServicio.Show()
    End Sub

    Private Sub MnuBCMov_Click(sender As Object, e As EventArgs) Handles MnuBCMov.Click
        Dim formMovimientoBanco As New FrmMovimientoBanco With {
            .MdiParent = Me
        }
        formMovimientoBanco.Show()
    End Sub

    Private Sub MnuContaAsiento_Click(sender As Object, e As EventArgs) Handles MnuContaAsiento.Click
        Dim formAsientoDiario As New FrmAsientoContable With {
            .MdiParent = Me
        }
        formAsientoDiario.Show()
    End Sub

    Private Sub MnuContaCierre_Click(sender As Object, e As EventArgs) Handles MnuContaCierre.Click
        Dim formcierreContable As New FrmCierreContable With {
            .MdiParent = Me
        }
        formcierreContable.Show()
    End Sub

    Private Sub MnuContaReporte_Click(sender As Object, e As EventArgs) Handles MnuContaReporte.Click
        Dim formMenuReportesContabilidad As New FrmMenuReportesContables With {
            .MdiParent = Me
        }
        formMenuReportesContabilidad.Show()
    End Sub

    Private Sub MnuDocElectFC_Click(sender As Object, e As EventArgs) Handles MnuDocElectFC.Click
        Dim formFacturaCompra As New FrmFacturaCompra With {
            .MdiParent = Me
        }
        formFacturaCompra.Show()
    End Sub

    Private Sub MnuDocElectCDE_Click(sender As Object, e As EventArgs) Handles MnuDocElectCDE.Click
        Dim formEstadoDocumentoElectronico As New FrmEstadoDocumentoElectronico With {
            .MdiParent = Me
        }
        formEstadoDocumentoElectronico.Show()
    End Sub

    Private Sub MnuDocElectRDE_Click(sender As Object, e As EventArgs) Handles MnuDocElectRDE.Click
        Dim formDetalleDocumentoElectronico As New FrmDetalleDocumentoElectronico With {
            .MdiParent = Me
        }
        formDetalleDocumentoElectronico.Show()
    End Sub

    Private Sub MnuCompraRegistro_Click(sender As Object, e As EventArgs) Handles MnuCompraRegistro.Click
        Dim formCompra As New FrmCompra With {
            .MdiParent = Me
        }
        formCompra.Show()
    End Sub

    Private Sub MnuCompraTraslado_Click(sender As Object, e As EventArgs) Handles MnuCompraTraslado.Click
        Dim formTraslado As New FrmTrasladoMercaderia With {
           .MdiParent = Me
       }
        formTraslado.Show()
    End Sub

    Private Sub MnuCompraAplicTraslado_Click(sender As Object, e As EventArgs) Handles MnuCompraAplicTraslado.Click
        Dim formAplicaTraslado As New FrmAplicaTrasladoListado With {
           .MdiParent = Me
       }
        formAplicaTraslado.Show()
    End Sub

    Private Sub MnuCompraAjusteInv_Click(sender As Object, e As EventArgs) Handles MnuCompraAjusteInv.Click
        Dim formAjusteInventario As New FrmAjusteInventario With {
            .MdiParent = Me
        }
        formAjusteInventario.Show()
    End Sub

    Private Sub MnuArchivoIngreso_Click(sender As Object, e As EventArgs) Handles MnuArchivoIngreso.Click
        Dim formIngreso As New FrmIngreso With {
            .MdiParent = Me
        }
        formIngreso.Show()
    End Sub

    Private Sub MnuArchivoEgreso_Click(sender As Object, e As EventArgs) Handles MnuArchivoEgreso.Click
        Dim formEgreso As New FrmEgreso With {
            .MdiParent = Me
        }
        formEgreso.Show()
    End Sub

    Private Sub MnuDocElectADE_Click(sender As Object, e As EventArgs) Handles MnuDocElectADE.Click
        Dim formRecepcion As New FrmAceptarDocumentoElectronico With {
            .MdiParent = Me
        }
        formRecepcion.Show()
    End Sub
#End Region

#Region "Eventos Formulario"
    Private Async Sub FrmPrincipal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        picLoader.Top = Me.Height / 2 - (picLoader.Height / 2)
        picLoader.Left = Me.Width / 2 - (picLoader.Width / 2)
        Try
            appSettings = ConfigurationManager.AppSettings
        Catch ex As Exception
            MessageBox.Show("Error al cargar el archivo de configuración del sistema. Por favor contacte con su proveedor del servicio. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            If appSettings.Get("Administrator") IsNot Nothing Then bolEsAdministrador = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Dim arrVersionActualApp As String = My.Application.Info.Version.ToString()
        Dim strUltimaVersionApp As String = ""
        picLoader.Visible = True
        Try
            strUltimaVersionApp = Await Puntoventa.ObtenerUltimaVersionApp()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End Try
        If RequiereActualizacion(arrVersionActualApp, strUltimaVersionApp) Then
            picLoader.Visible = False
            Dim formDescarga As New FrmDescargaActualizacion()
            formDescarga.ShowDialog()
            If bolDescargaCancelada Then
                MessageBox.Show("Actualización cancelada por el usuario. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Application.Exit()
            End If
        End If
        Dim strIdentificadoEquipoLocal = Puntoventa.ObtenerIdentificadorEquipo()
        If bolEsAdministrador Then
            listaEmpresas = Await Puntoventa.ObtenerListadoEmpresasAdministrador()
        Else
            Do
                Try
                    listaEmpresas = Await Puntoventa.ObtenerListadoEmpresasPorTerminal(strIdentificadoEquipoLocal)
                Catch ex As Exception
                    MessageBox.Show("No fue posible acceder al servicio web. Consulte con su proveedor del servicio.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                    Close()
                    Exit Sub
                End Try
                If listaEmpresas.Count = 0 Then
                    If MessageBox.Show("El equipo no se encuentra registrado. Desea proceder con el registro?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                        Dim formRegistro As New FrmRegistro()
                        formRegistro.ShowDialog()
                        If bolSalir Then
                            Close()
                            Exit Sub
                        End If
                    Else
                        Close()
                        Exit Sub
                    End If
                End If
            Loop While listaEmpresas.Count = 0
        End If
        picLoader.Visible = False
        Dim formSeguridad As New FrmSeguridad()
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("es-CR")
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "₡"
        Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        Dim empresa As Empresa = Nothing
        Do
            formSeguridad.ShowDialog()
            If bolSalir Then
                Close()
                Exit Sub
            Else
                picLoader.Visible = True
                Try
                    Dim strEncryptedPassword As String = Encriptador.EncriptarDatos(strContrasena)
                    empresa = Await Puntoventa.ValidarCredenciales(strCodigoUsuario, strEncryptedPassword, strIdEmpresa, strIdentificadoEquipoLocal)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Loop While empresa Is Nothing
        stsPrincipal.Items("tslUsuario").Text = "Usuario: " & empresa.Usuario.CodigoUsuario & "   Sucursal: " & empresa.EquipoRegistrado.NombreSucursal
        usuarioGlobal = empresa.Usuario
        empresaGlobal = empresa
        equipoGlobal = empresa.EquipoRegistrado
        decTipoCambioDolar = Await Puntoventa.ObtenerTipoCambioDolar(usuarioGlobal.Token)
        listaTipoIdentificacion = empresa.ListadoTipoIdentificacion
        listaFormaPagoCliente = empresa.ListadoFormaPagoCliente
        listaFormaPagoEmpresa = empresa.ListadoFormaPagoEmpresa
        listaTipoProducto = empresa.ListadoTipoProducto
        listaTipoImpuesto = empresa.ListadoTipoImpuesto
        listaTipoMoneda = empresa.ListadoTipoMoneda
        listaCondicionVenta = empresa.ListadoCondicionVenta
        listaTipoExoneracion = empresa.ListadoTipoExoneracion
        listaNombreInstExoneracion = empresa.ListadoNombreInstExoneracion
        listaTipoPrecio = empresa.ListadoTipoPrecio
        listaSucursales = New List(Of LlaveDescripcion)
        For Each sucursal As SucursalPorEmpresa In empresa.SucursalPorEmpresa
            listaSucursales.Add(New LlaveDescripcion(sucursal.IdSucursal, sucursal.NombreSucursal))
        Next
        listaActividadEconomica = New List(Of LlaveDescripcion)
        For Each actividad As ActividadEconomicaEmpresa In empresa.ActividadEconomicaEmpresa
            listaActividadEconomica.Add(New LlaveDescripcion(actividad.CodigoActividad, actividad.Descripcion))
        Next
        picLoader.Visible = False
        Dim formInicio As New FrmInicio()
        Dim bolAdministrador = usuarioGlobal.CodigoUsuario = "ADMIN"
        formInicio.ShowDialog()
        If empresaGlobal.AutoCompletaProducto Then
            picLoader.Visible = True
            Dim intTotalRegistros As Integer = Await Puntoventa.ObtenerTotalListaProductos(empresaGlobal.IdEmpresa, equipoGlobal.IdSucursal, True, True, False, False, 0, "", "", "", usuarioGlobal.Token)
            If intTotalRegistros > 0 Then
                Dim intCantPaginas As Integer = Math.Ceiling(intTotalRegistros / 1000)
                For intPagina As Integer = 1 To intCantPaginas
                    Dim listado As List(Of ProductoDetalle) = Await Puntoventa.ObtenerListadoProductos(empresaGlobal.IdEmpresa, equipoGlobal.IdSucursal, intPagina, 1000, True, True, False, False, 0, "", "", "", usuarioGlobal.Token)
                    listaProductos.AddRange(listado)
                Next
            End If
            picLoader.Visible = False
        End If
        mnuMenuPrincipal.Visible = True
        For Each reportePorEmpresa As ReportePorEmpresa In empresaGlobal.ReportePorEmpresa.OrderBy(Function(obj) obj.IdReporte)
            lstListaReportes.Add(reportePorEmpresa.CatalogoReporte.NombreReporte)
        Next
        If bolAdministrador Then
            bolSeleccionaSucursal = True
            bolAnularTransacciones = True
            bolModificaDescripcion = True
            bolModificaCliente = True
            bolModificaPrecioVenta = True
            For Each item As ToolStripMenuItem In mnuMenuPrincipal.Items
                item.Visible = True
                For Each subItem As ToolStripItem In item.DropDownItems
                    subItem.Visible = True
                Next
            Next
        Else
            Dim credenciales As CredencialesHacienda = Await Puntoventa.ObtenerCredencialesHacienda(empresa.IdEmpresa, usuarioGlobal.Token)
            If Not ValidarEmpresa(empresa, credenciales) Then
                If usuarioGlobal.RolePorUsuario.Where(Function(s) s.IdRole = 61).Count > 0 Then
                    MessageBox.Show("La información de la empresa requiere ser actualizada. Por favor ingrese al mantenimiento de Empresa para completar la información.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Try
                        objMenu = mnuMenuPrincipal.Items("MnuParam")
                        objMenu.Visible = True
                        objMenu.DropDownItems("MnuParamEmpresa").Visible = True
                    Catch ex As Exception
                    End Try
                Else
                    MessageBox.Show("La información de la empresa requiere ser actualizada por un usuario administrador.", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Close()
                    Exit Sub
                End If
            Else
                If usuarioGlobal.RolePorUsuario.Count > 0 Then
                    For Each permiso As RolePorUsuario In usuarioGlobal.RolePorUsuario
                        Try
                            If permiso.IdRole = 48 Then bolSeleccionaSucursal = True
                            If permiso.IdRole = 49 Then bolAnularTransacciones = True
                            If permiso.IdRole = 50 Then bolModificaDescripcion = True
                            If permiso.IdRole = 51 Then bolModificaCliente = True
                            If permiso.IdRole = 52 Then bolModificaPrecioVenta = True
                            If permiso.Role.MenuPadre <> "" Then
                                objMenu = mnuMenuPrincipal.Items(permiso.Role.MenuPadre)
                                objMenu.Visible = True
                                objMenu.DropDownItems(permiso.Role.MenuItem).Visible = True
                            End If
                        Catch ex As Exception
                        End Try
                    Next
                End If
            End If
        End If
        Try
            productoTranstorio = Await Puntoventa.ObtenerProductoTransitorio(empresaGlobal.IdEmpresa, usuarioGlobal.Token)
        Catch
            productoTranstorio = Nothing
        End Try
        Try
            productoImpuestoServicio = Await Puntoventa.ObtenerProductoImpuestoServicio(empresaGlobal.IdEmpresa, usuarioGlobal.Token)
        Catch
            productoImpuestoServicio = Nothing
        End Try
        dgvDecimal = New DataGridViewCellStyle With {
            .Format = "N2",
            .NullValue = "0",
            .Alignment = DataGridViewContentAlignment.MiddleRight
        }
        dgvInteger = New DataGridViewCellStyle With {
            .Format = "F0",
            .NullValue = "0",
            .Alignment = DataGridViewContentAlignment.MiddleCenter
        }
    End Sub
#End Region
End Class
