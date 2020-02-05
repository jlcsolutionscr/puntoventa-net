﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using System.Data;
using System.Threading.Tasks;
using LeandroSoftware.Core.TiposComunes;
using LeandroSoftware.Core.Dominio.Entidades;
using LeandroSoftware.ClienteWCF;
using System.Globalization;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresa : Form
    {
        private DataTable dtRolePorEmpresa;
        private DataTable dtReportePorEmpresa;
        private Empresa empresa;
        private SucursalPorEmpresa sucursal;
        private TerminalPorSucursal terminal;
        private bool bolLoading = true;
        private bool bolLogoModificado = false;
        private bool bolCertificadoModificado = false;
        private bool bolListadoReporteModificado = false;
        private bool bolListadoRoleModificado = false;
        private bool bolSucursalNueva = true;
        private bool bolTerminalNueva = true;
        private string strRutaCertificado;
        private string strToken;
        public bool bolEditing;
        public int intIdEmpresa = -1;

        public FrmEmpresa()
        {
            InitializeComponent();
        }

        private void IniciaMaestroDetalle()
        {
            dtRolePorEmpresa = new DataTable();
            dtRolePorEmpresa.Columns.Add("Id", typeof(int));
            dtRolePorEmpresa.Columns.Add("Descripcion", typeof(string));
            DataColumn[] columns = new DataColumn[1];
            columns[0] = dtRolePorEmpresa.Columns[0];
            dtRolePorEmpresa.PrimaryKey = columns;

            dtReportePorEmpresa = new DataTable();
            dtReportePorEmpresa.Columns.Add("Id", typeof(int));
            dtReportePorEmpresa.Columns.Add("Descripcion", typeof(string));
            columns = new DataColumn[1];
            columns[0] = dtReportePorEmpresa.Columns[0];
            dtReportePorEmpresa.PrimaryKey = columns;
        }

        private void CargarLineaDetalleReporte()
        {
            if (cboReportes.SelectedValue != null)
            {
                string strValor = cboReportes.SelectedValue.ToString();
                if (dtReportePorEmpresa.Rows.IndexOf(dtReportePorEmpresa.Rows.Find(strValor)) >= 0)
                {
                    MessageBox.Show("El reporte seleccionado ya fue agregado al detalle.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataRow objRowEquipo = dtReportePorEmpresa.NewRow();
                    objRowEquipo["Id"] = cboReportes.SelectedValue;
                    objRowEquipo["Descripcion"] = cboReportes.Text;
                    dtReportePorEmpresa.Rows.Add(objRowEquipo);
                    dgvReportePorEmpresa.Refresh();
                }
            }
        }

        private void CargarLineaDetalleRole()
        {
            if (cboRoles.SelectedValue != null)
            {
                string strValor = cboRoles.SelectedValue.ToString();
                if (dtRolePorEmpresa.Rows.IndexOf(dtRolePorEmpresa.Rows.Find(strValor)) >= 0)
                {
                    MessageBox.Show("El role seleccionado ya fue agregado al detalle.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    DataRow objRowEquipo = dtRolePorEmpresa.NewRow();
                    objRowEquipo["Id"] = cboRoles.SelectedValue;
                    objRowEquipo["Descripcion"] = cboRoles.Text;
                    dtRolePorEmpresa.Rows.Add(objRowEquipo);
                    dgvRolePorEmpresa.Refresh();
                }
            }
        }

        private void EstablecerPropiedadesDataGridView()
        {
            DataGridViewTextBoxColumn dvcReportePorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcNombreReporte = new DataGridViewTextBoxColumn();

            dgvReportePorEmpresa.Columns.Clear();
            dgvReportePorEmpresa.AutoGenerateColumns = false;

            dvcReportePorEmpresa.DataPropertyName = "Id";
            dvcReportePorEmpresa.HeaderText = "Id";
            dvcReportePorEmpresa.Width = 40;
            dvcReportePorEmpresa.Visible = true;
            dvcReportePorEmpresa.ReadOnly = true;
            dgvReportePorEmpresa.Columns.Add(dvcReportePorEmpresa);

            dvcNombreReporte.DataPropertyName = "Descripcion";
            dvcNombreReporte.HeaderText = "Reporte";
            dvcNombreReporte.Width = 302;
            dvcNombreReporte.Visible = true;
            dvcNombreReporte.ReadOnly = true;
            dgvReportePorEmpresa.Columns.Add(dvcNombreReporte);

            DataGridViewTextBoxColumn dvcIdRole = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcNombreRole = new DataGridViewTextBoxColumn();

            dgvRolePorEmpresa.Columns.Clear();
            dgvRolePorEmpresa.AutoGenerateColumns = false;

            dvcIdRole.DataPropertyName = "Id";
            dvcIdRole.HeaderText = "Id";
            dvcIdRole.Width = 40;
            dvcIdRole.Visible = true;
            dvcIdRole.ReadOnly = true;
            dgvRolePorEmpresa.Columns.Add(dvcIdRole);

            dvcNombreRole.DataPropertyName = "Descripcion";
            dvcNombreRole.HeaderText = "Role";
            dvcNombreRole.Width = 302;
            dvcNombreRole.Visible = true;
            dvcNombreRole.ReadOnly = true;
            dgvRolePorEmpresa.Columns.Add(dvcNombreRole);
        }

        public async Task CargarListaParametros()
        {
            cboTipoIdentificacion.ValueMember = "Id";
            cboTipoIdentificacion.DisplayMember = "Descripcion";
            IList<LlaveDescripcion> dsTipoIdentificacion = await Administrador.ObtenerListadoTipoIdentificacion(strToken);
            cboTipoIdentificacion.DataSource = dsTipoIdentificacion;
            // Carga listado reportes
            cboReportes.ValueMember = "Id";
            cboReportes.DisplayMember = "Descripcion";
            IList<LlaveDescripcion> dsReportes = await Administrador.ObtenerListadoCatalogoReportes(strToken);
            cboReportes.DataSource = dsReportes;
            cboRoles.ValueMember = "Id";
            cboRoles.DisplayMember = "Descripcion";
            IList<LlaveDescripcion> dsRoles = await Administrador.ObtenerListadoRoles(strToken);
            cboRoles.DataSource = dsRoles;
            // Carga Tipo Contrato
            cboTipoContrato.ValueMember = "Id";
            cboTipoContrato.DisplayMember = "Descripcion";
            IList<LlaveDescripcion> dsTipoContrato = new List<LlaveDescripcion>();
            LlaveDescripcion tipo = new LlaveDescripcion(1, "Pago mensual o anual");
            dsTipoContrato.Add(tipo);
            tipo = new LlaveDescripcion(2, "Limite de documentos anual");
            dsTipoContrato.Add(tipo);
            cboTipoContrato.DataSource = dsTipoContrato;
        }

        public async Task CargarProvincias()
        {
            IList<LlaveDescripcion> dsDataSet = await Administrador.ObtenerListadoProvincias(strToken);
            cboProvincia.DataSource = dsDataSet;
            cboProvincia.ValueMember = "Id";
            cboProvincia.DisplayMember = "Descripcion";
        }

        public async Task CargarCantones(int intIdProvincia)
        {
            IList<LlaveDescripcion> dsDataSet = await Administrador.ObtenerListadoCantones(intIdProvincia, strToken);
            cboCanton.DataSource = dsDataSet;
            cboCanton.ValueMember = "Id";
            cboCanton.DisplayMember = "Descripcion";
        }

        public async Task CargarDistritos(int intIdProvincia, int intIdCanton)
        {
            IList<LlaveDescripcion> dsDataSet = await Administrador.ObtenerListadoDistritos(intIdProvincia, intIdCanton, strToken);
            cboDistrito.DataSource = dsDataSet;
            cboDistrito.ValueMember = "Id";
            cboDistrito.DisplayMember = "Descripcion";
        }

        public async Task CargarBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            IList<LlaveDescripcion> dsDataSet = await Administrador.ObtenerListadoBarrios(intIdProvincia, intIdCanton, intIdDistrito, strToken);
            cboBarrio.DataSource = dsDataSet;
            cboBarrio.ValueMember = "Id";
            cboBarrio.DisplayMember = "Descripcion";
        }

        private async void CargarSucursalPorEmpresa()
        {
            if (txtIdEmpresa.Text != "" && txtIdSucursal.Text != "")
            {
                sucursal = await Administrador.ObtenerSucursalPorEmpresa(int.Parse(txtIdEmpresa.Text), int.Parse(txtIdSucursal.Text), strToken);
                if (sucursal == null)
                {
                    bolSucursalNueva = true;
                    bolTerminalNueva = true;
                    sucursal = new SucursalPorEmpresa();
                    sucursal.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                    sucursal.IdSucursal = int.Parse(txtIdSucursal.Text);
                    txtNombreSucursal.Text = txtNombreComercial.Text != "" ? txtNombreComercial.Text : txtNombreEmpresa.Text;
                    txtDireccionSucursal.Text = txtDireccion.Text;
                    txtTelefonoSucursal.Text = txtTelefono.Text;
                    txtIdTerminal.Text = "1";
                    txtDescripcionTerminal.Text = "Terminal 1";
                    txtValorRegistro.Text = "";
                    terminal = new TerminalPorSucursal();
                    terminal.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                    terminal.IdSucursal = int.Parse(txtIdSucursal.Text);
                    terminal.IdTerminal = 1;
                    terminal.ValorRegistro = "";
                    terminal.ImpresoraFactura = "";
                    terminal.UltimoDocFE = 0;
                    terminal.UltimoDocND = 0;
                    terminal.UltimoDocNC = 0;
                    terminal.UltimoDocTE = 0;
                    terminal.UltimoDocMR = 0;
                    terminal.IdTipoDispositivo = chkDispositivoMovil.Checked ? StaticTipoDispisitivo.AppMovil : StaticTipoDispisitivo.AppEscritorio;
                    txtIdTerminal.Enabled = false;
                    txtValorRegistro.Enabled = false;
                    btnCargarTerminal.Enabled = false;
                    MessageBox.Show("La sucursal no están registrada para la empresa actual. Ingrese la información y proceda a guardar los cambios. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bolSucursalNueva = false;
                    bolTerminalNueva = false;
                    terminal = null;
                    txtIdTerminal.Text = "";
                    txtDescripcionTerminal.Text = "";
                    chkDispositivoMovil.Checked = false;
                    txtNombreSucursal.Text = sucursal.NombreSucursal;
                    txtDireccionSucursal.Text = sucursal.Direccion;
                    txtTelefonoSucursal.Text = sucursal.Telefono;
                    txtIdTerminal.Enabled = true;
                    txtValorRegistro.Enabled = true;
                    btnCargarTerminal.Enabled = true;
                }
            }
        }

        private async void CargarTerminalPorSucursal()
        {
            if (txtIdEmpresa.Text != "" && txtIdSucursal.Text != "" && txtIdTerminal.Text != "")
            {
                terminal = await Administrador.ObtenerTerminalPorSucursal(int.Parse(txtIdEmpresa.Text), int.Parse(txtIdSucursal.Text), int.Parse(txtIdTerminal.Text), strToken);
                if (terminal == null)
                {
                    bolTerminalNueva = true;
                    terminal = new TerminalPorSucursal();
                    terminal.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                    terminal.IdSucursal = int.Parse(txtIdSucursal.Text);
                    terminal.IdTerminal = int.Parse(txtIdTerminal.Text);
                    txtValorRegistro.Text = "";
                    txtDescripcionTerminal.Text = "Terminal " + txtIdTerminal.Text;
                    chkDispositivoMovil.Checked = false;
                    MessageBox.Show("La sucursal y terminal ingresados no están registrados para la empresa actual. Ingrese la información y proceda a guardar los cambios. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    bolTerminalNueva = false;
                    txtValorRegistro.Text = terminal.ValorRegistro;
                    txtDescripcionTerminal.Text = "Terminal " + txtIdTerminal.Text;
                    chkDispositivoMovil.Checked = terminal.IdTipoDispositivo == StaticTipoDispisitivo.AppMovil;
                }
            }
        }

        private async void FrmEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                strToken = FrmMenu.strToken;
                IniciaMaestroDetalle();
                EstablecerPropiedadesDataGridView();
                await CargarListaParametros();
                await CargarProvincias();
                dgvReportePorEmpresa.DataSource = dtReportePorEmpresa;
                dgvRolePorEmpresa.DataSource = dtRolePorEmpresa;
                if (bolEditing)
                {
                    empresa = await Administrador.ObtenerEmpresa(intIdEmpresa, strToken);
                    if (empresa != null)
                    {
                        txtIdEmpresa.Text = empresa.IdEmpresa.ToString();
                        txtNombreEmpresa.Text = empresa.NombreEmpresa;
                        txtNombreComercial.Text = empresa.NombreComercial;
                        cboTipoIdentificacion.SelectedValue = empresa.IdTipoIdentificacion;
                        txtIdentificacion.Text = empresa.Identificacion;
                        txtCodigoActividad.Text = empresa.CodigoActividad;
                        await CargarCantones(empresa.IdProvincia);
                        await CargarDistritos(empresa.IdProvincia, empresa.IdCanton);
                        await CargarBarrios(empresa.IdProvincia, empresa.IdCanton, empresa.IdDistrito);
                        cboProvincia.SelectedValue = empresa.IdProvincia;
                        cboCanton.SelectedValue = empresa.IdCanton;
                        cboDistrito.SelectedValue = empresa.IdDistrito;
                        cboBarrio.SelectedValue = empresa.IdBarrio;
                        txtDireccion.Text = empresa.Direccion;
                        txtTelefono.Text = empresa.Telefono1;
                        txtCorreoNotificacion.Text = empresa.CorreoNotificacion;
                        txtNombreCertificado.Text = empresa.NombreCertificado;
                        txtPinCertificado.Text = empresa.PinCertificado;
                        txtUsuarioATV.Text = empresa.UsuarioHacienda;
                        txtClaveATV.Text = empresa.ClaveHacienda;
                        txtLineasFactura.Text = empresa.LineasPorFactura.ToString();
                        if (empresa.FechaVence != null) txtFecha.Text = DateTime.Parse(empresa.FechaVence.ToString()).ToString("dd-MM-yyyy");
                        cboTipoContrato.SelectedValue = empresa.TipoContrato;
                        txtCantidadDocumentos.Text = empresa.CantidadDisponible.ToString();
                        chkContabiliza.Checked = empresa.Contabiliza;
                        chkAutoCompleta.Checked = empresa.AutoCompletaProducto;
                        chkRecibeDocumentos.Checked = empresa.RecepcionGastos;
                        chkFacturaElectronica.Checked = empresa.PermiteFacturar;
                        chkRegimenSimplificado.Checked = empresa.RegimenSimplificado;
                        chkAsignaVendedor.Checked = empresa.AsignaVendedorPorDefecto;
                        if (empresa.Logotipo != null)
                        {
                            try
                            {
                                using (MemoryStream ms = new MemoryStream(empresa.Logotipo))
                                    picLogo.Image = Image.FromStream(ms);
                            }
                            catch (Exception)
                            {
                                picLogo.Image = null;
                            }
                        }
                        else
                        {
                            picLogo.Image = null;
                        }
                        IList<LlaveDescripcion> reportePorEmpresa = await Administrador.ObtenerListadoReportePorEmpresa(empresa.IdEmpresa, strToken);
                        foreach (var reporte in reportePorEmpresa)
                        {
                            DataRow objRowEquipo = dtReportePorEmpresa.NewRow();
                            objRowEquipo["Id"] = reporte.Id;
                            objRowEquipo["Descripcion"] = reporte.Descripcion;
                            dtReportePorEmpresa.Rows.Add(objRowEquipo);
                        }
                        dgvReportePorEmpresa.Refresh();
                        IList<LlaveDescripcion> rolePorEmpresa = await Administrador.ObtenerListadoRolePorEmpresa(empresa.IdEmpresa, strToken);
                        foreach (var role in rolePorEmpresa)
                        {
                            DataRow objRowEquipo = dtRolePorEmpresa.NewRow();
                            objRowEquipo["Id"] = role.Id;
                            objRowEquipo["Descripcion"] = role.Descripcion;
                            dtRolePorEmpresa.Rows.Add(objRowEquipo);
                        }
                        dgvRolePorEmpresa.Refresh();
                    }
                    else
                    {
                        MessageBox.Show("No se logró obtener información de la empresa seleccionada.", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                else
                {
                    await CargarCantones(1);
                    await CargarDistritos(1, 1);
                    await CargarBarrios(1, 1, 1);
                    empresa = new Empresa();
                }
                bolLoading = false;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private bool ValidarCampos()
        {
            if (txtIdEmpresa.Text == "" & txtNombreEmpresa.Text == "" & txtUsuarioATV.Text == "" & txtClaveATV.Text == "")
                return false;
            else
            {
                return true;
            }
        }

        private void CmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private async void CmdUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Información incompleta", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                if (txtIdEmpresa.Text != "") empresa.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                empresa.NombreEmpresa = txtNombreEmpresa.Text;
                empresa.NombreComercial = txtNombreComercial.Text;
                empresa.IdTipoIdentificacion = (int)cboTipoIdentificacion.SelectedValue;
                empresa.CodigoActividad = txtCodigoActividad.Text;
                empresa.Identificacion = txtIdentificacion.Text;
                empresa.IdProvincia = (int)cboProvincia.SelectedValue;
                empresa.IdCanton = (int)cboCanton.SelectedValue;
                empresa.IdDistrito = (int)cboDistrito.SelectedValue;
                empresa.IdBarrio = (int)cboBarrio.SelectedValue;
                empresa.Direccion = txtDireccion.Text;
                empresa.Telefono1 = txtTelefono.Text;
                empresa.CorreoNotificacion = txtCorreoNotificacion.Text;
                empresa.NombreCertificado = txtNombreCertificado.Text;
                empresa.PinCertificado = txtPinCertificado.Text;
                empresa.UsuarioHacienda = txtUsuarioATV.Text;
                empresa.ClaveHacienda = txtClaveATV.Text;
                empresa.LineasPorFactura = int.Parse(txtLineasFactura.Text);
                if (txtFecha.Text != "  /  /")
                {
                    empresa.FechaVence = DateTime.ParseExact(txtFecha.Text + " 23:59:59", "dd/MM/yyyy HH:mm:ss", CultureInfo.InvariantCulture);
                }
                else
                {
                    empresa.FechaVence = null;
                }
                empresa.IdTipoMoneda = 1;
                empresa.TipoContrato = (int)cboTipoContrato.SelectedValue;
                empresa.CantidadDisponible = int.Parse(txtCantidadDocumentos.Text);
                empresa.Contabiliza = chkContabiliza.Checked;
                empresa.AutoCompletaProducto = chkAutoCompleta.Checked;
                empresa.RecepcionGastos = chkRecibeDocumentos.Checked;
                empresa.PermiteFacturar = chkFacturaElectronica.Checked;
                empresa.RegimenSimplificado = chkRegimenSimplificado.Checked;
                empresa.AsignaVendedorPorDefecto = chkAsignaVendedor.Checked;
                if (txtIdEmpresa.Text == "")
                {
                    txtIdEmpresa.Text = await Administrador.AgregarEmpresa(empresa, strToken);
                }
                else
                {
                    await Administrador.ActualizarEmpresa(empresa, strToken);
                }
                if (bolLogoModificado)
                {
                    if (picLogo.Image != null)
                    {
                        byte[] bytLogotipo;
                        using (MemoryStream stream = new MemoryStream())
                        {
                            picLogo.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                            bytLogotipo = stream.ToArray();
                        }
                        await Administrador.ActualizarLogoEmpresa(int.Parse(txtIdEmpresa.Text), Convert.ToBase64String(bytLogotipo), strToken);
                    }
                    else
                    {
                        await Administrador.RemoverLogoEmpresa(int.Parse(txtIdEmpresa.Text), strToken);
                    }
                }
                if (bolCertificadoModificado && txtNombreCertificado.Text != "")
                {
                    byte[] bytCertificado = File.ReadAllBytes(strRutaCertificado);
                    await Administrador.ActualizarCertificadoEmpresa(int.Parse(txtIdEmpresa.Text), Convert.ToBase64String(bytCertificado), strToken);
                }
                if (bolListadoReporteModificado)
                {
                    List<ReportePorEmpresa> listado = new List<ReportePorEmpresa>();
                    foreach (DataRow row in dtReportePorEmpresa.Rows)
                    {
                        ReportePorEmpresa reporte = new ReportePorEmpresa();
                        reporte.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                        reporte.IdReporte = int.Parse(row["Id"].ToString());
                        listado.Add(reporte);
                    }
                    await Administrador.ActualizarReportePorEmpresa(int.Parse(txtIdEmpresa.Text), listado, strToken);
                }
                if (bolListadoRoleModificado)
                {
                    List<RolePorEmpresa> listado = new List<RolePorEmpresa>();
                    foreach (DataRow row in dtRolePorEmpresa.Rows)
                    {
                        RolePorEmpresa reporte = new RolePorEmpresa();
                        reporte.IdEmpresa = int.Parse(txtIdEmpresa.Text);
                        reporte.IdRole = int.Parse(row["Id"].ToString());
                        listado.Add(reporte);
                    }
                    await Administrador.ActualizarRolePorEmpresa(int.Parse(txtIdEmpresa.Text), listado, strToken);
                }
                if (sucursal != null)
                {
                    sucursal.NombreSucursal = txtNombreSucursal.Text;
                    sucursal.Direccion = txtDireccionSucursal.Text;
                    sucursal.Telefono = txtTelefonoSucursal.Text;
                    if (bolSucursalNueva)
                    {
                        await Administrador.AgregarSucursalPorEmpresa(sucursal, strToken);
                        bolSucursalNueva = false;
                    }
                    else
                        await Administrador.ActualizarSucursalPorEmpresa(sucursal, strToken);
                    if (terminal != null)
                    {
                        terminal.ValorRegistro = txtValorRegistro.Text;
                        terminal.IdTipoDispositivo = chkDispositivoMovil.Checked ? StaticTipoDispisitivo.AppMovil : StaticTipoDispisitivo.AppEscritorio;
                        if (bolTerminalNueva)
                        {
                            await Administrador.AgregarTerminalPorSucursal(terminal, strToken);
                            bolTerminalNueva = false;
                        }
                        else
                            await Administrador.ActualizarTerminalPorSucursal(terminal, strToken);
                    }
                }
                MessageBox.Show("Información registrada satisfactoriamente. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro" + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private async void CboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                await CargarCantones(intIdProvincia);
                await CargarDistritos(intIdProvincia, 1);
                await CargarBarrios(intIdProvincia, 1, 1);
            }
        }

        private async void CboCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                int intIdCanton = (int)cboCanton.SelectedValue;
                await CargarDistritos(intIdProvincia, intIdCanton);
                await CargarBarrios(intIdProvincia, intIdCanton, 1);
            }
        }

        private async void CboDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = (int)cboProvincia.SelectedValue;
                int intIdCanton = (int)cboCanton.SelectedValue;
                int intIdDistrito = (int)cboDistrito.SelectedValue;
                await CargarBarrios(intIdProvincia, intIdCanton, intIdDistrito);
            }
        }

        private void BtnInsertaReporte_Click(object sender, EventArgs e)
        {
            bolListadoReporteModificado = true;
            CargarLineaDetalleReporte();
        }

        private void BtnEliminaReporte_Click(object sender, EventArgs e)
        {
            bolListadoReporteModificado = true;
            if (dtReportePorEmpresa.Rows.Count > 0) dtReportePorEmpresa.Rows.Remove(dtReportePorEmpresa.Rows.Find(dgvReportePorEmpresa.CurrentRow.Cells[0].Value));
        }

        private void btnInsertaRole_Click(object sender, EventArgs e)
        {
            bolListadoRoleModificado = true;
            CargarLineaDetalleRole();
        }

        private void btnEliminaRole_Click(object sender, EventArgs e)
        {
            bolListadoRoleModificado = true;
            if (dtRolePorEmpresa.Rows.Count > 0) dtRolePorEmpresa.Rows.Remove(dtRolePorEmpresa.Rows.Find(dgvRolePorEmpresa.CurrentRow.Cells[0].Value));
        }

        private void BtnCargarLogo_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "png";
            ofdAbrirDocumento.Filter = "PNG Image Files|*.png;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName);
                    bolLogoModificado = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el archivo. Verifique que sea un archivo de imagen válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void btnLimpiarLogo_Click(object sender, EventArgs e)
        {
            DialogResult dialogResult = MessageBox.Show("Desea eliminar el logotipo?", "Leandro Software", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dialogResult == DialogResult.Yes)
            {
                picLogo.Image = null;
                bolLogoModificado = true;
            }
        }

        private void BtnCargarCertificado_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "p12";
            ofdAbrirDocumento.Filter = "Certificate Files|*.p12;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    strRutaCertificado = ofdAbrirDocumento.FileName;
                    txtNombreCertificado.Text = Path.GetFileName(strRutaCertificado);
                    bolCertificadoModificado = true;
                }
                catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el certificado. Verifique que sea un archivo .p12 válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void BtnCargarSucursal_Click(object sender, EventArgs e)
        {
            if (txtIdEmpresa.Text != "" && txtIdSucursal.Text != "")
            {
                CargarSucursalPorEmpresa();
            }
        }

        private void BtnCargarTerminal_Click(object sender, EventArgs e)
        {
            if (txtIdEmpresa.Text != "" && txtIdSucursal.Text != "" && txtIdTerminal.Text != "")
            {
                CargarTerminalPorSucursal();
            }
        }
    }
}