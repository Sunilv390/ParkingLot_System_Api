using CommonLayer.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace RepositoryLayer.Interface
{
    public interface IParkingRepository
    {
        ParkingPortal AddData(ParkingPortal parkingPortal);
        List<ParkingPortal> GetDetail();
        object GetVehicleByBrand(string name);
        object GetVehicleByNo(string number);
        object ParkStatus(ParkingStatus parkingStatus);
    }
}
