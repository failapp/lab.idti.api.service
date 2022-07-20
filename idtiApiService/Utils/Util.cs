using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using System.Globalization;
using System.Net.NetworkInformation;

namespace idtiApiService.Utils
{
    public class Util
    {

        public static bool IsNumeric(string s) {
            float output;
            return float.TryParse(s, out output);
        }

        public static int conversionBit(string data) {
            bool bln = Convert.ToBoolean(data);
            return (bln) ? 1 : 0;
        }


        public static string dateFormat(string fecha, Boolean formato_corto = false) {

            DateTime dFecha = DateTime.ParseExact(fecha, "yyyyMMdd",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None);
            if (formato_corto) {
                return dFecha.ToString("dd/MM/yy");
            } else {
                return dFecha.ToString("dd/MM/yyyy");
            }
        }


        public static string dateTimeFormat(string fecha, bool seg = true) {

            DateTime dFecha = DateTime.ParseExact(fecha, "yyyyMMddHHmmss",
                                                    CultureInfo.InvariantCulture,
                                                    DateTimeStyles.None);
            if (!seg) {
                return dFecha.ToString("dd/MM/yyyy HH:mm");
            } else {
                return dFecha.ToString("dd/MM/yyyy HH:mm:ss");
            }
        }


        public static string timeFormat(string hora, Boolean segundos = true) {

            string sHora = "";
            try {

                if (segundos) {
                    sHora = hora.Substring(0, 2) + ":" + hora.Substring(2, 2) + ":" + hora.Substring(4, 2);
                } else {
                    sHora = hora.Substring(0, 2) + ":" + hora.Substring(2, 2);
                }

            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return sHora;

        }


        public static bool IsDate(String fecha) {
            try {
                DateTime.Parse(fecha);
                return true;
            } catch {
                return false;
            }
        }



        public static bool pingDevice(string ipAddress) {

            bool bln = false;
            Ping Pings = new Ping();
            int timeout = 2000;
            try {
                if (Pings.Send(ipAddress, timeout).Status == IPStatus.Success) {
                    bln = true;
                } else {
                    bln = false;
                }
            } catch (Exception ex) {
                Console.WriteLine(ex.Message);
            }

            return bln;

        }

    }
}
