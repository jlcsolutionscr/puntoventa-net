<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCuentaEgreso
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Me.components = New System.ComponentModel.Container()
        Me.ClienteBindingSource = New System.Windows.Forms.BindingSource(Me.components)
        Me.btnCancelar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtIdCuenta = New System.Windows.Forms.TextBox()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        CType(Me.ClienteBindingSource, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnCancelar
        '
        Me.btnCancelar.Location = New System.Drawing.Point(94, 10)
        Me.btnCancelar.Name = "btnCancelar"
        Me.btnCancelar.Size = New System.Drawing.Size(78, 22)
        Me.btnCancelar.TabIndex = 50
        Me.btnCancelar.Text = "Cancelar"
        Me.btnCancelar.UseVisualStyleBackColor = True
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(10, 10)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(78, 22)
        Me.btnGuardar.TabIndex = 49
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(94, 64)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Multiline = True
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(360, 35)
        Me.txtDescripcion.TabIndex = 54
        '
        'txtIdCuenta
        '
        Me.txtIdCuenta.AcceptsReturn = True
        Me.txtIdCuenta.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdCuenta.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdCuenta.Enabled = False
        Me.txtIdCuenta.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdCuenta.Location = New System.Drawing.Point(94, 38)
        Me.txtIdCuenta.MaxLength = 0
        Me.txtIdCuenta.Name = "txtIdCuenta"
        Me.txtIdCuenta.ReadOnly = True
        Me.txtIdCuenta.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdCuenta.Size = New System.Drawing.Size(37, 20)
        Me.txtIdCuenta.TabIndex = 51
        Me.txtIdCuenta.TabStop = False
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(19, 65)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(69, 17)
        Me._lblLabels_1.TabIndex = 56
        Me._lblLabels_1.Text = "Descripción:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(19, 39)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(69, 17)
        Me._lblLabels_0.TabIndex = 53
        Me._lblLabels_0.Text = "Cuenta:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmCuentaEgreso
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(464, 139)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtIdCuenta)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Controls.Add(Me.btnCancelar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmCuentaEgreso"
        Me.ShowInTaskbar = False
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Datos"
        CType(Me.ClienteBindingSource, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ClienteBindingSource As System.Windows.Forms.BindingSource
    Friend WithEvents btnCancelar As System.Windows.Forms.Button
    Friend WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtIdCuenta As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
End Class