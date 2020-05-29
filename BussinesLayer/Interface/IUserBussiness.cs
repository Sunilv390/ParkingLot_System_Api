using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace BussinesLayer.Interface
{
    public interface IUserBussiness
    {
        UserDetail AddUserData(UserDetail userDetail);
        List<UserDetail> GetOwnerDetails();
        object Login(UserLogin login);
        
    }
}
