﻿namespace LeandroSoftware.Activator
{
    partial class FrmMenu
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
            this.MenuStrip1 = new System.Windows.Forms.MenuStrip();
            this.tsRegistrarEmpresa = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSubirNuevaVersionApp = new System.Windows.Forms.ToolStripMenuItem();
            this.documentosPendientesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consultarToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.tsSalir = new System.Windows.Forms.ToolStripMenuItem();
            this.parametrosDelSistemaToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.MenuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // MenuStrip1
            // 
            this.MenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tsRegistrarEmpresa,
            this.tsSubirNuevaVersionApp,
            this.parametrosDelSistemaToolStripMenuItem,
            this.documentosPendientesToolStripMenuItem,
            this.tsSalir});
            this.MenuStrip1.Location = new System.Drawing.Point(0, 0);
            this.MenuStrip1.Name = "MenuStrip1";
            this.MenuStrip1.Size = new System.Drawing.Size(800, 24);
            this.MenuStrip1.TabIndex = 10;
            this.MenuStrip1.Text = "MenuStrip1";
            // 
            // tsRegistrarEmpresa
            // 
            this.tsRegistrarEmpresa.Name = "tsRegistrarEmpresa";
            this.tsRegistrarEmpresa.Size = new System.Drawing.Size(113, 20);
            this.tsRegistrarEmpresa.Text = "Registrar Empresa";
            this.tsRegistrarEmpresa.Visible = false;
            this.tsRegistrarEmpresa.Click += new System.EventHandler(this.tsRegistrarEmpresa_Click);
            // 
            // tsSubirNuevaVersionApp
            // 
            this.tsSubirNuevaVersionApp.Name = "tsSubirNuevaVersionApp";
            this.tsSubirNuevaVersionApp.Size = new System.Drawing.Size(168, 20);
            this.tsSubirNuevaVersionApp.Text = "Subir Nueva Version del App";
            this.tsSubirNuevaVersionApp.Visible = false;
            this.tsSubirNuevaVersionApp.Click += new System.EventHandler(this.tsSubirNuevaVersionApp_Click);
            // 
            // documentosPendientesToolStripMenuItem
            // 
            this.documentosPendientesToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.consultarToolStripMenuItem});
            this.documentosPendientesToolStripMenuItem.Name = "documentosPendientesToolStripMenuItem";
            this.documentosPendientesToolStripMenuItem.Size = new System.Drawing.Size(148, 20);
            this.documentosPendientesToolStripMenuItem.Text = "Documentos Pendientes";
            // 
            // consultarToolStripMenuItem
            // 
            this.consultarToolStripMenuItem.Name = "consultarToolStripMenuItem";
            this.consultarToolStripMenuItem.Size = new System.Drawing.Size(125, 22);
            this.consultarToolStripMenuItem.Text = "Consultar";
            this.consultarToolStripMenuItem.Click += new System.EventHandler(this.ConsultarToolStripMenuItem_Click);
            // 
            // tsSalir
            // 
            this.tsSalir.Name = "tsSalir";
            this.tsSalir.Size = new System.Drawing.Size(41, 20);
            this.tsSalir.Text = "Salir";
            this.tsSalir.Click += new System.EventHandler(this.tsSalir_Click);
            // 
            // parametrosDelSistemaToolStripMenuItem
            // 
            this.parametrosDelSistemaToolStripMenuItem.Name = "parametrosDelSistemaToolStripMenuItem";
            this.parametrosDelSistemaToolStripMenuItem.Size = new System.Drawing.Size(141, 20);
            this.parametrosDelSistemaToolStripMenuItem.Text = "Parametros del sistema";
            this.parametrosDelSistemaToolStripMenuItem.Click += new System.EventHandler(this.parametrosDelSistemaToolStripMenuItem_Click);
            // 
            // FrmMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.ControlDark;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.MenuStrip1);
            this.IsMdiContainer = true;
            this.Name = "FrmMenu";
            this.Text = "FrmMenu";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.MenuStrip1.ResumeLayout(false);
            this.MenuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        internal System.Windows.Forms.MenuStrip MenuStrip1;
        internal System.Windows.Forms.ToolStripMenuItem tsRegistrarEmpresa;
        internal System.Windows.Forms.ToolStripMenuItem tsSalir;
        private System.Windows.Forms.ToolStripMenuItem tsSubirNuevaVersionApp;
        private System.Windows.Forms.ToolStripMenuItem documentosPendientesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem consultarToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem parametrosDelSistemaToolStripMenuItem;
    }
}