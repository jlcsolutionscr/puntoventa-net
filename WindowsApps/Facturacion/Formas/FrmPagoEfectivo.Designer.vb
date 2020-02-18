<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmPagoEfectivo
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
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()>
    Private Sub InitializeComponent()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtCambio = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtPagoDelCliente = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtTotalPagoEfectivo = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGuardar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnGuardar.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.btnGuardar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGuardar.Location = New System.Drawing.Point(149, 177)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnGuardar.Size = New System.Drawing.Size(104, 29)
        Me.btnGuardar.TabIndex = 0
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'txtCambio
        '
        Me.txtCambio.AcceptsReturn = True
        Me.txtCambio.BackColor = System.Drawing.SystemColors.Window
        Me.txtCambio.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCambio.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtCambio.ForeColor = System.Drawing.Color.Red
        Me.txtCambio.Location = New System.Drawing.Point(223, 120)
        Me.txtCambio.MaxLength = 0
        Me.txtCambio.Name = "txtCambio"
        Me.txtCambio.ReadOnly = True
        Me.txtCambio.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCambio.Size = New System.Drawing.Size(151, 35)
        Me.txtCambio.TabIndex = 3
        Me.txtCambio.TabStop = False
        Me.txtCambio.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.Font = New System.Drawing.Font("Arial Narrow", 18.0!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label6.ForeColor = System.Drawing.Color.Red
        Me.Label6.Location = New System.Drawing.Point(99, 122)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(118, 26)
        Me.Label6.TabIndex = 104
        Me.Label6.Text = "Su vuelto:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtPagoDelCliente
        '
        Me.txtPagoDelCliente.AcceptsReturn = True
        Me.txtPagoDelCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtPagoDelCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPagoDelCliente.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPagoDelCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPagoDelCliente.Location = New System.Drawing.Point(223, 79)
        Me.txtPagoDelCliente.MaxLength = 0
        Me.txtPagoDelCliente.Name = "txtPagoDelCliente"
        Me.txtPagoDelCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPagoDelCliente.Size = New System.Drawing.Size(151, 29)
        Me.txtPagoDelCliente.TabIndex = 2
        Me.txtPagoDelCliente.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(79, 79)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(138, 26)
        Me.Label3.TabIndex = 103
        Me.Label3.Text = "Pago del cliente:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtTotalPagoEfectivo
        '
        Me.txtTotalPagoEfectivo.AcceptsReturn = True
        Me.txtTotalPagoEfectivo.BackColor = System.Drawing.SystemColors.Window
        Me.txtTotalPagoEfectivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtTotalPagoEfectivo.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtTotalPagoEfectivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtTotalPagoEfectivo.Location = New System.Drawing.Point(223, 35)
        Me.txtTotalPagoEfectivo.MaxLength = 0
        Me.txtTotalPagoEfectivo.Name = "txtTotalPagoEfectivo"
        Me.txtTotalPagoEfectivo.ReadOnly = True
        Me.txtTotalPagoEfectivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtTotalPagoEfectivo.Size = New System.Drawing.Size(151, 29)
        Me.txtTotalPagoEfectivo.TabIndex = 1
        Me.txtTotalPagoEfectivo.TabStop = False
        Me.txtTotalPagoEfectivo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.Font = New System.Drawing.Font("Arial Narrow", 14.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(31, 35)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(186, 26)
        Me.Label1.TabIndex = 106
        Me.Label1.Text = "Total pago en efectivo:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmPagoEfectivo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(403, 233)
        Me.Controls.Add(Me.txtTotalPagoEfectivo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCambio)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtPagoDelCliente)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.btnGuardar)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(419, 272)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(419, 272)
        Me.Name = "FrmPagoEfectivo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Detalle de pago en efectivo"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Public WithEvents txtCambio As TextBox
    Public WithEvents Label6 As Label
    Public WithEvents txtPagoDelCliente As TextBox
    Public WithEvents Label3 As Label
    Public WithEvents txtTotalPagoEfectivo As TextBox
    Public WithEvents Label1 As Label
End Class