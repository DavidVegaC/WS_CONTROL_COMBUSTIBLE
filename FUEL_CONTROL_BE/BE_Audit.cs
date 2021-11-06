using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FUEL_CONTROL_BE
{
    public class BE_Audit
    {
        public string RegistrationStatus { get; set; }

        public string MigrationStatus { get; set; }

        public int RegistrationUser { get; set; }

        public DateTime RegistrationDate { get; set; }

        public int UpdateUser { get; set; }

        public DateTime UpdateDate { get; set; }

        public int UpdateProcess { get; set; }

        public string RegistrationStatusDesc { get; set; }

        public string UserDesc { get; set; }

        public string UserRegistrationDesc { get; set; }

        public string RegistrationDateString { get; set; }

        public string UpdateDateString { get; set; }

        public string MensajeConsulta { get; set; }

        public string ValorConsulta { get; set; }

        public int RecordsFound { set; get; }

        public string valorUtil { set; get; }
    }
}
