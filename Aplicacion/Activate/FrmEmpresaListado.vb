Imports MySql.Data.MySqlClient

Public Class FrmEmpresaListado
#Region "Variables"
    Private dtTable As DataTable
#End Region

#Region "Eventos Controles"
    Private Sub FrmEmpresa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dsDataSet As New DataSet()
            Dim strSQLString As String
            strSQLString = "SELECT IdEmpresa, NombreEmpresa FROM Empresa"
            Dim ocxConexion As MySqlConnection = New MySqlConnection(FrmMDIMenu.strCadenaConexion)
            ocxConexion.Open()
            Dim adapter As MySqlDataAdapter
            adapter = New MySqlDataAdapter(strSQLString, ocxConexion)
            adapter.Fill(dsDataSet, "Empresa")
            ocxConexion.Close()
            cboEmpresa.DataSource = dsDataSet.Tables("Empresa")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        cboEmpresa.ValueMember = "IdEmpresa"
        cboEmpresa.DisplayMember = "NombreEmpresa"
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnIngresar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIngresar.Click
        Dim formEmpresa As New FrmEmpresa(cboEmpresa.SelectedValue) With {
            .MdiParent = FrmMDIMenu
        }
        formEmpresa.Show()
    End Sub
#End Region
End Class