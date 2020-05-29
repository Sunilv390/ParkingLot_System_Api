using BussinesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BussinesLayer.Services
{
    public class UserBussiness : IUserBussiness
    {
        public readonly IUserRepository userRepository;

        public UserBussiness(IUserRepository _userRepository)
        {
            userRepository = _userRepository;
        }

        public UserDetail AddUserData(UserDetail userDetail)
        {
            try
            {
                var data = userRepository.AddUserData(userDetail);
                return data;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<UserDetail> GetOwnerDetails()
        {
            try
            {
                var result = userRepository.GetOwnerDetails();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object Login(UserLogin login)
        {
            try
            {
                var data = userRepository.Login(login);
                if (data == null)
                {
                    throw new Exception();
                }
                else
                {
                    return data;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
