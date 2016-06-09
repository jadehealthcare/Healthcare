using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Model.ViewModels
{
   public class UserRegistration
    {
        public int UserID { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public int Age { get; set; }
        public decimal Mobile { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public System.DateTime Date { get; set; }
        public int Points { get; set; }
    }
}
