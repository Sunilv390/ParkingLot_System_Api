using CommonLayer.Model;
using System.Collections.Generic;

namespace RepositoryLayer.Interface
{
    public interface IUserRepository
    {
        UserDetail AddUserData(UserDetail userDetail);
        List<UserDetail> GetOwnerDetails();
        object Login(UserLogin login);
       
    }
}
