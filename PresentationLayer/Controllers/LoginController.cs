using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using api_fact_weather_by_city.XMLModels;
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

namespace api_fact_weather_by_city.Controllers
{
    [Route("Login")]
    public class LoginController : Controller
    {
        //private UserRegistredServices _user;

        private UserServices<UserBL> _userServices {get;set;}


        public LoginController(UserServices<UserBL> userServices) 
        {
            _userServices = userServices;
        }

        //это страничка входа

        //'это пытается зайти в свое учетку
        [HttpPost]
        [Route("LoginIn")]
        public async Task<IActionResult> Login() 
        {
            string? auth;

            if(Request.Headers.ContainsKey("Api-Key"))
            {
                auth = Request.Headers["Api-Key"];
                if (auth is not null)
                {
                    if (!auth.Equals(""))
                    {
                        /*int startUserId = auth.IndexOf("UserId:");
                        int userIdSpace = auth.Substring(startUserId + 8).IndexOf(";"); 
                        string userId = auth.Substring(startUserId + 8, userIdSpace);
                        int startPassword = auth.IndexOf("Password:");
                        int passwordSpace = auth.Substring(startPassword + 10).IndexOf(";");
                        string password = auth.Substring(startPassword + 10, passwordSpace);  */         
                        
                        if(_userServices.CheckPassword(auth))
                        {
                            int id = _userServices.GetUserId(auth);
                            //bool res = _userServices.CheckAccessToAnyPage(userId,password);

                            /*if (!res)
                            {
                                return Content("You successfull authorized!!! But you dont have any access!!!");
                            }**/
                            await Authenticate(id, auth, _userServices.GetAvailablePage(id.ToString()));

                            return Content("You successfull authorized!!!");
                        }
                        else
                        {
                            return Content("Incorect Password or Login");
                        }

                    }
                    else
                    {
                         return Content("You dont have all Authorization atribute");
                    }
                }
                else
                {
                    return Content("You Authorization header is empty");
                }
            }
            else
            {
                return Content("Set Authorization header");
            }

            return Content("You authorization was succesfull");
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