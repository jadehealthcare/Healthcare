using Health.Model.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Health.Services.Contract
{
    interface IUser
    {
        int RegisterUser(UserRegistration userRegister);
        int LoginUser(UserRegistration userRegister);
    }
}
