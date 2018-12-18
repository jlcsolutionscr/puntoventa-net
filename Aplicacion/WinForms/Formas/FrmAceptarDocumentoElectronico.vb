Imports System.IO
Imports System.Xml

Public Class FrmAceptarDocumentoElectronico
#Region "Variables"
    Private strDatos As String
#End Region

#Region "Eventos Controles"
    Private Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Try
            If MessageBox.Show("Desea enviar el documento al servicio web de Hacienda?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Dim intEstado As Integer
                If rbnAceptado.Checked Then
                    intEstado = 0
                ElseIf rbnAceptarParcial.Checked Then
                    intEstado = 1
                Else
                    intEstado = 2
                End If
                'servicioFacturacion.GeneraMensajeReceptor(strDatos, FrmMenuPrincipal.empresaGlobal.IdEmpresa, FrmMenuPrincipal.intSucursal, FrmMenuPrincipal.intTerminal, intEstado)
                MessageBox.Show("Documento enviado satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Else
                MessageBox.Show("Proceso cancelado por el usuario. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar el documento electrónico: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub

    Private Sub BtnCargar_Click(sender As Object, e As EventArgs) Handles btnCargar.Click
        ofdAbrirDocumento.DefaultExt = "xml"
        ofdAbrirDocumento.Filter = "XML Files|*.xml"
        Dim result As DialogResult = ofdAbrirDocumento.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            Try
                Dim sw As New StringWriter()
                Dim xw As New XmlTextWriter(sw) With {
                    .Formatting = Formatting.Indented,
                    .Indentation = 4
                }
                Dim datos As XmlDocument = New XmlDocument()
                datos.Load(ofdAbrirDocumento.FileName)
                strDatos = datos.OuterXml
                datos.Save(xw)
                txtMensaje.Text = sw.ToString()
                btnEnviar.Enabled = True
            Catch ex As Exception
                MessageBox.Show("Error al intentar cargar el archivo. Por favor intente de nuevo o contacte a su proveedor. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                btnEnviar.Enabled = False
                Close()
                Exit Sub
            End Try
        End If
    End Sub
#End Region
End Class