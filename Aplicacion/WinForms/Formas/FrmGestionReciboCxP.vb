Imports System.Collections.Generic
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.Core.Dominio.Entidades

Public Class FrmGestionReciboCxP
#Region "Variables"
    Private I As Short
    Private dtbDatosLocal, dtbDetalleMovimiento As DataTable
    Private dtrRowDetMovimiento As DataRow
    Private bolInit As Boolean = True
    Private listadoMovimientos As IEnumerable(Of MovimientoCuentaPorPagar)
    Private movimientoCuentaPorPagar As MovimientoCuentaPorPagar
    Private proveedor As Proveedor

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
        dvcRecibo.Width = 140
        grdDetalleRecibo.Columns.Add(dvcRecibo)

        dvcDescripcion.DataPropertyName = "DESCRIPCION"
        dvcDescripcion.HeaderText = "Descripci�n"
        dvcDescripcion.Width = 200
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

    Private Sub CargarDetalleMovimiento(ByVal intIdProveedor As Integer)
        dtbDetalleMovimiento.Rows.Clear()
        Try
            'listadoMovimientos = servicioCuentaPorPagar.ObtenerListaMovimientos(StaticTipoCuentaPorPagar.Proveedores, intIdProveedor)
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
    Private Sub CmdAnular_Click(sender As Object, e As EventArgs) Handles CmdAnular.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                If MessageBox.Show("Desea anular este registro?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                    Try
                        'servicioCuentaPorPagar.AnularMovimientoCxP(grdDetalleRecibo.CurrentRow.Cells(0).Value, FrmMenuPrincipal.usuarioGlobal.IdUsuario)
                    Catch ex As Exception
                        MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                        Exit Sub
                    End Try
                    MessageBox.Show("Transacci�n procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    CargarDetalleMovimiento(proveedor.IdProveedor)
                End If
            Else
                MessageBox.Show("Debe seleccionar un registro para procesar", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
            End If
        Else
            MessageBox.Show("No existen registros para procesar", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Exclamation)
        End If
    End Sub

    Private Sub btnBuscarProveedor_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnBuscarProveedor.Click
        Dim formBusquedaProveedor As New FrmBusquedaProveedor()
        FrmPrincipal.intBusqueda = 0
        formBusquedaProveedor.ShowDialog()
        If FrmPrincipal.intBusqueda > 0 Then
            Try
                'proveedor = servicioCompras.ObtenerProveedor(FrmMenuPrincipal.intBusqueda)
            Catch ex As Exception
                MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            txtNombreProveedor.Text = proveedor.Nombre
            CargarDetalleMovimiento(proveedor.IdProveedor)
        End If
    End Sub

    Private Sub CmdImprimir_Click(sender As Object, e As EventArgs) Handles CmdImprimir.Click
        If grdDetalleRecibo.Rows.Count > 0 Then
            If grdDetalleRecibo.CurrentRow.Cells(0).Value.ToString <> "" Then
                'movimientoCuentaPorPagar = servicioCuentaPorPagar.ObtenerMovimiento(grdDetalleRecibo.CurrentRow.Cells(0).Value)
                reciboComprobante = New ModuloImpresion.ClsRecibo With {
                    .usuario = FrmPrincipal.usuarioGlobal,
                    .empresa = FrmPrincipal.empresaGlobal,
                    .equipo = FrmPrincipal.equipoGlobal,
                    .strConsecutivo = movimientoCuentaPorPagar.IdMovCxP,
                    .strRecibo = movimientoCuentaPorPagar.Recibo,
                    .strNombre = txtNombreProveedor.Text,
                    .strFechaAbono = movimientoCuentaPorPagar.Fecha,
                    .strTotalAbono = FormatNumber(movimientoCuentaPorPagar.Monto, 2)
                }
                arrDesgloseMov = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desgloseMovimiento As DesgloseMovimientoCuentaPorPagar In movimientoCuentaPorPagar.DesgloseMovimientoCuentaPorPagar
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago With {
                        .strDescripcion = desgloseMovimiento.CuentaPorPagar.NroDocOrig,
                        .strMonto = FormatNumber(desgloseMovimiento.Monto, 2)
                    }
                    arrDesgloseMov.Add(desglosePagoImpresion)
                Next
                reciboComprobante.arrDesgloseMov = arrDesgloseMov
                arrDesglosePago = New List(Of ModuloImpresion.ClsDesgloseFormaPago)()
                For Each desglosePago As DesglosePagoMovimientoCuentaPorPagar In movimientoCuentaPorPagar.DesglosePagoMovimientoCuentaPorPagar
                    desglosePagoImpresion = New ModuloImpresion.ClsDesgloseFormaPago With {
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

    Private Sub FrmAnulaReciboCxP_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
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