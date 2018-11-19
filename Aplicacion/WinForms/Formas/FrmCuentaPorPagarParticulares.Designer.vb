<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCuentaPorPagarParticulares
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
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents btnAnular As System.Windows.Forms.Button
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents btnImprimir As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtIdCxP As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCuentaPorPagarParticulares))
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtIdCxP = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.btnBuscarParticular = New System.Windows.Forms.Button()
        Me.txtNombre = New System.Windows.Forms.TextBox()
        Me.txtPlazo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtTotal = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDocumento = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtTipoTarjeta = New System.Windows.Forms.TextBox()
        Me.lblTipoTarjeta = New System.Windows.Forms.Label()
        Me.cboTipoBanco = New System.Windows.Forms.ComboBox()
        Me.lblBanco = New System.Windows.Forms.Label()
        Me.cboTipoMoneda = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.txtTipoCambio = New System.Windows.Forms.TextBox()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.cboFormaPago = New System.Windows.Forms.ComboBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtSaldoPorPagar = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.grdDesglosePago = New System.Windows.Forms.DataGridView()
        Me.btnEliminarPago = New System.Windows.Forms.Button()
        Me.btnInsertarPago = New System.Windows.Forms.Button()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(72, 117)
        Me.txtDescripcion.MaxLength = 300
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtDescripcion.Size = New System.Drawing.Size(554, 40)
        Me.txtDescripcion.TabIndex = 3
        '
        'btnAnular
        '
        Me.btnAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAnular.Enabled = False
        Me.btnAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAnular.Location = New System.Drawing.Point(200, 8)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAnular.Size = New System.Drawing.Size(64, 21)
        Me.btnAnular.TabIndex = 36
        Me.btnAnular.TabStop = False
        Me.btnAnular.Text = "&Anular"
        Me.btnAnular.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAgregar.Location = New System.Drawing.Point(264, 8)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAgregar.Size = New System.Drawing.Size(64, 21)
        Me.btnAgregar.TabIndex = 35
        Me.btnAgregar.TabStop = False
        Me.btnAgregar.Text = "&Nuevo"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBuscar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBuscar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBuscar.Location = New System.Drawing.Point(136, 8)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBuscar.Size = New System.Drawing.Size(64, 21)
        Me.btnBuscar.TabIndex = 34
        Me.btnBuscar.TabStop = False
        Me.btnBuscar.Text = "B&uscar"
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'btnImprimir
        '
        Me.btnImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnImprimir.Enabled = False
        Me.btnImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnImprimir.Location = New System.Drawing.Point(72, 8)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnImprimir.Size = New System.Drawing.Size(64, 21)
        Me.btnImprimir.TabIndex = 33
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "&Imprimir"
        Me.btnImprimir.UseVisualStyleBackColor = False
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
        Me.btnGuardar.TabIndex = 32
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'txtIdCxP
        '
        Me.txtIdCxP.AcceptsReturn = True
        Me.txtIdCxP.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCxP.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCxP.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCxP.Location = New System.Drawing.Point(71, 39)
        Me.txtIdCxP.MaxLength = 0
        Me.txtIdCxP.Name = "txtIdCxP"
        Me.txtIdCxP.ReadOnly = True
        Me.txtIdCxP.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCxP.Size = New System.Drawing.Size(73, 20)
        Me.txtIdCxP.TabIndex = 0
        Me.txtIdCxP.TabStop = False
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(71, 91)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 2
        Me.txtFecha.TabStop = False
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(-4, 117)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(70, 19)
        Me._lblLabels_11.TabIndex = 44
        Me._lblLabels_11.Text = "Descripción:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(24, 91)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(41, 19)
        Me._lblLabels_3.TabIndex = 15
        Me._lblLabels_3.Text = "Fecha:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(-1, 66)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(67, 19)
        Me._lblLabels_2.TabIndex = 14
        Me._lblLabels_2.Text = "Particular:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(0, 40)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "CxP No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnBuscarParticular
        '
        Me.btnBuscarParticular.Image = CType(resources.GetObject("btnBuscarParticular.Image"), System.Drawing.Image)
        Me.btnBuscarParticular.Location = New System.Drawing.Point(605, 65)
        Me.btnBuscarParticular.Name = "btnBuscarParticular"
        Me.btnBuscarParticular.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarParticular.TabIndex = 2
        Me.btnBuscarParticular.TabStop = False
        Me.btnBuscarParticular.UseVisualStyleBackColor = True
        '
        'txtNombre
        '
        Me.txtNombre.AcceptsReturn = True
        Me.txtNombre.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombre.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombre.Enabled = False
        Me.txtNombre.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombre.Location = New System.Drawing.Point(72, 65)
        Me.txtNombre.MaxLength = 0
        Me.txtNombre.Name = "txtNombre"
        Me.txtNombre.ReadOnly = True
        Me.txtNombre.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombre.Size = New System.Drawing.Size(530, 20)
        Me.txtNombre.TabIndex = 1
        Me.txtNombre.TabStop = False
        '
        'txtPlazo
        '
        Me.txtPlazo.AcceptsReturn = True
        Me.txtPlazo.BackColor = System.Drawing.SystemColors.Window
        Me.txtPlazo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPlazo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPlazo.Location = New System.Drawing.Point(73, 163)
        Me.txtPlazo.MaxLength = 0
        Me.txtPlazo.Name = "txtPlazo"
        Me.txtPlazo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPlazo.Size = New System.Drawing.Size(52, 20)
        Me.txtPlazo.TabIndex = 4
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(26, 163)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(41, 19)
        Me.Label1.TabIndex = 46
        Me.Label1.Text = "Plazo:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotal
        '
        Me.txtTotal.AcceptsReturn = True
        Me.txtTotal.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotal.Location = New System.Drawing.Point(72, 189)
        Me.txtTotal.MaxLength = 0
        Me.txtTotal.Name = "txtTotal"
        Me.txtTotal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotal.Size = New System.Drawing.Size(77, 20)
        Me.txtTotal.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(25, 189)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(41, 19)
        Me.Label2.TabIndex = 48
        Me.Label2.Text = "Total:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtDocumento
        '
        Me.txtDocumento.AcceptsReturn = True
        Me.txtDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocumento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocumento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocumento.Location = New System.Drawing.Point(372, 238)
        Me.txtDocumento.MaxLength = 0
        Me.txtDocumento.Name = "txtDocumento"
        Me.txtDocumento.ReadOnly = True
        Me.txtDocumento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocumento.Size = New System.Drawing.Size(100, 20)
        Me.txtDocumento.TabIndex = 9
        '
        'Label7
        '
        Me.Label7.BackColor = System.Drawing.Color.Transparent
        Me.Label7.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label7.Location = New System.Drawing.Point(372, 218)
        Me.Label7.Name = "Label7"
        Me.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label7.Size = New System.Drawing.Size(100, 19)
        Me.Label7.TabIndex = 180
        Me.Label7.Text = "Documento"
        Me.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTipoTarjeta
        '
        Me.txtTipoTarjeta.AcceptsReturn = True
        Me.txtTipoTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoTarjeta.Location = New System.Drawing.Point(302, 238)
        Me.txtTipoTarjeta.MaxLength = 0
        Me.txtTipoTarjeta.Name = "txtTipoTarjeta"
        Me.txtTipoTarjeta.ReadOnly = True
        Me.txtTipoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoTarjeta.Size = New System.Drawing.Size(70, 20)
        Me.txtTipoTarjeta.TabIndex = 8
        '
        'lblTipoTarjeta
        '
        Me.lblTipoTarjeta.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoTarjeta.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTipoTarjeta.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTipoTarjeta.Location = New System.Drawing.Point(302, 218)
        Me.lblTipoTarjeta.Name = "lblTipoTarjeta"
        Me.lblTipoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTipoTarjeta.Size = New System.Drawing.Size(70, 19)
        Me.lblTipoTarjeta.TabIndex = 179
        Me.lblTipoTarjeta.Text = "Tipo Tarjeta"
        Me.lblTipoTarjeta.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTipoBanco
        '
        Me.cboTipoBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoBanco.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoBanco.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoBanco.Enabled = False
        Me.cboTipoBanco.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoBanco.IntegralHeight = False
        Me.cboTipoBanco.ItemHeight = 13
        Me.cboTipoBanco.Location = New System.Drawing.Point(108, 238)
        Me.cboTipoBanco.Name = "cboTipoBanco"
        Me.cboTipoBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoBanco.Size = New System.Drawing.Size(194, 21)
        Me.cboTipoBanco.TabIndex = 7
        '
        'lblBanco
        '
        Me.lblBanco.BackColor = System.Drawing.Color.Transparent
        Me.lblBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBanco.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBanco.Location = New System.Drawing.Point(108, 218)
        Me.lblBanco.Name = "lblBanco"
        Me.lblBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBanco.Size = New System.Drawing.Size(194, 19)
        Me.lblBanco.TabIndex = 178
        Me.lblBanco.Text = "Banco"
        Me.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'cboTipoMoneda
        '
        Me.cboTipoMoneda.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboTipoMoneda.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboTipoMoneda.BackColor = System.Drawing.SystemColors.Window
        Me.cboTipoMoneda.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboTipoMoneda.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboTipoMoneda.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboTipoMoneda.IntegralHeight = False
        Me.cboTipoMoneda.ItemHeight = 13
        Me.cboTipoMoneda.Location = New System.Drawing.Point(472, 238)
        Me.cboTipoMoneda.Name = "cboTipoMoneda"
        Me.cboTipoMoneda.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoMoneda.Size = New System.Drawing.Size(105, 21)
        Me.cboTipoMoneda.TabIndex = 10
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(472, 218)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(105, 19)
        Me.Label4.TabIndex = 177
        Me.Label4.Text = "Moneda"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTipoCambio
        '
        Me.txtTipoCambio.AcceptsReturn = True
        Me.txtTipoCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoCambio.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoCambio.Location = New System.Drawing.Point(577, 238)
        Me.txtTipoCambio.MaxLength = 0
        Me.txtTipoCambio.Name = "txtTipoCambio"
        Me.txtTipoCambio.ReadOnly = True
        Me.txtTipoCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoCambio.Size = New System.Drawing.Size(73, 20)
        Me.txtTipoCambio.TabIndex = 11
        Me.txtTipoCambio.TabStop = False
        Me.txtTipoCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtMonto
        '
        Me.txtMonto.AcceptsReturn = True
        Me.txtMonto.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonto.Location = New System.Drawing.Point(649, 238)
        Me.txtMonto.MaxLength = 0
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonto.Size = New System.Drawing.Size(109, 20)
        Me.txtMonto.TabIndex = 12
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboFormaPago
        '
        Me.cboFormaPago.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboFormaPago.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboFormaPago.BackColor = System.Drawing.SystemColors.Window
        Me.cboFormaPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboFormaPago.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboFormaPago.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboFormaPago.IntegralHeight = False
        Me.cboFormaPago.ItemHeight = 13
        Me.cboFormaPago.Location = New System.Drawing.Point(8, 238)
        Me.cboFormaPago.Name = "cboFormaPago"
        Me.cboFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFormaPago.Size = New System.Drawing.Size(100, 21)
        Me.cboFormaPago.TabIndex = 6
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(649, 218)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(109, 19)
        Me.Label5.TabIndex = 175
        Me.Label5.Text = "Monto"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(8, 218)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(100, 19)
        Me.Label6.TabIndex = 174
        Me.Label6.Text = "Forma de Pago"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label9
        '
        Me.Label9.BackColor = System.Drawing.Color.Transparent
        Me.Label9.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label9.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label9.Location = New System.Drawing.Point(574, 218)
        Me.Label9.Name = "Label9"
        Me.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label9.Size = New System.Drawing.Size(76, 19)
        Me.Label9.TabIndex = 176
        Me.Label9.Text = "Tipo Cambio"
        Me.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtSaldoPorPagar
        '
        Me.txtSaldoPorPagar.AcceptsReturn = True
        Me.txtSaldoPorPagar.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoPorPagar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoPorPagar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoPorPagar.Location = New System.Drawing.Point(658, 351)
        Me.txtSaldoPorPagar.MaxLength = 0
        Me.txtSaldoPorPagar.Name = "txtSaldoPorPagar"
        Me.txtSaldoPorPagar.ReadOnly = True
        Me.txtSaldoPorPagar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoPorPagar.Size = New System.Drawing.Size(100, 20)
        Me.txtSaldoPorPagar.TabIndex = 16
        Me.txtSaldoPorPagar.TabStop = False
        Me.txtSaldoPorPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(561, 351)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(91, 19)
        Me.Label10.TabIndex = 173
        Me.Label10.Text = "Saldo por Pagar:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDesglosePago
        '
        Me.grdDesglosePago.AllowUserToAddRows = False
        Me.grdDesglosePago.AllowUserToDeleteRows = False
        Me.grdDesglosePago.AllowUserToResizeColumns = False
        Me.grdDesglosePago.AllowUserToResizeRows = False
        Me.grdDesglosePago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDesglosePago.Location = New System.Drawing.Point(8, 265)
        Me.grdDesglosePago.MultiSelect = False
        Me.grdDesglosePago.Name = "grdDesglosePago"
        Me.grdDesglosePago.ReadOnly = True
        Me.grdDesglosePago.RowHeadersVisible = False
        Me.grdDesglosePago.RowHeadersWidth = 30
        Me.grdDesglosePago.Size = New System.Drawing.Size(750, 80)
        Me.grdDesglosePago.TabIndex = 13
        Me.grdDesglosePago.TabStop = False
        '
        'btnEliminarPago
        '
        Me.btnEliminarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarPago.Location = New System.Drawing.Point(88, 351)
        Me.btnEliminarPago.Name = "btnEliminarPago"
        Me.btnEliminarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminarPago.TabIndex = 15
        Me.btnEliminarPago.TabStop = False
        Me.btnEliminarPago.Text = "&Eliminar"
        Me.btnEliminarPago.UseVisualStyleBackColor = False
        '
        'btnInsertarPago
        '
        Me.btnInsertarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarPago.Location = New System.Drawing.Point(8, 351)
        Me.btnInsertarPago.Name = "btnInsertarPago"
        Me.btnInsertarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertarPago.TabIndex = 14
        Me.btnInsertarPago.TabStop = False
        Me.btnInsertarPago.Text = "Insertar"
        Me.btnInsertarPago.UseVisualStyleBackColor = False
        '
        'FrmCuentasPorPagarParticulares
        '
        Me.AcceptButton = Me.btnInsertarPago
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 385)
        Me.Controls.Add(Me.txtDocumento)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtTipoTarjeta)
        Me.Controls.Add(Me.lblTipoTarjeta)
        Me.Controls.Add(Me.cboTipoBanco)
        Me.Controls.Add(Me.lblBanco)
        Me.Controls.Add(Me.cboTipoMoneda)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.txtTipoCambio)
        Me.Controls.Add(Me.txtMonto)
        Me.Controls.Add(Me.cboFormaPago)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtSaldoPorPagar)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.grdDesglosePago)
        Me.Controls.Add(Me.btnEliminarPago)
        Me.Controls.Add(Me.btnInsertarPago)
        Me.Controls.Add(Me.txtTotal)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtPlazo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtNombre)
        Me.Controls.Add(Me.btnBuscarParticular)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtIdCxP)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCuentasPorPagarParticulares"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Módulo de Cuentas por Pagar a Particulares"
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnBuscarParticular As System.Windows.Forms.Button
    Public WithEvents txtNombre As System.Windows.Forms.TextBox
    Public WithEvents txtPlazo As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtTotal As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtDocumento As System.Windows.Forms.TextBox
    Public WithEvents Label7 As System.Windows.Forms.Label
    Public WithEvents txtTipoTarjeta As System.Windows.Forms.TextBox
    Public WithEvents lblTipoTarjeta As System.Windows.Forms.Label
    Public WithEvents cboTipoBanco As System.Windows.Forms.ComboBox
    Public WithEvents lblBanco As System.Windows.Forms.Label
    Public WithEvents cboTipoMoneda As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents txtTipoCambio As System.Windows.Forms.TextBox
    Public WithEvents txtMonto As System.Windows.Forms.TextBox
    Public WithEvents cboFormaPago As System.Windows.Forms.ComboBox
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents Label9 As System.Windows.Forms.Label
    Public WithEvents txtSaldoPorPagar As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents grdDesglosePago As System.Windows.Forms.DataGridView
    Public WithEvents btnEliminarPago As System.Windows.Forms.Button
    Public WithEvents btnInsertarPago As System.Windows.Forms.Button
End Class