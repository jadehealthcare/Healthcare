using Health.Model.DBContext;
using Health.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Services.Services
{
    public class User
    {
        HealthCareEntities healthCareEntities = new HealthCareEntities();
        public int RegisterUser(UserRegistration userRegister)
        {
            ObjectParameter exist = new ObjectParameter("ReturnValue", typeof(int));
            DateTime date = DateTime.Now;
            healthCareEntities.sp_UserRegister(exist, userRegister.FirstName, userRegister.LastName, userRegister.Gender, userRegister.Age, userRegister.Mobile, userRegister.Email, userRegister.Password, date, 1);
            healthCareEntities.SaveChanges();

            return Convert.ToInt32(exist.Value);        }
        public int LoginUser(UserRegistration userRegister)
        {

            ObjectParameter exist = new ObjectParameter("is_match", typeof(int));
            healthCareEntities.sp_LoginUser(exist, userRegister.Email, userRegister.Password);
            return Convert.ToInt32(exist.Value);
        }

    }
}
