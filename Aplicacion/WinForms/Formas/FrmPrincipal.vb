Imports System.Threading
Imports System.Configuration
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Collections.Generic
Imports System.IO
Imports System.Linq
Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Core.Utilities
Imports LeandroSoftware.Core.TiposComunes

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
    Public strThumbprint As String
    Public strApplicationKey As String
    Public lstListaReportes As New List(Of String)
    Public listaEmpresa As New List(Of LlaveDescripcion)
    Public strKey As String
    Public decTipoCambioDolar As Decimal
    Public strCodigoUsuario As String
    Public strContrasena As String
    Public strIdEmpresa As String
    Public bolSalir As Boolean = False
#End Region

#Region "Métodos"
    Public Function ObtenerFechaFormateada(fecha As Date) As Date
        Return FormatDateTime(fecha, DateFormat.ShortDate)
    End Function

    Public Sub ValidaNumero(ByVal e As KeyPressEventArgs, ByVal oText As TextBox, Optional ByVal pbConPuntoDec As Boolean = True, Optional ByVal pnNumDecimal As Integer = 2, Optional ByVal psSimbolo As String = ".")
        Dim nDig As Integer
        Dim sTexto As String = String.Concat(oText.Text, e.KeyChar)
        If Asc(e.KeyChar) = Keys.Back Or Asc(e.KeyChar) = Keys.Return Then
            e.Handled = False
            Exit Sub
        End If
        If pbConPuntoDec Then
            If Char.IsDigit(e.KeyChar) Or e.KeyChar = psSimbolo Then
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
            If Char.IsDigit(e.KeyChar) Then
                e.Handled = False
            ElseIf Char.IsControl(e.KeyChar) Then
                e.Handled = False
            Else
                e.Handled = True
            End If
        End If
    End Sub

    Public Function ValidarEmpresa(empresa As Empresa)
        If empresa.NombreEmpresa.Length = 0 Or
            empresa.IdTipoIdentificacion < 0 Or
            empresa.Identificacion.Length = 0 Or
            empresa.IdProvincia < 0 Or
            empresa.IdCanton < 0 Or
            empresa.IdDistrito < 0 Or
            empresa.IdBarrio < 0 Or
            empresa.Direccion.Length = 0 Or
            empresa.Telefono.Length = 0 Or
            empresa.CorreoNotificacion.Length = 0 Or
            empresa.EquipoRegistrado.NombreSucursal.Length = 0 Or
            empresa.EquipoRegistrado.DireccionSucursal.Length = 0 Or
            empresa.EquipoRegistrado.TelefonoSucursal.Length = 0 Then
            Return False
        End If
        If Not empresa.RegimenSimplificado Then
            If empresa.NombreCertificado.Length = 0 Or
                empresa.PinCertificado.Length = 0 Or
                empresa.UsuarioHacienda.Length = 0 Or
                empresa.ClaveHacienda.Length = 0 Then
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
        If MessageBox.Show("Desea salir del sistema", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then Close()
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

    Private Sub ManuMantEmpresa_Click(sender As Object, e As EventArgs) Handles ManuMantEmpresa.Click
        Dim formEmpresa As New FrmEmpresa
        formEmpresa.ShowDialog()
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

    Public Sub MnuMantSucursal_Click(sender As Object, e As EventArgs) Handles MnuMantSucursal.Click
        Dim formSucursalListado As New FrmSucursalListado With {
            .MdiParent = Me
        }
        formSucursalListado.Show()
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

    Private Sub MnuMantCB_Click(sender As Object, e As EventArgs) Handles MnuMantCB.Click
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

    Public Sub MnuCapturaProforma_Click(sender As Object, e As EventArgs) Handles MnuCapturaProforma.Click
        Dim formProforma As New FrmProforma With {
            .MdiParent = Me
        }
        formProforma.Show()
    End Sub

    Public Sub MnuCapturaCompra_Click(sender As Object, e As EventArgs) Handles MnuCapturaCompra.Click
        Dim formCompra As New FrmCompra With {
            .MdiParent = Me
        }
        formCompra.Show()
    End Sub

    Public Sub MnuCapturaOrden_Click(sender As Object, e As EventArgs) Handles MnuCapturaOrden.Click
        Dim formOrdenCompra As New FrmOrdenCompra With {
            .MdiParent = Me
        }
        formOrdenCompra.Show()
    End Sub

    Private Sub MnuCapturaDevolucionProveedor_Click(sender As Object, e As EventArgs) Handles MnuDevolucionProveedor.Click
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

    Private Sub MnuCapturaTraslado_Click(sender As Object, e As EventArgs) Handles MnuCapturaTraslado.Click
        Dim formTraslado As New FrmTrasladoMercaderia With {
            .MdiParent = Me
        }
        formTraslado.Show()
    End Sub

    Private Sub MnuCapturaIngreso_Click(sender As Object, e As EventArgs) Handles MnuCapturaIngreso.Click
        Dim formIngreso As New FrmIngreso With {
            .MdiParent = Me
        }
        formIngreso.Show()
    End Sub

    Private Sub MnuCapturaEgreso_Click(sender As Object, e As EventArgs) Handles MnuCapturaEgreso.Click
        Dim formEgreso As New FrmEgreso With {
            .MdiParent = Me
        }
        formEgreso.Show()
    End Sub

    Private Sub MnuCapturaAI_Click(sender As Object, e As EventArgs) Handles MnuCapturaAI.Click
        Dim formAjusteInventario As New FrmAjusteInventario With {
            .MdiParent = Me
        }
        formAjusteInventario.Show()
    End Sub

    Public Sub MnuApRCxC_Click(sender As Object, e As EventArgs) Handles MnuApRCxC.Click
        Dim formReciboCxC As New FrmAplicaReciboCxC With {
            .MdiParent = Me
        }
        formReciboCxC.Show()
    End Sub

    Public Sub MnuApRCxP_Click(sender As Object, e As EventArgs) Handles MnuApRCxP.Click
        Dim formReciboCxP As New FrmAplicaReciboCxP With {
            .MdiParent = Me
        }
        formReciboCxP.Show()
    End Sub

    Public Sub MnuAnRCxC_Click(sender As Object, e As EventArgs) Handles MnuAnRCxC.Click
        Dim formAnulaRecCxC As New FrmAnulaReciboCxC With {
            .MdiParent = Me
        }
        formAnulaRecCxC.Show()
    End Sub

    Public Sub MnuAnRCxP_Click(sender As Object, e As EventArgs) Handles MnuAnRCxP.Click
        Dim formAnulaRecCxP As New FrmAnulaReciboCxP With {
            .MdiParent = Me
        }
        formAnulaRecCxP.Show()
    End Sub

    Public Sub MnuImpCxC_Click(sender As Object, e As EventArgs) Handles MnuImpCxC.Click
        Dim formImprimirRecCxC As New FrmImprimirReciboCxC With {
            .MdiParent = Me
        }
        formImprimirRecCxC.Show()
    End Sub

    Public Sub MnuImpCxP_Click(sender As Object, e As EventArgs) Handles MnuImpCxP.Click
        Dim formImprimirRecCxP As New FrmImprimirReciboCxP With {
            .MdiParent = Me
        }
        formImprimirRecCxP.Show()
    End Sub

    Private Sub MnuBCMov_Click(sender As Object, e As EventArgs) Handles MnuBCMov.Click
        Dim formMovimientoBanco As New FrmMovimientoBanco With {
            .MdiParent = Me
        }
        formMovimientoBanco.Show()
    End Sub

    Private Sub MnuContaAsiento_Click(sender As Object, e As EventArgs) Handles mnuContaAsiento.Click
        Dim formAsientoDiario As New FrmAsientoContable With {
            .MdiParent = Me
        }
        formAsientoDiario.Show()
    End Sub

    Private Sub MnuContaCierre_Click(sender As Object, e As EventArgs) Handles mnuContaCierre.Click
        Dim formcierreContable As New FrmCierreContable With {
            .MdiParent = Me
        }
        formcierreContable.Show()
    End Sub

    Private Sub MnuContaReporte_Click(sender As Object, e As EventArgs) Handles mnuContaReporte.Click
        Dim formMenuReportesContabilidad As New FrmMenuReportesContables With {
            .MdiParent = Me
        }
        formMenuReportesContabilidad.Show()
    End Sub

    Private Sub MnuDocElectADE_Click(sender As Object, e As EventArgs) Handles MnuDocElectADE.Click
        Dim formMenuAceptarComprobanteElectronico As New FrmAceptarDocumentoElectronico With {
            .MdiParent = Me
        }
        formMenuAceptarComprobanteElectronico.Show()
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
#End Region

#Region "Eventos Formulario"
    Private Async Sub FrmPrincipal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        picLoader.Top = Me.Height / 2 - (picLoader.Height / 2)
        picLoader.Left = Me.Width / 2 - (picLoader.Width / 2)
        Try
            appSettings = ConfigurationManager.AppSettings
        Catch ex As Exception
            MessageBox.Show("Error al cargar el archivo de configuración del sistema de activación. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            strThumbprint = appSettings.Get("AppThumptprint")
            strApplicationKey = appSettings.Get("ApplicationKey")
            If appSettings.Get("Administrator") IsNot Nothing Then bolEsAdministrador = appSettings.Get("Administrator")
            Dim bolCertificadoValido As Boolean = Utilitario.VerificarCertificado(strThumbprint)
            If Not bolCertificadoValido Then
                MessageBox.Show("No se logró validar el certificado requerido por la aplicación. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End If
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            strKey = Utilitario.ObtenerLlaveEncriptadoLocal(strThumbprint, strApplicationKey)
        Catch ex As Exception
            MessageBox.Show("Error al validar la llave criptográfica de la aplicación. Consulte con su proveedor del servicio.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End Try
        Dim strVersionActualApp = My.Application.Info.Version.ToString()
        Dim strUltimaVersionApp As String
        Try
            strUltimaVersionApp = Await Puntoventa.ObtenerUltimaVersionApp()
        Catch ex As Exception
            MessageBox.Show("No fue posible acceder al servicio web de facturación electrónica. Consulte con su proveedor del servicio.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End Try
        If strVersionActualApp <> strUltimaVersionApp Then
            If MessageBox.Show("El sistema requiere una actualización para continuar. Desea proceder con la instalación de la actualización?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                If File.Exists(Path.GetTempPath() + "/Updater.exe") Then
                    File.Delete(Path.GetTempPath() + "/Updater.exe")
                    File.Delete(Path.GetTempPath() + "/Updater.exe.config")
                End If
                Dim procStartInfo As New ProcessStartInfo
                Dim procExecuting As New Process
                File.Copy(Application.StartupPath() + "/Updater.exe", Path.GetTempPath() + "/Updater.exe")
                File.Copy(Application.StartupPath() + "/Updater.exe.config", Path.GetTempPath() + "/Updater.exe.config")
                With procStartInfo
                    .UseShellExecute = True
                    .FileName = Path.GetTempPath() + "/Updater.exe"
                    .WindowStyle = ProcessWindowStyle.Normal
                End With
                procExecuting = Process.Start(procStartInfo)
                Close()
                Exit Sub
            Else
                Close()
                Exit Sub
            End If
        Else
            If File.Exists(Path.GetTempPath() + "/Updater.exe") Then
                File.Delete(Path.GetTempPath() + "/Updater.exe")
                File.Delete(Path.GetTempPath() + "/Updater.exe.config")
            End If
        End If
        Dim strIdentificadoEquipoLocal = Utilitario.ObtenerIdentificadorEquipo()
        If bolEsAdministrador Then
            listaEmpresa = Await Puntoventa.ObtenerListadoEmpresasAdministrador()
        Else
            Do
                listaEmpresa = Await Puntoventa.ObtenerListadoEmpresasPorTerminal(strIdentificadoEquipoLocal)
                If listaEmpresa.Count = 0 Then
                    If MessageBox.Show("El equipo no se encuentra registrado. Desea proceder con el registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
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
            Loop While listaEmpresa.Count = 0
        End If
        Dim formSeguridad As New FrmSeguridad()
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("es-CR")
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "¢"
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
                Try
                    Dim strEncryptedPassword As String = Utilitario.EncriptarDatos(strContrasena, strKey)
                    empresa = Await Puntoventa.ValidarCredenciales(strCodigoUsuario, strEncryptedPassword, strIdEmpresa, strIdentificadoEquipoLocal)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                End Try
            End If
        Loop While empresa Is Nothing
        usuarioGlobal = empresa.Usuario
        empresaGlobal = empresa
        equipoGlobal = empresa.EquipoRegistrado
        decTipoCambioDolar = Await Puntoventa.ObtenerTipoCambioDolar(usuarioGlobal.Token)
        picLoader.Visible = False
        Dim formInicio As New FrmInicio()
        formInicio.ShowDialog()
        mnuMenuPrincipal.Visible = True
        If ValidarEmpresa(empresa) Then
            Try
                For Each reportePorEmpresa As ReportePorEmpresa In empresaGlobal.ReportePorEmpresa.OrderBy(Function(obj) obj.IdReporte)
                    lstListaReportes.Add(reportePorEmpresa.CatalogoReporte.NombreReporte)
                Next
                For Each permiso As RolePorUsuario In usuarioGlobal.RolePorUsuario
                    objMenu = mnuMenuPrincipal.Items(permiso.Role.MenuPadre)
                    objMenu.Visible = True
                    objMenu.DropDownItems(permiso.Role.MenuItem).Visible = True
                Next
            Catch ex As Exception
            End Try
        Else
            If usuarioGlobal.Modifica Then
                MessageBox.Show("La información de la empresa requiere ser actualizada. Por favor ingrese al mantenimiento de Empresa para completar la información.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Try
                    mnuMenuPrincipal.Items("MnuMant").Visible = True
                    objMenu.DropDownItems("ManuMantEmpresa").Visible = True
                Catch ex As Exception
                End Try
            Else
                MessageBox.Show("La información de la empresa requiere ser actualizada por un usuario administrador.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
                Exit Sub
            End If
        End If
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
