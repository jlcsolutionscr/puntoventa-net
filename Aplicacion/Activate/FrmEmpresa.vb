Imports MySql.Data.MySqlClient

Public Class FrmEmpresa
    Inherits Form

#Region "Variables"
    Private dtEquipos, objDatosLocal, dtEmpresa As DataTable
    Private cmb As MySqlCommandBuilder
    Private daDataAdapter As MySqlDataAdapter
    Private bndSource As New BindingSource
    Private objRowEquipo As DataRow
    Private bolInit As Boolean = True
    Private strCadenaConexion, strIdEmpresa As String
    Dim SQLString As String
    Dim I As Short
#End Region

#Region "Métodos"
    Private Function ValidarCampos() As Boolean
        If txtIdEmpresa.Text = "" And txtEquipo.Text = "" And txtPorcentajeIVA.Text = "" Then
            Return False
        Else
            Return True
        End If
    End Function

    Private Sub IniciaDetalleEmpresa()
        dtEquipos = New DataTable()
        dtEquipos.Columns.Add("VALORREGISTRO", GetType(String))
        dtEquipos.Columns.Add("IMPRESORAFACTURA", GetType(String))
        dtEquipos.Columns.Add("USAIMPRESORAIMPACTO", GetType(Boolean))
        Dim objPrimaryClmn(0) As DataColumn
        objPrimaryClmn(0) = dtEquipos.Columns(0)
        dtEquipos.PrimaryKey = objPrimaryClmn
    End Sub

    Private Sub CargarDetalleEquipos()
        objDatosLocal = New DataTable
        dtEquipos.Rows.Clear()
        If txtIdEmpresa.Text <> "" Then
            Dim adapter As MySqlDataAdapter
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            adapter = New MySqlDataAdapter("SELECT ValorRegistro, ImpresoraFactura, UsaImpresoraImpacto FROM DetalleRegistro WHERE IdEmpresa = " & txtIdEmpresa.Text, ocxConexion)
            Dim dsDataSet As DataSet = New DataSet()
            adapter.Fill(dsDataSet, "DetalleRegistro")
            ocxConexion.Close()
            objDatosLocal = dsDataSet.Tables("DetalleRegistro")
            If objDatosLocal.Rows.Count > 0 Then
                For I = 0 To objDatosLocal.Rows.Count - 1
                    objRowEquipo = dtEquipos.NewRow
                    objRowEquipo.Item(0) = objDatosLocal.Rows(I).Item(0)
                    objRowEquipo.Item(1) = objDatosLocal.Rows(I).Item(1)
                    objRowEquipo.Item(2) = objDatosLocal.Rows(I).Item(2)
                    dtEquipos.Rows.Add(objRowEquipo)
                Next
            End If
        End If
    End Sub

    Private Sub CargarLineaDetalleEquipo()
        If txtEquipo.Text <> "" Then
            Dim intIndice As Integer = dtEquipos.Rows.IndexOf(dtEquipos.Rows.Find(txtEquipo.Text))
            If intIndice >= 0 Then
                dtEquipos.Rows(intIndice).Item(1) = txtImpresoraFactura.Text
                dtEquipos.Rows(intIndice).Item(2) = chkUsaImpresoraImpacto.Checked
            Else
                objRowEquipo = dtEquipos.NewRow
                objRowEquipo.Item(0) = txtEquipo.Text
                objRowEquipo.Item(1) = txtImpresoraFactura.Text
                objRowEquipo.Item(2) = chkUsaImpresoraImpacto.Checked
                dtEquipos.Rows.Add(objRowEquipo)
                dgvEquipos.Refresh()
                txtEquipo.Text = ""
                txtImpresoraFactura.Text = ""
            End If
        End If
    End Sub

    Private Sub EstablecerPropiedadesDataGridView()
        dgvEquipos.Columns.Clear()
        dgvEquipos.AutoGenerateColumns = False

        Dim dvcValorEmpresa As New DataGridViewTextBoxColumn
        Dim dvcImpresoraFactura As New DataGridViewTextBoxColumn
        Dim dvcUsaImpresoraImpacto As New DataGridViewCheckBoxColumn

        dvcValorEmpresa.DataPropertyName = "VALORREGISTRO"
        dvcValorEmpresa.HeaderText = "Equipos Registrados"
        dvcValorEmpresa.Width = 100
        dvcValorEmpresa.Visible = True
        dvcValorEmpresa.ReadOnly = True
        dgvEquipos.Columns.Add(dvcValorEmpresa)

        dvcImpresoraFactura.DataPropertyName = "IMPRESORAFACTURA"
        dvcImpresoraFactura.HeaderText = "Impresora Fact"
        dvcImpresoraFactura.Width = 150
        dvcImpresoraFactura.Visible = True
        dvcImpresoraFactura.ReadOnly = True
        dgvEquipos.Columns.Add(dvcImpresoraFactura)

        dvcUsaImpresoraImpacto.DataPropertyName = "USAIMPRESORAIMPACTO"
        dvcUsaImpresoraImpacto.HeaderText = "Usa Impr. Impacto"
        dvcUsaImpresoraImpacto.Width = 80
        dvcUsaImpresoraImpacto.Visible = True
        dvcUsaImpresoraImpacto.ReadOnly = True
        dgvEquipos.Columns.Add(dvcUsaImpresoraImpacto)
    End Sub

    Private Sub CargarCombos(IdProvincia As Integer, IdCanton As Integer, IdDistrito As Integer)
        Try
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            Dim adapter As MySqlDataAdapter = New MySqlDataAdapter("SELECT IdTipoIdentificacion, Descripcion FROM TipoIdentificacion", ocxConexion)
            Dim dsDataSet As DataSet = New DataSet()
            adapter.Fill(dsDataSet, "TipoIdentificacion")
            objDatosLocal = dsDataSet.Tables("TipoIdentificacion")
            cboTipoIdentificacion.DataSource = dsDataSet.Tables("TipoIdentificacion")
            cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion"
            cboTipoIdentificacion.DisplayMember = "Descripcion"
            adapter = New MySqlDataAdapter("SELECT IdProvincia, Descripcion FROM Provincia", ocxConexion)
            adapter.Fill(dsDataSet, "Provincia")
            cboProvincia.DataSource = dsDataSet.Tables("Provincia")
            cboProvincia.ValueMember = "IdProvincia"
            cboProvincia.DisplayMember = "Descripcion"
            adapter = New MySqlDataAdapter("SELECT IdCanton, Descripcion FROM Canton WHERE IdProvincia = " & IdProvincia, ocxConexion)
            adapter.Fill(dsDataSet, "Canton")
            cboCanton.DataSource = dsDataSet.Tables("Canton")
            cboCanton.ValueMember = "IdCanton"
            cboCanton.DisplayMember = "Descripcion"
            adapter = New MySqlDataAdapter("SELECT IdDistrito, Descripcion FROM Distrito WHERE IdProvincia = " & IdProvincia & " And IdCanton = " & IdCanton, ocxConexion)
            adapter.Fill(dsDataSet, "Distrito")
            cboDistrito.DataSource = dsDataSet.Tables("Distrito")
            cboDistrito.ValueMember = "IdDistrito"
            cboDistrito.DisplayMember = "Descripcion"
            adapter = New MySqlDataAdapter("SELECT IdBarrio, Descripcion FROM Barrio WHERE IdProvincia = " & IdProvincia & " And IdCanton = " & IdCanton & " And IdDistrito = " & IdDistrito, ocxConexion)
            adapter.Fill(dsDataSet, "Barrio")
            cboBarrio.DataSource = dsDataSet.Tables("Barrio")
            cboBarrio.ValueMember = "IdBarrio"
            cboBarrio.DisplayMember = "Descripcion"
            ocxConexion.Close()
        Catch ex As Exception
            MessageBox.Show(ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            Exit Sub
        End Try
    End Sub
#End Region

#Region "Eventos Controles"
    Public Sub New(ByVal strEmpresa As String)
        InitializeComponent()
        ' Agregue cualquier inicialización después de la llamada a InitializeComponent().
        strIdEmpresa = strEmpresa
    End Sub

    Private Sub FrmEmpresa_Load(sender As Object, e As EventArgs) Handles MyBase.Load
        strCadenaConexion = "Database = " & FrmMDIMenu.strDatabase & ";User Id = " & FrmMDIMenu.strUser & ";Password = " & FrmMDIMenu.strPassword & "; data Source = " & FrmMDIMenu.strHost & "; SslMode = none"
        dtEmpresa = New DataTable()
        Try
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            daDataAdapter = New MySqlDataAdapter("SELECT IdEmpresa, NombreEmpresa, NombreComercial, IdTipoIdentificacion, Identificacion, IdProvincia, IdCanton, IdDistrito, IdBarrio, Direccion, Telefono, CuentaCorreoElectronico, ServicioFacturaElectronicaURL, IdCertificado, PinCertificado, UltimoDocFE, UltimoDocND, UltimoDocNC, UltimoDocTE, UltimoDocMR, Logotipo, FechaVence, PorcentajeIVA, LineasPorFactura, Contabiliza, AutoCompletaProducto, ModificaDescProducto, DesglosaServicioInst, PorcentajeInstalacion, CodigoServicioInst, IncluyeInsumosEnFactura, RespaldoEnLinea, CierrePorTurnos, FacturaElectronica FROM Empresa WHERE IdEmpresa=" + strIdEmpresa, ocxConexion)
            Dim dsDataSet As DataSet = New DataSet()
            daDataAdapter.Fill(dsDataSet, "empresa")
            ocxConexion.Close()
            dtEmpresa = dsDataSet.Tables("empresa")
        Catch ex As Exception
            MessageBox.Show("Error:   " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Me.Close()
            Exit Sub
        End Try
        bndSource.DataSource = dtEmpresa
        txtIdEmpresa.DataBindings.Add("Text", bndSource, "IdEmpresa")
        txtNombreEmpresa.DataBindings.Add("Text", bndSource, "NombreEmpresa")
        txtNombreComercial.DataBindings.Add("Text", bndSource, "NombreComercial")
        cboTipoIdentificacion.DataBindings.Add("SelectedValue", bndSource, "IdTipoIdentificacion")
        txtIdentificacion.DataBindings.Add("Text", bndSource, "Identificacion")
        cboProvincia.DataBindings.Add("SelectedValue", bndSource, "IdProvincia")
        cboCanton.DataBindings.Add("SelectedValue", bndSource, "IdCanton")
        cboDistrito.DataBindings.Add("SelectedValue", bndSource, "IdDistrito")
        cboBarrio.DataBindings.Add("SelectedValue", bndSource, "IdBarrio")
        txtDireccion.DataBindings.Add("Text", bndSource, "Direccion")
        txtTelefono.DataBindings.Add("Text", bndSource, "Telefono")
        txtCorreoElectronico.DataBindings.Add("Text", bndSource, "CuentaCorreoElectronico")
        txtServicioFacturaElectronica.DataBindings.Add("Text", bndSource, "ServicioFacturaElectronicaURL")
        txtIdCertificado.DataBindings.Add("Text", bndSource, "IdCertificado")
        txtPinCertificado.DataBindings.Add("Text", bndSource, "PinCertificado")
        txtUltimoDocFE.DataBindings.Add("Text", bndSource, "UltimoDocFE")
        txtUltimoDocND.DataBindings.Add("Text", bndSource, "UltimoDocND")
        txtUltimoDocNC.DataBindings.Add("Text", bndSource, "UltimoDocNC")
        txtUltimoDocTE.DataBindings.Add("Text", bndSource, "UltimoDocTE")
        txtUltimoDocMR.DataBindings.Add("Text", bndSource, "UltimoDocMR")
        picLogo.DataBindings.Add("Image", bndSource, "Logotipo", True, DataSourceUpdateMode.OnPropertyChanged, New Bitmap(My.Resources.emptyImage))
        txtFecha.DataBindings.Add("Text", bndSource, "FechaVence", True, DataSourceUpdateMode.OnPropertyChanged, "", "dd-MM-yyyy")
        ckbContabiliza.DataBindings.Add("Checked", dtEmpresa, "Contabiliza")
        txtPorcentajeIVA.DataBindings.Add("Text", bndSource, "PorcentajeIVA", True, DataSourceUpdateMode.OnPropertyChanged, 0)
        txtLineasFactura.DataBindings.Add("Text", bndSource, "LineasPorFactura", True, DataSourceUpdateMode.OnPropertyChanged, 0)
        ckbAutoCompleta.DataBindings.Add("Checked", dtEmpresa, "AutoCompletaProducto")
        chkModificaDesc.DataBindings.Add("Checked", dtEmpresa, "ModificaDescProducto")
        chkDesgloseInst.DataBindings.Add("Checked", dtEmpresa, "DesglosaServicioInst")
        txtPorcentajeInstalacion.DataBindings.Add("Text", bndSource, "PorcentajeInstalacion", True, DataSourceUpdateMode.OnPropertyChanged, 0)
        txtCodigoServInst.DataBindings.Add("Text", bndSource, "CodigoServicioInst", True, DataSourceUpdateMode.OnPropertyChanged, 0)
        chkIncluyeInsumosEnFactura.DataBindings.Add("Checked", dtEmpresa, "IncluyeInsumosEnFactura")
        chkRespaldoEnLinea.DataBindings.Add("Checked", dtEmpresa, "RespaldoEnLinea")
        chkCierrePorTurnos.DataBindings.Add("Checked", dtEmpresa, "CierrePorTurnos")
        chkFacturaElectronica.DataBindings.Add("Checked", dtEmpresa, "FacturaElectronica")
        IniciaDetalleEmpresa()
        EstablecerPropiedadesDataGridView()
        dgvEquipos.DataSource = dtEquipos
        CargarCombos(dtEmpresa.Rows(0).Item("IdProvincia"), dtEmpresa.Rows(0).Item("IdCanton"), dtEmpresa.Rows(0).Item("IdDistrito"))
        If dtEmpresa.Rows.Count() = 0 Then
            bndSource.AddNew()
            CargarCombos(1, 1, 1)
            txtIdEmpresa.ReadOnly = False
        Else
            CargarDetalleEquipos()
            dgvEquipos.Refresh()
            txtIdEmpresa.ReadOnly = True
        End If
        bolInit = False
    End Sub

    Private Sub BtnInsertarDetalle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnInsertarDetalle.Click
        CargarLineaDetalleEquipo()
    End Sub

    Private Sub BtnEliminarDetalle_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnEliminarDetalle.Click
        If dtEquipos.Rows.Count > 0 Then
            dtEquipos.Rows.Remove(dtEquipos.Rows.Find(dgvEquipos.CurrentRow.Cells(0).Value))
        End If
    End Sub

    Private Sub cboProvincia_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboProvincia.SelectedIndexChanged
        If Not bolInit And cboProvincia.SelectedValue IsNot Nothing Then
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            Dim dsDataSet As New DataSet()
            Dim adapter As MySqlDataAdapter
            adapter = New MySqlDataAdapter("SELECT IdCanton, Descripcion FROM Canton WHERE IdProvincia = " & cboProvincia.SelectedValue, ocxConexion)
            adapter.Fill(dsDataSet, "Canton")
            cboCanton.DataSource = dsDataSet.Tables("Canton")
            adapter = New MySqlDataAdapter("SELECT IdDistrito, Descripcion FROM Distrito WHERE IdProvincia = " & cboProvincia.SelectedValue & " And IdCanton = 1", ocxConexion)
            adapter.Fill(dsDataSet, "Distrito")
            cboDistrito.DataSource = dsDataSet.Tables("Distrito")
            adapter = New MySqlDataAdapter("SELECT IdBarrio, Descripcion FROM Barrio WHERE IdProvincia = " & cboProvincia.SelectedValue & " And IdCanton = 1 And IdDistrito = 1", ocxConexion)
            adapter.Fill(dsDataSet, "Barrio")
            cboBarrio.DataSource = dsDataSet.Tables("Barrio")
            ocxConexion.Close()
        End If
    End Sub

    Private Sub cboCanton_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboCanton.SelectedIndexChanged
        If Not bolInit And cboCanton.SelectedValue IsNot Nothing Then
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            Dim dsDataSet As New DataSet()
            Dim adapter As MySqlDataAdapter
            adapter = New MySqlDataAdapter("SELECT IdDistrito, Descripcion FROM Distrito WHERE IdProvincia = " & cboProvincia.SelectedValue & " And IdCanton = " & cboCanton.SelectedValue, ocxConexion)
            adapter.Fill(dsDataSet, "Distrito")
            cboDistrito.DataSource = dsDataSet.Tables("Distrito")
            adapter = New MySqlDataAdapter("SELECT IdBarrio, Descripcion FROM Barrio WHERE IdProvincia = " & cboProvincia.SelectedValue & " And IdCanton = " & cboCanton.SelectedValue & " And IdDistrito = 1", ocxConexion)
            adapter.Fill(dsDataSet, "Barrio")
            cboBarrio.DataSource = dsDataSet.Tables("Barrio")
            ocxConexion.Close()
        End If
    End Sub

    Private Sub cboDistrito_SelectedIndexChanged(sender As Object, e As EventArgs) Handles cboDistrito.SelectedIndexChanged
        If Not bolInit And cboDistrito.SelectedValue IsNot Nothing Then
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            Dim dsDataSet As New DataSet()
            Dim adapter As MySqlDataAdapter
            adapter = New MySqlDataAdapter("SELECT IdBarrio, Descripcion FROM Barrio WHERE IdProvincia = " & cboProvincia.SelectedValue & " And IdCanton = " & cboCanton.SelectedValue & " And IdDistrito = " & cboDistrito.SelectedValue, ocxConexion)
            adapter.Fill(dsDataSet, "Barrio")
            cboBarrio.DataSource = dsDataSet.Tables("Barrio")
            ocxConexion.Close()
        End If
    End Sub

    Private Sub CmdCancel_Click(sender As Object, e As EventArgs) Handles cmdCancel.Click
        bndSource.CancelEdit()
        Close()
    End Sub

    Private Sub btnCargarLogo_Click(sender As Object, e As EventArgs) Handles btnCargarLogo.Click
        ofdAbrirDocumento.DefaultExt = "png"
        ofdAbrirDocumento.Filter = "Image Files|*.jpg;*.jpeg;*.png;"
        Dim result As DialogResult = ofdAbrirDocumento.ShowDialog()
        If result = Windows.Forms.DialogResult.OK Then
            Try
                picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName)
            Catch ex As Exception
                MessageBox.Show("Error al intentar cargar el archivo. Verifique que sea un archivo de imagen válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error)
            End Try
        End If
    End Sub

    Private Sub CmdUpdate_Click(sender As Object, e As EventArgs) Handles cmdUpdate.Click
        If Not ValidarCampos() Then
            MessageBox.Show("Información incompleta", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
            Exit Sub
        End If
        Try
            cmb = New MySqlCommandBuilder(daDataAdapter)
            daDataAdapter.InsertCommand = cmb.GetInsertCommand.Clone
            daDataAdapter.InsertCommand.CommandText += "; SELECT last_insert_id() as IdEmpresa"
            daDataAdapter.InsertCommand.UpdatedRowSource = UpdateRowSource.FirstReturnedRecord
            daDataAdapter.UpdateCommand = cmb.GetUpdateCommand
            daDataAdapter.DeleteCommand = cmb.GetDeleteCommand
            bndSource.EndEdit()
            daDataAdapter.Update(dtEmpresa)
            dtEmpresa.AcceptChanges()
            SQLString = "DELETE FROM DetalleRegistro WHERE IdEmpresa = " & txtIdEmpresa.Text
            EjecutarSQL(strCadenaConexion, SQLString)
            For I = 0 To dtEquipos.Rows.Count - 1
                SQLString = "INSERT INTO DetalleRegistro VALUES(" & txtIdEmpresa.Text & "," & dtEquipos.Rows(I).Item(0).ToString & ",'" & dtEquipos.Rows(I).Item(1).ToString.Replace("\", "\\") & "', " & dtEquipos.Rows(I).Item(2).ToString & ")"
                EjecutarSQL(strCadenaConexion, SQLString)
            Next
        Catch ex As Exception
            dtEmpresa.RejectChanges()
            MessageBox.Show("Error al actualizar el registro: " & ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        End Try
        MessageBox.Show("Transacción procesada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information)
        Close()
    End Sub

    Private Sub CmdConsultar_Click(ByVal sender As Object, ByVal e As EventArgs) Handles CmdConsultar.Click
        Dim fs, d As Object
        fs = CreateObject("Scripting.FileSystemObject")
        d = fs.GetDrive(fs.GetDriveName(fs.GetAbsolutePathName("c:")))
        txtEquipo.Text = CStr(Val(d.SerialNumber))
    End Sub

    Private Sub EjecutarSQL(strCadenaConexion As String, SQLString As String)
        Try
            Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
            ocxConexion.Open()
            Dim odcDataCommand As MySqlCommand = New MySqlCommand(SQLString, ocxConexion)
            odcDataCommand.ExecuteNonQuery()
            ocxConexion.Close()
        Catch ex As Exception
            Throw ex
        End Try
    End Sub
#End Region
End Class