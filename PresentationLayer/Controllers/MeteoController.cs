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
using System.Text;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
using PL.PLModels;
using AutoMapper;
using System.Text.RegularExpressions;
using BusinessLayer.IServices;
using DB.TableModels;
using Static.Service;
using Microsoft.AspNetCore.OutputCaching;
using BusinessLayer.ParametrModel;
using BusinessLayer.Models;
using api_fact_weather_by_city.ViewModel;
using System.Collections.Generic;
using DBLayer.Context;

namespace api_fact_weather_by_city.Controllers
{

    public class MeteoController : Controller
    {
        private ISynopServicesAsync<SynopBL> _tree_hour_meteo { get; set; }

        private ISynopServicesAsync<MeasuringAMS_BL> _ten_minute_meteo { get; set; }

        private ISynopServicesAsync<GroundData_BL> _forecast_meteo { get; set; }

        private INoDataServices<SynopBL, NullDataTable> _commercial_tree_hour { get; set; }

        private INoDataServices<MeasuringAMS_BL, NullDataTable> _commercial_ten_minute { get; set; }

        private IUserServices<UserBL> _user { get; set; }

        private IMemoryCache _cache { get; set; }

        private IMapper _mapper { get; set; }

        private ILogger _logger { get; set; }


        public MeteoController
        (
            ISynopServicesAsync<SynopBL> tree_hour_meteo,
            ISynopServicesAsync<MeasuringAMS_BL> ten_minute_meteo,
            ISynopServicesAsync<GroundData_BL> forecast_meteo,
            INoDataServices<SynopBL, NullDataTable> commercial_tree_hour,
            INoDataServices<MeasuringAMS_BL, NullDataTable> commercial_ten_minute,
            IUserServices<UserBL> user,
            IMemoryCache cache,
            IMapper mapper
,
            ILoggerProvider loggerProvider

        )
        {
            _tree_hour_meteo = tree_hour_meteo;
            _ten_minute_meteo = ten_minute_meteo;
            _cache = cache;
            _mapper = mapper;
            _forecast_meteo = forecast_meteo;
            _commercial_tree_hour = commercial_tree_hour;
            _commercial_ten_minute = commercial_ten_minute;
            _user = user;
            _logger = loggerProvider.CreateLogger("MeteoLogger");
        }

        [HttpGet]
        [AutorizationApiFilter("Meteo")]
        [AvailableParameterFilter()]
        [ServiceFilter(typeof(ResponceFormatFilter<SynopPL, SynopXML>))]
        [Route("api/ThreeHourMeteo")]
        public async Task<ContentResult> ThreeHourMeteo(string resourceId, string param, string startAt = null, string endAt = null, string orderBy = null)
        {
            _logger.LogInformation("I recieve next parameters" + HttpContext.Request.Path + HttpContext.Request.QueryString.ToString());

            string id = HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault();

            _logger.LogInformation("User ID is " + id);


            MeteoParamModel_BL meteoParam = new MeteoParamModel_BL();

            meteoParam.SetStationList(resourceId);

            meteoParam.SetParamList(param);

            meteoParam.SetOrderByList(orderBy);

            meteoParam.SetDateList(startAt, endAt);

            meteoParam.SetModelId(null);

            List<SynopBL> list = new List<SynopBL>();

            list = await _tree_hour_meteo.SelectStationParamSpecifyDays(meteoParam);

            _logger.LogInformation("Row of information  found: " + list.Count());

            _tree_hour_meteo.Dispose();

            _user.Dispose();

            NoDataHandler(list, id);

            List<SynopPL> listPL = _mapper.Map<List<SynopPL>>(list);
            List<string> addList = new List<string>(meteoParam.paramList);

            addList.Add("SynopXML");
            addList.Add("ResourceId");
            addList.Add("DateObservation");
            addList.Add("DateWriter");

            HttpContext.Items.Add("ParamListArray", addList.ToArray());

            HttpContext.Response.Headers.Add("Check-Time-Headers", DateTime.Now.ToString());

            var json = JsonConvert.SerializeObject(listPL);

            ContentResult contentResult = new ContentResult
            {
                Content = json,
                StatusCode = 200
            };

            return contentResult;

        }

        [HttpGet]
        [Route("api/TenMinuteMeteo")]
        [AutorizationApiFilter("Meteo")]
        [AvailableParameterFilter()]
        [ServiceFilter(typeof(ResponceFormatFilter<MeasuringAMS_PL, MeasuringAMS_XML>))]
        //[AvailableParameterFilter()]
        public async Task<ContentResult> TenMinuteMeteo(string resourceId, string param, string startAt = null, string endAt = null, string orderBy = null)
        {
            _logger.LogInformation("I recieve next parameters" + HttpContext.Request.Path + HttpContext.Request.QueryString.ToString());

            string id = HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault();

            _logger.LogInformation("User ID is " + id);

            MeteoParamModel_BL meteoParam = new MeteoParamModel_BL();

            meteoParam.SetStationList(resourceId);

            meteoParam.SetParamList(param);

            meteoParam.SetOrderByList(orderBy);

            meteoParam.SetDateList(startAt, endAt);

            meteoParam.SetModelId(null);

            List<MeasuringAMS_BL> list = new List<MeasuringAMS_BL>();

            list = await _ten_minute_meteo.SelectStationParamSpecifyDays(meteoParam);

            _logger.LogInformation("Row of information  found:" + list.Count());

            _ten_minute_meteo.Dispose();

            _user.Dispose();

            NoDataHandler(list, id);

            List<MeasuringAMS_PL> listPL = _mapper.Map<List<MeasuringAMS_PL>>(list);
            List<string> addList = new List<string>(meteoParam.paramList);
            //
            addList.Add("MeasuringAMS_XML");
            addList.Add("ResourceId");
            addList.Add("DateObservation");
            addList.Add("DateWriter");

            HttpContext.Items.Add("ParamListArray", addList.ToArray());

            HttpContext.Response.Headers.Add("Check-Time-Headers", DateTime.Now.ToString());

            var json = JsonConvert.SerializeObject(listPL);

            ContentResult contentResult = new ContentResult
            {
                Content = json,
                StatusCode = 200
            };

            return contentResult;
        }

        [HttpGet]
        [Route("api/MeteoForecast")]
        [AutorizationApiFilter("Meteo")]
        [AvailableParameterFilter()]
        [ServiceFilter(typeof(ResponceFormatFilter<GroundData_PL, GroundDataXML>))]
        public async Task<ContentResult> MeteoForecast(string resourceId, string param, string startAt = null, string endAt = null, string orderBy = null)
        {
            _logger.LogInformation("I recieve next parameters" + HttpContext.Request.Path + HttpContext.Request.QueryString.ToString());

            string id = HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault();

            _logger.LogInformation("User ID is " + id);

            MeteoParamModel_BL meteoParam = new MeteoParamModel_BL();

            meteoParam.SetStationList(resourceId);

            meteoParam.SetParamList(param);

            meteoParam.SetOrderByList(orderBy);

            meteoParam.SetDateList(startAt, endAt);

            meteoParam.SetModelId("4");

            List<GroundData_BL> list = new List<GroundData_BL>();

            list = await _forecast_meteo.SelectStationParamSpecifyDays(meteoParam);

            _logger.LogInformation("Row of information  found:" + list.Count());

            _forecast_meteo.Dispose();

            _user.Dispose();

            //NoDataHandler(list, id);



            List<GroundData_PL> listPL = _mapper.Map<List<GroundData_PL>>(list);
            List<string> addList = new List<string>(meteoParam.paramList);

            addList.Add("GroundDataXML");
            addList.Add("ResourceId");
            addList.Add("DateObservation");
            addList.Add("DateWriter");
            HttpContext.Items.Add("ParamListArray", addList.ToArray());
            //

            //тпм потом обработчик вернет формат какой надо
            var json = JsonConvert.SerializeObject(listPL);

            HttpContext.Response.Headers.Add("Check-Time-Headers", DateTime.Now.ToString());

            ContentResult contentResult = new ContentResult
            {
                Content = json,
                StatusCode = 200
            };

            return contentResult;
        }

        //проверка полученны ли все данные, она очень длинная по времени и локальная
        // так что запущу ее в отдельном потоке
        // и не будем ждать
        ////*надо чото еще с этим делать*
        private async Task NoDataHandler<T>(List<T> list, string userId) where T : class
        {
            Action<List<T>, IServiceProvider?, string> a = async (act_list, provider, userId) =>
            {
                // получаем доступ к юзерам
                var __user = provider.GetService<IUserServices<UserBL>>();

                // получаем доступ к таблице с ошибками
                var __commercial = provider.GetService<INoDataServices<T, NullDataTable>>();

                //получаем все доступные пользователю параметры
                string[] param = __user.GetAvailableParam(Int32.Parse(userId));

                //получаем список тех данных у которых есть null
                var error_List = __commercial.CheckNullDataInList(act_list, param);

                __user.Dispose();

                // записываем в базу список ошибок
                __commercial.PutErrorData(Int32.Parse(userId), error_List);
                await __commercial.Save();
                await __commercial.Dispose();
            };
            Task.Run(() => a(list, ServiceHandler.provider, userId));
            return;
        }


    }


}