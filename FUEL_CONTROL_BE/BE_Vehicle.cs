using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Vehicle : BE_Audit
    { 
        public int IdVehicle { get; set; }

        public int IdCompany { get; set; }

        public int IdModel { get; set; }

        public String Plate { get; set; }

        public String VehicleDescription { get; set; }

    }
}
