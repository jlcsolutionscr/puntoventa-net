<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmInicio
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
    Public WithEvents CmdAceptar As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInicio))
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.picInicio = New System.Windows.Forms.PictureBox()
        Me.lblVersion = New System.Windows.Forms.Label()
        CType(Me.picInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAceptar.Location = New System.Drawing.Point(209, 628)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAceptar.Size = New System.Drawing.Size(97, 25)
        Me.CmdAceptar.TabIndex = 0
        Me.CmdAceptar.Text = "&Aceptar"
        Me.CmdAceptar.UseVisualStyleBackColor = False
        '
        'picInicio
        '
        Me.picInicio.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.picInicio.Image = CType(resources.GetObject("picInicio.Image"), System.Drawing.Image)
        Me.picInicio.InitialImage = CType(resources.GetObject("picInicio.InitialImage"), System.Drawing.Image)
        Me.picInicio.Location = New System.Drawing.Point(12, 12)
        Me.picInicio.Name = "picInicio"
        Me.picInicio.Size = New System.Drawing.Size(483, 610)
        Me.picInicio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom
        Me.picInicio.TabIndex = 2
        Me.picInicio.TabStop = False
        '
        'lblVersion
        '
        Me.lblVersion.AutoSize = True
        Me.lblVersion.ImageAlign = System.Drawing.ContentAlignment.MiddleRight
        Me.lblVersion.Location = New System.Drawing.Point(412, 628)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.Size = New System.Drawing.Size(39, 13)
        Me.lblVersion.TabIndex = 3
        Me.lblVersion.Text = "Label1"
        '
        'FrmInicio
        '
        Me.AcceptButton = Me.CmdAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(507, 667)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblVersion)
        Me.Controls.Add(Me.picInicio)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow
        Me.Location = New System.Drawing.Point(3, 19)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmInicio"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Inicio"
        CType(Me.picInicio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents picInicio As PictureBox
    Friend WithEvents lblVersion As Label
End Class