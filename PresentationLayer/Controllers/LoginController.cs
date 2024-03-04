using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using System.Xml.Serialization;
using api_fact_weather_by_city.Filters;
using PL.ViewModel;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using BL.Services;
using BL.Models;
using System.Text;
using Newtonsoft.Json;
using GeneralObject.StringCryptography;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.AspNetCore.SwaggerUI;

namespace api_fact_weather_by_city.Controllers
{
    public class LoginController : Controller
    {
        //private UserRegistredServices _user;

        private UserServices<UserBL> _userServices {get;set;}


        public LoginController(UserServices<UserBL> userServices) 
        {
            _userServices = userServices;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string[]), 200, "application/json")]
        [SwaggerResponse(200, "Success", typeof(string[]))]
        [ProducesResponseType(typeof(string), 400, "text/html")]
        [SwaggerResponse(400, "Your request to bad", typeof(string))]
        [ProducesResponseType(typeof(string), 500, "text/html")]
        [SwaggerResponse(500, "My code to bad", typeof(string))]
        [Route("api/LogIn/User")]
        public async Task<IActionResult> LogIn([FromBody] UserLoginPL userLogin)
        {
            if (userLogin is not null)
            {
                var str = userLogin.Username + " " + userLogin.Password;

                var encrypt = str.EncryptString();

                var str_decrypr = encrypt.DecryptString();
            
                if(_userServices.CheckUserPassword(str))
                {
                    int id = _userServices.GetUserIdByKey(encrypt);

                    string[] role = _userServices.GetAvailablePage(id.ToString());

                    await Authenticate(id, encrypt, role);

                    var json = JsonConvert.SerializeObject(role);

                    var b = new ContentResult() { Content = json, ContentType = "application/json", StatusCode = 200 };

                    return b;
                }
                else
                {
                    var b = new ContentResult() { Content = "Incorect Password or Login", ContentType = "text/html", StatusCode = 401 };
                    return b;
                }          
            }

            return Content("You dont have all Authorization atribute");
        }
            
        
        [HttpPost]
        [ProducesResponseType(typeof(string[]), 200, "application/json")]
        [SwaggerResponse(200, "Success")]
        [ProducesResponseType(typeof(string), 400, "text/html")]
        [SwaggerResponse(400, "Your request to bad", Description = "Your request to bad")]
        [ProducesResponseType(typeof(string), 500, "text/html")]
        [SwaggerResponse(500, "My code to bad")]
        [Route("api/LogIn/Key")]
        public async Task<IActionResult> Login([FromHeader(Name = "Api-Key")] string ApiKey) 
        {
            string? auth;

            if(!string.IsNullOrEmpty(ApiKey))
            {
                if (!string.IsNullOrWhiteSpace(ApiKey))   
                {          
                    if(_userServices.CheckPassword(ApiKey))
                    {
                        int id = _userServices.GetUserId(ApiKey);

                        string[] role = _userServices.GetAvailablePage(id.ToString());

                        var json = JsonConvert.SerializeObject(role);

                        await Authenticate(id, ApiKey, role);

                        if(role.Count() == 0)
                        {
                            //return new ContentResult() {Content = "{\n \n}", ContentType = "text/html", StatusCode= 200};
                            return new ContentResult() {Content = json, ContentType = "application/json", StatusCode= 200};
                        }

                        //var b = new ContentResult() { Content = "{\n\t" + string.Join(",\n\t", role) + "\n}", ContentType = "text/html", StatusCode = 200 };

                        var b = new ContentResult() { Content = json, ContentType = "application/json", StatusCode = 200 };

                        return b;
                    }
                    else
                    {
                        var b = new ContentResult() { Content = "Incorect Password or Login", ContentType = "text/html", StatusCode = 401 };
                        return b;
                    }

                }
                else
                {
                        return Content("You dont have all Authorization atribute");
                }
            }
            else
            {
                return Content("Set Authorization header");
            }

            return Content("You authorization was succesfull");
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "Success", typeof(string))]
        [ProducesResponseType(typeof(string), 400, "text/html")]
        [SwaggerResponse(400, "Your request to bad", typeof(string))]
        [ProducesResponseType(typeof(string), 500, "text/html")]
        [SwaggerResponse(500, "My code to bad", typeof(string))]
        [Route("api/LogOut")]
        public async Task<IActionResult> LogOut() 
        {
            foreach (var cookie in Request.Cookies.Keys)
            {
                Response.Cookies.Delete(cookie);
            }
            return new ContentResult() { Content = "You Succesfull logout!!!!" , ContentType = "text/html", StatusCode = 200};
        }

        //это куки ставит
        public async Task Authenticate(int user_Id, string user_password, string[] Role)
        {

            var list = new List<Claim>();

            if (Role is not null)
            {
                 foreach(var r in Role)
                 {
                    list.Add(new Claim(r, r));
                 }
            }

            var claims = new List<Claim>
            {                              
                new Claim("Id", user_Id.ToString()),
                new Claim("pass", user_password)
            };

            claims.AddRange(list);

            var claimsIdentity = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
        }
    }
}