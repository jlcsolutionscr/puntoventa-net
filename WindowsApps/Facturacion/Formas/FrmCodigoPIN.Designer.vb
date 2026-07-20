<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmCodigoPIN
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtPIN1 = New System.Windows.Forms.TextBox()
        Me.txtPIN2 = New System.Windows.Forms.TextBox()
        Me.txtPIN3 = New System.Windows.Forms.TextBox()
        Me.txtPIN4 = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'txtPIN1
        '
        Me.txtPIN1.Font = New System.Drawing.Font("Microsoft Sans Serif", 80.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIN1.Location = New System.Drawing.Point(63, 54)
        Me.txtPIN1.MaxLength = 1
        Me.txtPIN1.Name = "txtPIN1"
        Me.txtPIN1.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPIN1.Size = New System.Drawing.Size(80, 128)
        Me.txtPIN1.TabIndex = 0
        '
        'txtPIN2
        '
        Me.txtPIN2.Font = New System.Drawing.Font("Microsoft Sans Serif", 80.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIN2.Location = New System.Drawing.Point(149, 54)
        Me.txtPIN2.MaxLength = 1
        Me.txtPIN2.Name = "txtPIN2"
        Me.txtPIN2.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPIN2.Size = New System.Drawing.Size(80, 128)
        Me.txtPIN2.TabIndex = 1
        '
        'txtPIN3
        '
        Me.txtPIN3.Font = New System.Drawing.Font("Microsoft Sans Serif", 80.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIN3.Location = New System.Drawing.Point(235, 54)
        Me.txtPIN3.MaxLength = 1
        Me.txtPIN3.Name = "txtPIN3"
        Me.txtPIN3.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPIN3.Size = New System.Drawing.Size(80, 128)
        Me.txtPIN3.TabIndex = 2
        '
        'txtPIN4
        '
        Me.txtPIN4.Font = New System.Drawing.Font("Microsoft Sans Serif", 80.0!, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, CType(0, Byte))
        Me.txtPIN4.Location = New System.Drawing.Point(321, 54)
        Me.txtPIN4.MaxLength = 1
        Me.txtPIN4.Name = "txtPIN4"
        Me.txtPIN4.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPIN4.Size = New System.Drawing.Size(80, 128)
        Me.txtPIN4.TabIndex = 3
        '
        'FrmCodigoPIN
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(468, 242)
        Me.Controls.Add(Me.txtPIN4)
        Me.Controls.Add(Me.txtPIN3)
        Me.Controls.Add(Me.txtPIN2)
        Me.Controls.Add(Me.txtPIN1)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(484, 281)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(484, 281)
        Me.Name = "FrmCodigoPIN"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Ingrese el PIN del usuario"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub

    Friend WithEvents txtPIN1 As TextBox
    Friend WithEvents txtPIN2 As TextBox
    Friend WithEvents txtPIN3 As TextBox
    Friend WithEvents txtPIN4 As TextBox
End Class
