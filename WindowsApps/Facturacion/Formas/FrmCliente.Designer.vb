<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCliente
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer
    Public WithEvents txtFax As System.Windows.Forms.TextBox
    Public WithEvents txtCelular As System.Windows.Forms.TextBox
    Public WithEvents txtTelefono As System.Windows.Forms.TextBox
    Public WithEvents txtDireccion As System.Windows.Forms.TextBox
    Public WithEvents txtNombre As System.Windows.Forms.TextBox
    Public WithEvents txtIdentificacion As System.Windows.Forms.TextBox
    Public WithEvents txtIdCliente As System.Windows.Forms.TextBox
    Public WithEvents lblLabel8 As System.Windows.Forms.Label
    Public WithEvents lblLabel7 As System.Windows.Forms.Label
    Public WithEvents lblLabel6 As System.Windows.Forms.Label
    Public WithEvents lblLabel5 As System.Windows.Forms.Label
    Public WithEvents lblLabel4 As System.Windows.Forms.Label
    Public WithEvents lblLabel3 As System.Windows.Forms.Label
    Public WithEvents lblLabel2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtFax = New System.Windows.Forms.TextBox()
        Me.txtCelular = New System.Windows.Forms.TextBox()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.txtIdentificacion = New System.Windows.Forms.TextBox()
        Me.txtIdCliente = New System.Windows.Forms.TextBox()
        Me.lblLabel8 = New System.Windows.Forms.Label()
        Me.lblLabel7 = New System.Windows.Forms.Label()
        Me.lblLabel6 = New System.Windows.Forms.Label()
        Me.lblLabel5 = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel2 = New System.Windows.Forms.Label()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtCorreoElectronico = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboTipoIdentificacion = New System.Windows.Forms.ComboBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtNombreComercial = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboIdTipoPrecio = New System.Windows.Forms.ComboBox()
        Me.txtFechaExoneracion = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboTipoExoneracion = New System.Windows.Forms.ComboBox()
        Me.txtPorcentajeExoneracion = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNumDocExoneracion = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkPermiteCredito = New System.Windows.Forms.CheckBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboInstExoneracion = New System.Windows.Forms.ComboBox()
        Me.txtArticulo = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtInciso = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label37 = New System.Windows.Forms.Label()
        Me.cboActividadEconomica = New System.Windows.Forms.ComboBox()
        Me.SuspendLayout()
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax.Location = New System.Drawing.Point(429, 210)
        Me.txtFax.MaxLength = 9
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(96, 20)
        Me.txtFax.TabIndex = 8
        '
        'txtCelular
        '
        Me.txtCelular.AcceptsReturn = True
        Me.txtCelular.BackColor = System.Drawing.SystemColors.Window
        Me.txtCelular.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCelular.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCelular.Location = New System.Drawing.Point(291, 210)
        Me.txtCelular.MaxLength = 9
        Me.txtCelular.Name = "txtCelular"
        Me.txtCelular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCelular.Size = New System.Drawing.Size(96, 20)
        Me.txtCelular.TabIndex = 7
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(139, 210)
        Me.txtTelefono.MaxLength = 9
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(96, 20)
        Me.txtTelefono.TabIndex = 6
        '
        'txtDireccion
        '
        Me.txtDireccion.AcceptsReturn = True
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccion.Location = New System.Drawing.Point(139, 117)
        Me.txtDireccion.MaxLength = 0
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccion.Size = New System.Drawing.Size(386, 35)
        Me.txtDireccion.TabIndex = 3
        '
        'txtNombre
        '
        Me.txtNombre.AcceptsReturn = True
        Me.txtNombre.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombre.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombre.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombre.Location = New System.Drawing.Point(139, 158)
        Me.txtNombre.MaxLength = 0
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombre.Size = New System.Drawing.Size(386, 20)
        Me.txtNombre.TabIndex = 4
        '
        'txtIdentificacion
        '
        Me.txtIdentificacion.AcceptsReturn = True
        Me.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacion.Location = New System.Drawing.Point(139, 91)
        Me.txtIdentificacion.MaxLength = 35
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(192, 20)
        Me.txtIdentificacion.TabIndex = 2
        '
        'txtIdCliente
        '
        Me.txtIdCliente.AcceptsReturn = True
        Me.txtIdCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCliente.Location = New System.Drawing.Point(139, 38)
        Me.txtIdCliente.MaxLength = 0
        Me.txtIdCliente.Name = "txtIdCliente"
        Me.txtIdCliente.ReadOnly = True
        Me.txtIdCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCliente.Size = New System.Drawing.Size(49, 20)
        Me.txtIdCliente.TabIndex = 0
        Me.txtIdCliente.TabStop = False
        '
        'lblLabel8
        '
        Me.lblLabel8.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel8.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel8.Location = New System.Drawing.Point(392, 213)
        Me.lblLabel8.Name = "lblLabel8"
        Me.lblLabel8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel8.Size = New System.Drawing.Size(31, 17)
        Me.lblLabel8.TabIndex = 0
        Me.lblLabel8.Text = "Fax:"
        Me.lblLabel8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel7
        '
        Me.lblLabel7.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel7.Location = New System.Drawing.Point(239, 213)
        Me.lblLabel7.Name = "lblLabel7"
        Me.lblLabel7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel7.Size = New System.Drawing.Size(46, 17)
        Me.lblLabel7.TabIndex = 0
        Me.lblLabel7.Text = "Celular:"
        Me.lblLabel7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(-9, 213)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel6.TabIndex = 0
        Me.lblLabel6.Text = "Teléfono:"
        Me.lblLabel6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel5
        '
        Me.lblLabel5.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel5.Location = New System.Drawing.Point(-9, 120)
        Me.lblLabel5.Name = "lblLabel5"
        Me.lblLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel5.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel5.TabIndex = 0
        Me.lblLabel5.Text = "Dirección:"
        Me.lblLabel5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(-9, 161)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel4.TabIndex = 0
        Me.lblLabel4.Text = "Nombre:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(-9, 94)
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
        Me.lblLabel2.Location = New System.Drawing.Point(-9, 41)
        Me.lblLabel2.Name = "lblLabel2"
        Me.lblLabel2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel2.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel2.TabIndex = 0
        Me.lblLabel2.Text = "Cliente No.:"
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
        'txtCorreoElectronico
        '
        Me.txtCorreoElectronico.AcceptsReturn = True
        Me.txtCorreoElectronico.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorreoElectronico.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCorreoElectronico.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCorreoElectronico.Location = New System.Drawing.Point(139, 236)
        Me.txtCorreoElectronico.MaxLength = 0
        Me.txtCorreoElectronico.Name = "txtCorreoElectronico"
        Me.txtCorreoElectronico.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorreoElectronico.Size = New System.Drawing.Size(297, 20)
        Me.txtCorreoElectronico.TabIndex = 9
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(21, 239)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(112, 17)
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
        Me.cboTipoIdentificacion.Location = New System.Drawing.Point(139, 64)
        Me.cboTipoIdentificacion.Name = "cboTipoIdentificacion"
        Me.cboTipoIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoIdentificacion.Size = New System.Drawing.Size(192, 21)
        Me.cboTipoIdentificacion.TabIndex = 1
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(-9, 68)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(142, 17)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Tipo identificación:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.AcceptsReturn = True
        Me.txtNombreComercial.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreComercial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreComercial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreComercial.Location = New System.Drawing.Point(139, 184)
        Me.txtNombreComercial.MaxLength = 0
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreComercial.Size = New System.Drawing.Size(386, 20)
        Me.txtNombreComercial.TabIndex = 5
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(-9, 187)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(142, 17)
        Me.Label8.TabIndex = 64
        Me.Label8.Text = "Nombre comercial:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(-9, 287)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(142, 17)
        Me.Label10.TabIndex = 69
        Me.Label10.Text = "Tipo de precio:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboIdTipoPrecio
        '
        Me.cboIdTipoPrecio.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboIdTipoPrecio.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboIdTipoPrecio.BackColor = System.Drawing.SystemColors.Window
        Me.cboIdTipoPrecio.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIdTipoPrecio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIdTipoPrecio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIdTipoPrecio.IntegralHeight = False
        Me.cboIdTipoPrecio.ItemHeight = 13
        Me.cboIdTipoPrecio.Location = New System.Drawing.Point(139, 284)
        Me.cboIdTipoPrecio.Name = "cboIdTipoPrecio"
        Me.cboIdTipoPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdTipoPrecio.Size = New System.Drawing.Size(247, 21)
        Me.cboIdTipoPrecio.TabIndex = 11
        '
        'txtFechaExoneracion
        '
        Me.txtFechaExoneracion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFechaExoneracion.Location = New System.Drawing.Point(139, 391)
        Me.txtFechaExoneracion.Name = "txtFechaExoneracion"
        Me.txtFechaExoneracion.Size = New System.Drawing.Size(84, 20)
        Me.txtFechaExoneracion.TabIndex = 17
        Me.txtFechaExoneracion.Value = New Date(2013, 6, 9, 0, 0, 0, 0)
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label16.Location = New System.Drawing.Point(12, 311)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(121, 19)
        Me.Label16.TabIndex = 156
        Me.Label16.Text = "Tipo exoneración:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTipoExoneracion
        '
        Me.cboTipoExoneracion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoExoneracion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoExoneracion.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoExoneracion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoExoneracion.IntegralHeight = False
        Me.cboTipoExoneracion.ItemHeight = 13
        Me.cboTipoExoneracion.Location = New System.Drawing.Point(139, 311)
        Me.cboTipoExoneracion.Name = "cboTipoExoneracion"
        Me.cboTipoExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoExoneracion.Size = New System.Drawing.Size(247, 21)
        Me.cboTipoExoneracion.TabIndex = 12
        '
        'txtPorcentajeExoneracion
        '
        Me.txtPorcentajeExoneracion.AcceptsReturn = True
        Me.txtPorcentajeExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeExoneracion.Location = New System.Drawing.Point(333, 391)
        Me.txtPorcentajeExoneracion.MaxLength = 2
        Me.txtPorcentajeExoneracion.Name = "txtPorcentajeExoneracion"
        Me.txtPorcentajeExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeExoneracion.Size = New System.Drawing.Size(33, 20)
        Me.txtPorcentajeExoneracion.TabIndex = 18
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(261, 391)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(66, 19)
        Me.Label14.TabIndex = 154
        Me.Label14.Text = "Porcentaje:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(63, 391)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(70, 19)
        Me.Label13.TabIndex = 153
        Me.Label13.Text = "Fecha:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNumDocExoneracion
        '
        Me.txtNumDocExoneracion.AcceptsReturn = True
        Me.txtNumDocExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumDocExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumDocExoneracion.Location = New System.Drawing.Point(139, 365)
        Me.txtNumDocExoneracion.MaxLength = 0
        Me.txtNumDocExoneracion.Name = "txtNumDocExoneracion"
        Me.txtNumDocExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumDocExoneracion.Size = New System.Drawing.Size(103, 20)
        Me.txtNumDocExoneracion.TabIndex = 14
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(30, 365)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(103, 19)
        Me._lblLabels_3.TabIndex = 151
        Me._lblLabels_3.Text = "Nro. documento"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(160, 262)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(144, 17)
        Me.Label15.TabIndex = 158
        Me.Label15.Text = "Permite facturar a crédito"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'chkPermiteCredito
        '
        Me.chkPermiteCredito.AutoSize = True
        Me.chkPermiteCredito.Enabled = False
        Me.chkPermiteCredito.Location = New System.Drawing.Point(139, 264)
        Me.chkPermiteCredito.Name = "chkPermiteCredito"
        Me.chkPermiteCredito.Size = New System.Drawing.Size(15, 14)
        Me.chkPermiteCredito.TabIndex = 10
        Me.chkPermiteCredito.UseVisualStyleBackColor = True
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label3.Location = New System.Drawing.Point(13, 338)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(121, 19)
        Me.Label3.TabIndex = 160
        Me.Label3.Text = "Institución exoneración:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboInstExoneracion
        '
        Me.cboInstExoneracion.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboInstExoneracion.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboInstExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.cboInstExoneracion.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboInstExoneracion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboInstExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboInstExoneracion.IntegralHeight = False
        Me.cboInstExoneracion.ItemHeight = 13
        Me.cboInstExoneracion.Location = New System.Drawing.Point(140, 338)
        Me.cboInstExoneracion.Name = "cboInstExoneracion"
        Me.cboInstExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboInstExoneracion.Size = New System.Drawing.Size(385, 21)
        Me.cboInstExoneracion.TabIndex = 13
        '
        'txtArticulo
        '
        Me.txtArticulo.AcceptsReturn = True
        Me.txtArticulo.BackColor = System.Drawing.SystemColors.Window
        Me.txtArticulo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtArticulo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtArticulo.Location = New System.Drawing.Point(333, 365)
        Me.txtArticulo.MaxLength = 0
        Me.txtArticulo.Name = "txtArticulo"
        Me.txtArticulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtArticulo.Size = New System.Drawing.Size(66, 20)
        Me.txtArticulo.TabIndex = 15
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(271, 365)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(56, 19)
        Me.Label4.TabIndex = 162
        Me.Label4.Text = "Articulo"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtInciso
        '
        Me.txtInciso.AcceptsReturn = True
        Me.txtInciso.BackColor = System.Drawing.SystemColors.Window
        Me.txtInciso.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtInciso.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtInciso.Location = New System.Drawing.Point(459, 365)
        Me.txtInciso.MaxLength = 0
        Me.txtInciso.Name = "txtInciso"
        Me.txtInciso.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtInciso.Size = New System.Drawing.Size(66, 20)
        Me.txtInciso.TabIndex = 16
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(401, 365)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(52, 19)
        Me.Label5.TabIndex = 164
        Me.Label5.Text = "Inciso"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label37
        '
        Me.Label37.BackColor = System.Drawing.Color.Transparent
        Me.Label37.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label37.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label37.Location = New System.Drawing.Point(10, 421)
        Me.Label37.Name = "Label37"
        Me.Label37.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label37.Size = New System.Drawing.Size(123, 17)
        Me.Label37.TabIndex = 278
        Me.Label37.Text = "Actividad Economica:"
        Me.Label37.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboActividadEconomica
        '
        Me.cboActividadEconomica.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboActividadEconomica.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboActividadEconomica.BackColor = System.Drawing.SystemColors.Window
        Me.cboActividadEconomica.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboActividadEconomica.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboActividadEconomica.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboActividadEconomica.IntegralHeight = False
        Me.cboActividadEconomica.ItemHeight = 13
        Me.cboActividadEconomica.Location = New System.Drawing.Point(139, 417)
        Me.cboActividadEconomica.Name = "cboActividadEconomica"
        Me.cboActividadEconomica.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboActividadEconomica.Size = New System.Drawing.Size(386, 21)
        Me.cboActividadEconomica.TabIndex = 277
        '
        'FrmCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(539, 450)
        Me.Controls.Add(Me.Label37)
        Me.Controls.Add(Me.cboActividadEconomica)
        Me.Controls.Add(Me.txtInciso)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtArticulo)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboInstExoneracion)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.chkPermiteCredito)
        Me.Controls.Add(Me.txtFechaExoneracion)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cboTipoExoneracion)
        Me.Controls.Add(Me.txtPorcentajeExoneracion)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtNumDocExoneracion)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboIdTipoPrecio)
        Me.Controls.Add(Me.txtNombreComercial)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.cboTipoIdentificacion)
        Me.Controls.Add(Me.txtCorreoElectronico)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtFax)
        Me.Controls.Add(Me.txtCelular)
        Me.Controls.Add(Me.txtTelefono)
        Me.Controls.Add(Me.txtDireccion)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.txtIdentificacion)
        Me.Controls.Add(Me.txtIdCliente)
        Me.Controls.Add(Me.lblLabel8)
        Me.Controls.Add(Me.lblLabel7)
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
        Me.Name = "FrmCliente"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtCorreoElectronico As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents cboTipoIdentificacion As ComboBox
    Public WithEvents Label2 As Label
    Public WithEvents txtNombreComercial As TextBox
    Public WithEvents Label8 As Label
    Public WithEvents Label10 As Label
    Public WithEvents cboIdTipoPrecio As ComboBox
    Friend WithEvents txtFechaExoneracion As DateTimePicker
    Public WithEvents Label16 As Label
    Public WithEvents cboTipoExoneracion As ComboBox
    Public WithEvents txtPorcentajeExoneracion As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents Label13 As Label
    Public WithEvents txtNumDocExoneracion As TextBox
    Public WithEvents _lblLabels_3 As Label
    Public WithEvents Label15 As Label
    Friend WithEvents chkPermiteCredito As CheckBox
    Public WithEvents Label3 As Label
    Public WithEvents cboInstExoneracion As ComboBox
    Public WithEvents txtArticulo As TextBox
    Public WithEvents Label4 As Label
    Public WithEvents txtInciso As TextBox
    Public WithEvents Label5 As Label
    Public WithEvents Label37 As Label
    Public WithEvents cboActividadEconomica As ComboBox
End Class