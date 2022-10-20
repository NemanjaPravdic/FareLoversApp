using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace ResvoyageMobileApp.Models
{
    public static class Util
    {
        public static string GetCurrencySymbol(string CurrencyCode)
        {
            CultureInfo[] cultures = CultureInfo.GetCultures(CultureTypes.SpecificCultures);
            foreach (CultureInfo ci in cultures)
            {
                RegionInfo ri = new RegionInfo(ci.LCID);
                if (ri.ISOCurrencySymbol == CurrencyCode)
                {
                    return ri.CurrencySymbol;
                }
            }

            return CurrencyCode;
        }
    }
}
