Imports LeandroSoftware.ClienteWCF
Imports LeandroSoftware.Common.DatosComunes
Imports LeandroSoftware.Common.Dominio.Entidades
Imports LeandroSoftware.Common.Constantes
Imports System.Collections.Generic

Public Class FrmGestionAbonoOrdenServicio
#Region "Variables"
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private listadoMovimientos As IList(Of EfectivoDetalle)
    Private movimiento As MovimientoOrdenServicio
    Private ordenServicio As OrdenServicio
    Private decPagoEfectivo As Decimal
    'Variables de impresion
    Private reciboComprobante As ModuloImpresion.ClsRecibo
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleMovimiento()
        dtbDetalleMovimiento = New DataTable()
        dtbDetalleMovimiento.Columns.Add("ID", GetType(Integer))
        dtbDetalleMovimiento.Columns.Add("FECHA", GetType(Date))
        dtbDetalleMovimiento.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleMovimiento.Columns.Add("TOTAL", GetType(Decimal))
        dtbDetalleMovimiento.PrimaryKey = {dtbDetalleMovimiento.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleRecibo.Columns.Clear()
        grdDetalleRecibo.AutoGenerateColumns = False

        Dim dvcIdMov As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcMonto As New DataGridViewTextBoxColumn

        dvcIdMov.DataPropertyName = "ID"
        dvcIdMov.HeaderText = "Id"
        dvcIdMov.Width = 50
        grdDetalleRecibo.Columns.Add(dvcIdMov)

        dvcFecha.DataPropertyName = "FECHA"
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.Width = 70
        grdDetalleRecibo.Columns.Add(dvcFecha)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 380
        grdDetalleRecibo.Columns.Add(dvcDescripcion)

        dvcMonto.DataPropertyName = "TOTAL"
        dvcMonto.HeaderText = "Monto"
        dvcMonto.Width = 100
        dvcMonto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleRecibo.Columns.Add(dvcMonto)
    End Sub

    Private Async Sub CargarDetalleMovimiento(ByVal intIdOrdenServicio As Integer)
        dtbDetalleMovimiento.Rows.Clear()
        Try
            listadoMovimientos = Await Puntoventa.ObtenerListadoMovimientosOrdenServicio(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, intIdOrdenServicio, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        For Each mov As EfectivoDetalle In listadoMovimientos
            dtrRowDetMovimiento = dtbDetalleMovimiento.NewRow
            dtrRowDetMovimiento.Item(0) = mov.Id
            dtrRowDetMovimiento.Item(1) = mov.Fecha
            dtrRowDetMovimiento.Item(2) = mov.Descripcion
            dtrRowDetMovimiento.Item(3) = mov.Total
            dtbDetalleMovimiento.Rows.Add(dtrRowDetMovimiento)
        Next
        grdDetalleRecibo.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmGestionAbonoOrdenServicio_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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

    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                Dim formAnulacion As New FrmMotivoAnulacion()
                formAnulacion.bolConfirmacion = False
                formAnulacion.ShowDialog()
                If formAnulacion.bolConfirmacion Then
                    Try
                        Await Puntoventa.AnularMovimientoOrdenServicio(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarDetalleMovimiento(ordenServicio.IdCliente)
                End If
            Else
                MessageBox.Show("Debe seleccionar un registro para procesar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No existen registros para procesar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Async Sub btnBuscarOrdenServicio_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarOrdenServicio.Click
        Dim formBusquedaCliente As New FrmBusquedaOrdenServicio()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                ordenServicio = Await Puntoventa.ObtenerOrdenServicio(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtNombreCliente.Text = "Orden de servicio " & ordenServicio.IdOrden & " de " & ordenServicio.NombreCliente
            CargarDetalleMovimiento(ordenServicio.IdOrden)
        End If
    End Sub

    Private Async Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                Try
                    movimiento = Await Puntoventa.ObtenerMovimientoOrdenServicio(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                decPagoEfectivo = 0
                For Each item As DesglosePagoMovimientoOrdenServicio In movimiento.DesglosePagoMovimientoOrdenServicio
                    If item.IdFormaPago = StaticFormaPago.Efectivo Then decPagoEfectivo = item.MontoLocal
                Next
                reciboComprobante = New ModuloImpresion.ClsRecibo With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strConsecutivo = movimiento.IdMovOrden,
                    .strIdCuenta = movimiento.OrdenServicio.ConsecOrdenServicio,
                    .strNombre = movimiento.OrdenServicio.NombreCliente,
                    .strFechaAbono = movimiento.Fecha,
                    .strSaldoAnterior = FormatNumber(movimiento.SaldoActual, 2),
                    .strTotalAbono = FormatNumber(movimiento.Monto, 2),
                    .strSaldoActual = FormatNumber(movimiento.SaldoActual - movimiento.Monto, 2),
                    .strPagoCon = FormatNumber(decPagoEfectivo, 2),
                    .strCambio = FormatNumber(0, 2)
                }
                reciboComprobante.arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoOrdenServicio In movimiento.DesglosePagoMovimientoOrdenServicio
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(FrmPrincipal.ObtenerDescripcionFormaPagoCliente(desglosePago.IdFormaPago), FormatNumber(desglosePago.MontoLocal, 2))
                    reciboComprobante.arrDesglosePago.Add(desglosePagoImpresion)
                Next
                Try
                    ModuloImpresion.ImprimirReciboOrdenServicio(reciboComprobante)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                MessageBox.Show("Debe seleccionar un registro para imprimir", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub FrmGestionAbonoOrdenServicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleMovimiento()
            EstablecerPropiedadesDataGridView()
            grdDetalleRecibo.DataSource = dtbDetalleMovimiento
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub
#End Region
End Class