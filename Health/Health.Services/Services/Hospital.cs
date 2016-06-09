using Health.Model.DBContext;
using Health.Model.ViewModels;
using Health.Services.Contract;
using System;
using System.Data.Entity.Core.Objects;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Health.Services.Services
{
   public class Hospital:IHospital
    {
        HealthCareEntities healthCareEntities = new HealthCareEntities();

        public int RegisterHospital(HospitalRegister hostpitalRegister)
        {

            bool apr = true;

            ObjectParameter exist = new ObjectParameter("ReturnValue", typeof(int));

            DateTime date = DateTime.Now;
            healthCareEntities.sp_HosptialRegister(exist, hostpitalRegister.HospitalName, hostpitalRegister.Description, hostpitalRegister.Address, hostpitalRegister.City, hostpitalRegister.State, hostpitalRegister.Country, hostpitalRegister.PinCode, hostpitalRegister.Phone, hostpitalRegister.Mobile, hostpitalRegister.Email, date, apr, hostpitalRegister.Id, hostpitalRegister.Password, hostpitalRegister.latitude, hostpitalRegister.longitude);
            healthCareEntities.SaveChanges();

            return Convert.ToInt32(exist.Value);
        }
        public int LoginHospital(HospitalRegister hospitalRegister)
        {

            ObjectParameter exist = new ObjectParameter("is_match", typeof(int));
            healthCareEntities.sp_LoginHospital(exist, hospitalRegister.Id, hospitalRegister.Password);
            return Convert.ToInt32(exist.Value);
        }
        public ViewHospitalModel GetHospitalData()
        {
            ViewHospitalModel viewHospitalModel = new ViewHospitalModel();
            viewHospitalModel.hospitalList = healthCareEntities.sp_getHospitalData().ToList();
            //List<ViewHospitalModel> HospitalData = viewHospitalModel.hospitalList;
            return viewHospitalModel;
        }

    }
}
