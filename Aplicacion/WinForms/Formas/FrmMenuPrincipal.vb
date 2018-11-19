Imports System.IO
Imports System.Threading
Imports System.Configuration
Imports Unity
Imports Unity.Injection
Imports Unity.Lifetime
Imports Microsoft.Practices.Unity.Configuration
Imports LeandroSoftware.Puntoventa.Core
Imports LeandroSoftware.Puntoventa.Dominio.Entidades
Imports LeandroSoftware.Puntoventa.Datos
Imports LeandroSoftware.Puntoventa.Servicios
Imports LeandroSoftware.FacturaElectronicaHacienda.TiposDatos

Public Class FrmMenuPrincipal
#Region "Variables"
    Private driveName, strFechaVence As String
    Private bolEquipoRegistrado As Boolean = False
    Private objMenu As ToolStripMenuItem
    Private appSettings As Specialized.NameValueCollection
    Private servicioRespaldo As IRespaldoService

    Public usuarioGlobal As Usuario
    Public empresaGlobal As Empresa
    Public equipoGlobal As DetalleRegistro
    Public intBusqueda As Integer
    Public strBusqueda As String
    Public dgvDecimal As DataGridViewCellStyle
    Public dgvInteger As DataGridViewCellStyle
    Public unityContainer As IUnityContainer
    Public strAppThumptPrint As String
    Public intSucursal As Integer
    Public intTerminal As Integer
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

    Private Function ValidarCertificado(ByVal sender As Object, ByVal certificate As System.Security.Cryptography.X509Certificates.X509Certificate, ByVal chain As System.Security.Cryptography.X509Certificates.X509Chain, ByVal sslPolicyErrors As System.Net.Security.SslPolicyErrors) As Boolean
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

    Private Sub MnuArchivoRespaldo_Click(sender As Object, e As EventArgs) Handles mnuArchivoRespaldo.Click
        If MessageBox.Show("Desea realizar el respaldo de base de datos?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            Dim applicationKey As String = appSettings("ApplicationKey")
            Dim backupUser As String = Utilitario.DesencriptarDatos(strAppThumptPrint, appSettings("BackupUser"))
            Dim backupPassword As String = Utilitario.DesencriptarDatos(strAppThumptPrint, appSettings("BackupPassword"))
            Dim databaseHost As String = appSettings("DatabaseHost")
            Dim databaseName As String = appSettings("DatabaseName")
            Dim mySQLDumpOptions As String = appSettings("MySQLDumpOptions")
            Dim backupServer As String = appSettings("BackupServer")
            Dim strFileName As String = databaseName + "-" + Now.ToShortDateString().Replace("/", "") + ".sql"
            Dim bytes As Byte()
            Try
                Dim strData As String = servicioRespaldo.GenerarContenidoRespaldo(backupUser, backupPassword, databaseHost, databaseName, mySQLDumpOptions)
                bytes = Utilitario.EncriptarArchivo(strAppThumptPrint, applicationKey, strData)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            If Not empresaGlobal.RespaldoEnLinea Then
                Dim backupFilePath As String = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\" + strFileName
                If File.Exists(backupFilePath) Then
                    If MessageBox.Show("El respaldo ya fue generado para la fecha actual. Desea sobreescribir el archivo de respaldo?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.No Then
                        MessageBox.Show("Proceso de respaldo cancelado por el usuario.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                        Exit Sub
                    End If
                    File.Delete(backupFilePath)
                End If
                Try
                    Dim bw As BinaryWriter = New BinaryWriter(New FileStream(backupFilePath, FileMode.Create))
                    bw.Write(bytes)
                    bw.Close()
                Catch ex As Exception
                    MessageBox.Show("Se produjo un error al generar el archivo respaldo desde la base de datos: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                Try
                    servicioRespaldo = unityContainer.Resolve(Of IRespaldoService)()
                    servicioRespaldo.SubirRespaldo(bytes, strFileName)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            End If
            MessageBox.Show("Respaldo finalizado satisfactoriamente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
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

    Private Sub MnuParamTM_Click(sender As Object, e As EventArgs) Handles MnuParamTM.Click
        Dim formTipoMonedaListado As New FrmTipoMonedaListado With {
            .MdiParent = Me
        }
        formTipoMonedaListado.Show()
    End Sub

    Private Sub MnuParamBA_Click(sender As Object, e As EventArgs) Handles MnuParamBA.Click
        Dim formBancoAdquirienteListado As New FrmBancoAdquirienteListado With {
            .MdiParent = Me
        }
        formBancoAdquirienteListado.Show()
    End Sub

    Public Sub MnuMantCliente_Click(sender As Object, e As EventArgs) Handles MnuMantCliente.Click
        Dim formClienteListado As New FrmClienteListado With {
            .MdiParent = Me
        }
        formClienteListado.Show()
    End Sub

    Private Sub MnuMantParticular_Click(sender As Object, e As EventArgs) Handles MnuMantParticular.Click
        Dim formClienteListado As New FrmParticularListado With {
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

    Private Sub MnuCapturaCxPP_Click(sender As Object, e As EventArgs) Handles MnuCapturaCxPP.Click
        Dim formCuentasPorPagarParticulares As New FrmCuentaPorPagarParticulares With {
            .MdiParent = Me
        }
        formCuentasPorPagarParticulares.Show()
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

    Private Sub MnuApRCxPP_Click(sender As Object, e As EventArgs) Handles MnuApRCxPP.Click
        Dim formReciboCxP As New FrmAplicaReciboCxPParticular With {
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

    Public Sub MnuAnRCxPP_Click(sender As Object, e As EventArgs) Handles MnuAnRCxPP.Click
        Dim formAnulaRecCxP As New FrmAnulaReciboCxPParticular With {
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

    Public Sub MnuImpCxPP_Click(sender As Object, e As EventArgs) Handles MnuImpCxPP.Click
        Dim formImprimirRecCxP As New FrmImprimirReciboCxPParticular With {
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
#End Region

#Region "Eventos Formulario"
    Private Async Sub FrmMenuPrincipal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Dim hostname As String
        Dim database As String
        Dim username As String
        Dim password As String
        picLoader.Top = Me.Height / 2 - (picLoader.Height / 2)
        picLoader.Left = Me.Width / 2 - (picLoader.Width / 2)
        Try
            appSettings = ConfigurationManager.AppSettings
        Catch ex As Exception
            MessageBox.Show("Error al cargar el archivo de configuración del sistema de activación: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            Dim key As String = appSettings.Get("SubjectName")
            hostname = appSettings.Get("DatabaseHost")
            database = appSettings.Get("DatabaseName")
            username = appSettings.Get("LoginUser")
            password = appSettings.Get("LoginPassword")
            intSucursal = Integer.Parse(appSettings.Get("Sucursal"))
            intTerminal = Integer.Parse(appSettings.Get("Caja"))
            strAppThumptPrint = Utilitario.VerificarCertificadoPorNombre(key)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        Try
            Dim strConn As String = "Server=" + hostname + "; Database=" + database + "; UId=" + Utilitario.DesencriptarDatos(strAppThumptPrint, username) + "; Pwd=" + Utilitario.DesencriptarDatos(strAppThumptPrint, password) + ";"
            unityContainer = New UnityContainer()
            Dim section As UnityConfigurationSection = ConfigurationManager.GetSection("unity")
            section.Configure(unityContainer, "Service")
            unityContainer.RegisterInstance(GetType(String), "conectionString", strConn, New ContainerControlledLifetimeManager())
            unityContainer.RegisterType(Of IDbContext, LeandroContext)(New InjectionConstructor(New ResolvedParameter(GetType(String), "conectionString")))
            servicioRespaldo = unityContainer.Resolve(Of IRespaldoService)()
        Catch ex As Exception
            MessageBox.Show("Error al iniciar el menu principal: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try

        Dim formSeguridad As New FrmSeguridad()
        Dim fileSystemObject, drive As Object
        Thread.CurrentThread.CurrentCulture = New Globalization.CultureInfo("es-CR")
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencySymbol = "¢"
        Thread.CurrentThread.CurrentCulture.NumberFormat.NumberDecimalSeparator = "."
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyDecimalSeparator = "."
        Thread.CurrentThread.CurrentCulture.NumberFormat.NumberGroupSeparator = ","
        Thread.CurrentThread.CurrentCulture.NumberFormat.CurrencyGroupSeparator = ","
        formSeguridad.ShowDialog()
        If usuarioGlobal Is Nothing Then
            Close()
            Exit Sub
        End If

        fileSystemObject = CreateObject("Scripting.FileSystemObject")
        driveName = "C:\"
        drive = fileSystemObject.GetDrive(fileSystemObject.GetDriveName(fileSystemObject.GetAbsolutePathName(driveName)))
        For Each detalleEmpresa As DetalleRegistro In empresaGlobal.DetalleRegistro
            If detalleEmpresa.ValorRegistro = drive.SerialNumber Then
                bolEquipoRegistrado = True
                equipoGlobal = detalleEmpresa
                Exit For
            End If
        Next
        If Not bolEquipoRegistrado Then
            MessageBox.Show("Equipo no registrado para la empresa seleccionada. Contacte a su Proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End If
        If empresaGlobal.FechaVence IsNot Nothing Then
            If Today > empresaGlobal.FechaVence Then
                MessageBox.Show("Ha Expirado el período de Prueba del Producto. Contacte a su Proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
        picLoader.Visible = True
        Try
            Dim empresaDTO As EmpresaDTO = Await ComprobanteElectronicoService.ConsultarEmpresa(empresaGlobal)
            If empresaDTO.PermiteFacturar = "N" Then
                MessageBox.Show("La empresa no se encuentra activa para emitir documentos electrónicos. Contacte a su Proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            Else
                picLoader.Visible = False
                Dim formInicio As New FrmInicio()
                formInicio.ShowDialog()
                mnuMenuPrincipal.Visible = True
                For Each permiso As RolePorUsuario In usuarioGlobal.RolePorUsuario
                    objMenu = mnuMenuPrincipal.Items(permiso.Role.MenuPadre)
                    Try
                        objMenu.DropDownItems(permiso.Role.MenuItem).Visible = True
                    Catch ex As Exception
                    End Try
                Next
                If empresaGlobal.FacturaElectronica Then
                    objMenu = mnuMenuPrincipal.Items("MnuDocElect")
                    objMenu.DropDownItems("MnuDocElectCDE").Visible = True
                    objMenu.DropDownItems("MnuDocElectADE").Visible = True
                End If
            End If
        Catch
            MessageBox.Show("Error de conexión con el servicio web de factura electrónica. Por verifique su conexión a internet e intente más tarde. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End Try
    End Sub
#End Region
End Class
