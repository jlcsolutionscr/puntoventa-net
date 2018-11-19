<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAsientoContable
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
    Public WithEvents txtTotalDebito As System.Windows.Forms.TextBox
    Public WithEvents CmdAnular As System.Windows.Forms.Button
    Public WithEvents CmdAgregar As System.Windows.Forms.Button
    Public WithEvents CmdBuscar As System.Windows.Forms.Button
    Public WithEvents CmdImprimir As System.Windows.Forms.Button
    Public WithEvents CmdGuardar As System.Windows.Forms.Button
    Public WithEvents cmdEliminar As System.Windows.Forms.Button
    Public WithEvents cmdInsertar As System.Windows.Forms.Button
    Public WithEvents txtCredito As System.Windows.Forms.TextBox
    Public WithEvents cboCuentaContable As System.Windows.Forms.ComboBox
    Public WithEvents txtIdAsiento As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_11 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_7 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_6 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_3 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents grdDetalleAsiento As System.Windows.Forms.DataGridView
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.txtTotalDebito = New System.Windows.Forms.TextBox()
        Me.CmdAnular = New System.Windows.Forms.Button()
        Me.CmdAgregar = New System.Windows.Forms.Button()
        Me.CmdBuscar = New System.Windows.Forms.Button()
        Me.CmdImprimir = New System.Windows.Forms.Button()
        Me.CmdGuardar = New System.Windows.Forms.Button()
        Me.cmdEliminar = New System.Windows.Forms.Button()
        Me.cmdInsertar = New System.Windows.Forms.Button()
        Me.txtCredito = New System.Windows.Forms.TextBox()
        Me.cboCuentaContable = New System.Windows.Forms.ComboBox()
        Me.txtIdAsiento = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._lblLabels_11 = New System.Windows.Forms.Label()
        Me._lblLabels_7 = New System.Windows.Forms.Label()
        Me._lblLabels_6 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me.grdDetalleAsiento = New System.Windows.Forms.DataGridView()
        Me.txtDetalle = New System.Windows.Forms.TextBox()
        Me.txtDebito = New System.Windows.Forms.TextBox()
        Me.txtTotalCredito = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleAsiento, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtTotalDebito
        '
        Me.txtTotalDebito.AcceptsReturn = True
        Me.txtTotalDebito.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalDebito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalDebito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalDebito.Location = New System.Drawing.Point(301, 437)
        Me.txtTotalDebito.MaxLength = 300
        Me.txtTotalDebito.Name = "txtTotalDebito"
        Me.txtTotalDebito.ReadOnly = True
        Me.txtTotalDebito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalDebito.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTotalDebito.Size = New System.Drawing.Size(109, 20)
        Me.txtTotalDebito.TabIndex = 0
        Me.txtTotalDebito.TabStop = False
        Me.txtTotalDebito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
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
        Me.CmdAnular.TabIndex = 0
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
        Me.CmdAgregar.TabIndex = 0
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
        Me.CmdBuscar.TabIndex = 0
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
        Me.CmdImprimir.TabIndex = 0
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
        Me.CmdGuardar.TabIndex = 0
        Me.CmdGuardar.TabStop = False
        Me.CmdGuardar.Text = "&Guardar"
        Me.CmdGuardar.UseVisualStyleBackColor = False
        '
        'cmdEliminar
        '
        Me.cmdEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdEliminar.Location = New System.Drawing.Point(88, 434)
        Me.cmdEliminar.Name = "cmdEliminar"
        Me.cmdEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdEliminar.Size = New System.Drawing.Size(73, 25)
        Me.cmdEliminar.TabIndex = 0
        Me.cmdEliminar.TabStop = False
        Me.cmdEliminar.Text = "&Eliminar"
        Me.cmdEliminar.UseVisualStyleBackColor = False
        '
        'cmdInsertar
        '
        Me.cmdInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.cmdInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.cmdInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.cmdInsertar.Location = New System.Drawing.Point(8, 434)
        Me.cmdInsertar.Name = "cmdInsertar"
        Me.cmdInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cmdInsertar.Size = New System.Drawing.Size(73, 25)
        Me.cmdInsertar.TabIndex = 0
        Me.cmdInsertar.TabStop = False
        Me.cmdInsertar.Text = "Insertar"
        Me.cmdInsertar.UseVisualStyleBackColor = False
        '
        'txtCredito
        '
        Me.txtCredito.AcceptsReturn = True
        Me.txtCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCredito.Location = New System.Drawing.Point(490, 126)
        Me.txtCredito.MaxLength = 0
        Me.txtCredito.Name = "txtCredito"
        Me.txtCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCredito.Size = New System.Drawing.Size(118, 20)
        Me.txtCredito.TabIndex = 4
        Me.txtCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboCuentaContable
        '
        Me.cboCuentaContable.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.SuggestAppend
        Me.cboCuentaContable.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboCuentaContable.BackColor = System.Drawing.SystemColors.Window
        Me.cboCuentaContable.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCuentaContable.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCuentaContable.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCuentaContable.IntegralHeight = False
        Me.cboCuentaContable.ItemHeight = 13
        Me.cboCuentaContable.Location = New System.Drawing.Point(8, 126)
        Me.cboCuentaContable.Name = "cboCuentaContable"
        Me.cboCuentaContable.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCuentaContable.Size = New System.Drawing.Size(356, 21)
        Me.cboCuentaContable.TabIndex = 2
        '
        'txtIdAsiento
        '
        Me.txtIdAsiento.AcceptsReturn = True
        Me.txtIdAsiento.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdAsiento.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdAsiento.Enabled = False
        Me.txtIdAsiento.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdAsiento.Location = New System.Drawing.Point(76, 39)
        Me.txtIdAsiento.MaxLength = 0
        Me.txtIdAsiento.Name = "txtIdAsiento"
        Me.txtIdAsiento.ReadOnly = True
        Me.txtIdAsiento.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdAsiento.Size = New System.Drawing.Size(81, 20)
        Me.txtIdAsiento.TabIndex = 0
        Me.txtIdAsiento.TabStop = False
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.Enabled = False
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(225, 39)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 0
        Me.txtFecha.TabStop = False
        '
        '_lblLabels_11
        '
        Me._lblLabels_11.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_11.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_11.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_11.Location = New System.Drawing.Point(229, 438)
        Me._lblLabels_11.Name = "_lblLabels_11"
        Me._lblLabels_11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_11.Size = New System.Drawing.Size(69, 19)
        Me._lblLabels_11.TabIndex = 44
        Me._lblLabels_11.Text = "Total Débito:"
        Me._lblLabels_11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_7
        '
        Me._lblLabels_7.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_7.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_7.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_7.Location = New System.Drawing.Point(529, 107)
        Me._lblLabels_7.Name = "_lblLabels_7"
        Me._lblLabels_7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_7.Size = New System.Drawing.Size(40, 19)
        Me._lblLabels_7.TabIndex = 28
        Me._lblLabels_7.Text = "Crédito"
        Me._lblLabels_7.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_6
        '
        Me._lblLabels_6.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_6.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_6.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_6.Location = New System.Drawing.Point(409, 107)
        Me._lblLabels_6.Name = "_lblLabels_6"
        Me._lblLabels_6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_6.Size = New System.Drawing.Size(40, 19)
        Me._lblLabels_6.TabIndex = 27
        Me._lblLabels_6.Text = "Débito"
        Me._lblLabels_6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(138, 107)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(97, 19)
        Me._lblLabels_1.TabIndex = 24
        Me._lblLabels_1.Text = "Cuenta Contable"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(165, 40)
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
        Me._lblLabels_2.Location = New System.Drawing.Point(1, 64)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(71, 19)
        Me._lblLabels_2.TabIndex = 14
        Me._lblLabels_2.Text = "Detalle:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(7, 40)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_0.TabIndex = 13
        Me._lblLabels_0.Text = "Asiento No.:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleAsiento
        '
        Me.grdDetalleAsiento.AllowUserToAddRows = False
        Me.grdDetalleAsiento.AllowUserToDeleteRows = False
        Me.grdDetalleAsiento.AllowUserToResizeColumns = False
        Me.grdDetalleAsiento.AllowUserToResizeRows = False
        Me.grdDetalleAsiento.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleAsiento.Location = New System.Drawing.Point(8, 153)
        Me.grdDetalleAsiento.MultiSelect = False
        Me.grdDetalleAsiento.Name = "grdDetalleAsiento"
        Me.grdDetalleAsiento.ReadOnly = True
        Me.grdDetalleAsiento.RowHeadersVisible = False
        Me.grdDetalleAsiento.RowHeadersWidth = 30
        Me.grdDetalleAsiento.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.grdDetalleAsiento.Size = New System.Drawing.Size(600, 275)
        Me.grdDetalleAsiento.TabIndex = 5
        Me.grdDetalleAsiento.TabStop = False
        '
        'txtDetalle
        '
        Me.txtDetalle.AcceptsReturn = True
        Me.txtDetalle.BackColor = System.Drawing.SystemColors.Window
        Me.txtDetalle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDetalle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDetalle.Location = New System.Drawing.Point(76, 64)
        Me.txtDetalle.MaxLength = 0
        Me.txtDetalle.Multiline = True
        Me.txtDetalle.Name = "txtDetalle"
        Me.txtDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDetalle.Size = New System.Drawing.Size(532, 40)
        Me.txtDetalle.TabIndex = 1
        '
        'txtDebito
        '
        Me.txtDebito.AcceptsReturn = True
        Me.txtDebito.BackColor = System.Drawing.SystemColors.Window
        Me.txtDebito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDebito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDebito.Location = New System.Drawing.Point(370, 126)
        Me.txtDebito.MaxLength = 0
        Me.txtDebito.Name = "txtDebito"
        Me.txtDebito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDebito.Size = New System.Drawing.Size(118, 20)
        Me.txtDebito.TabIndex = 3
        Me.txtDebito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtTotalCredito
        '
        Me.txtTotalCredito.AcceptsReturn = True
        Me.txtTotalCredito.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalCredito.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalCredito.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalCredito.Location = New System.Drawing.Point(499, 437)
        Me.txtTotalCredito.MaxLength = 300
        Me.txtTotalCredito.Name = "txtTotalCredito"
        Me.txtTotalCredito.ReadOnly = True
        Me.txtTotalCredito.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalCredito.ScrollBars = System.Windows.Forms.ScrollBars.Vertical
        Me.txtTotalCredito.Size = New System.Drawing.Size(109, 20)
        Me.txtTotalCredito.TabIndex = 0
        Me.txtTotalCredito.TabStop = False
        Me.txtTotalCredito.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(416, 438)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(80, 19)
        Me.Label2.TabIndex = 68
        Me.Label2.Text = "Total Crédito:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAsientoContable
        '
        Me.AcceptButton = Me.cmdInsertar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(617, 464)
        Me.Controls.Add(Me.txtTotalCredito)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDebito)
        Me.Controls.Add(Me.txtDetalle)
        Me.Controls.Add(Me.grdDetalleAsiento)
        Me.Controls.Add(Me.txtTotalDebito)
        Me.Controls.Add(Me.CmdAnular)
        Me.Controls.Add(Me.CmdAgregar)
        Me.Controls.Add(Me.CmdBuscar)
        Me.Controls.Add(Me.CmdImprimir)
        Me.Controls.Add(Me.CmdGuardar)
        Me.Controls.Add(Me.cmdEliminar)
        Me.Controls.Add(Me.cmdInsertar)
        Me.Controls.Add(Me.txtCredito)
        Me.Controls.Add(Me.cboCuentaContable)
        Me.Controls.Add(Me.txtIdAsiento)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._lblLabels_11)
        Me.Controls.Add(Me._lblLabels_7)
        Me.Controls.Add(Me._lblLabels_6)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAsientoContable"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Empresa de Asiento de Diario Contable"
        CType(Me.grdDetalleAsiento, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtDetalle As System.Windows.Forms.TextBox
    Public WithEvents txtDebito As System.Windows.Forms.TextBox
    Public WithEvents txtTotalCredito As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
End Class