<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmFacturaCompra
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
    Public WithEvents txtTextoAdicional As System.Windows.Forms.TextBox
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtIdFactCompra As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmFacturaCompra))
        Me.txtTextoAdicional = New System.Windows.Forms.TextBox()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtIdFactCompra = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        Me.txtNombreComercial = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboBarrio = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.cboDistrito = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboCanton = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.cboProvincia = New System.Windows.Forms.ComboBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.cboTipoIdentificacion = New System.Windows.Forms.ComboBox()
        Me.txtCorreoElectronico = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtTelefono = New System.Windows.Forms.TextBox()
        Me.txtDireccion = New System.Windows.Forms.TextBox()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.txtIdentificacion = New System.Windows.Forms.TextBox()
        Me.lblLabel6 = New System.Windows.Forms.Label()
        Me.lblLabel5 = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.txtFechaExoneracion = New System.Windows.Forms.DateTimePicker()
        Me.Label16 = New System.Windows.Forms.Label()
        Me.cboTipoExoneracion = New System.Windows.Forms.ComboBox()
        Me.txtPorcentajeExoneracion = New System.Windows.Forms.TextBox()
        Me.Label14 = New System.Windows.Forms.Label()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtNombreInstExoneracion = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtNumDocExoneracion = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.btnBuscarClasificacion = New System.Windows.Forms.Button()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me._LblImpuesto_0 = New System.Windows.Forms.Label()
        Me.cboTipoImpuesto = New System.Windows.Forms.ComboBox()
        Me._LblTotal_6 = New System.Windows.Forms.Label()
        Me.cboUnidadMedida = New System.Windows.Forms.ComboBox()
        Me._lblSubTotal_5 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.grdDetalleProforma = New System.Windows.Forms.DataGridView()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.GroupBox1.SuspendLayout()
        CType(Me.grdDetalleProforma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTextoAdicional
        '
        Me.txtTextoAdicional.AcceptsReturn = True
        Me.txtTextoAdicional.BackColor = System.Drawing.SystemColors.Window
        Me.txtTextoAdicional.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTextoAdicional.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTextoAdicional.Location = New System.Drawing.Point(113, 602)
        Me.txtTextoAdicional.MaxLength = 500
        Me.txtTextoAdicional.Multiline = True
        Me.txtTextoAdicional.Name = "txtTextoAdicional"
        Me.txtTextoAdicional.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTextoAdicional.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTextoAdicional.Size = New System.Drawing.Size(704, 45)
        Me.txtTextoAdicional.TabIndex = 59
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAgregar.Location = New System.Drawing.Point(72, 8)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAgregar.Size = New System.Drawing.Size(64, 21)
        Me.btnAgregar.TabIndex = 84
        Me.btnAgregar.TabStop = False
        Me.btnAgregar.Text = "&Nuevo"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGuardar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGuardar.Location = New System.Drawing.Point(8, 8)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnGuardar.Size = New System.Drawing.Size(64, 21)
        Me.btnGuardar.TabIndex = 80
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'txtIdFactCompra
        '
        Me.txtIdFactCompra.AcceptsReturn = True
        Me.txtIdFactCompra.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdFactCompra.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdFactCompra.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdFactCompra.Location = New System.Drawing.Point(102, 39)
        Me.txtIdFactCompra.MaxLength = 0
        Me.txtIdFactCompra.Name = "txtIdFactCompra"
        Me.txtIdFactCompra.ReadOnly = True
        Me.txtIdFactCompra.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdFactCompra.Size = New System.Drawing.Size(73, 20)
        Me.txtIdFactCompra.TabIndex = 0
        Me.txtIdFactCompra.TabStop = False
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(7, 602)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(100, 19)
        Me._lblLabels_11.TabIndex = 44
        Me._lblLabels_11.Text = "Otras referencias:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(8, 39)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(88, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "Documento No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(231, 39)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(65, 20)
        Me.txtFecha.TabIndex = 4
        Me.txtFecha.TabStop = False
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(181, 39)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(44, 19)
        Me.Label15.TabIndex = 111
        Me.Label15.Text = "Fecha:"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNombreComercial
        '
        Me.txtNombreComercial.AcceptsReturn = True
        Me.txtNombreComercial.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreComercial.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreComercial.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreComercial.Location = New System.Drawing.Point(510, 172)
        Me.txtNombreComercial.MaxLength = 80
        Me.txtNombreComercial.Name = "txtNombreComercial"
        Me.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreComercial.Size = New System.Drawing.Size(298, 20)
        Me.txtNombreComercial.TabIndex = 10
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(405, 175)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(99, 17)
        Me.Label2.TabIndex = 201
        Me.Label2.Text = "Nombre comercial:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(370, 121)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(55, 17)
        Me.Label7.TabIndex = 200
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
        Me.cboBarrio.Location = New System.Drawing.Point(431, 118)
        Me.cboBarrio.Name = "cboBarrio"
        Me.cboBarrio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboBarrio.Size = New System.Drawing.Size(250, 21)
        Me.cboBarrio.TabIndex = 7
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(40, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(58, 17)
        Me.Label6.TabIndex = 199
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
        Me.cboDistrito.Location = New System.Drawing.Point(104, 119)
        Me.cboDistrito.Name = "cboDistrito"
        Me.cboDistrito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboDistrito.Size = New System.Drawing.Size(250, 21)
        Me.cboDistrito.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(365, 94)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(60, 17)
        Me.Label5.TabIndex = 198
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
        Me.cboCanton.Location = New System.Drawing.Point(431, 91)
        Me.cboCanton.Name = "cboCanton"
        Me.cboCanton.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCanton.Size = New System.Drawing.Size(250, 21)
        Me.cboCanton.TabIndex = 5
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(31, 95)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(67, 17)
        Me.Label4.TabIndex = 197
        Me.Label4.Text = "Provincia:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.cboProvincia.Location = New System.Drawing.Point(104, 92)
        Me.cboProvincia.Name = "cboProvincia"
        Me.cboProvincia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboProvincia.Size = New System.Drawing.Size(250, 21)
        Me.cboProvincia.TabIndex = 4
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(-1, 69)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(99, 17)
        Me.Label3.TabIndex = 196
        Me.Label3.Text = "Tipo identificación:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.TopRight
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
        Me.cboTipoIdentificacion.Location = New System.Drawing.Point(104, 65)
        Me.cboTipoIdentificacion.Name = "cboTipoIdentificacion"
        Me.cboTipoIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoIdentificacion.Size = New System.Drawing.Size(192, 21)
        Me.cboTipoIdentificacion.TabIndex = 2
        '
        'txtCorreoElectronico
        '
        Me.txtCorreoElectronico.AcceptsReturn = True
        Me.txtCorreoElectronico.BackColor = System.Drawing.SystemColors.Window
        Me.txtCorreoElectronico.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCorreoElectronico.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCorreoElectronico.Location = New System.Drawing.Point(510, 198)
        Me.txtCorreoElectronico.MaxLength = 160
        Me.txtCorreoElectronico.Name = "txtCorreoElectronico"
        Me.txtCorreoElectronico.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCorreoElectronico.Size = New System.Drawing.Size(298, 20)
        Me.txtCorreoElectronico.TabIndex = 12
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(407, 201)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(97, 17)
        Me.Label9.TabIndex = 195
        Me.Label9.Text = "Correo electrónico:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtTelefono
        '
        Me.txtTelefono.AcceptsReturn = True
        Me.txtTelefono.BackColor = System.Drawing.SystemColors.Window
        Me.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTelefono.Location = New System.Drawing.Point(104, 198)
        Me.txtTelefono.MaxLength = 20
        Me.txtTelefono.Name = "txtTelefono"
        Me.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTelefono.Size = New System.Drawing.Size(297, 20)
        Me.txtTelefono.TabIndex = 11
        '
        'txtDireccion
        '
        Me.txtDireccion.AcceptsReturn = True
        Me.txtDireccion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDireccion.Location = New System.Drawing.Point(104, 146)
        Me.txtDireccion.MaxLength = 250
        Me.txtDireccion.Name = "txtDireccion"
        Me.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDireccion.Size = New System.Drawing.Size(704, 20)
        Me.txtDireccion.TabIndex = 8
        '
        'txtNombre
        '
        Me.txtNombre.AcceptsReturn = True
        Me.txtNombre.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombre.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombre.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombre.Location = New System.Drawing.Point(104, 172)
        Me.txtNombre.MaxLength = 100
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
        Me.txtIdentificacion.Location = New System.Drawing.Point(431, 65)
        Me.txtIdentificacion.MaxLength = 12
        Me.txtIdentificacion.Name = "txtIdentificacion"
        Me.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdentificacion.Size = New System.Drawing.Size(192, 20)
        Me.txtIdentificacion.TabIndex = 3
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(-1, 201)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(99, 17)
        Me.lblLabel6.TabIndex = 178
        Me.lblLabel6.Text = "Teléfono:"
        Me.lblLabel6.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel5
        '
        Me.lblLabel5.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel5.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel5.Location = New System.Drawing.Point(-1, 149)
        Me.lblLabel5.Name = "lblLabel5"
        Me.lblLabel5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel5.Size = New System.Drawing.Size(99, 17)
        Me.lblLabel5.TabIndex = 177
        Me.lblLabel5.Text = "Dirección:"
        Me.lblLabel5.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(-1, 175)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(99, 17)
        Me.lblLabel4.TabIndex = 181
        Me.lblLabel4.Text = "Nombre:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(345, 68)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel3.Size = New System.Drawing.Size(80, 17)
        Me.lblLabel3.TabIndex = 176
        Me.lblLabel3.Text = "Identificación:"
        Me.lblLabel3.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'txtFechaExoneracion
        '
        Me.txtFechaExoneracion.Format = System.Windows.Forms.DateTimePickerFormat.Custom
        Me.txtFechaExoneracion.Location = New System.Drawing.Point(724, 224)
        Me.txtFechaExoneracion.Name = "txtFechaExoneracion"
        Me.txtFechaExoneracion.Size = New System.Drawing.Size(84, 20)
        Me.txtFechaExoneracion.TabIndex = 15
        Me.txtFechaExoneracion.Value = New Date(2013, 6, 9, 0, 0, 0, 0)
        '
        'Label16
        '
        Me.Label16.BackColor = System.Drawing.Color.Transparent
        Me.Label16.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label16.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label16.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Label16.Location = New System.Drawing.Point(-23, 224)
        Me.Label16.Name = "Label16"
        Me.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label16.Size = New System.Drawing.Size(121, 19)
        Me.Label16.TabIndex = 211
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
        Me.cboTipoExoneracion.Location = New System.Drawing.Point(104, 224)
        Me.cboTipoExoneracion.Name = "cboTipoExoneracion"
        Me.cboTipoExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoExoneracion.Size = New System.Drawing.Size(247, 21)
        Me.cboTipoExoneracion.TabIndex = 13
        '
        'txtPorcentajeExoneracion
        '
        Me.txtPorcentajeExoneracion.AcceptsReturn = True
        Me.txtPorcentajeExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentajeExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentajeExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentajeExoneracion.Location = New System.Drawing.Point(510, 250)
        Me.txtPorcentajeExoneracion.MaxLength = 3
        Me.txtPorcentajeExoneracion.Name = "txtPorcentajeExoneracion"
        Me.txtPorcentajeExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentajeExoneracion.Size = New System.Drawing.Size(26, 20)
        Me.txtPorcentajeExoneracion.TabIndex = 17
        Me.txtPorcentajeExoneracion.TabStop = False
        '
        'Label14
        '
        Me.Label14.BackColor = System.Drawing.Color.Transparent
        Me.Label14.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label14.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label14.Location = New System.Drawing.Point(379, 250)
        Me.Label14.Name = "Label14"
        Me.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label14.Size = New System.Drawing.Size(125, 19)
        Me.Label14.TabIndex = 209
        Me.Label14.Text = "Porcentaje exoneración:"
        Me.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(636, 224)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(82, 19)
        Me.Label8.TabIndex = 208
        Me.Label8.Text = "Fecha emisión:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNombreInstExoneracion
        '
        Me.txtNombreInstExoneracion.AcceptsReturn = True
        Me.txtNombreInstExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreInstExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreInstExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreInstExoneracion.Location = New System.Drawing.Point(104, 251)
        Me.txtNombreInstExoneracion.MaxLength = 160
        Me.txtNombreInstExoneracion.Name = "txtNombreInstExoneracion"
        Me.txtNombreInstExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreInstExoneracion.Size = New System.Drawing.Size(247, 20)
        Me.txtNombreInstExoneracion.TabIndex = 16
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(2, 251)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(96, 19)
        Me.Label10.TabIndex = 207
        Me.Label10.Text = "Nombre inst:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtNumDocExoneracion
        '
        Me.txtNumDocExoneracion.AcceptsReturn = True
        Me.txtNumDocExoneracion.BackColor = System.Drawing.SystemColors.Window
        Me.txtNumDocExoneracion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNumDocExoneracion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNumDocExoneracion.Location = New System.Drawing.Point(510, 224)
        Me.txtNumDocExoneracion.MaxLength = 40
        Me.txtNumDocExoneracion.Name = "txtNumDocExoneracion"
        Me.txtNumDocExoneracion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNumDocExoneracion.Size = New System.Drawing.Size(120, 20)
        Me.txtNumDocExoneracion.TabIndex = 14
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(384, 224)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(120, 19)
        Me._lblLabels_3.TabIndex = 206
        Me._lblLabels_3.Text = "Nro. doc. exoneración"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.btnBuscarClasificacion)
        Me.GroupBox1.Controls.Add(Me.txtImpuesto)
        Me.GroupBox1.Controls.Add(Me._lblLabels_6)
        Me.GroupBox1.Controls.Add(Me.txtTotal)
        Me.GroupBox1.Controls.Add(Me.txtSubTotal)
        Me.GroupBox1.Controls.Add(Me.Label12)
        Me.GroupBox1.Controls.Add(Me._LblImpuesto_0)
        Me.GroupBox1.Controls.Add(Me.cboTipoImpuesto)
        Me.GroupBox1.Controls.Add(Me._LblTotal_6)
        Me.GroupBox1.Controls.Add(Me.cboUnidadMedida)
        Me.GroupBox1.Controls.Add(Me._lblSubTotal_5)
        Me.GroupBox1.Controls.Add(Me.txtCodigo)
        Me.GroupBox1.Controls.Add(Me.Label1)
        Me.GroupBox1.Controls.Add(Me.txtDescripcion)
        Me.GroupBox1.Controls.Add(Me.txtPrecio)
        Me.GroupBox1.Controls.Add(Me.txtCantidad)
        Me.GroupBox1.Controls.Add(Me._lblLabels_8)
        Me.GroupBox1.Controls.Add(Me._lblLabels_7)
        Me.GroupBox1.Controls.Add(Me._lblLabels_1)
        Me.GroupBox1.Controls.Add(Me.grdDetalleProforma)
        Me.GroupBox1.Controls.Add(Me.btnEliminar)
        Me.GroupBox1.Controls.Add(Me.btnInsertar)
        Me.GroupBox1.Location = New System.Drawing.Point(7, 274)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(821, 322)
        Me.GroupBox1.TabIndex = 214
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Detalle factura compra"
        '
        'btnBuscarClasificacion
        '
        Me.btnBuscarClasificacion.Image = CType(resources.GetObject("btnBuscarClasificacion.Image"), System.Drawing.Image)
        Me.btnBuscarClasificacion.Location = New System.Drawing.Point(388, 19)
        Me.btnBuscarClasificacion.Name = "btnBuscarClasificacion"
        Me.btnBuscarClasificacion.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarClasificacion.TabIndex = 231
        Me.btnBuscarClasificacion.TabStop = False
        Me.btnBuscarClasificacion.UseVisualStyleBackColor = True
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(629, 289)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 219
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(46, 20)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(43, 19)
        Me._lblLabels_6.TabIndex = 230
        Me._lblLabels_6.Text = "Cant:"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(737, 289)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 220
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(529, 289)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 218
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(414, 22)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(83, 17)
        Me.Label12.TabIndex = 229
        Me.Label12.Text = "Tipo Impuesto:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblImpuesto_0
        '
        Me._LblImpuesto_0.BackColor = System.Drawing.Color.Transparent
        Me._LblImpuesto_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblImpuesto_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblImpuesto_0.Location = New System.Drawing.Point(598, 289)
        Me._LblImpuesto_0.Name = "_LblImpuesto_0"
        Me._LblImpuesto_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblImpuesto_0.Size = New System.Drawing.Size(32, 19)
        Me._LblImpuesto_0.TabIndex = 217
        Me._LblImpuesto_0.Text = "Imp:"
        Me._LblImpuesto_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTipoImpuesto
        '
        Me.cboTipoImpuesto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoImpuesto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoImpuesto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoImpuesto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoImpuesto.Items.AddRange(New Object() {"UND", "MT2", "MT3", "MT", "LT", "GL", "CTO", "CUB", "PAQ", "LAM", "VAR", "PZA"})
        Me.cboTipoImpuesto.Location = New System.Drawing.Point(503, 20)
        Me.cboTipoImpuesto.Name = "cboTipoImpuesto"
        Me.cboTipoImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoImpuesto.Size = New System.Drawing.Size(307, 21)
        Me.cboTipoImpuesto.TabIndex = 52
        '
        '_LblTotal_6
        '
        Me._LblTotal_6.BackColor = System.Drawing.Color.Transparent
        Me._LblTotal_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblTotal_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblTotal_6.Location = New System.Drawing.Point(697, 289)
        Me._LblTotal_6.Name = "_LblTotal_6"
        Me._LblTotal_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblTotal_6.Size = New System.Drawing.Size(42, 19)
        Me._LblTotal_6.TabIndex = 216
        Me._LblTotal_6.Text = "Total:"
        Me._LblTotal_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboUnidadMedida
        '
        Me.cboUnidadMedida.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUnidadMedida.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUnidadMedida.BackColor = System.Drawing.SystemColors.Window
        Me.cboUnidadMedida.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUnidadMedida.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnidadMedida.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUnidadMedida.IntegralHeight = False
        Me.cboUnidadMedida.ItemHeight = 13
        Me.cboUnidadMedida.Location = New System.Drawing.Point(601, 48)
        Me.cboUnidadMedida.Name = "cboUnidadMedida"
        Me.cboUnidadMedida.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUnidadMedida.Size = New System.Drawing.Size(54, 21)
        Me.cboUnidadMedida.TabIndex = 54
        '
        '_lblSubTotal_5
        '
        Me._lblSubTotal_5.BackColor = System.Drawing.Color.Transparent
        Me._lblSubTotal_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblSubTotal_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblSubTotal_5.Location = New System.Drawing.Point(465, 289)
        Me._lblSubTotal_5.Name = "_lblSubTotal_5"
        Me._lblSubTotal_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblSubTotal_5.Size = New System.Drawing.Size(65, 19)
        Me._lblSubTotal_5.TabIndex = 215
        Me._lblSubTotal_5.Text = "Sub-Total:"
        Me._lblSubTotal_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(195, 20)
        Me.txtCodigo.MaxLength = 13
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.ReadOnly = True
        Me.txtCodigo.Size = New System.Drawing.Size(187, 20)
        Me.txtCodigo.TabIndex = 51
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(21, 48)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(68, 19)
        Me.Label1.TabIndex = 226
        Me.Label1.Text = "Descripción:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(95, 48)
        Me.txtDescripcion.MaxLength = 200
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(455, 20)
        Me.txtDescripcion.TabIndex = 53
        '
        'txtPrecio
        '
        Me.txtPrecio.AcceptsReturn = True
        Me.txtPrecio.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecio.Location = New System.Drawing.Point(710, 48)
        Me.txtPrecio.MaxLength = 0
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecio.TabIndex = 55
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(95, 20)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(43, 20)
        Me.txtCantidad.TabIndex = 50
        Me.txtCantidad.Text = "1"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_8.Location = New System.Drawing.Point(556, 49)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(39, 19)
        Me._lblLabels_8.TabIndex = 225
        Me._lblLabels_8.Text = "U/M:"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_7.Location = New System.Drawing.Point(663, 49)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(41, 19)
        Me._lblLabels_7.TabIndex = 224
        Me._lblLabels_7.Text = "Precio:"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(143, 20)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(46, 19)
        Me._lblLabels_1.TabIndex = 223
        Me._lblLabels_1.Text = "Código:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleProforma
        '
        Me.grdDetalleProforma.AllowUserToAddRows = False
        Me.grdDetalleProforma.AllowUserToDeleteRows = False
        Me.grdDetalleProforma.AllowUserToResizeColumns = False
        Me.grdDetalleProforma.AllowUserToResizeRows = False
        Me.grdDetalleProforma.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleProforma.Location = New System.Drawing.Point(10, 83)
        Me.grdDetalleProforma.MultiSelect = False
        Me.grdDetalleProforma.Name = "grdDetalleProforma"
        Me.grdDetalleProforma.ReadOnly = True
        Me.grdDetalleProforma.RowHeadersVisible = False
        Me.grdDetalleProforma.RowHeadersWidth = 30
        Me.grdDetalleProforma.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleProforma.TabIndex = 56
        Me.grdDetalleProforma.TabStop = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminar.Location = New System.Drawing.Point(90, 289)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(99, 25)
        Me.btnEliminar.TabIndex = 58
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Limpiar detalle"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(11, 289)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 57
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'FrmFacturaCompra
        '
        Me.AcceptButton = Me.btnInsertar
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None
        Me.AutoScroll = True
        Me.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(839, 660)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.txtFechaExoneracion)
        Me.Controls.Add(Me.Label16)
        Me.Controls.Add(Me.cboTipoExoneracion)
        Me.Controls.Add(Me.txtPorcentajeExoneracion)
        Me.Controls.Add(Me.Label14)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtNombreInstExoneracion)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtNumDocExoneracion)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me.txtNombreComercial)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboBarrio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.cboCanton)
        Me.Controls.Add(Me.cboDistrito)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.cboProvincia)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.cboTipoIdentificacion)
        Me.Controls.Add(Me.txtCorreoElectronico)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtTelefono)
        Me.Controls.Add(Me.txtDireccion)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.txtIdentificacion)
        Me.Controls.Add(Me.lblLabel6)
        Me.Controls.Add(Me.lblLabel5)
        Me.Controls.Add(Me.lblLabel4)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.txtTextoAdicional)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtIdFactCompra)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmFacturaCompra"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de registro de facturas de compra"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        CType(Me.grdDetalleProforma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtFecha As TextBox
    Public WithEvents Label15 As Label
    Public WithEvents txtNombreComercial As TextBox
    Public WithEvents Label2 As Label
    Public WithEvents Label7 As Label
    Public WithEvents cboBarrio As ComboBox
    Public WithEvents Label6 As Label
    Public WithEvents cboDistrito As ComboBox
    Public WithEvents Label5 As Label
    Public WithEvents cboCanton As ComboBox
    Public WithEvents Label4 As Label
    Public WithEvents cboProvincia As ComboBox
    Public WithEvents Label3 As Label
    Public WithEvents cboTipoIdentificacion As ComboBox
    Public WithEvents txtCorreoElectronico As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents txtTelefono As TextBox
    Public WithEvents txtDireccion As TextBox
    Public WithEvents txtNombre As TextBox
    Public WithEvents txtIdentificacion As TextBox
    Public WithEvents lblLabel6 As Label
    Public WithEvents lblLabel5 As Label
    Public WithEvents lblLabel4 As Label
    Public WithEvents lblLabel3 As Label
    Friend WithEvents txtFechaExoneracion As DateTimePicker
    Public WithEvents Label16 As Label
    Public WithEvents cboTipoExoneracion As ComboBox
    Public WithEvents txtPorcentajeExoneracion As TextBox
    Public WithEvents Label14 As Label
    Public WithEvents Label8 As Label
    Public WithEvents txtNombreInstExoneracion As TextBox
    Public WithEvents Label10 As Label
    Public WithEvents txtNumDocExoneracion As TextBox
    Public WithEvents _lblLabels_3 As Label
    Friend WithEvents GroupBox1 As GroupBox
    Public WithEvents txtImpuesto As TextBox
    Public WithEvents _lblLabels_6 As Label
    Public WithEvents txtTotal As TextBox
    Public WithEvents txtSubTotal As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents _LblImpuesto_0 As Label
    Public WithEvents cboTipoImpuesto As ComboBox
    Public WithEvents _LblTotal_6 As Label
    Public WithEvents cboUnidadMedida As ComboBox
    Public WithEvents _lblSubTotal_5 As Label
    Friend WithEvents txtCodigo As TextBox
    Public WithEvents Label1 As Label
    Public WithEvents txtDescripcion As TextBox
    Public WithEvents txtPrecio As TextBox
    Public WithEvents txtCantidad As TextBox
    Public WithEvents _lblLabels_8 As Label
    Public WithEvents _lblLabels_7 As Label
    Public WithEvents _lblLabels_1 As Label
    Public WithEvents grdDetalleProforma As DataGridView
    Public WithEvents btnEliminar As Button
    Public WithEvents btnInsertar As Button
    Friend WithEvents btnBuscarClasificacion As Button
End Class