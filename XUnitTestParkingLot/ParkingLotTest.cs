using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Moq;
using ParkingLot_System.Controllers;
using System;
using Xunit;

namespace XUnitTestParkingLot
{
    public class ParkingLotTest
    {
        ParkingLotController parkingLotController;
        RegistrationController userLoginController;
        private readonly Mock<IParkingBusiness> parkingBussiness;
        private readonly Mock<IUserBussiness> userBussiness;
        private readonly Mock<IConfiguration> configuration;

        public ParkingLotTest()
        {
            parkingBussiness = new Mock<IParkingBusiness>();
            userBussiness = new Mock<IUserBussiness>();
            configuration = new Mock<IConfiguration>();
            parkingLotController = new ParkingLotController(parkingBussiness.Object);
            userLoginController = new RegistrationController(userBussiness.Object, configuration.Object);
        }

        [Fact]
        public void GetParkingData_which_return_Ok()
        {
            var data = parkingLotController.GetDetails();
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetParkingDetails_by_VehicleNumber_returns_Ok()
        {
            var data = parkingLotController.GetVehicleByNo("MH47P 1636");
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void GetParkingDetails_by_VehicleBrand_returns_Ok()
        {
            var data = parkingLotController.GetVehicleByBrand("BMW");
            Assert.IsType<OkObjectResult>(data);
        }

        [Fact]
        public void ParkingStatus_return_Ok()
        {
            ParkingStatus parkingStatus = new ParkingStatus()
            {
                ReceiptNo = 3
            };
            var Ok = parkingLotController.ParkStatus(parkingStatus);
            Assert.IsType<OkObjectResult>(Ok);
        }
    }
}
