using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Linq;

namespace ParkingLot_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ParkingLotController : ControllerBase
    {
        private readonly IParkingBusiness parkingBussiness;
        private readonly ParkingContext db;

        public ParkingLotController(IParkingBusiness _parkingBussiness,ParkingContext _db)
        {
            parkingBussiness = _parkingBussiness;
            db = _db;
           
        }

        // POST: api/ParkingLot
        //Add Parking Details
        [HttpPost]
        public ActionResult AddVehicle(ParkingPortal parkingPortal)
        {
            try
            {
                var data = parkingBussiness.AddData(parkingPortal);
                bool success = true;
                string message;
                message = "Data Added Successfully";
                return Ok(new { success, message, data });
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //GET: api/ParkingLot
        //Get vehicle details by Vehicle number
        [HttpGet]
        [Route("vehicleno")]
        public ActionResult GetVehicleByNo(string number)
        {
            try
            {
                var data = parkingBussiness.GetVehicleByNo(number);

                bool success = false;
                string message;
                try
                {
                    if (db.parkingPortals.Any(x => x.VehicleNumber == number))
                    {
                        success = true;
                        message = "Vehicle Details By Number";
                        return Ok(new { success, message, data });
                    }
                    else
                    {
                        message = "Data not Found";
                        return Ok(new { success, message });
                    }
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //GET: api/ParkingLot
        //get vehicle details by Brand name
        [HttpGet]
        [Route("brand")]
        public ActionResult GetVehicleByBrand(string name)
        {
            try
            {
                var data = parkingBussiness.GetVehicleByBrand(name);

                bool success = false;
                string message;
                try
                {
                    if (db.parkingPortals.Any(x => x.Brand == name))
                    {
                        success = true;
                        message = "Vehicle Details By Brand Name";
                        return Ok(new { success, message, data });
                    }
                    else
                    {
                        message = "Data not Found";
                        return Ok(new { success, message });
                    }
                }
                catch (Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            catch
            {
                return BadRequest("No Such Data");
            }
        }

        //POST: api/ParkingLot
        //Parking Status details
        [HttpPost]
        [Route("parkstatus")]
        public ActionResult ParkStatus(ParkingStatus parkingStatus)
        {
            var data = parkingBussiness.ParkStatus(parkingStatus);

            bool success = false;
            string message;
            if (data == null)
            {
                message = "Data not Found";
                return Ok(new { success, message });
            }
            else
            {
                success = true;
                message = "Parking Status";
                return Ok(new { success, message, data });
            }
        }

        //GET: api/ParkingLot
        //Get all Parking details.
        [HttpGet]
        public ActionResult GetDetails()
        {
            try
            {
                bool success = false;
                string message;
                var result = parkingBussiness.GetDetail();
                if (result == null)
                {
                    message = "Data not Found";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Parking Details";
                    return Ok(new { success, message, result });
                }
            }
            catch (Exception e)
            {
                return BadRequest(e.Message);
            }
        }
    }
}
