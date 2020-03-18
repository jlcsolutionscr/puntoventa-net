Imports System.IO
Imports System.Xml
Imports LeandroSoftware.ClienteWCF

Public Class FrmAceptarDocumentoElectronico
#Region "Variables"
    Private strDatos As String
#End Region

#Region "Eventos Controles"
    Private Sub FrmAceptarDocumentoElectronico_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        For Each ctl As Control In Controls
            If TypeOf (ctl) Is TextBox Then
                AddHandler DirectCast(ctl, TextBox).Enter, AddressOf EnterTexboxHandler
                AddHandler DirectCast(ctl, TextBox).Leave, AddressOf LeaveTexboxHandler
            End If
        Next
    End Sub

    Private Async Sub FrmAceptarDocumentoElectronico_Shown(sender As Object, e As EventArgs) Handles MyBase.Shown
        cboSucursal.ValueMember = "Id"
        cboSucursal.DisplayMember = "Descripcion"
        cboSucursal.DataSource = Await Puntoventa.ObtenerListadoSucursales(FrmPrincipal.empresaGlobal.IdEmpresa, FrmPrincipal.usuarioGlobal.Token)
        cboSucursal.SelectedValue = FrmPrincipal.equipoGlobal.IdSucursal
        cboSucursal.Enabled = FrmPrincipal.bolSeleccionaSucursal
    End Sub

    Private Sub EnterTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.PeachPuff
    End Sub

    Private Sub LeaveTexboxHandler(sender As Object, e As EventArgs)
        Dim textbox As TextBox = DirectCast(sender, TextBox)
        textbox.BackColor = Color.White
    End Sub

    Private Async Sub BtnEnviar_Click(sender As Object, e As EventArgs) Handles btnEnviar.Click
        Try
            If MessageBox.Show("Desea enviar el documento al servicio web de Hacienda?", "JLC Solutions CR", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
                Dim intEstado As Integer
                If rbnAceptado.Checked Then
                    intEstado = 0
                ElseIf rbnAceptarParcial.Checked Then
                    intEstado = 1
                Else
                    intEstado = 2
                End If
                Await Puntoventa.GenerarMensajeReceptor(strDatos, FrmPrincipal.empresaGlobal.IdEmpresa, cboSucursal.SelectedValue, 1, intEstado, chkIvaAcreditable.Checked, FrmPrincipal.usuarioGlobal.Token)
                MessageBox.Show("Documento enviado satisfactoriamente. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
                Close()
            Else
                MessageBox.Show("Proceso cancelado por el usuario. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Information)
            End If
        Catch ex As Exception
            MessageBox.Show("Error al cargar el documento electrónico: " & ex.Message, "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
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
                Dim documentoXml As XmlDocument = New XmlDocument()
                documentoXml.Load(ofdAbrirDocumento.FileName)
                If (documentoXml.GetElementsByTagName("Otros").Count > 0) Then
                    Dim otrosNode As XmlNode = documentoXml.GetElementsByTagName("Otros").Item(0)
                    otrosNode.InnerText = ""
                End If
                strDatos = documentoXml.OuterXml.Replace("'", "")
                documentoXml.Save(xw)
                txtMensaje.Text = sw.ToString()
                btnEnviar.Enabled = True
            Catch ex As Exception
                MessageBox.Show("Error al intentar cargar el archivo. Por favor intente de nuevo o contacte a su proveedor. . .", "JLC Solutions CR", MessageBoxButtons.OK, MessageBoxIcon.Error)
                btnEnviar.Enabled = False
                Close()
                Exit Sub
            End Try
        End If
    End Sub
#End Region
End Class