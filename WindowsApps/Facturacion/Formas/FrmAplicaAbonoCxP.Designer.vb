<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAplicaAbonoCxP
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
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtMonto As System.Windows.Forms.TextBox
    Public WithEvents txtObservaciones As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAplicaAbonoCxP))
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.txtObservaciones = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.txtDocumento = New System.Windows.Forms.TextBox()
        Me.lblAutorizacion = New System.Windows.Forms.Label()
        Me.txtTipoTarjeta = New System.Windows.Forms.TextBox()
        Me.lblTipoTarjeta = New System.Windows.Forms.Label()
        Me.cboTipoBanco = New System.Windows.Forms.ComboBox()
        Me.lblBanco = New System.Windows.Forms.Label()
        Me.grdDesglosePago = New System.Windows.Forms.DataGridView()
        Me.btnEliminarPago = New System.Windows.Forms.Button()
        Me.btnInsertarPago = New System.Windows.Forms.Button()
        Me.txtMontoPago = New System.Windows.Forms.TextBox()
        Me.cboFormaPago = New System.Windows.Forms.ComboBox()
        Me.Label4 = New System.Windows.Forms.Label()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtTotalAbonado = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSaldoPorPagar = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.btnBuscarCxP = New System.Windows.Forms.Button()
        Me.txtSaldoActual = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMontoOriginal = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSaldoPosterior = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRecibo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAgregar.Location = New System.Drawing.Point(136, 8)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAgregar.Size = New System.Drawing.Size(64, 21)
        Me.btnAgregar.TabIndex = 0
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
        Me.btnGuardar.TabIndex = 0
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'txtMonto
        '
        Me.txtMonto.AcceptsReturn = True
        Me.txtMonto.BackColor = System.Drawing.SystemColors.Window
        Me.txtMonto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMonto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMonto.Location = New System.Drawing.Point(346, 101)
        Me.txtMonto.MaxLength = 0
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonto.Size = New System.Drawing.Size(107, 20)
        Me.txtMonto.TabIndex = 8
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtObservaciones
        '
        Me.txtObservaciones.AcceptsReturn = True
        Me.txtObservaciones.BackColor = System.Drawing.SystemColors.Window
        Me.txtObservaciones.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtObservaciones.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtObservaciones.Location = New System.Drawing.Point(108, 127)
        Me.txtObservaciones.MaxLength = 0
        Me.txtObservaciones.Name = "txtObservaciones"
        Me.txtObservaciones.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtObservaciones.Size = New System.Drawing.Size(468, 20)
        Me.txtObservaciones.TabIndex = 4
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(108, 49)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 2
        Me.txtFecha.TabStop = False
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(244, 102)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(96, 19)
        Me._lblLabels_6.TabIndex = 16
        Me._lblLabels_6.Text = "Monto del abono:"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_5.Location = New System.Drawing.Point(12, 127)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(89, 19)
        Me._lblLabels_5.TabIndex = 15
        Me._lblLabels_5.Text = "Observaciones:"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(22, 49)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(79, 19)
        Me._lblLabels_3.TabIndex = 10
        Me._lblLabels_3.Text = "Fecha abono:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(33, 76)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(69, 19)
        Me._lblLabels_2.TabIndex = 9
        Me._lblLabels_2.Text = "Referencia:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'btnImprimir
        '
        Me.btnImprimir.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnImprimir.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnImprimir.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnImprimir.Location = New System.Drawing.Point(72, 8)
        Me.btnImprimir.Name = "btnImprimir"
        Me.btnImprimir.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnImprimir.Size = New System.Drawing.Size(64, 21)
        Me.btnImprimir.TabIndex = 0
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "&Tiquete"
        Me.btnImprimir.UseVisualStyleBackColor = False
        '
        'txtDocumento
        '
        Me.txtDocumento.AcceptsReturn = True
        Me.txtDocumento.BackColor = System.Drawing.SystemColors.Window
        Me.txtDocumento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDocumento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDocumento.Location = New System.Drawing.Point(549, 177)
        Me.txtDocumento.MaxLength = 0
        Me.txtDocumento.Name = "txtDocumento"
        Me.txtDocumento.ReadOnly = True
        Me.txtDocumento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDocumento.Size = New System.Drawing.Size(100, 20)
        Me.txtDocumento.TabIndex = 13
        '
        'lblAutorizacion
        '
        Me.lblAutorizacion.BackColor = System.Drawing.Color.Transparent
        Me.lblAutorizacion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblAutorizacion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblAutorizacion.Location = New System.Drawing.Point(549, 157)
        Me.lblAutorizacion.Name = "lblAutorizacion"
        Me.lblAutorizacion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblAutorizacion.Size = New System.Drawing.Size(100, 19)
        Me.lblAutorizacion.TabIndex = 111
        Me.lblAutorizacion.Text = "Documento"
        Me.lblAutorizacion.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTipoTarjeta
        '
        Me.txtTipoTarjeta.AcceptsReturn = True
        Me.txtTipoTarjeta.BackColor = System.Drawing.SystemColors.Window
        Me.txtTipoTarjeta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTipoTarjeta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTipoTarjeta.Location = New System.Drawing.Point(479, 177)
        Me.txtTipoTarjeta.MaxLength = 0
        Me.txtTipoTarjeta.Name = "txtTipoTarjeta"
        Me.txtTipoTarjeta.ReadOnly = True
        Me.txtTipoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTipoTarjeta.Size = New System.Drawing.Size(70, 20)
        Me.txtTipoTarjeta.TabIndex = 11
        '
        'lblTipoTarjeta
        '
        Me.lblTipoTarjeta.BackColor = System.Drawing.Color.Transparent
        Me.lblTipoTarjeta.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblTipoTarjeta.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblTipoTarjeta.Location = New System.Drawing.Point(479, 157)
        Me.lblTipoTarjeta.Name = "lblTipoTarjeta"
        Me.lblTipoTarjeta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblTipoTarjeta.Size = New System.Drawing.Size(70, 19)
        Me.lblTipoTarjeta.TabIndex = 110
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
        Me.cboTipoBanco.Location = New System.Drawing.Point(108, 177)
        Me.cboTipoBanco.Name = "cboTipoBanco"
        Me.cboTipoBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboTipoBanco.Size = New System.Drawing.Size(371, 21)
        Me.cboTipoBanco.TabIndex = 10
        '
        'lblBanco
        '
        Me.lblBanco.BackColor = System.Drawing.Color.Transparent
        Me.lblBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblBanco.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblBanco.Location = New System.Drawing.Point(108, 157)
        Me.lblBanco.Name = "lblBanco"
        Me.lblBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblBanco.Size = New System.Drawing.Size(371, 19)
        Me.lblBanco.TabIndex = 109
        Me.lblBanco.Text = "Banco"
        Me.lblBanco.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'grdDesglosePago
        '
        Me.grdDesglosePago.AllowUserToAddRows = False
        Me.grdDesglosePago.AllowUserToDeleteRows = False
        Me.grdDesglosePago.AllowUserToResizeColumns = False
        Me.grdDesglosePago.AllowUserToResizeRows = False
        Me.grdDesglosePago.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDesglosePago.Location = New System.Drawing.Point(8, 204)
        Me.grdDesglosePago.MultiSelect = False
        Me.grdDesglosePago.Name = "grdDesglosePago"
        Me.grdDesglosePago.ReadOnly = True
        Me.grdDesglosePago.RowHeadersVisible = False
        Me.grdDesglosePago.RowHeadersWidth = 30
        Me.grdDesglosePago.Size = New System.Drawing.Size(750, 89)
        Me.grdDesglosePago.TabIndex = 17
        Me.grdDesglosePago.TabStop = False
        '
        'btnEliminarPago
        '
        Me.btnEliminarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminarPago.Location = New System.Drawing.Point(88, 299)
        Me.btnEliminarPago.Name = "btnEliminarPago"
        Me.btnEliminarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminarPago.TabIndex = 19
        Me.btnEliminarPago.TabStop = False
        Me.btnEliminarPago.Text = "&Eliminar"
        Me.btnEliminarPago.UseVisualStyleBackColor = False
        '
        'btnInsertarPago
        '
        Me.btnInsertarPago.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertarPago.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertarPago.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertarPago.Location = New System.Drawing.Point(8, 299)
        Me.btnInsertarPago.Name = "btnInsertarPago"
        Me.btnInsertarPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertarPago.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertarPago.TabIndex = 18
        Me.btnInsertarPago.TabStop = False
        Me.btnInsertarPago.Text = "Insertar"
        Me.btnInsertarPago.UseVisualStyleBackColor = False
        '
        'txtMontoPago
        '
        Me.txtMontoPago.AcceptsReturn = True
        Me.txtMontoPago.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoPago.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMontoPago.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMontoPago.Location = New System.Drawing.Point(649, 177)
        Me.txtMontoPago.MaxLength = 0
        Me.txtMontoPago.Name = "txtMontoPago"
        Me.txtMontoPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoPago.Size = New System.Drawing.Size(109, 20)
        Me.txtMontoPago.TabIndex = 16
        Me.txtMontoPago.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.cboFormaPago.Location = New System.Drawing.Point(8, 177)
        Me.cboFormaPago.Name = "cboFormaPago"
        Me.cboFormaPago.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboFormaPago.Size = New System.Drawing.Size(100, 21)
        Me.cboFormaPago.TabIndex = 9
        '
        'Label4
        '
        Me.Label4.BackColor = System.Drawing.Color.Transparent
        Me.Label4.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label4.Location = New System.Drawing.Point(649, 157)
        Me.Label4.Name = "Label4"
        Me.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label4.Size = New System.Drawing.Size(109, 19)
        Me.Label4.TabIndex = 106
        Me.Label4.Text = "Monto"
        Me.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'Label5
        '
        Me.Label5.BackColor = System.Drawing.Color.Transparent
        Me.Label5.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label5.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label5.Location = New System.Drawing.Point(8, 157)
        Me.Label5.Name = "Label5"
        Me.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label5.Size = New System.Drawing.Size(100, 19)
        Me.Label5.TabIndex = 105
        Me.Label5.Text = "Forma de Pago"
        Me.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtTotalAbonado
        '
        Me.txtTotalAbonado.AcceptsReturn = True
        Me.txtTotalAbonado.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalAbonado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalAbonado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalAbonado.Location = New System.Drawing.Point(549, 76)
        Me.txtTotalAbonado.MaxLength = 0
        Me.txtTotalAbonado.Name = "txtTotalAbonado"
        Me.txtTotalAbonado.ReadOnly = True
        Me.txtTotalAbonado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalAbonado.Size = New System.Drawing.Size(107, 20)
        Me.txtTotalAbonado.TabIndex = 7
        Me.txtTotalAbonado.TabStop = False
        Me.txtTotalAbonado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label8
        '
        Me.Label8.BackColor = System.Drawing.Color.Transparent
        Me.Label8.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label8.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label8.Location = New System.Drawing.Point(462, 76)
        Me.Label8.Name = "Label8"
        Me.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label8.Size = New System.Drawing.Size(81, 19)
        Me.Label8.TabIndex = 118
        Me.Label8.Text = "Total abonado:"
        Me.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSaldoPorPagar
        '
        Me.txtSaldoPorPagar.AcceptsReturn = True
        Me.txtSaldoPorPagar.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoPorPagar.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoPorPagar.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoPorPagar.Location = New System.Drawing.Point(685, 299)
        Me.txtSaldoPorPagar.MaxLength = 0
        Me.txtSaldoPorPagar.Name = "txtSaldoPorPagar"
        Me.txtSaldoPorPagar.ReadOnly = True
        Me.txtSaldoPorPagar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoPorPagar.Size = New System.Drawing.Size(73, 20)
        Me.txtSaldoPorPagar.TabIndex = 20
        Me.txtSaldoPorPagar.TabStop = False
        Me.txtSaldoPorPagar.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label10
        '
        Me.Label10.BackColor = System.Drawing.Color.Transparent
        Me.Label10.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label10.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label10.Location = New System.Drawing.Point(587, 299)
        Me.Label10.Name = "Label10"
        Me.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label10.Size = New System.Drawing.Size(91, 19)
        Me.Label10.TabIndex = 149
        Me.Label10.Text = "Saldo por Pagar:"
        Me.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtId
        '
        Me.txtId.AcceptsReturn = True
        Me.txtId.BackColor = System.Drawing.SystemColors.Window
        Me.txtId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtId.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtId.Location = New System.Drawing.Point(108, 75)
        Me.txtId.MaxLength = 0
        Me.txtId.Name = "txtId"
        Me.txtId.ReadOnly = True
        Me.txtId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtId.Size = New System.Drawing.Size(107, 20)
        Me.txtId.TabIndex = 0
        Me.txtId.TabStop = False
        '
        'btnBuscarCxP
        '
        Me.btnBuscarCxP.Image = CType(resources.GetObject("btnBuscarCxP.Image"), System.Drawing.Image)
        Me.btnBuscarCxP.Location = New System.Drawing.Point(221, 74)
        Me.btnBuscarCxP.Name = "btnBuscarCxP"
        Me.btnBuscarCxP.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCxP.TabIndex = 0
        Me.btnBuscarCxP.TabStop = False
        Me.btnBuscarCxP.UseVisualStyleBackColor = True
        '
        'txtSaldoActual
        '
        Me.txtSaldoActual.AcceptsReturn = True
        Me.txtSaldoActual.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoActual.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoActual.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoActual.Location = New System.Drawing.Point(108, 101)
        Me.txtSaldoActual.MaxLength = 0
        Me.txtSaldoActual.Name = "txtSaldoActual"
        Me.txtSaldoActual.ReadOnly = True
        Me.txtSaldoActual.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoActual.Size = New System.Drawing.Size(107, 20)
        Me.txtSaldoActual.TabIndex = 153
        Me.txtSaldoActual.TabStop = False
        Me.txtSaldoActual.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(29, 102)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(73, 19)
        Me.Label3.TabIndex = 154
        Me.Label3.Text = "Saldo actual:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMontoOriginal
        '
        Me.txtMontoOriginal.AcceptsReturn = True
        Me.txtMontoOriginal.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoOriginal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMontoOriginal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMontoOriginal.Location = New System.Drawing.Point(346, 75)
        Me.txtMontoOriginal.MaxLength = 0
        Me.txtMontoOriginal.Name = "txtMontoOriginal"
        Me.txtMontoOriginal.ReadOnly = True
        Me.txtMontoOriginal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoOriginal.Size = New System.Drawing.Size(107, 20)
        Me.txtMontoOriginal.TabIndex = 155
        Me.txtMontoOriginal.TabStop = False
        Me.txtMontoOriginal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(260, 75)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(80, 19)
        Me.Label6.TabIndex = 156
        Me.Label6.Text = "Monto original:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtSaldoPosterior
        '
        Me.txtSaldoPosterior.AcceptsReturn = True
        Me.txtSaldoPosterior.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldoPosterior.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldoPosterior.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldoPosterior.Location = New System.Drawing.Point(549, 101)
        Me.txtSaldoPosterior.MaxLength = 0
        Me.txtSaldoPosterior.Name = "txtSaldoPosterior"
        Me.txtSaldoPosterior.ReadOnly = True
        Me.txtSaldoPosterior.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldoPosterior.Size = New System.Drawing.Size(107, 20)
        Me.txtSaldoPosterior.TabIndex = 157
        Me.txtSaldoPosterior.TabStop = False
        Me.txtSaldoPosterior.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(460, 102)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(83, 19)
        Me.Label1.TabIndex = 158
        Me.Label1.Text = "Saldo posterior:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRecibo
        '
        Me.txtRecibo.AcceptsReturn = True
        Me.txtRecibo.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecibo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecibo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecibo.Location = New System.Drawing.Point(651, 127)
        Me.txtRecibo.MaxLength = 0
        Me.txtRecibo.Name = "txtRecibo"
        Me.txtRecibo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecibo.Size = New System.Drawing.Size(107, 20)
        Me.txtRecibo.TabIndex = 159
        Me.txtRecibo.TabStop = False
        Me.txtRecibo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(582, 128)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(63, 19)
        Me.Label2.TabIndex = 160
        Me.Label2.Text = "Recibo nro:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAplicaAbonoCxP
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(767, 329)
        Me.Controls.Add(Me.txtRecibo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtSaldoPosterior)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtMontoOriginal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSaldoActual)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.btnBuscarCxP)
        Me.Controls.Add(Me.txtSaldoPorPagar)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.txtTotalAbonado)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtDocumento)
        Me.Controls.Add(Me.lblAutorizacion)
        Me.Controls.Add(Me.txtTipoTarjeta)
        Me.Controls.Add(Me.lblTipoTarjeta)
        Me.Controls.Add(Me.cboTipoBanco)
        Me.Controls.Add(Me.lblBanco)
        Me.Controls.Add(Me.grdDesglosePago)
        Me.Controls.Add(Me.btnEliminarPago)
        Me.Controls.Add(Me.btnInsertarPago)
        Me.Controls.Add(Me.txtMontoPago)
        Me.Controls.Add(Me.cboFormaPago)
        Me.Controls.Add(Me.Label4)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtMonto)
        Me.Controls.Add(Me.txtObservaciones)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_5)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(783, 368)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(783, 368)
        Me.Name = "FrmAplicaAbonoCxP"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aplicar abono a Cuentas por Pagar"
        CType(Me.grdDesglosePago, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents btnImprimir As System.Windows.Forms.Button
    Public WithEvents txtDocumento As System.Windows.Forms.TextBox
    Public WithEvents lblAutorizacion As System.Windows.Forms.Label
    Public WithEvents txtTipoTarjeta As System.Windows.Forms.TextBox
    Public WithEvents lblTipoTarjeta As System.Windows.Forms.Label
    Public WithEvents cboTipoBanco As System.Windows.Forms.ComboBox
    Public WithEvents lblBanco As System.Windows.Forms.Label
    Public WithEvents grdDesglosePago As System.Windows.Forms.DataGridView
    Public WithEvents btnEliminarPago As System.Windows.Forms.Button
    Public WithEvents btnInsertarPago As System.Windows.Forms.Button
    Public WithEvents txtMontoPago As System.Windows.Forms.TextBox
    Public WithEvents cboFormaPago As System.Windows.Forms.ComboBox
    Public WithEvents Label4 As System.Windows.Forms.Label
    Public WithEvents Label5 As System.Windows.Forms.Label
    Public WithEvents txtTotalAbonado As System.Windows.Forms.TextBox
    Public WithEvents Label8 As System.Windows.Forms.Label
    Public WithEvents txtSaldoPorPagar As System.Windows.Forms.TextBox
    Public WithEvents Label10 As System.Windows.Forms.Label
    Public WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarCxP As System.Windows.Forms.Button
    Public WithEvents txtSaldoActual As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtMontoOriginal As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtSaldoPosterior As TextBox
    Public WithEvents Label1 As Label
    Public WithEvents txtRecibo As TextBox
    Public WithEvents Label2 As Label
End Class