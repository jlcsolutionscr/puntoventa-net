<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmBusquedaNotaCredito
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
    Public WithEvents btnAplicar As System.Windows.Forms.Button
    Public WithEvents txtDetalle As System.Windows.Forms.TextBox
    Public WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmBusquedaNotaCredito))
        Me.btnAplicar = New System.Windows.Forms.Button()
        Me.txtDetalle = New System.Windows.Forms.TextBox()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.txtSaldo = New System.Windows.Forms.TextBox()
        Me.Label3 = New System.Windows.Forms.Label()
        Me.txtMontoOriginal = New System.Windows.Forms.TextBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me._lblLabels_3 = New System.Windows.Forms.Label()
        Me.SuspendLayout()
        '
        'btnAplicar
        '
        Me.btnAplicar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAplicar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAplicar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAplicar.Location = New System.Drawing.Point(476, 12)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAplicar.Size = New System.Drawing.Size(64, 21)
        Me.btnAplicar.TabIndex = 0
        Me.btnAplicar.TabStop = False
        Me.btnAplicar.Text = "Aplicar"
        Me.btnAplicar.UseVisualStyleBackColor = False
        '
        'txtDetalle
        '
        Me.txtDetalle.AcceptsReturn = True
        Me.txtDetalle.BackColor = System.Drawing.SystemColors.Window
        Me.txtDetalle.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDetalle.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDetalle.Location = New System.Drawing.Point(106, 116)
        Me.txtDetalle.MaxLength = 0
        Me.txtDetalle.Name = "txtDetalle"
        Me.txtDetalle.ReadOnly = True
        Me.txtDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDetalle.Size = New System.Drawing.Size(434, 20)
        Me.txtDetalle.TabIndex = 4
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_5.Location = New System.Drawing.Point(10, 116)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(89, 19)
        Me._lblLabels_5.TabIndex = 15
        Me._lblLabels_5.Text = "Detalle:"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(31, 13)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(69, 19)
        Me._lblLabels_2.TabIndex = 9
        Me._lblLabels_2.Text = "Referencia:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtId
        '
        Me.txtId.AcceptsReturn = True
        Me.txtId.BackColor = System.Drawing.SystemColors.Window
        Me.txtId.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtId.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtId.Location = New System.Drawing.Point(106, 12)
        Me.txtId.MaxLength = 0
        Me.txtId.Name = "txtId"
        Me.txtId.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtId.Size = New System.Drawing.Size(73, 20)
        Me.txtId.TabIndex = 0
        Me.txtId.TabStop = False
        '
        'btnBuscar
        '
        Me.btnBuscar.Image = CType(resources.GetObject("btnBuscar.Image"), System.Drawing.Image)
        Me.btnBuscar.Location = New System.Drawing.Point(185, 13)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscar.TabIndex = 0
        Me.btnBuscar.TabStop = False
        Me.btnBuscar.UseVisualStyleBackColor = True
        '
        'txtSaldo
        '
        Me.txtSaldo.AcceptsReturn = True
        Me.txtSaldo.BackColor = System.Drawing.SystemColors.Window
        Me.txtSaldo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtSaldo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtSaldo.Location = New System.Drawing.Point(106, 90)
        Me.txtSaldo.MaxLength = 0
        Me.txtSaldo.Name = "txtSaldo"
        Me.txtSaldo.ReadOnly = True
        Me.txtSaldo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtSaldo.Size = New System.Drawing.Size(107, 20)
        Me.txtSaldo.TabIndex = 153
        Me.txtSaldo.TabStop = False
        Me.txtSaldo.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label3
        '
        Me.Label3.BackColor = System.Drawing.Color.Transparent
        Me.Label3.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label3.Location = New System.Drawing.Point(27, 91)
        Me.Label3.Name = "Label3"
        Me.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label3.Size = New System.Drawing.Size(73, 19)
        Me.Label3.TabIndex = 154
        Me.Label3.Text = "Saldo actual:"
        Me.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtMontoOriginal
        '
        Me.txtMontoOriginal.AcceptsReturn = True
        Me.txtMontoOriginal.BackColor = System.Drawing.SystemColors.Window
        Me.txtMontoOriginal.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtMontoOriginal.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtMontoOriginal.Location = New System.Drawing.Point(106, 64)
        Me.txtMontoOriginal.MaxLength = 0
        Me.txtMontoOriginal.Name = "txtMontoOriginal"
        Me.txtMontoOriginal.ReadOnly = True
        Me.txtMontoOriginal.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtMontoOriginal.Size = New System.Drawing.Size(107, 20)
        Me.txtMontoOriginal.TabIndex = 155
        Me.txtMontoOriginal.TabStop = False
        Me.txtMontoOriginal.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(20, 64)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(80, 19)
        Me.Label6.TabIndex = 156
        Me.Label6.Text = "Monto original:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(106, 38)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(73, 20)
        Me.txtFecha.TabIndex = 157
        Me.txtFecha.TabStop = False
        '
        '_lblLabels_3
        '
        Me._lblLabels_3.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_3.Location = New System.Drawing.Point(20, 38)
        Me._lblLabels_3.Name = "_lblLabels_3"
        Me._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_3.Size = New System.Drawing.Size(79, 19)
        Me._lblLabels_3.TabIndex = 158
        Me._lblLabels_3.Text = "Fecha emision:"
        Me._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmAplicaNotaCredito
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(552, 150)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me._lblLabels_3)
        Me.Controls.Add(Me.txtMontoOriginal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSaldo)
        Me.Controls.Add(Me.Label3)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnAplicar)
        Me.Controls.Add(Me.txtDetalle)
        Me.Controls.Add(Me._lblLabels_5)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(568, 189)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(568, 189)
        Me.Name = "FrmAplicaNotaCredito"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aplicar Nota de Credito"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Public WithEvents txtId As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents txtSaldo As System.Windows.Forms.TextBox
    Public WithEvents Label3 As System.Windows.Forms.Label
    Public WithEvents txtMontoOriginal As System.Windows.Forms.TextBox
    Public WithEvents Label6 As System.Windows.Forms.Label
    Public WithEvents txtFecha As TextBox
    Public WithEvents _lblLabels_3 As Label
End Class