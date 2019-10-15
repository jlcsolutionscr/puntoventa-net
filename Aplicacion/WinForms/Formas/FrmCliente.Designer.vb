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
        Me.txtIdentificacionExtranjero = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
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
        Me.Label9 = New System.Windows.Forms.Label()
        Me.cboVendedor = New System.Windows.Forms.ComboBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.cboIdTipoPrecio = New System.Windows.Forms.ComboBox()
        Me.chkExonerado = New System.Windows.Forms.CheckBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.cboTipoImpuesto = New System.Windows.Forms.ComboBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'txtFax
        '
        Me.txtFax.AcceptsReturn = True
        Me.txtFax.BackColor = System.Drawing.SystemColors.Window
        Me.txtFax.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFax.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFax.Location = New System.Drawing.Point(162, 395)
        Me.txtFax.MaxLength = 9
        Me.txtFax.Name = "txtFax"
        Me.txtFax.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFax.Size = New System.Drawing.Size(132, 20)
        Me.txtFax.TabIndex = 13
        '
        'txtCelular
        '
        Me.txtCelular.AcceptsReturn = True
        Me.txtCelular.BackColor = System.Drawing.SystemColors.Window
        Me.txtCelular.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCelular.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCelular.Location = New System.Drawing.Point(162, 369)
        Me.txtCelular.MaxLength = 9
        Me.txtCelular.Name = "txtCelular"
        Me.txtCelular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCelular.Size = New System.Drawing.Size(132, 20)
        Me.txtCelular.TabIndex = 12
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(162, 343)
        Me.txtTelefono.MaxLength = 9
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(132, 20)
        Me.txtTelefono.TabIndex = 11
        '
        'txtDireccion
        '
        Me.txtDireccion.AcceptsReturn = True
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccion.Location = New System.Drawing.Point(162, 250)
        Me.txtDireccion.MaxLength = 0
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccion.Size = New System.Drawing.Size(459, 35)
        Me.txtDireccion.TabIndex = 8
        '
        'txtNombre
        '
        Me.txtNombre.AcceptsReturn = True
        Me.txtNombre.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombre.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombre.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombre.Location = New System.Drawing.Point(162, 291)
        Me.txtNombre.MaxLength = 0
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombre.Size = New System.Drawing.Size(297, 20)
        Me.txtNombre.TabIndex = 9
        '
        'txtIdentificacion
        '
        Me.txtIdentificacion.AcceptsReturn = True
        Me.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacion.Location = New System.Drawing.Point(162, 90)
        Me.txtIdentificacion.MaxLength = 35
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(217, 20)
        Me.txtIdentificacion.TabIndex = 2
        '
        'txtIdCliente
        '
        Me.txtIdCliente.AcceptsReturn = True
        Me.txtIdCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCliente.Enabled = False
        Me.txtIdCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCliente.Location = New System.Drawing.Point(162, 37)
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
        Me.lblLabel8.Location = New System.Drawing.Point(14, 398)
        Me.lblLabel8.Name = "lblLabel8"
        Me.lblLabel8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel8.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel8.TabIndex = 0
        Me.lblLabel8.Text = "Fax:"
        Me.lblLabel8.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel7
        '
        Me.lblLabel7.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel7.Location = New System.Drawing.Point(14, 372)
        Me.lblLabel7.Name = "lblLabel7"
        Me.lblLabel7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel7.Size = New System.Drawing.Size(142, 17)
        Me.lblLabel7.TabIndex = 0
        Me.lblLabel7.Text = "Celular:"
        Me.lblLabel7.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(14, 346)
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
        Me.lblLabel5.Location = New System.Drawing.Point(14, 253)
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
        Me.lblLabel4.Location = New System.Drawing.Point(14, 294)
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
        Me.lblLabel3.Location = New System.Drawing.Point(14, 93)
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
        Me.lblLabel2.Location = New System.Drawing.Point(14, 40)
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
        Me.txtCorreoElectronico.Location = New System.Drawing.Point(162, 421)
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
        Me.Label1.Location = New System.Drawing.Point(-4, 424)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(160, 17)
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
        Me.cboTipoIdentificacion.Location = New System.Drawing.Point(162, 63)
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
        Me.Label2.Location = New System.Drawing.Point(14, 67)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(142, 17)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Tipo de identificación:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtIdentificacionExtranjero
        '
        Me.txtIdentificacionExtranjero.AcceptsReturn = True
        Me.txtIdentificacionExtranjero.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacionExtranjero.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacionExtranjero.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacionExtranjero.Location = New System.Drawing.Point(162, 116)
        Me.txtIdentificacionExtranjero.MaxLength = 35
        Me.txtIdentificacionExtranjero.Name = "txtIdentificacionExtranjero"
        Me.txtIdentificacionExtranjero.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacionExtranjero.Size = New System.Drawing.Size(217, 20)
        Me.txtIdentificacionExtranjero.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(14, 119)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(142, 17)
        Me.Label3.TabIndex = 54
        Me.Label3.Text = "Identificación extranjero:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.cboProvincia.Location = New System.Drawing.Point(162, 142)
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
        Me.Label4.Location = New System.Drawing.Point(14, 145)
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
        Me.Label5.Location = New System.Drawing.Point(14, 172)
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
        Me.cboCanton.Location = New System.Drawing.Point(162, 169)
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
        Me.Label6.Location = New System.Drawing.Point(14, 199)
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
        Me.cboDistrito.Location = New System.Drawing.Point(162, 196)
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
        Me.Label7.Location = New System.Drawing.Point(14, 226)
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
        Me.cboBarrio.Location = New System.Drawing.Point(162, 223)
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
        Me.txtNombreComercial.Location = New System.Drawing.Point(162, 317)
        Me.txtNombreComercial.MaxLength = 0
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreComercial.Size = New System.Drawing.Size(297, 20)
        Me.txtNombreComercial.TabIndex = 10
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(14, 320)
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
        Me.Label9.Location = New System.Drawing.Point(14, 450)
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
        Me.cboVendedor.Location = New System.Drawing.Point(162, 447)
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
        Me.Label10.Location = New System.Drawing.Point(14, 477)
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
        Me.cboIdTipoPrecio.Location = New System.Drawing.Point(162, 474)
        Me.cboIdTipoPrecio.Name = "cboIdTipoPrecio"
        Me.cboIdTipoPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdTipoPrecio.Size = New System.Drawing.Size(192, 21)
        Me.cboIdTipoPrecio.TabIndex = 16
        '
        'chkExonerado
        '
        Me.chkExonerado.AutoSize = True
        Me.chkExonerado.Location = New System.Drawing.Point(162, 504)
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
        Me.Label11.Location = New System.Drawing.Point(12, 503)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(144, 17)
        Me.Label11.TabIndex = 71
        Me.Label11.Text = "Aplica Tarifa Diferenciada:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.cboTipoImpuesto.Location = New System.Drawing.Point(162, 524)
        Me.cboTipoImpuesto.Name = "cboTipoImpuesto"
        Me.cboTipoImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoImpuesto.Size = New System.Drawing.Size(319, 21)
        Me.cboTipoImpuesto.TabIndex = 145
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(73, 525)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(83, 17)
        Me.Label12.TabIndex = 146
        Me.Label12.Text = "Tipo Impuesto:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmCliente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(632, 565)
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
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboBarrio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.cboDistrito)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCanton)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboProvincia)
        Me.Controls.Add(Me.txtIdentificacionExtranjero)
        Me.Controls.Add(Me.Label3)
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
    Public WithEvents txtIdentificacionExtranjero As TextBox
    Public WithEvents Label3 As Label
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
    Public WithEvents Label9 As Label
    Public WithEvents cboVendedor As ComboBox
    Public WithEvents Label10 As Label
    Public WithEvents cboIdTipoPrecio As ComboBox
    Friend WithEvents chkExonerado As CheckBox
    Public WithEvents Label11 As Label
    Public WithEvents cboTipoImpuesto As ComboBox
    Public WithEvents Label12 As Label
End Class