﻿namespace LeandroSoftware.Activator
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
            this.txtClaveATV = new System.Windows.Forms.TextBox();
            this.Label25 = new System.Windows.Forms.Label();
            this.txtUsuarioATV = new System.Windows.Forms.TextBox();
            this.Label24 = new System.Windows.Forms.Label();
            this.btnCargarLogo = new System.Windows.Forms.Button();
            this.picLogo = new System.Windows.Forms.PictureBox();
            this.Label23 = new System.Windows.Forms.Label();
            this.Label22 = new System.Windows.Forms.Label();
            this.Label21 = new System.Windows.Forms.Label();
            this.Label20 = new System.Windows.Forms.Label();
            this.txtUltimoDocMR = new System.Windows.Forms.TextBox();
            this.txtUltimoDocTE = new System.Windows.Forms.TextBox();
            this.txtUltimoDocNC = new System.Windows.Forms.TextBox();
            this.txtUltimoDocND = new System.Windows.Forms.TextBox();
            this.txtUltimoDocFE = new System.Windows.Forms.TextBox();
            this.Label19 = new System.Windows.Forms.Label();
            this.txtPinCertificado = new System.Windows.Forms.TextBox();
            this.Label18 = new System.Windows.Forms.Label();
            this.txtIdCertificado = new System.Windows.Forms.TextBox();
            this.Label17 = new System.Windows.Forms.Label();
            this.txtServicioFacturaElectronica = new System.Windows.Forms.TextBox();
            this.Label16 = new System.Windows.Forms.Label();
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
            this.chkCierrePorTurnos = new System.Windows.Forms.CheckBox();
            this.chkRespaldoEnLinea = new System.Windows.Forms.CheckBox();
            this.chkIncluyeInsumosEnFactura = new System.Windows.Forms.CheckBox();
            this.chkUsaImpresoraImpacto = new System.Windows.Forms.CheckBox();
            this.txtCodigoServInst = new System.Windows.Forms.TextBox();
            this.Label10 = new System.Windows.Forms.Label();
            this.txtPorcentajeInstalacion = new System.Windows.Forms.TextBox();
            this.Label9 = new System.Windows.Forms.Label();
            this.chkDesgloseInst = new System.Windows.Forms.CheckBox();
            this.chkModificaDesc = new System.Windows.Forms.CheckBox();
            this.ckbAutoCompleta = new System.Windows.Forms.CheckBox();
            this.ckbContabiliza = new System.Windows.Forms.CheckBox();
            this.txtLineasFactura = new System.Windows.Forms.TextBox();
            this.Label8 = new System.Windows.Forms.Label();
            this.txtPorcentajeIVA = new System.Windows.Forms.TextBox();
            this.Label7 = new System.Windows.Forms.Label();
            this.txtImpresoraFactura = new System.Windows.Forms.TextBox();
            this.Label6 = new System.Windows.Forms.Label();
            this.txtTelefono = new System.Windows.Forms.TextBox();
            this.Label5 = new System.Windows.Forms.Label();
            this.txtDireccion = new System.Windows.Forms.TextBox();
            this.Label4 = new System.Windows.Forms.Label();
            this.Label3 = new System.Windows.Forms.Label();
            this.txtIdentificacion = new System.Windows.Forms.TextBox();
            this.Label2 = new System.Windows.Forms.Label();
            this.txtNombreComercial = new System.Windows.Forms.TextBox();
            this.Label1 = new System.Windows.Forms.Label();
            this.btnEliminarDetalle = new System.Windows.Forms.Button();
            this.btnInsertarDetalle = new System.Windows.Forms.Button();
            this.dgvEquipos = new System.Windows.Forms.DataGridView();
            this.CmdConsultar = new System.Windows.Forms.Button();
            this.txtFecha = new System.Windows.Forms.TextBox();
            this.txtEquipo = new System.Windows.Forms.TextBox();
            this.txtIdEmpresa = new System.Windows.Forms.TextBox();
            this._lblLabels_3 = new System.Windows.Forms.Label();
            this._lblLabels_1 = new System.Windows.Forms.Label();
            this._lblLabels_2 = new System.Windows.Forms.Label();
            this._lblLabels_0 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipos)).BeginInit();
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
            this.cmdUpdate.TabIndex = 127;
            this.cmdUpdate.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdUpdate.UseVisualStyleBackColor = false;
            this.cmdUpdate.Click += new System.EventHandler(this.cmdUpdate_Click);
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
            this.cmdCancel.TabIndex = 126;
            this.cmdCancel.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.cmdCancel.UseVisualStyleBackColor = false;
            this.cmdCancel.Click += new System.EventHandler(this.cmdCancel_Click);
            // 
            // txtClaveATV
            // 
            this.txtClaveATV.AcceptsReturn = true;
            this.txtClaveATV.BackColor = System.Drawing.SystemColors.Window;
            this.txtClaveATV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtClaveATV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtClaveATV.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtClaveATV.Location = new System.Drawing.Point(139, 478);
            this.txtClaveATV.MaxLength = 200;
            this.txtClaveATV.Name = "txtClaveATV";
            this.txtClaveATV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtClaveATV.Size = new System.Drawing.Size(199, 20);
            this.txtClaveATV.TabIndex = 199;
            // 
            // Label25
            // 
            this.Label25.BackColor = System.Drawing.Color.Transparent;
            this.Label25.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label25.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label25.Location = new System.Drawing.Point(36, 481);
            this.Label25.Name = "Label25";
            this.Label25.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label25.Size = new System.Drawing.Size(97, 17);
            this.Label25.TabIndex = 200;
            this.Label25.Text = "Contraseña ATV:";
            this.Label25.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUsuarioATV
            // 
            this.txtUsuarioATV.AcceptsReturn = true;
            this.txtUsuarioATV.BackColor = System.Drawing.SystemColors.Window;
            this.txtUsuarioATV.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUsuarioATV.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUsuarioATV.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUsuarioATV.Location = new System.Drawing.Point(139, 452);
            this.txtUsuarioATV.MaxLength = 200;
            this.txtUsuarioATV.Name = "txtUsuarioATV";
            this.txtUsuarioATV.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUsuarioATV.Size = new System.Drawing.Size(313, 20);
            this.txtUsuarioATV.TabIndex = 197;
            // 
            // Label24
            // 
            this.Label24.BackColor = System.Drawing.Color.Transparent;
            this.Label24.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label24.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label24.Location = new System.Drawing.Point(55, 455);
            this.Label24.Name = "Label24";
            this.Label24.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label24.Size = new System.Drawing.Size(78, 17);
            this.Label24.TabIndex = 198;
            this.Label24.Text = "Usuario ATV:";
            this.Label24.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // btnCargarLogo
            // 
            this.btnCargarLogo.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCargarLogo.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCargarLogo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnCargarLogo.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCargarLogo.Location = new System.Drawing.Point(482, 425);
            this.btnCargarLogo.Name = "btnCargarLogo";
            this.btnCargarLogo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCargarLogo.Size = new System.Drawing.Size(78, 26);
            this.btnCargarLogo.TabIndex = 196;
            this.btnCargarLogo.TabStop = false;
            this.btnCargarLogo.Text = "&Cargar logo";
            this.btnCargarLogo.UseVisualStyleBackColor = false;
            // 
            // picLogo
            // 
            this.picLogo.BackColor = System.Drawing.Color.White;
            this.picLogo.BorderStyle = System.Windows.Forms.BorderStyle.Fixed3D;
            this.picLogo.Location = new System.Drawing.Point(482, 259);
            this.picLogo.Name = "picLogo";
            this.picLogo.Size = new System.Drawing.Size(350, 160);
            this.picLogo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picLogo.TabIndex = 195;
            this.picLogo.TabStop = false;
            // 
            // Label23
            // 
            this.Label23.BackColor = System.Drawing.Color.Transparent;
            this.Label23.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label23.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label23.Location = new System.Drawing.Point(69, 635);
            this.Label23.Name = "Label23";
            this.Label23.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label23.Size = new System.Drawing.Size(64, 17);
            this.Label23.TabIndex = 194;
            this.Label23.Text = "Ultimo MR:";
            this.Label23.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label22
            // 
            this.Label22.BackColor = System.Drawing.Color.Transparent;
            this.Label22.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label22.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label22.Location = new System.Drawing.Point(224, 608);
            this.Label22.Name = "Label22";
            this.Label22.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label22.Size = new System.Drawing.Size(64, 17);
            this.Label22.TabIndex = 193;
            this.Label22.Text = "Ultimo TE:";
            this.Label22.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label21
            // 
            this.Label21.BackColor = System.Drawing.Color.Transparent;
            this.Label21.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label21.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label21.Location = new System.Drawing.Point(224, 580);
            this.Label21.Name = "Label21";
            this.Label21.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label21.Size = new System.Drawing.Size(64, 17);
            this.Label21.TabIndex = 192;
            this.Label21.Text = "Ultimo NC:";
            this.Label21.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // Label20
            // 
            this.Label20.BackColor = System.Drawing.Color.Transparent;
            this.Label20.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label20.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label20.Location = new System.Drawing.Point(69, 609);
            this.Label20.Name = "Label20";
            this.Label20.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label20.Size = new System.Drawing.Size(64, 17);
            this.Label20.TabIndex = 191;
            this.Label20.Text = "Ultimo ND:";
            this.Label20.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtUltimoDocMR
            // 
            this.txtUltimoDocMR.AcceptsReturn = true;
            this.txtUltimoDocMR.BackColor = System.Drawing.SystemColors.Window;
            this.txtUltimoDocMR.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUltimoDocMR.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUltimoDocMR.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUltimoDocMR.Location = new System.Drawing.Point(139, 634);
            this.txtUltimoDocMR.MaxLength = 50;
            this.txtUltimoDocMR.Name = "txtUltimoDocMR";
            this.txtUltimoDocMR.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUltimoDocMR.Size = new System.Drawing.Size(44, 20);
            this.txtUltimoDocMR.TabIndex = 150;
            // 
            // txtUltimoDocTE
            // 
            this.txtUltimoDocTE.AcceptsReturn = true;
            this.txtUltimoDocTE.BackColor = System.Drawing.SystemColors.Window;
            this.txtUltimoDocTE.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUltimoDocTE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUltimoDocTE.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUltimoDocTE.Location = new System.Drawing.Point(294, 605);
            this.txtUltimoDocTE.MaxLength = 50;
            this.txtUltimoDocTE.Name = "txtUltimoDocTE";
            this.txtUltimoDocTE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUltimoDocTE.Size = new System.Drawing.Size(44, 20);
            this.txtUltimoDocTE.TabIndex = 148;
            // 
            // txtUltimoDocNC
            // 
            this.txtUltimoDocNC.AcceptsReturn = true;
            this.txtUltimoDocNC.BackColor = System.Drawing.SystemColors.Window;
            this.txtUltimoDocNC.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUltimoDocNC.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUltimoDocNC.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUltimoDocNC.Location = new System.Drawing.Point(294, 579);
            this.txtUltimoDocNC.MaxLength = 50;
            this.txtUltimoDocNC.Name = "txtUltimoDocNC";
            this.txtUltimoDocNC.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUltimoDocNC.Size = new System.Drawing.Size(44, 20);
            this.txtUltimoDocNC.TabIndex = 147;
            // 
            // txtUltimoDocND
            // 
            this.txtUltimoDocND.AcceptsReturn = true;
            this.txtUltimoDocND.BackColor = System.Drawing.SystemColors.Window;
            this.txtUltimoDocND.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUltimoDocND.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUltimoDocND.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUltimoDocND.Location = new System.Drawing.Point(139, 608);
            this.txtUltimoDocND.MaxLength = 50;
            this.txtUltimoDocND.Name = "txtUltimoDocND";
            this.txtUltimoDocND.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUltimoDocND.Size = new System.Drawing.Size(44, 20);
            this.txtUltimoDocND.TabIndex = 145;
            // 
            // txtUltimoDocFE
            // 
            this.txtUltimoDocFE.AcceptsReturn = true;
            this.txtUltimoDocFE.BackColor = System.Drawing.SystemColors.Window;
            this.txtUltimoDocFE.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtUltimoDocFE.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtUltimoDocFE.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtUltimoDocFE.Location = new System.Drawing.Point(139, 582);
            this.txtUltimoDocFE.MaxLength = 50;
            this.txtUltimoDocFE.Name = "txtUltimoDocFE";
            this.txtUltimoDocFE.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtUltimoDocFE.Size = new System.Drawing.Size(44, 20);
            this.txtUltimoDocFE.TabIndex = 144;
            // 
            // Label19
            // 
            this.Label19.BackColor = System.Drawing.Color.Transparent;
            this.Label19.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label19.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label19.Location = new System.Drawing.Point(69, 585);
            this.Label19.Name = "Label19";
            this.Label19.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label19.Size = new System.Drawing.Size(64, 17);
            this.Label19.TabIndex = 190;
            this.Label19.Text = "Ultimo FE:";
            this.Label19.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPinCertificado
            // 
            this.txtPinCertificado.AcceptsReturn = true;
            this.txtPinCertificado.BackColor = System.Drawing.SystemColors.Window;
            this.txtPinCertificado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPinCertificado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPinCertificado.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPinCertificado.Location = new System.Drawing.Point(139, 426);
            this.txtPinCertificado.MaxLength = 50;
            this.txtPinCertificado.Name = "txtPinCertificado";
            this.txtPinCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPinCertificado.Size = new System.Drawing.Size(44, 20);
            this.txtPinCertificado.TabIndex = 143;
            // 
            // Label18
            // 
            this.Label18.BackColor = System.Drawing.Color.Transparent;
            this.Label18.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label18.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label18.Location = new System.Drawing.Point(14, 429);
            this.Label18.Name = "Label18";
            this.Label18.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label18.Size = new System.Drawing.Size(119, 17);
            this.Label18.TabIndex = 189;
            this.Label18.Text = "Pin certificado:";
            this.Label18.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtIdCertificado
            // 
            this.txtIdCertificado.AcceptsReturn = true;
            this.txtIdCertificado.BackColor = System.Drawing.SystemColors.Window;
            this.txtIdCertificado.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdCertificado.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdCertificado.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIdCertificado.Location = new System.Drawing.Point(139, 400);
            this.txtIdCertificado.MaxLength = 200;
            this.txtIdCertificado.Name = "txtIdCertificado";
            this.txtIdCertificado.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdCertificado.Size = new System.Drawing.Size(313, 20);
            this.txtIdCertificado.TabIndex = 142;
            // 
            // Label17
            // 
            this.Label17.BackColor = System.Drawing.Color.Transparent;
            this.Label17.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label17.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label17.Location = new System.Drawing.Point(55, 403);
            this.Label17.Name = "Label17";
            this.Label17.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label17.Size = new System.Drawing.Size(78, 17);
            this.Label17.TabIndex = 188;
            this.Label17.Text = "Id certificado:";
            this.Label17.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtServicioFacturaElectronica
            // 
            this.txtServicioFacturaElectronica.AcceptsReturn = true;
            this.txtServicioFacturaElectronica.BackColor = System.Drawing.SystemColors.Window;
            this.txtServicioFacturaElectronica.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtServicioFacturaElectronica.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtServicioFacturaElectronica.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtServicioFacturaElectronica.Location = new System.Drawing.Point(139, 354);
            this.txtServicioFacturaElectronica.MaxLength = 200;
            this.txtServicioFacturaElectronica.Multiline = true;
            this.txtServicioFacturaElectronica.Name = "txtServicioFacturaElectronica";
            this.txtServicioFacturaElectronica.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtServicioFacturaElectronica.Size = new System.Drawing.Size(313, 40);
            this.txtServicioFacturaElectronica.TabIndex = 141;
            // 
            // Label16
            // 
            this.Label16.BackColor = System.Drawing.Color.Transparent;
            this.Label16.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label16.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label16.Location = new System.Drawing.Point(14, 357);
            this.Label16.Name = "Label16";
            this.Label16.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label16.Size = new System.Drawing.Size(119, 17);
            this.Label16.TabIndex = 187;
            this.Label16.Text = "URL fact. electrónica:";
            this.Label16.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboBarrio
            // 
            this.cboBarrio.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboBarrio.FormattingEnabled = true;
            this.cboBarrio.Location = new System.Drawing.Point(139, 249);
            this.cboBarrio.Name = "cboBarrio";
            this.cboBarrio.Size = new System.Drawing.Size(147, 21);
            this.cboBarrio.TabIndex = 137;
            // 
            // Label15
            // 
            this.Label15.BackColor = System.Drawing.Color.Transparent;
            this.Label15.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label15.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label15.Location = new System.Drawing.Point(14, 252);
            this.Label15.Name = "Label15";
            this.Label15.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label15.Size = new System.Drawing.Size(119, 17);
            this.Label15.TabIndex = 186;
            this.Label15.Text = "Barrio:";
            this.Label15.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboDistrito
            // 
            this.cboDistrito.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboDistrito.FormattingEnabled = true;
            this.cboDistrito.Location = new System.Drawing.Point(139, 222);
            this.cboDistrito.Name = "cboDistrito";
            this.cboDistrito.Size = new System.Drawing.Size(147, 21);
            this.cboDistrito.TabIndex = 136;
            this.cboDistrito.SelectedIndexChanged += new System.EventHandler(this.cboDistrito_SelectedIndexChanged);
            // 
            // Label14
            // 
            this.Label14.BackColor = System.Drawing.Color.Transparent;
            this.Label14.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label14.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label14.Location = new System.Drawing.Point(14, 225);
            this.Label14.Name = "Label14";
            this.Label14.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label14.Size = new System.Drawing.Size(119, 17);
            this.Label14.TabIndex = 185;
            this.Label14.Text = "Distrito:";
            this.Label14.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboCanton
            // 
            this.cboCanton.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboCanton.FormattingEnabled = true;
            this.cboCanton.Location = new System.Drawing.Point(139, 194);
            this.cboCanton.Name = "cboCanton";
            this.cboCanton.Size = new System.Drawing.Size(147, 21);
            this.cboCanton.TabIndex = 135;
            this.cboCanton.SelectedIndexChanged += new System.EventHandler(this.cboCanton_SelectedIndexChanged);
            // 
            // Label13
            // 
            this.Label13.BackColor = System.Drawing.Color.Transparent;
            this.Label13.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label13.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label13.Location = new System.Drawing.Point(14, 197);
            this.Label13.Name = "Label13";
            this.Label13.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label13.Size = new System.Drawing.Size(119, 17);
            this.Label13.TabIndex = 184;
            this.Label13.Text = "Cantón:";
            this.Label13.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // cboProvincia
            // 
            this.cboProvincia.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboProvincia.FormattingEnabled = true;
            this.cboProvincia.Location = new System.Drawing.Point(139, 167);
            this.cboProvincia.Name = "cboProvincia";
            this.cboProvincia.Size = new System.Drawing.Size(147, 21);
            this.cboProvincia.TabIndex = 134;
            this.cboProvincia.SelectedIndexChanged += new System.EventHandler(this.cboProvincia_SelectedIndexChanged);
            // 
            // cboTipoIdentificacion
            // 
            this.cboTipoIdentificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboTipoIdentificacion.FormattingEnabled = true;
            this.cboTipoIdentificacion.Location = new System.Drawing.Point(139, 114);
            this.cboTipoIdentificacion.Name = "cboTipoIdentificacion";
            this.cboTipoIdentificacion.Size = new System.Drawing.Size(171, 21);
            this.cboTipoIdentificacion.TabIndex = 131;
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
            this.chkFacturaElectronica.Location = new System.Drawing.Point(689, 541);
            this.chkFacturaElectronica.Name = "chkFacturaElectronica";
            this.chkFacturaElectronica.Size = new System.Drawing.Size(152, 17);
            this.chkFacturaElectronica.TabIndex = 172;
            this.chkFacturaElectronica.TabStop = false;
            this.chkFacturaElectronica.Text = "Habilita factura electrónica";
            this.chkFacturaElectronica.UseVisualStyleBackColor = true;
            // 
            // txtCorreoNotificacion
            // 
            this.txtCorreoNotificacion.AcceptsReturn = true;
            this.txtCorreoNotificacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtCorreoNotificacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCorreoNotificacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCorreoNotificacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCorreoNotificacion.Location = new System.Drawing.Point(139, 328);
            this.txtCorreoNotificacion.MaxLength = 50;
            this.txtCorreoNotificacion.Name = "txtCorreoNotificacion";
            this.txtCorreoNotificacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCorreoNotificacion.Size = new System.Drawing.Size(313, 20);
            this.txtCorreoNotificacion.TabIndex = 140;
            // 
            // Label11
            // 
            this.Label11.BackColor = System.Drawing.Color.Transparent;
            this.Label11.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label11.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label11.Location = new System.Drawing.Point(14, 331);
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
            this.txtNombreEmpresa.MaxLength = 50;
            this.txtNombreEmpresa.Name = "txtNombreEmpresa";
            this.txtNombreEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNombreEmpresa.Size = new System.Drawing.Size(313, 20);
            this.txtNombreEmpresa.TabIndex = 129;
            // 
            // chkCierrePorTurnos
            // 
            this.chkCierrePorTurnos.AutoSize = true;
            this.chkCierrePorTurnos.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkCierrePorTurnos.Location = new System.Drawing.Point(689, 515);
            this.chkCierrePorTurnos.Name = "chkCierrePorTurnos";
            this.chkCierrePorTurnos.Size = new System.Drawing.Size(116, 17);
            this.chkCierrePorTurnos.TabIndex = 169;
            this.chkCierrePorTurnos.TabStop = false;
            this.chkCierrePorTurnos.Text = "Cierre por períodos";
            this.chkCierrePorTurnos.UseVisualStyleBackColor = true;
            // 
            // chkRespaldoEnLinea
            // 
            this.chkRespaldoEnLinea.AutoSize = true;
            this.chkRespaldoEnLinea.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkRespaldoEnLinea.Location = new System.Drawing.Point(689, 489);
            this.chkRespaldoEnLinea.Name = "chkRespaldoEnLinea";
            this.chkRespaldoEnLinea.Size = new System.Drawing.Size(113, 17);
            this.chkRespaldoEnLinea.TabIndex = 167;
            this.chkRespaldoEnLinea.TabStop = false;
            this.chkRespaldoEnLinea.Text = "Respaldo en línea";
            this.chkRespaldoEnLinea.UseVisualStyleBackColor = true;
            // 
            // chkIncluyeInsumosEnFactura
            // 
            this.chkIncluyeInsumosEnFactura.AutoSize = true;
            this.chkIncluyeInsumosEnFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkIncluyeInsumosEnFactura.Location = new System.Drawing.Point(689, 464);
            this.chkIncluyeInsumosEnFactura.Name = "chkIncluyeInsumosEnFactura";
            this.chkIncluyeInsumosEnFactura.Size = new System.Drawing.Size(152, 17);
            this.chkIncluyeInsumosEnFactura.TabIndex = 165;
            this.chkIncluyeInsumosEnFactura.TabStop = false;
            this.chkIncluyeInsumosEnFactura.Text = "Incluye insumos en factura";
            this.chkIncluyeInsumosEnFactura.UseVisualStyleBackColor = true;
            // 
            // chkUsaImpresoraImpacto
            // 
            this.chkUsaImpresoraImpacto.AutoSize = true;
            this.chkUsaImpresoraImpacto.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkUsaImpresoraImpacto.Location = new System.Drawing.Point(482, 92);
            this.chkUsaImpresoraImpacto.Name = "chkUsaImpresoraImpacto";
            this.chkUsaImpresoraImpacto.Size = new System.Drawing.Size(144, 17);
            this.chkUsaImpresoraImpacto.TabIndex = 160;
            this.chkUsaImpresoraImpacto.TabStop = false;
            this.chkUsaImpresoraImpacto.Text = "Utiliza Impresora Impacto";
            this.chkUsaImpresoraImpacto.UseVisualStyleBackColor = true;
            // 
            // txtCodigoServInst
            // 
            this.txtCodigoServInst.AcceptsReturn = true;
            this.txtCodigoServInst.BackColor = System.Drawing.SystemColors.Window;
            this.txtCodigoServInst.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtCodigoServInst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtCodigoServInst.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtCodigoServInst.Location = new System.Drawing.Point(294, 530);
            this.txtCodigoServInst.MaxLength = 50;
            this.txtCodigoServInst.Name = "txtCodigoServInst";
            this.txtCodigoServInst.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtCodigoServInst.Size = new System.Drawing.Size(44, 20);
            this.txtCodigoServInst.TabIndex = 154;
            this.txtCodigoServInst.TabStop = false;
            // 
            // Label10
            // 
            this.Label10.BackColor = System.Drawing.Color.Transparent;
            this.Label10.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label10.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label10.Location = new System.Drawing.Point(190, 532);
            this.Label10.Name = "Label10";
            this.Label10.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label10.Size = new System.Drawing.Size(98, 17);
            this.Label10.TabIndex = 181;
            this.Label10.Text = "Cod Serv. Inst.:";
            this.Label10.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPorcentajeInstalacion
            // 
            this.txtPorcentajeInstalacion.AcceptsReturn = true;
            this.txtPorcentajeInstalacion.BackColor = System.Drawing.SystemColors.Window;
            this.txtPorcentajeInstalacion.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPorcentajeInstalacion.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeInstalacion.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPorcentajeInstalacion.Location = new System.Drawing.Point(294, 504);
            this.txtPorcentajeInstalacion.MaxLength = 50;
            this.txtPorcentajeInstalacion.Name = "txtPorcentajeInstalacion";
            this.txtPorcentajeInstalacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPorcentajeInstalacion.Size = new System.Drawing.Size(44, 20);
            this.txtPorcentajeInstalacion.TabIndex = 153;
            this.txtPorcentajeInstalacion.TabStop = false;
            // 
            // Label9
            // 
            this.Label9.BackColor = System.Drawing.Color.Transparent;
            this.Label9.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label9.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label9.Location = new System.Drawing.Point(238, 507);
            this.Label9.Name = "Label9";
            this.Label9.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label9.Size = new System.Drawing.Size(50, 17);
            this.Label9.TabIndex = 180;
            this.Label9.Text = "% Inst:";
            this.Label9.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // chkDesgloseInst
            // 
            this.chkDesgloseInst.AutoSize = true;
            this.chkDesgloseInst.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkDesgloseInst.Location = new System.Drawing.Point(483, 542);
            this.chkDesgloseInst.Name = "chkDesgloseInst";
            this.chkDesgloseInst.Size = new System.Drawing.Size(177, 17);
            this.chkDesgloseInst.TabIndex = 170;
            this.chkDesgloseInst.TabStop = false;
            this.chkDesgloseInst.Text = "Desglosa servicio de instalación";
            this.chkDesgloseInst.UseVisualStyleBackColor = true;
            // 
            // chkModificaDesc
            // 
            this.chkModificaDesc.AutoSize = true;
            this.chkModificaDesc.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.chkModificaDesc.Location = new System.Drawing.Point(483, 516);
            this.chkModificaDesc.Name = "chkModificaDesc";
            this.chkModificaDesc.Size = new System.Drawing.Size(196, 17);
            this.chkModificaDesc.TabIndex = 168;
            this.chkModificaDesc.TabStop = false;
            this.chkModificaDesc.Text = "Modifica la descripción del producto";
            this.chkModificaDesc.UseVisualStyleBackColor = true;
            // 
            // ckbAutoCompleta
            // 
            this.ckbAutoCompleta.AutoSize = true;
            this.ckbAutoCompleta.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbAutoCompleta.Location = new System.Drawing.Point(483, 490);
            this.ckbAutoCompleta.Name = "ckbAutoCompleta";
            this.ckbAutoCompleta.Size = new System.Drawing.Size(189, 17);
            this.ckbAutoCompleta.TabIndex = 166;
            this.ckbAutoCompleta.TabStop = false;
            this.ckbAutoCompleta.Text = "Auto Completar Lista de Productos";
            this.ckbAutoCompleta.UseVisualStyleBackColor = true;
            // 
            // ckbContabiliza
            // 
            this.ckbContabiliza.AutoSize = true;
            this.ckbContabiliza.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ckbContabiliza.Location = new System.Drawing.Point(483, 464);
            this.ckbContabiliza.Name = "ckbContabiliza";
            this.ckbContabiliza.Size = new System.Drawing.Size(77, 17);
            this.ckbContabiliza.TabIndex = 164;
            this.ckbContabiliza.TabStop = false;
            this.ckbContabiliza.Text = "Contabiliza";
            this.ckbContabiliza.UseVisualStyleBackColor = true;
            // 
            // txtLineasFactura
            // 
            this.txtLineasFactura.AcceptsReturn = true;
            this.txtLineasFactura.BackColor = System.Drawing.SystemColors.Window;
            this.txtLineasFactura.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtLineasFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtLineasFactura.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtLineasFactura.Location = new System.Drawing.Point(139, 530);
            this.txtLineasFactura.MaxLength = 50;
            this.txtLineasFactura.Name = "txtLineasFactura";
            this.txtLineasFactura.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtLineasFactura.Size = new System.Drawing.Size(42, 20);
            this.txtLineasFactura.TabIndex = 155;
            this.txtLineasFactura.TabStop = false;
            // 
            // Label8
            // 
            this.Label8.BackColor = System.Drawing.Color.Transparent;
            this.Label8.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label8.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label8.Location = new System.Drawing.Point(34, 533);
            this.Label8.Name = "Label8";
            this.Label8.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label8.Size = new System.Drawing.Size(100, 17);
            this.Label8.TabIndex = 179;
            this.Label8.Text = "Líneas por Fact:";
            this.Label8.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtPorcentajeIVA
            // 
            this.txtPorcentajeIVA.AcceptsReturn = true;
            this.txtPorcentajeIVA.BackColor = System.Drawing.SystemColors.Window;
            this.txtPorcentajeIVA.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtPorcentajeIVA.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtPorcentajeIVA.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtPorcentajeIVA.Location = new System.Drawing.Point(139, 504);
            this.txtPorcentajeIVA.MaxLength = 50;
            this.txtPorcentajeIVA.Name = "txtPorcentajeIVA";
            this.txtPorcentajeIVA.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtPorcentajeIVA.Size = new System.Drawing.Size(44, 20);
            this.txtPorcentajeIVA.TabIndex = 152;
            // 
            // Label7
            // 
            this.Label7.BackColor = System.Drawing.Color.Transparent;
            this.Label7.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label7.Location = new System.Drawing.Point(82, 507);
            this.Label7.Name = "Label7";
            this.Label7.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label7.Size = new System.Drawing.Size(51, 17);
            this.Label7.TabIndex = 178;
            this.Label7.Text = "% IVA:";
            this.Label7.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // txtImpresoraFactura
            // 
            this.txtImpresoraFactura.AcceptsReturn = true;
            this.txtImpresoraFactura.BackColor = System.Drawing.SystemColors.Window;
            this.txtImpresoraFactura.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtImpresoraFactura.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtImpresoraFactura.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtImpresoraFactura.Location = new System.Drawing.Point(635, 66);
            this.txtImpresoraFactura.MaxLength = 50;
            this.txtImpresoraFactura.Name = "txtImpresoraFactura";
            this.txtImpresoraFactura.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtImpresoraFactura.Size = new System.Drawing.Size(197, 20);
            this.txtImpresoraFactura.TabIndex = 159;
            this.txtImpresoraFactura.TabStop = false;
            // 
            // Label6
            // 
            this.Label6.BackColor = System.Drawing.Color.Transparent;
            this.Label6.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label6.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label6.Location = new System.Drawing.Point(635, 46);
            this.Label6.Name = "Label6";
            this.Label6.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.Label6.Size = new System.Drawing.Size(199, 17);
            this.Label6.TabIndex = 177;
            this.Label6.Text = "Impresora Fact:";
            this.Label6.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // txtTelefono
            // 
            this.txtTelefono.AcceptsReturn = true;
            this.txtTelefono.BackColor = System.Drawing.SystemColors.Window;
            this.txtTelefono.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtTelefono.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtTelefono.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtTelefono.Location = new System.Drawing.Point(139, 302);
            this.txtTelefono.MaxLength = 50;
            this.txtTelefono.Name = "txtTelefono";
            this.txtTelefono.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtTelefono.Size = new System.Drawing.Size(125, 20);
            this.txtTelefono.TabIndex = 139;
            // 
            // Label5
            // 
            this.Label5.BackColor = System.Drawing.Color.Transparent;
            this.Label5.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label5.Location = new System.Drawing.Point(14, 305);
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
            this.txtDireccion.Location = new System.Drawing.Point(139, 276);
            this.txtDireccion.MaxLength = 50;
            this.txtDireccion.Name = "txtDireccion";
            this.txtDireccion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtDireccion.Size = new System.Drawing.Size(313, 20);
            this.txtDireccion.TabIndex = 138;
            // 
            // Label4
            // 
            this.Label4.BackColor = System.Drawing.Color.Transparent;
            this.Label4.Cursor = System.Windows.Forms.Cursors.Default;
            this.Label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.Label4.Location = new System.Drawing.Point(14, 279);
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
            this.Label3.Location = new System.Drawing.Point(14, 170);
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
            this.txtIdentificacion.MaxLength = 50;
            this.txtIdentificacion.Name = "txtIdentificacion";
            this.txtIdentificacion.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdentificacion.Size = new System.Drawing.Size(146, 20);
            this.txtIdentificacion.TabIndex = 132;
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
            this.txtNombreComercial.MaxLength = 50;
            this.txtNombreComercial.Name = "txtNombreComercial";
            this.txtNombreComercial.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtNombreComercial.Size = new System.Drawing.Size(313, 20);
            this.txtNombreComercial.TabIndex = 130;
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
            // btnEliminarDetalle
            // 
            this.btnEliminarDetalle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnEliminarDetalle.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnEliminarDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnEliminarDetalle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnEliminarDetalle.Location = new System.Drawing.Point(548, 227);
            this.btnEliminarDetalle.Name = "btnEliminarDetalle";
            this.btnEliminarDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnEliminarDetalle.Size = new System.Drawing.Size(65, 26);
            this.btnEliminarDetalle.TabIndex = 163;
            this.btnEliminarDetalle.TabStop = false;
            this.btnEliminarDetalle.Text = "&Eliminar";
            this.btnEliminarDetalle.UseVisualStyleBackColor = false;
            // 
            // btnInsertarDetalle
            // 
            this.btnInsertarDetalle.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnInsertarDetalle.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnInsertarDetalle.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnInsertarDetalle.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnInsertarDetalle.Location = new System.Drawing.Point(482, 227);
            this.btnInsertarDetalle.Name = "btnInsertarDetalle";
            this.btnInsertarDetalle.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnInsertarDetalle.Size = new System.Drawing.Size(65, 26);
            this.btnInsertarDetalle.TabIndex = 162;
            this.btnInsertarDetalle.TabStop = false;
            this.btnInsertarDetalle.Text = "&Insertar";
            this.btnInsertarDetalle.UseVisualStyleBackColor = false;
            // 
            // dgvEquipos
            // 
            this.dgvEquipos.AllowUserToAddRows = false;
            this.dgvEquipos.AllowUserToDeleteRows = false;
            this.dgvEquipos.AllowUserToResizeColumns = false;
            this.dgvEquipos.AllowUserToResizeRows = false;
            this.dgvEquipos.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvEquipos.Location = new System.Drawing.Point(483, 119);
            this.dgvEquipos.Name = "dgvEquipos";
            this.dgvEquipos.RowHeadersVisible = false;
            this.dgvEquipos.Size = new System.Drawing.Size(349, 102);
            this.dgvEquipos.TabIndex = 161;
            this.dgvEquipos.TabStop = false;
            // 
            // CmdConsultar
            // 
            this.CmdConsultar.BackColor = System.Drawing.SystemColors.Control;
            this.CmdConsultar.Cursor = System.Windows.Forms.Cursors.Default;
            this.CmdConsultar.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CmdConsultar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.CmdConsultar.Location = new System.Drawing.Point(614, 66);
            this.CmdConsultar.Name = "CmdConsultar";
            this.CmdConsultar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.CmdConsultar.Size = new System.Drawing.Size(20, 20);
            this.CmdConsultar.TabIndex = 158;
            this.CmdConsultar.TabStop = false;
            this.CmdConsultar.UseVisualStyleBackColor = false;
            // 
            // txtFecha
            // 
            this.txtFecha.AcceptsReturn = true;
            this.txtFecha.BackColor = System.Drawing.SystemColors.Window;
            this.txtFecha.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtFecha.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtFecha.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtFecha.Location = new System.Drawing.Point(139, 556);
            this.txtFecha.MaxLength = 0;
            this.txtFecha.Name = "txtFecha";
            this.txtFecha.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtFecha.Size = new System.Drawing.Size(85, 20);
            this.txtFecha.TabIndex = 156;
            this.txtFecha.TabStop = false;
            // 
            // txtEquipo
            // 
            this.txtEquipo.AcceptsReturn = true;
            this.txtEquipo.BackColor = System.Drawing.SystemColors.Window;
            this.txtEquipo.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtEquipo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtEquipo.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtEquipo.Location = new System.Drawing.Point(483, 66);
            this.txtEquipo.MaxLength = 0;
            this.txtEquipo.Name = "txtEquipo";
            this.txtEquipo.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtEquipo.Size = new System.Drawing.Size(131, 20);
            this.txtEquipo.TabIndex = 157;
            this.txtEquipo.TabStop = false;
            // 
            // txtIdEmpresa
            // 
            this.txtIdEmpresa.AcceptsReturn = true;
            this.txtIdEmpresa.BackColor = System.Drawing.SystemColors.Window;
            this.txtIdEmpresa.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txtIdEmpresa.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtIdEmpresa.ForeColor = System.Drawing.SystemColors.WindowText;
            this.txtIdEmpresa.Location = new System.Drawing.Point(139, 36);
            this.txtIdEmpresa.MaxLength = 0;
            this.txtIdEmpresa.Name = "txtIdEmpresa";
            this.txtIdEmpresa.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.txtIdEmpresa.Size = new System.Drawing.Size(49, 20);
            this.txtIdEmpresa.TabIndex = 128;
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
            this._lblLabels_1.Location = new System.Drawing.Point(20, 559);
            this._lblLabels_1.Name = "_lblLabels_1";
            this._lblLabels_1.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblLabels_1.Size = new System.Drawing.Size(113, 17);
            this._lblLabels_1.TabIndex = 149;
            this._lblLabels_1.Text = "Fecha vencimiento:";
            this._lblLabels_1.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // _lblLabels_2
            // 
            this._lblLabels_2.BackColor = System.Drawing.Color.Transparent;
            this._lblLabels_2.Cursor = System.Windows.Forms.Cursors.Default;
            this._lblLabels_2.Font = new System.Drawing.Font("Microsoft Sans Serif", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this._lblLabels_2.ForeColor = System.Drawing.SystemColors.ControlText;
            this._lblLabels_2.Location = new System.Drawing.Point(483, 46);
            this._lblLabels_2.Name = "_lblLabels_2";
            this._lblLabels_2.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this._lblLabels_2.Size = new System.Drawing.Size(151, 17);
            this._lblLabels_2.TabIndex = 146;
            this._lblLabels_2.Text = "Equipo";
            this._lblLabels_2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
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
            // FrmEmpresa
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(844, 658);
            this.Controls.Add(this.txtClaveATV);
            this.Controls.Add(this.Label25);
            this.Controls.Add(this.txtUsuarioATV);
            this.Controls.Add(this.Label24);
            this.Controls.Add(this.btnCargarLogo);
            this.Controls.Add(this.picLogo);
            this.Controls.Add(this.Label23);
            this.Controls.Add(this.Label22);
            this.Controls.Add(this.Label21);
            this.Controls.Add(this.Label20);
            this.Controls.Add(this.txtUltimoDocMR);
            this.Controls.Add(this.txtUltimoDocTE);
            this.Controls.Add(this.txtUltimoDocNC);
            this.Controls.Add(this.txtUltimoDocND);
            this.Controls.Add(this.txtUltimoDocFE);
            this.Controls.Add(this.Label19);
            this.Controls.Add(this.txtPinCertificado);
            this.Controls.Add(this.Label18);
            this.Controls.Add(this.txtIdCertificado);
            this.Controls.Add(this.Label17);
            this.Controls.Add(this.txtServicioFacturaElectronica);
            this.Controls.Add(this.Label16);
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
            this.Controls.Add(this.chkCierrePorTurnos);
            this.Controls.Add(this.chkRespaldoEnLinea);
            this.Controls.Add(this.chkIncluyeInsumosEnFactura);
            this.Controls.Add(this.chkUsaImpresoraImpacto);
            this.Controls.Add(this.txtCodigoServInst);
            this.Controls.Add(this.Label10);
            this.Controls.Add(this.txtPorcentajeInstalacion);
            this.Controls.Add(this.Label9);
            this.Controls.Add(this.chkDesgloseInst);
            this.Controls.Add(this.chkModificaDesc);
            this.Controls.Add(this.ckbAutoCompleta);
            this.Controls.Add(this.ckbContabiliza);
            this.Controls.Add(this.txtLineasFactura);
            this.Controls.Add(this.Label8);
            this.Controls.Add(this.txtPorcentajeIVA);
            this.Controls.Add(this.Label7);
            this.Controls.Add(this.txtImpresoraFactura);
            this.Controls.Add(this.Label6);
            this.Controls.Add(this.txtTelefono);
            this.Controls.Add(this.Label5);
            this.Controls.Add(this.txtDireccion);
            this.Controls.Add(this.Label4);
            this.Controls.Add(this.Label3);
            this.Controls.Add(this.txtIdentificacion);
            this.Controls.Add(this.Label2);
            this.Controls.Add(this.txtNombreComercial);
            this.Controls.Add(this.Label1);
            this.Controls.Add(this.btnEliminarDetalle);
            this.Controls.Add(this.btnInsertarDetalle);
            this.Controls.Add(this.dgvEquipos);
            this.Controls.Add(this.CmdConsultar);
            this.Controls.Add(this.txtFecha);
            this.Controls.Add(this.txtEquipo);
            this.Controls.Add(this.txtIdEmpresa);
            this.Controls.Add(this._lblLabels_3);
            this.Controls.Add(this._lblLabels_1);
            this.Controls.Add(this._lblLabels_2);
            this.Controls.Add(this._lblLabels_0);
            this.Controls.Add(this.cmdUpdate);
            this.Controls.Add(this.cmdCancel);
            this.Name = "FrmEmpresa";
            this.Text = "Registrar/Actualizar datos de la empresa seleccionada";
            this.Load += new System.EventHandler(this.FrmEmpresa_Load);
            ((System.ComponentModel.ISupportInitialize)(this.picLogo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.dgvEquipos)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        public System.Windows.Forms.Button cmdUpdate;
        public System.Windows.Forms.Button cmdCancel;
        private System.Windows.Forms.OpenFileDialog ofdAbrirDocumento;
        internal System.Windows.Forms.OpenFileDialog openFileDialog1;
        public System.Windows.Forms.TextBox txtClaveATV;
        public System.Windows.Forms.Label Label25;
        public System.Windows.Forms.TextBox txtUsuarioATV;
        public System.Windows.Forms.Label Label24;
        public System.Windows.Forms.Button btnCargarLogo;
        internal System.Windows.Forms.PictureBox picLogo;
        public System.Windows.Forms.Label Label23;
        public System.Windows.Forms.Label Label22;
        public System.Windows.Forms.Label Label21;
        public System.Windows.Forms.Label Label20;
        public System.Windows.Forms.TextBox txtUltimoDocMR;
        public System.Windows.Forms.TextBox txtUltimoDocTE;
        public System.Windows.Forms.TextBox txtUltimoDocNC;
        public System.Windows.Forms.TextBox txtUltimoDocND;
        public System.Windows.Forms.TextBox txtUltimoDocFE;
        public System.Windows.Forms.Label Label19;
        public System.Windows.Forms.TextBox txtPinCertificado;
        public System.Windows.Forms.Label Label18;
        public System.Windows.Forms.TextBox txtIdCertificado;
        public System.Windows.Forms.Label Label17;
        public System.Windows.Forms.TextBox txtServicioFacturaElectronica;
        public System.Windows.Forms.Label Label16;
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
        internal System.Windows.Forms.CheckBox chkCierrePorTurnos;
        internal System.Windows.Forms.CheckBox chkRespaldoEnLinea;
        internal System.Windows.Forms.CheckBox chkIncluyeInsumosEnFactura;
        internal System.Windows.Forms.CheckBox chkUsaImpresoraImpacto;
        public System.Windows.Forms.TextBox txtCodigoServInst;
        public System.Windows.Forms.Label Label10;
        public System.Windows.Forms.TextBox txtPorcentajeInstalacion;
        public System.Windows.Forms.Label Label9;
        internal System.Windows.Forms.CheckBox chkDesgloseInst;
        internal System.Windows.Forms.CheckBox chkModificaDesc;
        internal System.Windows.Forms.CheckBox ckbAutoCompleta;
        internal System.Windows.Forms.CheckBox ckbContabiliza;
        public System.Windows.Forms.TextBox txtLineasFactura;
        public System.Windows.Forms.Label Label8;
        public System.Windows.Forms.TextBox txtPorcentajeIVA;
        public System.Windows.Forms.Label Label7;
        public System.Windows.Forms.TextBox txtImpresoraFactura;
        public System.Windows.Forms.Label Label6;
        public System.Windows.Forms.TextBox txtTelefono;
        public System.Windows.Forms.Label Label5;
        public System.Windows.Forms.TextBox txtDireccion;
        public System.Windows.Forms.Label Label4;
        public System.Windows.Forms.Label Label3;
        public System.Windows.Forms.TextBox txtIdentificacion;
        public System.Windows.Forms.Label Label2;
        public System.Windows.Forms.TextBox txtNombreComercial;
        public System.Windows.Forms.Label Label1;
        public System.Windows.Forms.Button btnEliminarDetalle;
        public System.Windows.Forms.Button btnInsertarDetalle;
        internal System.Windows.Forms.DataGridView dgvEquipos;
        public System.Windows.Forms.Button CmdConsultar;
        public System.Windows.Forms.TextBox txtFecha;
        public System.Windows.Forms.TextBox txtEquipo;
        public System.Windows.Forms.TextBox txtIdEmpresa;
        public System.Windows.Forms.Label _lblLabels_3;
        public System.Windows.Forms.Label _lblLabels_1;
        public System.Windows.Forms.Label _lblLabels_2;
        public System.Windows.Forms.Label _lblLabels_0;
    }
}
