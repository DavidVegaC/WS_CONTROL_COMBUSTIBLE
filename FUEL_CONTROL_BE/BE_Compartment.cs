using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Compartment : BE_Audit
    {        
        public int IdCompartment { get; set; }

        public int IdProduct { get; set; }

        public int IdCompartmentType { get; set; }

        public string CompartmentName { get; set; }

        public double Capacity { set; get; }

        public int AlertCapacity { set; get; }

    }
}
