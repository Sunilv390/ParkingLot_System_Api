using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
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

        public ParkingLotController(IParkingBusiness _parkingBussiness)
        {
            parkingBussiness = _parkingBussiness;
        }

        // POST: api/ParkingLot
        //Add Parking Details
        [HttpPost]
        public ActionResult AddVehicle(ParkingPortal parkingPortal)
        {
            try
            {
                var data = parkingBussiness.AddData(parkingPortal);
                var count = parkingBussiness.CountSlot();
                bool success = false;
                string message;
                if (data == null)
                {
                    success = false;
                    message = "Parking is Full";
                    return Ok(new { success, message });
                }
                else
                {
                    success = true;
                    message = "Data Added Successfully";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Invalid Data";
                return BadRequest(new { success, message });
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
                    if (data != null)
                    {
                        success = true;
                        message = "Vehicle Details By Number";
                        return Ok(new { success, message, data });
                    }
                    else
                    {
                        message = "No such Vehicle";
                        return Ok(new { success, message });
                    }
                }
                catch(Exception e)
                {
                    throw new Exception(e.Message);
                }
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Data not Found";
                return BadRequest(new { success, message });
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
                    if (data != null)
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
            catch (Exception)
            {
                bool success = false;
                string message = "No Such Data";
                return BadRequest(new { success, message });
            }
        }

        //POST: api/ParkingLot
        //Parking Status details
        [HttpPost]
        [Route("parkstatus")]
        public ActionResult ParkStatus(ParkingStatus parkingStatus)
        {
            try
            {
                var data = parkingBussiness.ParkStatus(parkingStatus);
                string message;
                if (data == null)
                {
                    bool success = false;
                    message = "Data not Found";
                    return Ok(new { success, message });
                }
                else
                {
                    bool success = true;
                    message = "Parking Status";
                    return Ok(new { success, message, data });
                }
            }
            catch (Exception)
            {
                bool success = false;
                string message = "Fail to Unpark";
                return BadRequest(new { success, message });
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
            catch (Exception)
            {
                bool success = false;
                string message = "No such Data";
                return BadRequest(new { success, message });
            }
        }
    }
}
