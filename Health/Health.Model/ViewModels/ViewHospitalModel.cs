using Health.Model.DBContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Model.ViewModels
{
    public class ViewHospitalModel
    {
        public List<sp_getHospitalData_Result> hospitalList { get; set; }
    }
}
