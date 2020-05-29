﻿using BussinesLayer.Interface;
using CommonLayer.Model;
using RepositoryLayer.Interface;
using System;
using System.Collections.Generic;

namespace BussinesLayer.Services
{
    public class ParkingBusiness : IParkingBusiness
    {
        private readonly IParkingRepository parkingRepository;
        public ParkingBusiness(IParkingRepository _parkingRepository)
        {
            parkingRepository = _parkingRepository;
        }

        public ParkingPortal AddData(ParkingPortal parkingPortal)
        {
            try
            {
                var data = parkingRepository.AddData(parkingPortal);
                return data;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object GetVehicleByNo(string number)
        {
            try
            {
                var result = parkingRepository.GetVehicleByNo(number);
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object GetVehicleByBrand(string name)
        {
            try
            {
                var result = parkingRepository.GetVehicleByBrand(name);
                return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public object ParkStatus(ParkingStatus parkingStatus)
        {
            try
            {
                var result = parkingRepository.ParkStatus(parkingStatus);
                    return result;
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public List<ParkingPortal> GetDetail()
        {
            try
            {
                var result = parkingRepository.GetDetail();
                return result;
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }
    }
}