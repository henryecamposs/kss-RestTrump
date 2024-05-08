using Newtonsoft.Json;
using System;
using System.Data;
using System.Linq;
using System.Net;
using System.Windows.Forms;

namespace RestTrump
{
    public partial class frmValor_Divisa : Form
    {
        decimal DivisaBs;
        public frmValor_Divisa()
        {
            InitializeComponent();
        }
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

        private void frmValor_Divisa_Load(object sender, EventArgs e)
        {
            DivisaBs = (decimal)Properties.Settings.Default.LastDivisa;
            txtDivisa.Text =  DivisaBs.ToString();
            Text = "indique último factor cambio." ;
        }
        private static DataSet ReadDataFromJson(string jsonString, XmlReadMode mode = XmlReadMode.Auto)
        {
            //// Note:Json convertor needs a json with one node as root
            jsonString = "{ \"rootNode\": {" + jsonString.Trim().TrimStart('{').TrimEnd('}') + @"} }";
            //// Now it is secure that we have always a Json with one node as root 
            var xd = JsonConvert.DeserializeXmlNode(jsonString);

            //// DataSet is able to read from XML and return a proper DataSet
            var result = new DataSet();
            result.ReadXml(new System.Xml.XmlNodeReader(xd), mode);
            return result;
        }
        
        private void btnSalir_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Se va a actualizar el precio en divisas del producto a un valor de {txtDivisa.Text} Bs.", "Actualizando...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                Properties.Settings.Default.LastDivisa = Convert.ToDouble(txtDivisa.Text);
                DialogResult = DialogResult.Yes;
                this.Close();
            }
        }

        private void buttonX1_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Desea cancelar la actualización de divisas para nuevos productos?", "Cancelando...", MessageBoxButtons.YesNo, MessageBoxIcon.Warning) == DialogResult.Yes)
            {
                DialogResult = DialogResult.Cancel;
                this.Close();
            }
        }

        private void buttonX2_Click_1(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor;
            Program.ConsultaMonitorve();
            Cursor = Cursors.Default;
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
