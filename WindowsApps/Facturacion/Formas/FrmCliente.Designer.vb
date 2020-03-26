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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboVendedor = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboIdTipoPrecio = New System.Windows.Forms.ComboBox()
        Me.chkExonerado = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboTipoImpuesto = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtFechaExoneracion = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboTipoExoneracion = New System.Windows.Forms.ComboBox()
        Me.txtPorcentajeExoneracion = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.txtNombreInstExoneracion = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtNumDocExoneracion = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.chkPermiteCredito = New System.Windows.Forms.CheckBox()
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
        Me.txtFax.TabIndex = 13
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
        Me.txtCelular.TabIndex = 12
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
        Me.txtTelefono.TabIndex = 11
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
        Me.txtDireccion.TabIndex = 8
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
        Me.txtNombre.TabIndex = 9
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
        Me.txtCorreoElectronico.TabIndex = 14
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
        Me.txtNombreComercial.TabIndex = 10
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
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(-9, 291)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(142, 17)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Vendedor:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'cboVendedor
        '
        Me.cboVendedor.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboVendedor.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboVendedor.BackColor = System.Drawing.SystemColors.Window
        Me.cboVendedor.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboVendedor.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboVendedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboVendedor.IntegralHeight = False
        Me.cboVendedor.ItemHeight = 13
        Me.cboVendedor.Location = New System.Drawing.Point(139, 288)
        Me.cboVendedor.Name = "cboVendedor"
        Me.cboVendedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboVendedor.Size = New System.Drawing.Size(247, 21)
        Me.cboVendedor.TabIndex = 15
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(-9, 318)
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
        Me.cboIdTipoPrecio.Location = New System.Drawing.Point(139, 315)
        Me.cboIdTipoPrecio.Name = "cboIdTipoPrecio"
        Me.cboIdTipoPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdTipoPrecio.Size = New System.Drawing.Size(247, 21)
        Me.cboIdTipoPrecio.TabIndex = 16
        '
        'chkExonerado
        '
        Me.chkExonerado.AutoSize = True
        Me.chkExonerado.Location = New System.Drawing.Point(139, 345)
        Me.chkExonerado.Name = "chkExonerado"
        Me.chkExonerado.Size = New System.Drawing.Size(15, 14)
        Me.chkExonerado.TabIndex = 70
        Me.chkExonerado.UseVisualStyleBackColor = True
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(160, 343)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(144, 17)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "Tarifa Diferenciada"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft
        '
        'cboTipoImpuesto
        '
        Me.cboTipoImpuesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoImpuesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoImpuesto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoImpuesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoImpuesto.Enabled = False
        Me.cboTipoImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoImpuesto.Items.AddRange(New Object() {"UND", "MT2", "MT3", "MT", "LT", "GL", "CTO", "CUB", "PAQ", "LAM", "VAR", "PZA"})
        Me.cboTipoImpuesto.Location = New System.Drawing.Point(139, 365)
        Me.cboTipoImpuesto.Name = "cboTipoImpuesto"
        Me.cboTipoImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoImpuesto.Size = New System.Drawing.Size(247, 21)
        Me.cboTipoImpuesto.TabIndex = 145
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(50, 366)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(83, 17)
        Me.Label12.TabIndex = 146
        Me.Label12.Text = "Tipo Impuesto:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFechaExoneracion
        '
        Me.txtFechaExoneracion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFechaExoneracion.Location = New System.Drawing.Point(139, 471)
        Me.txtFechaExoneracion.Name = "txtFechaExoneracion"
        Me.txtFechaExoneracion.Size = New System.Drawing.Size(84, 20)
        Me.txtFechaExoneracion.TabIndex = 149
        Me.txtFechaExoneracion.Value = New Date(2013, 6, 9, 0, 0, 0, 0)
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label16.Location = New System.Drawing.Point(12, 392)
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
        Me.cboTipoExoneracion.Location = New System.Drawing.Point(139, 392)
        Me.cboTipoExoneracion.Name = "cboTipoExoneracion"
        Me.cboTipoExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoExoneracion.Size = New System.Drawing.Size(247, 21)
        Me.cboTipoExoneracion.TabIndex = 155
        '
        'txtPorcentajeExoneracion
        '
        Me.txtPorcentajeExoneracion.AcceptsReturn = True
        Me.txtPorcentajeExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeExoneracion.Location = New System.Drawing.Point(360, 471)
        Me.txtPorcentajeExoneracion.MaxLength = 0
        Me.txtPorcentajeExoneracion.Name = "txtPorcentajeExoneracion"
        Me.txtPorcentajeExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeExoneracion.Size = New System.Drawing.Size(26, 20)
        Me.txtPorcentajeExoneracion.TabIndex = 150
        Me.txtPorcentajeExoneracion.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(288, 471)
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
        Me.Label13.Location = New System.Drawing.Point(63, 471)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(70, 19)
        Me.Label13.TabIndex = 153
        Me.Label13.Text = "Fecha:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNombreInstExoneracion
        '
        Me.txtNombreInstExoneracion.AcceptsReturn = True
        Me.txtNombreInstExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreInstExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreInstExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreInstExoneracion.Location = New System.Drawing.Point(139, 445)
        Me.txtNombreInstExoneracion.MaxLength = 0
        Me.txtNombreInstExoneracion.Name = "txtNombreInstExoneracion"
        Me.txtNombreInstExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreInstExoneracion.Size = New System.Drawing.Size(386, 20)
        Me.txtNombreInstExoneracion.TabIndex = 148
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(75, 445)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(58, 19)
        Me.Label3.TabIndex = 152
        Me.Label3.Text = "Nombre institución:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNumDocExoneracion
        '
        Me.txtNumDocExoneracion.AcceptsReturn = True
        Me.txtNumDocExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumDocExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumDocExoneracion.Location = New System.Drawing.Point(139, 419)
        Me.txtNumDocExoneracion.MaxLength = 0
        Me.txtNumDocExoneracion.Name = "txtNumDocExoneracion"
        Me.txtNumDocExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumDocExoneracion.Size = New System.Drawing.Size(103, 20)
        Me.txtNumDocExoneracion.TabIndex = 147
        Me.txtNumDocExoneracion.TabStop = False
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(30, 419)
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
        Me.chkPermiteCredito.TabIndex = 157
        Me.chkPermiteCredito.UseVisualStyleBackColor = True
        '
        'FrmCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(539, 507)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.chkPermiteCredito)
        Me.Controls.Add(Me.txtFechaExoneracion)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cboTipoExoneracion)
        Me.Controls.Add(Me.txtPorcentajeExoneracion)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtNombreInstExoneracion)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtNumDocExoneracion)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me.cboTipoImpuesto)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.chkExonerado)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.cboIdTipoPrecio)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.cboVendedor)
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
    Public WithEvents Label9 As Label
    Public WithEvents cboVendedor As ComboBox
    Public WithEvents Label10 As Label
    Public WithEvents cboIdTipoPrecio As ComboBox
    Friend WithEvents chkExonerado As CheckBox
    Public WithEvents Label11 As Label
    Public WithEvents cboTipoImpuesto As ComboBox
    Public WithEvents Label12 As Label
    Friend WithEvents txtFechaExoneracion As DateTimePicker
    Public WithEvents Label16 As Label
    Public WithEvents cboTipoExoneracion As ComboBox
    Public WithEvents txtPorcentajeExoneracion As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents Label13 As Label
    Public WithEvents txtNombreInstExoneracion As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtNumDocExoneracion As TextBox
    Public WithEvents _lblLabels_3 As Label
    Public WithEvents Label15 As Label
    Friend WithEvents chkPermiteCredito As CheckBox
End Class