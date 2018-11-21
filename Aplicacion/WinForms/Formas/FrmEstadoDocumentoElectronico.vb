Imports LeandroSoftware.Puntoventa.Servicios
Imports Unity

Public Class FrmEstadoDocumentoElectronico
#Region "Variables"
    Private servicioFacturacion As IFacturacionService
    Private listadoDocumentosPendientes As IList
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcClave As New DataGridViewTextBoxColumn
        Dim dvcConsecutivo As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcEstado As New DataGridViewTextBoxColumn

        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdDocumento"
        dvcId.Width = 50
        dgvDatos.Columns.Add(dvcId)
        dvcClave.HeaderText = "Clave"
        dvcClave.DataPropertyName = "ClaveNumerica"
        dvcClave.Width = 320
        dgvDatos.Columns.Add(dvcClave)
        dvcConsecutivo.HeaderText = "Consecutivo"
        dvcConsecutivo.DataPropertyName = "Consecutivo"
        dvcConsecutivo.Width = 150
        dgvDatos.Columns.Add(dvcConsecutivo)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "FechaEmision"
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
            listadoDocumentosPendientes = Await servicioFacturacion.ObtenerListaDocumentosElectronicosPendientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
            dgvDatos.DataSource = listadoDocumentosPendientes
            If listadoDocumentosPendientes.Count() > 0 Then
                MessageBox.Show("Revise la información y procesa a procesar los documentos pendientes. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                btnEnviar.Enabled = True
            Else
                MessageBox.Show("No existen registros pendientes de procesar. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
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
        Try
            servicioFacturacion = FrmMenuPrincipal.unityContainer.Resolve(Of IFacturacionService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        EstablecerPropiedadesDataGridView()
        ActualizarDatos()
    End Sub

    Private Sub btnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        If MessageBox.Show("Desea enviar el documento al servicio web de Hacienda?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            Try
                servicioFacturacion.ProcesarDocumentosElectronicosPendientes(FrmMenuPrincipal.empresaGlobal.IdEmpresa, listadoDocumentosPendientes)
                MessageBox.Show("Registros procesados correctamente. La lista será actualizada. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                ActualizarDatos()
            Catch ex As Exception
                MessageBox.Show("Error al procesar los documentos pendientes. Por favor contacte con su proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End Try
        End If
    End Sub
#End Region
End Class