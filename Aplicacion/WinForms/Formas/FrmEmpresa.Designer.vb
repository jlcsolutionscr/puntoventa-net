<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmEmpresa
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()>
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents txtTelefono As System.Windows.Forms.TextBox
    Public WithEvents txtDireccion As System.Windows.Forms.TextBox
    Public WithEvents txtNombreEmpresa As System.Windows.Forms.TextBox
    Public WithEvents txtIdentificacion As System.Windows.Forms.TextBox
    Public WithEvents txtIdEmpresa As System.Windows.Forms.TextBox
    Public WithEvents lblLabel6 As System.Windows.Forms.Label
    Public WithEvents lblLabel5 As System.Windows.Forms.Label
    Public WithEvents lblLabel4 As System.Windows.Forms.Label
    Public WithEvents lblLabel3 As System.Windows.Forms.Label
    Public WithEvents lblLabel2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.txtNombreEmpresa = New System.Windows.Forms.TextBox()
        Me.txtIdentificacion = New System.Windows.Forms.TextBox()
        Me.txtIdEmpresa = New System.Windows.Forms.TextBox()
        Me.lblLabel6 = New System.Windows.Forms.Label()
        Me.lblLabel5 = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel2 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtCorreoNotificacion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboTipoIdentificacion = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.cboProvincia = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCanton = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboDistrito = New System.Windows.Forms.ComboBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboBarrio = New System.Windows.Forms.ComboBox()
        Me.txtNombreComercial = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtCodigoActividad = New System.Windows.Forms.TextBox()
        Me.lblCodigoActividad = New System.Windows.Forms.Label()
        Me.txtFechaRenovacion = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.gpbSucursal = New System.Windows.Forms.GroupBox()
        Me.txtIdTerminal = New System.Windows.Forms.TextBox()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.txtNombreImpresora = New System.Windows.Forms.TextBox()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.txtUltimoMR = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.txtUltimoTE = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtUltimoNC = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtUltimoND = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.txtUltimoFE = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNombreSucursal = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTelefonoSucursal = New System.Windows.Forms.TextBox()
        Me.txtDireccionSucursal = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtIdSucursal = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnCargarCertificado = New System.Windows.Forms.Button()
        Me.txtClaveATV = New System.Windows.Forms.TextBox()
        Me.Label25 = New System.Windows.Forms.Label()
        Me.txtUsuarioATV = New System.Windows.Forms.TextBox()
        Me.Label24 = New System.Windows.Forms.Label()
        Me.txtPinCertificado = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtNombreCertificado = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.ofdAbrirDocumento = New System.Windows.Forms.OpenFileDialog()
        Me.gpbSucursal.SuspendLayout()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(122, 343)
        Me.txtTelefono.MaxLength = 8
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(85, 20)
        Me.txtTelefono.TabIndex = 11
        '
        'txtDireccion
        '
        Me.txtDireccion.AcceptsReturn = True
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccion.Location = New System.Drawing.Point(122, 302)
        Me.txtDireccion.MaxLength = 160
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccion.Size = New System.Drawing.Size(313, 35)
        Me.txtDireccion.TabIndex = 10
        '
        'txtNombreEmpresa
        '
        Me.txtNombreEmpresa.AcceptsReturn = True
        Me.txtNombreEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreEmpresa.Location = New System.Drawing.Point(122, 63)
        Me.txtNombreEmpresa.MaxLength = 0
        Me.txtNombreEmpresa.Name = "txtNombreEmpresa"
        Me.txtNombreEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreEmpresa.Size = New System.Drawing.Size(313, 20)
        Me.txtNombreEmpresa.TabIndex = 1
        '
        'txtIdentificacion
        '
        Me.txtIdentificacion.AcceptsReturn = True
        Me.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacion.Location = New System.Drawing.Point(122, 142)
        Me.txtIdentificacion.MaxLength = 12
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(192, 20)
        Me.txtIdentificacion.TabIndex = 4
        '
        'txtIdEmpresa
        '
        Me.txtIdEmpresa.AcceptsReturn = True
        Me.txtIdEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdEmpresa.Location = New System.Drawing.Point(122, 37)
        Me.txtIdEmpresa.MaxLength = 0
        Me.txtIdEmpresa.Name = "txtIdEmpresa"
        Me.txtIdEmpresa.ReadOnly = True
        Me.txtIdEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdEmpresa.Size = New System.Drawing.Size(49, 20)
        Me.txtIdEmpresa.TabIndex = 0
        Me.txtIdEmpresa.TabStop = False
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(4, 346)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(112, 17)
        Me.lblLabel6.TabIndex = 0
        Me.lblLabel6.Text = "Tel�fono:"
        Me.lblLabel6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel5
        '
        Me.lblLabel5.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel5.Location = New System.Drawing.Point(4, 305)
        Me.lblLabel5.Name = "lblLabel5"
        Me.lblLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel5.Size = New System.Drawing.Size(112, 17)
        Me.lblLabel5.TabIndex = 0
        Me.lblLabel5.Text = "Direcci�n:"
        Me.lblLabel5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(4, 66)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(112, 17)
        Me.lblLabel4.TabIndex = 0
        Me.lblLabel4.Text = "Nombre empresa:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(-26, 145)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel3.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel3.TabIndex = 0
        Me.lblLabel3.Text = "Identificaci�n:"
        Me.lblLabel3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel2
        '
        Me.lblLabel2.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel2.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel2.Location = New System.Drawing.Point(-26, 40)
        Me.lblLabel2.Name = "lblLabel2"
        Me.lblLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel2.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel2.TabIndex = 0
        Me.lblLabel2.Text = "Id Unico:"
        Me.lblLabel2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(94, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 51
        Me.btnCancelar.TabStop = False
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(10, 10)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(78, 22)
        Me.btnGuardar.TabIndex = 50
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtCorreoNotificacion
        '
        Me.txtCorreoNotificacion.AcceptsReturn = True
        Me.txtCorreoNotificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorreoNotificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCorreoNotificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCorreoNotificacion.Location = New System.Drawing.Point(122, 369)
        Me.txtCorreoNotificacion.MaxLength = 0
        Me.txtCorreoNotificacion.Name = "txtCorreoNotificacion"
        Me.txtCorreoNotificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorreoNotificacion.Size = New System.Drawing.Size(313, 20)
        Me.txtCorreoNotificacion.TabIndex = 12
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(7, 372)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(109, 17)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Correo electr�nico:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboTipoIdentificacion
        '
        Me.cboTipoIdentificacion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoIdentificacion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoIdentificacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoIdentificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoIdentificacion.IntegralHeight = False
        Me.cboTipoIdentificacion.ItemHeight = 13
        Me.cboTipoIdentificacion.Location = New System.Drawing.Point(122, 115)
        Me.cboTipoIdentificacion.Name = "cboTipoIdentificacion"
        Me.cboTipoIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoIdentificacion.Size = New System.Drawing.Size(313, 21)
        Me.cboTipoIdentificacion.TabIndex = 3
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(-26, 119)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(142, 17)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Tipo de identificaci�n:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboProvincia
        '
        Me.cboProvincia.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboProvincia.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboProvincia.BackColor = System.Drawing.SystemColors.Window
        Me.cboProvincia.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboProvincia.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboProvincia.IntegralHeight = False
        Me.cboProvincia.ItemHeight = 13
        Me.cboProvincia.Location = New System.Drawing.Point(122, 194)
        Me.cboProvincia.Name = "cboProvincia"
        Me.cboProvincia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboProvincia.Size = New System.Drawing.Size(192, 21)
        Me.cboProvincia.TabIndex = 6
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(-26, 197)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(142, 17)
        Me.Label4.TabIndex = 57
        Me.Label4.Text = "Provincia:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(-26, 224)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(142, 17)
        Me.Label5.TabIndex = 59
        Me.Label5.Text = "Cant�n:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboCanton
        '
        Me.cboCanton.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCanton.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCanton.BackColor = System.Drawing.SystemColors.Window
        Me.cboCanton.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCanton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCanton.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCanton.IntegralHeight = False
        Me.cboCanton.ItemHeight = 13
        Me.cboCanton.Location = New System.Drawing.Point(122, 221)
        Me.cboCanton.Name = "cboCanton"
        Me.cboCanton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCanton.Size = New System.Drawing.Size(192, 21)
        Me.cboCanton.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(-26, 251)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(142, 17)
        Me.Label6.TabIndex = 61
        Me.Label6.Text = "Distrito:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboDistrito
        '
        Me.cboDistrito.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboDistrito.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboDistrito.BackColor = System.Drawing.SystemColors.Window
        Me.cboDistrito.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboDistrito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboDistrito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboDistrito.IntegralHeight = False
        Me.cboDistrito.ItemHeight = 13
        Me.cboDistrito.Location = New System.Drawing.Point(122, 248)
        Me.cboDistrito.Name = "cboDistrito"
        Me.cboDistrito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDistrito.Size = New System.Drawing.Size(192, 21)
        Me.cboDistrito.TabIndex = 8
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(-26, 278)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(142, 17)
        Me.Label7.TabIndex = 63
        Me.Label7.Text = "Barrio:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboBarrio
        '
        Me.cboBarrio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboBarrio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboBarrio.BackColor = System.Drawing.SystemColors.Window
        Me.cboBarrio.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboBarrio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboBarrio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboBarrio.IntegralHeight = False
        Me.cboBarrio.ItemHeight = 13
        Me.cboBarrio.Location = New System.Drawing.Point(122, 275)
        Me.cboBarrio.Name = "cboBarrio"
        Me.cboBarrio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBarrio.Size = New System.Drawing.Size(192, 21)
        Me.cboBarrio.TabIndex = 9
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.AcceptsReturn = True
        Me.txtNombreComercial.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreComercial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreComercial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreComercial.Location = New System.Drawing.Point(122, 89)
        Me.txtNombreComercial.MaxLength = 0
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreComercial.Size = New System.Drawing.Size(313, 20)
        Me.txtNombreComercial.TabIndex = 2
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 92)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(112, 17)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Nombre comercial:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtCodigoActividad
        '
        Me.txtCodigoActividad.AcceptsReturn = True
        Me.txtCodigoActividad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoActividad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoActividad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoActividad.Location = New System.Drawing.Point(122, 168)
        Me.txtCodigoActividad.MaxLength = 6
        Me.txtCodigoActividad.Name = "txtCodigoActividad"
        Me.txtCodigoActividad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoActividad.Size = New System.Drawing.Size(61, 20)
        Me.txtCodigoActividad.TabIndex = 5
        '
        'lblCodigoActividad
        '
        Me.lblCodigoActividad.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigoActividad.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCodigoActividad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCodigoActividad.Location = New System.Drawing.Point(-26, 171)
        Me.lblCodigoActividad.Name = "lblCodigoActividad"
        Me.lblCodigoActividad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblCodigoActividad.Size = New System.Drawing.Size(142, 17)
        Me.lblCodigoActividad.TabIndex = 66
        Me.lblCodigoActividad.Text = "C�digo Actividad:"
        Me.lblCodigoActividad.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFechaRenovacion
        '
        Me.txtFechaRenovacion.AcceptsReturn = True
        Me.txtFechaRenovacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtFechaRenovacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFechaRenovacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFechaRenovacion.Location = New System.Drawing.Point(122, 395)
        Me.txtFechaRenovacion.MaxLength = 10
        Me.txtFechaRenovacion.Name = "txtFechaRenovacion"
        Me.txtFechaRenovacion.ReadOnly = True
        Me.txtFechaRenovacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFechaRenovacion.Size = New System.Drawing.Size(85, 20)
        Me.txtFechaRenovacion.TabIndex = 13
        Me.txtFechaRenovacion.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(4, 398)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(112, 17)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Fecha Renovaci�n:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'gpbSucursal
        '
        Me.gpbSucursal.BackColor = System.Drawing.SystemColors.Control
        Me.gpbSucursal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gpbSucursal.Controls.Add(Me.txtIdTerminal)
        Me.gpbSucursal.Controls.Add(Me.Label22)
        Me.gpbSucursal.Controls.Add(Me.txtNombreImpresora)
        Me.gpbSucursal.Controls.Add(Me.Label21)
        Me.gpbSucursal.Controls.Add(Me.txtUltimoMR)
        Me.gpbSucursal.Controls.Add(Me.Label20)
        Me.gpbSucursal.Controls.Add(Me.txtUltimoTE)
        Me.gpbSucursal.Controls.Add(Me.Label19)
        Me.gpbSucursal.Controls.Add(Me.txtUltimoNC)
        Me.gpbSucursal.Controls.Add(Me.Label17)
        Me.gpbSucursal.Controls.Add(Me.txtUltimoND)
        Me.gpbSucursal.Controls.Add(Me.Label14)
        Me.gpbSucursal.Controls.Add(Me.txtUltimoFE)
        Me.gpbSucursal.Controls.Add(Me.Label13)
        Me.gpbSucursal.Controls.Add(Me.txtNombreSucursal)
        Me.gpbSucursal.Controls.Add(Me.Label3)
        Me.gpbSucursal.Controls.Add(Me.txtTelefonoSucursal)
        Me.gpbSucursal.Controls.Add(Me.txtDireccionSucursal)
        Me.gpbSucursal.Controls.Add(Me.Label10)
        Me.gpbSucursal.Controls.Add(Me.Label11)
        Me.gpbSucursal.Controls.Add(Me.txtIdSucursal)
        Me.gpbSucursal.Controls.Add(Me.Label12)
        Me.gpbSucursal.Cursor = System.Windows.Forms.Cursors.Default
        Me.gpbSucursal.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.gpbSucursal.Location = New System.Drawing.Point(450, 182)
        Me.gpbSucursal.Name = "gpbSucursal"
        Me.gpbSucursal.Padding = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.gpbSucursal.Size = New System.Drawing.Size(437, 263)
        Me.gpbSucursal.TabIndex = 14
        Me.gpbSucursal.TabStop = False
        Me.gpbSucursal.Text = "Datos de la Terminal en Uso"
        '
        'txtIdTerminal
        '
        Me.txtIdTerminal.AcceptsReturn = True
        Me.txtIdTerminal.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdTerminal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdTerminal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdTerminal.Location = New System.Drawing.Point(98, 149)
        Me.txtIdTerminal.MaxLength = 6
        Me.txtIdTerminal.Name = "txtIdTerminal"
        Me.txtIdTerminal.ReadOnly = True
        Me.txtIdTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdTerminal.Size = New System.Drawing.Size(47, 20)
        Me.txtIdTerminal.TabIndex = 104
        Me.txtIdTerminal.TabStop = False
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(35, 152)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(57, 17)
        Me.Label22.TabIndex = 98
        Me.Label22.Text = "Terminal:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNombreImpresora
        '
        Me.txtNombreImpresora.AcceptsReturn = True
        Me.txtNombreImpresora.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreImpresora.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreImpresora.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreImpresora.Location = New System.Drawing.Point(262, 149)
        Me.txtNombreImpresora.MaxLength = 20
        Me.txtNombreImpresora.Name = "txtNombreImpresora"
        Me.txtNombreImpresora.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreImpresora.Size = New System.Drawing.Size(85, 20)
        Me.txtNombreImpresora.TabIndex = 105
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(156, 152)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(100, 17)
        Me.Label21.TabIndex = 96
        Me.Label21.Text = "Nombre impresora:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUltimoMR
        '
        Me.txtUltimoMR.AcceptsReturn = True
        Me.txtUltimoMR.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoMR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoMR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoMR.Location = New System.Drawing.Point(335, 201)
        Me.txtUltimoMR.MaxLength = 20
        Me.txtUltimoMR.Name = "txtUltimoMR"
        Me.txtUltimoMR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoMR.Size = New System.Drawing.Size(70, 20)
        Me.txtUltimoMR.TabIndex = 110
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(208, 204)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(121, 17)
        Me.Label20.TabIndex = 94
        Me.Label20.Text = "Ultima Factura Gastos:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUltimoTE
        '
        Me.txtUltimoTE.AcceptsReturn = True
        Me.txtUltimoTE.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoTE.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoTE.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoTE.Location = New System.Drawing.Point(335, 175)
        Me.txtUltimoTE.MaxLength = 20
        Me.txtUltimoTE.Name = "txtUltimoTE"
        Me.txtUltimoTE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoTE.Size = New System.Drawing.Size(70, 20)
        Me.txtUltimoTE.TabIndex = 109
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(208, 178)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(121, 17)
        Me.Label19.TabIndex = 92
        Me.Label19.Text = "Ultimo Tiquete Elect:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUltimoNC
        '
        Me.txtUltimoNC.AcceptsReturn = True
        Me.txtUltimoNC.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoNC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoNC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoNC.Location = New System.Drawing.Point(134, 229)
        Me.txtUltimoNC.MaxLength = 20
        Me.txtUltimoNC.Name = "txtUltimoNC"
        Me.txtUltimoNC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoNC.Size = New System.Drawing.Size(70, 20)
        Me.txtUltimoNC.TabIndex = 108
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(7, 232)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(121, 17)
        Me.Label17.TabIndex = 90
        Me.Label17.Text = "Ultima Nota Cr�dito:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUltimoND
        '
        Me.txtUltimoND.AcceptsReturn = True
        Me.txtUltimoND.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoND.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoND.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoND.Location = New System.Drawing.Point(134, 203)
        Me.txtUltimoND.MaxLength = 20
        Me.txtUltimoND.Name = "txtUltimoND"
        Me.txtUltimoND.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoND.Size = New System.Drawing.Size(70, 20)
        Me.txtUltimoND.TabIndex = 107
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(7, 206)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(121, 17)
        Me.Label14.TabIndex = 88
        Me.Label14.Text = "Ultima Nota D�bito:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtUltimoFE
        '
        Me.txtUltimoFE.AcceptsReturn = True
        Me.txtUltimoFE.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoFE.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoFE.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoFE.Location = New System.Drawing.Point(134, 175)
        Me.txtUltimoFE.MaxLength = 20
        Me.txtUltimoFE.Name = "txtUltimoFE"
        Me.txtUltimoFE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoFE.Size = New System.Drawing.Size(70, 20)
        Me.txtUltimoFE.TabIndex = 106
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(7, 178)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(121, 17)
        Me.Label13.TabIndex = 86
        Me.Label13.Text = "Ultima Factura Elect:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNombreSucursal
        '
        Me.txtNombreSucursal.AcceptsReturn = True
        Me.txtNombreSucursal.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreSucursal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreSucursal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreSucursal.Location = New System.Drawing.Point(98, 51)
        Me.txtNombreSucursal.MaxLength = 40
        Me.txtNombreSucursal.Name = "txtNombreSucursal"
        Me.txtNombreSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreSucursal.Size = New System.Drawing.Size(307, 20)
        Me.txtNombreSucursal.TabIndex = 101
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(13, 54)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(79, 17)
        Me.Label3.TabIndex = 84
        Me.Label3.Text = "Nombre:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTelefonoSucursal
        '
        Me.txtTelefonoSucursal.AcceptsReturn = True
        Me.txtTelefonoSucursal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefonoSucursal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefonoSucursal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefonoSucursal.Location = New System.Drawing.Point(98, 118)
        Me.txtTelefonoSucursal.MaxLength = 20
        Me.txtTelefonoSucursal.Name = "txtTelefonoSucursal"
        Me.txtTelefonoSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefonoSucursal.Size = New System.Drawing.Size(85, 20)
        Me.txtTelefonoSucursal.TabIndex = 103
        '
        'txtDireccionSucursal
        '
        Me.txtDireccionSucursal.AcceptsReturn = True
        Me.txtDireccionSucursal.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccionSucursal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccionSucursal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccionSucursal.Location = New System.Drawing.Point(98, 77)
        Me.txtDireccionSucursal.MaxLength = 80
        Me.txtDireccionSucursal.Multiline = True
        Me.txtDireccionSucursal.Name = "txtDireccionSucursal"
        Me.txtDireccionSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccionSucursal.Size = New System.Drawing.Size(307, 35)
        Me.txtDireccionSucursal.TabIndex = 102
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(13, 121)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(79, 17)
        Me.Label10.TabIndex = 79
        Me.Label10.Text = "Tel�fono:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(13, 80)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(79, 17)
        Me.Label11.TabIndex = 80
        Me.Label11.Text = "Direcci�n:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtIdSucursal
        '
        Me.txtIdSucursal.AcceptsReturn = True
        Me.txtIdSucursal.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdSucursal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdSucursal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdSucursal.Location = New System.Drawing.Point(98, 25)
        Me.txtIdSucursal.MaxLength = 6
        Me.txtIdSucursal.Name = "txtIdSucursal"
        Me.txtIdSucursal.ReadOnly = True
        Me.txtIdSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdSucursal.Size = New System.Drawing.Size(47, 20)
        Me.txtIdSucursal.TabIndex = 100
        Me.txtIdSucursal.TabStop = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(35, 28)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(57, 17)
        Me.Label12.TabIndex = 78
        Me.Label12.Text = "Sucursal:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'GroupBox1
        '
        Me.GroupBox1.BackColor = System.Drawing.SystemColors.Control
        Me.GroupBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.GroupBox1.Controls.Add(Me.btnCargarCertificado)
        Me.GroupBox1.Controls.Add(Me.txtClaveATV)
        Me.GroupBox1.Controls.Add(Me.Label25)
        Me.GroupBox1.Controls.Add(Me.txtUsuarioATV)
        Me.GroupBox1.Controls.Add(Me.Label24)
        Me.GroupBox1.Controls.Add(Me.txtPinCertificado)
        Me.GroupBox1.Controls.Add(Me.Label18)
        Me.GroupBox1.Controls.Add(Me.txtNombreCertificado)
        Me.GroupBox1.Controls.Add(Me.Label16)
        Me.GroupBox1.Cursor = System.Windows.Forms.Cursors.Default
        Me.GroupBox1.FlatStyle = System.Windows.Forms.FlatStyle.Flat
        Me.GroupBox1.Location = New System.Drawing.Point(450, 25)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(437, 137)
        Me.GroupBox1.TabIndex = 262
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos para la generaci�n de Documentos Electr�nicos"
        '
        'btnCargarCertificado
        '
        Me.btnCargarCertificado.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCargarCertificado.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCargarCertificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargarCertificado.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCargarCertificado.Location = New System.Drawing.Point(377, 23)
        Me.btnCargarCertificado.Name = "btnCargarCertificado"
        Me.btnCargarCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCargarCertificado.Size = New System.Drawing.Size(47, 26)
        Me.btnCargarCertificado.TabIndex = 270
        Me.btnCargarCertificado.TabStop = False
        Me.btnCargarCertificado.Text = "C&argar"
        Me.btnCargarCertificado.UseVisualStyleBackColor = False
        '
        'txtClaveATV
        '
        Me.txtClaveATV.AcceptsReturn = True
        Me.txtClaveATV.BackColor = System.Drawing.SystemColors.Window
        Me.txtClaveATV.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtClaveATV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtClaveATV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtClaveATV.Location = New System.Drawing.Point(111, 104)
        Me.txtClaveATV.MaxLength = 100
        Me.txtClaveATV.Name = "txtClaveATV"
        Me.txtClaveATV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtClaveATV.Size = New System.Drawing.Size(313, 20)
        Me.txtClaveATV.TabIndex = 53
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(14, 107)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(91, 17)
        Me.Label25.TabIndex = 269
        Me.Label25.Text = "Contrase�a ATV:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUsuarioATV
        '
        Me.txtUsuarioATV.AcceptsReturn = True
        Me.txtUsuarioATV.BackColor = System.Drawing.SystemColors.Window
        Me.txtUsuarioATV.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUsuarioATV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuarioATV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUsuarioATV.Location = New System.Drawing.Point(111, 78)
        Me.txtUsuarioATV.MaxLength = 100
        Me.txtUsuarioATV.Name = "txtUsuarioATV"
        Me.txtUsuarioATV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUsuarioATV.Size = New System.Drawing.Size(313, 20)
        Me.txtUsuarioATV.TabIndex = 52
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(20, 81)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(85, 17)
        Me.Label24.TabIndex = 268
        Me.Label24.Text = "Usuario ATV:"
        Me.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPinCertificado
        '
        Me.txtPinCertificado.AcceptsReturn = True
        Me.txtPinCertificado.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCertificado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCertificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCertificado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPinCertificado.Location = New System.Drawing.Point(111, 52)
        Me.txtPinCertificado.MaxLength = 4
        Me.txtPinCertificado.Name = "txtPinCertificado"
        Me.txtPinCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCertificado.Size = New System.Drawing.Size(44, 20)
        Me.txtPinCertificado.TabIndex = 51
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(17, 55)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(88, 17)
        Me.Label18.TabIndex = 267
        Me.Label18.Text = "Pin certificado:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNombreCertificado
        '
        Me.txtNombreCertificado.AcceptsReturn = True
        Me.txtNombreCertificado.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreCertificado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreCertificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreCertificado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreCertificado.Location = New System.Drawing.Point(111, 26)
        Me.txtNombreCertificado.MaxLength = 200
        Me.txtNombreCertificado.Name = "txtNombreCertificado"
        Me.txtNombreCertificado.ReadOnly = True
        Me.txtNombreCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCertificado.Size = New System.Drawing.Size(260, 20)
        Me.txtNombreCertificado.TabIndex = 50
        Me.txtNombreCertificado.TabStop = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(35, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(73, 17)
        Me.Label16.TabIndex = 266
        Me.Label16.Text = "Certificado:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(898, 457)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.gpbSucursal)
        Me.Controls.Add(Me.txtFechaRenovacion)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtCodigoActividad)
        Me.Controls.Add(Me.lblCodigoActividad)
        Me.Controls.Add(Me.txtNombreComercial)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboBarrio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboDistrito)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCanton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboProvincia)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboTipoIdentificacion)
        Me.Controls.Add(Me.txtCorreoNotificacion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtTelefono)
        Me.Controls.Add(Me.txtDireccion)
        Me.Controls.Add(Me.txtNombreEmpresa)
        Me.Controls.Add(Me.txtIdentificacion)
        Me.Controls.Add(Me.txtIdEmpresa)
        Me.Controls.Add(Me.lblLabel6)
        Me.Controls.Add(Me.lblLabel5)
        Me.Controls.Add(Me.lblLabel4)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.lblLabel2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmEmpresa"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de datos de la empresa"
        Me.gpbSucursal.ResumeLayout(False)
        Me.gpbSucursal.PerformLayout()
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtCorreoNotificacion As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cboTipoIdentificacion As ComboBox
    Public WithEvents Label2 As Label
    Public WithEvents cboProvincia As ComboBox
    Public WithEvents Label4 As Label
    Public WithEvents Label5 As Label
    Public WithEvents cboCanton As ComboBox
    Public WithEvents Label6 As Label
    Public WithEvents cboDistrito As ComboBox
    Public WithEvents Label7 As Label
    Public WithEvents cboBarrio As ComboBox
    Public WithEvents txtNombreComercial As TextBox
    Public WithEvents Label8 As Label
    Public WithEvents txtCodigoActividad As TextBox
    Public WithEvents lblCodigoActividad As Label
    Public WithEvents txtFechaRenovacion As TextBox
    Public WithEvents Label9 As Label
    Friend WithEvents gpbSucursal As GroupBox
    Public WithEvents txtNombreSucursal As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtTelefonoSucursal As TextBox
    Public WithEvents txtDireccionSucursal As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents Label11 As Label
    Public WithEvents txtIdSucursal As TextBox
    Public WithEvents Label12 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Public WithEvents btnCargarCertificado As Button
    Public WithEvents txtClaveATV As TextBox
    Public WithEvents Label25 As Label
    Public WithEvents txtUsuarioATV As TextBox
    Public WithEvents Label24 As Label
    Public WithEvents txtPinCertificado As TextBox
    Public WithEvents Label18 As Label
    Public WithEvents txtNombreCertificado As TextBox
    Public WithEvents Label16 As Label
    Friend WithEvents ofdAbrirDocumento As OpenFileDialog
    Public WithEvents txtIdTerminal As TextBox
    Public WithEvents Label22 As Label
    Public WithEvents txtNombreImpresora As TextBox
    Public WithEvents Label21 As Label
    Public WithEvents txtUltimoMR As TextBox
    Public WithEvents Label20 As Label
    Public WithEvents txtUltimoTE As TextBox
    Public WithEvents Label19 As Label
    Public WithEvents txtUltimoNC As TextBox
    Public WithEvents Label17 As Label
    Public WithEvents txtUltimoND As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents txtUltimoFE As TextBox
    Public WithEvents Label13 As Label
End Class