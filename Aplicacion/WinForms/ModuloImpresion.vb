Imports System.IO
Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Drawing.Printing
Imports LeandroSoftware.Core.TiposComunes

Public Class ModuloImpresion
#Region "Variables"
    Private Shared lineas As IList = New List(Of ClsLineaImpresion)
    Private Class ClsLineaImpresion
        Public Sub New(saltos As Integer, texto As String, posicionX As Integer, porcX As Integer, fuente As Integer, alineado As StringAlignment, bold As Boolean)
            intSaltos = saltos
            strTexto = texto
            intPosicionX = posicionX
            intPorcX = porcX
            intFuente = fuente
            agtAlineado = alineado
            bolBold = bold
        End Sub

        Public intSaltos As Integer
        Public strTexto As String
        Public intPosicionX As Integer
        Public intPorcX As Integer
        Public intFuente As Integer
        Public agtAlineado As StringAlignment
        Public bolBold As Boolean
    End Class
    Public Class ClsEgreso
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strMonto As String
        Public strBeneficiario As String
        Public strConcepto As String
        Public strFormaPago As String
        Public strBanco As String
        Public strNroCheque As String
        Public arrDesglosePago As IList(Of ClsDesgloseFormaPago)
    End Class

    Public Class ClsIngreso
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strMonto As String
        Public strRecibidoDe As String
        Public strConcepto As String
        Public strFormaPago As String
        Public strBanco As String
        Public strNroMovimiento As String
        Public arrDesglosePago As IList(Of ClsDesgloseFormaPago)
    End Class

    Public Class ClsCuentaPorPagar
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strMonto As String
        Public strRecibidoDe As String
        Public strConcepto As String
        Public strFormaPago As String
        Public strBanco As String
        Public strNroMovimiento As String
        Public arrDesglosePago As IList(Of ClsDesgloseFormaPago)
    End Class

    Public Class ClsRecibo
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
        Public usuario As Usuario
        Public strConsecutivo As String
        Public strRecibo As String
        Public strNombre As String
        Public strFechaAbono As String
        Public strTotalAbono As String
        Public arrDesgloseMov As IList(Of ClsDesgloseFormaPago)
        Public arrDesglosePago As IList(Of ClsDesgloseFormaPago)
    End Class

    Public Class ClsAjusteInventario
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
        Public usuario As Usuario
        Public strId As String
        Public strFecha As String
        Public strDescripcion As String
        Public arrDetalleComprobante As IList(Of ClsDetalleComprobante)
    End Class

    Public Class ClsComprobante
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
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
        Public arrDetalleComprobante As IList(Of ClsDetalleComprobante)
        Public arrDesglosePago As IList(Of ClsDesgloseFormaPago)
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

    Private Shared Sub ImprimirTiquete(szPrinterName As String)
        Dim doc As PrintDocument = New PrintDocument()
        doc.PrinterSettings.PrinterName = szPrinterName
        AddHandler doc.PrintPage, New PrintPageEventHandler(AddressOf ProvideContent)
        doc.Print()
    End Sub


    Private Shared Sub ProvideContent(sender As Object, e As PrintPageEventArgs)
        'FontSize 8 41 Chars - FontSize 9 36 Chars - FontSize 10 33 Chars - FontSize 11 30 Chars - FontSize 12 27 Chars
        'FontSize 13 25 Chars - FontSize 14 23 Chars - FontSize 15 22 Chars - FontSize 16 20 Chars
        Dim maxWidth As Integer = 348.0496
        Dim i As Integer = 0
        Dim graphics As Graphics = e.Graphics
        Dim positionY As Integer = 20
        Dim sf As StringFormat = New StringFormat()
        'e.PageSettings.PaperSize.Width = 50
        While i < lineas.Count - 1
            Dim linea As ClsLineaImpresion = lineas(i)

            Dim fontStyle As FontStyle = IIf(linea.bolBold, FontStyle.Bold, FontStyle.Regular)
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = linea.agtAlineado
            Dim stringSize As SizeF = e.Graphics.MeasureString(linea.strTexto, New Font("Courier New", linea.intFuente, fontStyle))
            Dim rec As RectangleF = New RectangleF()
            rec.Width = maxWidth * linea.intPorcX / 100
            rec.Height = 17.4696178
            rec.X = maxWidth * linea.intPosicionX / 100
            rec.Y = positionY
            graphics.DrawString(linea.strTexto, New Font("Courier New", linea.intFuente, fontStyle), New SolidBrush(Color.Black), rec, sf)
            positionY += (20 * linea.intSaltos)
            i += 1
        End While
    End Sub
#End Region

#Region "Métodos"
    Private Shared Sub ImprimirEncabezado(objEquipo As EquipoRegistrado, objEmpresa As Empresa, strFecha As String, strCodigoUsuario As String, strTitulo As String)
        Dim esMatriz = objEquipo.ImpresoraMatriz
        lineas.Add(New ClsLineaImpresion(0, strFecha, 0, 50, 12, StringAlignment.Near, True))
        lineas.Add(New ClsLineaImpresion(2, strCodigoUsuario, 51, 50, 10, StringAlignment.Far, True))
        lineas.Add(New ClsLineaImpresion(1, strTitulo, 0, 100, 14, StringAlignment.Center, True))
        Dim strNombreComercial As String = objEmpresa.NombreComercial
        While strNombreComercial.Length > 27
            lineas.Add(New ClsLineaImpresion(1, strNombreComercial.Substring(0, 27), 0, 100, 12, StringAlignment.Center, True))
            strNombreComercial = strNombreComercial.Substring(27)
        End While
        lineas.Add(New ClsLineaImpresion(2, strNombreComercial, 0, 100, 12, StringAlignment.Center, True))
        Dim strDireccion As String = objEquipo.DireccionSucursal
        While strDireccion.Length > 33
            lineas.Add(New ClsLineaImpresion(1, strDireccion.Substring(0, 33), 0, 100, 10, StringAlignment.Center, False))
            strDireccion = strDireccion.Substring(33)
        End While
        lineas.Add(New ClsLineaImpresion(1, strDireccion, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(1, "TELEFONO: " + objEquipo.TelefonoSucursal, 0, 100, 10, StringAlignment.Center, False))
        Dim strNombreEmpresa = objEmpresa.NombreEmpresa
        While strNombreEmpresa.Length > 33
            lineas.Add(New ClsLineaImpresion(1, strNombreEmpresa.Substring(0, 33), 0, 100, 10, StringAlignment.Center, False))
            strNombreEmpresa = strNombreEmpresa.Substring(33)
        End While
        lineas.Add(New ClsLineaImpresion(1, strNombreEmpresa, 0, 100, 10, StringAlignment.Center, False))
        Dim strIdentificacion = objEmpresa.Identificacion
        While strIdentificacion.Length > 33
            lineas.Add(New ClsLineaImpresion(1, strIdentificacion.Substring(0, 33), 0, 100, 10, StringAlignment.Center, False))
            strIdentificacion = strIdentificacion.Substring(33)
        End While
        lineas.Add(New ClsLineaImpresion(1, strIdentificacion, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(2, objEmpresa.CorreoNotificacion, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(1, objEquipo.NombreSucursal, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(2, "Terminal: " & objEquipo.IdTerminal, 0, 100, 10, StringAlignment.Center, False))
    End Sub

    Private Shared Sub ImprimirDesglosePago(objDesglosePago As IList(Of ClsDesgloseFormaPago))
        lineas.Add(New ClsLineaImpresion(1, "Desglose Forma de pago", 0, 100, 10, StringAlignment.Center, False))
        For i As Integer = 0 To objDesglosePago.Count - 1
            lineas.Add(New ClsLineaImpresion(0, objDesglosePago(i).strDescripcion, 3, 57, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objDesglosePago(i).strMonto, 60, 40, 10, StringAlignment.Far, False))
        Next
    End Sub

    Private Shared Sub ImprimirDetalle(objDetalleComprobante As IList(Of ClsDetalleComprobante))
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(40, "_"), 0, 100, 10, StringAlignment.Near, False))
        lineas.Add(New ClsLineaImpresion(1, "Descripcion", 0, 100, 10, StringAlignment.Near, False))
        lineas.Add(New ClsLineaImpresion(0, "Cantidad", 0, 24, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(0, "P/U", 24, 45, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, "Total", 69, 45, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(40, "_"), 0, 100, 10, StringAlignment.Near, False))
        For i As Integer = 0 To objDetalleComprobante.Count - 1
            If CDbl(objDetalleComprobante(i).strPrecio) > 0 Then
                Dim strLinea = objDetalleComprobante(i).strDescripcion
                While strLinea.Length > 33
                    lineas.Add(New ClsLineaImpresion(1, strLinea.Substring(0, 33), 0, 100, 10, StringAlignment.Near, False))
                    strLinea = strLinea.Substring(33)
                End While
                lineas.Add(New ClsLineaImpresion(1, strLinea, 0, 100, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strCantidad, 0, 24, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strPrecio, 24, 45, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strTotalLinea, 69, 45, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(1, objDetalleComprobante(i).strExcento, 99, 1, 10, StringAlignment.Far, False))
            End If
        Next
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(40, "_"), 0, 100, 10, StringAlignment.Near, False))
    End Sub

    Private Shared Sub ImprimirTotales(objComprobante As ClsComprobante)
        lineas.Add(New ClsLineaImpresion(0, "Sub-Total:", 0, 60, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strSubTotal, 61, 40, 10, StringAlignment.Far, False))
        If objComprobante.strDescuento <> "" Then
            lineas.Add(New ClsLineaImpresion(0, "Descuento:", 0, 60, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objComprobante.strDescuento, 61, 40, 10, StringAlignment.Far, False))
        End If
        lineas.Add(New ClsLineaImpresion(0, "Impuesto:", 0, 60, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strImpuesto, 61, 40, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(0, "Total:", 0, 60, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strTotal, 61, 40, 10, StringAlignment.Far, False))
    End Sub

    Public Shared Sub ImprimirFactura(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE DE FACTURA")
            If objImpresion.strClaveNumerica <> "" Then
                lineas.Add(New ClsLineaImpresion(1, "Clave numerica", 0, 100, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.strClaveNumerica.Substring(0, 25), 0, 100, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(2, objImpresion.strClaveNumerica.Substring(25, 25), 0, 100, 10, StringAlignment.Center, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "Factura Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Cliente: " & objImpresion.strNombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Pago con:", 0, 60, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 61, 40, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 60, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(3, objImpresion.strCambio, 61, 40, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "IMPUESTO DE VENTAS INCLUIDO", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(1, "AUTORIZADO MEDIANTE RESOLUCION NUMERO", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(1, "DGT-R-48-2016 DEL 07-OCT-2016", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a SendStringToPrinter:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirProforma(ByVal objImpresion As ClsComprobante)
        'Dim strFactura As String = ""
        'Dim lengthPerLine As Integer = objImpresion.equipo.LargoLineaTiquete
        'Try
        '    strFactura += ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE PROFORMA")
        '    strFactura += Chr(13) & Chr(10)
        '    strFactura += "  Proforma Nro: " & objImpresion.strId & Chr(13) & Chr(10)
        '    strFactura += "  Vendedor: " & objImpresion.strVendedor.Substring(0, If(objImpresion.strVendedor.Length < (lengthPerLine - 12), objImpresion.strVendedor.Length, (lengthPerLine - 12))) & Chr(13) & Chr(10)
        '    strFactura += "  Cliente: " & objImpresion.strNombre.Substring(0, If(objImpresion.strNombre.Length < (lengthPerLine - 11), objImpresion.strNombre.Length, (lengthPerLine - 11))) & Chr(13) & Chr(10)
        '    strFactura += "  Fecha: " & objImpresion.strFecha & Chr(13) & Chr(10)
        '    If objImpresion.strDocumento <> "" Then strFactura += "  Documento: " & objImpresion.strDocumento & Chr(13) & Chr(10)
        '    strFactura += Chr(13) & Chr(10)
        '    strFactura += ImprimirDetalle(objImpresion.arrDetalleComprobante, lengthPerLine)
        '    strFactura += "".PadRight(40, "_") & Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    strFactura += ImprimirTotales(objImpresion, lengthPerLine)
        '    strFactura += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    strFactura += "".PadLeft((lengthPerLine - 24) / 2, " ") & "GRACIAS POR PREFERIRNOS" & Chr(13) & Chr(10)
        '    strFactura += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'Catch ex As Exception
        '    Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        'End Try
        'Try
        '    SendStringToPrinter(objImpresion.equipo.ImpresoraFactura, strFactura)
        'Catch ex As Exception
        '    Throw New Exception("Error invokando a SendStringToPrinter:" + ex.Message)
        'End Try
    End Sub

    Public Shared Sub ImprimirApartado(ByVal objImpresion As ClsComprobante)
        'Dim strFactura As String = ""
        'Dim lengthPerLine As Integer = objImpresion.equipo.LargoLineaTiquete
        'Try
        '    strFactura += ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE APARTADO")
        '    strFactura += Chr(13) & Chr(10)
        '    strFactura += "  Apartado Nro: " & objImpresion.strId & Chr(13) & Chr(10)
        '    strFactura += "  Vendedor: " & objImpresion.strVendedor.Substring(0, If(objImpresion.strVendedor.Length < (lengthPerLine - 12), objImpresion.strVendedor.Length, (lengthPerLine - 12))) & Chr(13) & Chr(10)
        '    strFactura += "  Cliente: " & objImpresion.strNombre.Substring(0, If(objImpresion.strNombre.Length < (lengthPerLine - 11), objImpresion.strNombre.Length, (lengthPerLine - 11))) & Chr(13) & Chr(10)
        '    strFactura += "  Fecha: " & objImpresion.strFecha & Chr(13) & Chr(10)
        '    If objImpresion.strDocumento <> "" Then strFactura += "  Documento: " & objImpresion.strDocumento & Chr(13) & Chr(10)
        '    strFactura += Chr(13) & Chr(10)
        '    strFactura += ImprimirDesglosePago(objImpresion.arrDesglosePago, lengthPerLine)
        '    strFactura += "".PadRight(40, "_") & Chr(13) & Chr(10)
        '    strFactura += ImprimirDetalle(objImpresion.arrDetalleComprobante, lengthPerLine)
        '    strFactura += "".PadRight(40, "_") & Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    strFactura += ImprimirTotales(objImpresion, lengthPerLine)
        '    strFactura += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    strFactura += "".PadLeft((lengthPerLine - 24) / 2, " ") & "GRACIAS POR PREFERIRNOS" & Chr(13) & Chr(10)
        '    strFactura += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'Catch ex As Exception
        '    Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        'End Try
        'Try
        '    SendStringToPrinter(objImpresion.equipo.ImpresoraFactura, strFactura)
        'Catch ex As Exception
        '    Throw New Exception("Error invokando a SendStringToPrinter:" + ex.Message)
        'End Try
    End Sub

    Public Shared Sub ImprimirOrdenServicio(ByVal objImpresion As ClsComprobante)
        'Dim strFactura As String = ""
        'Dim lengthPerLine As Integer = objImpresion.equipo.LargoLineaTiquete
        'Try
        '    strFactura += ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "ORDEN DE SERVICIO")
        '    strFactura += Chr(13) & Chr(10)
        '    strFactura += "  Orden Nro: " & objImpresion.strId & Chr(13) & Chr(10)
        '    strFactura += "  Vendedor: " & objImpresion.strVendedor.Substring(0, If(objImpresion.strVendedor.Length < (lengthPerLine - 12), objImpresion.strVendedor.Length, (lengthPerLine - 12))) & Chr(13) & Chr(10)
        '    strFactura += "  Cliente: " & objImpresion.strNombre.Substring(0, If(objImpresion.strNombre.Length < (lengthPerLine - 11), objImpresion.strNombre.Length, (lengthPerLine - 11))) & Chr(13) & Chr(10)
        '    strFactura += "  Fecha: " & objImpresion.strFecha & Chr(13) & Chr(10)
        '    If objImpresion.strDocumento <> "" Then strFactura += "  Documento: " & objImpresion.strDocumento & Chr(13) & Chr(10)
        '    strFactura += Chr(13) & Chr(10)
        '    strFactura += ImprimirDesglosePago(objImpresion.arrDesglosePago, lengthPerLine)
        '    strFactura += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        '    strFactura += ImprimirDetalle(objImpresion.arrDetalleComprobante, lengthPerLine)
        '    strFactura += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    strFactura += ImprimirTotales(objImpresion, lengthPerLine)
        '    strFactura += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        '    Dim leyenda As String = objImpresion.empresa.LeyendaOrdenServicio
        '    While leyenda.Length > lengthPerLine - 2
        '        strFactura += " " & leyenda.Substring(0, lengthPerLine - 2) & Chr(13) & Chr(10)
        '        leyenda = leyenda.Substring(lengthPerLine - 2)
        '    End While
        '    If leyenda.Length > 0 Then strFactura += "".PadRight((lengthPerLine - 2 - leyenda.Length) / 2, " ") + leyenda & Chr(13) & Chr(10)
        '    strFactura += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'Catch ex As Exception
        '    Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        'End Try
        'Try
        '    SendStringToPrinter(objImpresion.equipo.ImpresoraFactura, strFactura)
        'Catch ex As Exception
        '    Throw New Exception("Error invokando a SendStringToPrinter:" + ex.Message)
        'End Try
    End Sub

    Public Shared Sub ImprimirCompra(ByVal objCompra As ClsComprobante)
        'Dim strCompra As String = ""
        'Dim lengthPerLine As Integer = objCompra.equipo.LargoLineaTiquete
        'strCompra += ImprimirEncabezado(objCompra.equipo, objCompra.empresa, Date.Now.ToString("dd-MM-yyyy"), objCompra.usuario.CodigoUsuario, "TIQUETE DE COMPRA")
        'strCompra += Chr(13) & Chr(10)
        'strCompra += "  Compra Nro: " & objCompra.strId & Chr(13) & Chr(10)
        'strCompra += "  Proveedor: " & objCompra.strNombre.Substring(0, If(objCompra.strNombre.Length < (lengthPerLine - 13), objCompra.strNombre.Length, (lengthPerLine - 13))) & Chr(13) & Chr(10)
        'strCompra += "  Fecha: " & objCompra.strFecha & Chr(13) & Chr(10)
        'strCompra += ImprimirDesglosePago(objCompra.arrDesglosePago, lengthPerLine)
        'strCompra += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strCompra += ImprimirDetalle(objCompra.arrDetalleComprobante, lengthPerLine)
        'strCompra += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strCompra += ImprimirTotales(objCompra, lengthPerLine)
        'strCompra += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objCompra.equipo.ImpresoraFactura, strCompra)
    End Sub

    Public Shared Sub ImprimirDevolucionCliente(ByVal objDevolucion As ClsComprobante)
        'Dim strDevolucion As String = ""
        'Dim lengthPerLine As Integer = objDevolucion.equipo.LargoLineaTiquete
        'strDevolucion += ImprimirEncabezado(objDevolucion.equipo, objDevolucion.empresa, Date.Now.ToString("dd-MM-yyyy"), objDevolucion.usuario.CodigoUsuario, "DEVOLUCION DE MERCANCIA CLIENTES")
        'strDevolucion += Chr(13) & Chr(10)
        'strDevolucion += "  Movimiento Nro: " & objDevolucion.strId & Chr(13) & Chr(10)
        'strDevolucion += "  Factura Nro: " & objDevolucion.strDocumento & Chr(13) & Chr(10)
        'strDevolucion += "  Cliente: " & objDevolucion.strNombre.Substring(0, If(objDevolucion.strNombre.Length < (lengthPerLine - 11), objDevolucion.strNombre.Length, (lengthPerLine - 11))) & Chr(13) & Chr(10)
        'strDevolucion += "  Fecha: " & objDevolucion.strFecha & Chr(13) & Chr(10)
        'strDevolucion += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strDevolucion += ImprimirDetalle(objDevolucion.arrDetalleComprobante, lengthPerLine)
        'strDevolucion += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strDevolucion += ImprimirTotales(objDevolucion, lengthPerLine)
        'strDevolucion += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        'strDevolucion += "  Recibido por: __________________________" & Chr(13) & Chr(10)
        'strDevolucion += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objDevolucion.equipo.ImpresoraFactura, strDevolucion)
    End Sub

    Public Shared Sub ImprimirDevolucionProveedor(ByVal objDevolucion As ClsComprobante)
        'Dim strDevolucion As String = ""
        'Dim lengthPerLine As Integer = objDevolucion.equipo.LargoLineaTiquete
        'strDevolucion += ImprimirEncabezado(objDevolucion.equipo, objDevolucion.empresa, Date.Now.ToString("dd-MM-yyyy"), objDevolucion.usuario.CodigoUsuario, "DEVOLUCION DE MERCANCIA PROVEEDOR")
        'strDevolucion += Chr(13) & Chr(10)
        'strDevolucion += "  Movimiento Nro: " & objDevolucion.strId & Chr(13) & Chr(10)
        'strDevolucion += "  Compra Nro: " & objDevolucion.strDocumento & Chr(13) & Chr(10)
        'strDevolucion += "  Proveedor: " & objDevolucion.strNombre.Substring(0, If(objDevolucion.strNombre.Length < (lengthPerLine - 13), objDevolucion.strNombre.Length, (lengthPerLine - 13))) & Chr(13) & Chr(10)
        'strDevolucion += "  Fecha: " & objDevolucion.strFecha & Chr(13) & Chr(10)
        'strDevolucion += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strDevolucion += ImprimirDetalle(objDevolucion.arrDetalleComprobante, lengthPerLine)
        'strDevolucion += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strDevolucion += ImprimirTotales(objDevolucion, lengthPerLine)
        'strDevolucion += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        'strDevolucion += "  Recibido por: __________________________" & Chr(13) & Chr(10)
        'strDevolucion += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objDevolucion.equipo.ImpresoraFactura, strDevolucion)
    End Sub

    Public Shared Sub ImprimirTraslado(ByVal objTraslado As ClsComprobante)
        'Dim strTraslado As String = ""
        'Dim lengthPerLine As Integer = objTraslado.equipo.LargoLineaTiquete
        'strTraslado += ImprimirEncabezado(objTraslado.equipo, objTraslado.empresa, Date.Now.ToString("dd-MM-yyyy"), objTraslado.usuario.CodigoUsuario, "TRASLADO DE MERCANCIA")
        'strTraslado += Chr(13) & Chr(10)
        'strTraslado += "  Traslado Nro: " & objTraslado.strId & Chr(13) & Chr(10)
        'strTraslado += "  Fecha: " & objTraslado.strFecha & Chr(13) & Chr(10)
        'strTraslado += "  Sucursal Origen: " & objTraslado.strNombre & Chr(13) & Chr(10)
        'strTraslado += "  Sucursal Destino: " & objTraslado.strFormaPago & Chr(13) & Chr(10)
        'strTraslado += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strTraslado += ImprimirDetalle(objTraslado.arrDetalleComprobante, lengthPerLine)
        'strTraslado += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strTraslado += "Total:".PadLeft(lengthPerLine - 18, " ") & objTraslado.strTotal.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        'strTraslado += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        'strTraslado += "  Enviado por: " & objTraslado.strEnviadoPor & Chr(13) & Chr(10)
        'strTraslado += Chr(13) & Chr(10)
        'strTraslado += "  Recibido por: __________________________" & Chr(13) & Chr(10)
        'strTraslado += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objTraslado.equipo.ImpresoraFactura, strTraslado)
    End Sub

    Public Shared Sub ImprimirReciboCxC(ByVal objReciboCxC As ClsRecibo)
        'Dim strRecibo As String = ""
        'Dim lengthPerLine As Integer = objReciboCxC.equipo.LargoLineaTiquete
        'Dim i As Integer
        'strRecibo += ImprimirEncabezado(objReciboCxC.equipo, objReciboCxC.empresa, Date.Now.ToString("dd-MM-yyyy"), objReciboCxC.usuario.CodigoUsuario, "RECIBO CUENTA POR COBRAR")
        'strRecibo += Chr(13) & Chr(10)
        'strRecibo += "  Consecutivo: " & objReciboCxC.strConsecutivo & Chr(13) & Chr(10)
        'strRecibo += "  Cliente: " & objReciboCxC.strNombre.Substring(0, If(objReciboCxC.strNombre.Length < (lengthPerLine - 11), objReciboCxC.strNombre.Length, (lengthPerLine - 11))) & Chr(13) & Chr(10)
        'strRecibo += "  Fecha: " & objReciboCxC.strFechaAbono & Chr(13) & Chr(10)
        'strRecibo += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strRecibo += "  Desglose Cuentas por Cobrar Abonadas:" & Chr(13) & Chr(10)
        'For i = 0 To objReciboCxC.arrDesgloseMov.Count - 1
        '    strRecibo += "  Cuenta: " & objReciboCxC.arrDesgloseMov(i).strDescripcion.PadRight(15).Substring(0, lengthPerLine - 19) & " Monto: " & objReciboCxC.arrDesgloseMov(i).strMonto.PadLeft(17, " ") & Chr(13) & Chr(10)
        'Next
        'strRecibo += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strRecibo += ImprimirDesglosePago(objReciboCxC.arrDesglosePago, lengthPerLine)
        'strRecibo += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        'strRecibo += "Monto Total:".PadLeft(lengthPerLine - 18, " ") & objReciboCxC.strTotalAbono.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        'strRecibo += "".PadLeft((lengthPerLine - 24) / 2, " ") & "GRACIAS POR PREFERIRNOS" & Chr(13) & Chr(10)
        'strRecibo += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objReciboCxC.equipo.ImpresoraFactura, strRecibo)
    End Sub

    Public Shared Sub ImprimirReciboCxP(ByVal objReciboCxP As ClsRecibo)
        'Dim strRecibo As String = ""
        'Dim lengthPerLine As Integer = objReciboCxP.equipo.LargoLineaTiquete
        'strRecibo += ImprimirEncabezado(objReciboCxP.equipo, objReciboCxP.empresa, Date.Now.ToString("dd-MM-yyyy"), objReciboCxP.usuario.CodigoUsuario, "RECIBO CUENTA POR PAGAR")
        'strRecibo += Chr(13) & Chr(10)
        'strRecibo += "  Consecutivo: " & objReciboCxP.strConsecutivo & Chr(13) & Chr(10)
        'strRecibo += "  Recibo Nro: " & objReciboCxP.strRecibo & Chr(13) & Chr(10)
        'strRecibo += "  Proveedor: " & objReciboCxP.strNombre.Substring(0, If(objReciboCxP.strNombre.Length < 29, objReciboCxP.strNombre.Length, 29)) & Chr(13) & Chr(10)
        'strRecibo += "  Fecha: " & objReciboCxP.strFechaAbono & Chr(13) & Chr(10)
        'strRecibo += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strRecibo += "  Desglose Cuentas por Pagar Abonadas:" & Chr(13) & Chr(10)
        'For i As Integer = 0 To objReciboCxP.arrDesgloseMov.Count - 1
        '    strRecibo += "  Cuenta: " & objReciboCxP.arrDesgloseMov(i).strDescripcion.PadRight(15).Substring(0, lengthPerLine - 19) & " Monto: " & objReciboCxP.arrDesgloseMov(i).strMonto.PadLeft(17, " ") & Chr(13) & Chr(10)
        'Next
        'strRecibo += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strRecibo += ImprimirDesglosePago(objReciboCxP.arrDesglosePago, lengthPerLine)
        'strRecibo += Chr(13) & Chr(10) & Chr(13) & Chr(10)
        'strRecibo += "Monto Total:".PadLeft(lengthPerLine - 18, " ") & objReciboCxP.strTotalAbono.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        'strRecibo += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objReciboCxP.equipo.ImpresoraFactura, strRecibo)
    End Sub

    Public Shared Sub ImprimirEgreso(ByVal objEgreso As ClsEgreso)
        'Dim strEgreso As String = ""
        'Dim lengthPerLine As Integer = objEgreso.equipo.LargoLineaTiquete
        'strEgreso += ImprimirEncabezado(objEgreso.equipo, objEgreso.empresa, Date.Now.ToString("dd-MM-yyyy"), objEgreso.usuario.CodigoUsuario, "COMPROBANTE DE EGRESO")
        'strEgreso += Chr(13) & Chr(10)
        'strEgreso += "  Egreso Nro: " & objEgreso.strId & Chr(13) & Chr(10)
        'strEgreso += "  Pagado a: " & objEgreso.strBeneficiario.Substring(0, If(objEgreso.strBeneficiario.Length < (lengthPerLine - 15), objEgreso.strBeneficiario.Length, (lengthPerLine - 15))) & Chr(13) & Chr(10)
        'strEgreso += "  La suma de: " & objEgreso.strMonto.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        'strEgreso += Chr(13) & Chr(10)
        'Dim lineas As New List(Of String)
        'Dim strCadena = "  Concepto: " & objEgreso.strConcepto
        'For j As Integer = 0 To strCadena.Length() - 1 Step lengthPerLine - 4
        '    If strCadena.Length() < lengthPerLine - 4 Then
        '        lineas.Add(strCadena)
        '    Else
        '        lineas.Add(strCadena.Substring(0, lengthPerLine - 4))
        '        strCadena = strCadena.Substring(lengthPerLine - 4)
        '    End If
        'Next
        'For Each strLinea As String In lineas
        '    strEgreso += "  " & strLinea & Chr(13) & Chr(10)
        'Next
        'strEgreso += Chr(13) & Chr(10)
        'strEgreso += ImprimirDesglosePago(objEgreso.arrDesglosePago, lengthPerLine)
        'strEgreso += Chr(13) & Chr(10)
        'strEgreso += "  Recibido por: ________________________"
        'strEgreso += Chr(13) & Chr(10)
        'strEgreso += Chr(13) & Chr(10)
        'strEgreso += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objEgreso.equipo.ImpresoraFactura, strEgreso)
    End Sub

    Public Shared Sub ImprimirIngreso(ByVal objIngreso As ClsIngreso)
        'Dim strIngreso As String = ""
        'Dim lengthPerLine As Integer = objIngreso.equipo.LargoLineaTiquete
        'strIngreso += ImprimirEncabezado(objIngreso.equipo, objIngreso.empresa, Date.Now.ToString("dd-MM-yyyy"), objIngreso.usuario.CodigoUsuario, "COMPROBANTE DE INGRESO")
        'strIngreso += Chr(13) & Chr(10)
        'strIngreso += "  Ingreso Nro: " & objIngreso.strId & Chr(13) & Chr(10)
        'strIngreso += "  Recibo de: " & objIngreso.strRecibidoDe.Substring(0, If(objIngreso.strRecibidoDe.Length < (lengthPerLine - 15), objIngreso.strRecibidoDe.Length, (lengthPerLine - 15))) & Chr(13) & Chr(10)
        'strIngreso += "  La suma de: " & objIngreso.strMonto.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        'strIngreso += Chr(13) & Chr(10)
        'Dim lineas As New List(Of String)
        'Dim strCadena = "  Concepto: " & objIngreso.strConcepto
        'For j As Integer = 0 To strCadena.Length() - 1 Step lengthPerLine - 4
        '    If strCadena.Length() < lengthPerLine - 4 Then
        '        lineas.Add(strCadena)
        '    Else
        '        lineas.Add(strCadena.Substring(0, lengthPerLine - 4))
        '        strCadena = strCadena.Substring(lengthPerLine - 4)
        '    End If
        'Next
        'For Each strLinea As String In lineas
        '    strIngreso += "  " & strLinea & Chr(13) & Chr(10)
        'Next
        'strIngreso += Chr(13) & Chr(10)
        'strIngreso += ImprimirDesglosePago(objIngreso.arrDesglosePago, lengthPerLine)
        'strIngreso += Chr(13) & Chr(10)
        'strIngreso += "  Recibido por: ________________________"
        'strIngreso += Chr(13) & Chr(10)
        'strIngreso += Chr(13) & Chr(10)
        'strIngreso += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objIngreso.equipo.ImpresoraFactura, strIngreso)
    End Sub

    Public Shared Sub ImprimirCuentaPorPagar(ByVal objCuenta As ClsCuentaPorPagar)
        'Dim strCuenta As String = ""
        'Dim lengthPerLine As Integer = objCuenta.equipo.LargoLineaTiquete
        'strCuenta += ImprimirEncabezado(objCuenta.equipo, objCuenta.empresa, Date.Now.ToString("dd-MM-yyyy"), objCuenta.usuario.CodigoUsuario, "COMPROBANTE DE CUENTA POR PAGAR")
        'strCuenta += Chr(13) & Chr(10)
        'strCuenta += "  Cuenta Nro: " & objCuenta.strId & Chr(13) & Chr(10)
        'strCuenta += "  Recibo de: " & objCuenta.strRecibidoDe.Substring(0, If(objCuenta.strRecibidoDe.Length < (lengthPerLine - 15), objCuenta.strRecibidoDe.Length, (lengthPerLine - 15))) & Chr(13) & Chr(10)
        'strCuenta += "  La suma de: " & objCuenta.strMonto.ToString.PadLeft(17, " ") & Chr(13) & Chr(10)
        'strCuenta += Chr(13) & Chr(10)
        'Dim lineas As New List(Of String)
        'Dim strCadena = "  Concepto: " & objCuenta.strConcepto
        'For j As Integer = 0 To strCadena.Length() - 1 Step lengthPerLine - 4
        '    If strCadena.Length() < lengthPerLine - 4 Then
        '        lineas.Add(strCadena)
        '    Else
        '        lineas.Add(strCadena.Substring(0, lengthPerLine - 4))
        '        strCadena = strCadena.Substring(lengthPerLine - 4)
        '    End If
        'Next
        'For Each strLinea As String In lineas
        '    strCuenta += "  " & strLinea & Chr(13) & Chr(10)
        'Next
        'strCuenta += Chr(13) & Chr(10)
        'strCuenta += ImprimirDesglosePago(objCuenta.arrDesglosePago, lengthPerLine)
        'strCuenta += Chr(13) & Chr(10)
        'strCuenta += "  Recibido por: ________________________"
        'strCuenta += Chr(13) & Chr(10)
        'strCuenta += Chr(13) & Chr(10)
        'strCuenta += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objCuenta.equipo.ImpresoraFactura, strCuenta)
    End Sub

    Public Shared Sub ImprimirAjusteInventario(ByVal objAjuste As ClsAjusteInventario)
        'Dim strAjusteInventario As String = ""
        'Dim lengthPerLine As Integer = objAjuste.equipo.LargoLineaTiquete
        'strAjusteInventario += ImprimirEncabezado(objAjuste.equipo, objAjuste.empresa, Date.Now.ToString("dd-MM-yyyy"), objAjuste.usuario.CodigoUsuario, "AJUSTE DE INVENTARIO")
        'strAjusteInventario += Chr(13) & Chr(10)
        'strAjusteInventario += "  Movimiento Nro: " & objAjuste.strId & Chr(13) & Chr(10)
        'Dim lineas As New List(Of String)
        'Dim strCadena = "  Concepto: " & objAjuste.strDescripcion
        'For j As Integer = 0 To strCadena.Length() - 1 Step lengthPerLine - 4
        '    If strCadena.Length() < lengthPerLine - 4 Then
        '        lineas.Add(strCadena)
        '    Else
        '        lineas.Add(strCadena.Substring(0, lengthPerLine - 4))
        '        strCadena = strCadena.Substring(lengthPerLine - 4)
        '    End If
        'Next
        'For Each strLinea As String In lineas
        '    strAjusteInventario += "  " & strLinea & Chr(13) & Chr(10)
        'Next
        'strAjusteInventario += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strAjusteInventario += ImprimirDetalle(objAjuste.arrDetalleComprobante, lengthPerLine)
        'strAjusteInventario += "".PadRight(lengthPerLine, "_") & Chr(13) & Chr(10)
        'strAjusteInventario += "  Revisado por: ________________________" & Chr(13) & Chr(10)
        'strAjusteInventario += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objAjuste.equipo.ImpresoraFactura, strAjusteInventario)
    End Sub
#End Region
End Class
