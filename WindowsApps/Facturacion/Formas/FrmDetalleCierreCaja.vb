Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Collections.Generic

Public Class FrmDetalleCierreCaja
#Region "Variables"
    Public dtDatos As List(Of DetalleMovimientoCierreCaja)
#End Region

#Region "M�todos"
    Private Sub EstablecerPropiedadesDataGridView()
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcDescripcion As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        dgvListado.Columns.Clear()
        dgvListado.AutoGenerateColumns = False
        dvcId.HeaderText = "Mov Nro."
        dvcId.DataPropertyName = "IdReferencia"
        dvcId.Width = 100
        dgvListado.Columns.Add(dvcId)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 150
        dgvListado.Columns.Add(dvcFecha)
        dvcDescripcion.HeaderText = "Referencia"
        dvcDescripcion.DataPropertyName = "Descripcion"
        dvcDescripcion.Width = 400
        dgvListado.Columns.Add(dvcDescripcion)
        dvcTotal.HeaderText = "Total"
        dvcTotal.DataPropertyName = "Total"
        dvcTotal.Width = 120
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvListado.Columns.Add(dvcTotal)
    End Sub
#End Region

#Region "Eventos Controles"
    Private Sub FrmBusProd_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            EstablecerPropiedadesDataGridView()
            dgvListado.DataSource = dtDatos
            dgvListado.Refresh()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
    End Sub

    Private Sub btnRegresar_Click(sender As Object, e As EventArgs) Handles btnRegresar.Click
        Close()
    End Sub
#End Region
End Class
