Imports System.Threading
Imports System.Configuration
Imports LeandroSoftware.Core
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports System.Collections.Generic
Imports LeandroSoftware.AccesoDatos.TiposDatos

Public Class FrmMenuPrincipal
#Region "Variables"
    Private driveName, strFechaVence As String
    Private bolEquipoRegistrado As Boolean = False
    Private objMenu As ToolStripMenuItem
    Private appSettings As Specialized.NameValueCollection
    Public usuarioGlobal As Usuario
    Public empresaGlobal As Empresa
    Public equipoGlobal As DetalleRegistro
    Public intBusqueda As Integer
    Public strBusqueda As String
    Public dgvDecimal As DataGridViewCellStyle
    Public dgvInteger As DataGridViewCellStyle
    Public strThumbprint As String
    Public strIdentificacion As String
    Public intSucursal As Integer
    Public intTerminal As Integer
    Public lstListaReportes As New List(Of String)
    Public strServicioPuntoventaURL As String
    Public decTipoCambioDolar As Decimal
    Public datosConfig As DatosConfiguracion
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

    Private Sub MnuDocElectRDE_Click(sender As Object, e As EventArgs) Handles MnuDocElectRDE.Click
        Dim formDetalleDocumentoElectronico As New FrmDetalleDocumentoElectronico With {
            .MdiParent = Me
        }
        formDetalleDocumentoElectronico.Show()
    End Sub
#End Region

#Region "Eventos Formulario"
    Private Async Sub FrmMenuPrincipal_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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
            strIdentificacion = appSettings.Get("Identificacion")
            intSucursal = Integer.Parse(appSettings.Get("Sucursal"))
            intTerminal = Integer.Parse(appSettings.Get("Caja"))
            strServicioPuntoventaURL = appSettings.Get("ServicioPuntoventaURL")
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

        Dim formSeguridad As New FrmSeguridad()
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
        If empresaGlobal Is Nothing Then
            MessageBox.Show("La empresa no se encuentra registrada en el servicio de facturación electrónica. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End If
        If Not empresaGlobal.RegimenSimplificado Then
            If empresaGlobal.Certificado Is Nothing Then
                MessageBox.Show("La empresa no posee la llave criptográfica requerida. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            If empresaGlobal.PinCertificado = "" Then
                MessageBox.Show("La empresa no posee el parámetro PIN de la llave criptográfica. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
            If Not empresaGlobal.PermiteFacturar Then
                MessageBox.Show("La empresa no se encuentra activa para emitir documentos electrónicos. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
        End If
        If Today > empresaGlobal.FechaVence Then
            Dim strMensajeExpirado = ""
            If Not empresaGlobal.RegimenSimplificado Then
                strMensajeExpirado = "El período del plan de factura electrónica adquirido ha expirado. Por favor contacte con su proveedor del servicio. . ."
            Else
                strMensajeExpirado = "El período del plan del servicio del sistema de punto de venta ha expirado. Por favor contacte con su proveedor del servicio. . ."
            End If
            MessageBox.Show(strMensajeExpirado, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End If
        Dim strSerial As String = Utilitario.ObtenerSerialEquipo()
        For Each detalleEmpresa As DetalleRegistro In empresaGlobal.DetalleRegistro
            If detalleEmpresa.ValorRegistro = strSerial Then
                bolEquipoRegistrado = True
                equipoGlobal = detalleEmpresa
                Exit For
            End If
        Next
        If Not bolEquipoRegistrado Then
            MessageBox.Show("Equipo no registrado para la empresa seleccionada. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            Close()
            Exit Sub
        End If
        If empresaGlobal.FechaVence IsNot Nothing Then
            If Today > empresaGlobal.FechaVence Then
                MessageBox.Show("Ha Expirado el período de Prueba del Producto. Por favor contacte con su proveedor del servicio. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
                Close()
                Exit Sub
            End If
        End If
        For Each moduloPorEmpresa As ModuloPorEmpresa In empresaGlobal.ModuloPorEmpresa
            objMenu = mnuMenuPrincipal.Items(moduloPorEmpresa.Modulo.MenuPadre)
            objMenu.Visible = True
        Next
        For Each reportePorEmpresa As ReportePorEmpresa In empresaGlobal.ReportePorEmpresa
            lstListaReportes.Add(reportePorEmpresa.CatalogoReporte.NombreReporte)
        Next
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
        decTipoCambioDolar = Await ClienteWCF.ObtenerTipoCambioDolar()
        picLoader.Visible = False
        Dim formInicio As New FrmInicio()
        formInicio.ShowDialog()
        mnuMenuPrincipal.Visible = True
        Try
            If Not empresaGlobal.RegimenSimplificado Then
                objMenu = mnuMenuPrincipal.Items("MnuDocElect")
                objMenu.Visible = True
            End If
            For Each permiso As RolePorUsuario In usuarioGlobal.RolePorUsuario
                objMenu = mnuMenuPrincipal.Items(permiso.Role.MenuPadre)
                objMenu.DropDownItems(permiso.Role.MenuItem).Visible = True
            Next
        Catch ex As Exception
        End Try
    End Sub
#End Region
End Class
