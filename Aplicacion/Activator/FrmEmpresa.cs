using LeandroSoftware.AccesoDatos.Dominio.Entidades;
using LeandroSoftware.AccesoDatos.TiposDatos;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Net.Http;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Windows.Forms;
using System.Data;

namespace LeandroSoftware.Activator
{
    public partial class FrmEmpresa : Form
    {
        private DataTable dtEquipos;
        private DataTable objDatosLocal;
        private Empresa empresa;
        private bool bolLoading = true;
        private static HttpClient client = new HttpClient();
        public string strServicioPuntoventaURL;
        public bool bolEditing;
        public int intIdEmpresa = -1;

        public FrmEmpresa()
        {
            InitializeComponent();
        }

        private void IniciaDetalleEmpresa()
        {
            dtEquipos = new DataTable();
            dtEquipos.Columns.Add("VALORREGISTRO", typeof(string));
            dtEquipos.Columns.Add("IMPRESORAFACTURA", typeof(string));
            dtEquipos.Columns.Add("USAIMPRESORAIMPACTO", typeof(bool));
            DataColumn[] columns = new DataColumn[1];
            columns[0] = dtEquipos.Columns[0];
            dtEquipos.PrimaryKey = columns;
        }

        private void CargarDetalleEquipos()
        {
            DataTable objDatosLocal = new DataTable();
            dtEquipos.Rows.Clear();
            if (txtIdEmpresa.Text <> "")
            {
                Dim adapter As MySqlDataAdapter
                Dim ocxConexion As MySqlConnection = New MySqlConnection(strCadenaConexion)
                ocxConexion.Open()
                adapter = New MySqlDataAdapter("SELECT ValorRegistro, ImpresoraFactura, UsaImpresoraImpacto FROM DetalleRegistro WHERE IdEmpresa = " & txtIdEmpresa.Text, ocxConexion)
                Dim dsDataSet As DataSet = New DataSet()
                adapter.Fill(dsDataSet, "DetalleRegistro")
                ocxConexion.Close()
                objDatosLocal = dsDataSet.Tables("DetalleRegistro")
                If objDatosLocal.Rows.Count > 0 Then
                    For I = 0 To objDatosLocal.Rows.Count - 1
                        objRowEquipo = dtEquipos.NewRow
                        objRowEquipo.Item(0) = objDatosLocal.Rows(I).Item(0)
                        objRowEquipo.Item(1) = objDatosLocal.Rows(I).Item(1)
                        objRowEquipo.Item(2) = objDatosLocal.Rows(I).Item(2)
                        dtEquipos.Rows.Add(objRowEquipo)
                    Next
                End If
            End If
        }

    Private Sub CargarLineaDetalleEquipo()
        If txtEquipo.Text<> "" Then
            Dim intIndice As Integer = dtEquipos.Rows.IndexOf(dtEquipos.Rows.Find(txtEquipo.Text))
            If intIndice >= 0 Then
                dtEquipos.Rows(intIndice).Item(1) = txtImpresoraFactura.Text
                dtEquipos.Rows(intIndice).Item(2) = chkUsaImpresoraImpacto.Checked
            Else
                objRowEquipo = dtEquipos.NewRow
                objRowEquipo.Item(0) = txtEquipo.Text
                objRowEquipo.Item(1) = txtImpresoraFactura.Text
                objRowEquipo.Item(2) = chkUsaImpresoraImpacto.Checked
                dtEquipos.Rows.Add(objRowEquipo)
                dgvEquipos.Refresh()
                txtEquipo.Text = ""
                txtImpresoraFactura.Text = ""
            End If
        End If
    End Sub

    private void EstablecerPropiedadesDataGridView()
    {
            dgvEquipos.Columns.Clear();
            dgvEquipos.AutoGenerateColumns = false;

            DataGridViewTextBoxColumn dvcValorEmpresa = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcImpresoraFactura = new DataGridViewTextBoxColumn();
            DataGridViewTextBoxColumn dvcUsaImpresoraImpacto = new DataGridViewTextBoxColumn();

            dvcValorEmpresa.DataPropertyName = "VALORREGISTRO";
            dvcValorEmpresa.HeaderText = "Equipos Registrados";
            dvcValorEmpresa.Width = 100;
            dvcValorEmpresa.Visible = true;
            dvcValorEmpresa.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcValorEmpresa);

            dvcImpresoraFactura.DataPropertyName = "IMPRESORAFACTURA";
            dvcImpresoraFactura.HeaderText = "Impresora Fact";
            dvcImpresoraFactura.Width = 150;
        dvcImpresoraFactura.Visible = true;
        dvcImpresoraFactura.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcImpresoraFactura);

            dvcUsaImpresoraImpacto.DataPropertyName = "USAIMPRESORAIMPACTO";
            dvcUsaImpresoraImpacto.HeaderText = "Usa Impr. Impacto";
            dvcUsaImpresoraImpacto.Width = 80;
        dvcUsaImpresoraImpacto.Visible = true;
        dvcUsaImpresoraImpacto.ReadOnly = true;
            dgvEquipos.Columns.Add(dvcUsaImpresoraImpacto);
    }

        public void CargarTiposIdentificacion()
        {
            RequestDTO request = new RequestDTO
            {
                NombreMetodo = "ObtenerListaTipoIdentificacion",
                DatosPeticion = ""
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = new JavaScriptSerializer().Serialize(request);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = new JavaScriptSerializer().Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<TipoIdentificacion> dsDataSet = Array.Empty<TipoIdentificacion>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = new JavaScriptSerializer().Deserialize<List<TipoIdentificacion>>(respuesta.DatosPeticion);
                }
                cboTipoIdentificacion.DataSource = dsDataSet;
                cboTipoIdentificacion.ValueMember = "IdTipoIdentificacion";
                cboTipoIdentificacion.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarProvincias()
        {
            RequestDTO request = new RequestDTO
            {
                NombreMetodo = "ObtenerListaProvincias",
                DatosPeticion = ""
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = new JavaScriptSerializer().Serialize(request);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = new JavaScriptSerializer().Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Provincia> dsDataSet = Array.Empty<Provincia>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = new JavaScriptSerializer().Deserialize<List<Provincia>>(respuesta.DatosPeticion);
                }
                cboProvincia.DataSource = dsDataSet;
                cboProvincia.ValueMember = "IdProvincia";
                cboProvincia.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarCantones(int intIdProvincia)
        {
            RequestDTO request = new RequestDTO
            {
                NombreMetodo = "ObtenerListaCantones",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + "}"
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = new JavaScriptSerializer().Serialize(request);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = new JavaScriptSerializer().Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Canton> dsDataSet = Array.Empty<Canton>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = new JavaScriptSerializer().Deserialize<List<Canton>>(respuesta.DatosPeticion);
                }
                cboCanton.DataSource = dsDataSet;
                cboCanton.ValueMember = "IdCanton";
                cboCanton.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarDistritos(int intIdProvincia, int intIdCanton)
        {
            RequestDTO request = new RequestDTO
            {
                NombreMetodo = "ObtenerListaDistritos",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ",IdCanton: " + intIdCanton + "}"
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = new JavaScriptSerializer().Serialize(request);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = new JavaScriptSerializer().Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Distrito> dsDataSet = Array.Empty<Distrito>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = new JavaScriptSerializer().Deserialize<List<Distrito>>(respuesta.DatosPeticion);
                }
                cboDistrito.DataSource = dsDataSet;
                cboDistrito.ValueMember = "IdDistrito";
                cboDistrito.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        public void CargarBarrios(int intIdProvincia, int intIdCanton, int intIdDistrito)
        {
            RequestDTO request = new RequestDTO
            {
                NombreMetodo = "ObtenerListaBarrios",
                DatosPeticion = "{IdProvincia: " + intIdProvincia + ", IdCanton: " + intIdCanton + ", IdDistrito: " + intIdDistrito + "}"
            };
            Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
            string jsonRequest = new JavaScriptSerializer().Serialize(request);
            StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
            Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
            try
            {
                task1.Wait();
                if (!task1.Result.IsSuccessStatusCode)
                {
                    MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Close();
                    return;
                }
                ResponseDTO respuesta = new JavaScriptSerializer().Deserialize<ResponseDTO>(task1.Result.Content.ReadAsStringAsync().Result);
                IList<Barrio> dsDataSet = Array.Empty<Barrio>();
                if (respuesta.DatosPeticion != null)
                {
                    dsDataSet = new JavaScriptSerializer().Deserialize<List<Barrio>>(respuesta.DatosPeticion);
                }
                cboBarrio.DataSource = dsDataSet;
                cboBarrio.ValueMember = "IdBarrio";
                cboBarrio.DisplayMember = "Descripcion";
            }
            catch (AggregateException ex)
            {
                Exception newEx = ex.Flatten();
                MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Close();
            }
        }

        private void FrmEmpresa_Load(object sender, EventArgs e)
        {
            try
            {
                CargarTiposIdentificacion();
                CargarProvincias();
                CargarCantones(1);
                CargarDistritos(1, 1);
                CargarBarrios(1, 1, 1);
                if (bolLoading)
                {
                    RequestDTO request = new RequestDTO
                    {
                        NombreMetodo = ""
                    };

                    Uri uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
                    Task<HttpResponseMessage> task1 = client.GetAsync(uri);
                    try
                    {
                        task1.Wait();
                        if (!task1.Result.IsSuccessStatusCode)
                        {
                            MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            Close();
                        }
                        else
                        {
                            string results = task1.Result.Content.ReadAsStringAsync().Result;
                            JavaScriptSerializer json_serializer = new JavaScriptSerializer();
                            ResponseDTO datos = json_serializer.Deserialize<ResponseDTO>(results);
                            BinaryFormatter serializer = new BinaryFormatter();
                            using (var ms = new MemoryStream(Convert.FromBase64String(datos.DatosPeticion)))
                            {
                                empresa = (Empresa)serializer.Deserialize(ms);
                            }
                            txtIdEmpresa.Text = empresa.IdEmpresa.ToString();
                            txtNombreEmpresa.Text = empresa.NombreEmpresa;
                            txtUsuarioATV.Text = empresa.UsuarioHacienda;
                            txtClaveATV.Text = empresa.ClaveHacienda;
                            txtCorreoNotificacion.Text = empresa.CorreoNotificacion;
                            chkFacturaElectronica.Checked = empresa.PermiteFacturar;
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
                        }
                    }
                    catch (AggregateException ex)
                    {
                        Exception newEx = ex.Flatten();
                        MessageBox.Show("Error al consumir el servicio web de factura electrónica: " + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Close();
                    }
                }
                else
                {
                    empresa = new Empresa();
                }
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

        private void cmdCancel_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void cmdUpdate_Click(object sender, EventArgs e)
        {
            if (!ValidarCampos())
            {
                MessageBox.Show("Información incompleta", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            try
            {
                empresa.NombreEmpresa = txtNombreEmpresa.Text;
                empresa.UsuarioHacienda = txtUsuarioATV.Text;
                empresa.ClaveHacienda = txtClaveATV.Text;
                empresa.CorreoNotificacion = txtCorreoNotificacion.Text;
                empresa.PermiteFacturar = chkFacturaElectronica.Checked;
                if (picLogo.Image != null)
                {
                    MemoryStream stream = new MemoryStream();
                    picLogo.Image.Save(stream, System.Drawing.Imaging.ImageFormat.Png);
                    empresa.Logotipo = stream.ToArray();
                }
                else
                {
                    empresa.Logotipo = null;
                }
                BinaryFormatter serializer = new BinaryFormatter();
                string strDatos;
                using (MemoryStream ms = new MemoryStream())
                {
                    serializer.Serialize(ms, empresa);
                    strDatos = Convert.ToBase64String(ms.ToArray());
                }
                RequestDTO peticion = new RequestDTO();
                Uri uri;
                if (txtIdEmpresa.Text == "")
                {
                    peticion.NombreMetodo = "AgregarEmpresa";
                    uri = new Uri(strServicioPuntoventaURL + "/ejecutarconsulta");
                }
                else
                {
                    peticion.NombreMetodo = "ActualizarEmpresa";
                    uri = new Uri(strServicioPuntoventaURL + "/ejecutar");
                }
                peticion.DatosPeticion = strDatos;
                string jsonRequest = new JavaScriptSerializer().Serialize(empresa);
                StringContent stringContent = new StringContent(jsonRequest, Encoding.UTF8, "application/json");
                Task<HttpResponseMessage> task1 = client.PostAsync(uri, stringContent);
                try
                {
                    task1.Wait();
                    if (!task1.Result.IsSuccessStatusCode)
                    {
                        MessageBox.Show("Error: " + task1.Result.ReasonPhrase, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    if (txtIdEmpresa.Text == "")
                        txtIdEmpresa.Text = task1.Result.Content.ReadAsStringAsync().Result.Replace("\"", "");
                }
                catch (AggregateException ex)
                {
                    Exception newEx = ex.Flatten();
                    MessageBox.Show("Error al actualizar el registro: " + newEx.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error al actualizar el registro" + ex.Message, "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            Close();
        }

        private void btnCargarLogo_Click(object sender, EventArgs e)
        {
            ofdAbrirDocumento.DefaultExt = "png";
            ofdAbrirDocumento.Filter = "PNG Image Files|*.png;";
            DialogResult result = ofdAbrirDocumento.ShowDialog();
            if (result == DialogResult.OK)
            {
                try
                {
                    picLogo.Image = Image.FromFile(ofdAbrirDocumento.FileName);
                } catch (Exception)
                {
                    MessageBox.Show("Error al intentar cargar el archivo. Verifique que sea un archivo de imagen válido. . .", "Leandro Software", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void cboProvincia_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = int.Parse(cboProvincia.SelectedValue.ToString());
                CargarCantones(intIdProvincia);
                CargarDistritos(intIdProvincia, 1);
                CargarBarrios(intIdProvincia, 1, 1);
            }
        }

        private void cboCanton_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = int.Parse(cboProvincia.SelectedValue.ToString());
                int intIdCanton = int.Parse(cboCanton.SelectedValue.ToString());
                CargarDistritos(intIdProvincia, intIdCanton);
                CargarBarrios(intIdProvincia, intIdCanton, 1);
            }
        }

        private void cboDistrito_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!bolLoading)
            {
                int intIdProvincia = int.Parse(cboProvincia.SelectedValue.ToString());
                int intIdCanton = int.Parse(cboCanton.SelectedValue.ToString());
                int intIdDistrito = int.Parse(cboDistrito.SelectedValue.ToString());
                CargarBarrios(intIdProvincia, intIdCanton, intIdDistrito);
            }
        }
    }
}
