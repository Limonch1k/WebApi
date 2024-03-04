using api_fact_weather_by_city.Filters;
using BL.Models;
using BL.Services;
using Microsoft.AspNetCore.Mvc;
using GeneralObject.StringCryptography;
using Microsoft.AspNetCore.Cors;
using Swashbuckle.AspNetCore.Annotations;
using System.Web.Http.Filters;
using Newtonsoft.Json;
using PL.ViewModel;

namespace api_fact_weather_by_city.Controllers
{
    public class AdminController : Controller
    {
        private UserServices<UserBL> _userServices {get;set;}
        public AdminController(UserServices<UserBL> userServices) 
        {
            _userServices = userServices;
        }

        [HttpPost]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "You Successfully set user right", typeof(string))]
        [ProducesResponseType(typeof(string), 403, "text/html")]
        [SwaggerResponse(403, "You dont have permission to add this role", typeof(string))]
        [AutorizationApiFilter("AdminGidroMorpf")]
        [Route("api/Admin/Role")]
        public async Task<ContentResult> SetGidroMorpfRight([FromBody]UserRole userRole)
        {
            ContentResult contentResult = new ContentResult();

            if (!(userRole.role.Equals("GidroMorph") || userRole.role.Equals("GidroMorpfAdmin") || userRole.role.Equals("AdminGidroMorpf")))
            {
                contentResult.StatusCode = 403;
                contentResult.Content = "you dont have permission for this role";
                contentResult.ContentType = "text/html";

                return contentResult;
            }

            var userId = _userServices.GetUserIdByName(userRole.username);

            _userServices.SetUserRole(userId , userRole.role);

            await _userServices.Save();

            contentResult.StatusCode = 200;
            contentResult.Content = "Ok";
            contentResult.ContentType = "text/html";

            return contentResult;
        }


        [HttpDelete]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "You Successfully set user right", typeof(string))]
        [ProducesResponseType(typeof(string), 403, "text/html")]
        [SwaggerResponse(403, "You dont have permission to add this role", typeof(string))]
        [AutorizationApiFilter("AdminGidroMorpf")]
        [Route("api/Admin/UserRole")]
        public async Task<ContentResult> RemoveGidroMorpfRight([FromBody]UserRole userRole)
        {
            ContentResult contentResult = new ContentResult();

            if (!(userRole.role.Equals("GidroMorph") || userRole.role.Equals("GidroMorpfAdmin") || userRole.role.Equals("AdminGidroMorpf")))
            {
                contentResult.StatusCode = 403;
                contentResult.Content = "you dont have permission for this role";
                contentResult.ContentType = "text/html";

                return contentResult;
            }

            var userId = _userServices.GetUserIdByName(userRole.username);

            var b = _userServices.DeleteUserRole(userId, userRole.role);

            _userServices.Save();

            if (!b)
            {
                contentResult.StatusCode = 500;
            }

            return contentResult;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "You Successfully set user right", typeof(string))]
        [ProducesResponseType(typeof(string), 403, "text/html")]
        [SwaggerResponse(403, "You dont have permission to add this role", typeof(string))]
        [AutorizationApiFilter("AdminGidroMorpf")]
        [Route("api/Admin/UserRole")]

        public async Task<ContentResult> GetUserList()
        {
            ContentResult contentResult = new ContentResult();

            //var user_list = _userServices.GetUsersByRole(new string[] {"GidroMorph", "GidroMorpfAdmin"});

            var user_list = await _userServices.GetAll();

            var json = JsonConvert.SerializeObject(user_list.Select(x => new { username = x.Username, right = x.right.Select(x => x.Source)}));

            contentResult.Content = json;

            contentResult.ContentType = "application/json";

            contentResult.StatusCode = 200;

            return contentResult;
        }

        [HttpGet]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "You Successfully set user right", typeof(string))]
        [ProducesResponseType(typeof(string), 403, "text/html")]
        [SwaggerResponse(403, "You dont have permission to add this role", typeof(string))]
        [AutorizationApiFilter("AdminGidroMorpf")]
        [Route("api/Admin/RoleList")]

        public async Task<ContentResult> GetRoleList()
        {
            ContentResult contentResult = new ContentResult();

            string[] role = new string[] {"GidroMorph", "GidroMorpfAdmin", "AdminGidroMorpf"};

            string json = JsonConvert.SerializeObject(role);

            contentResult.Content = json;

            contentResult.ContentType = "application/json";

            return contentResult;
        }

        [HttpPut]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "You Successfully set user right", typeof(string))]
        [ProducesResponseType(typeof(string), 403, "text/html")]
        [SwaggerResponse(403, "You dont have permission to add this role", typeof(string))]
        [AutorizationApiFilter("AdminGidroMorpf")]
        [Route("api/Admin/GidroMorpfUser")]

        public async Task<ContentResult> AddGidroMorpfUser([FromBody]UserLoginPL model)
        {
            ContentResult contentResult = new ContentResult();

            bool b = true;

            if (model.Username.Contains(' ') || model.Password.Contains(' '))
            {
                b = false;
            }
            else
            {
                b = _userServices.AddUser(model.Username, model.Password);
            }

            if (b)
            {
                contentResult.Content = "user was add";
                contentResult.ContentType = "text/html";
                contentResult.StatusCode = 200;
            }
            else
            {
                contentResult.Content = "user wasn't add";
                contentResult.ContentType = "text/html";
                contentResult.StatusCode = 400;
            }

            return contentResult;
        }

        [HttpDelete]
        [ProducesResponseType(typeof(string), 200, "text/html")]
        [SwaggerResponse(200, "You Successfully set up user right", typeof(string))]
        [ProducesResponseType(typeof(string), 403, "text/html")]
        [SwaggerResponse(403, "You dont have permission to add this role", typeof(string))]
        [AutorizationApiFilter("AdminGidroMorpf")]
        [Route("api/Admin/GidroMorpfUser")]
        public async Task<ContentResult> RemoveGidroMorpfUser([FromBody]UserName user)
        {
            ContentResult contentResult = new ContentResult();

            var b = _userServices.RemoveUser(user.username);


            if (b)
            {
                contentResult.Content = "user was delete";
                contentResult.ContentType = "text/html";
                contentResult.StatusCode = 200;
            }
            else
            {
                contentResult.Content = "user wasn't delete";
                contentResult.ContentType = "text/html";
                contentResult.StatusCode = 400;
            }

            return contentResult;
        }
    }
}
