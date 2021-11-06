using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Operator : BE_Audit
    {
        public int IdOperator { get; set; }

        public string OperatorKey { get; set; }

        public string OperatorUser { get; set; }

        public string OperatorPassword { get; set; }

        public int IdPerson { set; get; }

        public string PersonName { set; get; }

        public string FirstLastName { set; get; }

        public string SecondLastName { set; get; }

        public string Photocheck { set; get; }

        public int IdOperatorValidationMap { set; get; }

        public string ValidationDescription { set; get; }

    }
}
