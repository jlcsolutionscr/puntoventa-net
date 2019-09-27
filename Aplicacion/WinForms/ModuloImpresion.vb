Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Drawing.Printing

Public Class ModuloImpresion
#Region "Variables"
    Public Class ClsEgreso
        Public empresa As Empresa
        Public equipo As TerminalPorEmpresa
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strMonto As String
        Public strBeneficiario As String
        Public strConcepto As String
        Public strFormaPago As String
        Public strBanco As String
        Public strNroCheque As String
        Public arrDesglosePago As IList(Of clsDesgloseFormaPago)
    End Class

    Public Class ClsIngreso
        Public empresa As Empresa
        Public equipo As TerminalPorEmpresa
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strMonto As String
        Public strRecibidoDe As String
        Public strConcepto As String
        Public strFormaPago As String
        Public strBanco As String
        Public strNroMovimiento As String
        Public arrDesglosePago As IList(Of clsDesgloseFormaPago)
    End Class

    Public Class ClsCuentaPorPagar
        Public empresa As Empresa
        Public equipo As TerminalPorEmpresa
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strMonto As String
        Public strRecibidoDe As String
        Public strConcepto As String
        Public strFormaPago As String
        Public strBanco As String
        Public strNroMovimiento As String
        Public arrDesglosePago As IList(Of clsDesgloseFormaPago)
    End Class

    Public Class ClsRecibo
        Public empresa As Empresa
        Public equipo As TerminalPorEmpresa
        Public usuario As Usuario
        Public strConsecutivo As String
        Public strRecibo As String
        Public strNombre As String
        Public strFechaAbono As String
        Public strTotalAbono As String
        Public arrDesgloseMov As IList(Of clsDesgloseFormaPago)
        Public arrDesglosePago As IList(Of clsDesgloseFormaPago)
    End Class

    Public Class ClsAjusteInventario
        Public empresa As Empresa
        Public equipo As TerminalPorEmpresa
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strDescripcion As String
        Public arrDetalleComprobante As IList(Of clsDetalleComprobante)
    End Class

    Public Class ClsComprobante
        Public empresa As Empresa
        Public equipo As TerminalPorEmpresa
        Public usuario As Usuario
        Public strVendedor As String
        Public intCliente As Integer
        Public strId As String
        Public strNombre As String
        Public strDocumento As String
        Public strFormaPago As String
        Public strEnviadoPor As String
        Public strFecha As String
        Public strSubTotal As String
        Public strDescuento As String
        Public strImpuesto As String
        Public strTotal As String
        Public strPagoCon As String
        Public strCambio As String
        Public strClaveNumerica As String
        Public arrDetalleComprobante As IList(Of clsDetalleComprobante)
        Public arrDesglosePago As IList(Of clsDesgloseFormaPago)
    End Class

    Public Class ClsDetalleComprobante
        Public strDescripcion As String
        Public strCantidad As String
        Public strPrecio As String
        Public strTotalLinea As String
        Public strExcento As String
    End Class

    Public Class ClsDesgloseFormaPago
        Public strDescripcion As String
        Public strMonto As String
        Public strNroDoc As String
    End Class
#End Region

#Region "Métodos sobrecargados"
    <StructLayout(LayoutKind.Sequential, CharSet:=CharSet.Unicode)>
    Structure DOCINFOW
        <MarshalAs(UnmanagedType.LPWStr)> Public pDocName As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pOutputFile As String
        <MarshalAs(UnmanagedType.LPWStr)> Public pDataType As String
    End Structure

    <DllImport("winspool.Drv", EntryPoint:="OpenPrinterW",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function OpenPrinter(ByVal src As String, ByRef hPrinter As IntPtr, ByVal pd As Integer) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="ClosePrinter",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function ClosePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="StartDocPrinterW",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function StartDocPrinter(ByVal hPrinter As IntPtr, ByVal level As Integer, ByRef pDI As DOCINFOW) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="EndDocPrinter",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function EndDocPrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="StartPagePrinter",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function StartPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="EndPagePrinter",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function EndPagePrinter(ByVal hPrinter As IntPtr) As Boolean
    End Function
    <DllImport("winspool.Drv", EntryPoint:="WritePrinter",
        SetLastError:=True, CharSet:=CharSet.Unicode,
        ExactSpelling:=True, CallingConvention:=CallingConvention.StdCall)>
    Private Shared Function WritePrinter(ByVal hPrinter As IntPtr, ByVal pBytes As IntPtr, ByVal dwCount As Integer, ByRef dwWritten As Integer) As Boolean
    End Function

    Private Shared Function SendBytesToPrinter(ByVal szPrinterName As String, ByVal pBytes As IntPtr, ByVal dwCount As Integer) As Boolean
        Dim hPrinter As IntPtr          ' The printer handle. 
        Dim dwError As Integer            ' Last error - in case there was trouble. 
        Dim di As New DOCINFOW          ' Describes your document (name, port, data type). 
        Dim dwWritten As Integer          ' The number of bytes written by WritePrinter(). 
        Dim bSuccess As Boolean         ' Your success code. 
        Try
            With di
                .pDocName = "Document from Leandro Software"
                .pDataType = "RAW"
            End With
            bSuccess = False
            If OpenPrinter(szPrinterName, hPrinter, 0) Then
                If StartDocPrinter(hPrinter, 1, di) Then
                    If StartPagePrinter(hPrinter) Then
                        bSuccess = WritePrinter(hPrinter, pBytes, dwCount, dwWritten)
                        EndPagePrinter(hPrinter)
                    End If
                    EndDocPrinter(hPrinter)
                End If
                ClosePrinter(hPrinter)
            End If
            If bSuccess = False Then
                dwError = Marshal.GetLastWin32Error()
            End If
        Catch ex As Exception
            Throw New Exception("Error en SendBytesToPrinter: " & szPrinterName)
        End Try
        Return bSuccess
    End Function

    Private Shared Sub SendStringToPrinter(ByVal szPrinterName As String, ByVal strInput As String)
        Dim pBytes As IntPtr
        Dim dwCount As Integer
        Dim bSuccess As Boolean
        Try
            Dim strDataString = strInput + Chr(12)
            dwCount = strDataString.Length
            pBytes = Marshal.StringToCoTaskMemAnsi(strDataString)
            bSuccess = SendBytesToPrinter(szPrinterName, pBytes, dwCount)
            If Not bSuccess Then
                Throw New Exception("No se logro imprimir el tiquete en el dispositivo: " & szPrinterName)
            End If
            Marshal.FreeCoTaskMem(pBytes)
        Catch ex As Exception
            Throw New Exception("Error en SendStringToPrinter: " & szPrinterName)
        End Try
    End Sub

    Private Sub SendDataToPrinter(ByVal szPrinterName As String, ByVal strFilePath As String)
        Dim printDocument As PrintDocument = New PrintDocument()
        Dim fileStream As FileStream = New FileStream(strFilePath, FileMode.Open)
        Dim streamReader As StreamReader = New StreamReader(fileStream)
        Dim stringToPrint As String = streamReader.ReadToEnd()
        printDocument.PrinterSettings.PrinterName = szPrinterName
        AddHandler printDocument.PrintPage, New PrintPageEventHandler(AddressOf OnPrintPage)
        printDocument.Print()
        streamReader.Close()
        fileStream.Close()
    End Sub

    Private Sub OnPrintPage(ByVal sender As Object, ByVal e As PrintPageEventArgs)
        Dim objGDI As Graphics = e.Graphics
        Static startPage As Integer = 0
        Dim intLeft As Integer = e.MarginBounds.Left
        Dim intWidth As Integer = e.MarginBounds.Width
        Dim intHeight As Integer = e.MarginBounds.Height
        Dim intYPosition As Integer = 0

        ' Use this for left/right/centre justification, trimming, etc
        ' ensure words aren't printed over the edge of the margin (start a new line)
        Dim objStringFormat As StringFormat = New StringFormat With {
            .Alignment = StringAlignment.Near,
            .Trimming = StringTrimming.Word
        }

        Dim regular As Font = New Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Regular)
        Dim bold As Font = New Font(FontFamily.GenericSansSerif, 10.0F, FontStyle.Bold)

        Dim sizeText As SizeF = objGDI.MeasureString("hello world", regular, intWidth)

        Dim rectLayout As RectangleF = New RectangleF(New PointF(intLeft, intYPosition), New SizeF(intWidth, sizeText.Height))

        ' draw the content, including formatting information
        objGDI.DrawString("hello world", regular, Brushes.Black, rectLayout, objStringFormat)

        objGDI.DrawString("FERREIRA MATERIALS PARA CONSTRUCAO LTDA", bold, Brushes.Black, 20, 10)
        objGDI.DrawString("EST ENGENHEIRO MARCILAC, 116, SAO PAOLO - SP", regular, Brushes.Black, 30, 30)
        objGDI.DrawString("Telefone: (11)5921-3826", regular, Brushes.Black, 110, 50)
        objGDI.DrawLine(Pens.Black, 80, 70, 320, 70)
        objGDI.DrawString("CUPOM NAO FISCAL", bold, Brushes.Black, 110, 80)
        objGDI.DrawLine(Pens.Black, 80, 100, 320, 100)

        objGDI.DrawString("COD | DESCRICAO                      | QTY | X | Vir Unit | Vir Total |", bold, Brushes.Black, 10, 120)
        objGDI.DrawLine(Pens.Black, 10, 140, 430, 140)

        regular.Dispose()
        bold.Dispose()
    End Sub
#End Region

#Region "Métodos"
    Private Shared Function ImprimirEncabezado(objEquipo As TerminalPorEmpresa, objEmpresa As Empresa, strFecha As String, Optional strCodigoUsuario As String = "") As String
        Dim strCadena As String = strFecha & strCodigoUsuario.PadLeft(30, " ") & Chr(13) & Chr(10)
        strCadena += Chr(13) & Chr(10)
        If objEquipo.NombreSucursal.Length > 40 Then
            strCadena += objEquipo.NombreSucursal.Substring(0, 40) & Chr(13) & Chr(10)
        Else
            strCadena += "".PadRight((40 - objEquipo.NombreSucursal.Length) / 2, " ") & objEquipo.NombreSucursal & Chr(13) & Chr(10)
        End If
        strCadena += Chr(13) & Chr(10)
        Dim strDireccion1 As String
        If objEquipo.Direccion.Length > 40 Then
            Dim intEspacioIndex = objEquipo.Direccion.Substring(0, 40).LastIndexOf(" ")
            Dim strDireccion2 As String
            If (intEspacioIndex >= 0) Then
                strDireccion1 = objEquipo.Direccion.Substring(0, intEspacioIndex)
                strDireccion2 = objEquipo.Direccion.Substring(intEspacioIndex + 1)
            Else
                strDireccion1 = objEquipo.NombreSucursal.Substring(0, 40)
                strDireccion2 = objEquipo.NombreSucursal.Substring(40)
            End If
            strCadena += "".PadRight((40 - strDireccion1.Length) / 2, " ") + strDireccion1 & Chr(13) & Chr(10)
            strCadena += "".PadRight((40 - strDireccion2.Length) / 2, " ") + strDireccion2 & Chr(13) & Chr(10)
        Else
            strDireccion1 = objEquipo.Direccion
            strCadena += "".PadRight((40 - strDireccion1.Length) / 2, " ") + strDireccion1 & Chr(13) & Chr(10)
        End If
        Dim strTelefono As String = "TELEFONO: " + objEquipo.Telefono
        strCadena += "".PadRight((40 - strTelefono.Length) / 2, " ") + strTelefono & Chr(13) & Chr(10)
        strCadena += Chr(13) & Chr(10)
        If objEmpresa.NombreEmpresa.Length > 40 Then
            strCadena += objEmpresa.NombreEmpresa.Substring(0, 40) & Chr(13) & Chr(10)
        Else
            strCadena += "".PadRight((40 - objEmpresa.NombreEmpresa.Length) / 2, " ") & objEmpresa.NombreEmpresa & Chr(13) & Chr(10)
        End If
        If objEmpresa.Identificacion.Length > 40 Then
            strCadena += objEmpresa.Identificacion.Substring(0, 40) & Chr(13) & Chr(10)
        Else
            strCadena += "".PadRight((40 - objEmpresa.Identificacion.Length) / 2, " ") & objEmpresa.Identificacion & Chr(13) & Chr(10)
        End If

        strCadena += "".PadRight((40 - objEmpresa.CorreoNotificacion.Length) / 2, " ") + objEmpresa.CorreoNotificacion & Chr(13) & Chr(10)
        Return strCadena
    End Function

    Private Shared Function ImprimirDesglosePago(objDesglosePago As IList(Of ClsDesgloseFormaPago)) As String
        Dim strDesglosePago As String = ""
        strDesglosePago += "Desglose de Forma de pago" & Chr(13) & Chr(10)
        For i As Integer = 0 To objDesglosePago.Count - 1
            strDesglosePago += " " & objDesglosePago(i).strDescripcion.PadRight(13) & " " & objDesglosePago(i).strMonto.PadLeft(13, " ") & " " & objDesglosePago(i).strNroDoc.PadLeft(11, " ") & Chr(13) & Chr(10)
        Next
        Return strDesglosePago
    End Function

    Private Shared Function ImprimirDetalle(objDetalleComprobante As IList(Of clsDetalleComprobante)) As String
        Dim strDetalle As String = ""
        strDetalle += "Descripcion" & Chr(13) & Chr(10)
        strDetalle += "Cant".PadLeft(6, " ") & "P/U".PadLeft(15, " ") & " Total".PadLeft(15, " ") & Chr(13) & Chr(10)
        For i As Integer = 0 To objDetalleComprobante.Count - 1
            If CDbl(objDetalleComprobante(i).strPrecio) > 0 Then
                Dim lineas As New List(Of String)
                Dim strLinea = objDetalleComprobante(i).strDescripcion
                For j As Integer = 0 To strLinea.Length() - 1 Step 40
                    If strLinea.Length() < 40 Then
                        lineas.Add(strLinea)
                    Else
                        lineas.Add(strLinea.Substring(0, 40))
                        strLinea = strLinea.Substring(40)
                    End If
                Next
                For Each strEachLinea As String In lineas
                    strDetalle += strEachLinea & Chr(13) & Chr(10)
                Next
                strDetalle += objDetalleComprobante(i).strCantidad.PadLeft(6, " ") & objDetalleComprobante(i).strPrecio.PadLeft(15, " ") & objDetalleComprobante(i).strTotalLinea.PadLeft(15, " ") & IIf(Not objDetalleComprobante(i).strExcento Is Nothing, "  " & objDetalleComprobante(i).strExcento, "") & Chr(13) & Chr(10)
            End If
        Next
        Return strDetalle
    End Function

    Private Shared Function ImprimirTotales(objComprobante As ClsComprobante) As String
        Dim strTotales As String = ""
        strTotales += "Sub-Total:".PadLeft(23, " ") & objComprobante.strSubTotal.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        If objComprobante.strDescuento <> "" Then
            strTotales += "Descuento:".PadLeft(23, " ") & objComprobante.strDescuento.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        End If
        strTotales += "Impuesto:".PadLeft(23, " ") & objComprobante.strImpuesto.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        strTotales += "Total:".PadLeft(23, " ") & objComprobante.strTotal.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        Return strTotales
    End Function

    Public Shared Sub ImprimirFactura(ByVal objFactura As ClsComprobante)
        Dim strFactura As String = ""
        Try
            strFactura += ImprimirEncabezado(objFactura.equipo, objFactura.empresa, Date.Now.ToString("dd-MM-yyyy"), objFactura.usuario.CodigoUsuario)
            strFactura += Chr(13) & Chr(10)
            If objFactura.strClaveNumerica <> "" Then
                strFactura += "Clave numerica".PadLeft(27, " ") & Chr(13) & Chr(10)
                strFactura += objFactura.strClaveNumerica.Substring(0, 25).PadLeft(32, " ") & Chr(13) & Chr(10)
                strFactura += objFactura.strClaveNumerica.Substring(25, 25).PadLeft(32, " ") & Chr(13) & Chr(10)
            End If
            strFactura += Chr(13) & Chr(10)
            strFactura += "       Sucursal: " & objFactura.equipo.IdSucursal.ToString().PadRight(4, " ") & "Terminal: " & objFactura.equipo.IdTerminal & Chr(13) & Chr(10)
            strFactura += Chr(13) & Chr(10)
            strFactura += "Factura Nro: " & objFactura.strId & Chr(13) & Chr(10)
            strFactura += "Vendedor: " & objFactura.strVendedor.Substring(0, If(objFactura.strVendedor.Length < 30, objFactura.strVendedor.Length, 30)) & Chr(13) & Chr(10)
            strFactura += "Nombre: " & objFactura.strNombre.Substring(0, If(objFactura.strNombre.Length < 32, objFactura.strNombre.Length, 32)) & Chr(13) & Chr(10)
            strFactura += "Fecha: " & objFactura.strFecha & Chr(13) & Chr(10)
            If objFactura.strDocumento <> "" Then strFactura += "Documento: " & objFactura.strDocumento & Chr(13) & Chr(10)
            strFactura += Chr(13) & Chr(10)
            strFactura += ImprimirDesglosePago(objFactura.arrDesglosePago)
            strFactura += "".PadRight(40, "_") & Chr(13) & Chr(10)
            strFactura += ImprimirDetalle(objFactura.arrDetalleComprobante)
            strFactura += "".PadRight(40, "_") & Chr(13) & Chr(10) & Chr(13) & Chr(10)
            strFactura += ImprimirTotales(objFactura)
            strFactura += "Pago con:".PadLeft(23, " ") & objFactura.strPagoCon.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
            strFactura += "Cambio:".PadLeft(23, " ") & objFactura.strCambio.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
            strFactura += Chr(13) & Chr(10) & Chr(13) & Chr(10)
            strFactura += "     IMPUESTO DE VENTAS INCLUIDO." & Chr(13) & Chr(10)
            strFactura += " AUTORIZADO MEDIANTE RESOLUCION NUMERO" & Chr(13) & Chr(10)
            strFactura += "     DGT-R-48-2016 DEL 07-OCT-2016" & Chr(13) & Chr(10) & Chr(13) & Chr(10)
            strFactura += "       GRACIAS POR PREFERIRNOS" & Chr(13) & Chr(10)
            strFactura += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            SendStringToPrinter(objFactura.equipo.ImpresoraFactura, strFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a SendStringToPrinter:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirCompra(ByVal objCompra As ClsComprobante)
        Dim strCompra As String = ""
        strCompra += ImprimirEncabezado(objCompra.equipo, objCompra.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strCompra += Chr(13) & Chr(10)
        strCompra += "Compra Nro: " & objCompra.strId & Chr(13) & Chr(10)
        strCompra += "Proveedor: " & objCompra.strNombre.Substring(0, If(objCompra.strNombre.Length < 29, objCompra.strNombre.Length, 29)) & Chr(13) & Chr(10)
        strCompra += "Fecha: " & objCompra.strFecha & Chr(13) & Chr(10)
        strCompra += ImprimirDesglosePago(objCompra.arrDesglosePago)
        strCompra += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strCompra += ImprimirDetalle(objCompra.arrDetalleComprobante)
        strCompra += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strCompra += ImprimirTotales(objCompra)
        strCompra += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objCompra.equipo.ImpresoraFactura, strCompra)
    End Sub

    Public Shared Sub ImprimirDevolucionCliente(ByVal objDevolucion As ClsComprobante)
        Dim strDevolucion As String = ""
        strDevolucion += "    DEVOLUCION DE MERCANCIA CLIENTES" & Chr(13) & Chr(10)
        strDevolucion += Chr(13) & Chr(10)
        strDevolucion += ImprimirEncabezado(objDevolucion.equipo, objDevolucion.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strDevolucion += Chr(13) & Chr(10)
        strDevolucion += "Movimiento Nro: " & objDevolucion.strId & Chr(13) & Chr(10)
        strDevolucion += "Factura Nro: " & objDevolucion.strDocumento & Chr(13) & Chr(10)
        strDevolucion += "Cliente: " & objDevolucion.strNombre.Substring(0, If(objDevolucion.strNombre.Length < 31, objDevolucion.strNombre.Length, 31)) & Chr(13) & Chr(10)
        strDevolucion += "Fecha: " & objDevolucion.strFecha & Chr(13) & Chr(10)
        strDevolucion += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strDevolucion += ImprimirDetalle(objDevolucion.arrDetalleComprobante)
        strDevolucion += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strDevolucion += ImprimirTotales(objDevolucion)
        strDevolucion += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        strDevolucion += "Recibido por: __________________________" & Chr(13) & Chr(10)
        strDevolucion += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objDevolucion.equipo.ImpresoraFactura, strDevolucion)
    End Sub

    Public Shared Sub ImprimirDevolucionProveedor(ByVal objDevolucion As ClsComprobante)
        Dim strDevolucion As String = ""
        strDevolucion += "   DEVOLUCION DE MERCANCIA PROVEEDOR" & Chr(13) & Chr(10)
        strDevolucion += Chr(13) & Chr(10)
        strDevolucion += ImprimirEncabezado(objDevolucion.equipo, objDevolucion.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strDevolucion += Chr(13) & Chr(10)
        strDevolucion += "Movimiento Nro: " & objDevolucion.strId & Chr(13) & Chr(10)
        strDevolucion += "Factura Nro: " & objDevolucion.strDocumento & Chr(13) & Chr(10)
        strDevolucion += "Proveedor: " & objDevolucion.strNombre.Substring(0, If(objDevolucion.strNombre.Length < 29, objDevolucion.strNombre.Length, 29)) & Chr(13) & Chr(10)
        strDevolucion += "Fecha: " & objDevolucion.strFecha & Chr(13) & Chr(10)
        strDevolucion += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strDevolucion += ImprimirDetalle(objDevolucion.arrDetalleComprobante)
        strDevolucion += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strDevolucion += ImprimirTotales(objDevolucion)
        strDevolucion += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        strDevolucion += "Recibido por: __________________________" & Chr(13) & Chr(10)
        strDevolucion += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objDevolucion.equipo.ImpresoraFactura, strDevolucion)
    End Sub

    Public Shared Sub ImprimirTraslado(ByVal objTraslado As ClsComprobante)
        Dim strTraslado As String = ""
        strTraslado += "         TRASLADO DE MERCANCIA" & Chr(13) & Chr(10)
        strTraslado += Chr(13) & Chr(10)
        strTraslado += ImprimirEncabezado(objTraslado.equipo, objTraslado.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strTraslado += Chr(13) & Chr(10)
        strTraslado += "Traslado Nro: " & objTraslado.strId & Chr(13) & Chr(10)
        strTraslado += "Tipo: " & objTraslado.strFormaPago & Chr(13) & Chr(10)
        strTraslado += "Sucursal: " & objTraslado.strNombre.Substring(0, If(objTraslado.strNombre.Length < 30, objTraslado.strNombre.Length, 30)) & Chr(13) & Chr(10)
        strTraslado += "Fecha: " & objTraslado.strFecha & Chr(13) & Chr(10)
        strTraslado += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strTraslado += ImprimirDetalle(objTraslado.arrDetalleComprobante)
        strTraslado += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strTraslado += "Total:".PadLeft(23, " ") & objTraslado.strTotal.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        strTraslado += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        strTraslado += "Enviado por: " & objTraslado.strEnviadoPor & Chr(13) & Chr(10)
        strTraslado += Chr(13) & Chr(10)
        strTraslado += "Recibido por: __________________________" & Chr(13) & Chr(10)
        strTraslado += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objTraslado.equipo.ImpresoraFactura, strTraslado)
    End Sub

    Public Shared Sub ImprimirReciboCxC(ByVal objReciboCxC As ClsRecibo)
        Dim strRecibo As String = ""
        Dim i As Integer
        strRecibo += "        RECIBO CUENTA POR COBRAR" & Chr(13) & Chr(10)
        strRecibo += Chr(13) & Chr(10)
        strRecibo += ImprimirEncabezado(objReciboCxC.equipo, objReciboCxC.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strRecibo += Chr(13) & Chr(10)
        strRecibo += "Consecutivo: " & objReciboCxC.strConsecutivo & Chr(13) & Chr(10)
        strRecibo += "Cliente: " & objReciboCxC.strNombre.Substring(0, If(objReciboCxC.strNombre.Length < 31, objReciboCxC.strNombre.Length, 31)) & Chr(13) & Chr(10)
        strRecibo += "Fecha: " & objReciboCxC.strFechaAbono & Chr(13) & Chr(10)
        strRecibo += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strRecibo += "Desglose Cuentas por Cobrar Abonadas:" & Chr(13) & Chr(10)
        For i = 0 To objReciboCxC.arrDesgloseMov.Count - 1
            strRecibo += "Cuenta: " & objReciboCxC.arrDesgloseMov(i).strDescripcion.PadRight(15).Substring(0, 15) & " Monto: " & objReciboCxC.arrDesgloseMov(i).strMonto.PadRight(11).Substring(0, 11) & Chr(13) & Chr(10)
        Next
        strRecibo += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strRecibo += ImprimirDesglosePago(objReciboCxC.arrDesglosePago)
        strRecibo += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        strRecibo += "Monto Total: " & objReciboCxC.strTotalAbono.ToString.PadLeft(27, " ") & Chr(13) & Chr(10)
        strRecibo += "       GRACIAS POR PREFERIRNOS" & Chr(13) & Chr(10)
        strRecibo += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objReciboCxC.equipo.ImpresoraFactura, strRecibo)
    End Sub

    Public Shared Sub ImprimirReciboCxP(ByVal objReciboCxP As ClsRecibo)
        Dim strRecibo As String = ""
        Dim i As Integer
        strRecibo += "       ABONO CUENTA POR PAGAR" & Chr(13) & Chr(10)
        strRecibo += Chr(13) & Chr(10)
        strRecibo += ImprimirEncabezado(objReciboCxP.equipo, objReciboCxP.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strRecibo += Chr(13) & Chr(10)
        strRecibo += "Consecutivo: " & objReciboCxP.strConsecutivo & Chr(13) & Chr(10)
        strRecibo += "Recibo Nro: " & objReciboCxP.strRecibo & Chr(13) & Chr(10)
        strRecibo += "Proveedor: " & objReciboCxP.strNombre.Substring(0, If(objReciboCxP.strNombre.Length < 29, objReciboCxP.strNombre.Length, 29)) & Chr(13) & Chr(10)
        strRecibo += "Fecha: " & objReciboCxP.strFechaAbono & Chr(13) & Chr(10)
        strRecibo += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strRecibo += "Desglose Cuentas por Pagar Abonadas:" & Chr(13) & Chr(10)
        For i = 0 To objReciboCxP.arrDesgloseMov.Count - 1
            strRecibo += "Cuenta: " & objReciboCxP.arrDesgloseMov(i).strDescripcion.PadRight(15).Substring(0, 15) & " Monto: " & objReciboCxP.arrDesgloseMov(i).strMonto.PadRight(11).Substring(0, 11) & Chr(13) & Chr(10)
        Next
        strRecibo += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strRecibo += ImprimirDesglosePago(objReciboCxP.arrDesglosePago)
        strRecibo += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        strRecibo += "Monto Total: " & objReciboCxP.strTotalAbono.ToString.PadLeft(27, " ") & Chr(13) & Chr(10)
        strRecibo += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objReciboCxP.equipo.ImpresoraFactura, strRecibo)
    End Sub

    Public Shared Sub ImprimirEgreso(ByVal objEgreso As ClsEgreso)
        Dim strEgreso As String = ""
        strEgreso += "         COMPROBANTE DE EGRESO" & Chr(13) & Chr(10)
        strEgreso += Chr(13) & Chr(10)
        strEgreso += ImprimirEncabezado(objEgreso.equipo, objEgreso.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strEgreso += Chr(13) & Chr(10)
        strEgreso += "Egreso Nro: " & objEgreso.strId & Chr(13) & Chr(10)
        strEgreso += "Fecha: " & objEgreso.strFecha & objEgreso.usuario.CodigoUsuario.PadLeft(23) & Chr(13) & Chr(10)
        strEgreso += "Pagado a: " & objEgreso.strBeneficiario.Substring(0, If(objEgreso.strBeneficiario.Length < 30, objEgreso.strBeneficiario.Length, 30)) & Chr(13) & Chr(10)
        strEgreso += "La suma de: " & objEgreso.strMonto.ToString.PadLeft(26, " ") & Chr(13) & Chr(10)
        strEgreso += Chr(13) & Chr(10)
        Dim lineas As New List(Of String)
        Dim strCadena = "Concepto: " & objEgreso.strConcepto
        For j As Integer = 0 To strCadena.Length() - 1 Step 40
            If strCadena.Length() < 40 Then
                lineas.Add(strCadena)
            Else
                lineas.Add(strCadena.Substring(0, 40))
                strCadena = strCadena.Substring(40)
            End If
        Next
        For Each strLinea As String In lineas
            strEgreso += strLinea & Chr(13) & Chr(10)
        Next
        strEgreso += Chr(13) & Chr(10)
        strEgreso += ImprimirDesglosePago(objEgreso.arrDesglosePago)
        strEgreso += Chr(13) & Chr(10)
        strEgreso += "Recibido por: __________________________"
        strEgreso += Chr(13) & Chr(10)
        strEgreso += Chr(13) & Chr(10)
        strEgreso += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objEgreso.equipo.ImpresoraFactura, strEgreso)
    End Sub

    Public Shared Sub ImprimirIngreso(ByVal objIngreso As ClsIngreso)
        Dim strIngreso As String = ""
        strIngreso += "          REGISTRO DE INGRESO" & Chr(13) & Chr(10)
        strIngreso += Chr(13) & Chr(10)
        strIngreso += ImprimirEncabezado(objIngreso.equipo, objIngreso.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strIngreso += Chr(13) & Chr(10)
        strIngreso += "Ingreso Nro: " & objIngreso.strId & Chr(13) & Chr(10)
        strIngreso += "Fecha: " & objIngreso.strFecha & objIngreso.usuario.CodigoUsuario.PadLeft(23) & Chr(13) & Chr(10)
        strIngreso += "Recibo de: " & objIngreso.strRecibidoDe.Substring(0, If(objIngreso.strRecibidoDe.Length < 29, objIngreso.strRecibidoDe.Length, 29)) & Chr(13) & Chr(10)
        strIngreso += "La suma de: " & objIngreso.strMonto.ToString.PadLeft(26, " ") & Chr(13) & Chr(10)
        strIngreso += Chr(13) & Chr(10)
        Dim lineas As New List(Of String)
        Dim strCadena = "Concepto: " & objIngreso.strConcepto
        For j As Integer = 0 To strCadena.Length() - 1 Step 40
            If strCadena.Length() < 40 Then
                lineas.Add(strCadena)
            Else
                lineas.Add(strCadena.Substring(0, 40))
                strCadena = strCadena.Substring(40)
            End If
        Next
        For Each strLinea As String In lineas
            strIngreso += strLinea & Chr(13) & Chr(10)
        Next
        strIngreso += Chr(13) & Chr(10)
        strIngreso += ImprimirDesglosePago(objIngreso.arrDesglosePago)
        strIngreso += Chr(13) & Chr(10)
        strIngreso += "Recibido por: __________________________"
        strIngreso += Chr(13) & Chr(10)
        strIngreso += Chr(13) & Chr(10)
        strIngreso += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objIngreso.equipo.ImpresoraFactura, strIngreso)
    End Sub

    Public Shared Sub ImprimirCuentaPorPagar(ByVal objcuenta As ClsCuentaPorPagar)
        Dim strCuenta As String = ""
        strCuenta += "    COMPROBANTE DE CUENTA POR PAGAR" & Chr(13) & Chr(10)
        strCuenta += Chr(13) & Chr(10)
        strCuenta += ImprimirEncabezado(objcuenta.equipo, objcuenta.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strCuenta += Chr(13) & Chr(10)
        strCuenta += "Cuenta Nro: " & objcuenta.strId & Chr(13) & Chr(10)
        strCuenta += "Fecha: " & objcuenta.strFecha & objcuenta.usuario.CodigoUsuario.PadLeft(23) & Chr(13) & Chr(10)
        strCuenta += "Recibo de: " & objcuenta.strRecibidoDe.Substring(0, If(objcuenta.strRecibidoDe.Length < 29, objcuenta.strRecibidoDe.Length, 29)) & Chr(13) & Chr(10)
        strCuenta += "La suma de: " & objcuenta.strMonto.ToString.PadLeft(26, " ") & Chr(13) & Chr(10)
        strCuenta += Chr(13) & Chr(10)
        Dim lineas As New List(Of String)
        Dim strCadena = "Concepto: " & objcuenta.strConcepto
        For j As Integer = 0 To strCadena.Length() - 1 Step 40
            If strCadena.Length() < 40 Then
                lineas.Add(strCadena)
            Else
                lineas.Add(strCadena.Substring(0, 40))
                strCadena = strCadena.Substring(40)
            End If
        Next
        For Each strLinea As String In lineas
            strCuenta += strLinea & Chr(13) & Chr(10)
        Next
        strCuenta += Chr(13) & Chr(10)
        strCuenta += ImprimirDesglosePago(objcuenta.arrDesglosePago)
        strCuenta += Chr(13) & Chr(10)
        strCuenta += "Recibido por: __________________________"
        strCuenta += Chr(13) & Chr(10)
        strCuenta += Chr(13) & Chr(10)
        strCuenta += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objcuenta.equipo.ImpresoraFactura, strCuenta)
    End Sub

    Public Shared Sub ImprimirAjusteInventario(ByVal objAjuste As ClsAjusteInventario)
        Dim strAjusteInventario As String = ""
        strAjusteInventario += "         AJUSTE DE INVENTARIO" & Chr(13) & Chr(10)
        strAjusteInventario += Chr(13) & Chr(10)
        strAjusteInventario += ImprimirEncabezado(objAjuste.equipo, objAjuste.empresa, Date.Now.ToString("dd-MM-yyyy"))
        strAjusteInventario += Chr(13) & Chr(10)
        strAjusteInventario += "Movimiento Nro: " & objAjuste.strId & Chr(13) & Chr(10)
        strAjusteInventario += "Fecha: " & objAjuste.strFecha & Chr(13) & Chr(10)
        Dim lineas As New List(Of String)
        Dim strCadena = "Concepto: " & objAjuste.strDescripcion
        For j As Integer = 0 To strCadena.Length() - 1 Step 40
            If strCadena.Length() < 40 Then
                lineas.Add(strCadena)
            Else
                lineas.Add(strCadena.Substring(0, 40))
                strCadena = strCadena.Substring(40)
            End If
        Next
        For Each strLinea As String In lineas
            strAjusteInventario += strLinea & Chr(13) & Chr(10)
        Next
        strAjusteInventario += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strAjusteInventario += ImprimirDetalle(objAjuste.arrDetalleComprobante)
        strAjusteInventario += "".PadRight(40, "_") & Chr(13) & Chr(10)
        strAjusteInventario += "Revisado por: __________________________" & Chr(13) & Chr(10)
        strAjusteInventario += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        SendStringToPrinter(objAjuste.equipo.ImpresoraFactura, strAjusteInventario)
    End Sub
#End Region
End Class
