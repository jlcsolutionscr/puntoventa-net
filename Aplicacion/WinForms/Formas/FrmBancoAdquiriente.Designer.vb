<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmBancoAdquiriente
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
        Me.txtIdBanco = New System.Windows.Forms.TextBox()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_9 = New System.Windows.Forms.Label()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.txtRetencion = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtComision = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
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
        Me.txtDescripcion.Location = New System.Drawing.Point(92, 91)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(260, 20)
        Me.txtDescripcion.TabIndex = 2
        '
        'txtIdBanco
        '
        Me.txtIdBanco.AcceptsReturn = True
        Me.txtIdBanco.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdBanco.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdBanco.Enabled = False
        Me.txtIdBanco.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdBanco.Location = New System.Drawing.Point(94, 38)
        Me.txtIdBanco.MaxLength = 0
        Me.txtIdBanco.Name = "txtIdBanco"
        Me.txtIdBanco.ReadOnly = True
        Me.txtIdBanco.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdBanco.Size = New System.Drawing.Size(37, 20)
        Me.txtIdBanco.TabIndex = 0
        Me.txtIdBanco.TabStop = False
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(17, 92)
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
        Me._lblLabels_0.Text = "Id:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_9
        '
        Me._lblLabels_9.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_9.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_9.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_9.Location = New System.Drawing.Point(38, 65)
        Me._lblLabels_9.Name = "_lblLabels_9"
        Me._lblLabels_9.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_9.Size = New System.Drawing.Size(50, 17)
        Me._lblLabels_9.TabIndex = 61
        Me._lblLabels_9.Text = "Código:"
        Me._lblLabels_9.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtCodigo
        '
        Me.txtCodigo.AcceptsReturn = True
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigo.Location = New System.Drawing.Point(92, 64)
        Me.txtCodigo.MaxLength = 0
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigo.Size = New System.Drawing.Size(171, 20)
        Me.txtCodigo.TabIndex = 1
        '
        'txtRetencion
        '
        Me.txtRetencion.AcceptsReturn = True
        Me.txtRetencion.BackColor = System.Drawing.SystemColors.Window
        Me.txtRetencion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtRetencion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtRetencion.Location = New System.Drawing.Point(92, 117)
        Me.txtRetencion.MaxLength = 0
        Me.txtRetencion.Name = "txtRetencion"
        Me.txtRetencion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtRetencion.Size = New System.Drawing.Size(55, 20)
        Me.txtRetencion.TabIndex = 3
        Me.txtRetencion.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(1, 118)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(87, 17)
        Me.Label1.TabIndex = 63
        Me.Label1.Text = "% Retención:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtComision
        '
        Me.txtComision.AcceptsReturn = True
        Me.txtComision.BackColor = System.Drawing.SystemColors.Window
        Me.txtComision.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtComision.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtComision.Location = New System.Drawing.Point(92, 143)
        Me.txtComision.MaxLength = 0
        Me.txtComision.Name = "txtComision"
        Me.txtComision.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtComision.Size = New System.Drawing.Size(55, 20)
        Me.txtComision.TabIndex = 4
        Me.txtComision.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(4, 144)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(84, 17)
        Me.Label2.TabIndex = 65
        Me.Label2.Text = "% Comisión:"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmBancoAdquiriente
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(364, 174)
        Me.Controls.Add(Me.txtComision)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtRetencion)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me._lblLabels_9)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtIdBanco)
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
        Me.Name = "FrmBancoAdquiriente"
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
    Public WithEvents txtIdBanco As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_9 As System.Windows.Forms.Label
    Public WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents txtRetencion As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtComision As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
End Class