<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class FrmAjusteInventario
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
    Public WithEvents btnAnular As System.Windows.Forms.Button
    Public WithEvents btnAgregar As System.Windows.Forms.Button
    Public WithEvents btnBuscar As System.Windows.Forms.Button
    Public WithEvents btnGuardar As System.Windows.Forms.Button
    Public WithEvents txtIdAjuste As System.Windows.Forms.TextBox
    Public WithEvents txtDescAjuste As System.Windows.Forms.TextBox
    Public WithEvents txtFecha As System.Windows.Forms.TextBox
    Public WithEvents lblLabel4 As System.Windows.Forms.Label
    Public WithEvents lblLabel3 As System.Windows.Forms.Label
    Public WithEvents lblLabel0 As System.Windows.Forms.Label
    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.btnAnular = New System.Windows.Forms.Button()
        Me.btnAgregar = New System.Windows.Forms.Button()
        Me.btnBuscar = New System.Windows.Forms.Button()
        Me.btnGuardar = New System.Windows.Forms.Button()
        Me.txtIdAjuste = New System.Windows.Forms.TextBox()
        Me.txtDescAjuste = New System.Windows.Forms.TextBox()
        Me.txtFecha = New System.Windows.Forms.TextBox()
        Me.lblLabel4 = New System.Windows.Forms.Label()
        Me.lblLabel3 = New System.Windows.Forms.Label()
        Me.lblLabel0 = New System.Windows.Forms.Label()
        Me.grdDetalleAjusteInventario = New System.Windows.Forms.DataGridView()
        Me.txtCodigo = New System.Windows.Forms.TextBox()
        Me.Label2 = New System.Windows.Forms.Label()
        Me.txtDescripcion = New System.Windows.Forms.TextBox()
        Me.txtPrecioCosto = New System.Windows.Forms.TextBox()
        Me.Label1 = New System.Windows.Forms.Label()
        Me.txtCantidad = New System.Windows.Forms.TextBox()
        Me.lblLabel6 = New System.Windows.Forms.Label()
        Me.lblLabel1 = New System.Windows.Forms.Label()
        Me.btnBusProd = New System.Windows.Forms.Button()
        Me.btnEliminar = New System.Windows.Forms.Button()
        Me.btnInsertar = New System.Windows.Forms.Button()
        Me.cboSucursal = New System.Windows.Forms.ComboBox()
        Me.Label6 = New System.Windows.Forms.Label()
        Me.txtExistencias = New System.Windows.Forms.TextBox()
        Me.Label15 = New System.Windows.Forms.Label()
        CType(Me.grdDetalleAjusteInventario, System.ComponentModel.ISupportInitialize).BeginInit()
        Me.SuspendLayout()
        '
        'btnAnular
        '
        Me.btnAnular.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAnular.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAnular.Enabled = False
        Me.btnAnular.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAnular.Location = New System.Drawing.Point(136, 8)
        Me.btnAnular.Name = "btnAnular"
        Me.btnAnular.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAnular.Size = New System.Drawing.Size(64, 21)
        Me.btnAnular.TabIndex = 35
        Me.btnAnular.TabStop = False
        Me.btnAnular.Text = "&Anular"
        Me.btnAnular.UseVisualStyleBackColor = False
        '
        'btnAgregar
        '
        Me.btnAgregar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnAgregar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnAgregar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnAgregar.Location = New System.Drawing.Point(200, 8)
        Me.btnAgregar.Name = "btnAgregar"
        Me.btnAgregar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnAgregar.Size = New System.Drawing.Size(64, 21)
        Me.btnAgregar.TabIndex = 34
        Me.btnAgregar.TabStop = False
        Me.btnAgregar.Text = "&Nuevo"
        Me.btnAgregar.UseVisualStyleBackColor = False
        '
        'btnBuscar
        '
        Me.btnBuscar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBuscar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBuscar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBuscar.Location = New System.Drawing.Point(72, 8)
        Me.btnBuscar.Name = "btnBuscar"
        Me.btnBuscar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBuscar.Size = New System.Drawing.Size(64, 21)
        Me.btnBuscar.TabIndex = 33
        Me.btnBuscar.TabStop = False
        Me.btnBuscar.Text = "B&uscar"
        Me.btnBuscar.UseVisualStyleBackColor = False
        '
        'btnGuardar
        '
        Me.btnGuardar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnGuardar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnGuardar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnGuardar.Location = New System.Drawing.Point(8, 8)
        Me.btnGuardar.Name = "btnGuardar"
        Me.btnGuardar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnGuardar.Size = New System.Drawing.Size(64, 21)
        Me.btnGuardar.TabIndex = 31
        Me.btnGuardar.TabStop = False
        Me.btnGuardar.Text = "&Guardar"
        Me.btnGuardar.UseVisualStyleBackColor = False
        '
        'txtIdAjuste
        '
        Me.txtIdAjuste.AcceptsReturn = True
        Me.txtIdAjuste.BackColor = System.Drawing.SystemColors.Window
        Me.txtIdAjuste.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtIdAjuste.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtIdAjuste.Location = New System.Drawing.Point(72, 40)
        Me.txtIdAjuste.MaxLength = 0
        Me.txtIdAjuste.Name = "txtIdAjuste"
        Me.txtIdAjuste.ReadOnly = True
        Me.txtIdAjuste.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtIdAjuste.Size = New System.Drawing.Size(65, 20)
        Me.txtIdAjuste.TabIndex = 0
        Me.txtIdAjuste.TabStop = False
        '
        'txtDescAjuste
        '
        Me.txtDescAjuste.AcceptsReturn = True
        Me.txtDescAjuste.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescAjuste.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescAjuste.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescAjuste.Location = New System.Drawing.Point(72, 119)
        Me.txtDescAjuste.MaxLength = 220
        Me.txtDescAjuste.Multiline = True
        Me.txtDescAjuste.Name = "txtDescAjuste"
        Me.txtDescAjuste.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescAjuste.Size = New System.Drawing.Size(680, 40)
        Me.txtDescAjuste.TabIndex = 4
        '
        'txtFecha
        '
        Me.txtFecha.AcceptsReturn = True
        Me.txtFecha.BackColor = System.Drawing.SystemColors.Window
        Me.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtFecha.Location = New System.Drawing.Point(72, 66)
        Me.txtFecha.MaxLength = 0
        Me.txtFecha.Name = "txtFecha"
        Me.txtFecha.ReadOnly = True
        Me.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtFecha.Size = New System.Drawing.Size(65, 20)
        Me.txtFecha.TabIndex = 2
        Me.txtFecha.TabStop = False
        '
        'lblLabel4
        '
        Me.lblLabel4.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel4.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel4.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel4.Location = New System.Drawing.Point(-2, 120)
        Me.lblLabel4.Name = "lblLabel4"
        Me.lblLabel4.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel4.Size = New System.Drawing.Size(68, 18)
        Me.lblLabel4.TabIndex = 13
        Me.lblLabel4.Text = "Descripci�n:"
        Me.lblLabel4.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel3
        '
        Me.lblLabel3.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel3.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel3.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel3.Location = New System.Drawing.Point(9, 67)
        Me.lblLabel3.Name = "lblLabel3"
        Me.lblLabel3.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel3.Size = New System.Drawing.Size(57, 19)
        Me.lblLabel3.TabIndex = 12
        Me.lblLabel3.Text = "Fecha:"
        Me.lblLabel3.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'lblLabel0
        '
        Me.lblLabel0.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel0.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel0.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel0.Location = New System.Drawing.Point(0, 40)
        Me.lblLabel0.Name = "lblLabel0"
        Me.lblLabel0.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel0.Size = New System.Drawing.Size(69, 19)
        Me.lblLabel0.TabIndex = 10
        Me.lblLabel0.Text = "Ajuste No:"
        Me.lblLabel0.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'grdDetalleAjusteInventario
        '
        Me.grdDetalleAjusteInventario.AllowUserToAddRows = False
        Me.grdDetalleAjusteInventario.AllowUserToDeleteRows = False
        Me.grdDetalleAjusteInventario.AllowUserToResizeColumns = False
        Me.grdDetalleAjusteInventario.AllowUserToResizeRows = False
        Me.grdDetalleAjusteInventario.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize
        Me.grdDetalleAjusteInventario.Location = New System.Drawing.Point(12, 219)
        Me.grdDetalleAjusteInventario.MultiSelect = False
        Me.grdDetalleAjusteInventario.Name = "grdDetalleAjusteInventario"
        Me.grdDetalleAjusteInventario.ReadOnly = True
        Me.grdDetalleAjusteInventario.RowHeadersVisible = False
        Me.grdDetalleAjusteInventario.Size = New System.Drawing.Size(740, 247)
        Me.grdDetalleAjusteInventario.TabIndex = 5
        '
        'txtCodigo
        '
        Me.txtCodigo.Location = New System.Drawing.Point(12, 193)
        Me.txtCodigo.Name = "txtCodigo"
        Me.txtCodigo.Size = New System.Drawing.Size(135, 20)
        Me.txtCodigo.TabIndex = 5
        '
        'Label2
        '
        Me.Label2.BackColor = System.Drawing.Color.Transparent
        Me.Label2.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label2.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label2.Location = New System.Drawing.Point(147, 173)
        Me.Label2.Name = "Label2"
        Me.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label2.Size = New System.Drawing.Size(425, 19)
        Me.Label2.TabIndex = 53
        Me.Label2.Text = "Descripci�n"
        Me.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtDescripcion
        '
        Me.txtDescripcion.AcceptsReturn = True
        Me.txtDescripcion.BackColor = System.Drawing.SystemColors.Window
        Me.txtDescripcion.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtDescripcion.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtDescripcion.Location = New System.Drawing.Point(147, 193)
        Me.txtDescripcion.MaxLength = 0
        Me.txtDescripcion.Name = "txtDescripcion"
        Me.txtDescripcion.ReadOnly = True
        Me.txtDescripcion.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtDescripcion.Size = New System.Drawing.Size(425, 20)
        Me.txtDescripcion.TabIndex = 6
        Me.txtDescripcion.TabStop = False
        '
        'txtPrecioCosto
        '
        Me.txtPrecioCosto.AcceptsReturn = True
        Me.txtPrecioCosto.BackColor = System.Drawing.SystemColors.Window
        Me.txtPrecioCosto.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtPrecioCosto.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtPrecioCosto.Location = New System.Drawing.Point(663, 193)
        Me.txtPrecioCosto.MaxLength = 0
        Me.txtPrecioCosto.Name = "txtPrecioCosto"
        Me.txtPrecioCosto.ReadOnly = True
        Me.txtPrecioCosto.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtPrecioCosto.Size = New System.Drawing.Size(89, 20)
        Me.txtPrecioCosto.TabIndex = 8
        Me.txtPrecioCosto.TextAlign = System.Windows.Forms.HorizontalAlignment.Right
        '
        'Label1
        '
        Me.Label1.BackColor = System.Drawing.Color.Transparent
        Me.Label1.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label1.Location = New System.Drawing.Point(664, 173)
        Me.Label1.Name = "Label1"
        Me.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label1.Size = New System.Drawing.Size(87, 19)
        Me.Label1.TabIndex = 52
        Me.Label1.Text = "Precio Costo"
        Me.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'txtCantidad
        '
        Me.txtCantidad.AcceptsReturn = True
        Me.txtCantidad.BackColor = System.Drawing.SystemColors.Window
        Me.txtCantidad.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtCantidad.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtCantidad.Location = New System.Drawing.Point(622, 193)
        Me.txtCantidad.MaxLength = 0
        Me.txtCantidad.Name = "txtCantidad"
        Me.txtCantidad.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtCantidad.Size = New System.Drawing.Size(41, 20)
        Me.txtCantidad.TabIndex = 7
        Me.txtCantidad.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'lblLabel6
        '
        Me.lblLabel6.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel6.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel6.Location = New System.Drawing.Point(622, 173)
        Me.lblLabel6.Name = "lblLabel6"
        Me.lblLabel6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel6.Size = New System.Drawing.Size(41, 19)
        Me.lblLabel6.TabIndex = 51
        Me.lblLabel6.Text = "Cant"
        Me.lblLabel6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'lblLabel1
        '
        Me.lblLabel1.BackColor = System.Drawing.Color.Transparent
        Me.lblLabel1.Cursor = System.Windows.Forms.Cursors.Default
        Me.lblLabel1.ForeColor = System.Drawing.SystemColors.ControlText
        Me.lblLabel1.Location = New System.Drawing.Point(12, 173)
        Me.lblLabel1.Name = "lblLabel1"
        Me.lblLabel1.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.lblLabel1.Size = New System.Drawing.Size(135, 19)
        Me.lblLabel1.TabIndex = 50
        Me.lblLabel1.Text = "C�digo"
        Me.lblLabel1.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'btnBusProd
        '
        Me.btnBusProd.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnBusProd.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnBusProd.Enabled = False
        Me.btnBusProd.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnBusProd.Location = New System.Drawing.Point(172, 472)
        Me.btnBusProd.Name = "btnBusProd"
        Me.btnBusProd.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnBusProd.Size = New System.Drawing.Size(73, 25)
        Me.btnBusProd.TabIndex = 56
        Me.btnBusProd.TabStop = False
        Me.btnBusProd.Text = "&Buscar"
        Me.btnBusProd.UseVisualStyleBackColor = False
        '
        'btnEliminar
        '
        Me.btnEliminar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnEliminar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnEliminar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnEliminar.Location = New System.Drawing.Point(92, 472)
        Me.btnEliminar.Name = "btnEliminar"
        Me.btnEliminar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnEliminar.Size = New System.Drawing.Size(73, 25)
        Me.btnEliminar.TabIndex = 55
        Me.btnEliminar.TabStop = False
        Me.btnEliminar.Text = "&Eliminar"
        Me.btnEliminar.UseVisualStyleBackColor = False
        '
        'btnInsertar
        '
        Me.btnInsertar.BackColor = System.Drawing.SystemColors.ButtonHighlight
        Me.btnInsertar.Cursor = System.Windows.Forms.Cursors.Default
        Me.btnInsertar.ForeColor = System.Drawing.SystemColors.ControlText
        Me.btnInsertar.Location = New System.Drawing.Point(12, 472)
        Me.btnInsertar.Name = "btnInsertar"
        Me.btnInsertar.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.btnInsertar.Size = New System.Drawing.Size(73, 25)
        Me.btnInsertar.TabIndex = 54
        Me.btnInsertar.TabStop = False
        Me.btnInsertar.Text = "Insertar"
        Me.btnInsertar.UseVisualStyleBackColor = False
        '
        'cboSucursal
        '
        Me.cboSucursal.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList
        Me.cboSucursal.FormattingEnabled = True
        Me.cboSucursal.Location = New System.Drawing.Point(72, 92)
        Me.cboSucursal.Name = "cboSucursal"
        Me.cboSucursal.Size = New System.Drawing.Size(300, 21)
        Me.cboSucursal.TabIndex = 3
        Me.cboSucursal.TabStop = False
        '
        'Label6
        '
        Me.Label6.BackColor = System.Drawing.Color.Transparent
        Me.Label6.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label6.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label6.Location = New System.Drawing.Point(5, 93)
        Me.Label6.Name = "Label6"
        Me.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label6.Size = New System.Drawing.Size(61, 19)
        Me.Label6.TabIndex = 148
        Me.Label6.Text = "Sucursal:"
        Me.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleRight
        '
        'txtExistencias
        '
        Me.txtExistencias.AcceptsReturn = True
        Me.txtExistencias.BackColor = System.Drawing.SystemColors.Window
        Me.txtExistencias.Cursor = System.Windows.Forms.Cursors.IBeam
        Me.txtExistencias.ForeColor = System.Drawing.SystemColors.WindowText
        Me.txtExistencias.Location = New System.Drawing.Point(572, 193)
        Me.txtExistencias.MaxLength = 0
        Me.txtExistencias.Name = "txtExistencias"
        Me.txtExistencias.ReadOnly = True
        Me.txtExistencias.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.txtExistencias.Size = New System.Drawing.Size(50, 20)
        Me.txtExistencias.TabIndex = 155
        Me.txtExistencias.TabStop = False
        Me.txtExistencias.TextAlign = System.Windows.Forms.HorizontalAlignment.Center
        '
        'Label15
        '
        Me.Label15.BackColor = System.Drawing.Color.Transparent
        Me.Label15.Cursor = System.Windows.Forms.Cursors.Default
        Me.Label15.ForeColor = System.Drawing.SystemColors.ControlText
        Me.Label15.Location = New System.Drawing.Point(572, 173)
        Me.Label15.Name = "Label15"
        Me.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.Label15.Size = New System.Drawing.Size(50, 19)
        Me.Label15.TabIndex = 156
        Me.Label15.Text = "Stock"
        Me.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleCenter
        '
        'FrmAjusteInventario
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.BackColor = System.Drawing.SystemColors.Control
        Me.ClientSize = New System.Drawing.Size(765, 510)
        Me.Controls.Add(Me.txtExistencias)
        Me.Controls.Add(Me.Label15)
        Me.Controls.Add(Me.cboSucursal)
        Me.Controls.Add(Me.Label6)
        Me.Controls.Add(Me.btnBusProd)
        Me.Controls.Add(Me.btnEliminar)
        Me.Controls.Add(Me.btnInsertar)
        Me.Controls.Add(Me.txtCodigo)
        Me.Controls.Add(Me.Label2)
        Me.Controls.Add(Me.txtDescripcion)
        Me.Controls.Add(Me.txtPrecioCosto)
        Me.Controls.Add(Me.Label1)
        Me.Controls.Add(Me.txtCantidad)
        Me.Controls.Add(Me.lblLabel6)
        Me.Controls.Add(Me.lblLabel1)
        Me.Controls.Add(Me.grdDetalleAjusteInventario)
        Me.Controls.Add(Me.btnAnular)
        Me.Controls.Add(Me.btnAgregar)
        Me.Controls.Add(Me.btnBuscar)
        Me.Controls.Add(Me.btnGuardar)
        Me.Controls.Add(Me.txtIdAjuste)
        Me.Controls.Add(Me.txtDescAjuste)
        Me.Controls.Add(Me.txtFecha)
        Me.Controls.Add(Me.lblLabel4)
        Me.Controls.Add(Me.lblLabel3)
        Me.Controls.Add(Me.lblLabel0)
        Me.Cursor = System.Windows.Forms.Cursors.Default
        Me.ForeColor = System.Drawing.SystemColors.ControlText
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle
        Me.KeyPreview = True
        Me.Location = New System.Drawing.Point(73, 22)
        Me.MaximizeBox = False
        Me.MaximumSize = New System.Drawing.Size(781, 549)
        Me.MinimizeBox = False
        Me.MinimumSize = New System.Drawing.Size(781, 549)
        Me.Name = "FrmAjusteInventario"
        Me.RightToLeft = System.Windows.Forms.RightToLeft.No
        Me.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
        Me.Text = "M�dulo de Ajustes de Inventario"
        CType(Me.grdDetalleAjusteInventario, System.ComponentModel.ISupportInitialize).EndInit()
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents grdDetalleAjusteInventario As System.Windows.Forms.DataGridView
    Friend WithEvents txtCodigo As System.Windows.Forms.TextBox
    Public WithEvents Label2 As System.Windows.Forms.Label
    Public WithEvents txtDescripcion As System.Windows.Forms.TextBox
    Public WithEvents txtPrecioCosto As System.Windows.Forms.TextBox
    Public WithEvents Label1 As System.Windows.Forms.Label
    Public WithEvents txtCantidad As System.Windows.Forms.TextBox
    Public WithEvents lblLabel6 As System.Windows.Forms.Label
    Public WithEvents lblLabel1 As System.Windows.Forms.Label
    Public WithEvents btnBusProd As System.Windows.Forms.Button
    Public WithEvents btnEliminar As System.Windows.Forms.Button
    Public WithEvents btnInsertar As System.Windows.Forms.Button
    Friend WithEvents cboSucursal As ComboBox
    Friend WithEvents Label6 As Label
    Public WithEvents txtExistencias As TextBox
    Public WithEvents Label15 As Label
End Class