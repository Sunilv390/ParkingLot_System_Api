using BussinesLayer.Interface;
using BussinesLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using ParkingLot_System.Controllers;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using Xunit;

namespace XUnitTestParkingLot
{
    public class ParkingLotTest
    {
        ParkingLotController parkingLotController;
        RegistrationController registrationController;

        private readonly IParkingBusiness _parkingLotBusiness;
        private readonly IParkingRepository parkingLotRepository;
        private readonly IUserRepository userRepository;
        private readonly IUserBussiness _userBusiness;
        private readonly IConfiguration configuration;
        public static DbContextOptions<ParkingContext> dbContextOptions { get; }

        public static string connectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\Documents\\ParkingDb.mdf;Integrated Security=True;Connect Timeout=30";

        static ParkingLotTest()
        {
            dbContextOptions = new DbContextOptionsBuilder<ParkingContext>()
                .UseSqlServer(connectionString)
                .Options;
        }

        public ParkingLotTest()
        {
            var context = new ParkingContext(dbContextOptions);

            userRepository = new UserRepository(context);
            _userBusiness = new UserBussiness(userRepository);

            parkingLotRepository = new ParkingRepository(context);
            _parkingLotBusiness = new ParkingBusiness(parkingLotRepository);


            IConfigurationBuilder _configuration = new ConfigurationBuilder();

            _configuration.AddJsonFile("appsettings.json");
            configuration = _configuration.Build();

            parkingLotController = new ParkingLotController(_parkingLotBusiness);
            registrationController = new RegistrationController(_userBusiness, configuration);
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
            var Ok = parkingLotController.GetVehicleByNo("MH47P 1636");
            Assert.IsType<OkObjectResult>(Ok);
        }

        [Fact]
        public void GetParkingDetails_by_VehicleNumber_returns_BadRequest()
        {
            var badrequest = parkingLotController.GetVehicleByNo("");
            Assert.IsType<BadRequestObjectResult>(badrequest);
        }

        [Fact]
        public void GetParkingDetails_by_VehicleBrand_returns_BadRequest()
        {
            var badRequest = parkingLotController.GetVehicleByBrand("");
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void GetParkingDetails_by_VehicleBrand_returns_Ok()
        {
            var Ok = parkingLotController.GetVehicleByBrand("BMW");
            Assert.IsType<OkObjectResult>(Ok);
        }

        [Fact]
        public void AddingParkingDetails_ReturnOKResult()
        {

            ParkingPortal details = new ParkingPortal()
            {
                DriverName = "Sunil",
                VehicleNumber = "MH47P 1636",
                Brand = "Jaguar",
                VehicleColor = "Black",
                Slot = "A",
                ParkingDate = DateTime.Now,
                Status = "Park"

            };

            // Act
            var okResult = parkingLotController.AddVehicle(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void AddingParkingDetails_ReturnBadRequest()
        {
            ParkingPortal details = new ParkingPortal()
            {
                DriverName = "",
                VehicleNumber = "",
                Brand = "",
                VehicleColor = "",
                Slot = "",
                ParkingDate = DateTime.Now,
                Status = ""

            };

            // Act
            var badRequest = parkingLotController.AddVehicle(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void UnParkDetails_ReturnOKResult()
        {
            //Arrange
            ParkingStatus details = new ParkingStatus()
            {
                ReceiptNo = 1013

            };
            // Act
            var okResult = parkingLotController.ParkStatus(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UnParkDetails_ReturnBadRequest()
        {

            ParkingStatus details = new ParkingStatus()
            {
                ReceiptNo = 10

            };

            // Act
            var badRequest = parkingLotController.ParkStatus(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        /// <summary>
        /// Registration Controller 
        /// </summary>
        [Fact]
        public void GetUserDetails_ReturnOKResult()
        {
            RegistrationController controller = new RegistrationController(_userBusiness, configuration);

            var okResult = controller.GetAllDetails();

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UserLogin_ReturnOKResult()
        {
            RegistrationController controller = new RegistrationController(_userBusiness, configuration);

            UserLogin details = new UserLogin()
            {
                Email = "",
                Password = "",
                UserType = ""

            };
            // Act
            var okResult = registrationController.Login (details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UserLogin_ReturnBadRequest()
        {

            UserLogin details = new UserLogin()
            {
                Email = "abhi",
                Password = "abhi123"

            };
            // Act
            var badRequest = registrationController.Login(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }

        [Fact]
        public void UserRegistration_ReturnOKResult()
        {

            var details = new UserDetail()
            {
                UserName = "yoshika1",
                Email = "yoshika1@gmail.com",
                Password = "yoshika",
                UserType = "Admin"

            };

            var okResult = registrationController.AddUser(details);

            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void UserRegistration_ReturnBadRequest()
        {

            var details = new UserDetail()
            {
                UserName = "yoshika1",
                Email = "yoshika1@gmail.com",
                Password = "yoshika",
                UserType = "Admin"

            };

            //Act
            var badRequest = registrationController.AddUser(details);

            // Assert
            Assert.IsType<BadRequestObjectResult>(badRequest);
        }
    }
}
