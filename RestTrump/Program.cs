using ksslib;
using ksslib_c;
using ksslib_c.Utiles;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Management;
using System.Net;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Windows.Media;
using vPOS;

namespace RestTrump
{
    static class Program
    {
        internal static DateTime LastDate;
        internal static DateTime LastDateUSe;
        internal static bool isActive;
        internal static string KeyClave;
        public static clsAppInfo AppInfo;
        internal static string Usuarioinstagram;
        internal static string BuscarPor;
        internal static bool esLicenciaDemo;
        internal static DateTime fechaLimite;
        internal static int nUsos = 0;
        internal static int maxUsos = 0;
        internal static int maxActualizaciones = 0;
        private static bool esSQLServer;

        /// </summary>


        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            AppInfo = new clsAppInfo(enuPaisesLenguaje.Es_Venezuela);

            try
            {
                Thread.CurrentThread.Name = "GUI Thread";
                AppDomain domain = Thread.GetDomain();
                domain.UnhandledException += ManejadorDeErroresGraves;
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Usuarioinstagram = Properties.Settings.Default.UsuarioInstagram;
                BuscarPor = Properties.Settings.Default.StartWith;

                KeyClave = GetHardDiskSerialNo();
                LastDate = SettingsManager.ReadSettingDateTime("LastDate", new DateTime(2020, 01, 01), KeyClave);
                LastDateUSe = SettingsManager.ReadSettingDateTime("LastDateUSe", DateTime.Now, KeyClave);
                isActive = SettingsManager.ReadSettingBoolean("isActive", false, KeyClave);
                esLicenciaDemo = SettingsManager.ReadSettingBoolean("esLicenciaDemo", true, KeyClave);
                nUsos = SettingsManager.ReadSettingInt("Usos", 1000, KeyClave);
                maxUsos = SettingsManager.ReadSettingInt("maxUsos", 50, KeyClave);
                maxActualizaciones = SettingsManager.ReadSettingInt("maxActualizaciones", 20, KeyClave);
                fechaLimite = esLicenciaDemo ? new DateTime(2021, 02, 01) : new DateTime(2024, 05, 02);
                isActive = isActive && LastDateUSe <= fechaLimite && !esLicenciaDemo ? true : false;
                esLicenciaDemo = esLicenciaDemo && nUsos <= 5 && DateTime.Now <= fechaLimite && maxUsos <= 50 ? true : false;
                if (LastDate.Date <= LastDateUSe.Date)
                {
                    if (DateTime.Now < fechaLimite)
                        if (isActive || esLicenciaDemo)
                        //if (OK.Equals(KeyClave))
                        {
                            nUsos++;
                            saveData();
                            Application.Run(new frmMain());
                            saveData();
                        }
                    string serialAnterior = SettingsManager.ReadSetting("keyclave", "");
                    if (serialAnterior.Length != 0 && LastDate.Date != new DateTime(2020, 01, 01))
                        if (!KeyClave.Equals(serialAnterior))
                        {
                            MessageBox.Show($"HDD es inválido:\nSerial Actual:{KeyClave}\n Serial Anterior:{serialAnterior}");
                        }

                    if (new frmSerial() { serialAnterior = serialAnterior, StartPosition = FormStartPosition.CenterScreen }.ShowDialog() == DialogResult.Yes)
                    {
                        nUsos++;
                        if (!isActive)
                            LastDate = LastDateUSe;
                        saveData();
                        Application.Run(new frmMain());
                        saveData();
                    }
                }
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex);
            }
            finally
            {

            }
        }

        private static void ManejadorDeErroresGraves(object sender, UnhandledExceptionEventArgs args)
        {
            Exception ex = (Exception)args.ExceptionObject;
            MethodBase targetSite = ex.TargetSite;
            if (!(targetSite == null))
            {
                _ = targetSite.Name;
            }
            MessageBox.Show(Application.OpenForms[Application.OpenForms.Count - 1], "Detalles del Error: \n\"" + ex.ToString() + "\"\n\nLa aplicación se cerrará.", "Error fatal en Fiscalizador:");
            Logger.Log("Error fatal en Fiscalizador: \nDetalles del Error: \n\"" + ex.ToString() + "\"\n");
            Logger.LogEnd();
            Process.GetCurrentProcess().Kill();
        }

        private static void saveData()
        {
            SettingsManager.AddUpdateAppSettings("LastDate", LastDate.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("LastDateUSe", DateTime.Now.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("isActive", isActive.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("esLicenciaDemo", esLicenciaDemo.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("Usos", nUsos.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("MaxUsos", maxUsos.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("MaxActualizaciones", maxActualizaciones.ToString(), KeyClave);
            SettingsManager.AddUpdateAppSettings("KeyClave", KeyClave);
        }
        internal static bool isInt(string strInt)
        {
            try
            {

                int mvInt = int.Parse(strInt);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isDate(string strDate)
        {
            try
            {
                DateTime myDateTime = DateTime.Parse(strDate);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool isBool(string str)
        {
            try
            {
                bool myDateTime = Convert.ToBoolean(str);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static string GetHardDiskSerialNo()
        {
            ManagementClass mangnmt = new ManagementClass("Win32_LogicalDisk");
            ManagementObjectCollection mcol = mangnmt.GetInstances();
            string result = "";
            foreach (ManagementObject strt in mcol)
            {
                if (strt["DeviceID"].Equals("C:"))
                    result += Convert.ToString(strt["VolumeSerialNumber"]);
            }
            return result;
        }
        #region instagram

        /// <summary>
        /// muestra las publicaciones del usuario instagram
        /// </summary>
        /// <param name="jsonString"></param>
        /// <param name="mode"></param>
        private static void loadfeedInstagram(string jsonString, XmlReadMode mode = XmlReadMode.Auto)
        {
            try
            {
                FeedInstagram feedInstagram = new FeedInstagram(jsonString, mode);
                //Load Image
                if (feedInstagram != null)
                {
                    frmMonitorvlaView frm = new frmMonitorvlaView(feedInstagram.FeedsCollection);
                    frm.StartPosition = FormStartPosition.CenterScreen;
                    frm.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex);
            }
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
        public static void ConsultaMonitorve()
        {
            using (WebClient wc = new WebClient())
            {
                try
                {
                    //var json = wc.DownloadString("https://s3.amazonaws.com/dolartoday/data.json");
                    //var dataset = ReadDataFromJson(json, XmlReadMode.InferTypedSchema);
                    //DataTable USD = dataset.Tables["USD"];
                    //DataTable TiME = dataset.Tables["_timestamp"];
                    //string sDolarToday = "DolarToday: " + string.Format("{0:N2}", USD.Rows[0]["dolartoday"]);
                    //string sTime = TiME.Rows[0]["fecha"].ToString();

                    //string sSicad = "SICAD: " + string.Format("{0:N2}", USD.Rows[0]["sicad2"]);
                    //string sPetro = " Petro: " + string.Format("{0:N2}", (Convert.ToDouble(USD.Rows[0]["sicad2"]) * 50));
                    //string result = $"{sTime} - {sDolarToday}" + $" - {sSicad}" + $" - {sPetro}";
                    //usuario = usuario.Length == 0 ? Usuarioinstagram : usuario;
                    var json = wc.DownloadString($"https://www.instagram.com/{Usuarioinstagram}/?__a=1");
                    loadfeedInstagram(json, XmlReadMode.InferTypedSchema);
                    //return Text = result;
                }
                catch (Exception ex)
                {
                    if (ex.GetType() == typeof(WebException))
                        ksslib.clsUtilErrors.Manejador_errores(null, true, $"No se puede conectar a {Usuarioinstagram}");
                    else
                        ksslib.clsUtilErrors.Manejador_errores(ex);
                }
            }
        }
        #endregion

        public static List<string> WrapText(string text, double pixels, string fontFamily, float emSize)
        {
            string[] originalWords = text.Split(new string[] { " " },
                StringSplitOptions.None);

            List<string> wrappedLines = new List<string>();

            StringBuilder actualLine = new StringBuilder();
            double actualWidth = 0;

            foreach (string word in originalWords)
            {
                string wordWithSpace = word + " ";
                FormattedText formattedWord = new FormattedText(wordWithSpace,
                    CultureInfo.CurrentCulture,
                    System.Windows.FlowDirection.LeftToRight,
                    new Typeface(fontFamily), emSize, System.Windows.Media.Brushes.Black);

                actualLine.Append(wordWithSpace);
                actualWidth += formattedWord.Width;

                if (actualWidth > pixels)
                {
                    actualLine.Remove(actualLine.Length - wordWithSpace.Length, wordWithSpace.Length);
                    wrappedLines.Add(actualLine.ToString());
                    actualLine = new StringBuilder();
                    actualLine.Append(wordWithSpace);
                    actualWidth = 0;
                    actualWidth += formattedWord.Width;
                }
            }

            if (actualLine.Length > 0)
                wrappedLines.Add(actualLine.ToString());

            return wrappedLines;
        }
        public static bool DatosConfig_ConexionBD(bool esReiniciar, string cnnStringName = "")
        {
            DialogResult frmResult = DialogResult.Cancel;
            try
            {
                if (esSQLServer)
                {
                    frmConection_SQLServer SQLfrm = new frmConection_SQLServer(AppInfo) { StartPosition = FormStartPosition.CenterScreen };
                    SQLfrm.cnnStringName = cnnStringName;

                    if ((frmResult = SQLfrm.ShowDialog()) == DialogResult.OK)
                    {
                        SqlConnectionStringBuilder cnnConexionHuespedBuild = SQLfrm.cnnStringBuilder;
                        Properties.Settings.Default.appCnnStringName = SQLfrm.cnnStringName;
                        Properties.Settings.Default.appCadenaConexionHuesped = cnnConexionHuespedBuild.ConnectionString.ToString();
                        Properties.Settings.Default.BaseDatosHuespedBD = cnnConexionHuespedBuild.InitialCatalog;
                        Properties.Settings.Default.PasswordHuespedDB = cnnConexionHuespedBuild.Password;
                        Properties.Settings.Default.UsuarioHuespedBD = cnnConexionHuespedBuild.UserID;
                        Properties.Settings.Default.NombreHuespedBD = cnnConexionHuespedBuild.DataSource;
                        guardarCnnString(Properties.Settings.Default.appCadenaConexionHuesped);
                        Properties.Settings.Default.Save();
                        return true;
                    }
                    return false;
                }
                else
                {
                    frmConection_VFoxPro DBFfrm = new frmConection_VFoxPro(AppInfo);
                    DBFfrm.cnnStringName = cnnStringName;
                    DBFfrm.esServidor = Properties.Settings.Default.esServidorBD;
                    if ((frmResult = DBFfrm.ShowDialog()) == DialogResult.OK)
                    {
                        Properties.Settings.Default.appCnnStringName = DBFfrm.cnnStringName;
                        Properties.Settings.Default.esServidorBD = DBFfrm.swServidor.Value;
                        Properties.Settings.Default.appCadenaConexionHuesped = DBFfrm.cnnStringBuilder.ToString();
                        Properties.Settings.Default.appDirDatosHostServer = DBFfrm.cnnStringBuilder.DataSource;
                        Properties.Settings.Default.appDir_Datos = Properties.Settings.Default.appDirDatosHostServer;
                        Properties.Settings.Default.appDirDatosHost = Properties.Settings.Default.appDir_Datos;
                        Properties.Settings.Default.appDirDatosHuesped = DBFfrm.swServidor.Value ? Properties.Settings.Default.appDirDatosHostServer : Properties.Settings.Default.appDir_Datos;
                        Properties.Settings.Default.appDirDatosHuespedServer = Properties.Settings.Default.appDirDatosHostServer;
                        System.Data.OleDb.OleDbConnectionStringBuilder cnnConexionHostBuild = DBFfrm.cnnStringBuilder;

                        //if (!Properties.Settings.Default.esServidorBD)
                        //{
                        //    Properties.Settings.Default.appCadenaConexionHuespedServer = cnnConexionHostBuild.ConnectionString.Replace(Path.GetDirectoryName(cnnConexionHostBuild.ConnectionString), Properties.Settings.Default.appDirDatosHuesped).ToString();
                        //}
                        //Properties.Settings.Default.appDir_Datos = Path.GetDirectoryName(cnnConexionHostBuild.ConnectionString);
                  
                        guardarCnnString(Properties.Settings.Default.appCadenaConexionHuesped);
                        return Program.esConexionconBD<System.Data.OleDb.OleDbConnection>(Properties.Settings.Default.appCadenaConexionHuesped);
                    }
                    return false;
                }
            }
            catch (System.Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex);
                return false;
            }
            finally
            {
                if (frmResult == DialogResult.OK && esReiniciar)
                    KssCodigo.ReiniciarAplicacion();
            }
        }
        public static Decimal AplicarRedondeo(decimal Valor, enTipoRedondeo tipoRedondeo)
        {
            decimal result = 0;
            Valor = Math.Round(Valor, MidpointRounding.AwayFromZero);
            if (Valor == 0)
                return result;
            ///////Obtener las unidades,decenas,centenas////////	
            int numero = Convert.ToInt32(Valor);
            int millares = numero / 1000;
            int centenas = (numero - (millares * 1000)) / 100;
            int decenas = (numero - (millares * 1000 + centenas * 100)) / 10;
            int unidades = numero - (millares * 1000 + centenas * 100 + decenas * 10);


            switch (tipoRedondeo)
            {
                case enTipoRedondeo.Sin_Redondeo:
                    return Valor;
                case enTipoRedondeo.Unidades_a_Cero:
                    if (unidades > 0 && Valor > 10)
                        unidades = 0;
                    break;
                case enTipoRedondeo.Unidades_a_Decimas:
                    if (unidades > 0)
                    {
                        unidades = 0;
                        decenas++;
                    }
                    break;
                case enTipoRedondeo.Cinco_Unidades_a_Decimas:
                    if (unidades >= 5)
                    {
                        unidades = 0;
                        decenas++;
                    }
                    break;
                case enTipoRedondeo.Unidades_a_Centesimas:
                    if (unidades >= 1 && Valor >= 10)
                    {
                        unidades = 0;
                        decenas = 0;
                        centenas++;
                    }
                    break;
                case enTipoRedondeo.Decimas_a_Cero:
                    if (decenas > 0 && Valor >= 10)
                    {
                        unidades = 0;
                        decenas = 0;
                    }
                    break;
                case enTipoRedondeo.Decimas_a_Centesimas:
                    if ((unidades > 0 || decenas > 0) && Valor >= 10)
                    {
                        unidades = 0;
                        decenas = 0;
                        centenas++;
                    }
                    break;
                case enTipoRedondeo.Cinco_Decimas_a_Centesimas:
                    if (decenas >= 50)
                    {
                        decenas = 0;
                        centenas++;
                    }
                    break;
                case enTipoRedondeo.Centesimas_a_Cero:
                    if (centenas > 0)
                    {
                        unidades = 0;
                        decenas = 0;
                        centenas = 0;
                    }
                    break;
                case enTipoRedondeo.CincoCentesimas_a_Milesimas:
                    if (centenas >= 5)
                    {
                        unidades = 0;
                        decenas = 0;
                        centenas = 10;
                    }
                    break;
                case enTipoRedondeo.Centesimas_a_Milesimas:
                    if (centenas > 0)
                    {
                        unidades = 0;
                        decenas = 0;
                        centenas = 10;
                    }
                    break;
                default:
                    break;
            }
            int valorUltimo = (millares * 1000) + (centenas * 100) + (decenas * 10) + (unidades);
            result = Convert.ToDecimal(valorUltimo);
            return result;
        }
        public enum enTipoRedondeo : int
        {
            Sin_Redondeo,
            Unidades_a_Cero,
            Cinco_Unidades_a_Decimas,
            Unidades_a_Decimas,
            Unidades_a_Centesimas,
            Decimas_a_Cero,
            Cinco_Decimas_a_Centesimas,
            Decimas_a_Centesimas,
            Centesimas_a_Cero,
            CincoCentesimas_a_Milesimas,
            Centesimas_a_Milesimas,
        }
        public static bool esConexionconBD<T>(string cnnString) where T : IDbConnection, new()
        {
            try
            {
                using (var conn = new T())
                {
                    conn.ConnectionString = cnnString;
                    conn.Open();
                    return true;
                }
            }
            catch (Exception ex)
            {
                ksslib.clsUtilErrors.Manejador_errores(ex);
                MessageBox.Show("" +
                       "Error al Intentar Conectarse a la Base de datos: " + cnnString + "\nVerifique la dirección e intente de nuevo!",
                       "Intento Conexión fallido",
                       MessageBoxButtons.OK, MessageBoxIcon.Error);
                if (DatosConfig_ConexionBD(false))
                    return true;
                return false;
            }
        }
        public static void guardarCnnString(string cnnString)
        {
            var config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            var connectionStrings = config.ConnectionStrings;
            foreach (ConnectionStringSettings connectionString in connectionStrings.ConnectionStrings)
            {
                if (!esSQLServer)
                {
                    if (connectionString.Name == "RestTrump.Properties.Settings.cnnRestTrump")
                        connectionString.ConnectionString = cnnString;
                }
                else
                {
                    if (connectionString.Name == "RestTrump.Properties.Settings.cnnRestTrump_SQLServer")
                        connectionString.ConnectionString = cnnString;
                }
            }
            config.Save(ConfigurationSaveMode.Modified);
        }
    }
    internal static class Logger
    {
        public static void Log(string e)
        {
            using StreamWriter streamWriter = new StreamWriter("ErrorLog.log", append: true, Encoding.GetEncoding("ISO-8859-1"));
            streamWriter.WriteLine($"{DateTime.Now} {e}");
            streamWriter.Close();
        }

        public static void LogStart()
        {
            using StreamWriter streamWriter = new StreamWriter("ErrorLog.log", append: true, Encoding.GetEncoding("ISO-8859-1"));
            streamWriter.WriteLine($"{DateTime.Now} ********** App Start **********");
            streamWriter.Close();
        }

        public static void LogEnd()
        {
            using StreamWriter streamWriter = new StreamWriter("ErrorLog.log", append: true, Encoding.GetEncoding("ISO-8859-1"));
            streamWriter.WriteLine($"{DateTime.Now} ********** App Closed ********** \n\n\n");
            streamWriter.Close();
        }
    }
}
