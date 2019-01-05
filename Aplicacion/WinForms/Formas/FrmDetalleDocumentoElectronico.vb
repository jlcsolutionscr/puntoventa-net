Imports System.IO
Imports System.Xml
Imports LeandroSoftware.AccesoDatos.Dominio.Entidades
Imports System.Text
Imports System.Threading.Tasks
Imports LeandroSoftware.AccesoDatos.ClienteWCF

Public Class FrmDetalleDocumentoElectronico
#Region "Variables"
    Private listadoDocumentosProcesados As IList
    Private intTotalDocumentos As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 17
    Private intCantidadDePaginas As Integer
    Private bolRespuestaVisible = False
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
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 150
        dgvDatos.Columns.Add(dvcFecha)
        dvcEstado.HeaderText = "Estado"
        dvcEstado.DataPropertyName = "EstadoEnvio"
        dvcEstado.Width = 80
        dgvDatos.Columns.Add(dvcEstado)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            listadoDocumentosProcesados = Await PuntoventaWCF.ObtenerListaDocumentosElectronicosProcesados(FrmMenuPrincipal.empresaGlobal.IdEmpresa, intNumeroPagina, intFilasPorPagina)
            dgvDatos.DataSource = listadoDocumentosProcesados
            If listadoDocumentosProcesados.Count() > 0 Then
                btnMostrarRespuesta.Enabled = True
            Else
                btnMostrarRespuesta.Enabled = False
            End If
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvDatos.Refresh()
    End Function

    Private Async Function ObtenerCantidadDocumentosProcesados() As Task
        Try
            intTotalDocumentos = Await PuntoventaWCF.ObtenerTotalDocumentosElectronicosProcesados(FrmMenuPrincipal.empresaGlobal.IdEmpresa)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
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
    End Function
#End Region

#Region "Eventos controles"
    Private Async Sub BtnFirst_Click(sender As Object, e As EventArgs) Handles btnFirst.Click
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub BtnPrevious_Click(sender As Object, e As EventArgs) Handles btnPrevious.Click
        If intIndiceDePagina > 1 Then
            intIndiceDePagina -= 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnNext_Click(sender As Object, e As EventArgs) Handles btnNext.Click
        If intCantidadDePaginas > intIndiceDePagina Then
            intIndiceDePagina += 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Async Sub BtnLast_Click(sender As Object, e As EventArgs) Handles btnLast.Click
        intIndiceDePagina = intCantidadDePaginas
        Await ActualizarDatos(intIndiceDePagina)
    End Sub

    Private Async Sub FrmDetalleDocumentoElectronico_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        rtxDetalleRespuesta.Visible = False
        EstablecerPropiedadesDataGridView()
        picLoader.Visible = True
        Await ObtenerCantidadDocumentosProcesados()
        intIndiceDePagina = 1
        Await ActualizarDatos(intIndiceDePagina)
        picLoader.Visible = False
    End Sub

    Private Async Sub BtnMostrarRespuesta_Click(sender As Object, e As EventArgs) Handles btnMostrarRespuesta.Click
        Try
            If Not bolRespuestaVisible Then
                Dim intIndex As Integer = dgvDatos.CurrentRow.Index
                Dim documento As DocumentoElectronico = listadoDocumentosProcesados.Item(intIndex)
                If documento.EstadoEnvio = "aceptado" Or documento.EstadoEnvio = "rechazado" Then
                    Dim consulta As DocumentoElectronico = Await PuntoventaWCF.ObtenerDocumentoElectronico(documento.IdDocumento)
                    If consulta.Respuesta IsNot Nothing Then
                        rtxDetalleRespuesta.Visible = True
                        Dim sw As New StringWriter()
                        Dim xw As New XmlTextWriter(sw) With {
                            .Formatting = Formatting.Indented,
                            .Indentation = 4
                        }
                        Dim datos As XmlDocument = New XmlDocument()
                        Dim strRespuesta As String = Encoding.UTF8.GetString(consulta.Respuesta)
                        datos.LoadXml(strRespuesta)
                        datos.Save(xw)
                        rtxDetalleRespuesta.Text = sw.ToString()
                        btnMostrarRespuesta.Text = "Mostrar lista"
                        bolRespuestaVisible = True
                    Else
                    End If
                End If
            Else
                rtxDetalleRespuesta.Visible = False
                bolRespuestaVisible = False
                btnMostrarRespuesta.Text = "Mostrar detalle de la respuesta"
            End If
        Catch ex As Exception
            rtxDetalleRespuesta.Visible = False
            MessageBox.Show("Error al procesar la respuesta del Ministerio de Hacienda. Contacte a su proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub
#End Region
End Class