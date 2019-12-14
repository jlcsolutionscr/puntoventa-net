<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()>
Partial Class FrmAplicaTraslado
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
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAplicaTraslado))
        Me.dgvListado = New System.Windows.Forms.DataGridView()
        Me.btnFiltrar = New System.Windows.Forms.Button()
        Me.lblDescripcion = New System.Windows.Forms.Label()
        Me.txtReferencia = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtId = New System.Windows.Forms.TextBox()
        Me.btnAplicar = New System.Windows.Forms.Button()
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'dgvListado
        '
        Me.dgvListado.AllowUserToAddRows = False
        Me.dgvListado.AllowUserToDeleteRows = False
        Me.dgvListado.AllowUserToResizeColumns = False
        Me.dgvListado.AllowUserToResizeRows = False
        Me.dgvListado.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.dgvListado.Location = New System.Drawing.Point(12, 38)
        Me.dgvListado.Name = "dgvListado"
        Me.dgvListado.ReadOnly = True
        Me.dgvListado.RowHeadersVisible = False
        Me.dgvListado.ScrollBars = System.Windows.Forms.ScrollBars.None
        Me.dgvListado.Size = New System.Drawing.Size(620, 310)
        Me.dgvListado.TabIndex = 3
        Me.dgvListado.TabStop = False
        '
        'btnFiltrar
        '
        Me.btnFiltrar.Image = CType(resources.GetObject("btnFiltrar.Image"), System.Drawing.Image)
        Me.btnFiltrar.Location = New System.Drawing.Point(610, 10)
        Me.btnFiltrar.Name = "btnFiltrar"
        Me.btnFiltrar.Size = New System.Drawing.Size(22, 22)
        Me.btnFiltrar.TabIndex = 2
        Me.btnFiltrar.TabStop = False
        Me.btnFiltrar.UseVisualStyleBackColor = True
        '
        'lblDescripcion
        '
        Me.lblDescripcion.AutoSize = True
        Me.lblDescripcion.Location = New System.Drawing.Point(141, 15)
        Me.lblDescripcion.Name = "lblDescripcion"
        Me.lblDescripcion.Size = New System.Drawing.Size(43, 13)
        Me.lblDescripcion.TabIndex = 8
        Me.lblDescripcion.Text = "Detalle:"
        '
        'txtReferencia
        '
        Me.txtReferencia.Location = New System.Drawing.Point(190, 12)
        Me.txtReferencia.Name = "txtReferencia"
        Me.txtReferencia.ReadOnly = True
        Me.txtReferencia.Size = New System.Drawing.Size(414, 20)
        Me.txtReferencia.TabIndex = 1
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 15)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(30, 13)
        Me.Label1.TabIndex = 41
        Me.Label1.Text = "Nro.:"
        '
        'txtId
        '
        Me.txtId.Location = New System.Drawing.Point(48, 12)
        Me.txtId.Name = "txtId"
        Me.txtId.Size = New System.Drawing.Size(87, 20)
        Me.txtId.TabIndex = 0
        '
        'btnAplicar
        '
        Me.btnAplicar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAplicar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAplicar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAplicar.Location = New System.Drawing.Point(540, 354)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAplicar.Size = New System.Drawing.Size(92, 21)
        Me.btnAplicar.TabIndex = 42
        Me.btnAplicar.TabStop = False
        Me.btnAplicar.Text = "Aplicar traslado"
        Me.btnAplicar.UseVisualStyleBackColor = False
        '
        'FrmAplicarTraslado
        '
        Me.AcceptButton = Me.btnFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(644, 387)
        Me.Controls.Add(Me.btnAplicar)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtId)
        Me.Controls.Add(Me.btnFiltrar)
        Me.Controls.Add(Me.lblDescripcion)
        Me.Controls.Add(Me.txtReferencia)
        Me.Controls.Add(Me.dgvListado)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAplicarTraslado"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Aplicación de Traslados"
        CType(Me.dgvListado, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dgvListado As System.Windows.Forms.DataGridView
    Friend WithEvents btnFiltrar As System.Windows.Forms.Button
    Friend WithEvents lblDescripcion As System.Windows.Forms.Label
    Friend WithEvents txtReferencia As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtId As System.Windows.Forms.TextBox
    Public WithEvents btnAplicar As Button
End Class