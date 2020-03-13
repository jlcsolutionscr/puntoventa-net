<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmMotivoAnulacion
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
    Public ToolTip1 As System.Windows.Forms.ToolTip
    Public WithEvents CmdCancelar As System.Windows.Forms.Button
    Public WithEvents CmdAceptar As System.Windows.Forms.Button
    Public WithEvents TxtMotivo As System.Windows.Forms.TextBox
    Public WithEvents LblUsuario As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ToolTip1 = New System.Windows.Forms.ToolTip(Me.components)
        Me.CmdCancelar = New System.Windows.Forms.Button()
        Me.CmdAceptar = New System.Windows.Forms.Button()
        Me.TxtMotivo = New System.Windows.Forms.TextBox()
        Me.LblUsuario = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'CmdCancelar
        '
        Me.CmdCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdCancelar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.CmdCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCancelar.Location = New System.Drawing.Point(322, 59)
        Me.CmdCancelar.Name = "CmdCancelar"
        Me.CmdCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCancelar.Size = New System.Drawing.Size(81, 25)
        Me.CmdCancelar.TabIndex = 3
        Me.CmdCancelar.Text = "&Cancelar"
        Me.CmdCancelar.UseVisualStyleBackColor = False
        '
        'CmdAceptar
        '
        Me.CmdAceptar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAceptar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAceptar.DialogResult = System.Windows.Forms.DialogResult.OK
        Me.CmdAceptar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAceptar.Location = New System.Drawing.Point(226, 59)
        Me.CmdAceptar.Name = "CmdAceptar"
        Me.CmdAceptar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAceptar.Size = New System.Drawing.Size(81, 25)
        Me.CmdAceptar.TabIndex = 2
        Me.CmdAceptar.Text = "&Aceptar"
        Me.CmdAceptar.UseVisualStyleBackColor = False
        '
        'TxtMotivo
        '
        Me.TxtMotivo.AcceptsReturn = True
        Me.TxtMotivo.BackColor = System.Drawing.SystemColors.Window
        Me.TxtMotivo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.TxtMotivo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.TxtMotivo.Location = New System.Drawing.Point(121, 17)
        Me.TxtMotivo.MaxLength = 100
        Me.TxtMotivo.Multiline = True
        Me.TxtMotivo.Name = "TxtMotivo"
        Me.TxtMotivo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.TxtMotivo.Size = New System.Drawing.Size(479, 36)
        Me.TxtMotivo.TabIndex = 0
        '
        'LblUsuario
        '
        Me.LblUsuario.BackColor = System.Drawing.Color.Transparent
        Me.LblUsuario.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblUsuario.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblUsuario.Location = New System.Drawing.Point(15, 18)
        Me.LblUsuario.Name = "LblUsuario"
        Me.LblUsuario.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblUsuario.Size = New System.Drawing.Size(100, 17)
        Me.LblUsuario.TabIndex = 4
        Me.LblUsuario.Text = "Motivo anulación:"
        Me.LblUsuario.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmMotivoAnulacion
        '
        Me.AcceptButton = Me.CmdAceptar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.CmdCancelar
        Me.ClientSize = New System.Drawing.Size(615, 95)
        Me.ControlBox = False
        Me.Controls.Add(Me.CmdCancelar)
        Me.Controls.Add(Me.CmdAceptar)
        Me.Controls.Add(Me.TxtMotivo)
        Me.Controls.Add(Me.LblUsuario)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMotivoAnulacion"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingrese el motivo de anulación del registro (opcional)"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
End Class