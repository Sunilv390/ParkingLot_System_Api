using CommonLayer.Model;
using System.Collections.Generic;

namespace BussinesLayer.Interface
{
    public interface IParkingBusiness
    {
        ParkingPortal AddData(ParkingPortal parkingPortal);
        List<ParkingPortal> GetDetail();
        object GetVehicleByNo(string number);
        object GetVehicleByBrand(string name);
        object ParkStatus(ParkingStatus parkingStatus);
        object CountSlot();
    }
}
