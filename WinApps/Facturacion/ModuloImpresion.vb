Imports System.Runtime.InteropServices
Imports System.Collections.Generic
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Drawing.Printing
Imports LeandroSoftware.Core.TiposComunes

Public Class ModuloImpresion
#Region "Variables"
    Private Shared lineas As IList = New List(Of ClsLineaImpresion)
    Private Class ClsLineaImpresion
        Public Sub New(saltos As Integer, texto As String, posicionX As Single, ancho As Single, fuente As Integer, alineado As StringAlignment, bold As Boolean)
            intSaltos = saltos
            strTexto = texto
            intPosicionX = posicionX
            intAncho = ancho
            intFuente = fuente
            agtAlineado = alineado
            bolBold = bold
        End Sub

        Public intSaltos As Integer
        Public strTexto As String
        Public intPosicionX As Single
        Public intAncho As Single
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
        Public strTelefono As String
        Public strDireccion As String
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
        Public Sub New(strDesc As String, strValue As String)
            strDescripcion = strDesc
            strMonto = strValue
        End Sub

        Public strDescripcion As String
        Public strMonto As String
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
        Dim i As Integer = 0
        Dim graphics As Graphics = e.Graphics
        Dim positionY As Integer = 0
        Dim sf As StringFormat = New StringFormat()
        While i < lineas.Count
            Dim linea As ClsLineaImpresion = lineas(i)
            Dim fontStyle As FontStyle = IIf(linea.bolBold, FontStyle.Bold, FontStyle.Regular)
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = linea.agtAlineado
            Dim rec As RectangleF = New RectangleF()
            rec.Width = linea.intAncho
            rec.Height = 18
            rec.X = linea.intPosicionX
            rec.Y = positionY
            graphics.DrawString(linea.strTexto, New Font("Lucida Sans Unicode", linea.intFuente, fontStyle), New SolidBrush(Color.Black), rec, sf)
            positionY += (20 * linea.intSaltos)
            i += 1
        End While
    End Sub
#End Region

#Region "Métodos"
    Private Shared Sub ImprimirEncabezado(objEquipo As EquipoRegistrado, objEmpresa As Empresa, strFecha As String, strCodigoUsuario As String, strTitulo As String)
        Dim esMatriz = objEquipo.ImpresoraMatriz
        lineas.Add(New ClsLineaImpresion(0, strFecha, 0, 143, 10, StringAlignment.Near, True))
        lineas.Add(New ClsLineaImpresion(2, strCodigoUsuario, 143, 143, 10, StringAlignment.Far, True))
        lineas.Add(New ClsLineaImpresion(1, strTitulo, 0, 286, 14, StringAlignment.Center, True))
        Dim strNombreComercial As String = objEmpresa.NombreComercial
        While strNombreComercial.Length > 30
            lineas.Add(New ClsLineaImpresion(1, strNombreComercial.Substring(0, 30), 0, 286, 12, StringAlignment.Center, True))
            strNombreComercial = strNombreComercial.Substring(30)
        End While
        lineas.Add(New ClsLineaImpresion(2, strNombreComercial, 0, 286, 12, StringAlignment.Center, True))
        Dim strDireccion As String = objEquipo.DireccionSucursal
        While strDireccion.Length > 34
            lineas.Add(New ClsLineaImpresion(1, strDireccion.Substring(0, 34), 0, 286, 10, StringAlignment.Center, False))
            strDireccion = strDireccion.Substring(34)
        End While
        lineas.Add(New ClsLineaImpresion(1, strDireccion, 0, 286, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(1, "TELEFONO: " + objEquipo.TelefonoSucursal, 0, 286, 10, StringAlignment.Center, False))
        Dim strNombreEmpresa As String = objEmpresa.NombreEmpresa
        While strNombreEmpresa.Length > 34
            lineas.Add(New ClsLineaImpresion(1, strNombreEmpresa.Substring(0, 34), 0, 286, 10, StringAlignment.Center, False))
            strNombreEmpresa = strNombreEmpresa.Substring(34)
        End While
        lineas.Add(New ClsLineaImpresion(1, strNombreEmpresa, 0, 286, 10, StringAlignment.Center, False))
        Dim strIdentificacion As String = objEmpresa.Identificacion
        If objEmpresa.Identificacion.Length > 34 Then strIdentificacion = strIdentificacion.Substring(0, 34)
        lineas.Add(New ClsLineaImpresion(1, strIdentificacion, 0, 286, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(2, objEmpresa.CorreoNotificacion, 0, 286, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(1, objEquipo.NombreSucursal, 0, 286, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(2, "Terminal: " & objEquipo.IdTerminal, 0, 286, 10, StringAlignment.Center, False))
    End Sub

    Private Shared Sub ImprimirDesglosePago(objDesglosePago As IList(Of ClsDesgloseFormaPago))
        lineas.Add(New ClsLineaImpresion(1, "Desglose Forma de pago", 0, 286, 10, StringAlignment.Center, False))
        For i As Integer = 0 To objDesglosePago.Count - 1
            lineas.Add(New ClsLineaImpresion(0, objDesglosePago(i).strDescripcion, 0, 155, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objDesglosePago(i).strMonto, 155, 131, 10, StringAlignment.Far, False))
        Next
    End Sub

    Private Shared Sub ImprimirDetalle(objDetalleComprobante As IList(Of ClsDetalleComprobante))
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
        lineas.Add(New ClsLineaImpresion(1, "Descripcion", 0, 286, 10, StringAlignment.Near, False))
        lineas.Add(New ClsLineaImpresion(0, "Cant", 0, 43, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(0, "P/U", 43, 114, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, "Total", 157, 114, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
        For i As Integer = 0 To objDetalleComprobante.Count - 1
            If CDbl(objDetalleComprobante(i).strPrecio) > 0 Then
                Dim strLinea As String = objDetalleComprobante(i).strDescripcion
                While strLinea.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, strLinea.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    strLinea = strLinea.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(1, strLinea, 0, 286, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strCantidad, 0, 43, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strPrecio, 43, 114, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strTotalLinea, 157, 114, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(1, objDetalleComprobante(i).strExcento, 271, 15, 10, StringAlignment.Far, False))
            End If
        Next
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
    End Sub

    Private Shared Sub ImprimirTotales(objComprobante As ClsComprobante)
        lineas.Add(New ClsLineaImpresion(0, "Sub-Total:", 0, 155, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strSubTotal, 155, 131, 10, StringAlignment.Far, False))
        If objComprobante.strDescuento <> "" Then
            lineas.Add(New ClsLineaImpresion(0, "Descuento:", 0, 155, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objComprobante.strDescuento, 155, 131, 10, StringAlignment.Far, False))
        End If
        lineas.Add(New ClsLineaImpresion(0, "Impuesto:", 0, 155, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strImpuesto, 155, 131, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(0, "Total:", 0, 155, 10, StringAlignment.Far, True))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strTotal, 155, 131, 10, StringAlignment.Far, True))
    End Sub

    Public Shared Sub ImprimirFactura(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE DE FACTURA")
            If objImpresion.strClaveNumerica <> "" Then
                lineas.Add(New ClsLineaImpresion(1, "Clave numerica", 0, 286, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.strClaveNumerica.Substring(0, 25), 0, 286, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(2, objImpresion.strClaveNumerica.Substring(25, 25), 0, 286, 10, StringAlignment.Center, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "Factura Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Pago con:", 0, 155, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 155, 131, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 155, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(3, objImpresion.strCambio, 155, 131, 10, StringAlignment.Far, False))
            If objImpresion.empresa.LeyendaFactura.Length > 0 Then
                Dim leyenda As String = objImpresion.empresa.LeyendaFactura
                While leyenda.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    leyenda = leyenda.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 286, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "AUTORIZADO MEDIANTE RESOLUCION NUMERO", 0, 286, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(1, "DGT-R-48-2016 DEL 07-OCT-2016", 0, 286, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirProforma(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE PROFORMA")
            lineas.Add(New ClsLineaImpresion(1, "Proforma Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            If objImpresion.empresa.LeyendaProforma.Length > 0 Then
                Dim leyenda As String = objImpresion.empresa.LeyendaProforma
                While leyenda.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    leyenda = leyenda.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 286, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirApartado(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE APARTADO")
            lineas.Add(New ClsLineaImpresion(1, "Apartado Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Abono inicial:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 155, 131, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo por pagar:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(3, objImpresion.strCambio, 155, 131, 10, StringAlignment.Far, True))
            If objImpresion.empresa.LeyendaApartado.Length > 0 Then
                Dim leyenda As String = objImpresion.empresa.LeyendaApartado
                While leyenda.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    leyenda = leyenda.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 286, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirOrdenServicio(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "ORDEN DE SERVICIO")
            lineas.Add(New ClsLineaImpresion(1, "Orden Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Entrega: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, True))
            Dim direccion As String = objImpresion.strDireccion
            If direccion.Length > 25 Then
                lineas.Add(New ClsLineaImpresion(1, "Dirección: " & direccion.Substring(0, 25), 0, 286, 10, StringAlignment.Near, False))
                direccion = direccion.Substring(25)
                While direccion.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, direccion.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    direccion = direccion.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(2, direccion, 0, 286, 10, StringAlignment.Near, False))
            ElseIf direccion.Length > 0 Then
                lineas.Add(New ClsLineaImpresion(2, "Dirección: " & direccion, 0, 286, 10, StringAlignment.Near, False))
            Else
                lineas.Add(New ClsLineaImpresion(2, "Dirección: NO HAY DIRECCION", 0, 286, 10, StringAlignment.Near, False))
            End If
            Dim notas As String = objImpresion.strDocumento
            If notas.Length > 27 Then
                lineas.Add(New ClsLineaImpresion(1, "Notas: " & notas.Substring(0, 27), 0, 286, 10, StringAlignment.Near, False))
                notas = notas.Substring(27)
                While notas.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, notas.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    notas = notas.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(2, notas, 0, 286, 10, StringAlignment.Near, False))
            ElseIf notas.Length > 0 Then
                lineas.Add(New ClsLineaImpresion(2, "Notas: " & notas, 0, 286, 10, StringAlignment.Near, False))
            Else
                lineas.Add(New ClsLineaImpresion(2, "Notas:", 0, 286, 10, StringAlignment.Near, False))
            End If
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Abono inicial:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 155, 131, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo por pagar:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(3, objImpresion.strCambio, 155, 131, 10, StringAlignment.Far, True))
            If objImpresion.empresa.LeyendaOrdenServicio.Length > 0 Then
                Dim leyenda As String = objImpresion.empresa.LeyendaOrdenServicio
                While leyenda.Length > 34
                    lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                    leyenda = leyenda.Substring(34)
                End While
                lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 286, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(2, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
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
        'strCompra += ImprimirDetalle(objCompra.arrDetalleComprobante, lengthPerLine)
        'strCompra += ImprimirTotales(objCompra, lengthPerLine)
        'strCompra += Chr(&HA) & Chr(&H1D) & "V" & Chr(66) & Chr(0)
        'SendStringToPrinter(objCompra.equipo.ImpresoraFactura, strCompra)
    End Sub

    Public Shared Sub ImprimirDevolucionCliente(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "DEVOLUCION CLIENTE")
            lineas.Add(New ClsLineaImpresion(1, "Mov. Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fact Nro: " & objImpresion.strDocumento, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirDevolucionProveedor(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "DEVOLUCION PROVEEDOR")
            lineas.Add(New ClsLineaImpresion(1, "Mov. Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Compra Nro: " & objImpresion.strDocumento, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Proveedor: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(3, "Recibido por: __________________________", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirTraslado(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TRASLADO DE MERCANCIA")
            lineas.Add(New ClsLineaImpresion(1, "Traslado Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFecha, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Destino: " & objImpresion.strFormaPago, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Enviado por: " & objImpresion.strEnviadoPor, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            lineas.Add(New ClsLineaImpresion(2, "Total: " & objImpresion.strTotal, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(3, "Recibido por: __________________________", 0, 286, 10, StringAlignment.Near, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirReciboCxC(ByVal objImpresion As ClsRecibo)
        lineas.Clear()
        Dim i As Integer
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO CXC")
            lineas.Add(New ClsLineaImpresion(1, "Consecutivo: " & objImpresion.strConsecutivo, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Desglose CxC Abonadas", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
            For i = 0 To objImpresion.arrDesgloseMov.Count - 1
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDesgloseMov(i).strDescripcion, 0, 155, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDesgloseMov(i).strMonto, 155, 131, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(0, "Monto Total:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalAbono, 155, 131, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirReciboCxP(ByVal objImpresion As ClsRecibo)
        lineas.Clear()
        Dim i As Integer
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO CXP")
            lineas.Add(New ClsLineaImpresion(1, "Consecutivo: " & objImpresion.strConsecutivo, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Proveedor: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Desglose CxP Abonadas", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
            For i = 0 To objImpresion.arrDesgloseMov.Count - 1
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDesgloseMov(i).strDescripcion, 0, 155, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDesgloseMov(i).strMonto, 155, 131, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(42, "_"), 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(0, "Monto Total:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalAbono, 155, 131, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirReciboApartado(ByVal objImpresion As ClsRecibo)
        lineas.Clear()
        Dim i As Integer
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO APARTADO")
            lineas.Add(New ClsLineaImpresion(1, "Consecutivo: " & objImpresion.strConsecutivo, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(0, "Monto Total:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalAbono, 155, 131, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirReciboOrdenServicio(ByVal objImpresion As ClsRecibo)
        lineas.Clear()
        Dim i As Integer
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO ORDEN SERV")
            lineas.Add(New ClsLineaImpresion(1, "Consecutivo: " & objImpresion.strConsecutivo, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Cliente: " & objImpresion.strNombre, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(0, "Monto Total:", 0, 155, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalAbono, 155, 131, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 286, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirEgreso(ByVal objImpresion As ClsEgreso)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "SALIDA DE EFECTIVO")
            lineas.Add(New ClsLineaImpresion(1, "Egreso Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Pagado a: " & objImpresion.strBeneficiario, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "La suma de: " & objImpresion.strMonto, 0, 286, 10, StringAlignment.Near, False))
            Dim concepto As String = "Concepto: " & objImpresion.strConcepto
            While concepto.Length > 34
                lineas.Add(New ClsLineaImpresion(1, concepto.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                concepto = concepto.Substring(34)
            End While
            lineas.Add(New ClsLineaImpresion(2, concepto, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(3, "Recibido por: __________________________", 0, 286, 10, StringAlignment.Near, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirIngreso(ByVal objImpresion As ClsIngreso)
        lineas.Clear()
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "INGRESO DE EFECTIVO")
            lineas.Add(New ClsLineaImpresion(1, "Ingreso Nro: " & objImpresion.strId, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibo de: " & objImpresion.strRecibidoDe, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "La suma de: " & objImpresion.strMonto, 0, 286, 10, StringAlignment.Near, False))
            Dim concepto As String = "Concepto: " & objImpresion.strConcepto
            While concepto.Length > 34
                lineas.Add(New ClsLineaImpresion(1, concepto.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                concepto = concepto.Substring(34)
            End While
            lineas.Add(New ClsLineaImpresion(2, concepto, 0, 286, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(3, "Recibido por: __________________________", 0, 286, 10, StringAlignment.Near, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirCierreEfectivo(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        Try
            lineas.Add(New ClsLineaImpresion(0, Now.ToString("dd/MM/yyyy"), 0, 143, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.usuario.CodigoUsuario, 143, 143, 10, StringAlignment.Far, True))
            Dim strNombreComercial As String = objImpresion.empresa.NombreComercial
            While strNombreComercial.Length > 30
                lineas.Add(New ClsLineaImpresion(1, strNombreComercial.Substring(0, 30), 0, 286, 12, StringAlignment.Center, True))
                strNombreComercial = strNombreComercial.Substring(30)
            End While
            lineas.Add(New ClsLineaImpresion(2, strNombreComercial, 0, 286, 12, StringAlignment.Center, True))
            lineas.Add(New ClsLineaImpresion(1, "CIERRE DE EFECTIVO", 0, 286, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 20, 266, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Detalle de Ingresos", 0, 286, 10, StringAlignment.Center, False))
            For i As Integer = 0 To objImpresion.arrDesglosePago.Count - 1
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDesglosePago(i).strDescripcion, 0, 155, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDesglosePago(i).strMonto, 155, 131, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(0, "Total de ingresos", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strDescuento, 0, 286, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "Detalle de Egresos", 0, 286, 10, StringAlignment.Center, False))
            For i As Integer = 0 To objImpresion.arrDetalleComprobante.Count - 1
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDetalleComprobante(i).strDescripcion, 0, 155, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDetalleComprobante(i).strTotalLinea, 155, 131, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(0, "Total de egresos", 0, 155, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strImpuesto, 155, 131, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cierre de efectivo", 0, 286, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strClaveNumerica, 0, 286, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Efectivo en caja", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strNombre, 0, 286, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Diferencia", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strDireccion, 0, 286, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Total de entrega", 0, 286, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strCambio, 0, 286, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Efectivo en caja", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strPagoCon, 0, 286, 10, StringAlignment.Far, False))
            Dim observaciones As String = "Nota: " & objImpresion.strDocumento
            While observaciones.Length > 34
                lineas.Add(New ClsLineaImpresion(1, observaciones.Substring(0, 34), 0, 286, 10, StringAlignment.Near, False))
                observaciones = observaciones.Substring(34)
            End While
            lineas.Add(New ClsLineaImpresion(2, observaciones, 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Procesado por: __________________________", 0, 286, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, ".", 0, 286, 10, StringAlignment.Near, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub
#End Region
End Class
