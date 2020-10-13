Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmGestionAbonoApartado
#Region "Variables"
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private listadoMovimientos As IList(Of EfectivoDetalle)
    Private movimiento As MovimientoApartado
    Private apartado As Apartado
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

    Private Async Sub CargarDetalleMovimiento(ByVal intIdApartado As Integer)
        dtbDetalleMovimiento.Rows.Clear()
        Try
            listadoMovimientos = Await Puntoventa.ObtenerListadoMovimientosApartado(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.equipoGlobal.IdSucursal, intIdApartado, FrmPrincipal.usuarioGlobal.Token)
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
    Private Sub FrmGestionAbonoApartado_Load(sender As Object, e As EventArgs) Handles MyBase.Load
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
                        Await Puntoventa.AnularMovimientoApartado(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.IdUsuario, formAnulacion.strMotivo, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarDetalleMovimiento(apartado.IdCliente)
                End If
            Else
                MessageBox.Show("Debe seleccionar un registro para procesar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No existen registros para procesar", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Async Sub btnBuscarApartado_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarApartado.Click
        Dim formBusquedaCliente As New FrmBusquedaApartado()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                apartado = Await Puntoventa.ObtenerApartado(FrmPrincipal.intBusqueda, FrmPrincipal.usuarioGlobal.Token)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtNombreCliente.Text = "Apartado " & apartado.IdApartado & " de " & apartado.NombreCliente
            CargarDetalleMovimiento(apartado.IdApartado)
        End If
    End Sub

    Private Async Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                Try
                    movimiento = Await Puntoventa.ObtenerMovimientoApartado(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                decPagoEfectivo = 0
                For Each item As DesglosePagoMovimientoApartado In movimiento.DesglosePagoMovimientoApartado
                    If item.IdFormaPago = StaticFormaPago.Efectivo Then decPagoEfectivo = item.MontoLocal
                Next
                reciboComprobante = New ModuloImpresion.ClsRecibo With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strConsecutivo = movimiento.IdMovApartado,
                    .strIdCuenta = movimiento.Apartado.ConsecApartado,
                    .strNombre = movimiento.Apartado.NombreCliente,
                    .strFechaAbono = movimiento.Fecha,
                    .strSaldoAnterior = FormatNumber(movimiento.SaldoActual, 2),
                    .strTotalAbono = FormatNumber(movimiento.Monto, 2),
                    .strSaldoActual = FormatNumber(movimiento.SaldoActual - movimiento.Monto, 2),
                    .strPagoCon = FormatNumber(decPagoEfectivo, 2),
                    .strCambio = FormatNumber(0, 2)
                }
                reciboComprobante.arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoApartado In movimiento.DesglosePagoMovimientoApartado
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(desglosePago.FormaPago.Descripcion, FormatNumber(desglosePago.MontoLocal, 2))
                    reciboComprobante.arrDesglosePago.Add(desglosePagoImpresion)
                Next
                Try
                    ModuloImpresion.ImprimirReciboApartado(reciboComprobante)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                MessageBox.Show("Debe seleccionar un registro para imprimir", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub FrmGestionAbonoApartado_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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