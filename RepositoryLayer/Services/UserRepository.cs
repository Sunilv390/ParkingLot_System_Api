using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;
using System.Linq;

namespace RepositoryLayer.Services
{
    public class UserRepository : IUserRepository
    {
        ParkingContext db;
        public UserRepository(ParkingContext _db)
        {
            db = _db;
        }

        public List<UserDetail> GetOwnerDetails()
        {
            return (from table in db.UserDetails
                    select new UserDetail
                    {
                        UserId = table.UserId,
                        UserName = table.UserName,
                        Email = table.Email,
                        Password=table.Password,
                        UserType = table.UserType
                    }).ToList();
        }

        public object Login(UserLogin login)
        {
            try
            {
                if (login.UserType.Contains("Owner"))
                {
                    return (from log in db.UserDetails
                            from p in db.parkingPortals
                            where log.Email == login.Email && log.Password == login.Password && log.UserType == login.UserType
                            select new
                            {
                                p.ReceiptNo,
                                p.DriverName,
                                p.Brand,
                                p.VehicleColor,
                                p.VehicleNumber,
                                p.Slot
                            }).ToList();
                }
                else if(login.UserType.Contains("Security"))
                {
                    return (from log in db.UserDetails
                            from p in db.parkingPortals
                            where log.Email == login.Email && log.Password == login.Password && log.UserType == login.UserType
                            select new
                            {
                                p.DriverName,
                                p.ParkingDate,
                                p.Slot,
                                p.VehicleNumber
                            }).ToList();
                }
                else if(login.UserType.Contains("Police"))
                {
                    return (from log in db.UserDetails
                            from p in db.parkingPortals
                            where log.Email == login.Email && log.Password == login.Password && log.UserType == login.UserType
                            select new
                            {
                                p.ReceiptNo,
                                p.DriverName,
                                p.Brand,
                                p.VehicleColor,
                                p.VehicleNumber,
                                p.Slot
                            }).ToList();
                }
                else if(login.UserType.Contains("Driver"))
                {
                    return (from log in db.UserDetails
                            from p in db.parkingPortals
                            where log.Email == login.Email && log.Password == login.Password && log.UserType == login.UserType
                            select new
                            {
                                p.Brand,
                                p.VehicleColor,
                                p.VehicleNumber
                            }).ToList();
                }
                else
                {
                    return "No such Records";
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public UserDetail AddUserData(UserDetail UserDetail)
        {
            try
            {
                db.Add(UserDetail);
                db.SaveChanges();
                return UserDetail;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}
