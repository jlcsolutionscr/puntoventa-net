<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmMenuReportesContables
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
    Public WithEvents LstReporte As System.Windows.Forms.ListBox
    Public WithEvents CmdVistaPrevia As System.Windows.Forms.Button
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.LstReporte = New System.Windows.Forms.ListBox()
        Me.CmdVistaPrevia = New System.Windows.Forms.Button()
        Me.dtpFechaInicial = New System.Windows.Forms.DateTimePicker()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.dtpFechaFinal = New System.Windows.Forms.DateTimePicker()
        Me.SuspendLayout()
        '
        'LstReporte
        '
        Me.LstReporte.BackColor = System.Drawing.SystemColors.Window
        Me.LstReporte.Cursor = System.Windows.Forms.Cursors.Default
        Me.LstReporte.ForeColor = System.Drawing.SystemColors.WindowText
        Me.LstReporte.Items.AddRange(New Object() {"Detalle de Movimientos de Diario", "Balance de Comprobación Histórico", "Balance de Comprobación Actual", "Balance de Perdidas y Ganancias", "Detalle de movimientos de Cuentas de Balance"})
        Me.LstReporte.Location = New System.Drawing.Point(12, 71)
        Me.LstReporte.Name = "LstReporte"
        Me.LstReporte.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.LstReporte.Size = New System.Drawing.Size(228, 186)
        Me.LstReporte.TabIndex = 4
        Me.LstReporte.TabStop = False
        '
        'CmdVistaPrevia
        '
        Me.CmdVistaPrevia.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.CmdVistaPrevia.Cursor = System.Windows.Forms.Cursors.Default
        Me.CmdVistaPrevia.ForeColor = System.Drawing.SystemColors.ControlText
        Me.CmdVistaPrevia.Location = New System.Drawing.Point(167, 12)
        Me.CmdVistaPrevia.Name = "CmdVistaPrevia"
        Me.CmdVistaPrevia.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.CmdVistaPrevia.Size = New System.Drawing.Size(73, 24)
        Me.CmdVistaPrevia.TabIndex = 5
        Me.CmdVistaPrevia.TabStop = False
        Me.CmdVistaPrevia.Text = "&Vista Previa"
        Me.CmdVistaPrevia.UseVisualStyleBackColor = False
        '
        'dtpFechaInicial
        '
        Me.dtpFechaInicial.CustomFormat = "MM/yyyy"
        Me.dtpFechaInicial.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaInicial.Location = New System.Drawing.Point(79, 16)
        Me.dtpFechaInicial.Name = "dtpFechaInicial"
        Me.dtpFechaInicial.Size = New System.Drawing.Size(82, 20)
        Me.dtpFechaInicial.TabIndex = 6
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(10, 20)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(69, 13)
        Me.Label1.TabIndex = 7
        Me.Label1.Text = "Fecha inicial:"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(17, 46)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(62, 13)
        Me.Label2.TabIndex = 9
        Me.Label2.Text = "Fecha final:"
        '
        'dtpFechaFinal
        '
        Me.dtpFechaFinal.CustomFormat = "MM/yyyy"
        Me.dtpFechaFinal.Format = System.Windows.Forms.DateTimePickerFormat.[Short]
        Me.dtpFechaFinal.Location = New System.Drawing.Point(79, 42)
        Me.dtpFechaFinal.Name = "dtpFechaFinal"
        Me.dtpFechaFinal.Size = New System.Drawing.Size(82, 20)
        Me.dtpFechaFinal.TabIndex = 8
        '
        'FrmMenuReportesContables
        '
        Me.AcceptButton = Me.CmdVistaPrevia
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(253, 270)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.dtpFechaFinal)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.dtpFechaInicial)
        Me.Controls.Add(Me.LstReporte)
        Me.Controls.Add(Me.CmdVistaPrevia)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.Location = New System.Drawing.Point(3, 22)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "FrmMenuReportesContables"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Menu de Reportes"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents dtpFechaInicial As System.Windows.Forms.DateTimePicker
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents dtpFechaFinal As System.Windows.Forms.DateTimePicker
End Class