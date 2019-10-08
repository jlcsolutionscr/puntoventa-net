﻿Imports LeandroSoftware.Core.ClienteWCF

Public Class FrmEstadoDocumentoElectronico
#Region "Variables"
    Private listadoDocumentosPendientes As IList
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcConsecutivo As New DataGridViewTextBoxColumn
        Dim dvcClave As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcEstado As New DataGridViewTextBoxColumn
        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdDocumento"
        dvcId.Width = 0
        dvcId.Visible = False
        dgvDatos.Columns.Add(dvcId)
        dvcConsecutivo.HeaderText = "Consecutivo"
        dvcConsecutivo.DataPropertyName = "Consecutivo"
        dvcConsecutivo.Width = 150
        dgvDatos.Columns.Add(dvcConsecutivo)
        dvcClave.HeaderText = "Clave"
        dvcClave.DataPropertyName = "ClaveNumerica"
        dvcClave.Width = 370
        dgvDatos.Columns.Add(dvcClave)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 150
        dgvDatos.Columns.Add(dvcFecha)
        dvcEstado.HeaderText = "Estado"
        dvcEstado.DataPropertyName = "EstadoEnvio"
        dvcEstado.Width = 80
        dgvDatos.Columns.Add(dvcEstado)
    End Sub

    Private Async Sub ActualizarDatos()
        Try
            picLoader.Visible = True
            listadoDocumentosPendientes = Await ClienteFEWCF.ObtenerListadoDocumentosElectronicosEnProceso(FrmPrincipal.empresaGlobal.IdEmpresa)
            dgvDatos.DataSource = listadoDocumentosPendientes
            If listadoDocumentosPendientes.Count() > 0 Then
                btnProcesar.Enabled = True
            Else
                MessageBox.Show("No existen registros pendientes. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            End If
            picLoader.Visible = False
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        dgvDatos.Refresh()
    End Sub
#End Region

#Region "Eventos controles"
    Private Sub FrmEstadoDocumentoElectronico_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        EstablecerPropiedadesDataGridView()
        ActualizarDatos()
    End Sub

    Private Async Sub BtnProcesar_Click(sender As Object, e As EventArgs) Handles btnProcesar.Click
        Try
            picLoader.Visible = True
            Await ClienteFEWCF.ProcesarDocumentosElectronicosPendientes(FrmPrincipal.empresaGlobal.IdEmpresa)
            Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        dgvDatos.Refresh()
    End Sub
#End Region
End Class