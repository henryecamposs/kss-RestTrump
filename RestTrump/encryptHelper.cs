using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Security.Cryptography;
using System.IO;
namespace RestTrump
{
    internal class encryptHelper
    {

        public static string encryptus(string x, string KeyEncrypt = "12345678")//function
        {
            try
            {

                string y = x;
                byte[] etext = UTF8Encoding.UTF8.GetBytes(y);
                string Code = KeyEncrypt;
                MD5CryptoServiceProvider mdhash = new MD5CryptoServiceProvider();
                byte[] keyarray = mdhash.ComputeHash(UTF8Encoding.UTF8.GetBytes(Code));
                TripleDESCryptoServiceProvider tds = new TripleDESCryptoServiceProvider();
                tds.Key = keyarray;
                tds.Mode = CipherMode.ECB;
                tds.Padding = PaddingMode.PKCS7;

                ICryptoTransform itransform = tds.CreateEncryptor();
                byte[] result = itransform.TransformFinalBlock(etext, 0, etext.Length);
                string encryptresult = Convert.ToBase64String(result);
                return encryptresult.ToString();
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Error encriptando: " + ex.Message);
                return "";
            }
        }

        public static bool dencryptus(string x, bool Default, string KeyEncrypt = "12345678")
        {
            return Program.isBool(dencryptus(x, KeyEncrypt)) ? Convert.ToBoolean(dencryptus(x, KeyEncrypt)) : Default;
        }
        public static DateTime dencryptus(string x, DateTime Default, string KeyEncrypt = "12345678")
        {
            return Program.isDate(dencryptus(x, KeyEncrypt)) ? Convert.ToDateTime(dencryptus(x, KeyEncrypt)) : Default;
        }
        public static int dencryptus(string x, int Default, string KeyEncrypt = "12345678")
        {
            return Program.isInt(dencryptus(x, KeyEncrypt)) ? Convert.ToInt32(dencryptus(x, KeyEncrypt)) : Default;
        }
        public static string dencryptus(string x, string KeyEncrypt = "12345678")
        {
            try
            {
                string y = x.Replace("\0", null).Replace(" ", null);
                byte[] etext = Convert.FromBase64String(y);
                string key = KeyEncrypt;
                MD5CryptoServiceProvider mdhash = new MD5CryptoServiceProvider();
                byte[] keyarray = mdhash.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                TripleDESCryptoServiceProvider tds = new TripleDESCryptoServiceProvider();
                tds.Key = keyarray;
                tds.Mode = CipherMode.ECB;
                tds.Padding = PaddingMode.PKCS7;

                ICryptoTransform itransform = tds.CreateDecryptor();
                byte[] result = itransform.TransformFinalBlock(etext, 0, etext.Length);
                string dencryptresult = UTF8Encoding.UTF8.GetString(result);
                return dencryptresult.ToString();
            }
            catch (Exception ex)
            {
                string EncriptVacio = encryptus("", KeyEncrypt);
                return dencryptus(EncriptVacio, KeyEncrypt);
            }
        }
    }
}
