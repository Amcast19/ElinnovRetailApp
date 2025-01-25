using ElinnovRetail.Models.Common;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElinnovRetail.App.Extensions
{
    public static class CurrencyExtension
    {
        public static string ConvertDecimalToCurrency(this decimal amount)
        {
            string formattedAmount = amount.ToString("C", new CultureInfo("en-PH"));
            return formattedAmount;
        }
    }
}
