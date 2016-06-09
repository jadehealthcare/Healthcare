using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Model.ViewModels
{
    public class HospitalDetail
    {
        public int ID { get; set; }
        public bool ICU { get; set; }
        public bool Ambulance { get; set; }
        public int HospitalID { get; set; }
    }
}
