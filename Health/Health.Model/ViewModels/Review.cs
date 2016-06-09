using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Model.ViewModels
{
  public class Review
    {
        public int ID { get; set; }
        public int UserID { get; set; }
        public int HospitalID { get; set; }
        public string review { get; set; }
        public System.DateTime Date { get; set; }
    }
}
