Imports MySql.Data.MySqlClient

Public Class FrmSeguridad
#Region "Variables"
    Private Acceso, Periodo As Boolean
#End Region

#Region "Eventos Controles"
    Private Sub CmdAceptar_Click(sender As Object, e As EventArgs) Handles CmdAceptar.Click
        If TxtUsuario.Text <> FrmMDIMenu.strUser Then
            MessageBox.Show("Usuario incorrecto.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            TxtUsuario.Text = ""
            TxtClave.Text = ""
            TxtUsuario.Focus()
        Else
            If TxtClave.Text = FrmMDIMenu.strPassword Then
                FrmMDIMenu.bolSeguridad = True
                Me.Close()
            Else
                MessageBox.Show("Clave incorrecta.  Intente de nuevo. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                TxtClave.Text = ""
                TxtClave.Focus()
            End If
        End If
    End Sub

    Private Sub CmdCancelar_Click(sender As Object, e As EventArgs) Handles CmdCancelar.Click
        Me.Close()
        Exit Sub
    End Sub

    Private Sub FrmSeguridad_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
        Try
            Dim ocxConexion As MySqlConnection = New MySqlConnection(FrmMDIMenu.strCadenaConexion)
            ocxConexion.Open()
            Dim odcDataCommand As MySqlCommand = New MySqlCommand("SELECT 1 FROM Dual", ocxConexion)
            Dim strResultado As String = odcDataCommand.ExecuteScalar()
            ocxConexion.Close()
        Catch ex As Exception
            MessageBox.Show("Error al tratar de establecer la conexiéon con la base de datos: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
        End Try
    End Sub
#End Region
End Class