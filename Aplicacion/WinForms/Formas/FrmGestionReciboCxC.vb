Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmGestionReciboCxC
#Region "Variables"
    Private I As Short
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private bolInit As Boolean = True
    Private listadoMovimientos As IEnumerable(Of MovimientoCuentaPorCobrar)
    Private movimientoCuentaPorCobrar As MovimientoCuentaPorCobrar
    Private cliente As Cliente

    Private reciboComprobante As ModuloImpresion.ClsRecibo
    Private desglosePagoImpresion As ModuloImpresion.ClsDesgloseFormaPago
    Private arrDesgloseMov, arrDesglosePago As List(Of ModuloImpresion.ClsDesgloseFormaPago)
#End Region

#Region "M�todos"
    Private Sub IniciaDetalleMovimiento()
        dtbDetalleMovimiento = New DataTable()
        dtbDetalleMovimiento.Columns.Add("IDMOV", GetType(Integer))
        dtbDetalleMovimiento.Columns.Add("USUARIO", GetType(String))
        dtbDetalleMovimiento.Columns.Add("TIPO", GetType(Integer))
        dtbDetalleMovimiento.Columns.Add("DESCRIPCION", GetType(String))
        dtbDetalleMovimiento.Columns.Add("FECHA", GetType(Date))
        dtbDetalleMovimiento.Columns.Add("MONTO", GetType(Decimal))
        dtbDetalleMovimiento.PrimaryKey = {dtbDetalleMovimiento.Columns(0)}
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        grdDetalleRecibo.Columns.Clear()
        grdDetalleRecibo.AutoGenerateColumns = False

        Dim dvcIdMov As New DataGridViewTextBoxColumn
        Dim dvcUsuario As New DataGridViewTextBoxColumn
        Dim dvcTipo As New DataGridViewTextBoxColumn
        Dim dvcRecibo As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcMonto As New DataGridViewTextBoxColumn

        dvcIdMov.DataPropertyName = "IDMOV"
        dvcIdMov.HeaderText = "Mov"
        dvcIdMov.Width = 60
        grdDetalleRecibo.Columns.Add(dvcIdMov)

        dvcUsuario.DataPropertyName = "USUARIO"
        dvcUsuario.HeaderText = "Usuario"
        dvcUsuario.Width = 50
        grdDetalleRecibo.Columns.Add(dvcUsuario)

        dvcTipo.DataPropertyName = "TIPO"
        dvcTipo.HeaderText = "Tipo"
        dvcTipo.Width = 50
        grdDetalleRecibo.Columns.Add(dvcTipo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripci�n"
        dvcDescripcion.Width = 340
        grdDetalleRecibo.Columns.Add(dvcDescripcion)

        dvcFecha.DataPropertyName = "FECHA"
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.Width = 70
        grdDetalleRecibo.Columns.Add(dvcFecha)

        dvcMonto.DataPropertyName = "MONTO"
        dvcMonto.HeaderText = "Monto"
        dvcMonto.Width = 90
        dvcMonto.DefaultCellStyle = FrmPrincipal.dgvDecimal
        grdDetalleRecibo.Columns.Add(dvcMonto)
    End Sub

    Private Sub CargarDetalleMovimiento(ByVal intIdCliente As Integer)
        dtbDetalleMovimiento.Rows.Clear()
        Try
            'listadoMovimientos = servicioCuentaPorCobrar.ObtenerListaMovimientos(intIdCliente)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        For Each mov As MovimientoCuentaPorCobrar In listadoMovimientos
            If Not mov.Nulo Then
                dtrRowDetMovimiento = dtbDetalleMovimiento.NewRow
                dtrRowDetMovimiento.Item(0) = mov.IdMovCxC
                dtrRowDetMovimiento.Item(1) = mov.Usuario.CodigoUsuario
                dtrRowDetMovimiento.Item(2) = mov.Tipo
                dtrRowDetMovimiento.Item(3) = mov.Descripcion
                dtrRowDetMovimiento.Item(4) = mov.Fecha
                dtrRowDetMovimiento.Item(5) = mov.Monto
                dtbDetalleMovimiento.Rows.Add(dtrRowDetMovimiento)
            End If
        Next
        grdDetalleRecibo.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    Try
                        'servicioCuentaPorCobrar.AnularMovimientoCxC(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    MessageBox.Show("Transacci�n procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarDetalleMovimiento(cliente.IdCliente)
                End If
            Else
                MessageBox.Show("Debe seleccionar un registro para procesar", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No existen registros para procesar", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            If FrmPrincipal.intBusqueda = StaticValoresPorDefecto.ClienteContado Then
                MessageBox.Show("El cliente indicado no corresponde a un cliente de cr�dito", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End If
            Try
                'cliente = servicioFacturacion.ObtenerCliente(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtNombreCliente.Text = cliente.Nombre
            CargarDetalleMovimiento(cliente.IdCliente)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                'movimientoCuentaPorCobrar = servicioCuentaPorCobrar.ObtenerMovimiento(grdDetalleRecibo.CurrentRow.Cells(0).Value)
                reciboComprobante = New ModuloImpresion.ClsRecibo With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strConsecutivo = movimientoCuentaPorCobrar.IdMovCxC,
                    .strNombre = txtNombreCliente.Text,
                    .strFechaAbono = movimientoCuentaPorCobrar.Fecha,
                    .strTotalAbono = FormatNumber(movimientoCuentaPorCobrar.Monto, 2)
                }
                arrDesgloseMov = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desgloseMovimiento As DesgloseMovimientoCuentaPorCobrar In movimientoCuentaPorCobrar.DesgloseMovimientoCuentaPorCobrar
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago With {
                        .strDescripcion = desgloseMovimiento.CuentaPorCobrar.NroDocOrig,
                        .strMonto = FormatNumber(desgloseMovimiento.Monto, 2)
                    }
                    arrDesgloseMov.Add(desglosePagoImpresion)
                Next
                reciboComprobante.arrDesgloseMov = arrDesgloseMov
                arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoCuentaPorCobrar In movimientoCuentaPorCobrar.DesglosePagoMovimientoCuentaPorCobrar
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago With {
                        .strDescripcion = desglosePago.FormaPago.Descripcion,
                        .strMonto = FormatNumber(desglosePago.MontoLocal, 2),
                        .strNroDoc = desglosePago.NroMovimiento
                    }
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                reciboComprobante.arrDesglosePago = arrDesglosePago
                Try
                    ModuloImpresion.ImprimirReciboCxC(reciboComprobante)
                Catch ex As Exception
                    MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                    Exit Sub
                End Try
            Else
                MessageBox.Show("Debe seleccionar un registro para imprimir", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No existen registros para imprimir", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub FrmAnulaReciboCxC_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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