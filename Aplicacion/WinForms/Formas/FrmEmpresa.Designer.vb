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
        Me.btnSiguiente = New System.Windows.Forms.Button()
        Me.btnAnterior = New System.Windows.Forms.Button()
        Me.txtIdTerminal = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNombreSucursal = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTelefonoSucursal = New System.Windows.Forms.TextBox()
        Me.txtDireccionSucursal = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtIdSucursal = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.TextBox1 = New System.Windows.Forms.TextBox()
        Me.TextBox2 = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
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
        Me.txtTelefono.Location = New System.Drawing.Point(122, 395)
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
        Me.txtDireccion.TabIndex = 8
        '
        'txtNombreEmpresa
        '
        Me.txtNombreEmpresa.AcceptsReturn = True
        Me.txtNombreEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreEmpresa.Location = New System.Drawing.Point(122, 343)
        Me.txtNombreEmpresa.MaxLength = 0
        Me.txtNombreEmpresa.Name = "txtNombreEmpresa"
        Me.txtNombreEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreEmpresa.Size = New System.Drawing.Size(313, 20)
        Me.txtNombreEmpresa.TabIndex = 9
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
        Me.txtIdentificacion.TabIndex = 2
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
        Me.lblLabel6.Location = New System.Drawing.Point(4, 398)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(112, 17)
        Me.lblLabel6.TabIndex = 0
        Me.lblLabel6.Text = "Teléfono:"
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
        Me.lblLabel5.Text = "Dirección:"
        Me.lblLabel5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(4, 346)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(112, 17)
        Me.lblLabel4.TabIndex = 0
        Me.lblLabel4.Text = "Nombre:"
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
        Me.lblLabel3.Text = "Identificación:"
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
        Me.txtCorreoNotificacion.Location = New System.Drawing.Point(122, 421)
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
        Me.Label1.Location = New System.Drawing.Point(7, 424)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(109, 17)
        Me.Label1.TabIndex = 51
        Me.Label1.Text = "Correo electrónico:"
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
        Me.cboTipoIdentificacion.TabIndex = 1
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
        Me.Label2.Text = "Tipo de identificación:"
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
        Me.cboProvincia.TabIndex = 4
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
        Me.Label5.Text = "Cantón:"
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
        Me.cboCanton.TabIndex = 5
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
        Me.cboDistrito.TabIndex = 6
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
        Me.cboBarrio.TabIndex = 7
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.AcceptsReturn = True
        Me.txtNombreComercial.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreComercial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreComercial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreComercial.Location = New System.Drawing.Point(122, 369)
        Me.txtNombreComercial.MaxLength = 0
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreComercial.Size = New System.Drawing.Size(313, 20)
        Me.txtNombreComercial.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(4, 372)
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
        Me.txtCodigoActividad.TabIndex = 3
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
        Me.lblCodigoActividad.Text = "Código Actividad:"
        Me.lblCodigoActividad.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFechaRenovacion
        '
        Me.txtFechaRenovacion.AcceptsReturn = True
        Me.txtFechaRenovacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtFechaRenovacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFechaRenovacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFechaRenovacion.Location = New System.Drawing.Point(122, 447)
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
        Me.Label9.Location = New System.Drawing.Point(4, 450)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(112, 17)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Fecha Renovación:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'gpbSucursal
        '
        Me.gpbSucursal.BackColor = System.Drawing.SystemColors.Control
        Me.gpbSucursal.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.gpbSucursal.Controls.Add(Me.btnSiguiente)
        Me.gpbSucursal.Controls.Add(Me.btnAnterior)
        Me.gpbSucursal.Controls.Add(Me.txtIdTerminal)
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
        Me.gpbSucursal.Location = New System.Drawing.Point(449, 197)
        Me.gpbSucursal.Name = "gpbSucursal"
        Me.gpbSucursal.Padding = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.gpbSucursal.Size = New System.Drawing.Size(422, 192)
        Me.gpbSucursal.TabIndex = 14
        Me.gpbSucursal.TabStop = False
        Me.gpbSucursal.Text = "Datos de la Sucursal"
        '
        'btnSiguiente
        '
        Me.btnSiguiente.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnSiguiente.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnSiguiente.Enabled = False
        Me.btnSiguiente.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnSiguiente.Location = New System.Drawing.Point(222, 157)
        Me.btnSiguiente.Name = "btnSiguiente"
        Me.btnSiguiente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnSiguiente.Size = New System.Drawing.Size(77, 21)
        Me.btnSiguiente.TabIndex = 22
        Me.btnSiguiente.TabStop = False
        Me.btnSiguiente.Text = "Siguiente >>"
        Me.btnSiguiente.UseVisualStyleBackColor = False
        '
        'btnAnterior
        '
        Me.btnAnterior.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAnterior.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAnterior.Enabled = False
        Me.btnAnterior.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAnterior.Location = New System.Drawing.Point(139, 157)
        Me.btnAnterior.Name = "btnAnterior"
        Me.btnAnterior.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAnterior.Size = New System.Drawing.Size(77, 21)
        Me.btnAnterior.TabIndex = 21
        Me.btnAnterior.TabStop = False
        Me.btnAnterior.Text = "<< Anterior"
        Me.btnAnterior.UseVisualStyleBackColor = False
        '
        'txtIdTerminal
        '
        Me.txtIdTerminal.AcceptsReturn = True
        Me.txtIdTerminal.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdTerminal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdTerminal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdTerminal.Location = New System.Drawing.Point(226, 25)
        Me.txtIdTerminal.MaxLength = 6
        Me.txtIdTerminal.Name = "txtIdTerminal"
        Me.txtIdTerminal.ReadOnly = True
        Me.txtIdTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdTerminal.Size = New System.Drawing.Size(47, 20)
        Me.txtIdTerminal.TabIndex = 16
        Me.txtIdTerminal.TabStop = False
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(163, 28)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(57, 17)
        Me.Label13.TabIndex = 86
        Me.Label13.Text = "Terminal:"
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
        Me.txtNombreSucursal.Size = New System.Drawing.Size(297, 20)
        Me.txtNombreSucursal.TabIndex = 17
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
        Me.txtTelefonoSucursal.TabIndex = 19
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
        Me.txtDireccionSucursal.Size = New System.Drawing.Size(297, 35)
        Me.txtDireccionSucursal.TabIndex = 18
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
        Me.Label10.Text = "Teléfono:"
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
        Me.Label11.Text = "Dirección:"
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
        Me.txtIdSucursal.TabIndex = 15
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
        'TextBox1
        '
        Me.TextBox1.AcceptsReturn = True
        Me.TextBox1.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox1.Location = New System.Drawing.Point(122, 63)
        Me.TextBox1.MaxLength = 80
        Me.TextBox1.Name = "TextBox1"
        Me.TextBox1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBox1.Size = New System.Drawing.Size(313, 20)
        Me.TextBox1.TabIndex = 236
        '
        'TextBox2
        '
        Me.TextBox2.AcceptsReturn = True
        Me.TextBox2.BackColor = System.Drawing.SystemColors.Window
        Me.TextBox2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TextBox2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.TextBox2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TextBox2.Location = New System.Drawing.Point(122, 89)
        Me.TextBox2.MaxLength = 80
        Me.TextBox2.Name = "TextBox2"
        Me.TextBox2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TextBox2.Size = New System.Drawing.Size(313, 20)
        Me.TextBox2.TabIndex = 237
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(8, 92)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(108, 17)
        Me.Label15.TabIndex = 239
        Me.Label15.Text = "Nombre comercial:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(5, 66)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(111, 17)
        Me._lblLabels_3.TabIndex = 238
        Me._lblLabels_3.Text = "Nombre empresa:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.GroupBox1.Location = New System.Drawing.Point(449, 40)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Padding = New System.Windows.Forms.Padding(6, 3, 6, 3)
        Me.GroupBox1.Size = New System.Drawing.Size(463, 137)
        Me.GroupBox1.TabIndex = 262
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Datos para la generación de Documentos Electrónicos"
        '
        'btnCargarCertificado
        '
        Me.btnCargarCertificado.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCargarCertificado.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCargarCertificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargarCertificado.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCargarCertificado.Location = New System.Drawing.Point(400, 26)
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
        Me.txtClaveATV.Location = New System.Drawing.Point(134, 104)
        Me.txtClaveATV.MaxLength = 100
        Me.txtClaveATV.Name = "txtClaveATV"
        Me.txtClaveATV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtClaveATV.Size = New System.Drawing.Size(313, 20)
        Me.txtClaveATV.TabIndex = 265
        '
        'Label25
        '
        Me.Label25.BackColor = System.Drawing.Color.Transparent
        Me.Label25.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label25.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label25.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label25.Location = New System.Drawing.Point(31, 107)
        Me.Label25.Name = "Label25"
        Me.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label25.Size = New System.Drawing.Size(97, 17)
        Me.Label25.TabIndex = 269
        Me.Label25.Text = "Contraseña ATV:"
        Me.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUsuarioATV
        '
        Me.txtUsuarioATV.AcceptsReturn = True
        Me.txtUsuarioATV.BackColor = System.Drawing.SystemColors.Window
        Me.txtUsuarioATV.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUsuarioATV.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUsuarioATV.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUsuarioATV.Location = New System.Drawing.Point(134, 78)
        Me.txtUsuarioATV.MaxLength = 100
        Me.txtUsuarioATV.Name = "txtUsuarioATV"
        Me.txtUsuarioATV.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUsuarioATV.Size = New System.Drawing.Size(313, 20)
        Me.txtUsuarioATV.TabIndex = 264
        '
        'Label24
        '
        Me.Label24.BackColor = System.Drawing.Color.Transparent
        Me.Label24.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label24.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label24.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label24.Location = New System.Drawing.Point(50, 81)
        Me.Label24.Name = "Label24"
        Me.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label24.Size = New System.Drawing.Size(78, 17)
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
        Me.txtPinCertificado.Location = New System.Drawing.Point(134, 52)
        Me.txtPinCertificado.MaxLength = 4
        Me.txtPinCertificado.Name = "txtPinCertificado"
        Me.txtPinCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCertificado.Size = New System.Drawing.Size(44, 20)
        Me.txtPinCertificado.TabIndex = 263
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(9, 55)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(119, 17)
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
        Me.txtNombreCertificado.Location = New System.Drawing.Point(134, 26)
        Me.txtNombreCertificado.MaxLength = 200
        Me.txtNombreCertificado.Name = "txtNombreCertificado"
        Me.txtNombreCertificado.ReadOnly = True
        Me.txtNombreCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCertificado.Size = New System.Drawing.Size(260, 20)
        Me.txtNombreCertificado.TabIndex = 262
        Me.txtNombreCertificado.TabStop = False
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(12, 27)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(119, 17)
        Me.Label16.TabIndex = 266
        Me.Label16.Text = "Certificado:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(923, 479)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.TextBox1)
        Me.Controls.Add(Me.TextBox2)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me._lblLabels_3)
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
    Public WithEvents txtIdTerminal As TextBox
    Public WithEvents Label13 As Label
    Public WithEvents txtNombreSucursal As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtTelefonoSucursal As TextBox
    Public WithEvents txtDireccionSucursal As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents Label11 As Label
    Public WithEvents txtIdSucursal As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents btnSiguiente As Button
    Public WithEvents btnAnterior As Button
    Public WithEvents TextBox1 As TextBox
    Public WithEvents TextBox2 As TextBox
    Public WithEvents Label15 As Label
    Public WithEvents _lblLabels_3 As Label
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
End Class