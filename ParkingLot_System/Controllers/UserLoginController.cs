using BussinesLayer.Interface;
using CommonLayer.Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ParkingLot_System.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserLoginController : ControllerBase
    {
        private readonly IUserBussiness userBussiness;
        private readonly IConfiguration _config;
        public UserLoginController(IUserBussiness _userBussiness, IConfiguration config)
        {
            userBussiness = _userBussiness;
            _config = config;
        }

        // POST: api/ParkingLot
        //Add Users according to there Role
        [HttpPost]
        public ActionResult AddUser(UserDetail model)
        {
            try
            {
                var data = userBussiness.AddUserData(model);
                bool success = false;
                string message;
                if (!ModelState.IsValid)
                {
                    message = "Details not added";
                    return Ok(new { success, message });
                }
                success = true;
                message = "Data Added Successfully";
                return Ok(new { success, message, data });
            }
            catch (Exception e)
            {
                return BadRequest(new { e.Message });
            }
        }

        //GET: api/ParkingLot
        //Get all details according to there Role
        [Authorize(Roles ="Owner,Driver,Security,Police")]
        [HttpGet]
        public ActionResult GetOwnerDetails()
        {
            try
            {
                bool success = false;
                string message;
                var result = userBussiness.GetOwnerDetails();
                if (result == null)
                {
                    message = "Data not Found";
                    return Ok(new { success, message });
                }
                success = true;
                message = "Parking Details";
                return Ok(new { success, message, result });
            }
            catch(Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        //POST: api/ParkingLot
        [HttpPost]
        [Route("login")]
        public ActionResult Login(UserLogin login)
        {
            var data = userBussiness.Login(login);

            if (data == null)
            {
                bool success = false;
                string message;
                message = "Email and Password not Valid";
                return Ok(new { success, message });
            }
            else
            {
                var jsontoken = GenerateToken(login);
                bool success = true;
                string message;
                message = "Successfully Login";
                return Ok(new { success, message, data, jsontoken });
            }
        }

       

        //Generates Token for Login
        private string GenerateToken(UserLogin responseData)
        {
            try
            {
                var symmetricSecuritykey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

                var signingCreds = new SigningCredentials(symmetricSecuritykey, SecurityAlgorithms.HmacSha256);

                var claims = new List<Claim>();
                claims.Add(new Claim(ClaimTypes.Role, responseData.UserType));
                claims.Add(new Claim("Email", responseData.Email.ToString()));
                claims.Add(new Claim("Password", responseData.Password.ToString()));
                var token = new JwtSecurityToken(_config["Jwt:Issuer"],
                    _config["Jwt:Issuer"],
                    claims,
                    expires: DateTime.Now.AddMinutes(5),
                    signingCredentials: signingCreds);
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
