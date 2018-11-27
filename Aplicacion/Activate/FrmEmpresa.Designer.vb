<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> Partial Class FrmEmpresa
#Region "Código generado por el Diseñador de Windows Forms "
	<System.Diagnostics.DebuggerNonUserCode()> Public Sub New()
		MyBase.New()
		'Llamada necesaria para el Diseñador de Windows Forms.
		InitializeComponent()
	End Sub
	'Form reemplaza a Dispose para limpiar la lista de componentes.
	<System.Diagnostics.DebuggerNonUserCode()> Protected Overloads Overrides Sub Dispose(ByVal Disposing As Boolean)
		If Disposing Then
			If Not components Is Nothing Then
				components.Dispose()
			End If
		End If
		MyBase.Dispose(Disposing)
	End Sub
	'Requerido por el Diseñador de Windows Forms
	Private components As System.ComponentModel.IContainer
    Public WithEvents txtEquipo As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents txtRegistro As System.Windows.Forms.TextBox
    Public WithEvents cmdUpdate As System.Windows.Forms.Button
    Public WithEvents cmdCancel As System.Windows.Forms.Button
    Public WithEvents txtIdEmpresa As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar mediante el Diseñador de Windows Forms.
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmEmpresa))
        Me.cmdUpdate = New System.Windows.Forms.Button()
        Me.cmdCancel = New System.Windows.Forms.Button()
        Me.txtEquipo = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.txtIdEmpresa = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.CmdConsultar = New System.Windows.Forms.Button()
        Me.dgvEquipos = New System.Windows.Forms.DataGridView()
        Me.btnEliminarDetalle = New System.Windows.Forms.Button()
        Me.btnInsertarDetalle = New System.Windows.Forms.Button()
        Me.txtNombreComercial = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtIdentificacion = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtImpresoraFactura = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPorcentajeIVA = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtLineasFactura = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.ckbContabiliza = New System.Windows.Forms.CheckBox()
        Me.ckbAutoCompleta = New System.Windows.Forms.CheckBox()
        Me.chkModificaDesc = New System.Windows.Forms.CheckBox()
        Me.chkDesgloseInst = New System.Windows.Forms.CheckBox()
        Me.txtPorcentajeInstalacion = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCodigoServInst = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkUsaImpresoraImpacto = New System.Windows.Forms.CheckBox()
        Me.chkIncluyeInsumosEnFactura = New System.Windows.Forms.CheckBox()
        Me.chkRespaldoEnLinea = New System.Windows.Forms.CheckBox()
        Me.chkCierrePorTurnos = New System.Windows.Forms.CheckBox()
        Me.txtNombreEmpresa = New System.Windows.Forms.TextBox()
        Me.txtCorreoElectronico = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkFacturaElectronica = New System.Windows.Forms.CheckBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.cboTipoIdentificacion = New System.Windows.Forms.ComboBox()
        Me.cboProvincia = New System.Windows.Forms.ComboBox()
        Me.cboCanton = New System.Windows.Forms.ComboBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.cboDistrito = New System.Windows.Forms.ComboBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.cboBarrio = New System.Windows.Forms.ComboBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtServicioFacturaElectronica = New System.Windows.Forms.TextBox()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.txtIdCertificado = New System.Windows.Forms.TextBox()
        Me.Label17 = New System.Windows.Forms.Label()
        Me.txtPinCertificado = New System.Windows.Forms.TextBox()
        Me.Label18 = New System.Windows.Forms.Label()
        Me.txtUltimoDocFE = New System.Windows.Forms.TextBox()
        Me.Label19 = New System.Windows.Forms.Label()
        Me.txtUltimoDocND = New System.Windows.Forms.TextBox()
        Me.txtUltimoDocNC = New System.Windows.Forms.TextBox()
        Me.txtUltimoDocTE = New System.Windows.Forms.TextBox()
        Me.txtUltimoDocMR = New System.Windows.Forms.TextBox()
        Me.Label20 = New System.Windows.Forms.Label()
        Me.Label21 = New System.Windows.Forms.Label()
        Me.Label22 = New System.Windows.Forms.Label()
        Me.Label23 = New System.Windows.Forms.Label()
        Me.picLogo = New System.Windows.Forms.PictureBox()
        Me.btnCargarLogo = New System.Windows.Forms.Button()
        Me.ofdAbrirDocumento = New System.Windows.Forms.OpenFileDialog()
        CType(Me.dgvEquipos, System.ComponentModel.ISupportInitialize).BeginInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdUpdate
        '
        Me.cmdUpdate.BackColor = System.Drawing.SystemColors.Control
        Me.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdUpdate.Image = CType(resources.GetObject("cmdUpdate.Image"), System.Drawing.Image)
        Me.cmdUpdate.Location = New System.Drawing.Point(8, 8)
        Me.cmdUpdate.Name = "cmdUpdate"
        Me.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdUpdate.Size = New System.Drawing.Size(25, 25)
        Me.cmdUpdate.TabIndex = 99
        Me.cmdUpdate.TabStop = False
        Me.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdUpdate.UseVisualStyleBackColor = False
        '
        'cmdCancel
        '
        Me.cmdCancel.BackColor = System.Drawing.SystemColors.Control
        Me.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdCancel.Image = CType(resources.GetObject("cmdCancel.Image"), System.Drawing.Image)
        Me.cmdCancel.Location = New System.Drawing.Point(48, 8)
        Me.cmdCancel.Name = "cmdCancel"
        Me.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdCancel.Size = New System.Drawing.Size(25, 25)
        Me.cmdCancel.TabIndex = 98
        Me.cmdCancel.TabStop = False
        Me.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter
        Me.cmdCancel.UseVisualStyleBackColor = False
        '
        'txtEquipo
        '
        Me.txtEquipo.AcceptsReturn = True
        Me.txtEquipo.BackColor = System.Drawing.SystemColors.Window
        Me.txtEquipo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEquipo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtEquipo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEquipo.Location = New System.Drawing.Point(476, 79)
        Me.txtEquipo.MaxLength = 0
        Me.txtEquipo.Name = "txtEquipo"
        Me.txtEquipo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEquipo.Size = New System.Drawing.Size(131, 20)
        Me.txtEquipo.TabIndex = 25
        Me.txtEquipo.TabStop = False
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(360, 471)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(85, 20)
        Me.txtFecha.TabIndex = 24
        Me.txtFecha.TabStop = False
        '
        'txtIdEmpresa
        '
        Me.txtIdEmpresa.AcceptsReturn = True
        Me.txtIdEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdEmpresa.Location = New System.Drawing.Point(132, 49)
        Me.txtIdEmpresa.MaxLength = 0
        Me.txtIdEmpresa.Name = "txtIdEmpresa"
        Me.txtIdEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdEmpresa.Size = New System.Drawing.Size(49, 20)
        Me.txtIdEmpresa.TabIndex = 0
        Me.txtIdEmpresa.TabStop = False
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(7, 78)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(119, 17)
        Me._lblLabels_3.TabIndex = 19
        Me._lblLabels_3.Text = "Nombre empresa:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(188, 474)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(166, 17)
        Me._lblLabels_1.TabIndex = 18
        Me._lblLabels_1.Text = "Fecha:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(476, 59)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(151, 17)
        Me._lblLabels_2.TabIndex = 16
        Me._lblLabels_2.Text = "Equipo"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(7, 52)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(119, 17)
        Me._lblLabels_0.TabIndex = 4
        Me._lblLabels_0.Text = "No. Equipo:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'CmdConsultar
        '
        Me.CmdConsultar.BackColor = System.Drawing.SystemColors.Control
        Me.CmdConsultar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdConsultar.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.CmdConsultar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdConsultar.Location = New System.Drawing.Point(607, 79)
        Me.CmdConsultar.Name = "CmdConsultar"
        Me.CmdConsultar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdConsultar.Size = New System.Drawing.Size(20, 20)
        Me.CmdConsultar.TabIndex = 26
        Me.CmdConsultar.TabStop = False
        Me.CmdConsultar.UseVisualStyleBackColor = False
        '
        'dgvEquipos
        '
        Me.dgvEquipos.AllowUserToAddRows = False
        Me.dgvEquipos.AllowUserToDeleteRows = False
        Me.dgvEquipos.AllowUserToResizeColumns = False
        Me.dgvEquipos.AllowUserToResizeRows = False
        Me.dgvEquipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvEquipos.Location = New System.Drawing.Point(476, 132)
        Me.dgvEquipos.Name = "dgvEquipos"
        Me.dgvEquipos.RowHeadersVisible = False
        Me.dgvEquipos.Size = New System.Drawing.Size(349, 102)
        Me.dgvEquipos.TabIndex = 29
        Me.dgvEquipos.TabStop = False
        '
        'btnEliminarDetalle
        '
        Me.btnEliminarDetalle.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarDetalle.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarDetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnEliminarDetalle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarDetalle.Location = New System.Drawing.Point(541, 240)
        Me.btnEliminarDetalle.Name = "btnEliminarDetalle"
        Me.btnEliminarDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarDetalle.Size = New System.Drawing.Size(65, 26)
        Me.btnEliminarDetalle.TabIndex = 31
        Me.btnEliminarDetalle.TabStop = False
        Me.btnEliminarDetalle.Text = "&Eliminar"
        Me.btnEliminarDetalle.UseVisualStyleBackColor = False
        '
        'btnInsertarDetalle
        '
        Me.btnInsertarDetalle.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarDetalle.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarDetalle.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnInsertarDetalle.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarDetalle.Location = New System.Drawing.Point(475, 240)
        Me.btnInsertarDetalle.Name = "btnInsertarDetalle"
        Me.btnInsertarDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarDetalle.Size = New System.Drawing.Size(65, 26)
        Me.btnInsertarDetalle.TabIndex = 30
        Me.btnInsertarDetalle.TabStop = False
        Me.btnInsertarDetalle.Text = "&Insertar"
        Me.btnInsertarDetalle.UseVisualStyleBackColor = False
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.AcceptsReturn = True
        Me.txtNombreComercial.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreComercial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreComercial.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreComercial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreComercial.Location = New System.Drawing.Point(132, 101)
        Me.txtNombreComercial.MaxLength = 50
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreComercial.Size = New System.Drawing.Size(313, 20)
        Me.txtNombreComercial.TabIndex = 2
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(10, 104)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(116, 17)
        Me.Label1.TabIndex = 40
        Me.Label1.Text = "Nombre comercial:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIdentificacion
        '
        Me.txtIdentificacion.AcceptsReturn = True
        Me.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdentificacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdentificacion.Location = New System.Drawing.Point(132, 154)
        Me.txtIdentificacion.MaxLength = 50
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(146, 20)
        Me.txtIdentificacion.TabIndex = 4
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(8, 157)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(119, 17)
        Me.Label2.TabIndex = 42
        Me.Label2.Text = "Identificación:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(7, 183)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(119, 17)
        Me.Label3.TabIndex = 44
        Me.Label3.Text = "Provincia:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDireccion
        '
        Me.txtDireccion.AcceptsReturn = True
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccion.Location = New System.Drawing.Point(132, 289)
        Me.txtDireccion.MaxLength = 50
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccion.Size = New System.Drawing.Size(313, 20)
        Me.txtDireccion.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(7, 292)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(119, 17)
        Me.Label4.TabIndex = 46
        Me.Label4.Text = "Otras señas:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(132, 315)
        Me.txtTelefono.MaxLength = 50
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(125, 20)
        Me.txtTelefono.TabIndex = 10
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(7, 318)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(119, 17)
        Me.Label5.TabIndex = 48
        Me.Label5.Text = "Teléfono:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtImpresoraFactura
        '
        Me.txtImpresoraFactura.AcceptsReturn = True
        Me.txtImpresoraFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpresoraFactura.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpresoraFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtImpresoraFactura.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpresoraFactura.Location = New System.Drawing.Point(628, 79)
        Me.txtImpresoraFactura.MaxLength = 50
        Me.txtImpresoraFactura.Name = "txtImpresoraFactura"
        Me.txtImpresoraFactura.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpresoraFactura.Size = New System.Drawing.Size(197, 20)
        Me.txtImpresoraFactura.TabIndex = 27
        Me.txtImpresoraFactura.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(628, 59)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(199, 17)
        Me.Label6.TabIndex = 50
        Me.Label6.Text = "Impresora Fact:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtPorcentajeIVA
        '
        Me.txtPorcentajeIVA.AcceptsReturn = True
        Me.txtPorcentajeIVA.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeIVA.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeIVA.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPorcentajeIVA.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeIVA.Location = New System.Drawing.Point(132, 445)
        Me.txtPorcentajeIVA.MaxLength = 50
        Me.txtPorcentajeIVA.Name = "txtPorcentajeIVA"
        Me.txtPorcentajeIVA.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeIVA.Size = New System.Drawing.Size(44, 20)
        Me.txtPorcentajeIVA.TabIndex = 20
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(7, 448)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(119, 17)
        Me.Label7.TabIndex = 52
        Me.Label7.Text = "% IVA:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtLineasFactura
        '
        Me.txtLineasFactura.AcceptsReturn = True
        Me.txtLineasFactura.BackColor = System.Drawing.SystemColors.Window
        Me.txtLineasFactura.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtLineasFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtLineasFactura.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtLineasFactura.Location = New System.Drawing.Point(132, 471)
        Me.txtLineasFactura.MaxLength = 50
        Me.txtLineasFactura.Name = "txtLineasFactura"
        Me.txtLineasFactura.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtLineasFactura.Size = New System.Drawing.Size(42, 20)
        Me.txtLineasFactura.TabIndex = 23
        Me.txtLineasFactura.TabStop = False
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(7, 474)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(120, 17)
        Me.Label8.TabIndex = 54
        Me.Label8.Text = "Líneas por Fact:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'ckbContabiliza
        '
        Me.ckbContabiliza.AutoSize = True
        Me.ckbContabiliza.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbContabiliza.Location = New System.Drawing.Point(476, 477)
        Me.ckbContabiliza.Name = "ckbContabiliza"
        Me.ckbContabiliza.Size = New System.Drawing.Size(77, 17)
        Me.ckbContabiliza.TabIndex = 33
        Me.ckbContabiliza.TabStop = False
        Me.ckbContabiliza.Text = "Contabiliza"
        Me.ckbContabiliza.UseVisualStyleBackColor = True
        '
        'ckbAutoCompleta
        '
        Me.ckbAutoCompleta.AutoSize = True
        Me.ckbAutoCompleta.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.ckbAutoCompleta.Location = New System.Drawing.Point(476, 503)
        Me.ckbAutoCompleta.Name = "ckbAutoCompleta"
        Me.ckbAutoCompleta.Size = New System.Drawing.Size(189, 17)
        Me.ckbAutoCompleta.TabIndex = 35
        Me.ckbAutoCompleta.TabStop = False
        Me.ckbAutoCompleta.Text = "Auto Completar Lista de Productos"
        Me.ckbAutoCompleta.UseVisualStyleBackColor = True
        '
        'chkModificaDesc
        '
        Me.chkModificaDesc.AutoSize = True
        Me.chkModificaDesc.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkModificaDesc.Location = New System.Drawing.Point(476, 529)
        Me.chkModificaDesc.Name = "chkModificaDesc"
        Me.chkModificaDesc.Size = New System.Drawing.Size(196, 17)
        Me.chkModificaDesc.TabIndex = 37
        Me.chkModificaDesc.TabStop = False
        Me.chkModificaDesc.Text = "Modifica la descripción del producto"
        Me.chkModificaDesc.UseVisualStyleBackColor = True
        '
        'chkDesgloseInst
        '
        Me.chkDesgloseInst.AutoSize = True
        Me.chkDesgloseInst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkDesgloseInst.Location = New System.Drawing.Point(476, 555)
        Me.chkDesgloseInst.Name = "chkDesgloseInst"
        Me.chkDesgloseInst.Size = New System.Drawing.Size(177, 17)
        Me.chkDesgloseInst.TabIndex = 39
        Me.chkDesgloseInst.TabStop = False
        Me.chkDesgloseInst.Text = "Desglosa servicio de instalación"
        Me.chkDesgloseInst.UseVisualStyleBackColor = True
        '
        'txtPorcentajeInstalacion
        '
        Me.txtPorcentajeInstalacion.AcceptsReturn = True
        Me.txtPorcentajeInstalacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeInstalacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeInstalacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPorcentajeInstalacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeInstalacion.Location = New System.Drawing.Point(237, 445)
        Me.txtPorcentajeInstalacion.MaxLength = 50
        Me.txtPorcentajeInstalacion.Name = "txtPorcentajeInstalacion"
        Me.txtPorcentajeInstalacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeInstalacion.Size = New System.Drawing.Size(44, 20)
        Me.txtPorcentajeInstalacion.TabIndex = 21
        Me.txtPorcentajeInstalacion.TabStop = False
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(181, 448)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(50, 17)
        Me.Label9.TabIndex = 60
        Me.Label9.Text = "% Inst:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigoServInst
        '
        Me.txtCodigoServInst.AcceptsReturn = True
        Me.txtCodigoServInst.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoServInst.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoServInst.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCodigoServInst.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoServInst.Location = New System.Drawing.Point(401, 445)
        Me.txtCodigoServInst.MaxLength = 50
        Me.txtCodigoServInst.Name = "txtCodigoServInst"
        Me.txtCodigoServInst.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoServInst.Size = New System.Drawing.Size(44, 20)
        Me.txtCodigoServInst.TabIndex = 22
        Me.txtCodigoServInst.TabStop = False
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(297, 447)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(98, 17)
        Me.Label10.TabIndex = 62
        Me.Label10.Text = "Cod Serv. Inst.:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkUsaImpresoraImpacto
        '
        Me.chkUsaImpresoraImpacto.AutoSize = True
        Me.chkUsaImpresoraImpacto.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkUsaImpresoraImpacto.Location = New System.Drawing.Point(475, 105)
        Me.chkUsaImpresoraImpacto.Name = "chkUsaImpresoraImpacto"
        Me.chkUsaImpresoraImpacto.Size = New System.Drawing.Size(144, 17)
        Me.chkUsaImpresoraImpacto.TabIndex = 28
        Me.chkUsaImpresoraImpacto.TabStop = False
        Me.chkUsaImpresoraImpacto.Text = "Utiliza Impresora Impacto"
        Me.chkUsaImpresoraImpacto.UseVisualStyleBackColor = True
        '
        'chkIncluyeInsumosEnFactura
        '
        Me.chkIncluyeInsumosEnFactura.AutoSize = True
        Me.chkIncluyeInsumosEnFactura.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkIncluyeInsumosEnFactura.Location = New System.Drawing.Point(682, 477)
        Me.chkIncluyeInsumosEnFactura.Name = "chkIncluyeInsumosEnFactura"
        Me.chkIncluyeInsumosEnFactura.Size = New System.Drawing.Size(152, 17)
        Me.chkIncluyeInsumosEnFactura.TabIndex = 34
        Me.chkIncluyeInsumosEnFactura.TabStop = False
        Me.chkIncluyeInsumosEnFactura.Text = "Incluye insumos en factura"
        Me.chkIncluyeInsumosEnFactura.UseVisualStyleBackColor = True
        '
        'chkRespaldoEnLinea
        '
        Me.chkRespaldoEnLinea.AutoSize = True
        Me.chkRespaldoEnLinea.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkRespaldoEnLinea.Location = New System.Drawing.Point(682, 502)
        Me.chkRespaldoEnLinea.Name = "chkRespaldoEnLinea"
        Me.chkRespaldoEnLinea.Size = New System.Drawing.Size(113, 17)
        Me.chkRespaldoEnLinea.TabIndex = 36
        Me.chkRespaldoEnLinea.TabStop = False
        Me.chkRespaldoEnLinea.Text = "Respaldo en línea"
        Me.chkRespaldoEnLinea.UseVisualStyleBackColor = True
        '
        'chkCierrePorTurnos
        '
        Me.chkCierrePorTurnos.AutoSize = True
        Me.chkCierrePorTurnos.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkCierrePorTurnos.Location = New System.Drawing.Point(682, 528)
        Me.chkCierrePorTurnos.Name = "chkCierrePorTurnos"
        Me.chkCierrePorTurnos.Size = New System.Drawing.Size(116, 17)
        Me.chkCierrePorTurnos.TabIndex = 38
        Me.chkCierrePorTurnos.TabStop = False
        Me.chkCierrePorTurnos.Text = "Cierre por períodos"
        Me.chkCierrePorTurnos.UseVisualStyleBackColor = True
        '
        'txtNombreEmpresa
        '
        Me.txtNombreEmpresa.AcceptsReturn = True
        Me.txtNombreEmpresa.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreEmpresa.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtNombreEmpresa.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreEmpresa.Location = New System.Drawing.Point(132, 75)
        Me.txtNombreEmpresa.MaxLength = 50
        Me.txtNombreEmpresa.Name = "txtNombreEmpresa"
        Me.txtNombreEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreEmpresa.Size = New System.Drawing.Size(313, 20)
        Me.txtNombreEmpresa.TabIndex = 1
        '
        'txtCorreoElectronico
        '
        Me.txtCorreoElectronico.AcceptsReturn = True
        Me.txtCorreoElectronico.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorreoElectronico.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCorreoElectronico.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCorreoElectronico.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCorreoElectronico.Location = New System.Drawing.Point(132, 341)
        Me.txtCorreoElectronico.MaxLength = 50
        Me.txtCorreoElectronico.Name = "txtCorreoElectronico"
        Me.txtCorreoElectronico.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorreoElectronico.Size = New System.Drawing.Size(313, 20)
        Me.txtCorreoElectronico.TabIndex = 11
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(7, 344)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(119, 17)
        Me.Label11.TabIndex = 64
        Me.Label11.Text = "Correo electrónico:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkFacturaElectronica
        '
        Me.chkFacturaElectronica.AutoSize = True
        Me.chkFacturaElectronica.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.chkFacturaElectronica.Location = New System.Drawing.Point(682, 554)
        Me.chkFacturaElectronica.Name = "chkFacturaElectronica"
        Me.chkFacturaElectronica.Size = New System.Drawing.Size(152, 17)
        Me.chkFacturaElectronica.TabIndex = 40
        Me.chkFacturaElectronica.TabStop = False
        Me.chkFacturaElectronica.Text = "Habilita factura electrónica"
        Me.chkFacturaElectronica.UseVisualStyleBackColor = True
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(7, 130)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(119, 17)
        Me.Label12.TabIndex = 66
        Me.Label12.Text = "Tipo identificación:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTipoIdentificacion
        '
        Me.cboTipoIdentificacion.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboTipoIdentificacion.FormattingEnabled = True
        Me.cboTipoIdentificacion.Location = New System.Drawing.Point(132, 127)
        Me.cboTipoIdentificacion.Name = "cboTipoIdentificacion"
        Me.cboTipoIdentificacion.Size = New System.Drawing.Size(171, 21)
        Me.cboTipoIdentificacion.TabIndex = 3
        '
        'cboProvincia
        '
        Me.cboProvincia.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboProvincia.FormattingEnabled = True
        Me.cboProvincia.Location = New System.Drawing.Point(132, 180)
        Me.cboProvincia.Name = "cboProvincia"
        Me.cboProvincia.Size = New System.Drawing.Size(147, 21)
        Me.cboProvincia.TabIndex = 5
        '
        'cboCanton
        '
        Me.cboCanton.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboCanton.FormattingEnabled = True
        Me.cboCanton.Location = New System.Drawing.Point(132, 207)
        Me.cboCanton.Name = "cboCanton"
        Me.cboCanton.Size = New System.Drawing.Size(147, 21)
        Me.cboCanton.TabIndex = 6
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(7, 210)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(119, 17)
        Me.Label13.TabIndex = 69
        Me.Label13.Text = "Cantón:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboDistrito
        '
        Me.cboDistrito.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboDistrito.FormattingEnabled = True
        Me.cboDistrito.Location = New System.Drawing.Point(132, 235)
        Me.cboDistrito.Name = "cboDistrito"
        Me.cboDistrito.Size = New System.Drawing.Size(147, 21)
        Me.cboDistrito.TabIndex = 7
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(7, 238)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(119, 17)
        Me.Label14.TabIndex = 71
        Me.Label14.Text = "Distrito:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboBarrio
        '
        Me.cboBarrio.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.cboBarrio.FormattingEnabled = True
        Me.cboBarrio.Location = New System.Drawing.Point(132, 262)
        Me.cboBarrio.Name = "cboBarrio"
        Me.cboBarrio.Size = New System.Drawing.Size(147, 21)
        Me.cboBarrio.TabIndex = 8
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(7, 265)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(119, 17)
        Me.Label15.TabIndex = 73
        Me.Label15.Text = "Barrio:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtServicioFacturaElectronica
        '
        Me.txtServicioFacturaElectronica.AcceptsReturn = True
        Me.txtServicioFacturaElectronica.BackColor = System.Drawing.SystemColors.Window
        Me.txtServicioFacturaElectronica.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtServicioFacturaElectronica.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtServicioFacturaElectronica.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtServicioFacturaElectronica.Location = New System.Drawing.Point(132, 367)
        Me.txtServicioFacturaElectronica.MaxLength = 200
        Me.txtServicioFacturaElectronica.Name = "txtServicioFacturaElectronica"
        Me.txtServicioFacturaElectronica.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtServicioFacturaElectronica.Size = New System.Drawing.Size(313, 20)
        Me.txtServicioFacturaElectronica.TabIndex = 12
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.Location = New System.Drawing.Point(7, 370)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(119, 17)
        Me.Label16.TabIndex = 75
        Me.Label16.Text = "URL fact. electrónica:"
        Me.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtIdCertificado
        '
        Me.txtIdCertificado.AcceptsReturn = True
        Me.txtIdCertificado.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCertificado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCertificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtIdCertificado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCertificado.Location = New System.Drawing.Point(132, 393)
        Me.txtIdCertificado.MaxLength = 200
        Me.txtIdCertificado.Name = "txtIdCertificado"
        Me.txtIdCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCertificado.Size = New System.Drawing.Size(313, 20)
        Me.txtIdCertificado.TabIndex = 13
        '
        'Label17
        '
        Me.Label17.BackColor = System.Drawing.Color.Transparent
        Me.Label17.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label17.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label17.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label17.Location = New System.Drawing.Point(48, 396)
        Me.Label17.Name = "Label17"
        Me.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label17.Size = New System.Drawing.Size(78, 17)
        Me.Label17.TabIndex = 77
        Me.Label17.Text = "Id certificado:"
        Me.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPinCertificado
        '
        Me.txtPinCertificado.AcceptsReturn = True
        Me.txtPinCertificado.BackColor = System.Drawing.SystemColors.Window
        Me.txtPinCertificado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPinCertificado.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPinCertificado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPinCertificado.Location = New System.Drawing.Point(132, 419)
        Me.txtPinCertificado.MaxLength = 50
        Me.txtPinCertificado.Name = "txtPinCertificado"
        Me.txtPinCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPinCertificado.Size = New System.Drawing.Size(44, 20)
        Me.txtPinCertificado.TabIndex = 14
        '
        'Label18
        '
        Me.Label18.BackColor = System.Drawing.Color.Transparent
        Me.Label18.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label18.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label18.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label18.Location = New System.Drawing.Point(7, 422)
        Me.Label18.Name = "Label18"
        Me.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label18.Size = New System.Drawing.Size(119, 17)
        Me.Label18.TabIndex = 79
        Me.Label18.Text = "Pin certificado:"
        Me.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUltimoDocFE
        '
        Me.txtUltimoDocFE.AcceptsReturn = True
        Me.txtUltimoDocFE.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoDocFE.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoDocFE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoDocFE.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoDocFE.Location = New System.Drawing.Point(132, 497)
        Me.txtUltimoDocFE.MaxLength = 50
        Me.txtUltimoDocFE.Name = "txtUltimoDocFE"
        Me.txtUltimoDocFE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoDocFE.Size = New System.Drawing.Size(44, 20)
        Me.txtUltimoDocFE.TabIndex = 15
        '
        'Label19
        '
        Me.Label19.BackColor = System.Drawing.Color.Transparent
        Me.Label19.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label19.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label19.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label19.Location = New System.Drawing.Point(62, 500)
        Me.Label19.Name = "Label19"
        Me.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label19.Size = New System.Drawing.Size(64, 17)
        Me.Label19.TabIndex = 101
        Me.Label19.Text = "Ultimo FE:"
        Me.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtUltimoDocND
        '
        Me.txtUltimoDocND.AcceptsReturn = True
        Me.txtUltimoDocND.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoDocND.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoDocND.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoDocND.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoDocND.Location = New System.Drawing.Point(132, 523)
        Me.txtUltimoDocND.MaxLength = 50
        Me.txtUltimoDocND.Name = "txtUltimoDocND"
        Me.txtUltimoDocND.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoDocND.Size = New System.Drawing.Size(44, 20)
        Me.txtUltimoDocND.TabIndex = 16
        '
        'txtUltimoDocNC
        '
        Me.txtUltimoDocNC.AcceptsReturn = True
        Me.txtUltimoDocNC.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoDocNC.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoDocNC.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoDocNC.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoDocNC.Location = New System.Drawing.Point(401, 499)
        Me.txtUltimoDocNC.MaxLength = 50
        Me.txtUltimoDocNC.Name = "txtUltimoDocNC"
        Me.txtUltimoDocNC.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoDocNC.Size = New System.Drawing.Size(44, 20)
        Me.txtUltimoDocNC.TabIndex = 17
        '
        'txtUltimoDocTE
        '
        Me.txtUltimoDocTE.AcceptsReturn = True
        Me.txtUltimoDocTE.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoDocTE.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoDocTE.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoDocTE.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoDocTE.Location = New System.Drawing.Point(259, 497)
        Me.txtUltimoDocTE.MaxLength = 50
        Me.txtUltimoDocTE.Name = "txtUltimoDocTE"
        Me.txtUltimoDocTE.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoDocTE.Size = New System.Drawing.Size(44, 20)
        Me.txtUltimoDocTE.TabIndex = 18
        '
        'txtUltimoDocMR
        '
        Me.txtUltimoDocMR.AcceptsReturn = True
        Me.txtUltimoDocMR.BackColor = System.Drawing.SystemColors.Window
        Me.txtUltimoDocMR.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUltimoDocMR.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtUltimoDocMR.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUltimoDocMR.Location = New System.Drawing.Point(259, 523)
        Me.txtUltimoDocMR.MaxLength = 50
        Me.txtUltimoDocMR.Name = "txtUltimoDocMR"
        Me.txtUltimoDocMR.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUltimoDocMR.Size = New System.Drawing.Size(44, 20)
        Me.txtUltimoDocMR.TabIndex = 19
        '
        'Label20
        '
        Me.Label20.BackColor = System.Drawing.Color.Transparent
        Me.Label20.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label20.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label20.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label20.Location = New System.Drawing.Point(62, 524)
        Me.Label20.Name = "Label20"
        Me.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label20.Size = New System.Drawing.Size(64, 17)
        Me.Label20.TabIndex = 109
        Me.Label20.Text = "Ultimo ND:"
        Me.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label21
        '
        Me.Label21.BackColor = System.Drawing.Color.Transparent
        Me.Label21.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label21.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label21.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label21.Location = New System.Drawing.Point(331, 500)
        Me.Label21.Name = "Label21"
        Me.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label21.Size = New System.Drawing.Size(64, 17)
        Me.Label21.TabIndex = 110
        Me.Label21.Text = "Ultimo NC:"
        Me.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label22
        '
        Me.Label22.BackColor = System.Drawing.Color.Transparent
        Me.Label22.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label22.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label22.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label22.Location = New System.Drawing.Point(189, 500)
        Me.Label22.Name = "Label22"
        Me.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label22.Size = New System.Drawing.Size(64, 17)
        Me.Label22.TabIndex = 111
        Me.Label22.Text = "Ultimo TE:"
        Me.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label23
        '
        Me.Label23.BackColor = System.Drawing.Color.Transparent
        Me.Label23.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label23.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label23.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label23.Location = New System.Drawing.Point(189, 524)
        Me.Label23.Name = "Label23"
        Me.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label23.Size = New System.Drawing.Size(64, 17)
        Me.Label23.TabIndex = 112
        Me.Label23.Text = "Ultimo MR:"
        Me.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'picLogo
        '
        Me.picLogo.BackColor = System.Drawing.Color.White
        Me.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picLogo.Location = New System.Drawing.Point(475, 272)
        Me.picLogo.Name = "picLogo"
        Me.picLogo.Size = New System.Drawing.Size(350, 160)
        Me.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picLogo.TabIndex = 113
        Me.picLogo.TabStop = False
        '
        'btnCargarLogo
        '
        Me.btnCargarLogo.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCargarLogo.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCargarLogo.Font = New System.Drawing.Font("Microsoft Sans Serif", 8.25!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnCargarLogo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCargarLogo.Location = New System.Drawing.Point(475, 438)
        Me.btnCargarLogo.Name = "btnCargarLogo"
        Me.btnCargarLogo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCargarLogo.Size = New System.Drawing.Size(78, 26)
        Me.btnCargarLogo.TabIndex = 114
        Me.btnCargarLogo.TabStop = False
        Me.btnCargarLogo.Text = "&Cargar logo"
        Me.btnCargarLogo.UseVisualStyleBackColor = False
        '
        'FrmEmpresa
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(844, 584)
        Me.Controls.Add(Me.btnCargarLogo)
        Me.Controls.Add(Me.picLogo)
        Me.Controls.Add(Me.Label23)
        Me.Controls.Add(Me.Label22)
        Me.Controls.Add(Me.Label21)
        Me.Controls.Add(Me.Label20)
        Me.Controls.Add(Me.txtUltimoDocMR)
        Me.Controls.Add(Me.txtUltimoDocTE)
        Me.Controls.Add(Me.txtUltimoDocNC)
        Me.Controls.Add(Me.txtUltimoDocND)
        Me.Controls.Add(Me.txtUltimoDocFE)
        Me.Controls.Add(Me.Label19)
        Me.Controls.Add(Me.txtPinCertificado)
        Me.Controls.Add(Me.Label18)
        Me.Controls.Add(Me.txtIdCertificado)
        Me.Controls.Add(Me.Label17)
        Me.Controls.Add(Me.txtServicioFacturaElectronica)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cboBarrio)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboDistrito)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.cboCanton)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.cboProvincia)
        Me.Controls.Add(Me.cboTipoIdentificacion)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.chkFacturaElectronica)
        Me.Controls.Add(Me.txtCorreoElectronico)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtNombreEmpresa)
        Me.Controls.Add(Me.chkCierrePorTurnos)
        Me.Controls.Add(Me.chkRespaldoEnLinea)
        Me.Controls.Add(Me.chkIncluyeInsumosEnFactura)
        Me.Controls.Add(Me.chkUsaImpresoraImpacto)
        Me.Controls.Add(Me.txtCodigoServInst)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPorcentajeInstalacion)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.chkDesgloseInst)
        Me.Controls.Add(Me.chkModificaDesc)
        Me.Controls.Add(Me.ckbAutoCompleta)
        Me.Controls.Add(Me.ckbContabiliza)
        Me.Controls.Add(Me.txtLineasFactura)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtPorcentajeIVA)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtImpresoraFactura)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtTelefono)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtDireccion)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtIdentificacion)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtNombreComercial)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnEliminarDetalle)
        Me.Controls.Add(Me.btnInsertarDetalle)
        Me.Controls.Add(Me.dgvEquipos)
        Me.Controls.Add(Me.CmdConsultar)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.txtEquipo)
        Me.Controls.Add(Me.cmdUpdate)
        Me.Controls.Add(Me.cmdCancel)
        Me.Controls.Add(Me.txtIdEmpresa)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmEmpresa"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.Manual
        Me.Text = "Empresa"
        CType(Me.dgvEquipos, System.ComponentModel.ISupportInitialize).EndInit()
        CType(Me.picLogo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents CmdConsultar As System.Windows.Forms.Button
    Friend WithEvents dgvEquipos As System.Windows.Forms.DataGridView
    Public WithEvents btnEliminarDetalle As System.Windows.Forms.Button
    Public WithEvents btnInsertarDetalle As System.Windows.Forms.Button
    Public WithEvents txtNombreComercial As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtIdentificacion As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtDireccion As System.Windows.Forms.TextBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents txtTelefono As System.Windows.Forms.TextBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtImpresoraFactura As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtPorcentajeIVA As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txtLineasFactura As System.Windows.Forms.TextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Friend WithEvents ckbContabiliza As System.Windows.Forms.CheckBox
    Friend WithEvents ckbAutoCompleta As System.Windows.Forms.CheckBox
    Friend WithEvents chkModificaDesc As System.Windows.Forms.CheckBox
    Friend WithEvents chkDesgloseInst As System.Windows.Forms.CheckBox
    Public WithEvents txtPorcentajeInstalacion As System.Windows.Forms.TextBox
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents txtCodigoServInst As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Friend WithEvents chkUsaImpresoraImpacto As System.Windows.Forms.CheckBox
    Friend WithEvents chkIncluyeInsumosEnFactura As System.Windows.Forms.CheckBox
    Friend WithEvents chkRespaldoEnLinea As System.Windows.Forms.CheckBox
    Friend WithEvents chkCierrePorTurnos As System.Windows.Forms.CheckBox
    Public WithEvents txtNombreEmpresa As System.Windows.Forms.TextBox
    Public WithEvents txtCorreoElectronico As System.Windows.Forms.TextBox
    Public WithEvents Label11 As System.Windows.Forms.Label
    Friend WithEvents chkFacturaElectronica As System.Windows.Forms.CheckBox
    Public WithEvents Label12 As System.Windows.Forms.Label
    Friend WithEvents cboTipoIdentificacion As System.Windows.Forms.ComboBox
    Friend WithEvents cboProvincia As System.Windows.Forms.ComboBox
    Friend WithEvents cboCanton As System.Windows.Forms.ComboBox
    Public WithEvents Label13 As System.Windows.Forms.Label
    Friend WithEvents cboDistrito As System.Windows.Forms.ComboBox
    Public WithEvents Label14 As System.Windows.Forms.Label
    Friend WithEvents cboBarrio As System.Windows.Forms.ComboBox
    Public WithEvents Label15 As System.Windows.Forms.Label
    Public WithEvents txtServicioFacturaElectronica As TextBox
    Public WithEvents Label16 As Label
    Public WithEvents txtIdCertificado As TextBox
    Public WithEvents Label17 As Label
    Public WithEvents txtPinCertificado As TextBox
    Public WithEvents Label18 As Label
    Public WithEvents txtUltimoDocFE As TextBox
    Public WithEvents Label19 As Label
    Public WithEvents txtUltimoDocND As TextBox
    Public WithEvents txtUltimoDocNC As TextBox
    Public WithEvents txtUltimoDocTE As TextBox
    Public WithEvents txtUltimoDocMR As TextBox
    Public WithEvents Label20 As Label
    Public WithEvents Label21 As Label
    Public WithEvents Label22 As Label
    Public WithEvents Label23 As Label
    Friend WithEvents picLogo As PictureBox
    Public WithEvents btnCargarLogo As Button
    Friend WithEvents ofdAbrirDocumento As OpenFileDialog
#End Region
End Class