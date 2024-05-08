using ksslib;
using
    System;
using System.Windows.Forms;

namespace RestTrump
{
    public partial class frmSerial : Form
    {
        private string codigoActivacion;
        private string _Serial;
        int pos1 = 0;
        int pos2 = 0;
        int pos3 = 0;
        private string Serial
        {
            get
            {
                try
                {
                _Serial =
                       ((DateTime.Now.Ticks * DateTime.Now.Millisecond / 2) * (2020)).ToString().Substring(0, 5) + "-" +
                       ((DateTime.Now.Ticks * DateTime.Now.Second / 4) * (2020)).ToString().Substring(3, 5) + "-" +
                       ((DateTime.Now.Ticks * DateTime.Now.Minute / 6) * (2020)).ToString().Substring(4, 5);
                if (_Serial.Length < 17 || _Serial.StartsWith("-"))
                    return Serial;

                }
                catch (Exception)
                {
                    return Serial;
                }
                return _Serial;
            }

        }

        public string serialAnterior { get; internal set; }

        public frmSerial()
        {
            InitializeComponent();
        }

        private void button2_Click(object sender, EventArgs e)
        {

            try
            {

                string[] arrSerial = _Serial.Split('-');
                int Comodin = lblPosiciones.Text.Substring(lblPosiciones.Text.Length - 1, 1) == "0" ? 4 : Convert.ToInt32(lblPosiciones.Text.Substring(lblPosiciones.Text.Length - 1, 1));
                int strpos1 = Convert.ToInt32(arrSerial[0].ToString().Substring(pos1, pos1 < 4 ? 2 : 1)) * Comodin;
                int strpos2 = Convert.ToInt32(arrSerial[1].ToString().Substring(pos2, pos2 < 4 ? 2 : 1)) * Comodin;
                int strpos3 = Convert.ToInt32(arrSerial[2].ToString().Substring(pos3, pos3 < 4 ? 2 : 1)) * Comodin;
                codigoActivacion = strpos1.ToString() + strpos2.ToString() + strpos3.ToString();

                if (textBox1.Text.Equals(codigoActivacion))
                {
                    DialogResult = DialogResult.Yes;
                    kss_msjDelay.Show("Hemos Activado el <b>MODO FULL</b>" +
                           "\n<b>Gracias por preferirnos!.</b>" +
                           "\nPronto estaremos Trabajando con <b>DELIVERY WEB</b>" +
                           "\nPregunta como funciona\n",
                           enuMsgBoxImag.msgInformacion, enuMsgBoxResult.msgOK, 50, "Disfrute de Nuestra Versión FULL");
                    //crear archivo
                    SettingsManager.AddUpdateAppSettings("serialActivacion", encryptHelper.encryptus(_Serial.ToString(), Program.KeyClave));
                    Program.nUsos = 0;
                    Program.LastDate = DateTime.Now;
                    Program.LastDateUSe = DateTime.Now;
                    Program.esLicenciaDemo = false;
                    Program.isActive = true;

                }
                else
                {
                    if (Program.maxUsos >= 50)
                    {
                        kss_msjDelay.Show("Ya disfrutó del <b>MODO DEMO?</b>" +
                           "\nPorfavor Contacte al Teléfono 04143662876 " +
                           "\npara seguir disfrutando de la Aplicación." +
                           "\n<b>KALIXTO SYSTEMS SOLUTIONS</b>\n",
                           enuMsgBoxImag.msgInformacion, enuMsgBoxResult.msgOK, 50, "Solicite su <b>Versión FULL</b>");
                    }
                    else
                    {
                        if (kss_msjDelay.Show("Codigo Inválido.\n¿Desea Activar el <b>MODO DEMO?</b>\n\n",
                            enuMsgBoxImag.msgInformacion, enuMsgBoxResult.msgYesNO, 50, "Inválido") == DialogResult.No)
                        {
                            DialogResult = DialogResult.Cancel;
                            Program.nUsos = 1000;
                            Program.LastDate = DateTime.Now;
                            Program.LastDateUSe = DateTime.Now;
                            Program.esLicenciaDemo = false;
                            Program.isActive = false;
                        }
                        else
                        {
                            DialogResult = DialogResult.Yes;
                            kss_msjDelay.Show("Hemos Activado el <b>MODO DEMO</b>" +
                                "\nLe Recomendamos Aquirir la Licencia Completa." +
                                "\nEn Esta Versión <b>Limitada</b> solo podrá:" +
                                "\n<b>-</b>Modificar Registros en Catidades limitadas." +
                                "\n<b>-</b>Transferir a Numero Limitado de Estaciones" +
                                "\n<b>-</b>Tiene la oportunidad de Usarlo solo 5 veces" +
                                "\nPronto estaremos Trabajando con <b>DELIVERY WEB</b>" +
                                "\n<b> KALIXTO SYSTEMS SOLUTIONS </b>\n\n"
                                , enuMsgBoxImag.msgCuidado, enuMsgBoxResult.msgOK, 20, "Disfrute de Nuestra Versión DEMO");

                            Program.nUsos = 0;
                            Program.maxUsos = 50;
                            Program.maxActualizaciones = 20;
                            Program.LastDate = DateTime.Now;
                            Program.LastDateUSe = DateTime.Now;
                            Program.esLicenciaDemo = true;
                            Program.isActive = false;
                        }

                    }
                }
                Properties.Settings.Default.Save();
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
            }
            finally
            {
                Close();
            }
        }

        private void frmSerial_Load(object sender, EventArgs e)
        {
            clsUtilForms _clsUtilForm = new ksslib.clsUtilForms(this);
            base.MouseMove += _clsUtilForm.Me_MouseMove;
            base.MouseDown += _clsUtilForm.Me_MouseDown;
            this.label3.MouseMove += _clsUtilForm.Me_MouseMove;
            this.label3.MouseDown += _clsUtilForm.Me_MouseDown;

            Random rnd = new Random();
            label1.Text = Serial;
            lblSerialAnterior.Text = serialAnterior;
            pos1 = rnd.Next(4);
            pos2 = rnd.Next(4);
            while (pos3==0)
            {
                pos3 = rnd.Next(4);
            }
            lblPosiciones.Text = pos1.ToString() + pos2.ToString() + pos3.ToString();
            lblSerialHDD.Text = Program.GetHardDiskSerialNo();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
