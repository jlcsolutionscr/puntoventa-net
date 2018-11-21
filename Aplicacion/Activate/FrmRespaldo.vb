Imports System.IO
Imports LeandroSoftware.Core
Imports MySql.Data.MySqlClient

Public Class FrmRespaldo
    Private Sub BtnRespalda_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnRespalda.Click
        If MessageBox.Show("Desea realizar el respaldo de base de datos?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.Yes Then
            Dim strFileName As String = cboDatabase.SelectedValue & "-" & "-" & DateTime.Now.ToShortDateString.Replace("/", "") & ".sql"
            Dim strData As String
            Dim bytes() As Byte = New Byte() {}
            Dim strBackupType As String
            If rdbDatos.Checked Then
                strBackupType = " --complete-insert --no-create-info"
            Else
                strBackupType = " --no-data"
            End If
            Try
                strData = Utilitario.GenerarRespaldo(FrmMDIMenu.strUser, FrmMDIMenu.strPassword, FrmMDIMenu.strHost, cboDatabase.SelectedValue, FrmMDIMenu.strMySQLDumpOptions & strBackupType)
                bytes = System.Text.Encoding.UTF8.GetBytes(strData)
            Catch ex As Exception
                MessageBox.Show("Error al generar el respaldo de la base de datos: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try

            If File.Exists(strFileName) Then
                If MessageBox.Show("El respaldo ya fue generado para la fecha actual. Desea sobreescribir el archivo de respaldo?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = MsgBoxResult.No Then
                    MessageBox.Show("Proceso de respaldo cancelado por el usuario.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
                    Exit Sub
                End If
                File.Delete(strFileName)
            End If
            Dim bw As BinaryWriter
            Try
                bw = New BinaryWriter(New FileStream(strFileName, FileMode.Create))
                bw.Write(bytes)
                bw.Close()
            Catch ex As Exception
                MessageBox.Show("Error al generar el archivo de respaldo: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
                Exit Sub
            End Try
            MessageBox.Show("Respaldo finalizado satisfactoriamente.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End If
    End Sub

    Private Sub FrmRespaldo_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Dim dsDataSet As New DataSet()
        Dim ocxConexion As MySqlConnection = New MySqlConnection(FrmMDIMenu.strCadenaConexion)
        ocxConexion.Open()
        Dim adapter As MySqlDataAdapter
        adapter = New MySqlDataAdapter("SELECT SCHEMA_NAME AS BaseDeDatos FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME NOT IN('information_schema','performance_schema','test','mysql')", ocxConexion)
        adapter.Fill(dsDataSet, "Producto")
        ocxConexion.Close()
        cboDatabase.DataSource = dsDataSet.Tables("Producto")
        cboDatabase.ValueMember = "BaseDeDatos"
        cboDatabase.DisplayMember = "BaseDeDatos"
    End Sub
End Class
