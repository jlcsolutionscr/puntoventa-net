Imports MySql.Data.MySqlClient

Public Class FrmDatabase
    Inherits Form

#Region "Variables"
    Private dtTable As DataTable
    Private strHost, strDatabase, strUser, strPassword, strFormulario As String
    Public cargado As Boolean = False
#End Region

#Region "Eventos Controles"
    Public Sub New(ByVal pFormulario As String)
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        strFormulario = pFormulario
    End Sub

    Private Sub FrmEmpresa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        Try
            Dim dsDataSet As New DataSet()
            Dim strSQLString As String
            If strFormulario = "Empresa" Then
                strSQLString = "SELECT SCHEMA_NAME AS BaseDeDatos FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME NOT IN('mysql','information_schema','performance_schema','test')"
            Else
                strSQLString = "SELECT SCHEMA_NAME AS BaseDeDatos FROM INFORMATION_SCHEMA.SCHEMATA WHERE SCHEMA_NAME NOT IN('information_schema','performance_schema','test')"
            End If
            Dim ocxConexion As MySqlConnection = New MySqlConnection(FrmMDIMenu.strCadenaConexion)
            ocxConexion.Open()
            Dim adapter As MySqlDataAdapter
            adapter = New MySqlDataAdapter(strSQLString, ocxConexion)
            adapter.Fill(dsDataSet, "Producto")
            ocxConexion.Close()
            cboDatabase.DataSource = dsDataSet.Tables("Producto")
        Catch ex As Exception
            MessageBox.Show("Error: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        cboDatabase.ValueMember = "BaseDeDatos"
        cboDatabase.DisplayMember = "BaseDeDatos"
    End Sub

    Private Sub BtnSalir_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnSalir.Click
        Me.Close()
    End Sub

    Private Sub BtnIngresar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnIngresar.Click
        If strFormulario = "Empresa" Then
            FrmMDIMenu.strDatabase = cboDatabase.Text
            Dim formEmpresa As New FrmEmpresa With {
                .MdiParent = FrmMDIMenu
            }
            formEmpresa.Show()
        End If
    End Sub
#End Region
End Class