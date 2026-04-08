<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmDescargaActualizacion
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
    Public WithEvents btnCancelar As System.Windows.Forms.Button
    Public WithEvents btnRegistrar As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnRegistrar = New System.Windows.Forms.Button()
        Me.pgbProgresoDescarga = New System.Windows.Forms.ProgressBar()
        Me.lblEtiqueta = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnCancelar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnCancelar.DialogResult = System.Windows.Forms.DialogResult.Cancel
        Me.btnCancelar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnCancelar.Location = New System.Drawing.Point(301, 295)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnCancelar.Size = New System.Drawing.Size(81, 25)
        Me.btnCancelar.TabIndex = 6
        Me.btnCancelar.TabStop = False
        Me.btnCancelar.Text = "&Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = False
        '
        'btnRegistrar
        '
        Me.btnRegistrar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnRegistrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnRegistrar.Enabled = False
        Me.btnRegistrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnRegistrar.Location = New System.Drawing.Point(214, 295)
        Me.btnRegistrar.Name = "btnRegistrar"
        Me.btnRegistrar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnRegistrar.Size = New System.Drawing.Size(81, 25)
        Me.btnRegistrar.TabIndex = 5
        Me.btnRegistrar.Text = "&Registrar"
        Me.btnRegistrar.UseVisualStyleBackColor = False
        '
        'pgbProgresoDescarga
        '
        Me.pgbProgresoDescarga.Location = New System.Drawing.Point(12, 64)
        Me.pgbProgresoDescarga.Name = "pgbProgresoDescarga"
        Me.pgbProgresoDescarga.Size = New System.Drawing.Size(460, 23)
        Me.pgbProgresoDescarga.TabIndex = 7
        '
        'lblEtiqueta
        '
        Me.lblEtiqueta.AutoSize = True
        Me.lblEtiqueta.Font = New System.Drawing.Font("Microsoft Sans Serif", 12.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.lblEtiqueta.Location = New System.Drawing.Point(89, 31)
        Me.lblEtiqueta.Name = "lblEtiqueta"
        Me.lblEtiqueta.Size = New System.Drawing.Size(311, 20)
        Me.lblEtiqueta.TabIndex = 8
        Me.lblEtiqueta.Text = "Descarga en progreso. Por favor espere."
        '
        'FrmDescargaActualizacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.CancelButton = Me.btnCancelar
        Me.ClientSize = New System.Drawing.Size(484, 112)
        Me.ControlBox = False
        Me.Controls.Add(Me.lblEtiqueta)
        Me.Controls.Add(Me.pgbProgresoDescarga)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnRegistrar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(500, 150)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(500, 150)
        Me.Name = "FrmDescargaActualizacion"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización del sistema"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents pgbProgresoDescarga As ProgressBar
    Friend WithEvents lblEtiqueta As Label
End Class