<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmActivacion
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
        Me.txtNombreBaseDatos = New System.Windows.Forms.TextBox()
        Me.txtHostRespaldo = New System.Windows.Forms.TextBox()
        Me.txtClaveRespaldo = New System.Windows.Forms.TextBox()
        Me.txtUsuarioRespaldo = New System.Windows.Forms.TextBox()
        Me.btnSeleccionar = New System.Windows.Forms.Button()
        Me.Label5 = New System.Windows.Forms.Label()
        Me.txtParametrosRespaldo = New System.Windows.Forms.TextBox()
        Me.btnPAramPorDefecto = New System.Windows.Forms.Button()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtRutaArchivoConfiguracion = New System.Windows.Forms.TextBox()
        Me.Label7 = New System.Windows.Forms.Label()
        Me.txtServidorRespaldo = New System.Windows.Forms.TextBox()
        Me.Label8 = New System.Windows.Forms.Label()
        Me.txtSucursal = New System.Windows.Forms.TextBox()
        Me.Label9 = New System.Windows.Forms.Label()
        Me.txtCaja = New System.Windows.Forms.TextBox()
        Me.Label10 = New System.Windows.Forms.Label()
        Me.Label11 = New System.Windows.Forms.Label()
        Me.txtClaveLogin = New System.Windows.Forms.TextBox()
        Me.txtUsuarioLogin = New System.Windows.Forms.TextBox()
        Me.SuspendLayout()
        '
        'label4
        '
        Me.label4.AutoSize = True
        Me.label4.Location = New System.Drawing.Point(93, 171)
        Me.label4.Name = "label4"
        Me.label4.Size = New System.Drawing.Size(78, 13)
        Me.label4.TabIndex = 27
        Me.label4.Text = "Base de datos:"
        '
        'label3
        '
        Me.label3.AutoSize = True
        Me.label3.Location = New System.Drawing.Point(81, 145)
        Me.label3.Name = "label3"
        Me.label3.Size = New System.Drawing.Size(90, 13)
        Me.label3.TabIndex = 26
        Me.label3.Text = "Host de respaldo:"
        '
        'label2
        '
        Me.label2.AutoSize = True
        Me.label2.Location = New System.Drawing.Point(64, 119)
        Me.label2.Name = "label2"
        Me.label2.Size = New System.Drawing.Size(107, 13)
        Me.label2.TabIndex = 25
        Me.label2.Text = "Contraseña respaldo:"
        '
        'label1
        '
        Me.label1.AutoSize = True
        Me.label1.Location = New System.Drawing.Point(82, 93)
        Me.label1.Name = "label1"
        Me.label1.Size = New System.Drawing.Size(89, 13)
        Me.label1.TabIndex = 24
        Me.label1.Text = "Usuario respaldo:"
        '
        'btnGuardar
        '
        Me.btnGuardar.Location = New System.Drawing.Point(513, 293)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.Size = New System.Drawing.Size(63, 23)
        Me.btnGuardar.TabIndex = 10
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = True
        '
        'txtNombreBaseDatos
        '
        Me.txtNombreBaseDatos.Location = New System.Drawing.Point(177, 168)
        Me.txtNombreBaseDatos.Name = "txtNombreBaseDatos"
        Me.txtNombreBaseDatos.Size = New System.Drawing.Size(105, 20)
        Me.txtNombreBaseDatos.TabIndex = 4
        '
        'txtHostRespaldo
        '
        Me.txtHostRespaldo.Location = New System.Drawing.Point(177, 142)
        Me.txtHostRespaldo.Name = "txtHostRespaldo"
        Me.txtHostRespaldo.Size = New System.Drawing.Size(105, 20)
        Me.txtHostRespaldo.TabIndex = 3
        '
        'txtClaveRespaldo
        '
        Me.txtClaveRespaldo.Location = New System.Drawing.Point(177, 116)
        Me.txtClaveRespaldo.Name = "txtClaveRespaldo"
        Me.txtClaveRespaldo.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClaveRespaldo.Size = New System.Drawing.Size(105, 20)
        Me.txtClaveRespaldo.TabIndex = 2
        '
        'txtUsuarioRespaldo
        '
        Me.txtUsuarioRespaldo.Location = New System.Drawing.Point(177, 90)
        Me.txtUsuarioRespaldo.Name = "txtUsuarioRespaldo"
        Me.txtUsuarioRespaldo.Size = New System.Drawing.Size(105, 20)
        Me.txtUsuarioRespaldo.TabIndex = 1
        '
        'btnSeleccionar
        '
        Me.btnSeleccionar.Location = New System.Drawing.Point(504, 12)
        Me.btnSeleccionar.Name = "btnSeleccionar"
        Me.btnSeleccionar.Size = New System.Drawing.Size(72, 22)
        Me.btnSeleccionar.TabIndex = 0
        Me.btnSeleccionar.TabStop = False
        Me.btnSeleccionar.Text = "Seleccionar"
        Me.btnSeleccionar.UseVisualStyleBackColor = True
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(48, 197)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(123, 13)
        Me.Label5.TabIndex = 34
        Me.Label5.Text = "Parametros del respaldo:"
        '
        'txtParametrosRespaldo
        '
        Me.txtParametrosRespaldo.Location = New System.Drawing.Point(177, 194)
        Me.txtParametrosRespaldo.Name = "txtParametrosRespaldo"
        Me.txtParametrosRespaldo.Size = New System.Drawing.Size(357, 20)
        Me.txtParametrosRespaldo.TabIndex = 5
        '
        'btnPAramPorDefecto
        '
        Me.btnPAramPorDefecto.Location = New System.Drawing.Point(376, 293)
        Me.btnPAramPorDefecto.Name = "btnPAramPorDefecto"
        Me.btnPAramPorDefecto.Size = New System.Drawing.Size(131, 23)
        Me.btnPAramPorDefecto.TabIndex = 9
        Me.btnPAramPorDefecto.TabStop = False
        Me.btnPAramPorDefecto.Text = "Parametros por defecto"
        Me.btnPAramPorDefecto.UseVisualStyleBackColor = True
        '
        'Label6
        '
        Me.Label6.AutoSize = True
        Me.Label6.Location = New System.Drawing.Point(43, 15)
        Me.Label6.Name = "Label6"
        Me.Label6.Size = New System.Drawing.Size(128, 13)
        Me.Label6.TabIndex = 37
        Me.Label6.Text = "Archivo de configuración:"
        '
        'txtRutaArchivoConfiguracion
        '
        Me.txtRutaArchivoConfiguracion.Location = New System.Drawing.Point(177, 12)
        Me.txtRutaArchivoConfiguracion.Name = "txtRutaArchivoConfiguracion"
        Me.txtRutaArchivoConfiguracion.ReadOnly = True
        Me.txtRutaArchivoConfiguracion.Size = New System.Drawing.Size(321, 20)
        Me.txtRutaArchivoConfiguracion.TabIndex = 0
        Me.txtRutaArchivoConfiguracion.TabStop = False
        '
        'Label7
        '
        Me.Label7.AutoSize = True
        Me.Label7.Location = New System.Drawing.Point(50, 223)
        Me.Label7.Name = "Label7"
        Me.Label7.Size = New System.Drawing.Size(121, 13)
        Me.Label7.TabIndex = 39
        Me.Label7.Text = "Url servidor de respaldo:"
        '
        'txtServidorRespaldo
        '
        Me.txtServidorRespaldo.Location = New System.Drawing.Point(177, 220)
        Me.txtServidorRespaldo.Name = "txtServidorRespaldo"
        Me.txtServidorRespaldo.Size = New System.Drawing.Size(357, 20)
        Me.txtServidorRespaldo.TabIndex = 6
        '
        'Label8
        '
        Me.Label8.AutoSize = True
        Me.Label8.Location = New System.Drawing.Point(86, 249)
        Me.Label8.Name = "Label8"
        Me.Label8.Size = New System.Drawing.Size(85, 13)
        Me.Label8.TabIndex = 41
        Me.Label8.Text = "Código sucursal:"
        '
        'txtSucursal
        '
        Me.txtSucursal.Location = New System.Drawing.Point(177, 246)
        Me.txtSucursal.Name = "txtSucursal"
        Me.txtSucursal.Size = New System.Drawing.Size(43, 20)
        Me.txtSucursal.TabIndex = 7
        '
        'Label9
        '
        Me.Label9.AutoSize = True
        Me.Label9.Location = New System.Drawing.Point(81, 275)
        Me.Label9.Name = "Label9"
        Me.Label9.Size = New System.Drawing.Size(85, 13)
        Me.Label9.TabIndex = 43
        Me.Label9.Text = "Número de caja:"
        '
        'txtCaja
        '
        Me.txtCaja.Location = New System.Drawing.Point(177, 272)
        Me.txtCaja.Name = "txtCaja"
        Me.txtCaja.Size = New System.Drawing.Size(66, 20)
        Me.txtCaja.TabIndex = 8
        '
        'Label10
        '
        Me.Label10.AutoSize = True
        Me.Label10.Location = New System.Drawing.Point(69, 67)
        Me.Label10.Name = "Label10"
        Me.Label10.Size = New System.Drawing.Size(102, 13)
        Me.Label10.TabIndex = 47
        Me.Label10.Text = "Contraseña sistema:"
        '
        'Label11
        '
        Me.Label11.AutoSize = True
        Me.Label11.Location = New System.Drawing.Point(87, 41)
        Me.Label11.Name = "Label11"
        Me.Label11.Size = New System.Drawing.Size(84, 13)
        Me.Label11.TabIndex = 46
        Me.Label11.Text = "Usuario sistema:"
        '
        'txtClaveLogin
        '
        Me.txtClaveLogin.Location = New System.Drawing.Point(177, 64)
        Me.txtClaveLogin.Name = "txtClaveLogin"
        Me.txtClaveLogin.PasswordChar = Global.Microsoft.VisualBasic.ChrW(42)
        Me.txtClaveLogin.Size = New System.Drawing.Size(105, 20)
        Me.txtClaveLogin.TabIndex = 45
        '
        'txtUsuarioLogin
        '
        Me.txtUsuarioLogin.Location = New System.Drawing.Point(177, 38)
        Me.txtUsuarioLogin.Name = "txtUsuarioLogin"
        Me.txtUsuarioLogin.Size = New System.Drawing.Size(105, 20)
        Me.txtUsuarioLogin.TabIndex = 44
        '
        'FrmActivacion
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.Color.FromArgb(CType(CType(193, Byte), Integer), CType(CType(215, Byte), Integer), CType(CType(225, Byte), Integer))
        Me.ClientSize = New System.Drawing.Size(578, 318)
        Me.Controls.Add(Me.Label10)
        Me.Controls.Add(Me.Label11)
        Me.Controls.Add(Me.txtClaveLogin)
        Me.Controls.Add(Me.txtUsuarioLogin)
        Me.Controls.Add(Me.Label9)
        Me.Controls.Add(Me.txtCaja)
        Me.Controls.Add(Me.Label8)
        Me.Controls.Add(Me.txtSucursal)
        Me.Controls.Add(Me.Label7)
        Me.Controls.Add(Me.txtServidorRespaldo)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.txtRutaArchivoConfiguracion)
        Me.Controls.Add(Me.btnPAramPorDefecto)
        Me.Controls.Add(Me.Label5)
        Me.Controls.Add(Me.txtParametrosRespaldo)
        Me.Controls.Add(Me.btnSeleccionar)
        Me.Controls.Add(Me.label4)
        Me.Controls.Add(Me.label3)
        Me.Controls.Add(Me.label2)
        Me.Controls.Add(Me.label1)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtNombreBaseDatos)
        Me.Controls.Add(Me.txtHostRespaldo)
        Me.Controls.Add(Me.txtClaveRespaldo)
        Me.Controls.Add(Me.txtUsuarioRespaldo)
        Me.Name = "FrmActivacion"
        Me.Text = "Controlador de Variables de Configuración"
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Private WithEvents label4 As System.Windows.Forms.Label
    Private WithEvents label3 As System.Windows.Forms.Label
    Private WithEvents label2 As System.Windows.Forms.Label
    Private WithEvents label1 As System.Windows.Forms.Label
    Private WithEvents btnGuardar As System.Windows.Forms.Button
    Private WithEvents txtNombreBaseDatos As System.Windows.Forms.TextBox
    Private WithEvents txtHostRespaldo As System.Windows.Forms.TextBox
    Private WithEvents txtClaveRespaldo As System.Windows.Forms.TextBox
    Private WithEvents txtUsuarioRespaldo As System.Windows.Forms.TextBox
    Private WithEvents btnSeleccionar As System.Windows.Forms.Button
    Private WithEvents Label5 As System.Windows.Forms.Label
    Private WithEvents txtParametrosRespaldo As System.Windows.Forms.TextBox
    Friend WithEvents btnPAramPorDefecto As System.Windows.Forms.Button
    Private WithEvents Label6 As System.Windows.Forms.Label
    Private WithEvents txtRutaArchivoConfiguracion As System.Windows.Forms.TextBox
    Private WithEvents Label7 As System.Windows.Forms.Label
    Private WithEvents txtServidorRespaldo As System.Windows.Forms.TextBox
    Private WithEvents Label8 As System.Windows.Forms.Label
    Private WithEvents txtSucursal As System.Windows.Forms.TextBox
    Private WithEvents Label9 As System.Windows.Forms.Label
    Private WithEvents txtCaja As System.Windows.Forms.TextBox
    Private WithEvents Label10 As System.Windows.Forms.Label
    Private WithEvents Label11 As System.Windows.Forms.Label
    Private WithEvents txtClaveLogin As System.Windows.Forms.TextBox
    Private WithEvents txtUsuarioLogin As System.Windows.Forms.TextBox
End Class
