<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAnulaReciboCxC
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
    Public WithEvents CmdAnular As System.Windows.Forms.Button
    Public WithEvents _lblLabels_2 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(FrmAnulaReciboCxC))
        Me.CmdAnular = New System.Windows.Forms.Button()
        Me._lblLabels_2 = New System.Windows.Forms.Label()
        Me.grdDetalleRecibo = New System.Windows.Forms.DataGridView()
        Me.txtNombreCliente = New System.Windows.Forms.TextBox()
        Me.btnBuscarCliente = New System.Windows.Forms.Button()
        CType(Me.grdDetalleRecibo, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'CmdAnular
        '
        Me.CmdAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdAnular.Location = New System.Drawing.Point(632, 10)
        Me.CmdAnular.Name = "CmdAnular"
        Me.CmdAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdAnular.Size = New System.Drawing.Size(64, 21)
        Me.CmdAnular.TabIndex = 0
        Me.CmdAnular.TabStop = False
        Me.CmdAnular.Text = "&Anular"
        Me.CmdAnular.UseVisualStyleBackColor = False
        '
        '_lblLabels_2
        '
        Me._lblLabels_2.BackColor = System.Drawing.Color.Transparent
        Me._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default
        Me._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText
        Me._lblLabels_2.Location = New System.Drawing.Point(15, 12)
        Me._lblLabels_2.Name = "_lblLabels_2"
        Me._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me._lblLabels_2.Size = New System.Drawing.Size(51, 19)
        Me._lblLabels_2.TabIndex = 9
        Me._lblLabels_2.Text = "Cliente:"
        Me._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleRecibo
        '
        Me.grdDetalleRecibo.AllowUserToAddRows = False
        Me.grdDetalleRecibo.AllowUserToDeleteRows = False
        Me.grdDetalleRecibo.AllowUserToResizeColumns = False
        Me.grdDetalleRecibo.AllowUserToResizeRows = False
        Me.grdDetalleRecibo.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleRecibo.Location = New System.Drawing.Point(12, 38)
        Me.grdDetalleRecibo.MultiSelect = False
        Me.grdDetalleRecibo.Name = "grdDetalleRecibo"
        Me.grdDetalleRecibo.ReadOnly = True
        Me.grdDetalleRecibo.RowHeadersVisible = False
        Me.grdDetalleRecibo.Size = New System.Drawing.Size(684, 252)
        Me.grdDetalleRecibo.StandardTab = True
        Me.grdDetalleRecibo.TabIndex = 3
        Me.grdDetalleRecibo.TabStop = False
        '
        'txtNombreCliente
        '
        Me.txtNombreCliente.AcceptsReturn = True
        Me.txtNombreCliente.BackColor = System.Drawing.SystemColors.Window
        Me.txtNombreCliente.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtNombreCliente.Enabled = False
        Me.txtNombreCliente.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtNombreCliente.Location = New System.Drawing.Point(72, 12)
        Me.txtNombreCliente.MaxLength = 0
        Me.txtNombreCliente.Name = "txtNombreCliente"
        Me.txtNombreCliente.ReadOnly = True
        Me.txtNombreCliente.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtNombreCliente.Size = New System.Drawing.Size(405, 20)
        Me.txtNombreCliente.TabIndex = 0
        Me.txtNombreCliente.TabStop = False
        '
        'btnBuscarCliente
        '
        Me.btnBuscarCliente.Image = CType(resources.GetObject("btnBuscarCliente.Image"), System.Drawing.Image)
        Me.btnBuscarCliente.Location = New System.Drawing.Point(478, 12)
        Me.btnBuscarCliente.Name = "btnBuscarCliente"
        Me.btnBuscarCliente.Size = New System.Drawing.Size(20, 20)
        Me.btnBuscarCliente.TabIndex = 11
        Me.btnBuscarCliente.TabStop = False
        Me.btnBuscarCliente.UseVisualStyleBackColor = True
        '
        'FrmAnulaReciboCxC
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(708, 303)
        Me.Controls.Add(Me.txtNombreCliente)
        Me.Controls.Add(Me.btnBuscarCliente)
        Me.Controls.Add(Me.grdDetalleRecibo)
        Me.Controls.Add(Me.CmdAnular)
        Me.Controls.Add(Me._lblLabels_2)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmAnulaReciboCxC"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Anular Recibo a Cuentas por Cobrar"
        CType(Me.grdDetalleRecibo, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleRecibo As System.Windows.Forms.DataGridView
    Public WithEvents txtNombreCliente As System.Windows.Forms.TextBox
    Friend WithEvents btnBuscarCliente As System.Windows.Forms.Button
End Class