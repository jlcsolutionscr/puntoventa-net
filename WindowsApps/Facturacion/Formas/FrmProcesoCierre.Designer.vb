<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmProcesoCierre
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
    Friend WithEvents CmdEjecutar As System.Windows.Forms.Button
    Friend WithEvents CmdCambiarFecha As System.Windows.Forms.Button
    Friend WithEvents txtEstado As System.Windows.Forms.TextBox
    Friend WithEvents txtFecha As System.Windows.Forms.TextBox
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents Label1 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.CmdEjecutar = New System.Windows.Forms.Button
        Me.CmdCambiarFecha = New System.Windows.Forms.Button
        Me.txtEstado = New System.Windows.Forms.TextBox
        Me.txtFecha = New System.Windows.Forms.TextBox
        Me.Label2 = New System.Windows.Forms.Label
        Me.Label1 = New System.Windows.Forms.Label
        Me.LblTitulo = New System.Windows.Forms.Label
        Me.SuspendLayout()
        '
        'CmdEjecutar
        '
        Me.CmdEjecutar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdEjecutar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdEjecutar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdEjecutar.Location = New System.Drawing.Point(46, 96)
        Me.CmdEjecutar.Name = "CmdEjecutar"
        Me.CmdEjecutar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdEjecutar.Size = New System.Drawing.Size(113, 23)
        Me.CmdEjecutar.TabIndex = 7
        Me.CmdEjecutar.TabStop = False
        Me.CmdEjecutar.Text = "Ejecuta Proceso"
        Me.CmdEjecutar.UseVisualStyleBackColor = False
        '
        'CmdCambiarFecha
        '
        Me.CmdCambiarFecha.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdCambiarFecha.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdCambiarFecha.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdCambiarFecha.Location = New System.Drawing.Point(166, 96)
        Me.CmdCambiarFecha.Name = "CmdCambiarFecha"
        Me.CmdCambiarFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdCambiarFecha.Size = New System.Drawing.Size(113, 23)
        Me.CmdCambiarFecha.TabIndex = 6
        Me.CmdCambiarFecha.TabStop = False
        Me.CmdCambiarFecha.Text = "Cambio de Fecha"
        Me.CmdCambiarFecha.UseVisualStyleBackColor = False
        '
        'txtEstado
        '
        Me.txtEstado.AcceptsReturn = True
        Me.txtEstado.BackColor = System.Drawing.SystemColors.Window
        Me.txtEstado.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtEstado.Enabled = False
        Me.txtEstado.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtEstado.Location = New System.Drawing.Point(176, 64)
        Me.txtEstado.MaxLength = 0
        Me.txtEstado.Name = "txtEstado"
        Me.txtEstado.ReadOnly = True
        Me.txtEstado.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtEstado.Size = New System.Drawing.Size(105, 20)
        Me.txtEstado.TabIndex = 4
        Me.txtEstado.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.Enabled = False
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(176, 40)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(105, 20)
        Me.txtFecha.TabIndex = 2
        Me.txtFecha.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(40, 64)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(129, 17)
        Me.Label2.TabIndex = 5
        Me.Label2.Text = "Estado del proceso:"
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(40, 40)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(129, 17)
        Me.Label1.TabIndex = 3
        Me.Label1.Text = "Fecha de proceso:"
        '
        'LblTitulo
        '
        Me.LblTitulo.BackColor = System.Drawing.Color.Transparent
        Me.LblTitulo.Cursor = System.Windows.Forms.Cursors.Default
        Me.LblTitulo.Font = New System.Drawing.Font("Tahoma", 8.25!, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.LblTitulo.ForeColor = System.Drawing.SystemColors.ControlText
        Me.LblTitulo.Location = New System.Drawing.Point(114, 9)
        Me.LblTitulo.Name = "LblTitulo"
        Me.LblTitulo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LblTitulo.Size = New System.Drawing.Size(96, 17)
        Me.LblTitulo.TabIndex = 8
        Me.LblTitulo.Text = "Proceso Diario"
        Me.LblTitulo.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmProcesoCierre
        '
        Me.AcceptButton = Me.CmdEjecutar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(325, 136)
        Me.Controls.Add(Me.LblTitulo)
        Me.Controls.Add(Me.CmdEjecutar)
        Me.Controls.Add(Me.CmdCambiarFecha)
        Me.Controls.Add(Me.txtEstado)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.Label1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmProcesoCierre"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Cálculo de interés Mensual"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents LblTitulo As System.Windows.Forms.Label
End Class