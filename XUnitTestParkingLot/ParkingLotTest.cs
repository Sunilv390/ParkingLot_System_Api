using BussinesLayer.Interface;
using BussinesLayer.Services;
using CommonLayer.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Moq;
using ParkingLot_System.Controllers;
using RepositoryLayer.Interface;
using RepositoryLayer.Services;
using System;
using Xunit;

namespace XUnitTestParkingLot
{
    public class ParkingLotTest
    {
        private readonly Mock<IParkingBusiness> _mockRepo;
        private readonly Mock<IUserBussiness> _userRepo;
        private readonly IParkingBusiness parkingBL;
        private readonly IParkingRepository parkingRL;

        private readonly RegistrationController _user;
        private readonly ParkingLotController parkingLotController;

        private readonly ParkingContext db;
        private readonly IUserRepository userRL;
        private readonly IUserBussiness userBL;

        private readonly UserDetail userDetail;
        private readonly ParkingStatus unparkCar;
        private readonly ParkingPortal parkingDetails;

        private readonly IConfiguration _config;
        public static DbContextOptions<ParkingContext> dbContext { get; }

        public static string sqlConnectionString = "Data Source=(LocalDB)\\MSSQLLocalDB;AttachDbFilename=C:\\Users\\ASUS\\Documents\\ParkingDb.mdf;Integrated Security=True;Connect Timeout=30";


        static ParkingLotTest()
        {
            dbContext = new DbContextOptionsBuilder<ParkingContext>().UseSqlServer(sqlConnectionString).Options;
        }

        public ParkingLotTest()
        {
            var context = new ParkingContext(dbContext);
            _mockRepo = new Mock<IParkingBusiness>();
            _userRepo = new Mock<IUserBussiness>();
            userRL = new UserRepository(context);
            userBL = new UserBussiness(userRL);
            parkingRL = new ParkingRepository(context);
            parkingBL = new ParkingBusiness(parkingRL);
            IConfigurationBuilder configuration = new ConfigurationBuilder();
            configuration.AddJsonFile("appsettings.json");
            _config = configuration.Build();
            // db = new DBContext();
            parkingLotController = new ParkingLotController(_mockRepo.Object, db);


            //   usercontroller = new UserController(userBL, _config, parkingBL);
            _user = new RegistrationController(_userRepo.Object,_config, parkingBL);
            //  loginUser = new LoginUser();
            userDetail = new UserDetail();
            parkingDetails = new ParkingPortal();
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
            var BadRequestResult = parkingLotController.GetVehicleByNo("MH47P 1636");
            Assert.IsType<OkObjectResult>(BadRequestResult);
        }

        [Fact]
        public void GetParkingDetails_by_VehicleBrand_returns_Ok()
        {
            var BadRequestResult = parkingLotController.GetVehicleByBrand("BMW");
            Assert.IsType<OkObjectResult>(BadRequestResult);
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

        [Fact]
        public void AddParkingData()
        {
            var BadRequestResult = parkingLotController.AddVehicle(parkingDetails);

            Assert.IsType<BadRequestObjectResult>(BadRequestResult);
        }

        [Fact]
        public void AddUnparkData()
        {
            var OkResult = parkingLotController.ParkStatus(unparkCar);

            Assert.IsType<OkObjectResult>(OkResult);
        }

        [Fact]
        public void GetLoginDetails()
        {
            var controller = new RegistrationController(userBL, _config, parkingBL);
            UserLogin log = new UserLogin()
            {
                Email = "mohit23@gmail.com",
                Password = "mohit23",
                UserType = "Police"
            };
            var OkResult = controller.Login(log);

            Assert.IsType<OkObjectResult>(OkResult);
        }
    }
}
