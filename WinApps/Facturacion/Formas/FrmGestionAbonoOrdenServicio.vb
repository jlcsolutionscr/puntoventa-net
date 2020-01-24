Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades
Imports LeandroSoftware.ClienteWCF

Public Class FrmGestionAbonoOrdenServicio
#Region "Variables"
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private bolInit As Boolean = True
    Private listadoMovimientos As IList(Of CuentaDetalle)
    Private movimientoOrdenServicio As MovimientoOrdenServicio
    Private ordenServicio As OrdenServicio
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
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        For Each mov As CuentaDetalle In listadoMovimientos
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
    Private Async Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    Try
                        Await Puntoventa.AnularMovimientoOrdenServicio(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.IdUsuario, FrmPrincipal.usuarioGlobal.Token)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarDetalleMovimiento(ordenServicio.IdCliente)
                End If
            Else
                MessageBox.Show("Debe seleccionar un registro para procesar", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No existen registros para procesar", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
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
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                    movimientoOrdenServicio = Await Puntoventa.ObtenerMovimientoOrdenServicio(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmPrincipal.usuarioGlobal.Token)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
                reciboComprobante = New ModuloImpresion.ClsRecibo With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strConsecutivo = movimientoOrdenServicio.IdMovOrden,
                    .strNombre = movimientoOrdenServicio.OrdenServicio.NombreCliente,
                    .strFechaAbono = movimientoOrdenServicio.Fecha,
                    .strTotalAbono = FormatNumber(movimientoOrdenServicio.Monto, 2)
                }
                reciboComprobante.arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoOrdenServicio In movimientoOrdenServicio.DesglosePagoMovimientoOrdenServicio
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago(desglosePago.FormaPago.Descripcion, FormatNumber(desglosePago.MontoLocal, 2))
                    reciboComprobante.arrDesglosePago.Add(desglosePagoImpresion)
                Next
                Try
                    ModuloImpresion.ImprimirReciboOrdenServicio(reciboComprobante)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                MessageBox.Show("Debe seleccionar un registro para imprimir", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        End If
    End Sub

    Private Sub FrmGestionAbonoOrdenServicio_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            IniciaDetalleMovimiento()
            EstablecerPropiedadesDataGridView()
            grdDetalleRecibo.DataSource = dtbDetalleMovimiento
            bolInit = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub
#End Region
End Class