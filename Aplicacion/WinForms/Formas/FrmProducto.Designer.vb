<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProducto
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
    Public WithEvents txtPrecioVenta1 As System.Windows.Forms.TextBox
    Public WithEvents cboUnidad As System.Windows.Forms.ComboBox
    Public WithEvents CboLinea As System.Windows.Forms.ComboBox
    Public WithEvents txtIndExistencia As System.Windows.Forms.TextBox
    Public WithEvents txtPrecioCosto As System.Windows.Forms.TextBox
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents txtIdProducto As System.Windows.Forms.TextBox
    Public WithEvents lblPrecioVenta1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_12 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_10 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProducto))
        Me.txtPrecioVenta1 = New System.Windows.Forms.TextBox()
        Me.cboUnidad = New System.Windows.Forms.ComboBox()
        Me.CboLinea = New System.Windows.Forms.ComboBox()
        Me.txtIndExistencia = New System.Windows.Forms.TextBox()
        Me.txtPrecioCosto = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.txtIdProducto = New System.Windows.Forms.TextBox()
        Me.lblPrecioVenta1 = New System.Windows.Forms.Label()
        Me._lblLabels_12 = New System.Windows.Forms.Label()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_10 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.cboTipoProducto = New System.Windows.Forms.ComboBox()
        Me.ptbImagen = New System.Windows.Forms.PictureBox()
        Me.btnCargarImagen = New System.Windows.Forms.Button()
        Me.ofdAbrirImagen = New System.Windows.Forms.OpenFileDialog()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.btnEliminarImagen = New System.Windows.Forms.Button()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.chkExcento = New System.Windows.Forms.CheckBox()
        Me.btnBuscarProveedor = New System.Windows.Forms.Button()
        Me.txtProveedor = New System.Windows.Forms.TextBox()
        Me.txtPrecioVenta2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta4 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta5 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        CType(Me.ptbImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPrecioVenta1
        '
        Me.txtPrecioVenta1.AcceptsReturn = True
        Me.txtPrecioVenta1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta1.Location = New System.Drawing.Point(94, 263)
        Me.txtPrecioVenta1.MaxLength = 0
        Me.txtPrecioVenta1.Name = "txtPrecioVenta1"
        Me.txtPrecioVenta1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta1.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta1.TabIndex = 8
        Me.txtPrecioVenta1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboUnidad
        '
        Me.cboUnidad.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboUnidad.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboUnidad.BackColor = System.Drawing.SystemColors.Window
        Me.cboUnidad.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboUnidad.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboUnidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboUnidad.Items.AddRange(New Object() {"UND", "MT2", "MT3", "MT", "LT", "GL", "CTO", "CUB", "PAQ", "LAM", "VAR", "PZA"})
        Me.cboUnidad.Location = New System.Drawing.Point(94, 439)
        Me.cboUnidad.Name = "cboUnidad"
        Me.cboUnidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboUnidad.Size = New System.Drawing.Size(128, 21)
        Me.cboUnidad.TabIndex = 15
        '
        'CboLinea
        '
        Me.CboLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.CboLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.CboLinea.BackColor = System.Drawing.SystemColors.Window
        Me.CboLinea.Cursor = System.Windows.Forms.Cursors.Default
        Me.CboLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.CboLinea.ForeColor = System.Drawing.SystemColors.WindowText
        Me.CboLinea.Location = New System.Drawing.Point(94, 91)
        Me.CboLinea.Name = "CboLinea"
        Me.CboLinea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CboLinea.Size = New System.Drawing.Size(319, 21)
        Me.CboLinea.TabIndex = 2
        '
        'txtIndExistencia
        '
        Me.txtIndExistencia.AcceptsReturn = True
        Me.txtIndExistencia.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndExistencia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndExistencia.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIndExistencia.Location = New System.Drawing.Point(94, 413)
        Me.txtIndExistencia.MaxLength = 0
        Me.txtIndExistencia.Name = "txtIndExistencia"
        Me.txtIndExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndExistencia.Size = New System.Drawing.Size(45, 20)
        Me.txtIndExistencia.TabIndex = 14
        '
        'txtPrecioCosto
        '
        Me.txtPrecioCosto.AcceptsReturn = True
        Me.txtPrecioCosto.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioCosto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioCosto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioCosto.Location = New System.Drawing.Point(94, 237)
        Me.txtPrecioCosto.MaxLength = 0
        Me.txtPrecioCosto.Name = "txtPrecioCosto"
        Me.txtPrecioCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioCosto.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioCosto.TabIndex = 7
        Me.txtPrecioCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(94, 211)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.ReadOnly = True
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(56, 20)
        Me.txtCantidad.TabIndex = 6
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(94, 170)
        Me.txtDescripcion.MaxLength = 200
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(319, 35)
        Me.txtDescripcion.TabIndex = 5
        '
        'txtCodigo
        '
        Me.txtCodigo.AcceptsReturn = True
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigo.Location = New System.Drawing.Point(94, 118)
        Me.txtCodigo.MaxLength = 50
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigo.Size = New System.Drawing.Size(203, 20)
        Me.txtCodigo.TabIndex = 3
        '
        'txtIdProducto
        '
        Me.txtIdProducto.AcceptsReturn = True
        Me.txtIdProducto.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdProducto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdProducto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdProducto.Location = New System.Drawing.Point(94, 38)
        Me.txtIdProducto.MaxLength = 0
        Me.txtIdProducto.Name = "txtIdProducto"
        Me.txtIdProducto.ReadOnly = True
        Me.txtIdProducto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdProducto.Size = New System.Drawing.Size(66, 20)
        Me.txtIdProducto.TabIndex = 0
        Me.txtIdProducto.TabStop = False
        '
        'lblPrecioVenta1
        '
        Me.lblPrecioVenta1.BackColor = System.Drawing.Color.Transparent
        Me.lblPrecioVenta1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblPrecioVenta1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblPrecioVenta1.Location = New System.Drawing.Point(5, 264)
        Me.lblPrecioVenta1.Name = "lblPrecioVenta1"
        Me.lblPrecioVenta1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrecioVenta1.Size = New System.Drawing.Size(83, 17)
        Me.lblPrecioVenta1.TabIndex = 0
        Me.lblPrecioVenta1.Text = "Precio Venta 1:"
        Me.lblPrecioVenta1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_12
        '
        Me._lblLabels_12.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_12.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_12.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_12.Location = New System.Drawing.Point(5, 440)
        Me._lblLabels_12.Name = "_lblLabels_12"
        Me._lblLabels_12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_12.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_12.TabIndex = 0
        Me._lblLabels_12.Text = "Unidad:"
        Me._lblLabels_12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(5, 414)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_11.TabIndex = 0
        Me._lblLabels_11.Text = "Punto Reorden:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_10
        '
        Me._lblLabels_10.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_10.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_10.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_10.Location = New System.Drawing.Point(32, 391)
        Me._lblLabels_10.Name = "_lblLabels_10"
        Me._lblLabels_10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_10.Size = New System.Drawing.Size(56, 17)
        Me._lblLabels_10.TabIndex = 0
        Me._lblLabels_10.Text = "Excento:"
        Me._lblLabels_10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_9.Location = New System.Drawing.Point(5, 65)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_9.TabIndex = 0
        Me._lblLabels_9.Text = "Tipo:"
        Me._lblLabels_9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(5, 238)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_6.TabIndex = 0
        Me._lblLabels_6.Text = "Precio Costo:"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_5.Location = New System.Drawing.Point(5, 212)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_5.TabIndex = 0
        Me._lblLabels_5.Text = "Existencia:"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_4.Location = New System.Drawing.Point(5, 171)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_4.TabIndex = 0
        Me._lblLabels_4.Text = "Descripción:"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(5, 145)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_3.TabIndex = 0
        Me._lblLabels_3.Text = "Proveedor:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(5, 119)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_2.TabIndex = 0
        Me._lblLabels_2.Text = "Codigo:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(5, 92)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_1.TabIndex = 0
        Me._lblLabels_1.Text = "Linea:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(5, 39)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_0.TabIndex = 0
        Me._lblLabels_0.Text = "Producto No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboTipoProducto
        '
        Me.cboTipoProducto.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboTipoProducto.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoProducto.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoProducto.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoProducto.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoProducto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoProducto.Location = New System.Drawing.Point(94, 64)
        Me.cboTipoProducto.Name = "cboTipoProducto"
        Me.cboTipoProducto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoProducto.Size = New System.Drawing.Size(106, 21)
        Me.cboTipoProducto.TabIndex = 1
        '
        'ptbImagen
        '
        Me.ptbImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ptbImagen.Location = New System.Drawing.Point(424, 38)
        Me.ptbImagen.Name = "ptbImagen"
        Me.ptbImagen.Size = New System.Drawing.Size(321, 292)
        Me.ptbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ptbImagen.TabIndex = 0
        Me.ptbImagen.TabStop = False
        '
        'btnCargarImagen
        '
        Me.btnCargarImagen.Image = CType(resources.GetObject("btnCargarImagen.Image"), System.Drawing.Image)
        Me.btnCargarImagen.Location = New System.Drawing.Point(679, 336)
        Me.btnCargarImagen.Name = "btnCargarImagen"
        Me.btnCargarImagen.Size = New System.Drawing.Size(30, 28)
        Me.btnCargarImagen.TabIndex = 16
        Me.btnCargarImagen.TabStop = False
        Me.btnCargarImagen.UseVisualStyleBackColor = True
        '
        'ofdAbrirImagen
        '
        Me.ofdAbrirImagen.FileName = "ofdAbrirImagen"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(537, 18)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(106, 17)
        Me.Label1.TabIndex = 0
        Me.Label1.Text = "Imagen del Producto"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnEliminarImagen
        '
        Me.btnEliminarImagen.Image = CType(resources.GetObject("btnEliminarImagen.Image"), System.Drawing.Image)
        Me.btnEliminarImagen.Location = New System.Drawing.Point(715, 336)
        Me.btnEliminarImagen.Name = "btnEliminarImagen"
        Me.btnEliminarImagen.Size = New System.Drawing.Size(30, 28)
        Me.btnEliminarImagen.TabIndex = 17
        Me.btnEliminarImagen.TabStop = False
        Me.btnEliminarImagen.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(94, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 52
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(10, 10)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(78, 22)
        Me.btnGuardar.TabIndex = 51
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'chkExcento
        '
        Me.chkExcento.AutoSize = True
        Me.chkExcento.Location = New System.Drawing.Point(94, 393)
        Me.chkExcento.Name = "chkExcento"
        Me.chkExcento.Size = New System.Drawing.Size(15, 14)
        Me.chkExcento.TabIndex = 13
        Me.chkExcento.UseVisualStyleBackColor = True
        '
        'btnBuscarProveedor
        '
        Me.btnBuscarProveedor.Image = CType(resources.GetObject("btnBuscarProveedor.Image"), System.Drawing.Image)
        Me.btnBuscarProveedor.Location = New System.Drawing.Point(393, 142)
        Me.btnBuscarProveedor.Name = "btnBuscarProveedor"
        Me.btnBuscarProveedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarProveedor.TabIndex = 136
        Me.btnBuscarProveedor.TabStop = False
        Me.btnBuscarProveedor.UseVisualStyleBackColor = True
        '
        'txtProveedor
        '
        Me.txtProveedor.AcceptsReturn = True
        Me.txtProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtProveedor.Location = New System.Drawing.Point(94, 144)
        Me.txtProveedor.MaxLength = 0
        Me.txtProveedor.Name = "txtProveedor"
        Me.txtProveedor.ReadOnly = True
        Me.txtProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtProveedor.Size = New System.Drawing.Size(293, 20)
        Me.txtProveedor.TabIndex = 4
        '
        'txtPrecioVenta2
        '
        Me.txtPrecioVenta2.AcceptsReturn = True
        Me.txtPrecioVenta2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta2.Location = New System.Drawing.Point(94, 289)
        Me.txtPrecioVenta2.MaxLength = 0
        Me.txtPrecioVenta2.Name = "txtPrecioVenta2"
        Me.txtPrecioVenta2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta2.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta2.TabIndex = 9
        Me.txtPrecioVenta2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(0, 290)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(88, 17)
        Me.Label2.TabIndex = 137
        Me.Label2.Text = "Precio Venta 2:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrecioVenta3
        '
        Me.txtPrecioVenta3.AcceptsReturn = True
        Me.txtPrecioVenta3.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta3.Location = New System.Drawing.Point(94, 315)
        Me.txtPrecioVenta3.MaxLength = 0
        Me.txtPrecioVenta3.Name = "txtPrecioVenta3"
        Me.txtPrecioVenta3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta3.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta3.TabIndex = 10
        Me.txtPrecioVenta3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(0, 316)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(88, 17)
        Me.Label3.TabIndex = 139
        Me.Label3.Text = "Precio Venta 3:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrecioVenta4
        '
        Me.txtPrecioVenta4.AcceptsReturn = True
        Me.txtPrecioVenta4.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta4.Location = New System.Drawing.Point(94, 341)
        Me.txtPrecioVenta4.MaxLength = 0
        Me.txtPrecioVenta4.Name = "txtPrecioVenta4"
        Me.txtPrecioVenta4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta4.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta4.TabIndex = 11
        Me.txtPrecioVenta4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(0, 342)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(88, 17)
        Me.Label4.TabIndex = 141
        Me.Label4.Text = "Precio Venta 4:"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrecioVenta5
        '
        Me.txtPrecioVenta5.AcceptsReturn = True
        Me.txtPrecioVenta5.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta5.Location = New System.Drawing.Point(94, 367)
        Me.txtPrecioVenta5.MaxLength = 0
        Me.txtPrecioVenta5.Name = "txtPrecioVenta5"
        Me.txtPrecioVenta5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta5.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta5.TabIndex = 12
        Me.txtPrecioVenta5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(0, 368)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(88, 17)
        Me.Label5.TabIndex = 143
        Me.Label5.Text = "Precio Venta 5:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(752, 475)
        Me.Controls.Add(Me.txtPrecioVenta5)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPrecioVenta4)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPrecioVenta3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtPrecioVenta2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnBuscarProveedor)
        Me.Controls.Add(Me.txtProveedor)
        Me.Controls.Add(Me.chkExcento)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminarImagen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCargarImagen)
        Me.Controls.Add(Me.ptbImagen)
        Me.Controls.Add(Me.cboTipoProducto)
        Me.Controls.Add(Me.txtPrecioVenta1)
        Me.Controls.Add(Me.cboUnidad)
        Me.Controls.Add(Me.CboLinea)
        Me.Controls.Add(Me.txtIndExistencia)
        Me.Controls.Add(Me.txtPrecioCosto)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.txtIdProducto)
        Me.Controls.Add(Me.lblPrecioVenta1)
        Me.Controls.Add(Me._lblLabels_12)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_10)
        Me.Controls.Add(Me._lblLabels_9)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_5)
        Me.Controls.Add(Me._lblLabels_4)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProducto"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        CType(Me.ptbImagen, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents cboTipoProducto As System.Windows.Forms.ComboBox
    Friend WithEvents ptbImagen As System.Windows.Forms.PictureBox
    Friend WithEvents btnCargarImagen As System.Windows.Forms.Button
    Friend WithEvents ofdAbrirImagen As System.Windows.Forms.OpenFileDialog
    Public WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents btnEliminarImagen As System.Windows.Forms.Button
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Friend WithEvents chkExcento As System.Windows.Forms.CheckBox
    Friend WithEvents btnBuscarProveedor As System.Windows.Forms.Button
    Public WithEvents txtProveedor As System.Windows.Forms.TextBox
    Public WithEvents txtPrecioVenta2 As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtPrecioVenta3 As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtPrecioVenta4 As TextBox
    Public WithEvents Label4 As Label
    Public WithEvents txtPrecioVenta5 As TextBox
    Public WithEvents Label5 As Label
End Class