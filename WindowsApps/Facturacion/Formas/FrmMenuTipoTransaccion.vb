Imports LeandroSoftware.Core.TiposComunes
Imports System.Collections.Generic

Public Class FrmMenuTipoTransaccion
#Region "Variables"
    Private dtListaFormaPago As New DataTable, drListaFormaPago As DataRow
    Public clFormasPago As IEnumerable(Of LlaveDescripcion)
#End Region

#Region "Métodos"
    Private Sub CargarCombos()
        dtListaFormaPago.Clear()
        dtListaFormaPago.Columns.Add(New DataColumn("IdFormaPago", GetType(Integer)))
        dtListaFormaPago.Columns.Add(New DataColumn("Descripcion", GetType(String)))
        drListaFormaPago = dtListaFormaPago.NewRow()
        drListaFormaPago(0) = "-1"
        drListaFormaPago(1) = "Todos"
        dtListaFormaPago.Rows.Add(drListaFormaPago)
        For Each formaPago As LlaveDescripcion In clFormasPago
            drListaFormaPago = dtListaFormaPago.NewRow()
            drListaFormaPago(0) = formaPago.Id
            drListaFormaPago(1) = formaPago.Descripcion
            dtListaFormaPago.Rows.Add(drListaFormaPago)
        Next
        cboTipoTransaccion.ValueMember = "IdFormaPago"
        cboTipoTransaccion.DisplayMember = "Descripcion"
        cboTipoTransaccion.DataSource = dtListaFormaPago
        cboTipoTransaccion.SelectedValue = -1
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmRptMenu_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            CargarCombos()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Sub btnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        If cboTipoTransaccion.SelectedValue IsNot Nothing Then
            FrmPrincipal.intBusqueda = cboTipoTransaccion.SelectedValue
            Close()
        Else
            FrmPrincipal.intBusqueda = 0
            Close()
        End If
    End Sub
#End Region
End Class