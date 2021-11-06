using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_User : BE_Audit
    {
        public int IdUser { get; set; }

        public string UUser { get; set; }

        public string UPassword { get; set; }

        public int ULevel { get; set; }

        public int IdPerson { set; get; }

        public string PersonName { set; get; }

        public string FirstLastName { set; get; }

        public string SecondLastName { set; get; }

        public string Photocheck { set; get; }
    }
}
