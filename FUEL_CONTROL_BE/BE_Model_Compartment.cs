using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Model_Compartment : BE_Audit
    {
        public int IdModelCompartment { get; set; }

        public int IdModel { get; set; }

        public int IdCompartment { get; set; }

        public int CompartmentNumber { get; set; }
    }
}
