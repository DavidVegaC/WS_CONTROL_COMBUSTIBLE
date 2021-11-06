using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Reason : BE_Audit
    {        
        public int IdReason{ get; set; }

        public int IdProduct { get; set; }

        public string ReasonName { get; set; }

        public int ReasonNumber { get; set; }        

    }
}
