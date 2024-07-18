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
    Public WithEvents cboLinea As System.Windows.Forms.ComboBox
    Public WithEvents txtIndExistencia As System.Windows.Forms.TextBox
    Public WithEvents txtPrecioCosto As System.Windows.Forms.TextBox
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents txtIdProducto As System.Windows.Forms.TextBox
    Public WithEvents lblPrecioVenta1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_4 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProducto))
        Me.txtPrecioVenta1 = New System.Windows.Forms.TextBox()
        Me.cboLinea = New System.Windows.Forms.ComboBox()
        Me.txtIndExistencia = New System.Windows.Forms.TextBox()
        Me.txtPrecioCosto = New System.Windows.Forms.TextBox()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.txtIdProducto = New System.Windows.Forms.TextBox()
        Me.lblPrecioVenta1 = New System.Windows.Forms.Label()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_4 = New System.Windows.Forms.Label()
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
        Me.txtPrecioVenta2 = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta3 = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta4 = New System.Windows.Forms.TextBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtPrecioVenta5 = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.cboTipoImpuesto = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPrecioImpuesto5 = New System.Windows.Forms.TextBox()
        Me.txtPrecioImpuesto4 = New System.Windows.Forms.TextBox()
        Me.txtPrecioImpuesto3 = New System.Windows.Forms.TextBox()
        Me.txtPrecioImpuesto2 = New System.Windows.Forms.TextBox()
        Me.txtPrecioImpuesto1 = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtCodigoProveedor = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtPorcUtilidad = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtObservacion = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.chkActivo = New System.Windows.Forms.CheckBox()
        Me.txtMarca = New System.Windows.Forms.TextBox()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.chkModificaPrecio = New System.Windows.Forms.CheckBox()
        Me.txtPorcDescuento = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.txtCodigoClasificacion = New System.Windows.Forms.TextBox()
        Me.Label13 = New System.Windows.Forms.Label()
        Me.btnBuscarClasificacion = New System.Windows.Forms.Button()
        CType(Me.ptbImagen, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtPrecioVenta1
        '
        Me.txtPrecioVenta1.AcceptsReturn = True
        Me.txtPrecioVenta1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta1.Location = New System.Drawing.Point(121, 318)
        Me.txtPrecioVenta1.MaxLength = 0
        Me.txtPrecioVenta1.Name = "txtPrecioVenta1"
        Me.txtPrecioVenta1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta1.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta1.TabIndex = 11
        Me.txtPrecioVenta1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboLinea
        '
        Me.cboLinea.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboLinea.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboLinea.BackColor = System.Drawing.SystemColors.Window
        Me.cboLinea.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboLinea.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboLinea.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboLinea.Location = New System.Drawing.Point(121, 91)
        Me.cboLinea.Name = "cboLinea"
        Me.cboLinea.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboLinea.Size = New System.Drawing.Size(319, 21)
        Me.cboLinea.TabIndex = 2
        '
        'txtIndExistencia
        '
        Me.txtIndExistencia.AcceptsReturn = True
        Me.txtIndExistencia.BackColor = System.Drawing.SystemColors.Window
        Me.txtIndExistencia.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIndExistencia.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIndExistencia.Location = New System.Drawing.Point(121, 448)
        Me.txtIndExistencia.MaxLength = 0
        Me.txtIndExistencia.Name = "txtIndExistencia"
        Me.txtIndExistencia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIndExistencia.Size = New System.Drawing.Size(45, 20)
        Me.txtIndExistencia.TabIndex = 16
        '
        'txtPrecioCosto
        '
        Me.txtPrecioCosto.AcceptsReturn = True
        Me.txtPrecioCosto.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioCosto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioCosto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioCosto.Location = New System.Drawing.Point(121, 264)
        Me.txtPrecioCosto.MaxLength = 0
        Me.txtPrecioCosto.Name = "txtPrecioCosto"
        Me.txtPrecioCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioCosto.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioCosto.TabIndex = 9
        Me.txtPrecioCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(121, 196)
        Me.txtDescripcion.MaxLength = 200
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(319, 35)
        Me.txtDescripcion.TabIndex = 7
        '
        'txtCodigo
        '
        Me.txtCodigo.AcceptsReturn = True
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigo.Location = New System.Drawing.Point(121, 118)
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
        Me.txtIdProducto.Location = New System.Drawing.Point(121, 38)
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
        Me.lblPrecioVenta1.Location = New System.Drawing.Point(32, 319)
        Me.lblPrecioVenta1.Name = "lblPrecioVenta1"
        Me.lblPrecioVenta1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblPrecioVenta1.Size = New System.Drawing.Size(83, 17)
        Me.lblPrecioVenta1.TabIndex = 0
        Me.lblPrecioVenta1.Text = "Precio Venta 1:"
        Me.lblPrecioVenta1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(32, 449)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_11.TabIndex = 0
        Me._lblLabels_11.Text = "Punto Reorden:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_9.Location = New System.Drawing.Point(32, 65)
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
        Me._lblLabels_6.Location = New System.Drawing.Point(32, 265)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_6.TabIndex = 0
        Me._lblLabels_6.Text = "Precio Costo:"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_4
        '
        Me._lblLabels_4.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_4.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_4.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_4.Location = New System.Drawing.Point(32, 197)
        Me._lblLabels_4.Name = "_lblLabels_4"
        Me._lblLabels_4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_4.Size = New System.Drawing.Size(83, 17)
        Me._lblLabels_4.TabIndex = 0
        Me._lblLabels_4.Text = "Descripción:"
        Me._lblLabels_4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(32, 119)
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
        Me._lblLabels_1.Location = New System.Drawing.Point(32, 92)
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
        Me._lblLabels_0.Location = New System.Drawing.Point(32, 39)
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
        Me.cboTipoProducto.Location = New System.Drawing.Point(121, 64)
        Me.cboTipoProducto.Name = "cboTipoProducto"
        Me.cboTipoProducto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoProducto.Size = New System.Drawing.Size(203, 21)
        Me.cboTipoProducto.TabIndex = 1
        '
        'ptbImagen
        '
        Me.ptbImagen.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.ptbImagen.Location = New System.Drawing.Point(451, 38)
        Me.ptbImagen.Name = "ptbImagen"
        Me.ptbImagen.Size = New System.Drawing.Size(321, 292)
        Me.ptbImagen.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.ptbImagen.TabIndex = 0
        Me.ptbImagen.TabStop = False
        '
        'btnCargarImagen
        '
        Me.btnCargarImagen.Image = CType(resources.GetObject("btnCargarImagen.Image"), System.Drawing.Image)
        Me.btnCargarImagen.Location = New System.Drawing.Point(706, 336)
        Me.btnCargarImagen.Name = "btnCargarImagen"
        Me.btnCargarImagen.Size = New System.Drawing.Size(30, 28)
        Me.btnCargarImagen.TabIndex = 40
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
        Me.btnEliminarImagen.Location = New System.Drawing.Point(742, 336)
        Me.btnEliminarImagen.Name = "btnEliminarImagen"
        Me.btnEliminarImagen.Size = New System.Drawing.Size(30, 28)
        Me.btnEliminarImagen.TabIndex = 41
        Me.btnEliminarImagen.TabStop = False
        Me.btnEliminarImagen.UseVisualStyleBackColor = True
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(94, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 52
        Me.btnCancelar.TabStop = False
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(10, 10)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(78, 22)
        Me.btnGuardar.TabIndex = 51
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtPrecioVenta2
        '
        Me.txtPrecioVenta2.AcceptsReturn = True
        Me.txtPrecioVenta2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioVenta2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioVenta2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioVenta2.Location = New System.Drawing.Point(121, 344)
        Me.txtPrecioVenta2.MaxLength = 0
        Me.txtPrecioVenta2.Name = "txtPrecioVenta2"
        Me.txtPrecioVenta2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta2.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta2.TabIndex = 12
        Me.txtPrecioVenta2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(27, 345)
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
        Me.txtPrecioVenta3.Location = New System.Drawing.Point(121, 370)
        Me.txtPrecioVenta3.MaxLength = 0
        Me.txtPrecioVenta3.Name = "txtPrecioVenta3"
        Me.txtPrecioVenta3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta3.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta3.TabIndex = 13
        Me.txtPrecioVenta3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(27, 371)
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
        Me.txtPrecioVenta4.Location = New System.Drawing.Point(121, 396)
        Me.txtPrecioVenta4.MaxLength = 0
        Me.txtPrecioVenta4.Name = "txtPrecioVenta4"
        Me.txtPrecioVenta4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta4.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta4.TabIndex = 14
        Me.txtPrecioVenta4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(27, 397)
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
        Me.txtPrecioVenta5.Location = New System.Drawing.Point(121, 422)
        Me.txtPrecioVenta5.MaxLength = 0
        Me.txtPrecioVenta5.Name = "txtPrecioVenta5"
        Me.txtPrecioVenta5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioVenta5.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioVenta5.TabIndex = 15
        Me.txtPrecioVenta5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(27, 423)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(88, 17)
        Me.Label5.TabIndex = 143
        Me.Label5.Text = "Precio Venta 5:"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
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
        Me.cboTipoImpuesto.Location = New System.Drawing.Point(121, 237)
        Me.cboTipoImpuesto.Name = "cboTipoImpuesto"
        Me.cboTipoImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoImpuesto.Size = New System.Drawing.Size(319, 21)
        Me.cboTipoImpuesto.TabIndex = 8
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(32, 238)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(83, 17)
        Me.Label6.TabIndex = 144
        Me.Label6.Text = "Tipo Impuesto:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPrecioImpuesto5
        '
        Me.txtPrecioImpuesto5.AcceptsReturn = True
        Me.txtPrecioImpuesto5.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioImpuesto5.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioImpuesto5.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioImpuesto5.Location = New System.Drawing.Point(245, 422)
        Me.txtPrecioImpuesto5.MaxLength = 0
        Me.txtPrecioImpuesto5.Name = "txtPrecioImpuesto5"
        Me.txtPrecioImpuesto5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioImpuesto5.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioImpuesto5.TabIndex = 34
        Me.txtPrecioImpuesto5.TabStop = False
        Me.txtPrecioImpuesto5.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrecioImpuesto4
        '
        Me.txtPrecioImpuesto4.AcceptsReturn = True
        Me.txtPrecioImpuesto4.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioImpuesto4.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioImpuesto4.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioImpuesto4.Location = New System.Drawing.Point(245, 396)
        Me.txtPrecioImpuesto4.MaxLength = 0
        Me.txtPrecioImpuesto4.Name = "txtPrecioImpuesto4"
        Me.txtPrecioImpuesto4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioImpuesto4.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioImpuesto4.TabIndex = 33
        Me.txtPrecioImpuesto4.TabStop = False
        Me.txtPrecioImpuesto4.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrecioImpuesto3
        '
        Me.txtPrecioImpuesto3.AcceptsReturn = True
        Me.txtPrecioImpuesto3.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioImpuesto3.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioImpuesto3.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioImpuesto3.Location = New System.Drawing.Point(245, 370)
        Me.txtPrecioImpuesto3.MaxLength = 0
        Me.txtPrecioImpuesto3.Name = "txtPrecioImpuesto3"
        Me.txtPrecioImpuesto3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioImpuesto3.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioImpuesto3.TabIndex = 32
        Me.txtPrecioImpuesto3.TabStop = False
        Me.txtPrecioImpuesto3.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrecioImpuesto2
        '
        Me.txtPrecioImpuesto2.AcceptsReturn = True
        Me.txtPrecioImpuesto2.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioImpuesto2.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioImpuesto2.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioImpuesto2.Location = New System.Drawing.Point(245, 344)
        Me.txtPrecioImpuesto2.MaxLength = 0
        Me.txtPrecioImpuesto2.Name = "txtPrecioImpuesto2"
        Me.txtPrecioImpuesto2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioImpuesto2.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioImpuesto2.TabIndex = 31
        Me.txtPrecioImpuesto2.TabStop = False
        Me.txtPrecioImpuesto2.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtPrecioImpuesto1
        '
        Me.txtPrecioImpuesto1.AcceptsReturn = True
        Me.txtPrecioImpuesto1.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioImpuesto1.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioImpuesto1.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioImpuesto1.Location = New System.Drawing.Point(245, 318)
        Me.txtPrecioImpuesto1.MaxLength = 0
        Me.txtPrecioImpuesto1.Name = "txtPrecioImpuesto1"
        Me.txtPrecioImpuesto1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioImpuesto1.Size = New System.Drawing.Size(106, 20)
        Me.txtPrecioImpuesto1.TabIndex = 30
        Me.txtPrecioImpuesto1.TabStop = False
        Me.txtPrecioImpuesto1.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(243, 291)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(106, 17)
        Me.Label7.TabIndex = 150
        Me.Label7.Text = "Con Impuesto"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCodigoProveedor
        '
        Me.txtCodigoProveedor.AcceptsReturn = True
        Me.txtCodigoProveedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoProveedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoProveedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoProveedor.Location = New System.Drawing.Point(121, 144)
        Me.txtCodigoProveedor.MaxLength = 50
        Me.txtCodigoProveedor.Name = "txtCodigoProveedor"
        Me.txtCodigoProveedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoProveedor.Size = New System.Drawing.Size(203, 20)
        Me.txtCodigoProveedor.TabIndex = 4
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(10, 145)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(105, 17)
        Me.Label8.TabIndex = 151
        Me.Label8.Text = "Codigo proveedor:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPorcUtilidad
        '
        Me.txtPorcUtilidad.AcceptsReturn = True
        Me.txtPorcUtilidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcUtilidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcUtilidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcUtilidad.Location = New System.Drawing.Point(121, 290)
        Me.txtPorcUtilidad.MaxLength = 0
        Me.txtPorcUtilidad.Name = "txtPorcUtilidad"
        Me.txtPorcUtilidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcUtilidad.Size = New System.Drawing.Size(106, 20)
        Me.txtPorcUtilidad.TabIndex = 10
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(12, 291)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(103, 17)
        Me.Label9.TabIndex = 152
        Me.Label9.Text = "Porcentaje utilidad:"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtObservacion
        '
        Me.txtObservacion.AcceptsReturn = True
        Me.txtObservacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtObservacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtObservacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtObservacion.Location = New System.Drawing.Point(121, 500)
        Me.txtObservacion.MaxLength = 200
        Me.txtObservacion.Name = "txtObservacion"
        Me.txtObservacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtObservacion.Size = New System.Drawing.Size(651, 20)
        Me.txtObservacion.TabIndex = 19
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(32, 501)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(83, 17)
        Me.Label10.TabIndex = 153
        Me.Label10.Text = "Observaciones:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkActivo
        '
        Me.chkActivo.AutoSize = True
        Me.chkActivo.Location = New System.Drawing.Point(121, 526)
        Me.chkActivo.Name = "chkActivo"
        Me.chkActivo.Size = New System.Drawing.Size(101, 17)
        Me.chkActivo.TabIndex = 20
        Me.chkActivo.TabStop = False
        Me.chkActivo.Text = "Producto activo"
        Me.chkActivo.UseVisualStyleBackColor = True
        '
        'txtMarca
        '
        Me.txtMarca.AcceptsReturn = True
        Me.txtMarca.BackColor = System.Drawing.SystemColors.Window
        Me.txtMarca.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMarca.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMarca.Location = New System.Drawing.Point(121, 474)
        Me.txtMarca.MaxLength = 50
        Me.txtMarca.Name = "txtMarca"
        Me.txtMarca.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMarca.Size = New System.Drawing.Size(230, 20)
        Me.txtMarca.TabIndex = 18
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(32, 475)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(83, 17)
        Me.Label11.TabIndex = 155
        Me.Label11.Text = "Marca:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'chkModificaPrecio
        '
        Me.chkModificaPrecio.AutoSize = True
        Me.chkModificaPrecio.Location = New System.Drawing.Point(245, 526)
        Me.chkModificaPrecio.Name = "chkModificaPrecio"
        Me.chkModificaPrecio.Size = New System.Drawing.Size(183, 17)
        Me.chkModificaPrecio.TabIndex = 21
        Me.chkModificaPrecio.TabStop = False
        Me.chkModificaPrecio.Text = "Permite modificar precio de venta"
        Me.chkModificaPrecio.UseVisualStyleBackColor = True
        '
        'txtPorcDescuento
        '
        Me.txtPorcDescuento.AcceptsReturn = True
        Me.txtPorcDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcDescuento.Location = New System.Drawing.Point(299, 448)
        Me.txtPorcDescuento.MaxLength = 0
        Me.txtPorcDescuento.Name = "txtPorcDescuento"
        Me.txtPorcDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcDescuento.Size = New System.Drawing.Size(52, 20)
        Me.txtPorcDescuento.TabIndex = 17
        Me.txtPorcDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(198, 449)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(95, 17)
        Me.Label12.TabIndex = 156
        Me.Label12.Text = "Porc. Descuento:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigoClasificacion
        '
        Me.txtCodigoClasificacion.AcceptsReturn = True
        Me.txtCodigoClasificacion.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigoClasificacion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigoClasificacion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigoClasificacion.Location = New System.Drawing.Point(121, 170)
        Me.txtCodigoClasificacion.MaxLength = 20
        Me.txtCodigoClasificacion.Name = "txtCodigoClasificacion"
        Me.txtCodigoClasificacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigoClasificacion.Size = New System.Drawing.Size(203, 20)
        Me.txtCodigoClasificacion.TabIndex = 5
        '
        'Label13
        '
        Me.Label13.BackColor = System.Drawing.Color.Transparent
        Me.Label13.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label13.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label13.Location = New System.Drawing.Point(10, 171)
        Me.Label13.Name = "Label13"
        Me.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label13.Size = New System.Drawing.Size(105, 17)
        Me.Label13.TabIndex = 158
        Me.Label13.Text = "Codigo CABYS:"
        Me.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBuscarClasificacion
        '
        Me.btnBuscarClasificacion.Image = CType(resources.GetObject("btnBuscarClasificacion.Image"), System.Drawing.Image)
        Me.btnBuscarClasificacion.Location = New System.Drawing.Point(330, 169)
        Me.btnBuscarClasificacion.Name = "btnBuscarClasificacion"
        Me.btnBuscarClasificacion.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarClasificacion.TabIndex = 159
        Me.btnBuscarClasificacion.TabStop = False
        Me.btnBuscarClasificacion.UseVisualStyleBackColor = True
        '
        'FrmProducto
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(785, 553)
        Me.Controls.Add(Me.btnBuscarClasificacion)
        Me.Controls.Add(Me.txtCodigoClasificacion)
        Me.Controls.Add(Me.Label13)
        Me.Controls.Add(Me.txtPorcDescuento)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.chkModificaPrecio)
        Me.Controls.Add(Me.txtMarca)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.chkActivo)
        Me.Controls.Add(Me.txtObservacion)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtPorcUtilidad)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtCodigoProveedor)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtPrecioImpuesto5)
        Me.Controls.Add(Me.txtPrecioImpuesto4)
        Me.Controls.Add(Me.txtPrecioImpuesto3)
        Me.Controls.Add(Me.txtPrecioImpuesto2)
        Me.Controls.Add(Me.txtPrecioImpuesto1)
        Me.Controls.Add(Me.cboTipoImpuesto)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPrecioVenta5)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtPrecioVenta4)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtPrecioVenta3)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtPrecioVenta2)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.btnEliminarImagen)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnCargarImagen)
        Me.Controls.Add(Me.ptbImagen)
        Me.Controls.Add(Me.cboTipoProducto)
        Me.Controls.Add(Me.txtPrecioVenta1)
        Me.Controls.Add(Me.cboLinea)
        Me.Controls.Add(Me.txtIndExistencia)
        Me.Controls.Add(Me.txtPrecioCosto)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.txtIdProducto)
        Me.Controls.Add(Me.lblPrecioVenta1)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_9)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_4)
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
    Public WithEvents txtPrecioVenta2 As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtPrecioVenta3 As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtPrecioVenta4 As TextBox
    Public WithEvents Label4 As Label
    Public WithEvents txtPrecioVenta5 As TextBox
    Public WithEvents Label5 As Label
    Public WithEvents cboTipoImpuesto As ComboBox
    Public WithEvents Label6 As Label
    Public WithEvents txtPrecioImpuesto5 As TextBox
    Public WithEvents txtPrecioImpuesto4 As TextBox
    Public WithEvents txtPrecioImpuesto3 As TextBox
    Public WithEvents txtPrecioImpuesto2 As TextBox
    Public WithEvents txtPrecioImpuesto1 As TextBox
    Public WithEvents Label7 As Label
    Public WithEvents txtCodigoProveedor As TextBox
    Public WithEvents Label8 As Label
    Public WithEvents txtPorcUtilidad As TextBox
    Public WithEvents Label9 As Label
    Public WithEvents txtObservacion As TextBox
    Public WithEvents Label10 As Label
    Friend WithEvents chkActivo As CheckBox
    Public WithEvents txtMarca As TextBox
    Public WithEvents Label11 As Label
    Friend WithEvents chkModificaPrecio As CheckBox
    Public WithEvents txtPorcDescuento As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents txtCodigoClasificacion As TextBox
    Public WithEvents Label13 As Label
    Friend WithEvents btnBuscarClasificacion As Button
End Class