using api_fact_weather_by_city.Filters;
using Microsoft.AspNetCore.Mvc;

namespace api_fact_weather_by_city.Controllers
{
    public class AdminController : Controller
    {
        public AdminController() 
        {

        }

        [HttpPut]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/ApiKey")]
        public async Task<ContentResult> CreateApiKeyToUserById(string user_id, string api_key = null) 
        {



            return null;
        }

        [HttpGet]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/ApiKey")]
        public async Task<ContentResult> GetApiKeyByUserId(string user_id) 
        {

            return null;
        }

        [HttpPost]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/ApiKey")]
        public async Task<ContentResult> UpdateApiKeyByUserId(string user_id, string api_key = null) 
        {
            return null;
        }

        [HttpDelete]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/ApiKey")]
        public async Task<ContentResult> DeleteApiKeyByUserId(string user_id) 
        {
            return null;
        }

        [HttpPut]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/User")]
        public async Task<ContentResult> AddUser(string user_id, string api_key = null, string access_key = null) 
        {
            return null;
        }

        [HttpGet]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/User")]
        public async Task<ContentResult> GetUser(string user_id)
        {
            return null;
        }

        [HttpPost]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/User")]
        public async Task<ContentResult> UpdateUser(string user_id, string api_key = null, string access_key = null)
        {
            return null;
        }

        [HttpDelete]
        [AutorizationApiFilter("Admin")]
        [Route("api/admin/User")]
        public async Task<ContentResult> DeleteUser(string user_id)
        {
            return null;
        }
    }
}
