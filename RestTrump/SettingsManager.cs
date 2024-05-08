using ksslib;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RestTrump
{
    public class SettingsManager
    {

        public static bool ReadSettingBoolean(string key, bool Default, string keyClave = "")
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] == null ? Default : !string.IsNullOrEmpty(keyClave) ? Convert.ToBoolean(encryptHelper.dencryptus(appSettings[key], keyClave)) : Convert.ToBoolean(appSettings[key]);
                return result;
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex,false);
                return Default;
            }
        }
        public static DateTime ReadSettingDateTime(string key, DateTime Default, string keyClave = "")
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] == null ? Default : !string.IsNullOrEmpty(keyClave) ? Convert.ToDateTime(encryptHelper.dencryptus(appSettings[key], keyClave)) : Convert.ToDateTime(appSettings[key]);
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
        }
        public static Double ReadSettingDouble(string key, Double Default, string keyClave = "")
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] == null ? Default : !string.IsNullOrEmpty(keyClave) ? Convert.ToDouble(encryptHelper.dencryptus(appSettings[key], keyClave)) : Convert.ToDouble(appSettings[key]);
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
        }

        public static int ReadSettingInt(string key, int Default, string keyClave = "")
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] == null ? Default : !string.IsNullOrEmpty(keyClave) ? Convert.ToInt32(encryptHelper.dencryptus(appSettings[key], keyClave)) : Convert.ToInt32(appSettings[key]);
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
        }
        public static string ReadSetting(string key, string Default, string keyClave = "")
        {
            try
            {
                var appSettings = ConfigurationManager.AppSettings;
                var result = appSettings[key] == null ? Default : !string.IsNullOrEmpty(keyClave) ? encryptHelper.dencryptus(appSettings[key], keyClave) : appSettings[key].ToString();
                return result;
            }
            catch (ConfigurationErrorsException ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return string.Empty;
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex, false);
                return Default;
            }
        }

        public static void AddUpdateAppSettings(string key, string value, string keyClave = "")
        {
            try
            {
                var configFile = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
                var settings = configFile.AppSettings.Settings;
                value = !string.IsNullOrEmpty(keyClave) ? encryptHelper.encryptus(value, keyClave) : value;
                if (settings.Count == 0 | settings[key] == null)
                {
                    settings.Add(key, value);
                }
                else
                {
                    settings[key].Value = value;
                }
                configFile.Save(ConfigurationSaveMode.Modified);
                ConfigurationManager.RefreshSection(configFile.AppSettings.SectionInformation.Name);
            }
            catch (ConfigurationErrorsException ex)
            {
                clsUtilErrors.Manejador_errores(ex );
            }
            catch (Exception ex)
            {
                clsUtilErrors.Manejador_errores(ex );
            }
        }
    }

}
