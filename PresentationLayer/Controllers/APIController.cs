using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using api_fact_weather_by_city.XMLModels;
using System.Xml.Serialization;
using api_fact_weather_by_city.Filters;
using Microsoft.AspNetCore.Authorization;
using BL.Services;
using BL.Models;
using BL.IServices;
using System.Text.Json;
using System.Text.Encodings.Web;
using System.Text.Unicode;
using Microsoft.Extensions.Caching.Memory;

namespace api_fact_weather_by_city.Controllers
{
    
    public class APIController : Controller
    {
        private ISynopServicesAsync<AverageTempBL> _cligts {get;set;} 

        private IMemoryCache _cache {get;set;}

        public APIController(ISynopServicesAsync<AverageTempBL> cligts, IMemoryCache cache)
        {
            _cligts = cligts;
            _cache = cache;
        }

        [HttpGet]
        [AutorizationApiFilter("Info")]
        [Route("api/getTodayWeather")]
        public async  Task<ContentResult> GetInformationAboutResourceProvided()
        {
            ContentResult contentResult = new ContentResult();
            string content = "{ResourceList:{MeteoFact, MeteoForecast, GidroFact}}";
                
            return contentResult;
        }

        [HttpGet]
        [AutorizationApiFilter("Info")]
        [Route("api/InformationAboutResource")]

        public async Task<ContentResult> InformationAboutResource(string ResourceName)
        {
            ContentResult contentResult = new ContentResult();
            string content = "";
            if (ResourceName.Equals("MeteoFact")) 
            {
                content = "To get data by 3 hour term you can create the same request by template below: \\n " +
                    "{ api/getThreeHourMeteo?[resourceId=111,222,...,...]\\n" +
                    "&[param=Temp,Precip,...,...]\\n" +
                    "&[startAt=2023-01-10 20:00:00, \'yyyy-mm-dd hh-MM-ss\', ...]\\n" +
                    "&[endAt=2023-01-10 20:00:00, 'yyyy-mm-dd hh-MM-ss', ...]}\\n" +
                    "&[orderBy=Temp,Precip,...,...]\\n" +
                    "You can mix or swap order of this parameters or dont specify all of them\\n" +
                    "Then Api will work on parameters that you have already paid\\n" +
                    "\\n" +
                    "\\n" +
                    "\\n" +
                    "To get data by 10 minutes term you can create the same request by template below: \\n " +
                    "{ api/getTenMinuteMeteo?[resourceId=111,222,...,...]\\n" +
                    "&[param=Temp,Precip,...,...]\\n" +
                    "&[startAt=2023-01-10 20:00:00, \'yyyy-mm-dd hh-MM-ss\', ...]\\n" +
                    "&[endAt=2023-01-10 20:00:00, 'yyyy-mm-dd hh-MM-ss', ...]}\\n" +
                    "&[orderBy=Temp,Precip,...,...]\\n" +
                    "You can mix or swap order of this parameters or dont specify all of them\\n" +
                    "Then Api will work on parameters that you have already paid\\n";
            }

            if (ResourceName.Equals("MeteoForecast"))
            {
                content = "To get data you can create the same request by template: \\n " +
                    "{ api/getMeteoForecast?[resourceId=111,222,...,...]\\n" +
                    "&[param=Temp,Precip,...,...]\\n" +
                    "&[startAt=2023-01-10 20:00:00, \'yyyy-mm-dd hh-MM-ss\', ...]\\n" +
                    "&[endAt=2023-01-10 20:00:00, 'yyyy-mm-dd hh-MM-ss', ...]}\\n" +
                    "&[orderBy=Temp,Precip,...,...]\\n" +
                    "You can mix or swap order of this parameters or dont specify all of them\\n" +
                    "Then Api will work on parameters that you have already paid";
            }
            return null;          
        }
    
    }
}