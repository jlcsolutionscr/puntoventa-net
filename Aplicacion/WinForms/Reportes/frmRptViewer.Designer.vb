<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class frmRptViewer
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
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.crtViewer = New CrystalDecisions.Windows.Forms.CrystalReportViewer
        Me.SuspendLayout()
        '
        'crtViewer
        '
        Me.crtViewer.ActiveViewIndex = -1
        Me.crtViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle
        Me.crtViewer.ToolPanelView = CrystalDecisions.Windows.Forms.ToolPanelViewType.None
        Me.crtViewer.Dock = System.Windows.Forms.DockStyle.Fill
        Me.crtViewer.Location = New System.Drawing.Point(0, 0)
        Me.crtViewer.Name = "crtViewer"
        Me.crtViewer.SelectionFormula = ""
        Me.crtViewer.Size = New System.Drawing.Size(407, 266)
        Me.crtViewer.TabIndex = 0
        Me.crtViewer.ViewTimeSelectionFormula = ""
        '
        'frmRptViewer
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(407, 266)
        Me.Controls.Add(Me.crtViewer)
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "frmRptViewer"
        Me.Text = "BackGround Report"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents crtViewer As CrystalDecisions.Windows.Forms.CrystalReportViewer
End Class
