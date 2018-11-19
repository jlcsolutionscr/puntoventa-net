<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmDesencriptar
    Inherits System.Windows.Forms.Form

    'Form reemplaza a Dispose para limpiar la lista de componentes.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        If disposing AndAlso components IsNot Nothing Then
            components.Dispose()
        End If
        MyBase.Dispose(disposing)
    End Sub

    'Requerido por el Diseñador de Windows Forms
    Private components As System.ComponentModel.IContainer

    'NOTA: el Diseñador de Windows Forms necesita el siguiente procedimiento
    'Se puede modificar usando el Diseñador de Windows Forms.  
    'No lo modifique con el editor de código.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnDesencriptar = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtArchivo = New System.Windows.Forms.TextBox()
        Me.btnFileLoader = New System.Windows.Forms.Button()
        Me.ofdAbrirArchivo = New System.Windows.Forms.OpenFileDialog()
        Me.SuspendLayout()
        '
        'btnDesencriptar
        '
        Me.btnDesencriptar.Location = New System.Drawing.Point(150, 58)
        Me.btnDesencriptar.Name = "btnDesencriptar"
        Me.btnDesencriptar.Size = New System.Drawing.Size(81, 29)
        Me.btnDesencriptar.TabIndex = 2
        Me.btnDesencriptar.Text = "Desencriptar"
        Me.btnDesencriptar.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(12, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(46, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Archivo:"
        '
        'txtArchivo
        '
        Me.txtArchivo.Location = New System.Drawing.Point(64, 23)
        Me.txtArchivo.Name = "txtArchivo"
        Me.txtArchivo.Size = New System.Drawing.Size(290, 20)
        Me.txtArchivo.TabIndex = 0
        '
        'btnFileLoader
        '
        Me.btnFileLoader.BackColor = System.Drawing.Color.Transparent
        Me.btnFileLoader.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None
        Me.btnFileLoader.Location = New System.Drawing.Point(357, 23)
        Me.btnFileLoader.Margin = New System.Windows.Forms.Padding(0)
        Me.btnFileLoader.Name = "btnFileLoader"
        Me.btnFileLoader.Size = New System.Drawing.Size(22, 20)
        Me.btnFileLoader.TabIndex = 3
        Me.btnFileLoader.TabStop = False
        Me.btnFileLoader.UseVisualStyleBackColor = False
        '
        'ofdAbrirArchivo
        '
        Me.ofdAbrirArchivo.FileName = "OpenFileDialog1"
        '
        'FrmRestaura
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(384, 121)
        Me.Controls.Add(Me.btnFileLoader)
        Me.Controls.Add(Me.txtArchivo)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.btnDesencriptar)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(400, 159)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(400, 159)
        Me.Name = "FrmRestaura"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de Base de Datos"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnDesencriptar As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents txtArchivo As System.Windows.Forms.TextBox
    Friend WithEvents btnFileLoader As System.Windows.Forms.Button
    Friend WithEvents ofdAbrirArchivo As System.Windows.Forms.OpenFileDialog

End Class
