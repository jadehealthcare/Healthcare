using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Model.ViewModels
{
    public class HospitalRegister
    {
        public int HospitalID { get; set; }
        public string HospitalName { get; set; }
        public string Description { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public decimal PinCode { get; set; }
        public decimal Phone { get; set; }
        public decimal Mobile { get; set; }
        public string Email { get; set; }
        public System.DateTime Date { get; set; }
        public bool Approval { get; set; }
        public string Id { get; set; }
        public string Password { get; set; }

        public decimal latitude { get; set; }
        public decimal longitude { get; set; }
    }
}
