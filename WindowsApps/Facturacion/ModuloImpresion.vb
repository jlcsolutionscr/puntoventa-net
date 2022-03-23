Imports System.Collections.Generic
Imports LeandroSoftware.Common.Dominio.Entidades
Imports System.Drawing.Printing
Imports LeandroSoftware.Common.DatosComunes

Public Class ModuloImpresion
#Region "Variables"
    Private Shared lineas As IList = New List(Of ClsLineaImpresion)
    Private Shared charCount As Integer

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
        Public strIdCuenta As String
        Public strNombre As String
        Public strRecibo As String
        Public strFechaAbono As String
        Public strSaldoAnterior As String
        Public strTotalAbono As String
        Public strSaldoActual As String
        Public strPagoCon As String
        Public strCambio As String
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

    Public Class ClsCierreCaja
        Public empresa As Empresa
        Public equipo As EquipoRegistrado
        Public usuario As Usuario
        Public strFecha As String
        Public strTotalIngresos As String
        Public strTotalEgresos As String
        Public strTotalEfectivo As String
        Public strEfectivoCaja As String
        Public strSobrante As String
        Public strRetiroEfectivo As String
        Public strCierreEfectivoProx As String
        Public strObservaciones As String
        Public strVentasEfectivo As String
        Public strVentasTarjeta As String
        Public strVentasTransferencia As String
        Public strVentasCredito As String
        Public strTotalVentas As String
        Public strAdelantosEfectivo As String
        Public strAdelantosTarjeta As String
        Public strAdelantosTransferencia As String
        Public strTotalAdelantos As String
        Public arrDetalleIngresos As IList(Of ClsDesgloseFormaPago)
        Public arrDetalleEgresos As IList(Of ClsDesgloseFormaPago)
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
        Public strDescripcion As String
        Public strDetalle As String
        Public strDocumento As String
        Public strFormaPago As String
        Public strEnviadoPor As String
        Public strFecha As String
        Public strSubTotal As String
        Public strDescuento As String
        Public strImpuesto As String
        Public strTotal As String
        Public strAdelanto As String
        Public strSaldo As String
        Public strPagoCon As String
        Public strCambio As String
        Public strTipoDocumento As String
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

#Region "Métodos sobrcargados"
    Private Shared Sub ImprimirTiquete(szPrinterName As String)
        Dim doc As PrintDocument = New PrintDocument()
        doc.PrinterSettings.PrinterName = szPrinterName
        AddHandler doc.PrintPage, New PrintPageEventHandler(AddressOf ProvideContent)
        doc.Print()
    End Sub


    Private Shared Sub ProvideContent(sender As Object, e As PrintPageEventArgs)
        'FontSize 8 41 Chars - FontSize 9 36 Chars - FontSize 10 32 Chars - FontSize 11 30 Chars - FontSize 12 27 Chars
        'FontSize 13 25 Chars - FontSize 14 23 Chars - FontSize 15 22 Chars - FontSize 16 20 Chars
        Dim i As Integer = 0
        Dim paperWith = 3.5375 * charCount
        Dim graphics As Graphics = e.Graphics
        Dim positionY As Integer = 0
        Dim sf As StringFormat = New StringFormat()
        While i < lineas.Count
            Dim linea As ClsLineaImpresion = lineas(i)
            Dim fontStyle As FontStyle = IIf(linea.bolBold, FontStyle.Bold, FontStyle.Regular)
            sf.LineAlignment = StringAlignment.Center
            sf.Alignment = linea.intAlineado
            Dim rec As RectangleF = New RectangleF()
            rec.Width = paperWith * linea.intAncho / 100
            rec.Height = 20
            rec.X = paperWith * linea.intPosicionX / 100
            rec.Y = positionY
            Dim intFontSize As Integer = linea.intFuente / 80 * charCount
            graphics.DrawString(linea.strTexto, New Font("Lucida Console", intFontSize, fontStyle), New SolidBrush(Color.Black), rec, sf)
            positionY += (20 * linea.intSaltos)
            i += 1
        End While
    End Sub
#End Region

#Region "Métodos"
    Private Shared Sub ImprimirEncabezado(objEquipo As EquipoRegistrado, objEmpresa As Empresa, strFecha As String, strCodigoUsuario As String, strTitulo As String)
        lineas.Add(New ClsLineaImpresion(0, strFecha, 0, 50, 10, StringAlignment.Near, True))
        lineas.Add(New ClsLineaImpresion(2, strCodigoUsuario, 50, 50, 10, StringAlignment.Far, True))
        lineas.Add(New ClsLineaImpresion(1, strTitulo, 0, 100, 14, StringAlignment.Center, True))
        Dim strNombreComercial As String = objEmpresa.NombreComercial
        While strNombreComercial.Length > 30
            lineas.Add(New ClsLineaImpresion(1, strNombreComercial.Substring(0, 30), 0, 100, 12, StringAlignment.Center, True))
            strNombreComercial = strNombreComercial.Substring(30)
        End While
        lineas.Add(New ClsLineaImpresion(2, strNombreComercial, 0, 100, 12, StringAlignment.Center, True))
        Dim strDireccion As String = objEquipo.DireccionSucursal
        While strDireccion.Length > 32
            lineas.Add(New ClsLineaImpresion(1, strDireccion.Substring(0, 32), 0, 100, 10, StringAlignment.Center, False))
            strDireccion = strDireccion.Substring(32)
        End While
        lineas.Add(New ClsLineaImpresion(1, strDireccion, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(1, "TELEFONO: " + objEquipo.TelefonoSucursal, 0, 100, 10, StringAlignment.Center, False))
        Dim strNombreEmpresa As String = objEmpresa.NombreEmpresa
        While strNombreEmpresa.Length > 32
            lineas.Add(New ClsLineaImpresion(1, strNombreEmpresa.Substring(0, 32), 0, 100, 10, StringAlignment.Center, False))
            strNombreEmpresa = strNombreEmpresa.Substring(32)
        End While
        lineas.Add(New ClsLineaImpresion(1, strNombreEmpresa, 0, 100, 10, StringAlignment.Center, False))
        Dim strIdentificacion As String = objEmpresa.Identificacion
        If objEmpresa.Identificacion.Length > 32 Then strIdentificacion = strIdentificacion.Substring(0, 32)
        lineas.Add(New ClsLineaImpresion(1, strIdentificacion, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(2, objEmpresa.CorreoNotificacion, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(1, objEquipo.NombreSucursal, 0, 100, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(2, "Terminal: " & objEquipo.IdTerminal, 0, 100, 10, StringAlignment.Center, False))
    End Sub

    Private Shared Sub ImprimirDesglosePago(objDesglosePago As IList(Of ClsDesgloseFormaPago))
        lineas.Add(New ClsLineaImpresion(1, "Desglose Forma de pago", 0, 100, 10, StringAlignment.Center, False))
        For i As Integer = 0 To objDesglosePago.Count - 1
            lineas.Add(New ClsLineaImpresion(0, objDesglosePago(i).strDescripcion, 0, 54, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objDesglosePago(i).strMonto, 54, 46, 10, StringAlignment.Far, False))
        Next
    End Sub

    Private Shared Sub ImprimirDetalle(objDetalleComprobante As IList(Of ClsDetalleComprobante))
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
        lineas.Add(New ClsLineaImpresion(1, "Descripción", 0, 100, 10, StringAlignment.Near, False))
        lineas.Add(New ClsLineaImpresion(0, "Cant", 0, 15, 10, StringAlignment.Center, False))
        lineas.Add(New ClsLineaImpresion(0, "P/U", 15, 40, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, "Total", 55, 40, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
        For i As Integer = 0 To objDetalleComprobante.Count - 1
            If CDbl(objDetalleComprobante(i).strPrecio) > 0 Then
                Dim strLinea As String = objDetalleComprobante(i).strDescripcion
                While strLinea.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, strLinea.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    strLinea = strLinea.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, strLinea, 0, 100, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strCantidad, 0, 15, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strPrecio, 15, 40, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(0, objDetalleComprobante(i).strTotalLinea, 55, 40, 10, StringAlignment.Far, False))
                lineas.Add(New ClsLineaImpresion(1, objDetalleComprobante(i).strExcento, 95, 5, 10, StringAlignment.Far, False))
            End If
        Next
        lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
    End Sub

    Private Shared Sub ImprimirTotales(objComprobante As ClsComprobante)
        lineas.Add(New ClsLineaImpresion(0, "Sub-Total:", 0, 54, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strSubTotal, 54, 46, 10, StringAlignment.Far, False))
        If objComprobante.strDescuento <> "" Then
            lineas.Add(New ClsLineaImpresion(0, "Descuento:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objComprobante.strDescuento, 54, 46, 10, StringAlignment.Far, False))
        End If
        lineas.Add(New ClsLineaImpresion(0, "Impuesto:", 0, 54, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strImpuesto, 54, 46, 10, StringAlignment.Far, False))
        lineas.Add(New ClsLineaImpresion(0, "Total:", 0, 54, 10, StringAlignment.Far, True))
        lineas.Add(New ClsLineaImpresion(1, objComprobante.strTotal, 54, 46, 10, StringAlignment.Far, True))
    End Sub

    Public Shared Sub ImprimirFactura(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE DE FACTURA")
            If objImpresion.strClaveNumerica <> "" Then
                lineas.Add(New ClsLineaImpresion(1, objImpresion.strTipoDocumento, 0, 100, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.strClaveNumerica.Substring(0, 25), 0, 100, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(2, objImpresion.strClaveNumerica.Substring(25, 25), 0, 100, 10, StringAlignment.Center, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "Factura Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.strDocumento.Length > 0 Then lineas.Add(New ClsLineaImpresion(1, "Ref: " & objImpresion.strDocumento, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.strDetalle.Length > 0 Then
                Dim observacion As String = "Observación: " & objImpresion.strDetalle
                While observacion.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, observacion.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    observacion = observacion.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, observacion, 0, 100, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "", 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.arrDesglosePago.Count > 0 Then
                ImprimirDesglosePago(objImpresion.arrDesglosePago)
            Else
                lineas.Add(New ClsLineaImpresion(1, "Desglose Forma de pago", 0, 100, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(0, "Crédito", 0, 54, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.strTotal, 54, 46, 10, StringAlignment.Far, False))
            End If
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            If objImpresion.empresa.LeyendaFactura IsNot Nothing Then
                If objImpresion.empresa.LeyendaFactura.Length > 0 Then
                    Dim leyenda As String = objImpresion.empresa.LeyendaFactura
                    While leyenda.Length > 32
                        lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                        leyenda = leyenda.Substring(32)
                    End While
                    lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 100, 10, StringAlignment.Near, False))
                End If
            End If
            If objImpresion.arrDesglosePago.Count = 0 Then lineas.Add(New ClsLineaImpresion(2, "Firma: _________________________", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "AUTORIZADO MEDIANTE RESOLUCION", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(2, "DGT-R-48-2016 DEL 07-OCT-2016", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE PROFORMA")
            lineas.Add(New ClsLineaImpresion(1, "Proforma Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.strDocumento.Length > 0 Then
                Dim observacion As String = "Observación: " & objImpresion.strDocumento
                While observacion.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, observacion.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    observacion = observacion.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, observacion, 0, 100, 10, StringAlignment.Near, False))
            End If
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(2, " ", 54, 46, 10, StringAlignment.Far, False))
            If objImpresion.empresa.LeyendaProforma IsNot Nothing Then
                If objImpresion.empresa.LeyendaProforma.Length > 0 Then
                    Dim leyenda As String = objImpresion.empresa.LeyendaProforma
                    While leyenda.Length > 32
                        lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                        leyenda = leyenda.Substring(32)
                    End While
                    lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 100, 10, StringAlignment.Near, False))
                End If
            End If
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE APARTADO")
            lineas.Add(New ClsLineaImpresion(1, "Apartado Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Teléfono: " & objImpresion.strTelefono, 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.strDetalle.Length > 0 Then
                Dim observacion As String = "Observación: " & objImpresion.strDetalle
                While observacion.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, observacion.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    observacion = observacion.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(2, observacion, 0, 100, 10, StringAlignment.Near, False))
            End If
            If objImpresion.arrDesglosePago.Count > 0 Then ImprimirDesglosePago(objImpresion.arrDesglosePago)
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Abono inicial:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strAdelanto, 54, 46, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo por pagar:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strSaldo, 54, 46, 10, StringAlignment.Far, True))
            If objImpresion.empresa.LeyendaApartado IsNot Nothing Then
                If objImpresion.empresa.LeyendaApartado.Length > 0 Then
                    Dim leyenda As String = objImpresion.empresa.LeyendaApartado
                    While leyenda.Length > 32
                        lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                        leyenda = leyenda.Substring(32)
                    End While
                    lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 100, 10, StringAlignment.Near, False))
                End If
            End If
            lineas.Add(New ClsLineaImpresion(3, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "ORDEN DE SERVICIO")
            lineas.Add(New ClsLineaImpresion(1, "Orden Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Vendedor: " & objImpresion.strVendedor, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Teléfono: " & objImpresion.strTelefono, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Entrega: " & objImpresion.strDocumento, 0, 100, 10, StringAlignment.Near, True))
            If objImpresion.strDireccion.Length > 0 Then
                Dim direccion As String = "Dirección: " & objImpresion.strDireccion
                While direccion.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, direccion.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    direccion = direccion.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, direccion, 0, 100, 10, StringAlignment.Near, False))
            End If
            If objImpresion.strDescripcion.Length > 0 Then
                Dim descripcion As String = "Descripción: " & objImpresion.strDescripcion
                While descripcion.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, descripcion.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    descripcion = descripcion.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, descripcion, 0, 100, 10, StringAlignment.Near, False))
            End If
            If objImpresion.strDetalle.Length > 0 Then
                Dim notas As String = "Notas: " & objImpresion.strDetalle
                While notas.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, notas.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    notas = notas.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, notas, 0, 100, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "", 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.arrDesglosePago.Count > 0 Then ImprimirDesglosePago(objImpresion.arrDesglosePago)
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Abono inicial:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strAdelanto, 54, 46, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo por pagar:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strSaldo, 54, 46, 10, StringAlignment.Far, True))
            If objImpresion.empresa.LeyendaOrdenServicio IsNot Nothing Then
                If objImpresion.empresa.LeyendaOrdenServicio.Length > 0 Then
                    Dim leyenda As String = objImpresion.empresa.LeyendaOrdenServicio
                    While leyenda.Length > 32
                        lineas.Add(New ClsLineaImpresion(1, leyenda.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                        leyenda = leyenda.Substring(32)
                    End While
                    lineas.Add(New ClsLineaImpresion(2, leyenda, 0, 100, 10, StringAlignment.Near, False))
                End If
            End If
            lineas.Add(New ClsLineaImpresion(2, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirCompra(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TIQUETE DE COMPRA")
            lineas.Add(New ClsLineaImpresion(1, "Compra Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Factura Nro: " & objImpresion.strDocumento, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Proveedor: " & objImpresion.strNombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Descripción", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(0, "Cant", 0, 50, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(0, "Precio", 50, 50, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            For i As Integer = 0 To objImpresion.arrDetalleComprobante.Count - 1
                Dim strLinea As String = objImpresion.arrDetalleComprobante(i).strDescripcion
                While strLinea.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, strLinea.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    strLinea = strLinea.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, strLinea, 0, 100, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDetalleComprobante(i).strCantidad, 0, 50, 10, StringAlignment.Center, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDetalleComprobante(i).strPrecio, 50, 50, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirDevolucionCliente(ByVal objImpresion As ClsComprobante)
        lineas.Clear()
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "DEVOLUCION CLIENTE")
            lineas.Add(New ClsLineaImpresion(1, "Mov. Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fact Nro: " & objImpresion.strDocumento, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            If objImpresion.strDetalle.Length > 0 Then
                Dim detalle As String = "Detalle: " & objImpresion.strDetalle
                While detalle.Length > 32
                    lineas.Add(New ClsLineaImpresion(1, detalle.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                    detalle = detalle.Substring(32)
                End While
                lineas.Add(New ClsLineaImpresion(1, detalle, 0, 100, 10, StringAlignment.Near, False))
            End If
            lineas.Add(New ClsLineaImpresion(1, "", 0, 100, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(2, " ", 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "DEVOLUCION PROVEEDOR")
            lineas.Add(New ClsLineaImpresion(1, "Mov. Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Compra Nro: " & objImpresion.strDocumento, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Proveedor: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(1, "Recibido por: __________________________", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(1, " ", 54, 46, 10, StringAlignment.Far, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "TRASLADO DE MERCANCIA")
            lineas.Add(New ClsLineaImpresion(1, "Traslado Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFecha, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Destino: " & objImpresion.strFormaPago, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Enviado por: " & objImpresion.strEnviadoPor, 0, 100, 10, StringAlignment.Near, False))
            ImprimirDetalle(objImpresion.arrDetalleComprobante)
            ImprimirTotales(objImpresion)
            lineas.Add(New ClsLineaImpresion(1, " ", 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibido por: __________________________", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, " ", 54, 46, 10, StringAlignment.Far, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO CXC")
            lineas.Add(New ClsLineaImpresion(1, "Cuenta nro: " + objImpresion.strIdCuenta, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibo nro: " & objImpresion.strConsecutivo, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(2, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(0, "Saldo anterior:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strSaldoAnterior, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Monto abonado:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strTotalAbono, 54, 46, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo actual:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strSaldoActual, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO CXC")
            lineas.Add(New ClsLineaImpresion(1, "Cuenta nro: " + objImpresion.strIdCuenta, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibo nro: " & objImpresion.strConsecutivo, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(2, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(0, "Saldo anterior:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strSaldoAnterior, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Monto abonado:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strTotalAbono, 54, 46, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo actual:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strSaldoActual, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO APARTADO")
            lineas.Add(New ClsLineaImpresion(1, "Apartado nro: " + objImpresion.strIdCuenta, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibo nro: " & objImpresion.strConsecutivo, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(2, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(0, "Saldo anterior:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strSaldoAnterior, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Monto abonado:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strTotalAbono, 54, 46, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo actual:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strSaldoActual, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "RECIBO ABONO ORDEN")
            lineas.Add(New ClsLineaImpresion(1, "Orden nro: " + objImpresion.strIdCuenta, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibo nro: " & objImpresion.strConsecutivo, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Fecha: " & objImpresion.strFechaAbono, 0, 100, 10, StringAlignment.Near, False))
            Dim nombre As String = "Cliente: " & objImpresion.strNombre
            While nombre.Length > 32
                lineas.Add(New ClsLineaImpresion(1, nombre.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                nombre = nombre.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(1, nombre, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            ImprimirDesglosePago(objImpresion.arrDesglosePago)
            lineas.Add(New ClsLineaImpresion(2, "".PadRight(32, "_"), 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(0, "Saldo anterior:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strSaldoAnterior, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Monto abonado:", 0, 54, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strTotalAbono, 54, 46, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Saldo actual:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strSaldoActual, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Pago efectivo:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strPagoCon, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cambio:", 0, 54, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strCambio, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "GRACIAS POR PREFERIRNOS", 0, 100, 10, StringAlignment.Center, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "SALIDA DE EFECTIVO")
            lineas.Add(New ClsLineaImpresion(1, "Egreso Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Pagado a: " & objImpresion.strBeneficiario, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "La suma de: " & objImpresion.strMonto, 0, 100, 10, StringAlignment.Near, False))
            Dim concepto As String = "Concepto: " & objImpresion.strConcepto
            While concepto.Length > 32
                lineas.Add(New ClsLineaImpresion(1, concepto.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                concepto = concepto.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(2, concepto, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(3, "Recibido por: __________________", 0, 100, 10, StringAlignment.Near, False))
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
        charCount = objImpresion.equipo.AnchoLinea
        Try
            ImprimirEncabezado(objImpresion.equipo, objImpresion.empresa, Date.Now.ToString("dd-MM-yyyy"), objImpresion.usuario.CodigoUsuario, "INGRESO DE EFECTIVO")
            lineas.Add(New ClsLineaImpresion(1, "Ingreso Nro: " & objImpresion.strId, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Recibo de: " & objImpresion.strRecibidoDe, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "La suma de: " & objImpresion.strMonto, 0, 100, 10, StringAlignment.Near, False))
            Dim concepto As String = "Concepto: " & objImpresion.strConcepto
            While concepto.Length > 32
                lineas.Add(New ClsLineaImpresion(1, concepto.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                concepto = concepto.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(2, concepto, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(3, "Recibido por: __________________", 0, 100, 10, StringAlignment.Near, False))
        Catch ex As Exception
            Throw New Exception("Error formulando el string de impresion:" + ex.Message)
        End Try
        Try
            ImprimirTiquete(objImpresion.equipo.ImpresoraFactura)
        Catch ex As Exception
            Throw New Exception("Error invokando a ImprimirTiquete:" + ex.Message)
        End Try
    End Sub

    Public Shared Sub ImprimirCierreEfectivo(ByVal objImpresion As ClsCierreCaja)
        lineas.Clear()
        charCount = objImpresion.equipo.AnchoLinea
        Try
            lineas.Add(New ClsLineaImpresion(0, Now.ToString("dd/MM/yyyy"), 0, 143, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.usuario.CodigoUsuario, 143, 143, 10, StringAlignment.Far, True))
            Dim strNombreComercial As String = objImpresion.empresa.NombreComercial
            While strNombreComercial.Length > 30
                lineas.Add(New ClsLineaImpresion(1, strNombreComercial.Substring(0, 30), 0, 100, 12, StringAlignment.Center, True))
                strNombreComercial = strNombreComercial.Substring(30)
            End While
            lineas.Add(New ClsLineaImpresion(2, strNombreComercial, 0, 100, 12, StringAlignment.Center, True))
            lineas.Add(New ClsLineaImpresion(1, "CIERRE DE EFECTIVO", 0, 100, 10, StringAlignment.Center, False))
            lineas.Add(New ClsLineaImpresion(2, "Fecha: " & objImpresion.strFecha, 20, 66, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, "Detalle de Ingresos", 0, 100, 10, StringAlignment.Center, False))
            For i As Integer = 0 To objImpresion.arrDetalleIngresos.Count - 1
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDetalleIngresos(i).strDescripcion, 0, 54, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDetalleIngresos(i).strMonto, 54, 46, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(0, "Total de ingresos", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalIngresos, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(1, "Detalle de Egresos", 0, 100, 10, StringAlignment.Center, False))
            For i As Integer = 0 To objImpresion.arrDetalleEgresos.Count - 1
                lineas.Add(New ClsLineaImpresion(0, objImpresion.arrDetalleEgresos(i).strDescripcion, 0, 54, 10, StringAlignment.Near, False))
                lineas.Add(New ClsLineaImpresion(1, objImpresion.arrDetalleEgresos(i).strMonto, 54, 46, 10, StringAlignment.Far, False))
            Next
            lineas.Add(New ClsLineaImpresion(0, "Total de egresos", 0, 54, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalEgresos, 54, 46, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Cierre de efectivo", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strTotalEfectivo, 0, 100, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Efectivo en caja", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strEfectivoCaja, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Sobrante", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strSobrante, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Próx inicio efectivo", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strCierreEfectivoProx, 0, 100, 10, StringAlignment.Far, True))
            lineas.Add(New ClsLineaImpresion(0, "Total de entrega", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strRetiroEfectivo, 0, 100, 10, StringAlignment.Far, True))

            lineas.Add(New ClsLineaImpresion(0, "Ventas efectivo", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strVentasEfectivo, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Ventas tarjeta", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strVentasTarjeta, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Ventas transfer", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strVentasTransferencia, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Ventas crédito", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strVentasCredito, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Total de ventas", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalVentas, 0, 100, 10, StringAlignment.Far, False))

            lineas.Add(New ClsLineaImpresion(0, "Adelantos efectivo", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strAdelantosEfectivo, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Adelantos tarjeta", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strAdelantosTarjeta, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Adelantos transfer", 0, 100, 10, StringAlignment.Near, True))
            lineas.Add(New ClsLineaImpresion(1, objImpresion.strAdelantosTransferencia, 0, 100, 10, StringAlignment.Far, False))
            lineas.Add(New ClsLineaImpresion(0, "Total adelantos", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, objImpresion.strTotalAdelantos, 0, 100, 10, StringAlignment.Far, True))
            Dim observaciones As String = "Nota: " & objImpresion.strObservaciones
            While observaciones.Length > 32
                lineas.Add(New ClsLineaImpresion(1, observaciones.Substring(0, 32), 0, 100, 10, StringAlignment.Near, False))
                observaciones = observaciones.Substring(32)
            End While
            lineas.Add(New ClsLineaImpresion(2, observaciones, 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(2, "Procesado por: _________________", 0, 100, 10, StringAlignment.Near, False))
            lineas.Add(New ClsLineaImpresion(1, ".", 0, 100, 10, StringAlignment.Near, False))
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
