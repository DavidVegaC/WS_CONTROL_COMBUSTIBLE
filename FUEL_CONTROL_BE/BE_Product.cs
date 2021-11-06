using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Product : BE_Audit
    {
        public int IdProduct { get; set; }

        public int NumberProduct { get; set; }

        public string ProductName { get; set; }

        public string MeasurementUnit { set; get; }

        public string ElipseCode { set; get; }

    }
}
