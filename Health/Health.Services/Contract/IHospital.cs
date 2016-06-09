using Health.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Services.Contract
{
   public  interface IHospital
    {
        int RegisterHospital(HospitalRegister hostpitalRegister);
        int LoginHospital(HospitalRegister hospitalRegister);
        ViewHospitalModel GetHospitalData();
    }
}
