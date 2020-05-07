Imports System.IO
Imports System.Xml
Imports LeandroSoftware.Core.Dominio.Entidades
Imports System.Text
Imports System.Threading.Tasks
Imports LeandroSoftware.Core.TiposComunes
Imports LeandroSoftware.ClienteWCF

Public Class FrmDetalleDocumentoElectronico
#Region "Variables"
    Private listadoDocumentosProcesados As IList
    Private intTotalDocumentos As Integer
    Private intIndiceDePagina As Integer
    Private intFilasPorPagina As Integer = 17
    Private intCantidadDePaginas As Integer
    Private bolRespuestaVisible = False
    Private bolInit As Boolean = False
#End Region

#Region "Métodos"
    Private Sub EstablecerPropiedadesDataGridView()
        Dim dvcId As New DataGridViewTextBoxColumn
        Dim dvcFecha As New DataGridViewTextBoxColumn
        Dim dvcConsecutivo As New DataGridViewTextBoxColumn
        Dim dvcNombreReceptor As New DataGridViewTextBoxColumn
        Dim dvcTotal As New DataGridViewTextBoxColumn
        Dim dvcEstado As New DataGridViewTextBoxColumn
        dgvDatos.Columns.Clear()
        dgvDatos.AutoGenerateColumns = False
        dvcId.HeaderText = "Id"
        dvcId.DataPropertyName = "IdDocumento"
        dvcId.Width = 0
        dvcId.Visible = False
        dgvDatos.Columns.Add(dvcId)
        dvcFecha.HeaderText = "Fecha"
        dvcFecha.DataPropertyName = "Fecha"
        dvcFecha.Width = 70
        dgvDatos.Columns.Add(dvcFecha)
        dvcConsecutivo.HeaderText = "Consecutivo"
        dvcConsecutivo.DataPropertyName = "Consecutivo"
        dvcConsecutivo.Width = 150
        dgvDatos.Columns.Add(dvcConsecutivo)
        dvcNombreReceptor.HeaderText = "Receptor"
        dvcNombreReceptor.DataPropertyName = "NombreReceptor"
        dvcNombreReceptor.Width = 367
        dgvDatos.Columns.Add(dvcNombreReceptor)
        dvcTotal.HeaderText = "Total"
        dvcTotal.DataPropertyName = "MontoTotal"
        dvcTotal.Width = 100
        dvcTotal.DefaultCellStyle = FrmPrincipal.dgvDecimal
        dgvDatos.Columns.Add(dvcTotal)
        dvcEstado.HeaderText = "Estado"
        dvcEstado.DataPropertyName = "EstadoEnvio"
        dvcEstado.Width = 80
        dgvDatos.Columns.Add(dvcEstado)
    End Sub

    Private Async Function ActualizarDatos(ByVal intNumeroPagina As Integer) As Task
        Try
            listadoDocumentosProcesados = Await Puntoventa.ObtenerListadoDocumentosElectronicosProcesados(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, intNumeroPagina, intFilasPorPagina, FrmPrincipal.usuarioGlobal.Token)
            dgvDatos.DataSource = listadoDocumentosProcesados
            If listadoDocumentosProcesados.Count() > 0 Then
                btnMostrarRespuesta.Enabled = True
            Else
                btnMostrarRespuesta.Enabled = False
            End If
            lblPagina.Text = "Página " & intNumeroPagina & " de " & intCantidadDePaginas
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
            Exit Function
        End Try
        dgvDatos.Refresh()
    End Function

    Private Async Function ObtenerCantidadDocumentosProcesados() As Task
        Try
            intTotalDocumentos = Await Puntoventa.ObtenerTotalDocumentosElectronicosProcesados(FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, FrmPrincipal.usuarioGlobal.Token)
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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

    Private Async Function CargarCombos() As Task
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.bolSeleccionaSucursal
    End Function
#End Region

#Region "Eventos controles"
    Private Sub FrmDetalleDocumentoElectronico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub
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
        Try
            Await CargarCombos()
            rtxDetalleRespuesta.Visible = False
            EstablecerPropiedadesDataGridView()
            Await ObtenerCantidadDocumentosProcesados()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
            bolInit = True
        Catch ex As Exception
            MessageBox.Show(ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Close()
        End Try
    End Sub

    Private Async Sub BtnMostrarRespuesta_Click(sender As Object, e As EventArgs) Handles btnMostrarRespuesta.Click
        Try
            If Not bolRespuestaVisible Then
                Dim intIndex As Integer = dgvDatos.CurrentRow.Index
                Dim documento As DocumentoDetalle = listadoDocumentosProcesados.Item(intIndex)
                If documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Aceptado Or documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Rechazado Then
                    Dim consulta As DocumentoElectronico = Await Puntoventa.ObtenerDocumentoElectronico(documento.IdDocumento, FrmPrincipal.usuarioGlobal.Token)
                    rtxDetalleRespuesta.Visible = True
                    If consulta.Respuesta IsNot Nothing Then
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
                    Else
                        rtxDetalleRespuesta.Text = consulta.ErrorEnvio
                    End If
                    btnMostrarRespuesta.Text = "Mostrar lista"
                    bolRespuestaVisible = True
                    btnMostrarXML.Enabled = False
                End If
            Else
                rtxDetalleRespuesta.Visible = False
                bolRespuestaVisible = False
                btnMostrarRespuesta.Text = "Mostrar respuesta"
                btnMostrarXML.Enabled = True
            End If
        Catch ex As Exception
            rtxDetalleRespuesta.Visible = False
            MessageBox.Show("Error al procesar la petición de datos del documento electrónico. Contacte a su proveedor. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub btnReenviarNotificacion_Click(sender As Object, e As EventArgs) Handles btnReenviarNotificacion.Click
        Dim strCorreoReceptor = ""
        Dim intIndex As Integer = dgvDatos.CurrentRow.Index
        Dim documento As DocumentoDetalle = listadoDocumentosProcesados.Item(intIndex)
        If documento.CorreoNotificacion <> "" Then
            If MessageBox.Show("Desea utilizar la dirección(es) de correo electrónico registrada(s) en el documento?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = DialogResult.Yes Then
                strCorreoReceptor = documento.CorreoNotificacion
            Else
                strCorreoReceptor = InputBox("Ingrese la(s) dirección(es) de correo electrónico donde se enviará el comprobante, separados por punto y coma:")
            End If
        Else
            strCorreoReceptor = InputBox("Ingrese la(s) dirección(es) de correo electrónico donde se enviará el comprobante, separados por punto y coma:")
        End If
        If strCorreoReceptor <> "" Then
            Try
                Await Puntoventa.EnviarNotificacion(documento.IdDocumento, strCorreoReceptor, FrmPrincipal.usuarioGlobal.Token)
                MessageBox.Show("Comprobante electrónico enviado satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al enviar el comprobante:" & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        Else
            MessageBox.Show("Debe ingresar la dirección(es) de correo electrónico para hacer el envío del comprobante. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End If
    End Sub

    Private Async Sub btnMostrarXML_Click(sender As Object, e As EventArgs) Handles btnMostrarXML.Click
        Try
            If Not bolRespuestaVisible Then
                Dim intIndex As Integer = dgvDatos.CurrentRow.Index
                Dim documento As DocumentoDetalle = listadoDocumentosProcesados.Item(intIndex)
                If documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Aceptado Or documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Rechazado Then
                    Dim consulta As DocumentoElectronico = Await Puntoventa.ObtenerDocumentoElectronico(documento.IdDocumento, FrmPrincipal.usuarioGlobal.Token)
                    rtxDetalleRespuesta.Visible = True
                    If consulta.DatosDocumento IsNot Nothing Then
                        Dim sw As New StringWriter()
                        Dim xw As New XmlTextWriter(sw) With {
                            .Formatting = Formatting.Indented,
                            .Indentation = 4
                        }
                        Dim datos As XmlDocument = New XmlDocument()
                        Dim strRespuesta As String = Encoding.UTF8.GetString(consulta.DatosDocumento)
                        datos.LoadXml(strRespuesta)
                        datos.Save(xw)
                        rtxDetalleRespuesta.Text = sw.ToString()
                    Else
                        rtxDetalleRespuesta.Text = "No se puede mostrar la información de este documento. . ."
                    End If
                    btnMostrarXML.Text = "Mostrar lista"
                    bolRespuestaVisible = True
                    btnMostrarRespuesta.Enabled = False
                End If
            Else
                rtxDetalleRespuesta.Visible = False
                bolRespuestaVisible = False
                btnMostrarXML.Text = "Mostrar XML"
                btnMostrarRespuesta.Enabled = True
            End If
        Catch ex As Exception
            rtxDetalleRespuesta.Visible = False
            MessageBox.Show("Error al procesar la petición de datos del documento electrónico. Contacte a su proveedor. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
        End Try
    End Sub

    Private Async Sub cboSucursal_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboSucursal.SelectedIndexChanged
        If bolInit Then
            Await ObtenerCantidadDocumentosProcesados()
            intIndiceDePagina = 1
            Await ActualizarDatos(intIndiceDePagina)
        End If
    End Sub

    Private Sub dgvDatos_CellContentClick(sender As Object, e As DataGridViewCellEventArgs) Handles dgvDatos.CellContentClick
        Dim intIndex As Integer = dgvDatos.CurrentRow.Index
        Dim documento As DocumentoDetalle = listadoDocumentosProcesados.Item(intIndex)
        btnReenviarNotificacion.Enabled = False
        btnGenerar.Enabled = False
        If documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Aceptado And documento.EsMensajeReceptor = "N" Then btnReenviarNotificacion.Enabled = True
        If documento.EstadoEnvio = StaticEstadoDocumentoElectronico.Rechazado And documento.EsMensajeReceptor = "N" And documento.Reprocesado = False Then btnGenerar.Enabled = True
    End Sub

    Private Async Sub btnGenerar_Click(sender As Object, e As EventArgs) Handles btnGenerar.Click
        If MessageBox.Show("Esta seguro de generar nuevamente el documento electronico?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            btnGenerar.Enabled = False
            dgvDatos.Enabled = False
            Dim intIndex As Integer = dgvDatos.CurrentRow.Index
            Dim documento As DocumentoDetalle = listadoDocumentosProcesados.Item(intIndex)
            Try
                Await Puntoventa.ReprocesarDocumentoElectronico(documento.IdDocumento, FrmPrincipal.usuarioGlobal.Token)
                listadoDocumentosProcesados.Item(intIndex).Reprocesado = True
                MessageBox.Show("Transacción procesada satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Catch ex As Exception
                MessageBox.Show("Error al enviar el comprobante:" & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
            btnGenerar.Enabled = True
            dgvDatos.Enabled = True
        End If
    End Sub
#End Region
End Class