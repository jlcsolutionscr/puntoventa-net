<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmActualizarPrecioProducto
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
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.CmdFiltrar = New System.Windows.Forms.Button()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me._lblLabels_0 = New System.Windows.Forms.Label()
        Me._lblLabels_5 = New System.Windows.Forms.Label()
        Me._lblLabels_1 = New System.Windows.Forms.Label()
        Me.GrdDetalle = New System.Windows.Forms.DataGridView()
        Me.btnAplicar = New System.Windows.Forms.Button()
        Me.cboLinea = New System.Windows.Forms.ComboBox()
        Me.txtPorcentaje = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        CType(Me.GrdDetalle, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'txtCodigo
        '
        Me.txtCodigo.AcceptsReturn = True
        Me.txtCodigo.BackColor = System.Drawing.SystemColors.Window
        Me.txtCodigo.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCodigo.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCodigo.Location = New System.Drawing.Point(80, 36)
        Me.txtCodigo.MaxLength = 0
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCodigo.Size = New System.Drawing.Size(187, 20)
        Me.txtCodigo.TabIndex = 1
        '
        'CmdFiltrar
        '
        Me.CmdFiltrar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdFiltrar.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdFiltrar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdFiltrar.Location = New System.Drawing.Point(392, 9)
        Me.CmdFiltrar.Name = "CmdFiltrar"
        Me.CmdFiltrar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdFiltrar.Size = New System.Drawing.Size(55, 21)
        Me.CmdFiltrar.TabIndex = 3
        Me.CmdFiltrar.TabStop = False
        Me.CmdFiltrar.Text = "Filtrar"
        Me.CmdFiltrar.UseVisualStyleBackColor = False
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(80, 62)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(528, 20)
        Me.txtDescripcion.TabIndex = 2
        '
        '_lblLabels_0
        '
        Me._lblLabels_0.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_0.Location = New System.Drawing.Point(9, 8)
        Me._lblLabels_0.Name = "_lblLabels_0"
        Me._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_0.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_0.TabIndex = 7
        Me._lblLabels_0.Text = "Línea:"
        Me._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_5
        '
        Me._lblLabels_5.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_5.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_5.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_5.Location = New System.Drawing.Point(9, 62)
        Me._lblLabels_5.Name = "_lblLabels_5"
        Me._lblLabels_5.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_5.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_5.TabIndex = 6
        Me._lblLabels_5.Text = "Descripción"
        Me._lblLabels_5.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        '_lblLabels_1
        '
        Me._lblLabels_1.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_1.Location = New System.Drawing.Point(9, 36)
        Me._lblLabels_1.Name = "_lblLabels_1"
        Me._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_1.Size = New System.Drawing.Size(65, 19)
        Me._lblLabels_1.TabIndex = 5
        Me._lblLabels_1.Text = "Código:"
        Me._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'GrdDetalle
        '
        Me.GrdDetalle.AllowUserToAddRows = False
        Me.GrdDetalle.AllowUserToDeleteRows = False
        Me.GrdDetalle.AllowUserToResizeColumns = False
        Me.GrdDetalle.AllowUserToResizeRows = False
        Me.GrdDetalle.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing
        Me.GrdDetalle.Location = New System.Drawing.Point(8, 90)
        Me.GrdDetalle.Name = "GrdDetalle"
        Me.GrdDetalle.ReadOnly = True
        Me.GrdDetalle.RowHeadersVisible = False
        Me.GrdDetalle.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect
        Me.GrdDetalle.Size = New System.Drawing.Size(599, 281)
        Me.GrdDetalle.TabIndex = 8
        Me.GrdDetalle.TabStop = False
        '
        'btnAplicar
        '
        Me.btnAplicar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAplicar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAplicar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAplicar.Location = New System.Drawing.Point(516, 36)
        Me.btnAplicar.Name = "btnAplicar"
        Me.btnAplicar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAplicar.Size = New System.Drawing.Size(91, 21)
        Me.btnAplicar.TabIndex = 5
        Me.btnAplicar.TabStop = False
        Me.btnAplicar.Text = "Aplicar"
        Me.btnAplicar.UseVisualStyleBackColor = False
        '
        'cboLinea
        '
        Me.cboLinea.FormattingEnabled = True
        Me.cboLinea.Location = New System.Drawing.Point(80, 9)
        Me.cboLinea.Name = "cboLinea"
        Me.cboLinea.Size = New System.Drawing.Size(306, 21)
        Me.cboLinea.TabIndex = 0
        '
        'txtPorcentaje
        '
        Me.txtPorcentaje.AcceptsReturn = True
        Me.txtPorcentaje.BackColor = System.Drawing.SystemColors.Window
        Me.txtPorcentaje.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPorcentaje.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPorcentaje.Location = New System.Drawing.Point(551, 10)
        Me.txtPorcentaje.MaxLength = 0
        Me.txtPorcentaje.Name = "txtPorcentaje"
        Me.txtPorcentaje.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPorcentaje.Size = New System.Drawing.Size(56, 20)
        Me.txtPorcentaje.TabIndex = 4
        Me.txtPorcentaje.TabStop = False
        Me.txtPorcentaje.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(453, 10)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(92, 19)
        Me.Label1.TabIndex = 12
        Me.Label1.Text = "Porc. Incremento:"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'FrmActualizarPrecioProducto
        '
        Me.AcceptButton = Me.CmdFiltrar
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(616, 383)
        Me.Controls.Add(Me.txtPorcentaje)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboLinea)
        Me.Controls.Add(Me.btnAplicar)
        Me.Controls.Add(Me.GrdDetalle)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.CmdFiltrar)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me._lblLabels_0)
        Me.Controls.Add(Me._lblLabels_5)
        Me.Controls.Add(Me._lblLabels_1)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmActualizarPrecioProducto"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Actualización de Precio de Venta de Productos"
        CType(Me.GrdDetalle, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents GrdDetalle As System.Windows.Forms.DataGridView
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Friend WithEvents CmdFiltrar As System.Windows.Forms.Button
    Friend WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Friend WithEvents _lblLabels_0 As System.Windows.Forms.Label
    Friend WithEvents _lblLabels_5 As System.Windows.Forms.Label
    Friend WithEvents _lblLabels_1 As System.Windows.Forms.Label
    Friend WithEvents btnAplicar As System.Windows.Forms.Button
    Friend WithEvents cboLinea As System.Windows.Forms.ComboBox
    Friend WithEvents txtPorcentaje As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
End Class