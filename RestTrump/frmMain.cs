using System;
using System.Drawing;
using System.Windows.Forms;
using System.Configuration;
using System.Data.OleDb;
using System.IO;
using System.Data;
using DevComponents.DotNetBar;
using DevComponents.DotNetBar.Controls;
using System.Collections.Generic;
using System.Linq;
using DevComponents.Editors;
using static RestTrump.Program;
using RestTrump.dsPpalTableAdapters;

namespace RestTrump
{
    public partial class frmMain : Form
    {
        string cnnString = "";
        Color Rojo = Color.Red;
        Color Verde = Color.Lime;
        string cnnStringModel = @"Provider=VFPOLEDB.1;Data Source={0}";
        string appDirConfig;
        private bool esconectado;
        decimal DivisaBs;
        private bool esChanging;
        private decimal codigoMoneda;
        List<CheckBox> gruposFiltrados;
        private bool esChanginTodo;
        private bool esConectado
        {
            get
            {
                //Establecer COnexion
                OleDbConnection cnn = new OleDbConnection(cnnString);
                try
                {
                    cnn.Open();
                    if (cnn.State == System.Data.ConnectionState.Open)
                        esconectado = true;
                    else
                    {
                        MessageBox.Show("" +
                       "No éxiste conexión con " + appDirConfig + "\nVerifique la dirección e intente de nuevo!",
                       "Intento Conexión fallido",
                       MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        esconectado = false;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("" +
                        "Error al Intentar Conectarse a la Base de datos en " + appDirConfig + "\nVerifique la dirección e intente de nuevo!",
                        "Intento Conexión fallido",
                        MessageBoxButtons.OK, MessageBoxIcon.Error);
                    MessageBox.Show(ex.Message);
                    esconectado = false;
                }
                finally
                {
                    cnn = null;
                }
                if (esconectado)
                {
                    guardarDatos();
                }

                lblNumRegistros.Visible = esconectado;
                return esconectado;
            }
        }

        public frmMain()
        {
            InitializeComponent();
            appDirConfig = Properties.Settings.Default.appDir_Datos;
            DivisaBs = (decimal)Properties.Settings.Default.LastDivisa;
            cnnString = Properties.Settings.Default.appCadenaConexionHuesped;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Probar si existe Conexion
            //chkPrecio3.Checked = Properties.Settings.Default.esPrecio3;
            txtDivisa.Text = DivisaBs.ToString();
            DecimalesCalculo = Properties.Settings.Default.DecimalCalculo;

            dsPpal.invenc.ColumnChanged += Inven_ColumnChanged;
            if (ProbarConexion() && esconectado)
                ProductosNuevos();
            loadDatos();
            InicializarSearch();
        }
        private void Inven_ColumnChanged(object sender, DataColumnChangeEventArgs e)
        {
            try
            {
                if (!esChanginTodo)
                    if (!esChanging)
                    {
                        if (dsPpal.invenc_usa.Rows.Count > 0)
                            initGridFinal();
                        esChanging = true;
                        DataRow item = e.Row;
                        getDatosItem(item);
                        switch (e.Column.ColumnName)
                        {
                            case "precio":
                                setCalcularTotales(lastPrecio, 0, true, esPorLote: true);
                                break;
                            case "divisa":
                                setCalcularTotales(lastDivisa, 1, true, false, true);
                                break;
                        }
                        try
                        {
                            item.BeginEdit();
                            item["divisa"] = lastDivisa;
                            item["divisafactor"] = lastDivisaFactor;
                            item["precio"] = lastPrecio;


                        }
                        catch (Exception ex)
                        {
                            ksslib.clsUtilErrors.Manejador_errores(ex, true);
                        }
                        finally
                        {
                            item.EndEdit();
                            esChanging = false;
                        }
                    }
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex, false);
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea Cerrar la Aplicación?", "Cerrar Aplicación", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                if (MessageBox.Show("Desea Guardar los Cambios?", "Guardar Cambios", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    guardarDatos();
                }
                Properties.Settings.Default.LastDateUSe = DateTime.Now >= Program.LastDate ? encryptHelper.encryptus(DateTime.Now.ToString(), Program.KeyClave) : encryptHelper.encryptus(Program.LastDate.ToString(), Program.KeyClave);
                Properties.Settings.Default.Save();
                this.Close();
                Application.Exit();
            }
        }
        private void initGridFinal()
        {
            if (!esModificadores)
                this.dsPpal.invenc_usa.Clear();
            else
                dsPpal.tmpmodifi.Clear();

            this.gridFinal.PrimaryGrid.DataSource = null;
        }
        private void Simular_Click(object sender, EventArgs e)
        {
            errorTry++;
            try
            {
                //Guarda los cambios manuales
                if (dsPpal.invenc.GetChanges() != null)
                    guardarCambiosDBGrid();

                FactorDivisaBs = Convert.ToDecimal(txtDivisa.Text);
                //Realizar calculos a Trumps
                int modi = 0;
                string txt = "";
                txt = dsPpal.HasChanges() ? "MODIFICADOS" : "FILTRADOS";
                msjProgress(1, "Actualizando Origen/Destino...\nEspere por favor");
                List<DataRow> dt;
                if (!esModificadores)
                    dt = dsPpal.invenc.GetChanges() != null ? dsPpal.invenc.GetChanges().Rows.Cast<DataRow>().ToList() : dsPpal.invenc.Rows.Cast<DataRow>().ToList();
                else
                    dt = dsPpal.modifi.GetChanges() != null ? dsPpal.modifi.GetChanges().Rows.Cast<DataRow>().ToList() : dsPpal.modifi.Rows.Cast<DataRow>().ToList();
                int Registros = dt.Count;
                dsPpal.tmpmodifi.Clear();
                dsPpal.invenc_usa.Clear();
                try
                {
                    foreach (System.Data.DataRow item in dt)
                    {
                        getDatosItem(item);
                        if (!esModificadores)
                            dsPpal.invenc_usa.ImportRow(item);
                        else
                            dsPpal.tmpmodifi.ImportRow(item);
                        DataRow[] tmpRow = null;
                        if (!esModificadores)
                            tmpRow = dsPpal.invenc_usa.Select($"codigo like '{lastCodigo}'");
                        else
                            tmpRow = dsPpal.tmpmodifi.Select($"codigo like '{lastCodigo}'");

                        if (tmpRow != null)
                        {
                            foreach (var _tmpRow in tmpRow)
                            {
                                lastDivisaFactor = 0;
                                setCalcularTotales(lastDivisa, 1);
                                _tmpRow.BeginEdit();
                                _tmpRow["precio"] = lastPrecio;
                                _tmpRow["divisa"] = lastDivisa;
                                _tmpRow["divisafactor"] = lastDivisaFactor;
                                string descr = _tmpRow["descr"].ToString();
                                _tmpRow.EndEdit();
                                if (!esModificadores)
                                    modi += inven_usaTableAdapter.UpdateQueryPorCodigo(Math.Round(lastPrecio, 2), lastDivisa, lastDivisaFactor.ToString(), lastCodigo);
                                else
                                    modi++;
                            }
                        }

                        //decimal newDivisa = lastDivisa == 0 ? Convert.ToDecimal(lastPrecio / DivisaBs) : lastDivisa;
                        //decimal newCostoDivisa = lastCostoDivisa == 0 ? Convert.ToDecimal(lastPrecio / DivisaBs) : lastCostoDivisa;
                        //newDivisa = Convert.ToDecimal(string.Format("{0:n2}", newDivisa));
                        //newCostoDivisa = Convert.ToDecimal(string.Format("{0:n2}", newCostoDivisa));
                        //invenTableAdapter.UpdateQueryPorCodigo(newDivisa, newCostoDivisa, lastPrecio, descr, descr, descr, codigo);
                        //decimal newPrecio = newDivisa * DivisaBs;
                        //newPrecio = Convert.ToDecimal(string.Format("{0:n2}", newPrecio));
                        msjProgress(CalcularProgreso(modi, Registros), msjStatus: string.Format("Actualizados {0} de {1}", modi, Registros));
                    }
                }
                catch (Exception ex)
                {
                    ksslib.clsUtilErrors.Manejador_errores(ex, true);
                }
                finally
                {
                    this.gridFinal.PrimaryGrid.DataSource = !esModificadores ? (DataTable)dsPpal.invenc_usa : (DataTable)dsPpal.tmpmodifi;
                    btnActualizar.Enabled = true;
                    msjProgress(esFin: true);
                }

                ////Enlazar grid Secundario a bindingInvenUSA
                ////Rellenar Binding USA
                //invenTableAdapter.Fill(dsPpal.invenc);
                //string filtro = invenTableAdapter.Adapter.SelectCommand.CommandText.Replace("invenc", "invenc_usa");
                //inven_usaTableAdapter.ClearBeforeFill = true;

                //if (inven_usaTableAdapter.Adapter.SelectCommand == null)
                //    inven_usaTableAdapter.Adapter.SelectCommand = new OleDbCommand();

                //inven_usaTableAdapter.Adapter.SelectCommand.CommandText = filtro;
                //inven_usaTableAdapter.Fill(dsPpal.invenc_usa);
                //this.gridFinal.PrimaryGrid.DataSource = this.invenUsaBindingSource;
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
            }
        }
        void initDatosItem()
        {
            lastDivisa = 0;
            lastDivisaFactor = 0;
            lastPrecio = 0;
            lastCodigo = "";
        }
        void getDatosItem(DataRow item)
        {
            initDatosItem();
            lastDivisaFactor = item["divisafactor"].ToString().Trim().Length == 0 ? 0 : !Program.isInt(item["divisafactor"].ToString().Trim()) ? 0 : decimal.Round(Convert.ToDecimal(item["divisafactor"]), DecimalesCalculo, MidpointRounding.AwayFromZero);

            lastDescr = item["descr"].ToString();
            lastDivisa = decimal.Round(Convert.ToDecimal(item["Divisa"]), DecimalesCalculo);
            lastPrecio = decimal.Round(Convert.ToDecimal(item["Precio"]), DecimalesCalculo, MidpointRounding.AwayFromZero);
            lastCodigo = item["Codigo"].ToString();
        }

        private void btnProbar_Click(object sender, EventArgs e)
        {
            //Probar si existe conexion
            appDirConfig = Properties.Settings.Default.appDir_Datos;
            cnnString = Properties.Settings.Default.appCadenaConexionHuesped;
            ProbarConexion();
        }

        private void btnActualizar_Click(object sender, EventArgs e)
        {
            try
            {
                if (gridFinal.PrimaryGrid.Rows.Count > 0)
                {
                    if (
                         MessageBox.Show("" +
                               "Se van a MODIFICAR los datos." +
                               "\nEsta operación es probable NO PUEDE SER REVERTIDA." +
                               "\n¿Desea Continuar?",
                               "Cambio de Precios",
                               MessageBoxButtons.YesNo, MessageBoxIcon.Information) == DialogResult.Yes
                        )
                    {
                        msjProgress(1, "Actualizando Archivo BD...\nEspere por favor");
                        if (!esModificadores)
                        {
                            if (File.Exists(appDirConfig + "\\invenc_usa.dbf"))
                            {
                                File.Copy(appDirConfig + "\\invenc.dbf", appDirConfig + "\\invenc_Back.dbf", true);
                                File.Copy(appDirConfig + "\\invenc_usa.dbf", appDirConfig + "\\invenc.dbf", true);
                                msjProgress(esFin: true);
                            }
                            else
                            {
                                MessageBox.Show("Datos NO ACTUALIZADOS!/nIntente de nuevo.");
                            }
                        }
                        else
                        {
                            int modi = 0;
                            int registros = dsPpal.tmpmodifi.Rows.Count;
                            foreach (DataRow item in dsPpal.tmpmodifi.Rows)
                            {
                                getDatosItem(item);
                                modi += setUpdateModificadores();
                                msjProgress(CalcularProgreso(modi, registros), msjStatus: string.Format("Actualizados {0} de {1}", modi, registros));
                            }
                        };
                    }
                }
            }
            finally
            {
                msjProgress(esFin: true);
                ProbarConexion();
                btnActualizar.Enabled = false;
            }

        }

        #region METODOS
        private int guardarCambiosDBGrid()
        {
            int modi = 0;
            int rows = dsPpal.invenc.GetChanges().Rows.Count;
            msjProgress(1, "Actualizando Cambios Actuales...\nEspere por favor");
            foreach (System.Data.DataRow item in dsPpal.invenc.GetChanges().Rows)
            {
                decimal lastDivisa = Convert.ToDecimal(item["Divisa"]);
                decimal lastPrecio = Convert.ToDecimal(item["Precio"]);
                string codigo = item["codigo"].ToString();
                string descr = item["descr"].ToString();
                modi += invenTableAdapter.UpdateQueryPorCodigo(Math.Round(lastPrecio, 2), lastDivisa, descr, descr, descr, codigo);
                msjProgress(CalcularProgreso(modi, rows), msjStatus: string.Format("Actualizados {0} de {1}", modi, rows));
            }
            msjProgress(esFin: true);
            return modi;
        }
        #endregion

        #region CONFIG

        private void guardarDatos()
        {
            Program.guardarCnnString(cnnString);
            Properties.Settings.Default.LastDateUSe = encryptHelper.encryptus(DateTime.Now.ToString(), Program.KeyClave);
            Properties.Settings.Default.LastDivisa = Convert.ToDouble(txtDivisa.Text);
            Properties.Settings.Default.TipoRedondeo = (int)TipoRedondeo;
            Properties.Settings.Default.Save();
            loadDatos();
        }
        private void loadDatos()
        {
            cnnString = txtDirDatos.Text = Properties.Settings.Default.appCadenaConexionHuesped;
            lastDivisaFactor = Convert.ToDecimal(Properties.Settings.Default.LastDivisa);
            TipoRedondeo = (enTipoRedondeo)Properties.Settings.Default.TipoRedondeo;
            FactorDivisaBs = (decimal)Properties.Settings.Default.LastDivisa;
            codigoMoneda = Properties.Settings.Default.codigoMoneda;
        }


        private void guardadCnnString()
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStrings = config.ConnectionStrings;
            foreach (ConnectionStringSettings connectionString in connectionStrings.ConnectionStrings)
            {
                if (connectionString.Name == "RestTrump.Properties.Settings.cnnRestTrump")
                {
                    connectionString.ConnectionString = cnnString;
                }
            }
            config.Save(ConfigurationSaveMode.Modified);
        }
        #endregion

        #region Conexiones
        private void SaveDatos()
        {
            this.Validate();
            this.invenBindingSource.EndEdit();
            this.invenUsaBindingSource.EndEdit();
            this.bindingSourceModifi.EndEdit();
            this.tableAdapterManager.UpdateAll(this.dsPpal);
        }
        private bool ProbarConexion()
        {
            this.GridOriginal.PrimaryGrid.DataSource = null;
            this.gridFinal.PrimaryGrid.DataSource = null;
            try
            {
                OleDbCommand cmm = new OleDbCommand();
                string sqlFiltro =
                    @"SELECT  altoi, anchoi, areceta, barra, bloqueor, cantides, clasifi, codigo, codsaint AS divisafactor, colorf, descr, descrkp, fuentec, fuentef, fuentes, fuentet, fuentetop, grupo, lastsin, lastup, marca, maximo_ex, maximo_sus, medidae, 
                         nivel, nombre, peso, pidecanti, pidepre, precio, precio1, precio2 AS divisa, retorno, servi, titulo, tiva, unidad, unidadc, unidade
FROM            invenc";
                if (esConectado)
                {

                    string filtro = "";
                    if (esTieneFiltro && !esModificadores)
                    {
                        int i = 0;
                        sqlFiltro += " where {0} ";
                        foreach (CheckBox chk in gruposFiltrados)
                        {
                            if (chk.Checked)
                            {
                                if (i > 0)
                                    filtro += " or ";
                                filtro += "  (grupo like '" + chk.Tag.ToString() + "')";
                                i++;
                            }
                        }
                        cmm.CommandText = string.Format(sqlFiltro, filtro);
                        invenTableAdapter.Adapter.SelectCommand.CommandText = string.Format(sqlFiltro, filtro);
                    }
                    else
                    {
                        cmm.CommandText = "SELECT precio2 AS divisa,   grupo, codigo, descr, precio, tiva FROM invenc";
                        invenTableAdapter.Adapter.SelectCommand = cmm;

                    }

                    invenTableAdapter.Connection.ConnectionString = cnnString;
                    inven_usaTableAdapter.Connection.ConnectionString = cnnString;
                    taxTableAdapter.Connection.ConnectionString = cnnString;
                    monedasTableAdapter1.Connection.ConnectionString = cnnString;
                    grupoTableAdapter1.Connection.ConnectionString = cnnString;
                    tipo_empaTableAdapter1.Connection.ConnectionString = cnnString;
                    modifiTableAdapter1.Connection.ConnectionString = cnnString;

                    invenTableAdapter.Fill(dsPpal.invenc);
                    taxTableAdapter.Fill(dsPpal.tax);
                    monedasTableAdapter1.Fill(dsPpal.monedas);
                    grupoTableAdapter1.Fill(dsPpal.grupo);
                    tipo_empaTableAdapter1.Fill(dsPpal.tipo_empa);
                    modifiTableAdapter1.Fill(dsPpal.modifi);

                    if (tipo_empaTableAdapter1.GetDataDescrUNIDAD().Rows.Count >= 1)
                        tipoUnidad_Default = tipo_empaTableAdapter1.GetDataDescrUNIDAD().Rows[0]["codigo"].ToString();

                    lblNumRegistros.Text = dsPpal.invenc.Rows.Count.ToString() + " Registros";
                    crearArchivoEspejo();
                    this.GridOriginal.PrimaryGrid.DataSource = esModificadores ? this.bindingSourceModifi : this.invenBindingSource;
                    this.gridFinal.PrimaryGrid.DataSource = null;
                    return true;
                }
                else return false;
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
                return false;
            }
        }

        private void crearArchivoEspejo()
        {
            if (File.Exists(appDirConfig + "\\invenc.dbf"))
                File.Copy(appDirConfig + "\\invenc.dbf", appDirConfig + "\\invenc_usa.dbf", true);
        }
        #endregion

        private void txtDivisa_ValueChanged(object sender, EventArgs e)
        {
            DivisaBs = Convert.ToDecimal(txtDivisa.Text);
        }

        private void ProductosNuevos()
        {
            frmValor_Divisa frm = new frmValor_Divisa() { StartPosition = FormStartPosition.CenterParent };
            frm.ShowDialog(this);
            if (frm.DialogResult == DialogResult.Yes)
            {
                int modi = 0;
                int rows = invenTableAdapter.GetDataByProductosNuevos().Rows.Count;
                DivisaBs = (decimal)Properties.Settings.Default.LastDivisa;
                txtDivisa.Text = DivisaBs.ToString();
                msjProgress(1, "Actualizando DIVISA\nProductos Nuevos!!!");
                foreach (DataRow item in invenTableAdapter.GetDataByProductosNuevos())
                {
                    decimal lastDivisa = Convert.ToDecimal(string.Format("{0:N2}", item["divisa"]));
                    decimal lastPrecio = Convert.ToDecimal(string.Format("{0:N2}", item["Precio"]));
                    string codigo = item["codigo"].ToString();
                    string descr = item["descr"].ToString();



                    lastDivisa = Math.Round(lastPrecio / DivisaBs, 2);

                    modi += invenTableAdapter.UpdateQueryPorCodigo(Math.Round(lastPrecio, 2), lastDivisa, descr, descr, descr, codigo);
                    msjProgress(CalcularProgreso(modi, rows), msjStatus: string.Format("Actualizados {0} de {1}", modi, rows));
                }
                msjProgress(esFin: true);
            }
            else
            {
                MessageBox.Show("Aún existen valores de Divisa NULOS.");
            }
        }

        private int CalcularProgreso(int ini, int fin) => ini * 100 / fin;

        private void msjProgress(int Value = 0, string msjAccion = "", string msjStatus = "", bool esFin = false)
        {
            if (Value == 1)
                centrarDialogos(panelProgressBar);
            if (Value > 100)
                Value = 100;
            panelProgressBar.Visible = !esFin;
            if (esFin)
            {
                Cursor = Cursors.Default;
                progressBarX1.Value = 0;
                lblbarAccion.Text = "";
                lblbarStatusAccion.Text = "";
            }
            else
            {
                Cursor = Cursors.WaitCursor;
                progressBarX1.Value = Value;
                lblbarAccion.Text = msjAccion.Length == 0 ? lblbarAccion.Text : msjAccion;
                lblbarStatusAccion.Text = msjStatus.Length == 0 ? lblbarStatusAccion.Text : msjStatus;
            }
            Application.DoEvents();
        }

        #region Panel Buscar

        //Search
        private dsPpalTableAdapters.invencTableAdapter invenTableAdapterSrch;
        private dsPpalTableAdapters.modifiTableAdapter modifiTableAdapterSrch;
        private DataTable dtSrchEncontred;
        private int _PosSrch;
        private int _totalSrchEncontred;
        private bool esFiltrarTodos;

        private int totalSrchEncontred
        {
            get => _totalSrchEncontred;
            set
            {
                if (value <= 1)
                    panelMoveSearch.Visible = false;
                else
                    panelMoveSearch.Visible = true;
                _totalSrchEncontred = value;
            }
        }
        private int PosSrch
        {
            set
            {
                if (value > totalSrchEncontred)
                    value = totalSrchEncontred - 1;
                else if (totalSrchEncontred == 0)
                    value = 0;

                if (value < 0)
                    value = 0;
                _PosSrch = value;

                btnSrchNext.Enabled = true;
                btnSrchPrev.Enabled = true;
                if (value == 0)
                    btnSrchPrev.Enabled = false;
                if (value == (totalSrchEncontred - 1))
                    btnSrchNext.Enabled = false;
            }
            get => _PosSrch;
        }
        private bool estienefiltro;
        private int errorTry;
        private Control selectedControl;
        private clsCargarDialogControl cargarDialogControl;

        private int DecimalesCalculo;
        private decimal _lastPrecio;
        private decimal _lastdivisafactor;
        private decimal _lastdivisa;

        decimal lastDivisaFactor
        {
            get { return _lastdivisafactor == 0 ? 0.001m : _lastdivisafactor >= 5000 ? 5000 : _lastdivisafactor; }
            set { _lastdivisafactor = value; }
        }

        private enTipoRedondeo TipoRedondeo;
        private decimal _srchOLDlastPrecio;
        private decimal _srchOLDlastdivisa;
        private decimal _srchOLDlastdivisafactor;
        private string lastCodigo;
        private string tipoUnidad_Default = "";
        private string descrMoneda;
        private bool esModificadores;

        decimal lastDivisa
        {
            get { return _lastdivisa == 0 ? 0 : _lastdivisa >= 5000 ? 5000 : _lastdivisa; }
            set { _lastdivisa = value; }
        }

        private string lastDescr;
        private int statusSize;

        decimal lastPrecio
        {
            get => _lastPrecio;
            set
            {
                _lastPrecio = chkRedondeo.Checked ? AplicarRedondeo(value, TipoRedondeo) : value;
            }
        }
        private clsCargarDialogControl CargarDialogControl
        {
            get
            {
                if (cargarDialogControl == null)
                {
                    cargarDialogControl = new clsCargarDialogControl(this);
                    cargarDialogControl.DialogClosed += CargarDialogControl_DialogClosed;
                }
                return cargarDialogControl;
            }
        }

        private void CargarDialogControl_DialogClosed(object sender)
        {
            throw new NotImplementedException();
        }

        public bool esTieneFiltro
        {
            get => estienefiltro;
            private set
            {
                estienefiltro = value;
                this.btnFiltrar.Symbol = value ? "" : "";
            }
        }

        private decimal FactorDivisaBs { get; set; }

        private void InicializarSearch()
        {
            if (!esModificadores)
                invenTableAdapterSrch = new dsPpalTableAdapters.invencTableAdapter();
            else
                modifiTableAdapterSrch = new dsPpalTableAdapters.modifiTableAdapter();

            invenTableAdapterSrch = null;
            txtDescripcionSearch.Text = "";
            dtSrchEncontred = null;
            PosSrch = 0;
            totalSrchEncontred = 0;
            txtSearch.Text = "";
            lblSrchEncontrados.Text = "";
            txtDescripcionSearch.Text = "";
            txtSrchDivisa.Text = "0.00";
            txtSrchPrecio.Text = "0.00";
            txtCodigoBarras.Text = "";
            txtCodigoInterno.Text = "";
            chkImpuesto.Checked = false;
            chkInventario.Checked = false;
            chkLeeBalanza.Checked = false;
            chkPideCanti.Checked = false;
            chkPidePrecio.Checked = false;
            chkSubGrupo.Checked = false;
            cmbTipoEmpaque.SelectedIndex = -1;
            cmbTipoIVA.SelectedIndex = -1;
            cmbTipoUnidad.SelectedIndex = -1;
            cmbSrchGrupo.SelectedIndex = -1;

            lblSrchFactorDivisa.Text = lastDivisaFactor.ToString("N2");
            chkRedondeo.Checked = TipoRedondeo == enTipoRedondeo.Sin_Redondeo ? false : true;
            chkPrecioFijo.Checked = false;
            TipoRedondeo = (enTipoRedondeo)Properties.Settings.Default.TipoRedondeo;
            lblTipoRedondeo.Text = "Redondeo: " + TipoRedondeo.ToString().Replace("_", " ");
            btnEliminar.Visible = false;
            txtSearch.Focus();

        }
        private void buttonX2_Click(object sender, EventArgs e)
        {
            panelSearch.Visible = false;
            btnProbar_Click(null, null);
        }

        private void btnSearch_Click(object sender, EventArgs e)
        //{
            => CargarDialogControl.cargarDlgBackTransparent(panelSearch, true, true);
        //    centrarDialogos(panelSearch);
        //    panelSearch.Visible = true;
        //}


        private void btnSrchProdcuto_Click(object sender, EventArgs e)
        {
            try
            {
                if (!esModificadores)
                {
                    if (invenTableAdapterSrch == null)
                        invenTableAdapterSrch = new dsPpalTableAdapters.invencTableAdapter();
                }
                else
                    if (modifiTableAdapterSrch == null)
                    modifiTableAdapterSrch = new dsPpalTableAdapters.modifiTableAdapter();

                PosSrch = 0;
                dtSrchEncontred = new DataTable();
                string textoBuscar = "%" + txtSearch.Text.Trim() + "%";
                //Busqueda por codigo  
                dtSrchEncontred = esModificadores ? (DataTable)modifiTableAdapterSrch.GetDataByCodigo(textoBuscar) : (DataTable)invenTableAdapterSrch.GetDataByCodigo(textoBuscar);
                totalSrchEncontred = dtSrchEncontred.Rows.Count;

                //Busqueda por descripcion
                if (totalSrchEncontred == 0)
                {
                    dtSrchEncontred = esModificadores ? (DataTable)modifiTableAdapterSrch.GetDataByDescr(textoBuscar) : (DataTable)invenTableAdapterSrch.GetDataByDescr(textoBuscar.ToUpper());
                    totalSrchEncontred = dtSrchEncontred.Rows.Count;
                }
                if (totalSrchEncontred > 0)
                {
                    lblProductoNoExiste.Visible = false;
                    btnSrchPrev_Click(btnSrchPrev, new EventArgs());
                    txtSrchPrecio.Focus();
                }
                else
                {
                    InicializarSearch();
                    txtSearch.SelectAll();
                    lblProductoNoExiste.Visible = true;
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(string.Format("Ha ocurrido una excepcion: {0}", ex.Message), "Excepción");
            }
        }

        private void btnSrchPrev_Click(object sender, EventArgs e)
        {
            try
            {
                if (dtSrchEncontred != null)
                {
                    switch (((SymbolBox)sender).Tag)
                    {
                        case "1"://prev
                            PosSrch--;
                            break;
                        case "2"://next
                            PosSrch++;
                            break;
                    }
                    lblSrchEncontrados.Text = string.Format("{0}/{1}", PosSrch + 1, totalSrchEncontred);
                    DataRow rowEdit = dtSrchEncontred.Rows[PosSrch];
                    _srchOLDlastPrecio = Convert.ToDecimal(rowEdit["precio"]);
                    _srchOLDlastdivisa = Convert.ToDecimal(rowEdit["divisa"]);
                    _srchOLDlastdivisafactor = rowEdit["divisafactor"].ToString().Trim().Length == 0 ? 0 : Convert.ToDecimal(rowEdit["divisafactor"]);

                    lastPrecio = _srchOLDlastPrecio;
                    lastDivisa = _srchOLDlastdivisa;
                    if (lastPrecio > 0 && _srchOLDlastdivisa == 0)
                        setCalcularTotales(lastPrecio, 0);
                    else if (lastDivisa > 0)
                        setCalcularTotales(lastDivisa, 1);
                    if (!esModificadores)
                    {
                        mostrarCtlrsItemsmenu(true);
                        txtCodigoBarras.Text = rowEdit["barra"].ToString().Trim();
                        chkInventario.Checked = Convert.ToBoolean(rowEdit["areceta"]);
                        chkLeeBalanza.Checked = Convert.ToBoolean(rowEdit["peso"]);
                        chkImpuesto.Checked = Convert.ToBoolean(rowEdit["tiva"]); ;
                        chkPideCanti.Checked = Convert.ToBoolean(rowEdit["pidecanti"]);
                        chkPidePrecio.Checked = Convert.ToBoolean(rowEdit["pidepre"]);
                        chkSubGrupo.Checked = Convert.ToBoolean(rowEdit["nivel"]);
                        chkTroquelado.Checked = Convert.ToBoolean(rowEdit["clasifi"]);
                        txtUndEmpaque.Text = Convert.ToDecimal(rowEdit["unidade"]) == 0 ? "1,00" : Convert.ToDecimal(rowEdit["unidade"]).ToString("N2");
                        txtUndVenta.Text = (rowEdit["CANTIDES"].ToString()).Trim().Length == 0 ? "1,00" : Convert.ToDecimal(rowEdit["CANTIDES"]).ToString("N2");
                        cmbSrchGrupo.SelectedValue = rowEdit["grupo"].ToString().Trim();
                        cmbTipoEmpaque.SelectedValue = rowEdit["MEDIDAE"].ToString().Trim().Length == 0 ? tipoUnidad_Default : rowEdit["unidadc"].ToString().Trim();
                        cmbTipoUnidad.SelectedValue = rowEdit["UNIDADC"].ToString().Trim().Length == 0 ? tipoUnidad_Default : rowEdit["unidadv"].ToString().Trim();
                        cmbTipoIVA.SelectedValue = rowEdit["tiva"].ToString().Trim();

                        switch (Convert.ToInt16(rowEdit["retorno"]))
                        {
                            case 1:
                                radioButton1.Checked = true;
                                break;
                            case 2:
                                radioButton2.Checked = true;
                                break;
                            case 3:
                                radioButton3.Checked = true;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        mostrarCtlrsItemsmenu(false);
                    }

                    txtDescripcionSearch.Text = rowEdit["descr"].ToString().Trim();
                    txtSrchPrecio.Text = (lastDivisa * lastDivisaFactor).ToString("N2");
                    txtSrchDivisa.Text = lastDivisa.ToString("N2");
                    txtCodigoInterno.Text = rowEdit["codigo"].ToString().Trim();

                    txtSrchOldDivisa.Text = _srchOLDlastdivisa.ToString("N2");
                    txtSrchOldPrecio.Text = _srchOLDlastPrecio.ToString("N2");
                    txtsrchOldDivisaFactor.Text = _srchOLDlastdivisafactor.ToString("N2");

                    //btnEliminar.Visible = true;
                    txtSrchPrecio.Focus();
                    txtSrchPrecio.SelectAll();
                    lastCodigo = rowEdit["codigo"].ToString();
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(string.Format("Ha ocurrido una excepcion: {0}", ex.Message), "Excepción");
            }
        }

        private void mostrarCtlrsItemsmenu(bool v)
        {
            btnAddGrupos.Visible = v;
            txtCodigoBarras.Visible = v;
            chkSubGrupo.Visible = v;
            chkTroquelado.Visible = v;
            labelX22.Visible = v;
            labelX21.Visible = v;
            cmbSrchGrupo.Visible = v;
            panNotSubGrupo.Visible = v;

        }
        int setUpdateModificadores()
        {
            return modifiTableAdapter1.UpdateQuerybyCodigo(lastDescr, lastPrecio, lastDivisa, lastDivisaFactor.ToString(), lastCodigo);
            //MessageBox.Show("Datos Actualizados!", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);

        }
        private void btnAct_Click(object sender, EventArgs e)
        {
            //Actualizar datos
            try

            {
                if (sender != null)
                {
                    lastDescr = txtDescripcionSearch.Text.Trim().Length > 0 ? txtDescripcionSearch.Text : dtSrchEncontred.Rows[PosSrch]["descr"].ToString();
                    lastPrecio = Convert.ToDecimal(txtSrchPrecio.Text);
                    lastDivisa = Convert.ToDecimal(txtSrchDivisa.Text);
                    lastCodigo = txtCodigoInterno.Text;
                }
                if (!esModificadores)
                {
                    decimal altoi = 107;
                    decimal anchoi = 107;
                    decimal fuentec = 0;
                    string fuentef = "B";
                    decimal fuentes = 0;
                    string fuentet = "arial";
                    decimal fuentetop = 0;
                    decimal colorf = 11974326;
                    decimal areceta = Convert.ToInt16(chkInventario.Checked);
                    string barra = txtCodigoBarras.Text;
                    decimal clasifi = Convert.ToDecimal(chkTroquelado.Checked);
                    string descrkp = lastDescr;
                    string nombre = lastDescr;
                    string grupo = cmbSrchGrupo.SelectedValue.ToString();
                    string medidae = cmbTipoEmpaque.SelectedValue.ToString();
                    decimal nivel = Convert.ToDecimal(chkSubGrupo.Checked);
                    decimal peso = Convert.ToDecimal(chkLeeBalanza.Checked);
                    decimal pidecanti = Convert.ToInt16(chkPideCanti.Checked);
                    decimal pidepre = Convert.ToInt16(chkPidePrecio.Checked);
                    decimal tiva = Convert.ToInt16(cmbTipoIVA.SelectedValue);
                    string unidadc = cmbTipoUnidad.SelectedValue.ToString();
                    decimal cantides = Convert.ToDecimal(txtUndVenta.Text);
                    decimal unidade = Convert.ToDecimal(txtUndEmpaque.Text);
                    System.DateTime lastsin = new DateTime();
                    System.DateTime lastup = DateTime.Now;
                    decimal bloqueor = 0;
                    decimal marca = 0;
                    decimal maximo_ex = 0;
                    decimal maximo_sus = 0;
                    decimal precio1 = 0;
                    decimal retorno = 0;
                    decimal servi = 0;
                    decimal titulo = 1;
                    decimal unidad = 0;
                    if (invenTableAdapter.UpdateQueryModificado(altoi, anchoi, areceta, barra, bloqueor, cantides, clasifi, lastCodigo, lastDivisaFactor.ToString(), colorf, lastDescr, descrkp, fuentec, fuentef, fuentes, fuentet, fuentetop, grupo, lastsin, lastup, marca, maximo_ex, maximo_sus, medidae, nivel, nombre, peso, pidecanti, pidepre, lastPrecio, precio1, lastDivisa, retorno, servi, titulo, tiva, unidad, unidadc, unidade, lastCodigo) > 0)
                        MessageBox.Show("Datos Actualizados!", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                {
                    if (setUpdateModificadores() > 0)
                        MessageBox.Show("Datos Actualizados!", "Actualizado", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }

            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
            }
        }

        private void panelSearch_VisibleChanged(object sender, EventArgs e)
        {
            InicializarSearch();
        }


        #endregion

        #region Filtrar porGrupos
        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            centrarDialogos(panelFiltro);
            panelFiltro.Visible = true;
        }

        private void panelFiltro_VisibleChanged(object sender, EventArgs e)
        {
            if (panelFiltro.Visible)
            {
                flowGrupoItems.Controls.Clear();
                //Rellenar el flow con bd

                dsPpalTableAdapters.grupoTableAdapter grupoTableAdapter = new dsPpalTableAdapters.grupoTableAdapter();
                DataRowCollection drGrupo = grupoTableAdapter.GetData().Rows;

                if (gruposFiltrados == null)
                {
                    gruposFiltrados = new List<CheckBox>();
                    foreach (DataRow row in drGrupo)
                    {
                        CheckBox chk = new CheckBox();
                        chk.AutoSize = false;
                        chk.BackColor = System.Drawing.Color.WhiteSmoke;
                        chk.Location = new System.Drawing.Point(8, 8);
                        chk.Name = "chk_" + row["codigo"].ToString();
                        chk.Size = new System.Drawing.Size(115, 17);
                        chk.Text = row["nombre"].ToString().Trim();
                        chk.Tag = row["codigo"].ToString();
                        flowGrupoItems.Controls.Add(chk);
                        gruposFiltrados.Add(chk);
                    }
                }
                else
                    foreach (CheckBox chk in gruposFiltrados)
                    {
                        flowGrupoItems.Controls.Add(chk);
                    }
            }
        }

        private void btnFiltrarTodos_Click(object sender, EventArgs e)
        {
            esFiltrarTodos = !esFiltrarTodos;
            btnFiltrarTodos.Text = esFiltrarTodos ? "Ninguno" : "Todos";
            foreach (Control chk in flowGrupoItems.Controls)
            {
                if (chk is CheckBox)
                    ((CheckBox)chk).Checked = esFiltrarTodos;
            }
        }
        private void btnOKFiltrar_Click(object sender, EventArgs e)
        {
            if (!esModificadores)
            {
                esTieneFiltro = false;
                foreach (CheckBox chk in gruposFiltrados)
                {
                    if (chk.Checked)
                    {
                        esTieneFiltro = true;
                        break;
                    }
                }
            }
            ProbarConexion();
            panelFiltro.Visible = false;
        }
        #endregion
        private void txtSrchPrecio_KeyPress(object sender, KeyPressEventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (e.KeyChar.Equals('.'))
                e.KeyChar = ',';
            if (txt.Text.Contains(','))
            {
                if (!char.IsDigit(e.KeyChar))
                {
                    e.Handled = true;

                    if (txt.Text.Length > 0 && e.KeyChar.Equals(','))
                    {
                        int i = txt.Text.IndexOf(',');
                        txt.SelectionStart = i + 1;
                        txt.SelectionLength = txt.Text.Length - 1 - i;

                    }
                }
                if (e.KeyChar == '\b' || e.KeyChar == '\r' || e.KeyChar == '\u0003' || e.KeyChar == '\u0016')
                    e.Handled = false;
            }
            else
            {
                if (!char.IsDigit(e.KeyChar))
                    e.Handled = true;

                if (e.KeyChar == '\b' || e.KeyChar == '\r' || e.KeyChar == '\u0003' || e.KeyChar == '\u0016')
                    e.Handled = false;

                if (e.KeyChar == ',')
                {
                    if (txt.Text.Length == 0)
                    {
                        e.Handled = true;
                        txt.Text = "0,";
                        txt.SelectionStart = txt.Text.Length;
                    }
                    else
                    {
                        txt.SelectionStart = txt.TextLength;
                        e.Handled = false;
                    }

                }

            }
        }
        private void txtSrchPrecio_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{Tab}");
                    return;
                }
                dynamic txt = sender;
                txt.MaxLength = 18;
                char charActual = Convert.ToChar(e.KeyValue);
                if (txt.GetType() is TextBox)
                    if (txt.TextLength >= 19)
                        return;
                if (char.IsDigit(charActual) || charActual == '\b')
                {
                    string texto = txt.Text;
                    int posicion = txt.SelectionStart;
                    string decimales = "";
                    if (texto.Length > 0)
                    {
                        string[] BloquesIni = texto.Split('.');
                        string txttmp = texto;
                        if (texto.Contains(','))
                        {
                            txttmp = texto.Substring(0, texto.IndexOf(","));
                            decimales = texto.Substring(texto.IndexOf(","), texto.Length - texto.IndexOf(","));
                        }
                        texto = txttmp.Replace(".", "");
                        texto = Convert.ToDouble(texto).ToString("N0");
                        texto = texto + decimales;
                        txt.Text = texto;

                        txt.SelectionStart =
                            texto.Split('.').Length > BloquesIni.Length ? posicion + 1 :
                            texto.Split('.').Length < BloquesIni.Length ? posicion - 1 : posicion;
                    }
                }
                else
                    e.Handled = false;
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex, false);
            }

        }

        void setCalcularTotales(decimal Valor, int Opcion, bool esMargenFijo = false, bool esPrecioFijo = false, bool esPorLote = false)
        {
            if (Valor == 0)
            {
                lastPrecio = 0;
                lastDivisa = 0;
                return;
            }
            lastDivisaFactor = FactorDivisaBs;
            //if (Valor <= 0 && Opcion < 3)
            //    Valor = 0.10m;
            if (esPrecioFijo)
                chkPrecioFijo.Checked = esPrecioFijo;
            if (chkPrecioFijo.Checked)
                esPrecioFijo = true;
            switch (Opcion)
            {
                case 0://precio
                    lastDivisa = decimal.Round(Valor / lastDivisaFactor, DecimalesCalculo);
                    break;
                case 1://divisa
                    if (!chkPrecioFijo.Checked || !esPrecioFijo)
                        lastPrecio = decimal.Round(Valor * lastDivisaFactor, 2, MidpointRounding.AwayFromZero);

                    break;
            }

        }

        private void txtSrchPrecio_Leave(object sender, EventArgs e)
        {
            try
            {
                //if (totalSrchDatosEncontrados > 0)
                //{
                if (((TextBoxX)sender).Text.Trim().Length == 0)
                    ((TextBoxX)sender).Text = "0";

                lastPrecio = Convert.ToDecimal(txtSrchPrecio.Text);
                lastDivisa = Convert.ToDecimal(txtSrchDivisa.Text);
                switch (((TextBoxX)sender).Tag.ToString())
                {
                    case "0":
                        setCalcularTotales(lastPrecio, 0);
                        break;
                    case "1":
                        setCalcularTotales(lastDivisa, 1);
                        break;
                    default:
                        break;
                }
                //verificarConsistencias();
                txtSrchDivisa.Text = lastDivisa.ToString($"N{DecimalesCalculo}");
                txtSrchPrecio.Text = lastPrecio.ToString("N2");
            }
            catch (Exception)
            {

            }
        }
        private void verificarConsistencias()
        {
            if (FactorDivisaBs * lastDivisa != lastPrecio)
            {
                if (Properties.Settings.Default.esPrioridadPrecio)
                {
                    if (FactorDivisaBs * lastDivisa != lastPrecio)
                        lastDivisa = decimal.Round(lastPrecio / FactorDivisaBs, DecimalesCalculo);
                }
                else
                {
                    if (FactorDivisaBs / lastPrecio != FactorDivisaBs)
                        lastPrecio = decimal.Round(lastDivisa / FactorDivisaBs, 2, MidpointRounding.AwayFromZero);
                }
            }
        }

        private void buttonX3_Click(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Program.ConsultaMonitorve();
            Cursor = Cursors.Default;

        }

        private void centrarDialogos(Control control)
        {
            control.Location = new Point(this.Width / 2 - (control.Width / 2), this.Height / 2 - (control.Height / 2));
        }

        private void superGridControl1_EditorValueChanged(object sender, DevComponents.DotNetBar.SuperGrid.GridEditEventArgs e)
        {
            var etmp = e.GridCell.Value;
            e.Cancel = ksslib_c.Utiles.clsUtil_Strings.IsDouble(e.EditControl.EditorValue.ToString());
            if (!e.Cancel)
                e.EditControl.EditorValue = etmp;
        }

        private void PanelSearch_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SuperGridControl1_CellInfoDoubleClick(object sender, DevComponents.DotNetBar.SuperGrid.GridCellDoubleClickEventArgs e)
        {

            if (e.GridCell.GridColumn.DataPropertyName.ToUpper().Equals("DESCR"))
            {
                if (e.GridCell.Value != null)
                {
                    btnSearch.PerformClick();
                    txtSearch.Text = e.GridCell.Value.ToString().Trim();
                    btnSrchProdcuto_Click(null, null);
                }
            }
        }

        private void BtnEliminar_Click(object sender, EventArgs e)
        {
            string codigo = dtSrchEncontred.Rows[PosSrch]["codigo"].ToString();
            if (MessageBox.Show("Desea Eliminar el Producto Seleccionado", "Eliminar Producto", MessageBoxButtons.YesNo) == DialogResult.Yes)
                if (invenTableAdapter.DeleteQueryPorCodigo(codigo) > 0)
                    MessageBox.Show("Datos Eliminados!!!", "Eliminado", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void BtnNuevo_Click(object sender, EventArgs e)
        {

        }

        private void FrmMain_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Enter:
                    if (selectedControl == txtDivisa)
                        GridOriginal.Focus();
                    if (selectedControl == txtSearch)
                        btnSrchProdcuto_Click(null, null);
                    break;
                default:
                    break;
            }
        }
        private void txtCodigoBarras_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                SendKeys.Send("{Tab}");
                return;
            }
        }
        private void TxtSearch_Enter(object sender, EventArgs e)
        {
            selectedControl = txtSearch;
        }

        private void TxtDivisa_Enter(object sender, EventArgs e)
        {
            selectedControl = txtDivisa;

        }

        private void TxtCodigoInterno_Enter(object sender, EventArgs e)
        {
            selectedControl = (Control)sender;
        }

        private void ChkImpuesto_CheckedChanged(object sender, EventArgs e)
        {
            if (chkImpuesto.Checked)
            {
                cmbTipoIVA.Enabled = true;
                cmbTipoIVA.SelectedValue = 1;
            }
            else
            {
                cmbTipoIVA.Enabled = false;
                cmbTipoIVA.SelectedValue = 0;
            }
        }

        private void ChkSubGrupo_CheckedChanged(object sender, EventArgs e)
        {
            panNotSubGrupo.Enabled = !chkSubGrupo.Checked;

        }

        private void ChkInventario_CheckedChanged(object sender, EventArgs e)
        {
            panInventario.Enabled = chkInventario.Checked;
        }

        private void ChkTroquelado_CheckedChanged(object sender, EventArgs e)
        {
            chkPrecioFijo.Checked = chkTroquelado.Checked;

        }

        private void TxtSrchPrecio_KeyUp_1(object sender, KeyEventArgs e)
        {
            try
            {
                if (e.KeyCode == Keys.Enter)
                {
                    SendKeys.Send("{Tab}");
                    return;
                }
                dynamic txt = sender;
                txt.MaxLength = 18;
                char charActual = Convert.ToChar(e.KeyValue);
                if (txt.GetType() is TextBox)
                    if (txt.TextLength >= 19)
                        return;
                if (char.IsDigit(charActual) || charActual == '\b')
                {
                    string texto = txt.Text;
                    int posicion = txt.SelectionStart;
                    string decimales = "";
                    if (texto.Length > 0)
                    {
                        string[] BloquesIni = texto.Split('.');
                        string txttmp = texto;
                        if (texto.Contains(','))
                        {
                            txttmp = texto.Substring(0, texto.IndexOf(","));
                            decimales = texto.Substring(texto.IndexOf(","), texto.Length - texto.IndexOf(","));
                        }
                        texto = txttmp.Replace(".", "");
                        texto = Convert.ToDouble(texto).ToString("N0");
                        texto = texto + decimales;
                        txt.Text = texto;

                        txt.SelectionStart =
                            texto.Split('.').Length > BloquesIni.Length ? posicion + 1 :
                            texto.Split('.').Length < BloquesIni.Length ? posicion - 1 : posicion;
                    }
                }
                else
                    e.Handled = false;
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex, false);
            }
        }

        private void txtDivisa_ButtonCustomClick(object sender, EventArgs e)
        {
            DataRowCollection rows = monedasTableAdapter1.GetDataBy1(codigoMoneda).Rows;
            if (rows.Count > 0)
            {
                txtDivisa.Text = Convert.ToString(rows[0]["Factor"]);
                descMoneda();
            }
            rows = null;
        }

        private void descMoneda()
        {
            DataRowCollection rows = monedasTableAdapter1.GetDataBy1(codigoMoneda).Rows;
            if (rows.Count > 0)
                descrMoneda = rows[0]["descr"].ToString().Trim().ToUpper() + " Bs." + rows[0]["Factor"].ToString();

        }

        private void txtDivisa_ButtonCustom2Click(object sender, EventArgs e)
        {
            try
            {
                int i = 00;
                if (MessageBox.Show($"Se va a modificar la Moneda:\n{descrMoneda}\n¿Desea Continuar?", "Modificando...", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                    i = monedasTableAdapter1.UpdateQuery(Convert.ToDecimal(txtDivisa.Text), codigoMoneda);
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex, true);
            }

        }

        private void rdmodificadores_CheckedChanged(object sender, EventArgs e)
        {
            flowGrupoItems.Visible = !((RadioButton)sender).Checked;
            btnFiltrarTodos.Visible = flowGrupoItems.Visible;
            esModificadores = rdmodificadores.Checked;
        }

        private void btnMinimize_Click(object sender, EventArgs e)
        {
            this.Location = new Point(0, 0);
            if (statusSize == 0)
            {
                statusSize = 1;
                btnMinimize.Symbol = "";
                WindowState = FormWindowState.Normal;
                this.Size = new Size(50, 50);
            }
            else
            {
                btnMinimize.Symbol = "";
                WindowState = FormWindowState.Maximized;
                statusSize = 0;
            }
        }

        private void buttonX4_Click(object sender, EventArgs e)
        {
            new frmConfig()
            {
                StartPosition = FormStartPosition.CenterParent
            }.ShowDialog(this);

            loadDatos();
            ProbarConexion();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.dpana.com.ve");
        }
    }


}
