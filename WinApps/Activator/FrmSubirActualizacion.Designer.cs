namespace LeandroSoftware.Activator
{
    partial class FrmSubirActualizacion
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.ofdAbrirDocumento = new System.Windows.Forms.OpenFileDialog();
            this.btnCargar = new System.Windows.Forms.Button();
            this.txtFileName = new System.Windows.Forms.TextBox();
            this.txtVersion = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtSubVersion = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtRevision = new System.Windows.Forms.TextBox();
            this.btnActualizar = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.txtBuild = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // btnCargar
            // 
            this.btnCargar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnCargar.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnCargar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnCargar.Location = new System.Drawing.Point(391, 9);
            this.btnCargar.Name = "btnCargar";
            this.btnCargar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnCargar.Size = new System.Drawing.Size(90, 25);
            this.btnCargar.TabIndex = 1;
            this.btnCargar.TabStop = false;
            this.btnCargar.Text = "Cargar archivo";
            this.btnCargar.UseVisualStyleBackColor = false;
            this.btnCargar.Click += new System.EventHandler(this.btnCargar_Click);
            // 
            // txtFileName
            // 
            this.txtFileName.Location = new System.Drawing.Point(12, 12);
            this.txtFileName.Name = "txtFileName";
            this.txtFileName.ReadOnly = true;
            this.txtFileName.Size = new System.Drawing.Size(374, 20);
            this.txtFileName.TabIndex = 2;
            // 
            // txtVersion
            // 
            this.txtVersion.Location = new System.Drawing.Point(60, 57);
            this.txtVersion.Name = "txtVersion";
            this.txtVersion.Size = new System.Drawing.Size(29, 20);
            this.txtVersion.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 61);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 13);
            this.label1.TabIndex = 4;
            this.label1.Text = "Versión";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(95, 61);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 13);
            this.label2.TabIndex = 6;
            this.label2.Text = "Sub Versión";
            // 
            // txtSubVersion
            // 
            this.txtSubVersion.Location = new System.Drawing.Point(165, 57);
            this.txtSubVersion.Name = "txtSubVersion";
            this.txtSubVersion.Size = new System.Drawing.Size(29, 20);
            this.txtSubVersion.TabIndex = 5;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(289, 61);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 13);
            this.label3.TabIndex = 8;
            this.label3.Text = "Revisión";
            // 
            // txtRevision
            // 
            this.txtRevision.Location = new System.Drawing.Point(343, 57);
            this.txtRevision.Name = "txtRevision";
            this.txtRevision.Size = new System.Drawing.Size(29, 20);
            this.txtRevision.TabIndex = 7;
            // 
            // btnActualizar
            // 
            this.btnActualizar.BackColor = System.Drawing.SystemColors.ButtonHighlight;
            this.btnActualizar.Cursor = System.Windows.Forms.Cursors.Default;
            this.btnActualizar.Enabled = false;
            this.btnActualizar.ForeColor = System.Drawing.SystemColors.ControlText;
            this.btnActualizar.Location = new System.Drawing.Point(400, 55);
            this.btnActualizar.Name = "btnActualizar";
            this.btnActualizar.RightToLeft = System.Windows.Forms.RightToLeft.No;
            this.btnActualizar.Size = new System.Drawing.Size(90, 25);
            this.btnActualizar.TabIndex = 9;
            this.btnActualizar.TabStop = false;
            this.btnActualizar.Text = "Actualizar";
            this.btnActualizar.UseVisualStyleBackColor = false;
            this.btnActualizar.Click += new System.EventHandler(this.btnActualizar_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(200, 61);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(30, 13);
            this.label4.TabIndex = 11;
            this.label4.Text = "Build";
            // 
            // txtBuild
            // 
            this.txtBuild.Location = new System.Drawing.Point(254, 57);
            this.txtBuild.Name = "txtBuild";
            this.txtBuild.Size = new System.Drawing.Size(29, 20);
            this.txtBuild.TabIndex = 10;
            // 
            // FrmSubirActualizacion
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(193)))), ((int)(((byte)(215)))), ((int)(((byte)(225)))));
            this.ClientSize = new System.Drawing.Size(493, 94);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtBuild);
            this.Controls.Add(this.btnActualizar);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtRevision);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtSubVersion);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtVersion);
            this.Controls.Add(this.txtFileName);
            this.Controls.Add(this.btnCargar);
            this.Name = "FrmSubirActualizacion";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Subir actualización de versión del punto de venta";
            this.Load += new System.EventHandler(this.FrmSubirActualizacion_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.OpenFileDialog ofdAbrirDocumento;
        public System.Windows.Forms.Button btnCargar;
        private System.Windows.Forms.TextBox txtFileName;
        private System.Windows.Forms.TextBox txtVersion;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtSubVersion;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtRevision;
        public System.Windows.Forms.Button btnActualizar;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtBuild;
    }
}