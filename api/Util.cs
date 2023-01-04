using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Fluxus.Api
{
    public class Util
    {

        public static dynamic DateOrNull(string data)
        {
            if (string.IsNullOrEmpty(data))
                return null;

            return Convert.ToDateTime(data);
        }

        public static string DateTimeToShortDateString(string date)
        {
            if (string.IsNullOrEmpty(date))
                return null;
            
            return Convert.ToDateTime(date).ToString("dd/MM/yyyy");   
        }

    }
}
