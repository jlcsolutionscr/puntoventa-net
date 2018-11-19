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
    Public WithEvents CmdClave As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmInicio))
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.CmdClave = New System.Windows.Forms.Button()
        Me.Panel1 = New System.Windows.Forms.Panel()
        Me.lblVersion = New System.Windows.Forms.Label()
        Me.ImgInicio = New System.Windows.Forms.PictureBox()
        Me.Panel1.SuspendLayout()
        CType(Me.ImgInicio, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAceptar.Location = New System.Drawing.Point(145, 265)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAceptar.Size = New System.Drawing.Size(97, 25)
        Me.CmdAceptar.TabIndex = 0
        Me.CmdAceptar.Text = "&Aceptar"
        Me.CmdAceptar.UseVisualStyleBackColor = False
        '
        'CmdClave
        '
        Me.CmdClave.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdClave.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdClave.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdClave.Location = New System.Drawing.Point(33, 265)
        Me.CmdClave.Name = "CmdClave"
        Me.CmdClave.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdClave.Size = New System.Drawing.Size(97, 25)
        Me.CmdClave.TabIndex = 1
        Me.CmdClave.Text = "Cambiar Clave"
        Me.CmdClave.UseVisualStyleBackColor = False
        '
        'Panel1
        '
        Me.Panel1.BackColor = System.Drawing.SystemColors.MenuHighlight
        Me.Panel1.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D
        Me.Panel1.Controls.Add(Me.lblVersion)
        Me.Panel1.Controls.Add(Me.ImgInicio)
        Me.Panel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Panel1.Location = New System.Drawing.Point(12, 12)
        Me.Panel1.Name = "Panel1"
        Me.Panel1.Size = New System.Drawing.Size(254, 227)
        Me.Panel1.TabIndex = 7
        '
        'lblVersion
        '
        Me.lblVersion.BackColor = System.Drawing.Color.Transparent
        Me.lblVersion.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblVersion.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblVersion.Location = New System.Drawing.Point(157, 205)
        Me.lblVersion.Name = "lblVersion"
        Me.lblVersion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblVersion.Size = New System.Drawing.Size(81, 17)
        Me.lblVersion.TabIndex = 7
        Me.lblVersion.Text = "Label1"
        Me.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopRight
        '
        'ImgInicio
        '
        Me.ImgInicio.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.ImgInicio.Cursor = System.Windows.Forms.Cursors.Default
        Me.ImgInicio.ErrorImage = Nothing
        Me.ImgInicio.Image = CType(resources.GetObject("ImgInicio.Image"), System.Drawing.Image)
        Me.ImgInicio.Location = New System.Drawing.Point(16, 17)
        Me.ImgInicio.Name = "ImgInicio"
        Me.ImgInicio.Size = New System.Drawing.Size(222, 176)
        Me.ImgInicio.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage
        Me.ImgInicio.TabIndex = 8
        Me.ImgInicio.TabStop = False
        '
        'FrmInicio
        '
        Me.AcceptButton = Me.CmdAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.ControlLight
        Me.ClientSize = New System.Drawing.Size(278, 302)
        Me.ControlBox = False
        Me.Controls.Add(Me.Panel1)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Controls.Add(Me.CmdClave)
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
        Me.Panel1.ResumeLayout(False)
        CType(Me.ImgInicio, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents Panel1 As System.Windows.Forms.Panel
    Public WithEvents lblVersion As System.Windows.Forms.Label
    Public WithEvents ImgInicio As System.Windows.Forms.PictureBox
End Class