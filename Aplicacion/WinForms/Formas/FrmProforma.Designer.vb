<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProforma
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
    Public WithEvents cmdBusProd As System.Windows.Forms.Button
    Public WithEvents CmdAnular As System.Windows.Forms.Button
    Public WithEvents CmdAgregar As System.Windows.Forms.Button
    Public WithEvents CmdBuscar As System.Windows.Forms.Button
    Public WithEvents CmdImprimir As System.Windows.Forms.Button
    Public WithEvents CmdGuardar As System.Windows.Forms.Button
    Public WithEvents cmdEliminar As System.Windows.Forms.Button
    Public WithEvents cmdInsertar As System.Windows.Forms.Button
    Public WithEvents txtImpuesto As System.Windows.Forms.TextBox
    Public WithEvents txtIdProforma As System.Windows.Forms.TextBox
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents txtDescuento As System.Windows.Forms.TextBox
    Public WithEvents txtSubTotal As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents _LblImpuesto_0 As System.Windows.Forms.Label
    Public WithEvents _LblTotal_6 As System.Windows.Forms.Label
    Public WithEvents _LblDescuento_1 As System.Windows.Forms.Label
    Public WithEvents _lblSubTotal_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents grdDetalleProforma As System.Windows.Forms.DataGridView
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmProforma))
        Me.cmdBusProd = New System.Windows.Forms.Button()
        Me.CmdAnular = New System.Windows.Forms.Button()
        Me.CmdAgregar = New System.Windows.Forms.Button()
        Me.CmdBuscar = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.CmdGuardar = New System.Windows.Forms.Button()
        Me.cmdEliminar = New System.Windows.Forms.Button()
        Me.cmdInsertar = New System.Windows.Forms.Button()
        Me.txtImpuesto = New System.Windows.Forms.TextBox()
        Me.txtIdProforma = New System.Windows.Forms.TextBox()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.txtDescuento = New System.Windows.Forms.TextBox()
        Me.txtSubTotal = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._LblImpuesto_0 = New System.Windows.Forms.Label()
        Me._LblTotal_6 = New System.Windows.Forms.Label()
        Me._LblDescuento_1 = New System.Windows.Forms.Label()
        Me._lblSubTotal_5 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.grdDetalleProforma = New System.Windows.Forms.DataGridView()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        Me.txtNombreCliente = New System.Windows.Forms.TextBox()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtUnidad = New System.Windows.Forms.TextBox()
        Me.txtPrecio = New System.Windows.Forms.TextBox()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me._lblLabels_8 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.txtDocumento = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me.txtVendedor = New System.Windows.Forms.TextBox()
        Me.btnBuscaVendedor = New System.Windows.Forms.Button()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtPlazoCredito = New System.Windows.Forms.TextBox()
        Me.Label12 = New System.Windows.Forms.Label()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.cboIdCondicionVenta = New System.Windows.Forms.ComboBox()
        CType(Me.grdDetalleProforma, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'cmdBusProd
        '
        Me.cmdBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdBusProd.Location = New System.Drawing.Point(168, 398)
        Me.cmdBusProd.Name = "cmdBusProd"
        Me.cmdBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdBusProd.Size = New System.Drawing.Size(73, 25)
        Me.cmdBusProd.TabIndex = 28
        Me.cmdBusProd.TabStop = False
        Me.cmdBusProd.Text = "&Buscar"
        Me.cmdBusProd.UseVisualStyleBackColor = False
        '
        'CmdAnular
        '
        Me.CmdAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAnular.Enabled = False
        Me.CmdAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAnular.Location = New System.Drawing.Point(200, 8)
        Me.CmdAnular.Name = "CmdAnular"
        Me.CmdAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAnular.Size = New System.Drawing.Size(64, 21)
        Me.CmdAnular.TabIndex = 33
        Me.CmdAnular.TabStop = False
        Me.CmdAnular.Text = "&Anular"
        Me.CmdAnular.UseVisualStyleBackColor = False
        '
        'CmdAgregar
        '
        Me.CmdAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAgregar.Location = New System.Drawing.Point(264, 8)
        Me.CmdAgregar.Name = "CmdAgregar"
        Me.CmdAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAgregar.Size = New System.Drawing.Size(64, 21)
        Me.CmdAgregar.TabIndex = 34
        Me.CmdAgregar.TabStop = False
        Me.CmdAgregar.Text = "&Nuevo"
        Me.CmdAgregar.UseVisualStyleBackColor = False
        '
        'CmdBuscar
        '
        Me.CmdBuscar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdBuscar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdBuscar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdBuscar.Location = New System.Drawing.Point(136, 8)
        Me.CmdBuscar.Name = "CmdBuscar"
        Me.CmdBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdBuscar.Size = New System.Drawing.Size(64, 21)
        Me.CmdBuscar.TabIndex = 32
        Me.CmdBuscar.TabStop = False
        Me.CmdBuscar.Text = "B&uscar"
        Me.CmdBuscar.UseVisualStyleBackColor = False
        '
        'CmdImprimir
        '
        Me.CmdImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdImprimir.Enabled = False
        Me.CmdImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdImprimir.Location = New System.Drawing.Point(72, 8)
        Me.CmdImprimir.Name = "CmdImprimir"
        Me.CmdImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdImprimir.Size = New System.Drawing.Size(64, 21)
        Me.CmdImprimir.TabIndex = 31
        Me.CmdImprimir.TabStop = False
        Me.CmdImprimir.Text = "&Imprimir"
        Me.CmdImprimir.UseVisualStyleBackColor = False
        '
        'CmdGuardar
        '
        Me.CmdGuardar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdGuardar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdGuardar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdGuardar.Location = New System.Drawing.Point(8, 8)
        Me.CmdGuardar.Name = "CmdGuardar"
        Me.CmdGuardar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdGuardar.Size = New System.Drawing.Size(64, 21)
        Me.CmdGuardar.TabIndex = 30
        Me.CmdGuardar.TabStop = False
        Me.CmdGuardar.Text = "&Guardar"
        Me.CmdGuardar.UseVisualStyleBackColor = False
        '
        'cmdEliminar
        '
        Me.cmdEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEliminar.Location = New System.Drawing.Point(88, 398)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEliminar.Size = New System.Drawing.Size(73, 25)
        Me.cmdEliminar.TabIndex = 27
        Me.cmdEliminar.TabStop = False
        Me.cmdEliminar.Text = "&Eliminar"
        Me.cmdEliminar.UseVisualStyleBackColor = False
        '
        'cmdInsertar
        '
        Me.cmdInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInsertar.Location = New System.Drawing.Point(8, 398)
        Me.cmdInsertar.Name = "cmdInsertar"
        Me.cmdInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInsertar.Size = New System.Drawing.Size(73, 25)
        Me.cmdInsertar.TabIndex = 26
        Me.cmdInsertar.TabStop = False
        Me.cmdInsertar.Text = "Insertar"
        Me.cmdInsertar.UseVisualStyleBackColor = False
        '
        'txtImpuesto
        '
        Me.txtImpuesto.AcceptsReturn = True
        Me.txtImpuesto.BackColor = System.Drawing.SystemColors.Window
        Me.txtImpuesto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtImpuesto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtImpuesto.Location = New System.Drawing.Point(735, 446)
        Me.txtImpuesto.MaxLength = 0
        Me.txtImpuesto.Name = "txtImpuesto"
        Me.txtImpuesto.ReadOnly = True
        Me.txtImpuesto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtImpuesto.Size = New System.Drawing.Size(73, 20)
        Me.txtImpuesto.TabIndex = 31
        Me.txtImpuesto.TabStop = False
        Me.txtImpuesto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtIdProforma
        '
        Me.txtIdProforma.AcceptsReturn = True
        Me.txtIdProforma.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdProforma.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdProforma.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdProforma.Location = New System.Drawing.Point(105, 36)
        Me.txtIdProforma.MaxLength = 0
        Me.txtIdProforma.Name = "txtIdProforma"
        Me.txtIdProforma.ReadOnly = True
        Me.txtIdProforma.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdProforma.Size = New System.Drawing.Size(81, 20)
        Me.txtIdProforma.TabIndex = 0
        Me.txtIdProforma.TabStop = False
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(735, 470)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.ReadOnly = True
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtTotal.TabIndex = 32
        Me.txtTotal.TabStop = False
        Me.txtTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtDescuento
        '
        Me.txtDescuento.AcceptsReturn = True
        Me.txtDescuento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescuento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescuento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescuento.Location = New System.Drawing.Point(735, 422)
        Me.txtDescuento.MaxLength = 0
        Me.txtDescuento.Name = "txtDescuento"
        Me.txtDescuento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescuento.Size = New System.Drawing.Size(73, 20)
        Me.txtDescuento.TabIndex = 30
        Me.txtDescuento.TabStop = False
        Me.txtDescuento.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtSubTotal
        '
        Me.txtSubTotal.AcceptsReturn = True
        Me.txtSubTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtSubTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSubTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSubTotal.Location = New System.Drawing.Point(735, 398)
        Me.txtSubTotal.MaxLength = 0
        Me.txtSubTotal.Name = "txtSubTotal"
        Me.txtSubTotal.ReadOnly = True
        Me.txtSubTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSubTotal.Size = New System.Drawing.Size(73, 20)
        Me.txtSubTotal.TabIndex = 29
        Me.txtSubTotal.TabStop = False
        Me.txtSubTotal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(105, 89)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 5
        Me.txtFecha.TabStop = False
        '
        '_LblImpuesto_0
        '
        Me._LblImpuesto_0.BackColor = System.Drawing.Color.Transparent
        Me._LblImpuesto_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblImpuesto_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblImpuesto_0.Location = New System.Drawing.Point(664, 446)
        Me._LblImpuesto_0.Name = "_LblImpuesto_0"
        Me._LblImpuesto_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblImpuesto_0.Size = New System.Drawing.Size(65, 19)
        Me._LblImpuesto_0.TabIndex = 23
        Me._LblImpuesto_0.Text = "Impuesto:"
        Me._LblImpuesto_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblTotal_6
        '
        Me._LblTotal_6.BackColor = System.Drawing.Color.Transparent
        Me._LblTotal_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblTotal_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblTotal_6.Location = New System.Drawing.Point(664, 470)
        Me._LblTotal_6.Name = "_LblTotal_6"
        Me._LblTotal_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblTotal_6.Size = New System.Drawing.Size(65, 19)
        Me._LblTotal_6.TabIndex = 21
        Me._LblTotal_6.Text = "Total:"
        Me._LblTotal_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_LblDescuento_1
        '
        Me._LblDescuento_1.BackColor = System.Drawing.Color.Transparent
        Me._LblDescuento_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._LblDescuento_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._LblDescuento_1.Location = New System.Drawing.Point(664, 422)
        Me._LblDescuento_1.Name = "_LblDescuento_1"
        Me._LblDescuento_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._LblDescuento_1.Size = New System.Drawing.Size(65, 19)
        Me._LblDescuento_1.TabIndex = 20
        Me._LblDescuento_1.Text = "Descuento:"
        Me._LblDescuento_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblSubTotal_5
        '
        Me._lblSubTotal_5.BackColor = System.Drawing.Color.Transparent
        Me._lblSubTotal_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblSubTotal_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblSubTotal_5.Location = New System.Drawing.Point(664, 398)
        Me._lblSubTotal_5.Name = "_lblSubTotal_5"
        Me._lblSubTotal_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblSubTotal_5.Size = New System.Drawing.Size(65, 19)
        Me._lblSubTotal_5.TabIndex = 17
        Me._lblSubTotal_5.Text = "Sub-Total:"
        Me._lblSubTotal_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(42, 89)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(57, 19)
        Me._lblLabels_3.TabIndex = 15
        Me._lblLabels_3.Text = "Fecha:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(34, 64)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_2.TabIndex = 14
        Me._lblLabels_2.Text = "Cliente:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(34, 36)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "Profor No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleProforma
        '
        Me.grdDetalleProforma.AllowUserToAddRows = False
        Me.grdDetalleProforma.AllowUserToDeleteRows = False
        Me.grdDetalleProforma.AllowUserToResizeColumns = False
        Me.grdDetalleProforma.AllowUserToResizeRows = False
        Me.grdDetalleProforma.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleProforma.Location = New System.Drawing.Point(8, 192)
        Me.grdDetalleProforma.MultiSelect = False
        Me.grdDetalleProforma.Name = "grdDetalleProforma"
        Me.grdDetalleProforma.ReadOnly = True
        Me.grdDetalleProforma.RowHeadersVisible = False
        Me.grdDetalleProforma.RowHeadersWidth = 30
        Me.grdDetalleProforma.Size = New System.Drawing.Size(800, 200)
        Me.grdDetalleProforma.TabIndex = 25
        Me.grdDetalleProforma.TabStop = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Image = CType(resources.GetObject("btnBuscarCliente.Image"), System.Drawing.Image)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(543, 64)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCliente.TabIndex = 4
        Me.btnBuscarCliente.TabStop = False
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'txtNombreCliente
        '
        Me.txtNombreCliente.AcceptsReturn = True
        Me.txtNombreCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreCliente.Location = New System.Drawing.Point(105, 64)
        Me.txtNombreCliente.MaxLength = 0
        Me.txtNombreCliente.Name = "txtNombreCliente"
        Me.txtNombreCliente.ReadOnly = True
        Me.txtNombreCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCliente.Size = New System.Drawing.Size(437, 20)
        Me.txtNombreCliente.TabIndex = 3
        Me.txtNombreCliente.TabStop = False
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(51, 166)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(252, 20)
        Me.txtCodigo.TabIndex = 21
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(303, 146)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(345, 19)
        Me.Label1.TabIndex = 74
        Me.Label1.Text = "Descripción"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(303, 166)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(345, 20)
        Me.txtDescripcion.TabIndex = 22
        Me.txtDescripcion.TabStop = False
        '
        'txtUnidad
        '
        Me.txtUnidad.AcceptsReturn = True
        Me.txtUnidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtUnidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtUnidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtUnidad.Location = New System.Drawing.Point(648, 166)
        Me.txtUnidad.MaxLength = 0
        Me.txtUnidad.Name = "txtUnidad"
        Me.txtUnidad.ReadOnly = True
        Me.txtUnidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtUnidad.Size = New System.Drawing.Size(60, 20)
        Me.txtUnidad.TabIndex = 23
        Me.txtUnidad.TabStop = False
        Me.txtUnidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'txtPrecio
        '
        Me.txtPrecio.AcceptsReturn = True
        Me.txtPrecio.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecio.Location = New System.Drawing.Point(708, 166)
        Me.txtPrecio.MaxLength = 0
        Me.txtPrecio.Name = "txtPrecio"
        Me.txtPrecio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecio.Size = New System.Drawing.Size(100, 20)
        Me.txtPrecio.TabIndex = 24
        Me.txtPrecio.TabStop = False
        Me.txtPrecio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(8, 166)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(43, 20)
        Me.txtCantidad.TabIndex = 20
        Me.txtCantidad.Text = "1"
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        '_lblLabels_8
        '
        Me._lblLabels_8.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_8.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_8.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_8.Location = New System.Drawing.Point(648, 146)
        Me._lblLabels_8.Name = "_lblLabels_8"
        Me._lblLabels_8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_8.Size = New System.Drawing.Size(60, 19)
        Me._lblLabels_8.TabIndex = 73
        Me._lblLabels_8.Text = "U/M"
        Me._lblLabels_8.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_7.Location = New System.Drawing.Point(708, 146)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(99, 19)
        Me._lblLabels_7.TabIndex = 72
        Me._lblLabels_7.Text = "Precio"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(8, 146)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(58, 19)
        Me._lblLabels_6.TabIndex = 71
        Me._lblLabels_6.Text = "Cant"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(51, 146)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(252, 19)
        Me._lblLabels_1.TabIndex = 70
        Me._lblLabels_1.Text = "Código"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDocumento
        '
        Me.txtDocumento.AcceptsReturn = True
        Me.txtDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocumento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocumento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocumento.Location = New System.Drawing.Point(297, 89)
        Me.txtDocumento.MaxLength = 300
        Me.txtDocumento.Name = "txtDocumento"
        Me.txtDocumento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocumento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDocumento.Size = New System.Drawing.Size(100, 20)
        Me.txtDocumento.TabIndex = 4
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(199, 89)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(92, 19)
        Me._lblLabels_11.TabIndex = 76
        Me._lblLabels_11.Text = "Nro. Documento:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtVendedor
        '
        Me.txtVendedor.AcceptsReturn = True
        Me.txtVendedor.BackColor = System.Drawing.SystemColors.Window
        Me.txtVendedor.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtVendedor.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtVendedor.Location = New System.Drawing.Point(533, 9)
        Me.txtVendedor.MaxLength = 0
        Me.txtVendedor.Name = "txtVendedor"
        Me.txtVendedor.ReadOnly = True
        Me.txtVendedor.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtVendedor.Size = New System.Drawing.Size(250, 20)
        Me.txtVendedor.TabIndex = 1
        Me.txtVendedor.TabStop = False
        '
        'btnBuscaVendedor
        '
        Me.btnBuscaVendedor.Image = CType(resources.GetObject("btnBuscaVendedor.Image"), System.Drawing.Image)
        Me.btnBuscaVendedor.Location = New System.Drawing.Point(782, 8)
        Me.btnBuscaVendedor.Name = "btnBuscaVendedor"
        Me.btnBuscaVendedor.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscaVendedor.TabIndex = 2
        Me.btnBuscaVendedor.TabStop = False
        Me.btnBuscaVendedor.UseVisualStyleBackColor = True
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(462, 9)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(65, 19)
        Me.Label2.TabIndex = 79
        Me.Label2.Text = "Vendedor:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPlazoCredito
        '
        Me.txtPlazoCredito.AcceptsReturn = True
        Me.txtPlazoCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlazoCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlazoCredito.Enabled = False
        Me.txtPlazoCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlazoCredito.Location = New System.Drawing.Point(500, 115)
        Me.txtPlazoCredito.MaxLength = 300
        Me.txtPlazoCredito.Name = "txtPlazoCredito"
        Me.txtPlazoCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlazoCredito.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtPlazoCredito.Size = New System.Drawing.Size(42, 20)
        Me.txtPlazoCredito.TabIndex = 8
        Me.txtPlazoCredito.TabStop = False
        '
        'Label12
        '
        Me.Label12.BackColor = System.Drawing.Color.Transparent
        Me.Label12.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label12.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label12.Location = New System.Drawing.Point(407, 116)
        Me.Label12.Name = "Label12"
        Me.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label12.Size = New System.Drawing.Size(87, 19)
        Me.Label12.TabIndex = 108
        Me.Label12.Text = "Plazo de crédito:"
        Me.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(10, 115)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(89, 19)
        Me.Label7.TabIndex = 107
        Me.Label7.Text = "Condición venta:"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'cboIdCondicionVenta
        '
        Me.cboIdCondicionVenta.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboIdCondicionVenta.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboIdCondicionVenta.BackColor = System.Drawing.SystemColors.Window
        Me.cboIdCondicionVenta.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIdCondicionVenta.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboIdCondicionVenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIdCondicionVenta.IntegralHeight = False
        Me.cboIdCondicionVenta.ItemHeight = 13
        Me.cboIdCondicionVenta.Location = New System.Drawing.Point(105, 115)
        Me.cboIdCondicionVenta.Name = "cboIdCondicionVenta"
        Me.cboIdCondicionVenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdCondicionVenta.Size = New System.Drawing.Size(295, 21)
        Me.cboIdCondicionVenta.TabIndex = 7
        '
        'FrmProforma
        '
        Me.AcceptButton = Me.cmdInsertar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(814, 498)
        Me.Controls.Add(Me.txtPlazoCredito)
        Me.Controls.Add(Me.Label12)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.cboIdCondicionVenta)
        Me.Controls.Add(Me.txtVendedor)
        Me.Controls.Add(Me.btnBuscaVendedor)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDocumento)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtUnidad)
        Me.Controls.Add(Me.txtPrecio)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me._lblLabels_8)
        Me.Controls.Add(Me._lblLabels_7)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me.txtNombreCliente)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.grdDetalleProforma)
        Me.Controls.Add(Me.cmdBusProd)
        Me.Controls.Add(Me.CmdAnular)
        Me.Controls.Add(Me.CmdAgregar)
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.CmdImprimir)
        Me.Controls.Add(Me.CmdGuardar)
        Me.Controls.Add(Me.cmdEliminar)
        Me.Controls.Add(Me.cmdInsertar)
        Me.Controls.Add(Me.txtImpuesto)
        Me.Controls.Add(Me.txtIdProforma)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.txtDescuento)
        Me.Controls.Add(Me.txtSubTotal)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._LblImpuesto_0)
        Me.Controls.Add(Me._LblTotal_6)
        Me.Controls.Add(Me._LblDescuento_1)
        Me.Controls.Add(Me._lblSubTotal_5)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProforma"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Proformas"
        CType(Me.grdDetalleProforma, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
    Public WithEvents txtNombreCliente As System.Windows.Forms.TextBox
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtUnidad As System.Windows.Forms.TextBox
    Public WithEvents txtPrecio As System.Windows.Forms.TextBox
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_8 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents txtDocumento As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents txtVendedor As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscaVendedor As System.Windows.Forms.Button
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtPlazoCredito As TextBox
    Public WithEvents Label12 As Label
    Public WithEvents Label7 As Label
    Public WithEvents cboIdCondicionVenta As ComboBox
End Class