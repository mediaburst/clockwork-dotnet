using System;
using System.Collections.Generic;
using System.Text;

namespace Clockwork.XML
{
    public class Balance_Resp
    {
        public string AccountType { get; set; }
        
        public decimal Balance { get; set; }

        public Currency Currency { get; set; }

        public int? ErrNo { get; set; }
        public string ErrDesc { get; set; }
    }
}
