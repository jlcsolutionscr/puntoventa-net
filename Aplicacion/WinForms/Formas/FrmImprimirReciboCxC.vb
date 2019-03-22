Imports System.Collections.Generic
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades

Public Class FrmImprimirReciboCxC
#Region "Variables"
    Private I As Short
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private dblSaldo As Decimal
    Private bolInit As Boolean = True
    Private listadoMovimientos As IEnumerable(Of MovimientoCuentaPorCobrar)
    Private movimientoCuentaPorCobrar As MovimientoCuentaPorCobrar
    Private reciboComprobante As ModuloImpresion.ClsRecibo
    Private desglosePagoImpresion As ModuloImpresion.clsDesgloseFormaPago
    Private arrDesgloseMov, arrDesglosePago As List(Of ModuloImpresion.clsDesgloseFormaPago)
    Private cliente As Cliente
#End Region

#Region "Métodos"
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
        dvcDescripcion.HeaderText = "Descripción"
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
    Private Sub FrmImprimirReciboCxC_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        IniciaDetalleMovimiento()
        EstablecerPropiedadesDataGridView()
        grdDetalleRecibo.DataSource = dtbDetalleMovimiento
        bolInit = False
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
                arrDesgloseMov = New List(Of ModuloImpresion.clsDesgloseFormaPago)()
                For Each desgloseMovimiento As DesgloseMovimientoCuentaPorCobrar In movimientoCuentaPorCobrar.DesgloseMovimientoCuentaPorCobrar
                    desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                        .strDescripcion = desgloseMovimiento.CuentaPorCobrar.NroDocOrig,
                        .strMonto = FormatNumber(desgloseMovimiento.Monto, 2)
                    }
                    arrDesgloseMov.Add(desglosePagoImpresion)
                Next
                reciboComprobante.arrDesgloseMov = arrDesgloseMov
                arrDesglosePago = New List(Of ModuloImpresion.clsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoCuentaPorCobrar In movimientoCuentaPorCobrar.DesglosePagoMovimientoCuentaPorCobrar
                    desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
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

    Private Sub btnBuscarCliente_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarCliente.Click
        Dim formBusquedaCliente As New FrmBusquedaCliente()
        FrmPrincipal.intBusqueda = 0
        formBusquedaCliente.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
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
#End Region
End Class