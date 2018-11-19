<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmRespaldo
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
        Me.btnRespalda = New System.Windows.Forms.Button()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.cboDatabase = New System.Windows.Forms.ComboBox()
        Me.GroupBox1 = New System.Windows.Forms.GroupBox()
        Me.rdbDatos = New System.Windows.Forms.RadioButton()
        Me.rdbEstructura = New System.Windows.Forms.RadioButton()
        Me.GroupBox1.SuspendLayout()
        Me.SuspendLayout()
        '
        'btnRespalda
        '
        Me.btnRespalda.Location = New System.Drawing.Point(169, 105)
        Me.btnRespalda.Name = "btnRespalda"
        Me.btnRespalda.Size = New System.Drawing.Size(81, 29)
        Me.btnRespalda.TabIndex = 1
        Me.btnRespalda.Text = "Respalda"
        Me.btnRespalda.UseVisualStyleBackColor = True
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(42, 26)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(78, 13)
        Me.Label1.TabIndex = 4
        Me.Label1.Text = "Base de datos:"
        '
        'cboDatabase
        '
        Me.cboDatabase.FormattingEnabled = True
        Me.cboDatabase.Location = New System.Drawing.Point(126, 23)
        Me.cboDatabase.Name = "cboDatabase"
        Me.cboDatabase.Size = New System.Drawing.Size(124, 21)
        Me.cboDatabase.TabIndex = 3
        '
        'GroupBox1
        '
        Me.GroupBox1.Controls.Add(Me.rdbDatos)
        Me.GroupBox1.Controls.Add(Me.rdbEstructura)
        Me.GroupBox1.Location = New System.Drawing.Point(64, 50)
        Me.GroupBox1.Name = "GroupBox1"
        Me.GroupBox1.Size = New System.Drawing.Size(186, 49)
        Me.GroupBox1.TabIndex = 41
        Me.GroupBox1.TabStop = False
        Me.GroupBox1.Text = "Tipo de Respaldo"
        '
        'rdbDatos
        '
        Me.rdbDatos.AutoSize = True
        Me.rdbDatos.Checked = True
        Me.rdbDatos.Location = New System.Drawing.Point(24, 19)
        Me.rdbDatos.Name = "rdbDatos"
        Me.rdbDatos.Size = New System.Drawing.Size(53, 17)
        Me.rdbDatos.TabIndex = 41
        Me.rdbDatos.TabStop = True
        Me.rdbDatos.Text = "Datos"
        Me.rdbDatos.UseVisualStyleBackColor = True
        '
        'rdbEstructura
        '
        Me.rdbEstructura.AutoSize = True
        Me.rdbEstructura.Location = New System.Drawing.Point(92, 19)
        Me.rdbEstructura.Name = "rdbEstructura"
        Me.rdbEstructura.Size = New System.Drawing.Size(73, 17)
        Me.rdbEstructura.TabIndex = 40
        Me.rdbEstructura.Text = "Estructura"
        Me.rdbEstructura.UseVisualStyleBackColor = True
        '
        'FrmRespaldo
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(290, 142)
        Me.Controls.Add(Me.GroupBox1)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.cboDatabase)
        Me.Controls.Add(Me.btnRespalda)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(306, 180)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(306, 159)
        Me.Name = "FrmRespaldo"
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "Mantenimiento de Base de Datos"
        Me.GroupBox1.ResumeLayout(False)
        Me.GroupBox1.PerformLayout()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents btnRespalda As System.Windows.Forms.Button
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents cboDatabase As System.Windows.Forms.ComboBox
    Friend WithEvents GroupBox1 As System.Windows.Forms.GroupBox
    Friend WithEvents rdbDatos As System.Windows.Forms.RadioButton
    Friend WithEvents rdbEstructura As System.Windows.Forms.RadioButton

End Class
