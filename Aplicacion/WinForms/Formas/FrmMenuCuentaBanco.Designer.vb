<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenuCuentaBanco
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
    Public WithEvents cboIdCuentaBanco As System.Windows.Forms.ComboBox
    Public WithEvents btnProcesar As System.Windows.Forms.Button
    Public WithEvents Id2Label As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.cboIdCuentaBanco = New System.Windows.Forms.ComboBox()
        Me.btnProcesar = New System.Windows.Forms.Button()
        Me.Id2Label = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'cboIdCuentaBanco
        '
        Me.cboIdCuentaBanco.AutoCompleteMode = System.Windows.Forms.AutoCompleteMode.Suggest
        Me.cboIdCuentaBanco.AutoCompleteSource = System.Windows.Forms.AutoCompleteSource.ListItems
        Me.cboIdCuentaBanco.BackColor = System.Drawing.SystemColors.Window
        Me.cboIdCuentaBanco.Cursor = System.Windows.Forms.Cursors.Default
        Me.cboIdCuentaBanco.ForeColor = System.Drawing.SystemColors.WindowText
        Me.cboIdCuentaBanco.Location = New System.Drawing.Point(113, 28)
        Me.cboIdCuentaBanco.Name = "cboIdCuentaBanco"
        Me.cboIdCuentaBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.cboIdCuentaBanco.Size = New System.Drawing.Size(287, 21)
        Me.cboIdCuentaBanco.TabIndex = 2
        Me.cboIdCuentaBanco.TabStop = False
        '
        'btnProcesar
        '
        Me.btnProcesar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnProcesar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnProcesar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.btnProcesar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnProcesar.Location = New System.Drawing.Point(406, 25)
        Me.btnProcesar.Name = "btnProcesar"
        Me.btnProcesar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnProcesar.Size = New System.Drawing.Size(73, 24)
        Me.btnProcesar.TabIndex = 0
        Me.btnProcesar.TabStop = False
        Me.btnProcesar.Text = "&Procesar"
        Me.btnProcesar.UseVisualStyleBackColor = False
        '
        'Id2Label
        '
        Me.Id2Label.BackColor = System.Drawing.Color.Transparent
        Me.Id2Label.Cursor = System.Windows.Forms.Cursors.Default
        Me.Id2Label.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Id2Label.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.Id2Label.Location = New System.Drawing.Point(16, 29)
        Me.Id2Label.Name = "Id2Label"
        Me.Id2Label.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Id2Label.Size = New System.Drawing.Size(91, 17)
        Me.Id2Label.TabIndex = 8
        Me.Id2Label.Text = "Cuenta Bancaria:"
        Me.Id2Label.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmMenuCuentaBanco
        '
        Me.AcceptButton = Me.btnProcesar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(499, 75)
        Me.Controls.Add(Me.cboIdCuentaBanco)
        Me.Controls.Add(Me.btnProcesar)
        Me.Controls.Add(Me.Id2Label)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMenuCuentaBanco"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Seleccione la cuenta bancaria para procesar"
        Me.ResumeLayout(False)

    End Sub
End Class