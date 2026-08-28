using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace haylie.Kingdom
{
    public class Magic
    {
        public static string GetIPAddress(HttpContext p_context)
        {
            string l_ip = p_context.Request.ServerVariables["HTTP_X_FORWARDED_FOR"];

            if (!string.IsNullOrEmpty(l_ip))
            {
                string[] addresses = l_ip.Split(',');
                if (addresses.Length != 0)
                    return addresses[0];

            }
            return p_context.Request.ServerVariables["REMOTE_ADDR"];
        }

        public static string BuildKey(string p_param)
        {
            return _Locksmith.Hash(p_param);
        }
        public static bool KeyValidation(string p_param, string p_real)
        {
            return _Locksmith.Verify(p_param, p_real);
        }
        public static string DayLabel(DateTime p_param)
        {
            string result = "";

            switch (p_param.DayOfWeek.ToString().ToLower())
            {
                case "sunday": result = "domingo"; break;
                case "monday": result = "segunda-feira"; break;
                case "tuesday": result = "terça-feira"; break;
                case "wednesday": result = "quarta-feira"; break;
                case "thursday": result = "quinta-feira"; break;
                case "friday": result = "sexta-feira"; break;
                case "saturday": result = "sábado"; break;
            }

            return result;
        }
        public static string FixDate(DateTime p_param)
        {
            string result = p_param.Day.ToString("00") + "/";
            result += p_param.Month.ToString("00") + "/" + p_param.Year.ToString("0000");

            return result;
        }

        public static bool IsNumeric(string p_param)
        {
            double test;
            return double.TryParse(p_param, out test);
        }

    }
}