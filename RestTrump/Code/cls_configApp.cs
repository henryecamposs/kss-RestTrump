using System;
using System.Diagnostics;
using ksslib_c.App;

namespace vPOS
{
    internal class cls_configApp : clsConfigGralEstacionXML
    {

        //Propiedades de aplicacion
        public int nroDecimalesRedondeo = 2;
        public string claveAcceso { get; set; }
        public string nombrePuertoCOM { get; set; }
        public bool esIVAIncluido { get; set; }
        public bool esEasy {
            get;
            set; }
        public bool esRest { get; set; }
        public string nombreCaja { get; set; }
        public string CodigoCaja { get; set; }
        public bool esOnline { get; set; }
        public Decimal  nroSiguiente { get; internal set; }
        public string  cajaFechaAnterior { get; internal set; }
        public Int32 UltimoNumeroFactura { get; internal set; }
        public int PorcentajeDefault { get { return _PorcentajeDefault; } set { _PorcentajeDefault = value; } }
       
        internal bool esAutoFix;
        public string TimeAutofix { get; internal set; }
        public bool esXDpto { get; internal set; }
        public string fechaUltimoAutoFix { get; set; }

        public bool esCOM { get; set; }
        public bool GetReport { get; internal set; }
        public bool esActiveIF { get; set; }

        internal bool esHead;
        internal string Encabezado1;
        internal string Encabezado2;
        internal string Encabezado3;
        internal string Encabezado4;
        internal string Encabezado5;
        internal string Encabezado6;
        internal int HorasLaborablesDia;
        internal string Time;
        private int _PorcentajeDefault=30;
        public bool esSQL { get; internal set; }
        public bool esClientPersonalized { get; internal set; }

        //Metodos aplicacion
        public bool GuardarConfig()
        {
            try
            {
                
                clsConfigXML cfg;
                // Crear el objeto de configuración
                cfg = new clsConfigXML(archivoConfigServer, true);
                for (int i = 1; i <= 3; i++)
                {
                    switch (i)
                    {
                        case 2:
                            cfg.FileName = archivo_seguridad_1;
                            break;
                        case 3:
                            cfg.FileName = archivo_seguridad_2;
                            break;
                    }
                    //Estacion
                    cfg.SetValue("App", "nroDecimales", nroDecimalesRedondeo);
                    cfg.SetValue("App", "claveAcceso", claveAcceso);
                    cfg.SetValue("App", "esIVAIncluido", esIVAIncluido);
                    cfg.SetValue("App", "nombrePuertoCOM", nombrePuertoCOM);
                    cfg.SetValue("App", "esRest", esRest );
                    cfg.SetValue("App", "esOnline", esOnline  );
                    cfg.SetValue("App", "nombreCaja", nombreCaja );
                    cfg.SetValue("App", "CodigoCaja", CodigoCaja);
                    cfg.SetValue("App", "nroSiguiente", nroSiguiente.ToString());
                    cfg.SetValue("App", "cajaFechaAnterior", cajaFechaAnterior);
                    cfg.SetValue("App", "UltimoNumeroFactura", UltimoNumeroFactura);
                    cfg.SetValue("App", "esHead", esHead);
                    cfg.SetValue("App", "Encabezado1", Encabezado1);
                    cfg.SetValue("App", "Encabezado2", Encabezado2);
                    cfg.SetValue("App", "Encabezado3", Encabezado3);
                    cfg.SetValue("App", "Encabezado4", Encabezado4);
                    cfg.SetValue("App", "Encabezado5", Encabezado5);
                    cfg.SetValue("App", "Encabezado6", Encabezado6);
                    cfg.SetValue("App", "Time", Time);
                    cfg.SetValue("App", "HorasLaborablesDia", HorasLaborablesDia);
                    cfg.SetValue("App", "PorcentajeDefault", PorcentajeDefault);
                    cfg.SetValue("App", "esAutoFix", esAutoFix);
                    cfg.SetValue("App", "esXDpto", esXDpto);
                    cfg.SetValue("App", "esEasy", esEasy);
                    cfg.SetValue("App", "TimeAutofix", TimeAutofix);
                    cfg.SetValue("App", "fechaUltimoAutoFix", fechaUltimoAutoFix);
                    cfg.SetValue("App", "esCOM", esCOM);
                    cfg.SetValue("App", "GetReport", GetReport);
                    cfg.SetValue("App", "esActiveIF", esActiveIF);
                    cfg.SetValue("App", "esSQL", esSQL);
                    cfg.SetValue("App", "esClientPersonalized", esClientPersonalized);

                }
                return base.GuardarConfig();
            }
            catch(Exception ex)
            {
                Debug.Print(ex.Message );
                return false;
            }
        }
        //leer datos 
        public bool LeerConfig()
        {
            try
            {
                clsConfigXML cfg;
                // Crear el objeto de configuración
                cfg = new clsConfigXML(archivoConfigServer, false);
                if (System.IO.File.Exists(archivoConfigServer))
                {
                    base.LeerConfig();
                    //Estacion
                    nroDecimalesRedondeo = Convert.ToInt32(cfg.GetValue("App", "nroDecimales"));
                    claveAcceso = cfg.GetValue("App", "claveAcceso").ToString();
                    esIVAIncluido = cfg.GetValue("App", "esIVAIncluido",true );
                    nombrePuertoCOM = cfg.GetValue("App", "nombrePuertoCOM").ToString();
                    esRest = cfg.GetValue("App", "esRest", false);
                    esOnline  = cfg.GetValue("App", "esOnline", false);
                    nombreCaja = cfg.GetValue("App", "nombreCaja","");
                    CodigoCaja = cfg.GetValue("App", "CodigoCaja", "");
                    nroSiguiente = cfg.GetValue("App", "nroSiguiente", 1000);
                    cajaFechaAnterior = cfg.GetValue("App", "cajaFechaAnterior", "01/01/2017");
                    UltimoNumeroFactura = cfg.GetValue("App", "UltimoNumeroFactura", 999);
                    esHead= cfg.GetValue("App", "esHead", false);
                    Encabezado1 = cfg.GetValue("App", "Encabezado1","");
                    Encabezado2 = cfg.GetValue("App", "Encabezado2", "");
                    Encabezado3 = cfg.GetValue("App", "Encabezado3", "");
                    Encabezado4 = cfg.GetValue("App", "Encabezado4", "");
                    Encabezado5 = cfg.GetValue("App", "Encabezado5", "");
                    Encabezado6 = cfg.GetValue("App", "Encabezado6", "");
                    HorasLaborablesDia = cfg.GetValue("App", "HorasLaborablesDia", 0);
                    PorcentajeDefault  = cfg.GetValue("App", "PorcentajeDefault", 30);
                    Time = cfg.GetValue("App", "Time", "11:00:00");
                    esAutoFix = cfg.GetValue("App", "esAutoFix", false);
                    esXDpto = cfg.GetValue("App", "esXDpto", false);
                    esEasy = cfg.GetValue("App", "esEasy", false);
                    TimeAutofix = cfg.GetValue("App", "TimeAutofix", "17:00");
                    fechaUltimoAutoFix = cfg.GetValue("App", "fechaUltimoAutoFix", "01/01/2017");
                    esCOM = cfg.GetValue("App", "esCOM", true);
                    GetReport = cfg.GetValue("App", "GetReport", true);
                    esActiveIF = cfg.GetValue("App", "esActiveIF", true);
                    esSQL = cfg.GetValue("App", "esSQL", false);
                    esClientPersonalized = cfg.GetValue("App", "esClientPersonalized", true);
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception ex)
            {
                Debug.Print(ex.Message);
                return false;
            }
        }

        public cls_configApp(bool esIniciandoApp)
            : base(esIniciandoApp)
        {
        }

    }
}
