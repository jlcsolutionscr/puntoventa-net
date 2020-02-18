<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmIngreso
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
    Public WithEvents btnAnular As System.Windows.Forms.Button
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents btnImprimir As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtMonto As System.Windows.Forms.TextBox
    Public WithEvents cboCuentaIngreso As System.Windows.Forms.ComboBox
    Public WithEvents txtIdIngreso As System.Windows.Forms.TextBox
    Public WithEvents txtDetalle As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents lblLabel7 As System.Windows.Forms.Label
    Public WithEvents lblLabel4 As System.Windows.Forms.Label
    Public WithEvents lblLabel3 As System.Windows.Forms.Label
    Public WithEvents lblLabel0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnImprimir = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtMonto = New System.Windows.Forms.TextBox()
        Me.cboCuentaIngreso = New System.Windows.Forms.ComboBox()
        Me.txtIdIngreso = New System.Windows.Forms.TextBox()
        Me.txtDetalle = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.lblLabel7 = New System.Windows.Forms.Label()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel0 = New System.Windows.Forms.Label()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtRecibidoDe = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
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
        Me.btnAnular.TabIndex = 0
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
        Me.btnAgregar.TabIndex = 0
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
        Me.btnBuscar.TabIndex = 0
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
        Me.btnImprimir.TabIndex = 0
        Me.btnImprimir.TabStop = False
        Me.btnImprimir.Text = "&Tiquete"
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
        Me.txtMonto.Location = New System.Drawing.Point(84, 169)
        Me.txtMonto.MaxLength = 0
        Me.txtMonto.Name = "txtMonto"
        Me.txtMonto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMonto.Size = New System.Drawing.Size(100, 20)
        Me.txtMonto.TabIndex = 5
        Me.txtMonto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'cboCuentaIngreso
        '
        Me.cboCuentaIngreso.BackColor = System.Drawing.SystemColors.Window
        Me.cboCuentaIngreso.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboCuentaIngreso.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboCuentaIngreso.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboCuentaIngreso.Location = New System.Drawing.Point(84, 90)
        Me.cboCuentaIngreso.Name = "cboCuentaIngreso"
        Me.cboCuentaIngreso.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboCuentaIngreso.Size = New System.Drawing.Size(360, 21)
        Me.cboCuentaIngreso.TabIndex = 2
        '
        'txtIdIngreso
        '
        Me.txtIdIngreso.AcceptsReturn = True
        Me.txtIdIngreso.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdIngreso.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdIngreso.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdIngreso.Location = New System.Drawing.Point(84, 40)
        Me.txtIdIngreso.MaxLength = 0
        Me.txtIdIngreso.Name = "txtIdIngreso"
        Me.txtIdIngreso.ReadOnly = True
        Me.txtIdIngreso.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdIngreso.Size = New System.Drawing.Size(73, 20)
        Me.txtIdIngreso.TabIndex = 0
        Me.txtIdIngreso.TabStop = False
        '
        'txtDetalle
        '
        Me.txtDetalle.AcceptsReturn = True
        Me.txtDetalle.BackColor = System.Drawing.SystemColors.Window
        Me.txtDetalle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDetalle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDetalle.Location = New System.Drawing.Point(84, 143)
        Me.txtDetalle.MaxLength = 255
        Me.txtDetalle.Name = "txtDetalle"
        Me.txtDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDetalle.Size = New System.Drawing.Size(484, 20)
        Me.txtDetalle.TabIndex = 4
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(84, 64)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 1
        Me.txtFecha.TabStop = False
        '
        'lblLabel7
        '
        Me.lblLabel7.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel7.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel7.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel7.Location = New System.Drawing.Point(34, 169)
        Me.lblLabel7.Name = "lblLabel7"
        Me.lblLabel7.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel7.Size = New System.Drawing.Size(44, 19)
        Me.lblLabel7.TabIndex = 28
        Me.lblLabel7.Text = "Monto:"
        Me.lblLabel7.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(21, 143)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(57, 19)
        Me.lblLabel4.TabIndex = 13
        Me.lblLabel4.Text = "Detalle:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(12, 64)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel3.Size = New System.Drawing.Size(66, 19)
        Me.lblLabel3.TabIndex = 12
        Me.lblLabel3.Text = "Fecha:"
        Me.lblLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel0
        '
        Me.lblLabel0.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel0.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel0.Location = New System.Drawing.Point(12, 40)
        Me.lblLabel0.Name = "lblLabel0"
        Me.lblLabel0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel0.Size = New System.Drawing.Size(66, 19)
        Me.lblLabel0.TabIndex = 10
        Me.lblLabel0.Text = "Id:"
        Me.lblLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(34, 90)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(44, 19)
        Me.Label1.TabIndex = 36
        Me.Label1.Text = "Cuenta:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtRecibidoDe
        '
        Me.txtRecibidoDe.AcceptsReturn = True
        Me.txtRecibidoDe.BackColor = System.Drawing.SystemColors.Window
        Me.txtRecibidoDe.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRecibidoDe.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRecibidoDe.Location = New System.Drawing.Point(84, 117)
        Me.txtRecibidoDe.MaxLength = 100
        Me.txtRecibidoDe.Name = "txtRecibidoDe"
        Me.txtRecibidoDe.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRecibidoDe.Size = New System.Drawing.Size(360, 20)
        Me.txtRecibidoDe.TabIndex = 3
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(8, 117)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(70, 19)
        Me.Label3.TabIndex = 163
        Me.Label3.Text = "Recibido de:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmIngreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(581, 202)
        Me.Controls.Add(Me.txtRecibidoDe)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnImprimir)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtMonto)
        Me.Controls.Add(Me.cboCuentaIngreso)
        Me.Controls.Add(Me.txtIdIngreso)
        Me.Controls.Add(Me.txtDetalle)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.lblLabel7)
        Me.Controls.Add(Me.lblLabel4)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.lblLabel0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(597, 241)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(597, 241)
        Me.Name = "FrmIngreso"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Registro de Ingresos de Efectivo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtRecibidoDe As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
End Class