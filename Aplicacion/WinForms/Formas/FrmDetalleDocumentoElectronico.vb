Imports LeandroSoftware.Puntoventa.Servicios
Imports Unity

Public Class FrmDetalleDocumentoElectronico
#Region "Variables"
    Private servicioFacturacion As IFacturacionService
    Private listadoDocumentosPprocesados As IList
    Private intTotalDocumentos As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 17
    Private intCantidadDePaginas As Integer
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

    Private Sub ActualizarDatos(ByVal intNumeroPagina As Integer)
        Try
            listadoDocumentosPprocesados = servicioFacturacion.ObtenerListaDocumentosElectronicosProcesados(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina)
            dgvDatos.DataSource = listadoDocumentosPprocesados
            If listadoDocumentosPprocesados.Count() > 0 Then
                btnGenerarPDF.Enabled = False
            Else
                btnGenerarPDF.Enabled = False
            End If
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        dgvDatos.Refresh()
    End Sub

    Private Async Sub ValidarCantidadClientes()
        Try
            intTotalDocumentos = Await servicioFacturacion.ObtenerTotalDocumentosElectronicosProcesados(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        intCantidadDePaginas = Math.Truncate(intTotalDocumentos / intFilasPorPagina) + IIf((intTotalDocumentos Mod intFilasPorPagina) = 0, 0, 1)

        If intCantidadDePaginas > 1 Then
            btnLast.Enabled = True
            btnNext.Enabled = True
            btnPrevious.Enabled = True
            btnFirst.Enabled = True
        Else
            btnLast.Enabled = False
            btnNext.Enabled = False
            btnPrevious.Enabled = False
            btnFirst.Enabled = False
        End If
    End Sub
#End Region

#Region "Eventos controles"
    Private Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub BtnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Sub FrmDetalleDocumentoElectronico_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        Try
            servicioFacturacion = FrmMenuPrincipal.unityContainer.Resolve(Of IFacturacionService)()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Sub
        End Try
        EstablecerPropiedadesDataGridView()
        picLoader.Visible = True
        ValidarCantidadClientes()
        intIndiceDePagina = 1
        ActualizarDatos(intIndiceDePagina)
        picLoader.Visible = False
    End Sub

    Private Sub btnGenerarPDF_Click(sender As Object, e As EventArgs) Handles btnGenerarPDF.Click
        If MessageBox.Show("Desea generar el documento en formato PDF?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            Try
                'TODO Generar PDF
                MessageBox.Show("Registros procesados correctamente. La lista será actualizada. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al procesar los documentos pendientes. Por favor contacte con su proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Close()
            End Try
        End If
    End Sub
#End Region
End Class