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
        Me.SuspendLayout()
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(162, 343)
        Me.txtTelefono.MaxLength = 8
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(85, 20)
        Me.txtTelefono.TabIndex = 12
        '
        'txtDireccion
        '
        Me.txtDireccion.AcceptsReturn = True
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccion.Location = New System.Drawing.Point(162, 250)
        Me.txtDireccion.MaxLength = 160
        Me.txtDireccion.Multiline = True
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccion.Size = New System.Drawing.Size(371, 35)
        Me.txtDireccion.TabIndex = 9
        '
        'txtNombreEmpresa
        '
        Me.txtNombreEmpresa.AcceptsReturn = True
        Me.txtNombreEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreEmpresa.Location = New System.Drawing.Point(162, 291)
        Me.txtNombreEmpresa.MaxLength = 0
        Me.txtNombreEmpresa.Name = "txtNombreEmpresa"
        Me.txtNombreEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreEmpresa.Size = New System.Drawing.Size(297, 20)
        Me.txtNombreEmpresa.TabIndex = 10
        '
        'txtIdentificacion
        '
        Me.txtIdentificacion.AcceptsReturn = True
        Me.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacion.Location = New System.Drawing.Point(162, 90)
        Me.txtIdentificacion.MaxLength = 12
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(217, 20)
        Me.txtIdentificacion.TabIndex = 2
        '
        'txtIdEmpresa
        '
        Me.txtIdEmpresa.AcceptsReturn = True
        Me.txtIdEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdEmpresa.Location = New System.Drawing.Point(162, 37)
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
        Me.txtCorreoNotificacion.Location = New System.Drawing.Point(162, 369)
        Me.txtCorreoNotificacion.MaxLength = 0
        Me.txtCorreoNotificacion.Name = "txtCorreoNotificacion"
        Me.txtCorreoNotificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorreoNotificacion.Size = New System.Drawing.Size(297, 20)
        Me.txtCorreoNotificacion.TabIndex = 13
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(-4, 372)
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
        Me.cboProvincia.TabIndex = 5
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
        Me.cboCanton.TabIndex = 6
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
        Me.cboDistrito.TabIndex = 7
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
        Me.cboBarrio.TabIndex = 8
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
        Me.txtNombreComercial.TabIndex = 11
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
        'txtCodigoActividad
        '
        Me.txtCodigoActividad.AcceptsReturn = True
        Me.txtCodigoActividad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoActividad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoActividad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoActividad.Location = New System.Drawing.Point(162, 116)
        Me.txtCodigoActividad.MaxLength = 6
        Me.txtCodigoActividad.Name = "txtCodigoActividad"
        Me.txtCodigoActividad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoActividad.Size = New System.Drawing.Size(61, 20)
        Me.txtCodigoActividad.TabIndex = 4
        '
        'lblCodigoActividad
        '
        Me.lblCodigoActividad.BackColor = System.Drawing.Color.Transparent
        Me.lblCodigoActividad.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblCodigoActividad.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblCodigoActividad.Location = New System.Drawing.Point(14, 119)
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
        Me.txtFechaRenovacion.Location = New System.Drawing.Point(162, 395)
        Me.txtFechaRenovacion.MaxLength = 10
        Me.txtFechaRenovacion.Name = "txtFechaRenovacion"
        Me.txtFechaRenovacion.ReadOnly = True
        Me.txtFechaRenovacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFechaRenovacion.Size = New System.Drawing.Size(85, 20)
        Me.txtFechaRenovacion.TabIndex = 14
        Me.txtFechaRenovacion.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(14, 398)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(142, 17)
        Me.Label9.TabIndex = 67
        Me.Label9.Text = "Fecha Renovación:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'FrmEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(544, 426)
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
End Class