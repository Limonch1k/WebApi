using api_fact_weather_by_city.Filters;
using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.ViewModel;
using api_fact_weather_by_city.XMLModels;
using AutoMapper;
using BusinessLayer.IServices;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using PL.PLModels;
using System.Collections.Generic;

namespace api_fact_weather_by_city.Controllers
{
    public class GidroMorpfController : Controller
    {

        private IGidroMorpfServices _gidroMorpf { get; set; }

        private IMapper _mapper {get;set;}

        public GidroMorpfController(IGidroMorpfServices gidroChemistry, IMapper mapper) 
        {
            _gidroMorpf = gidroChemistry;
            _mapper = mapper;
        }


        [HttpGet]
        [Route("api/GidroMorpf/PooList")]
        //[ServiceFilter(typeof(ResponceFormatFilter<string, string>))]
        public async Task<ContentResult> PoolList()
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetPoolList();
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;

            return contentResult;
        }

        [HttpGet]
        [Route("api/GidroMorpf/RiverList")]
        //[ServiceFilter(typeof(ResponceFormatFilter<string, string>))]
        public async Task<ContentResult> RiverList()
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetRiverList();
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;

            return contentResult;
            
        }

        [HttpGet]
        [Route("api/GidroMorpf/ResourceIdList")]
        //[ServiceFilter(typeof(ResponceFormatFilter<string, string>))]
        public async Task<ContentResult> ResourceIdList() 
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetResourceIdList();
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;

            return contentResult;
        }

        [HttpGet]
        [Route("api/GidroMorpf/CategoryList")]

        public async Task<ContentResult> CategoryList(bool indicators = false)

        {
            ContentResult contentResult = new ContentResult();
            List<GmfCategoryBL> list;
            if (indicators is false)
            {
                list = _gidroMorpf.GetCategoryList(false);
            }
            else 
            {
                list = _gidroMorpf.GetCategoryList(true);
            }
            
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;
            return contentResult;

        }

        [HttpGet]
        [Route("api/GidroMorpf/Protocols")]
        public async Task<ContentResult> GetProtocols(string kod, string year) 
        {
            ContentResult contentResult = new ContentResult();
            List<GmfProtocolBL> protocol;
            protocol = _gidroMorpf.GetProtocols(kod, year);

            var json = JsonConvert.SerializeObject(protocol);
            contentResult.Content = json;
            return contentResult;
        }

        [HttpPut]
        [Route("api/GidroMorpf/Protocols")]
        [ServiceFilter(typeof(InputModelFilter<GmfProtocolJson, GmfProtocolPL>))]
        public async Task<ContentResult> PutProtocols(List<GmfProtocolPL> models) 
        {
            ContentResult contentResult = new ContentResult();

            var models_bl = _mapper.Map<List<GmfProtocolBL>>(models);

            var succ = _gidroMorpf.InsertProtocols(models_bl);

            if (succ)
            {
                contentResult.StatusCode = 200;
            }
            else 
            {
                contentResult.StatusCode = 300;
            }
            

            return contentResult;
        }

        //public async Task<ContentResult> GetProtocols(string kod, string year,)

        /*public async Task<ContentResult> GetProtocolKod(string bass, string river) 
        {
            ContentResult contentResult = new ContentResult();
            int kod;

            kod = _gidroMorpf.GetProtocolKod(bass,river);
            var json = JsonConvert.SerializeObject();
            return contentResult;
        }*/

        [HttpGet]
        [Route("api/GidroMorpf/IndicatorList")]

        public async Task<ContentResult> IndicatorList(string kod = null, string year = null)
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetIndicatorList(kod, year);

            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;
            return contentResult;

        }

        [HttpGet]
        [Route("api/GidroMorpf/PunktList")]
        [ServiceFilter(typeof(ResponceFormatFilter<GmfPunctBL, GmfPunctBL>))]
        public async Task<ContentResult> GePunktList()
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetPunctList();

            var json = JsonConvert.SerializeObject(list);

            List<string> addList = new List<string>();

            addList.Add("GmfPunctBL");
            addList.Add("Kod");
            addList.Add("Bass");
            addList.Add("Punkt");
            addList.Add("River");

            HttpContext.Items.Add("ParamListArray", addList.ToArray());

            contentResult.Content = json;
            contentResult.ContentType = "application/json";
            return contentResult;

        }
    }
}
