Imports System.Collections.Generic
Imports LeandroSoftware.PuntoVenta.Core.CommonTypes
Imports LeandroSoftware.PuntoVenta.Dominio.Entidades
Imports LeandroSoftware.PuntoVenta.Servicios
Imports Unity

Public Class FrmImprimirReciboCxP
#Region "Variables"
    Private I As Short
    Private dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private dblSaldo As Decimal
    Private bolInit As Boolean = True
    Private servicioCompras As ICompraService
    Private servicioCuentaPorPagar As ICuentaPorPagarService
    Private listadoMovimientos As IEnumerable(Of MovimientoCuentaPorPagar)
    Private movimientoCuentaPorPagar As MovimientoCuentaPorPagar
    Private reciboComprobante As ModuloImpresion.clsRecibo
    Private desglosePagoImpresion As ModuloImpresion.clsDesgloseFormaPago
    Private arrDesgloseMov, arrDesglosePago As List(Of ModuloImpresion.clsDesgloseFormaPago)
    Private proveedor As Proveedor
#End Region

#Region "Métodos"
    Private Sub IniciaDetalleMovimiento()
        dtbDetalleMovimiento = New DataTable()
        dtbDetalleMovimiento.Columns.Add("IDMOV", GetType(Int32))
        dtbDetalleMovimiento.Columns.Add("USUARIO", GetType(String))
        dtbDetalleMovimiento.Columns.Add("TIPO", GetType(Int32))
        dtbDetalleMovimiento.Columns.Add("RECIBO", GetType(String))
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

        dvcRecibo.DataPropertyName = "RECIBO"
        dvcRecibo.HeaderText = "Recibo"
        dvcRecibo.Width = 100
        grdDetalleRecibo.Columns.Add(dvcRecibo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripción"
        dvcDescripcion.Width = 240
        grdDetalleRecibo.Columns.Add(dvcDescripcion)

        dvcFecha.DataPropertyName = "FECHA"
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.Width = 70
        grdDetalleRecibo.Columns.Add(dvcFecha)

        dvcMonto.DataPropertyName = "MONTO"
        dvcMonto.HeaderText = "Monto"
        dvcMonto.Width = 90
        dvcMonto.DefaultCellStyle = FrmMenuPrincipal.dgvDecimal
        grdDetalleRecibo.Columns.Add(dvcMonto)
    End Sub

    Private Sub CargarDetalleMovimiento(ByVal intIdProveedor As Integer)
        dtbDetalleMovimiento.Rows.Clear()
        Try
            listadoMovimientos = servicioCuentaPorPagar.ObtenerListaMovimientos(StaticTipoCuentaPorPagar.Proveedores, intIdProveedor)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
        For Each mov As MovimientoCuentaPorPagar In listadoMovimientos
            If Not mov.Nulo Then
                dtrRowDetMovimiento = dtbDetalleMovimiento.NewRow
                dtrRowDetMovimiento.Item(0) = mov.IdMovCxP
                dtrRowDetMovimiento.Item(1) = mov.Usuario.CodigoUsuario
                dtrRowDetMovimiento.Item(2) = mov.Tipo
                dtrRowDetMovimiento.Item(3) = mov.Recibo
                dtrRowDetMovimiento.Item(4) = mov.Descripcion
                dtrRowDetMovimiento.Item(5) = mov.Fecha
                dtrRowDetMovimiento.Item(6) = mov.Monto
                dtbDetalleMovimiento.Rows.Add(dtrRowDetMovimiento)
            End If
        Next
        grdDetalleRecibo.Refresh()
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmAnulaReciboCxP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioCompras = FrmMenuPrincipal.unityContainer.Resolve(Of ICompraService)()
            servicioCuentaPorPagar = FrmMenuPrincipal.unityContainer.Resolve(Of ICuentaPorPagarService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        IniciaDetalleMovimiento()
        EstablecerPropiedadesDataGridView()
        grdDetalleRecibo.DataSource = dtbDetalleMovimiento
        bolInit = False
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                movimientoCuentaPorPagar = servicioCuentaPorPagar.ObtenerMovimiento(grdDetalleRecibo.CurrentRow.Cells(0).Value)
                reciboComprobante = New ModuloImpresion.clsRecibo With {
                    .usuario = FrmMenuPrincipal.usuarioGlobal,
                    .empresa = FrmMenuPrincipal.empresaGlobal,
                    .equipo = FrmMenuPrincipal.equipoGlobal,
                    .strConsecutivo = movimientoCuentaPorPagar.IdMovCxP,
                    .strRecibo = movimientoCuentaPorPagar.Recibo,
                    .strNombre = txtNombreProveedor.Text,
                    .strFechaAbono = movimientoCuentaPorPagar.Fecha,
                    .strTotalAbono = FormatNumber(movimientoCuentaPorPagar.Monto, 2)
                }
                arrDesgloseMov = New List(Of ModuloImpresion.clsDesgloseFormaPago)()
                For Each desgloseMovimiento As DesgloseMovimientoCuentaPorPagar In movimientoCuentaPorPagar.DesgloseMovimientoCuentaPorPagar
                    desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                        .strDescripcion = desgloseMovimiento.CuentaPorPagar.NroDocOrig,
                        .strMonto = FormatNumber(desgloseMovimiento.Monto, 2)
                    }
                    arrDesgloseMov.Add(desglosePagoImpresion)
                Next
                reciboComprobante.arrDesgloseMov = arrDesgloseMov
                arrDesglosePago = New List(Of ModuloImpresion.clsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoCuentaPorPagar In movimientoCuentaPorPagar.DesglosePagoMovimientoCuentaPorPagar
                    desglosePagoImpresion = New ModuloImpresion.clsDesgloseFormaPago With {
                        .strDescripcion = desglosePago.FormaPago.Descripcion,
                        .strMonto = FormatNumber(desglosePago.MontoLocal, 2),
                        .strNroDoc = desglosePago.NroMovimiento
                    }
                    arrDesglosePago.Add(desglosePagoImpresion)
                Next
                reciboComprobante.arrDesglosePago = arrDesglosePago
                Try
                    ModuloImpresion.ImprimirReciboCxP(reciboComprobante)
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

    Private Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmMenuPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmMenuPrincipal.intBusqueda > 0 Then
            Try
                proveedor = servicioCompras.ObtenerProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtNombreProveedor.Text = proveedor.Nombre
            CargarDetalleMovimiento(proveedor.IdProveedor)
        End If
    End Sub
#End Region
End Class