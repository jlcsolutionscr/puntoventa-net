namespace LeandroSoftware.Activator
{
    partial class FrmEmpresa
    {
        /// <summary>
        /// Variable del diseñador necesaria.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Limpiar los recursos que se estén usando.
        /// </summary>
        /// <param name="disposing">true si los recursos administrados se deben desechar; false en caso contrario.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Código generado por el Diseñador de Windows Forms

        /// <summary>
        /// Método necesario para admitir el Diseñador. No se puede modificar
        /// el contenido de este método con el editor de código.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(FrmEmpresa));
            this.cmdUpdate = new System.Windows.Forms.Button();
            this.cmdCancel = new System.Windows.Forms.Button();
            this.ofdAbrirDocumento = new System.Windows.Forms.OpenFileDialog();
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.cboBarrio = new System.Windows.Forms.ComboBox();
            this.Label15 = new System.Windows.Forms.Label();
            this.cboDistrito = new System.Windows.Forms.ComboBox();
            this.Label14 = new System.Windows.Forms.Label();
            this.cboCanton = new System.Windows.Forms.ComboBox();
            this.Label13 = new System.Windows.Forms.Label();
            this.cboProvincia = new System.Windows.Forms.ComboBox();
            this.cboTipoIdentificacion = new System.Windows.Forms.ComboBox();
            this.Label12 = new System.Windows.Forms.Label();
            this.chkFacturaElectronica = new System.Windows.Forms.CheckBox();
            this.txtCorreoNotificacion = new System.Windows.Forms.TextBox();
            this.Label11 = new System.Windows.Forms.Label();
            this.txtNombreEmpresa = new System.Windows.Forms.TextBox();
            this.chkRecibeDocumentos = new System.Windows.Forms.CheckBox();
            this.chkAutoCompleta = new System.Windows.Forms.CheckBox();
            this.chkContabiliza = new System.Windows.Forms.CheckBox();
            this.txtLineasFactura = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtNombreComercial = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.txtIdEmpresa = new System.Windows.Forms.TextBox();
            this._lblLabels_3 = new System.Windows.Forms.Label();
            this._lblLabels_1 = new System.Windows.Forms.Label();
            this._lblLabels_0 = new System.Windows.Forms.Label();
            this.chkRegimenSimplificado = new System.Windows.Forms.CheckBox();
            this.txtCantidadDocumentos = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.cboTipoContrato = new System.Windows.Forms.ComboBox();
            this.label30 = new System.Windows.Forms.Label();
            this.txtCodigoActividad = new System.Windows.Forms.TextBox();
            this.label31 = new System.Windows.Forms.Label();
            this.txtFecha = new System.Windows.Forms.MaskedTextBox();
            this.chkAsignaVendedor = new System.Windows.Forms.CheckBox();
            this.txtDescripcionTerminal = new System.Windows.Forms.TextBox();
            this.chkDispositivoMovil = new System.Windows.Forms.CheckBox();
            this.lblSucursal = new System.Windows.Forms.Label();
            this.btnCargarTerminal = new System.Windows.Forms.Button();
            this.txtIdSucursal = new System.Windows.Forms.TextBox();
            this.btnCargarSucursal = new System.Windows.Forms.Button();
            this.label27 = new System.Windows.Forms.Label();
            this.txtValorRegistro = new System.Windows.Forms.TextBox();
            this.txtIdTerminal = new System.Windows.Forms.TextBox();
            this._lblLabels_2 = new System.Windows.Forms.Label();
            this.label32 = new System.Windows.Forms.Label();
            this.txtTelefonoSucursal = new System.Windows.Forms.TextBox();
            this.txtNombreSucursal = new System.Windows.Forms.TextBox();
            this.label34 = new System.Windows.Forms.Label();
            this.label33 = new System.Windows.Forms.Label();
            this.txtDireccionSucursal = new System.Windows.Forms.TextBox();
            this.TabRoles = new System.Windows.Forms.TabPage();
            this.cboRoles = new System.Windows.Forms.ComboBox();
            this.label6 = new System.Windows.Forms.Label();
            this.btnEliminaRole = new System.Windows.Forms.Button();
            this.btnInsertaRole = new System.Windows.Forms.Button();
            this.dgvRolePorEmpresa = new System.Windows.Forms.DataGridView();
            this.tabReportes = new System.Windows.Forms.TabPage();
            this.cboReportes = new System.Windows.Forms.ComboBox();
            this.label26 = new System.Windows.Forms.Label();
            this.btnEliminaReporte = new System.Windows.Forms.Button();
            this.btnInsertaReporte = new System.Windows.Forms.Button();
            this.dgvReportePorEmpresa = new System.Windows.Forms.DataGridView();
            this.tabContainer = new System.Windows.Forms.TabControl();
            this.TabRoles.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvRolePorEmpresa)).BeginInit();
            this.tabReportes.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportePorEmpresa)).BeginInit();
            this.tabContainer.SuspendLayout();
            this.SuspendLayout();
            // 
            // cmdUpdate
            // 
            this.cmdUpdate.BackColor = System.Drawing.SystemColors.Control;
            this.cmdUpdate.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdUpdate.Image = ((System.Drawing.Image)(resources.GetObject("cmdUpdate.Image")));
            this.cmdUpdate.Location = new System.Drawing.Point(13, 12);
            this.cmdUpdate.Name = "cmdUpdate";
            this.cmdUpdate.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdUpdate.Size = new System.Drawing.Size(25, 25);
            this.cmdUpdate.TabIndex = 41;
            this.cmdUpdate.TabStop = false;
            this.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdUpdate.UseVisualStyleBackColor = false;
            this.cmdUpdate.Click += new System.EventHandler(this.CmdUpdate_Click);
            // 
            // cmdCancel
            // 
            this.cmdCancel.BackColor = System.Drawing.SystemColors.Control;
            this.cmdCancel.Cursor = System.Windows.Forms.Cursors.Default;
            this.cmdCancel.ForeColor = System.Drawing.SystemColors.ControlText;
            this.cmdCancel.Image = ((System.Drawing.Image)(resources.GetObject("cmdCancel.Image")));
            this.cmdCancel.Location = new System.Drawing.Point(53, 12);
            this.cmdCancel.Name = "cmdCancel";
            this.cmdCancel.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.cmdCancel.Size = new System.Drawing.Size(25, 25);
            this.cmdCancel.TabIndex = 42;
            this.cmdCancel.TabStop = false;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.CmdCancel_Click);
            // 
            // cboBarrio
            // 
            this.cboBarrio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboBarrio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBarrio.FormattingEnabled = true;
            this.cboBarrio.Location = new System.Drawing.Point(137, 275);
            this.cboBarrio.Name = "cboBarrio";
            this.cboBarrio.Size = new System.Drawing.Size(147, 21);
            this.cboBarrio.TabIndex = 10;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.Transparent;
            this.Label15.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label15.Location = new System.Drawing.Point(12, 278);
            this.Label15.Name = "Label15";
            this.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label15.Size = new System.Drawing.Size(119, 17);
            this.Label15.TabIndex = 186;
            this.Label15.Text = "Barrio:";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDistrito
            // 
            this.cboDistrito.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboDistrito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDistrito.FormattingEnabled = true;
            this.cboDistrito.Location = new System.Drawing.Point(137, 248);
            this.cboDistrito.Name = "cboDistrito";
            this.cboDistrito.Size = new System.Drawing.Size(147, 21);
            this.cboDistrito.TabIndex = 9;
            this.cboDistrito.SelectedIndexChanged += new System.EventHandler(this.CboDistrito_SelectedIndexChanged);
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label14.Location = new System.Drawing.Point(12, 251);
            this.Label14.Name = "Label14";
            this.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label14.Size = new System.Drawing.Size(119, 17);
            this.Label14.TabIndex = 185;
            this.Label14.Text = "Distrito:";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCanton
            // 
            this.cboCanton.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboCanton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCanton.FormattingEnabled = true;
            this.cboCanton.Location = new System.Drawing.Point(137, 220);
            this.cboCanton.Name = "cboCanton";
            this.cboCanton.Size = new System.Drawing.Size(147, 21);
            this.cboCanton.TabIndex = 8;
            this.cboCanton.SelectedIndexChanged += new System.EventHandler(this.CboCanton_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label13.Location = new System.Drawing.Point(12, 223);
            this.Label13.Name = "Label13";
            this.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label13.Size = new System.Drawing.Size(119, 17);
            this.Label13.TabIndex = 184;
            this.Label13.Text = "Cantón:";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProvincia
            // 
            this.cboProvincia.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboProvincia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProvincia.FormattingEnabled = true;
            this.cboProvincia.Location = new System.Drawing.Point(137, 193);
            this.cboProvincia.Name = "cboProvincia";
            this.cboProvincia.Size = new System.Drawing.Size(147, 21);
            this.cboProvincia.TabIndex = 7;
            this.cboProvincia.SelectedIndexChanged += new System.EventHandler(this.CboProvincia_SelectedIndexChanged);
            // 
            // cboTipoIdentificacion
            // 
            this.cboTipoIdentificacion.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoIdentificacion.FormattingEnabled = true;
            this.cboTipoIdentificacion.Location = new System.Drawing.Point(139, 114);
            this.cboTipoIdentificacion.Name = "cboTipoIdentificacion";
            this.cboTipoIdentificacion.Size = new System.Drawing.Size(171, 21);
            this.cboTipoIdentificacion.TabIndex = 4;
            // 
            // Label12
            // 
            this.Label12.BackColor = System.Drawing.Color.Transparent;
            this.Label12.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label12.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label12.Location = new System.Drawing.Point(14, 117);
            this.Label12.Name = "Label12";
            this.Label12.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label12.Size = new System.Drawing.Size(119, 17);
            this.Label12.TabIndex = 183;
            this.Label12.Text = "Tipo identificación:";
            this.Label12.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkFacturaElectronica
            // 
            this.chkFacturaElectronica.AutoSize = true;
            this.chkFacturaElectronica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkFacturaElectronica.Location = new System.Drawing.Point(32, 497);
            this.chkFacturaElectronica.Name = "chkFacturaElectronica";
            this.chkFacturaElectronica.Size = new System.Drawing.Size(99, 17);
            this.chkFacturaElectronica.TabIndex = 40;
            this.chkFacturaElectronica.TabStop = false;
            this.chkFacturaElectronica.Text = "Empresa activa";
            this.chkFacturaElectronica.UseVisualStyleBackColor = true;
            // 
            // txtCorreoNotificacion
            // 
            this.txtCorreoNotificacion.AcceptsReturn = true;
            this.txtCorreoNotificacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtCorreoNotificacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCorreoNotificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoNotificacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCorreoNotificacion.Location = new System.Drawing.Point(137, 354);
            this.txtCorreoNotificacion.MaxLength = 200;
            this.txtCorreoNotificacion.Name = "txtCorreoNotificacion";
            this.txtCorreoNotificacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCorreoNotificacion.Size = new System.Drawing.Size(313, 20);
            this.txtCorreoNotificacion.TabIndex = 13;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label11.Location = new System.Drawing.Point(12, 357);
            this.Label11.Name = "Label11";
            this.Label11.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label11.Size = new System.Drawing.Size(119, 17);
            this.Label11.TabIndex = 182;
            this.Label11.Text = "Correo electrónico:";
            this.Label11.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombreEmpresa
            // 
            this.txtNombreEmpresa.AcceptsReturn = true;
            this.txtNombreEmpresa.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombreEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreEmpresa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNombreEmpresa.Location = new System.Drawing.Point(139, 62);
            this.txtNombreEmpresa.MaxLength = 80;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNombreEmpresa.Size = new System.Drawing.Size(313, 20);
            this.txtNombreEmpresa.TabIndex = 2;
            // 
            // chkRecibeDocumentos
            // 
            this.chkRecibeDocumentos.AutoSize = true;
            this.chkRecibeDocumentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRecibeDocumentos.Location = new System.Drawing.Point(207, 543);
            this.chkRecibeDocumentos.Name = "chkRecibeDocumentos";
            this.chkRecibeDocumentos.Size = new System.Drawing.Size(187, 17);
            this.chkRecibeDocumentos.TabIndex = 45;
            this.chkRecibeDocumentos.TabStop = false;
            this.chkRecibeDocumentos.Text = "Habilita recepcion de documentos";
            this.chkRecibeDocumentos.UseVisualStyleBackColor = true;
            // 
            // chkAutoCompleta
            // 
            this.chkAutoCompleta.AutoSize = true;
            this.chkAutoCompleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAutoCompleta.Location = new System.Drawing.Point(207, 520);
            this.chkAutoCompleta.Name = "chkAutoCompleta";
            this.chkAutoCompleta.Size = new System.Drawing.Size(189, 17);
            this.chkAutoCompleta.TabIndex = 43;
            this.chkAutoCompleta.TabStop = false;
            this.chkAutoCompleta.Text = "Auto Completar Lista de Productos";
            this.chkAutoCompleta.UseVisualStyleBackColor = true;
            // 
            // chkContabiliza
            // 
            this.chkContabiliza.AutoSize = true;
            this.chkContabiliza.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkContabiliza.Location = new System.Drawing.Point(32, 520);
            this.chkContabiliza.Name = "chkContabiliza";
            this.chkContabiliza.Size = new System.Drawing.Size(77, 17);
            this.chkContabiliza.TabIndex = 46;
            this.chkContabiliza.TabStop = false;
            this.chkContabiliza.Text = "Contabiliza";
            this.chkContabiliza.UseVisualStyleBackColor = true;
            // 
            // txtLineasFactura
            // 
            this.txtLineasFactura.AcceptsReturn = true;
            this.txtLineasFactura.BackColor = System.Drawing.SystemColors.Window;
            this.txtLineasFactura.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLineasFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineasFactura.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLineasFactura.Location = new System.Drawing.Point(137, 380);
            this.txtLineasFactura.MaxLength = 5;
            this.txtLineasFactura.Name = "txtLineasFactura";
            this.txtLineasFactura.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLineasFactura.Size = new System.Drawing.Size(42, 20);
            this.txtLineasFactura.TabIndex = 20;
            this.txtLineasFactura.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label8.Location = new System.Drawing.Point(32, 383);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label8.Size = new System.Drawing.Size(100, 17);
            this.Label8.TabIndex = 179;
            this.Label8.Text = "Líneas por Fact:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelefono
            // 
            this.txtTelefono.AcceptsReturn = true;
            this.txtTelefono.BackColor = System.Drawing.SystemColors.Window;
            this.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTelefono.Location = new System.Drawing.Point(137, 328);
            this.txtTelefono.MaxLength = 20;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTelefono.Size = new System.Drawing.Size(125, 20);
            this.txtTelefono.TabIndex = 12;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Location = new System.Drawing.Point(12, 331);
            this.Label5.Name = "Label5";
            this.Label5.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label5.Size = new System.Drawing.Size(119, 17);
            this.Label5.TabIndex = 176;
            this.Label5.Text = "Teléfono:";
            this.Label5.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDireccion
            // 
            this.txtDireccion.AcceptsReturn = true;
            this.txtDireccion.BackColor = System.Drawing.SystemColors.Window;
            this.txtDireccion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDireccion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDireccion.Location = new System.Drawing.Point(137, 302);
            this.txtDireccion.MaxLength = 160;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDireccion.Size = new System.Drawing.Size(313, 20);
            this.txtDireccion.TabIndex = 11;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(12, 305);
            this.Label4.Name = "Label4";
            this.Label4.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label4.Size = new System.Drawing.Size(119, 17);
            this.Label4.TabIndex = 175;
            this.Label4.Text = "Otras señas:";
            this.Label4.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label3
            // 
            this.Label3.BackColor = System.Drawing.Color.Transparent;
            this.Label3.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label3.Location = new System.Drawing.Point(12, 196);
            this.Label3.Name = "Label3";
            this.Label3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label3.Size = new System.Drawing.Size(119, 17);
            this.Label3.TabIndex = 174;
            this.Label3.Text = "Provincia:";
            this.Label3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdentificacion
            // 
            this.txtIdentificacion.AcceptsReturn = true;
            this.txtIdentificacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtIdentificacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdentificacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIdentificacion.Location = new System.Drawing.Point(139, 141);
            this.txtIdentificacion.MaxLength = 20;
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdentificacion.Size = new System.Drawing.Size(146, 20);
            this.txtIdentificacion.TabIndex = 5;
            // 
            // Label2
            // 
            this.Label2.BackColor = System.Drawing.Color.Transparent;
            this.Label2.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label2.Location = new System.Drawing.Point(15, 144);
            this.Label2.Name = "Label2";
            this.Label2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label2.Size = new System.Drawing.Size(119, 17);
            this.Label2.TabIndex = 173;
            this.Label2.Text = "Identificación:";
            this.Label2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtNombreComercial
            // 
            this.txtNombreComercial.AcceptsReturn = true;
            this.txtNombreComercial.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombreComercial.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreComercial.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreComercial.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNombreComercial.Location = new System.Drawing.Point(139, 88);
            this.txtNombreComercial.MaxLength = 80;
            this.txtNombreComercial.Name = "txtNombreComercial";
            this.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNombreComercial.Size = new System.Drawing.Size(313, 20);
            this.txtNombreComercial.TabIndex = 3;
            // 
            // Label1
            // 
            this.Label1.BackColor = System.Drawing.Color.Transparent;
            this.Label1.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label1.Location = new System.Drawing.Point(17, 91);
            this.Label1.Name = "Label1";
            this.Label1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label1.Size = new System.Drawing.Size(116, 17);
            this.Label1.TabIndex = 171;
            this.Label1.Text = "Nombre comercial:";
            this.Label1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdEmpresa
            // 
            this.txtIdEmpresa.AcceptsReturn = true;
            this.txtIdEmpresa.BackColor = System.Drawing.SystemColors.Window;
            this.txtIdEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdEmpresa.Enabled = false;
            this.txtIdEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdEmpresa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIdEmpresa.Location = new System.Drawing.Point(139, 36);
            this.txtIdEmpresa.MaxLength = 0;
            this.txtIdEmpresa.Name = "txtIdEmpresa";
            this.txtIdEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdEmpresa.Size = new System.Drawing.Size(49, 20);
            this.txtIdEmpresa.TabIndex = 1;
            this.txtIdEmpresa.TabStop = false;
            // 
            // _lblLabels_3
            // 
            this._lblLabels_3.BackColor = System.Drawing.Color.Transparent;
            this._lblLabels_3.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblLabels_3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLabels_3.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblLabels_3.Location = new System.Drawing.Point(14, 65);
            this._lblLabels_3.Name = "_lblLabels_3";
            this._lblLabels_3.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblLabels_3.Size = new System.Drawing.Size(119, 17);
            this._lblLabels_3.TabIndex = 151;
            this._lblLabels_3.Text = "Nombre empresa:";
            this._lblLabels_3.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLabels_1
            // 
            this._lblLabels_1.BackColor = System.Drawing.Color.Transparent;
            this._lblLabels_1.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblLabels_1.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLabels_1.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblLabels_1.Location = new System.Drawing.Point(18, 409);
            this._lblLabels_1.Name = "_lblLabels_1";
            this._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblLabels_1.Size = new System.Drawing.Size(113, 17);
            this._lblLabels_1.TabIndex = 149;
            this._lblLabels_1.Text = "Fecha vencimiento:";
            this._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLabels_0
            // 
            this._lblLabels_0.BackColor = System.Drawing.Color.Transparent;
            this._lblLabels_0.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblLabels_0.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLabels_0.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblLabels_0.Location = new System.Drawing.Point(14, 39);
            this._lblLabels_0.Name = "_lblLabels_0";
            this._lblLabels_0.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblLabels_0.Size = new System.Drawing.Size(119, 17);
            this._lblLabels_0.TabIndex = 133;
            this._lblLabels_0.Text = "No. Equipo:";
            this._lblLabels_0.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkRegimenSimplificado
            // 
            this.chkRegimenSimplificado.AutoSize = true;
            this.chkRegimenSimplificado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRegimenSimplificado.Location = new System.Drawing.Point(207, 497);
            this.chkRegimenSimplificado.Name = "chkRegimenSimplificado";
            this.chkRegimenSimplificado.Size = new System.Drawing.Size(127, 17);
            this.chkRegimenSimplificado.TabIndex = 41;
            this.chkRegimenSimplificado.TabStop = false;
            this.chkRegimenSimplificado.Text = "Regimen Simplificado";
            this.chkRegimenSimplificado.UseVisualStyleBackColor = true;
            // 
            // txtCantidadDocumentos
            // 
            this.txtCantidadDocumentos.AcceptsReturn = true;
            this.txtCantidadDocumentos.BackColor = System.Drawing.SystemColors.Window;
            this.txtCantidadDocumentos.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCantidadDocumentos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCantidadDocumentos.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCantidadDocumentos.Location = new System.Drawing.Point(136, 459);
            this.txtCantidadDocumentos.MaxLength = 0;
            this.txtCantidadDocumentos.Name = "txtCantidadDocumentos";
            this.txtCantidadDocumentos.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCantidadDocumentos.Size = new System.Drawing.Size(49, 20);
            this.txtCantidadDocumentos.TabIndex = 23;
            this.txtCantidadDocumentos.TabStop = false;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.Transparent;
            this.label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(17, 462);
            this.label7.Name = "label7";
            this.label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label7.Size = new System.Drawing.Size(113, 17);
            this.label7.TabIndex = 204;
            this.label7.Text = "Cantidad documentos:";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboTipoContrato
            // 
            this.cboTipoContrato.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboTipoContrato.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoContrato.FormattingEnabled = true;
            this.cboTipoContrato.Location = new System.Drawing.Point(136, 432);
            this.cboTipoContrato.Name = "cboTipoContrato";
            this.cboTipoContrato.Size = new System.Drawing.Size(126, 21);
            this.cboTipoContrato.TabIndex = 22;
            // 
            // label30
            // 
            this.label30.BackColor = System.Drawing.Color.Transparent;
            this.label30.Cursor = System.Windows.Forms.Cursors.Default;
            this.label30.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label30.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label30.Location = new System.Drawing.Point(17, 433);
            this.label30.Name = "label30";
            this.label30.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label30.Size = new System.Drawing.Size(113, 17);
            this.label30.TabIndex = 206;
            this.label30.Text = "Tipo Contrato:";
            this.label30.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtCodigoActividad
            // 
            this.txtCodigoActividad.AcceptsReturn = true;
            this.txtCodigoActividad.BackColor = System.Drawing.SystemColors.Window;
            this.txtCodigoActividad.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCodigoActividad.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoActividad.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigoActividad.Location = new System.Drawing.Point(138, 167);
            this.txtCodigoActividad.MaxLength = 6;
            this.txtCodigoActividad.Name = "txtCodigoActividad";
            this.txtCodigoActividad.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCodigoActividad.Size = new System.Drawing.Size(63, 20);
            this.txtCodigoActividad.TabIndex = 6;
            // 
            // label31
            // 
            this.label31.BackColor = System.Drawing.Color.Transparent;
            this.label31.Cursor = System.Windows.Forms.Cursors.Default;
            this.label31.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label31.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label31.Location = new System.Drawing.Point(14, 170);
            this.label31.Name = "label31";
            this.label31.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label31.Size = new System.Drawing.Size(119, 17);
            this.label31.TabIndex = 208;
            this.label31.Text = "Actividad Económica:";
            this.label31.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtFecha
            // 
            this.txtFecha.Location = new System.Drawing.Point(137, 406);
            this.txtFecha.Mask = "00/00/0000";
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.Size = new System.Drawing.Size(82, 20);
            this.txtFecha.TabIndex = 21;
            this.txtFecha.ValidatingType = typeof(System.DateTime);
            // 
            // chkAsignaVendedor
            // 
            this.chkAsignaVendedor.AutoSize = true;
            this.chkAsignaVendedor.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkAsignaVendedor.Location = new System.Drawing.Point(32, 543);
            this.chkAsignaVendedor.Name = "chkAsignaVendedor";
            this.chkAsignaVendedor.Size = new System.Drawing.Size(163, 17);
            this.chkAsignaVendedor.TabIndex = 209;
            this.chkAsignaVendedor.TabStop = false;
            this.chkAsignaVendedor.Text = "Asigna vendedor por defecto";
            this.chkAsignaVendedor.UseVisualStyleBackColor = true;
            // 
            // txtDescripcionTerminal
            // 
            this.txtDescripcionTerminal.AcceptsReturn = true;
            this.txtDescripcionTerminal.BackColor = System.Drawing.SystemColors.Window;
            this.txtDescripcionTerminal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDescripcionTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDescripcionTerminal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDescripcionTerminal.Location = new System.Drawing.Point(637, 484);
            this.txtDescripcionTerminal.MaxLength = 0;
            this.txtDescripcionTerminal.Name = "txtDescripcionTerminal";
            this.txtDescripcionTerminal.ReadOnly = true;
            this.txtDescripcionTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDescripcionTerminal.Size = new System.Drawing.Size(164, 20);
            this.txtDescripcionTerminal.TabIndex = 262;
            this.txtDescripcionTerminal.TabStop = false;
            // 
            // chkDispositivoMovil
            // 
            this.chkDispositivoMovil.AutoSize = true;
            this.chkDispositivoMovil.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDispositivoMovil.Location = new System.Drawing.Point(536, 537);
            this.chkDispositivoMovil.Name = "chkDispositivoMovil";
            this.chkDispositivoMovil.Size = new System.Drawing.Size(104, 17);
            this.chkDispositivoMovil.TabIndex = 261;
            this.chkDispositivoMovil.TabStop = false;
            this.chkDispositivoMovil.Text = "Dispositivo móvil";
            this.chkDispositivoMovil.UseVisualStyleBackColor = true;
            // 
            // lblSucursal
            // 
            this.lblSucursal.BackColor = System.Drawing.Color.Transparent;
            this.lblSucursal.Cursor = System.Windows.Forms.Cursors.Default;
            this.lblSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblSucursal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.lblSucursal.Location = new System.Drawing.Point(467, 382);
            this.lblSucursal.Name = "lblSucursal";
            this.lblSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.lblSucursal.Size = new System.Drawing.Size(64, 17);
            this.lblSucursal.TabIndex = 252;
            this.lblSucursal.Text = "Sucursal:";
            this.lblSucursal.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCargarTerminal
            // 
            this.btnCargarTerminal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCargarTerminal.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCargarTerminal.Enabled = false;
            this.btnCargarTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarTerminal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCargarTerminal.Location = new System.Drawing.Point(585, 483);
            this.btnCargarTerminal.Name = "btnCargarTerminal";
            this.btnCargarTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCargarTerminal.Size = new System.Drawing.Size(46, 22);
            this.btnCargarTerminal.TabIndex = 260;
            this.btnCargarTerminal.TabStop = false;
            this.btnCargarTerminal.Text = "Cargar";
            this.btnCargarTerminal.UseVisualStyleBackColor = false;
            this.btnCargarTerminal.Click += new System.EventHandler(this.BtnCargarTerminal_Click);
            // 
            // txtIdSucursal
            // 
            this.txtIdSucursal.AcceptsReturn = true;
            this.txtIdSucursal.BackColor = System.Drawing.SystemColors.Window;
            this.txtIdSucursal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdSucursal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIdSucursal.Location = new System.Drawing.Point(537, 380);
            this.txtIdSucursal.MaxLength = 5;
            this.txtIdSucursal.Name = "txtIdSucursal";
            this.txtIdSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdSucursal.Size = new System.Drawing.Size(44, 20);
            this.txtIdSucursal.TabIndex = 246;
            // 
            // btnCargarSucursal
            // 
            this.btnCargarSucursal.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCargarSucursal.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCargarSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarSucursal.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCargarSucursal.Location = new System.Drawing.Point(585, 379);
            this.btnCargarSucursal.Name = "btnCargarSucursal";
            this.btnCargarSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCargarSucursal.Size = new System.Drawing.Size(46, 22);
            this.btnCargarSucursal.TabIndex = 259;
            this.btnCargarSucursal.TabStop = false;
            this.btnCargarSucursal.Text = "Cargar";
            this.btnCargarSucursal.UseVisualStyleBackColor = false;
            this.btnCargarSucursal.Click += new System.EventHandler(this.BtnCargarSucursal_Click);
            // 
            // label27
            // 
            this.label27.BackColor = System.Drawing.Color.Transparent;
            this.label27.Cursor = System.Windows.Forms.Cursors.Default;
            this.label27.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Location = new System.Drawing.Point(476, 486);
            this.label27.Name = "label27";
            this.label27.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label27.Size = new System.Drawing.Size(54, 17);
            this.label27.TabIndex = 253;
            this.label27.Text = "Terminal:";
            this.label27.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtValorRegistro
            // 
            this.txtValorRegistro.AcceptsReturn = true;
            this.txtValorRegistro.BackColor = System.Drawing.SystemColors.Window;
            this.txtValorRegistro.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtValorRegistro.Enabled = false;
            this.txtValorRegistro.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtValorRegistro.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtValorRegistro.Location = new System.Drawing.Point(536, 511);
            this.txtValorRegistro.MaxLength = 0;
            this.txtValorRegistro.Name = "txtValorRegistro";
            this.txtValorRegistro.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtValorRegistro.Size = new System.Drawing.Size(266, 20);
            this.txtValorRegistro.TabIndex = 251;
            this.txtValorRegistro.TabStop = false;
            // 
            // txtIdTerminal
            // 
            this.txtIdTerminal.AcceptsReturn = true;
            this.txtIdTerminal.BackColor = System.Drawing.SystemColors.Window;
            this.txtIdTerminal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdTerminal.Enabled = false;
            this.txtIdTerminal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdTerminal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIdTerminal.Location = new System.Drawing.Point(536, 484);
            this.txtIdTerminal.MaxLength = 5;
            this.txtIdTerminal.Name = "txtIdTerminal";
            this.txtIdTerminal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdTerminal.Size = new System.Drawing.Size(44, 20);
            this.txtIdTerminal.TabIndex = 250;
            // 
            // _lblLabels_2
            // 
            this._lblLabels_2.BackColor = System.Drawing.Color.Transparent;
            this._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblLabels_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblLabels_2.ImageAlign = System.Drawing.ContentAlignment.MiddleRight;
            this._lblLabels_2.Location = new System.Drawing.Point(478, 512);
            this._lblLabels_2.Name = "_lblLabels_2";
            this._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblLabels_2.Size = new System.Drawing.Size(52, 17);
            this._lblLabels_2.TabIndex = 258;
            this._lblLabels_2.Text = "Equipo:";
            this._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label32
            // 
            this.label32.BackColor = System.Drawing.Color.Transparent;
            this.label32.Cursor = System.Windows.Forms.Cursors.Default;
            this.label32.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label32.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label32.Location = new System.Drawing.Point(467, 407);
            this.label32.Name = "label32";
            this.label32.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label32.Size = new System.Drawing.Size(64, 17);
            this.label32.TabIndex = 255;
            this.label32.Text = "Nombre:";
            this.label32.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtTelefonoSucursal
            // 
            this.txtTelefonoSucursal.AcceptsReturn = true;
            this.txtTelefonoSucursal.BackColor = System.Drawing.SystemColors.Window;
            this.txtTelefonoSucursal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTelefonoSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefonoSucursal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTelefonoSucursal.Location = new System.Drawing.Point(536, 458);
            this.txtTelefonoSucursal.MaxLength = 50;
            this.txtTelefonoSucursal.Name = "txtTelefonoSucursal";
            this.txtTelefonoSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTelefonoSucursal.Size = new System.Drawing.Size(115, 20);
            this.txtTelefonoSucursal.TabIndex = 249;
            // 
            // txtNombreSucursal
            // 
            this.txtNombreSucursal.AcceptsReturn = true;
            this.txtNombreSucursal.BackColor = System.Drawing.SystemColors.Window;
            this.txtNombreSucursal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtNombreSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtNombreSucursal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtNombreSucursal.Location = new System.Drawing.Point(536, 406);
            this.txtNombreSucursal.MaxLength = 50;
            this.txtNombreSucursal.Name = "txtNombreSucursal";
            this.txtNombreSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNombreSucursal.Size = new System.Drawing.Size(265, 20);
            this.txtNombreSucursal.TabIndex = 247;
            // 
            // label34
            // 
            this.label34.BackColor = System.Drawing.Color.Transparent;
            this.label34.Cursor = System.Windows.Forms.Cursors.Default;
            this.label34.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label34.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label34.Location = new System.Drawing.Point(467, 459);
            this.label34.Name = "label34";
            this.label34.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label34.Size = new System.Drawing.Size(64, 17);
            this.label34.TabIndex = 257;
            this.label34.Text = "Teléfono:";
            this.label34.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label33
            // 
            this.label33.BackColor = System.Drawing.Color.Transparent;
            this.label33.Cursor = System.Windows.Forms.Cursors.Default;
            this.label33.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label33.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label33.Location = new System.Drawing.Point(467, 433);
            this.label33.Name = "label33";
            this.label33.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label33.Size = new System.Drawing.Size(64, 17);
            this.label33.TabIndex = 256;
            this.label33.Text = "Dirección:";
            this.label33.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtDireccionSucursal
            // 
            this.txtDireccionSucursal.AcceptsReturn = true;
            this.txtDireccionSucursal.BackColor = System.Drawing.SystemColors.Window;
            this.txtDireccionSucursal.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtDireccionSucursal.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtDireccionSucursal.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtDireccionSucursal.Location = new System.Drawing.Point(536, 432);
            this.txtDireccionSucursal.MaxLength = 50;
            this.txtDireccionSucursal.Name = "txtDireccionSucursal";
            this.txtDireccionSucursal.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDireccionSucursal.Size = new System.Drawing.Size(265, 20);
            this.txtDireccionSucursal.TabIndex = 248;
            // 
            // TabRoles
            // 
            this.TabRoles.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.TabRoles.Controls.Add(this.cboRoles);
            this.TabRoles.Controls.Add(this.label6);
            this.TabRoles.Controls.Add(this.btnEliminaRole);
            this.TabRoles.Controls.Add(this.btnInsertaRole);
            this.TabRoles.Controls.Add(this.dgvRolePorEmpresa);
            this.TabRoles.Location = new System.Drawing.Point(4, 22);
            this.TabRoles.Name = "TabRoles";
            this.TabRoles.Padding = new System.Windows.Forms.Padding(3);
            this.TabRoles.Size = new System.Drawing.Size(378, 335);
            this.TabRoles.TabIndex = 2;
            this.TabRoles.Text = "Roles";
            // 
            // cboRoles
            // 
            this.cboRoles.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboRoles.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboRoles.FormattingEnabled = true;
            this.cboRoles.Location = new System.Drawing.Point(10, 31);
            this.cboRoles.Name = "cboRoles";
            this.cboRoles.Size = new System.Drawing.Size(361, 21);
            this.cboRoles.TabIndex = 216;
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.Transparent;
            this.label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(7, 11);
            this.label6.Name = "label6";
            this.label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label6.Size = new System.Drawing.Size(116, 17);
            this.label6.TabIndex = 220;
            this.label6.Text = "Roles por empresa:";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEliminaRole
            // 
            this.btnEliminaRole.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminaRole.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEliminaRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminaRole.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEliminaRole.Location = new System.Drawing.Point(74, 303);
            this.btnEliminaRole.Name = "btnEliminaRole";
            this.btnEliminaRole.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEliminaRole.Size = new System.Drawing.Size(65, 26);
            this.btnEliminaRole.TabIndex = 219;
            this.btnEliminaRole.TabStop = false;
            this.btnEliminaRole.Text = "Eliminar";
            this.btnEliminaRole.UseVisualStyleBackColor = false;
            this.btnEliminaRole.Click += new System.EventHandler(this.btnEliminaRole_Click);
            // 
            // btnInsertaRole
            // 
            this.btnInsertaRole.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInsertaRole.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInsertaRole.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertaRole.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInsertaRole.Location = new System.Drawing.Point(8, 303);
            this.btnInsertaRole.Name = "btnInsertaRole";
            this.btnInsertaRole.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInsertaRole.Size = new System.Drawing.Size(65, 26);
            this.btnInsertaRole.TabIndex = 218;
            this.btnInsertaRole.TabStop = false;
            this.btnInsertaRole.Text = "Insertar";
            this.btnInsertaRole.UseVisualStyleBackColor = false;
            this.btnInsertaRole.Click += new System.EventHandler(this.btnInsertaRole_Click);
            // 
            // dgvRolePorEmpresa
            // 
            this.dgvRolePorEmpresa.AllowUserToAddRows = false;
            this.dgvRolePorEmpresa.AllowUserToDeleteRows = false;
            this.dgvRolePorEmpresa.AllowUserToResizeColumns = false;
            this.dgvRolePorEmpresa.AllowUserToResizeRows = false;
            this.dgvRolePorEmpresa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvRolePorEmpresa.Location = new System.Drawing.Point(8, 58);
            this.dgvRolePorEmpresa.Name = "dgvRolePorEmpresa";
            this.dgvRolePorEmpresa.RowHeadersVisible = false;
            this.dgvRolePorEmpresa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvRolePorEmpresa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvRolePorEmpresa.Size = new System.Drawing.Size(362, 239);
            this.dgvRolePorEmpresa.TabIndex = 217;
            this.dgvRolePorEmpresa.TabStop = false;
            // 
            // tabReportes
            // 
            this.tabReportes.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.tabReportes.Controls.Add(this.cboReportes);
            this.tabReportes.Controls.Add(this.label26);
            this.tabReportes.Controls.Add(this.btnEliminaReporte);
            this.tabReportes.Controls.Add(this.btnInsertaReporte);
            this.tabReportes.Controls.Add(this.dgvReportePorEmpresa);
            this.tabReportes.Location = new System.Drawing.Point(4, 22);
            this.tabReportes.Name = "tabReportes";
            this.tabReportes.Padding = new System.Windows.Forms.Padding(3);
            this.tabReportes.Size = new System.Drawing.Size(378, 335);
            this.tabReportes.TabIndex = 1;
            this.tabReportes.Text = "Reportes";
            // 
            // cboReportes
            // 
            this.cboReportes.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.cboReportes.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboReportes.FormattingEnabled = true;
            this.cboReportes.Location = new System.Drawing.Point(10, 31);
            this.cboReportes.Name = "cboReportes";
            this.cboReportes.Size = new System.Drawing.Size(361, 21);
            this.cboReportes.TabIndex = 211;
            // 
            // label26
            // 
            this.label26.BackColor = System.Drawing.Color.Transparent;
            this.label26.Cursor = System.Windows.Forms.Cursors.Default;
            this.label26.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label26.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label26.Location = new System.Drawing.Point(7, 11);
            this.label26.Name = "label26";
            this.label26.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.label26.Size = new System.Drawing.Size(116, 17);
            this.label26.TabIndex = 215;
            this.label26.Text = "Reportes por empresa:";
            this.label26.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // btnEliminaReporte
            // 
            this.btnEliminaReporte.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminaReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEliminaReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminaReporte.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEliminaReporte.Location = new System.Drawing.Point(74, 303);
            this.btnEliminaReporte.Name = "btnEliminaReporte";
            this.btnEliminaReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEliminaReporte.Size = new System.Drawing.Size(65, 26);
            this.btnEliminaReporte.TabIndex = 214;
            this.btnEliminaReporte.TabStop = false;
            this.btnEliminaReporte.Text = "Eliminar";
            this.btnEliminaReporte.UseVisualStyleBackColor = false;
            this.btnEliminaReporte.Click += new System.EventHandler(this.BtnEliminaReporte_Click);
            // 
            // btnInsertaReporte
            // 
            this.btnInsertaReporte.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInsertaReporte.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInsertaReporte.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertaReporte.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInsertaReporte.Location = new System.Drawing.Point(8, 303);
            this.btnInsertaReporte.Name = "btnInsertaReporte";
            this.btnInsertaReporte.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInsertaReporte.Size = new System.Drawing.Size(65, 26);
            this.btnInsertaReporte.TabIndex = 213;
            this.btnInsertaReporte.TabStop = false;
            this.btnInsertaReporte.Text = "Insertar";
            this.btnInsertaReporte.UseVisualStyleBackColor = false;
            this.btnInsertaReporte.Click += new System.EventHandler(this.BtnInsertaReporte_Click);
            // 
            // dgvReportePorEmpresa
            // 
            this.dgvReportePorEmpresa.AllowUserToAddRows = false;
            this.dgvReportePorEmpresa.AllowUserToDeleteRows = false;
            this.dgvReportePorEmpresa.AllowUserToResizeColumns = false;
            this.dgvReportePorEmpresa.AllowUserToResizeRows = false;
            this.dgvReportePorEmpresa.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            this.dgvReportePorEmpresa.Location = new System.Drawing.Point(8, 58);
            this.dgvReportePorEmpresa.Name = "dgvReportePorEmpresa";
            this.dgvReportePorEmpresa.RowHeadersVisible = false;
            this.dgvReportePorEmpresa.RowHeadersWidthSizeMode = System.Windows.Forms.DataGridViewRowHeadersWidthSizeMode.DisableResizing;
            this.dgvReportePorEmpresa.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dgvReportePorEmpresa.Size = new System.Drawing.Size(362, 239);
            this.dgvReportePorEmpresa.TabIndex = 212;
            this.dgvReportePorEmpresa.TabStop = false;
            // 
            // tabContainer
            // 
            this.tabContainer.Controls.Add(this.tabReportes);
            this.tabContainer.Controls.Add(this.TabRoles);
            this.tabContainer.Location = new System.Drawing.Point(458, 12);
            this.tabContainer.Name = "tabContainer";
            this.tabContainer.SelectedIndex = 0;
            this.tabContainer.Size = new System.Drawing.Size(386, 361);
            this.tabContainer.TabIndex = 24;
            this.tabContainer.TabStop = false;
            // 
            // FrmEmpresa
            // 
            this.AcceptButton = this.btnInsertaRole;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(851, 571);
            this.Controls.Add(this.txtDescripcionTerminal);
            this.Controls.Add(this.chkDispositivoMovil);
            this.Controls.Add(this.lblSucursal);
            this.Controls.Add(this.btnCargarTerminal);
            this.Controls.Add(this.txtIdSucursal);
            this.Controls.Add(this.btnCargarSucursal);
            this.Controls.Add(this.label27);
            this.Controls.Add(this.txtValorRegistro);
            this.Controls.Add(this.txtIdTerminal);
            this.Controls.Add(this._lblLabels_2);
            this.Controls.Add(this.label32);
            this.Controls.Add(this.txtTelefonoSucursal);
            this.Controls.Add(this.txtNombreSucursal);
            this.Controls.Add(this.label34);
            this.Controls.Add(this.label33);
            this.Controls.Add(this.txtDireccionSucursal);
            this.Controls.Add(this.chkAsignaVendedor);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtCodigoActividad);
            this.Controls.Add(this.label31);
            this.Controls.Add(this.label30);
            this.Controls.Add(this.cboTipoContrato);
            this.Controls.Add(this.txtCantidadDocumentos);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.tabContainer);
            this.Controls.Add(this.chkRegimenSimplificado);
            this.Controls.Add(this.cboBarrio);
            this.Controls.Add(this.Label15);
            this.Controls.Add(this.cboDistrito);
            this.Controls.Add(this.Label14);
            this.Controls.Add(this.cboCanton);
            this.Controls.Add(this.Label13);
            this.Controls.Add(this.cboProvincia);
            this.Controls.Add(this.cboTipoIdentificacion);
            this.Controls.Add(this.Label12);
            this.Controls.Add(this.chkFacturaElectronica);
            this.Controls.Add(this.txtCorreoNotificacion);
            this.Controls.Add(this.Label11);
            this.Controls.Add(this.txtNombreEmpresa);
            this.Controls.Add(this.chkRecibeDocumentos);
            this.Controls.Add(this.chkAutoCompleta);
            this.Controls.Add(this.chkContabiliza);
            this.Controls.Add(this.txtLineasFactura);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtIdentificacion);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtNombreComercial);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.txtIdEmpresa);
            this.Controls.Add(this._lblLabels_3);
            this.Controls.Add(this._lblLabels_1);
            this.Controls.Add(this._lblLabels_0);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdCancel);
            this.Name = "FrmEmpresa";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Registrar/Actualizar datos de la empresa seleccionada";
            this.Load += new System.EventHandler(this.FrmEmpresa_Load);
            this.TabRoles.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvRolePorEmpresa)).EndInit();
            this.tabReportes.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvReportePorEmpresa)).EndInit();
            this.tabContainer.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button cmdUpdate;
        public System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.OpenFileDialog ofdAbrirDocumento;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        internal System.Windows.Forms.ComboBox cboBarrio;
        public System.Windows.Forms.Label Label15;
        internal System.Windows.Forms.ComboBox cboDistrito;
        public System.Windows.Forms.Label Label14;
        internal System.Windows.Forms.ComboBox cboCanton;
        public System.Windows.Forms.Label Label13;
        internal System.Windows.Forms.ComboBox cboProvincia;
        internal System.Windows.Forms.ComboBox cboTipoIdentificacion;
        public System.Windows.Forms.Label Label12;
        internal System.Windows.Forms.CheckBox chkFacturaElectronica;
        public System.Windows.Forms.TextBox txtCorreoNotificacion;
        public System.Windows.Forms.Label Label11;
        public System.Windows.Forms.TextBox txtNombreEmpresa;
        internal System.Windows.Forms.CheckBox chkRecibeDocumentos;
        internal System.Windows.Forms.CheckBox chkAutoCompleta;
        internal System.Windows.Forms.CheckBox chkContabiliza;
        public System.Windows.Forms.TextBox txtLineasFactura;
        public System.Windows.Forms.Label Label8;
        public System.Windows.Forms.TextBox txtTelefono;
        public System.Windows.Forms.Label Label5;
        public System.Windows.Forms.TextBox txtDireccion;
        public System.Windows.Forms.Label Label4;
        public System.Windows.Forms.Label Label3;
        public System.Windows.Forms.TextBox txtIdentificacion;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.TextBox txtNombreComercial;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.TextBox txtIdEmpresa;
        public System.Windows.Forms.Label _lblLabels_3;
        public System.Windows.Forms.Label _lblLabels_1;
        public System.Windows.Forms.Label _lblLabels_0;
        internal System.Windows.Forms.CheckBox chkRegimenSimplificado;
        public System.Windows.Forms.TextBox txtCantidadDocumentos;
        public System.Windows.Forms.Label label7;
        internal System.Windows.Forms.ComboBox cboTipoContrato;
        public System.Windows.Forms.Label label30;
        public System.Windows.Forms.TextBox txtCodigoActividad;
        public System.Windows.Forms.Label label31;
        private System.Windows.Forms.MaskedTextBox txtFecha;
        internal System.Windows.Forms.CheckBox chkAsignaVendedor;
        public System.Windows.Forms.TextBox txtDescripcionTerminal;
        internal System.Windows.Forms.CheckBox chkDispositivoMovil;
        public System.Windows.Forms.Label lblSucursal;
        public System.Windows.Forms.Button btnCargarTerminal;
        public System.Windows.Forms.TextBox txtIdSucursal;
        public System.Windows.Forms.Button btnCargarSucursal;
        public System.Windows.Forms.Label label27;
        public System.Windows.Forms.TextBox txtValorRegistro;
        public System.Windows.Forms.TextBox txtIdTerminal;
        public System.Windows.Forms.Label _lblLabels_2;
        public System.Windows.Forms.Label label32;
        public System.Windows.Forms.TextBox txtTelefonoSucursal;
        public System.Windows.Forms.TextBox txtNombreSucursal;
        public System.Windows.Forms.Label label34;
        public System.Windows.Forms.Label label33;
        public System.Windows.Forms.TextBox txtDireccionSucursal;
        private System.Windows.Forms.TabPage TabRoles;
        internal System.Windows.Forms.ComboBox cboRoles;
        public System.Windows.Forms.Label label6;
        public System.Windows.Forms.Button btnEliminaRole;
        public System.Windows.Forms.Button btnInsertaRole;
        internal System.Windows.Forms.DataGridView dgvRolePorEmpresa;
        private System.Windows.Forms.TabPage tabReportes;
        internal System.Windows.Forms.ComboBox cboReportes;
        public System.Windows.Forms.Label label26;
        public System.Windows.Forms.Button btnEliminaReporte;
        public System.Windows.Forms.Button btnInsertaReporte;
        internal System.Windows.Forms.DataGridView dgvReportePorEmpresa;
        private System.Windows.Forms.TabControl tabContainer;
    }
}

