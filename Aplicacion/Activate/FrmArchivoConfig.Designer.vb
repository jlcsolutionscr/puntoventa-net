<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmArchivoConfig
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
        Me.label4 = New System.Windows.Forms.Label()
        Me.label3 = New System.Windows.Forms.Label()
        Me.label2 = New System.Windows.Forms.Label()
        Me.label1 = New System.Windows.Forms.Label()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtPassword = New System.Windows.Forms.TextBox()
        Me.txtUser = New System.Windows.Forms.TextBox()
        Me.txtDatabase = New System.Windows.Forms.TextBox()
        Me.txtHost = New System.Windows.Forms.TextBox()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtSqlDumpParams = New System.Windows.Forms.TextBox()
        Me.btnParamPorDefecto = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtSubjectName = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(196, 41)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(56, 13)
        Me.label4.TabIndex = 27
        Me.label4.Text = "Password:"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(3, 41)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(46, 13)
        Me.label3.TabIndex = 26
        Me.label3.Text = "Usuario:"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(172, 15)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(80, 13)
        Me.label2.TabIndex = 25
        Me.label2.Text = "Base de Datos:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(17, 15)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(32, 13)
        Me.label1.TabIndex = 24
        Me.label1.Text = "Host:"
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(300, 123)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(63, 26)
        Me.btnGuardar.TabIndex = 6
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtPassword
        '
        Me.txtPassword.Location = New System.Drawing.Point(258, 38)
        Me.txtPassword.Name = "txtPassword"
        Me.txtPassword.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtPassword.Size = New System.Drawing.Size(105, 20)
        Me.txtPassword.TabIndex = 3
        '
        'txtUser
        '
        Me.txtUser.Location = New System.Drawing.Point(55, 38)
        Me.txtUser.Name = "txtUser"
        Me.txtUser.Size = New System.Drawing.Size(105, 20)
        Me.txtUser.TabIndex = 2
        '
        'txtDatabase
        '
        Me.txtDatabase.Location = New System.Drawing.Point(258, 12)
        Me.txtDatabase.Name = "txtDatabase"
        Me.txtDatabase.Size = New System.Drawing.Size(105, 20)
        Me.txtDatabase.TabIndex = 1
        '
        'txtHost
        '
        Me.txtHost.Location = New System.Drawing.Point(55, 12)
        Me.txtHost.Name = "txtHost"
        Me.txtHost.Size = New System.Drawing.Size(105, 20)
        Me.txtHost.TabIndex = 0
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(3, 67)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(91, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "SqlDump Params:"
        '
        'txtSqlDumpParams
        '
        Me.txtSqlDumpParams.Location = New System.Drawing.Point(100, 64)
        Me.txtSqlDumpParams.Name = "txtSqlDumpParams"
        Me.txtSqlDumpParams.Size = New System.Drawing.Size(263, 20)
        Me.txtSqlDumpParams.TabIndex = 4
        '
        'btnParamPorDefecto
        '
        Me.btnParamPorDefecto.Location = New System.Drawing.Point(6, 123)
        Me.btnParamPorDefecto.Name = "btnParamPorDefecto"
        Me.btnParamPorDefecto.Size = New System.Drawing.Size(71, 23)
        Me.btnParamPorDefecto.TabIndex = 35
        Me.btnParamPorDefecto.Text = "Restablecer"
        Me.btnParamPorDefecto.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(20, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(74, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "SubjectName:"
        '
        'txtSubjectName
        '
        Me.txtSubjectName.Location = New System.Drawing.Point(100, 90)
        Me.txtSubjectName.Name = "txtSubjectName"
        Me.txtSubjectName.Size = New System.Drawing.Size(194, 20)
        Me.txtSubjectName.TabIndex = 36
        '
        'FrmArchivoConfig
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(366, 153)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtSubjectName)
        Me.Controls.Add(Me.btnParamPorDefecto)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtSqlDumpParams)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtPassword)
        Me.Controls.Add(Me.txtUser)
        Me.Controls.Add(Me.txtDatabase)
        Me.Controls.Add(Me.txtHost)
        Me.MaximumSize = New System.Drawing.Size(382, 191)
        Me.MinimumSize = New System.Drawing.Size(382, 191)
        Me.Name = "FrmArchivoConfig"
        Me.Text = "Controlador de Variables de Configuración"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btnGuardar As System.Windows.Forms.Button
    Private WithEvents txtPassword As System.Windows.Forms.TextBox
    Private WithEvents txtUser As System.Windows.Forms.TextBox
    Private WithEvents txtDatabase As System.Windows.Forms.TextBox
    Private WithEvents txtHost As System.Windows.Forms.TextBox
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents txtSqlDumpParams As System.Windows.Forms.TextBox
    Friend WithEvents btnParamPorDefecto As System.Windows.Forms.Button
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents txtSubjectName As System.Windows.Forms.TextBox
End Class
