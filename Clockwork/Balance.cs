using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork
{
    public class Balance
    {
        /// <summary>
        /// Balance in your currency
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Currency Symbol for display
        /// </summary>
        public string CurrencySymbol { get; set; }
        
        /// <summary>
        /// ISO 4217 3 letter currency code
        /// </summary>
        public string CurrencyCode { get; set; }

        /// <summary>
        /// Type of billing on this account (Pay as you go or Invoice)
        /// </summary>
        public AccountType AccountType { get; set; }
    }
}
