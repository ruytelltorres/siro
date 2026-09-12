using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;

namespace CMACMaynas.Web.SIRO.Seguridad.Auth.Helpers
{
    public class FunGlobales
    {
        
        //private static string sExpRegIPs = "^(([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5]).){3}([1-9]?[0-9]|1[0-9]{2}|2[0-4][0-9]|25[0-5])$";
       
        public static string base64Decode(string data)
        {
            byte[] toDecodeByte = Convert.FromBase64String(data);

            System.Text.UTF8Encoding encoder = new System.Text.UTF8Encoding();
            System.Text.Decoder utf8Decode = encoder.GetDecoder();

            int charCount = utf8Decode.GetCharCount(toDecodeByte, 0, toDecodeByte.Length);

            char[] decodedChar = new char[charCount];
            utf8Decode.GetChars(toDecodeByte, 0, toDecodeByte.Length, decodedChar, 0);
            string result = new String(decodedChar);
            return result;
        }

        public static string base64Encode(string cadena)
        {
            byte[] cadenaByte = new byte[cadena.Length];
            cadenaByte = System.Text.Encoding.UTF8.GetBytes(cadena);
            string encodedCadena = Convert.ToBase64String(cadenaByte);
            return encodedCadena;
        }



    }
}
