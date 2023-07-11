using api_fact_weather_by_city.Filters;
using BL.IServices;
using BL.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace api_fact_weather_by_city.Controllers
{
    public class UserController : Controller
    {
        private IUserServices<UserBL> _user { get; set; }

        [AutorizationApiFilter("api/Cabinet")]
        public async Task<ContentResult> GetParamList() 
        {
            ContentResult contentResult = new ContentResult();
            string user_id = HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault();
            var list = _user.GetAvailableParam(Int32.Parse(user_id));
            var json = JsonSerializer.Serialize(list);
            contentResult.Content = json;
            return contentResult;

        }

        [AutorizationApiFilter("api/Cabinet")]
        public async Task<ContentResult> GetResourceIdList() 
        {
            ContentResult contentResult = new ContentResult();
            string user_id = HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault();
            var list = _user.GetAvailablePunkt(Int32.Parse(user_id));
            var json = JsonSerializer.Serialize(list);
            contentResult.Content = json;
            return contentResult;
        }

        public async Task<ContentResult> GetPageList() 
        {
            ContentResult contentResult = new ContentResult();
            string user_id = HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault();
            var list = _user.GetAvailablePage(user_id);
            var json = JsonSerializer.Serialize(list);
            contentResult.Content = json;
            return contentResult;
        }
    }


}
