<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCierreContable
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
    Public WithEvents CmdProcesar As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmCierreContable))
        Me.CmdProcesar = New System.Windows.Forms.Button()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CmdProcesar
        '
        Me.CmdProcesar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdProcesar.ForeColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdProcesar.Image = CType(resources.GetObject("CmdProcesar.Image"), System.Drawing.Image)
        Me.CmdProcesar.Location = New System.Drawing.Point(153, 22)
        Me.CmdProcesar.Name = "CmdProcesar"
        Me.CmdProcesar.Size = New System.Drawing.Size(30, 30)
        Me.CmdProcesar.TabIndex = 0
        Me.CmdProcesar.TabStop = False
        Me.CmdProcesar.UseVisualStyleBackColor = False
        '
        'Label11
        '
        Me.Label11.BackColor = System.Drawing.Color.Transparent
        Me.Label11.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label11.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label11.Location = New System.Drawing.Point(16, 29)
        Me.Label11.Name = "Label11"
        Me.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label11.Size = New System.Drawing.Size(131, 17)
        Me.Label11.TabIndex = 26
        Me.Label11.Text = "Ejecutar cierre mensual:"
        Me.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmCierreContable
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(198, 74)
        Me.Controls.Add(Me.CmdProcesar)
        Me.Controls.Add(Me.Label11)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCierreContable"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cierre Mensual Contable"
        Me.ResumeLayout(False)

    End Sub
    Public WithEvents Label11 As System.Windows.Forms.Label
End Class