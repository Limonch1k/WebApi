using api_fact_weather_by_city.Filters;
using api_fact_weather_by_city.JSONModels._29._131;
using api_fact_weather_by_city.ViewModel;
using AutoMapper;
using AutoMapper.Internal;
using BusinessLayer.IServices;
using BusinessLayer.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Swashbuckle.AspNetCore.Annotations;
using Swashbuckle.Examples;
using System.Collections.Generic;
using System.ComponentModel;

namespace api_fact_weather_by_city.Controllers
{
    public class GidroMorpfController : Controller
    {
        //это интерфейс бизнес уровня который предоставляет функционал бизнесс уровня, не важно какой реализации
        private IGidroMorpfServices _gidroMorpf { get; set; }

        // это интерфейс маппера , который служит для преобразование одних уровней и форматов моделей данных в другие уровнии и форматы моделей данных 
        private IMapper _mapper {get;set;}

        // это контроллер с параметрами объектов , которые будут использоваться в классе, контроллер вызывает DI технология, которая сама туда передает реализацию параметров,
        //которую посчитает нужной сделать , в зависимости от наших настроек в startup.cs 
        public GidroMorpfController(IGidroMorpfServices gidroChemistry, IMapper mapper) 
        {
            _gidroMorpf = gidroChemistry;
            _mapper = mapper;
        }

        /// <summary>
        /// эндпоинт который предоставляет список всех бассейнов с участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]
        [Route("api/GidroMorpf/BassList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string[]), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You Successfull get list of pools", typeof(string[]))]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description= "This endpoint provide you data about list of pool that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(ResponceFormatFilter<string, string>))]
        public async Task<ContentResult> GetBassList([FromHeader(Name = "Content-Type")] string ContentType = "application/json")
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetBassList();
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;

            return contentResult;
        }

        [HttpDelete]
        [Route("api/GidroMorpf/BassList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]

        public async Task<ContentResult> DeleteBassList([FromBody] string[] poolList)
        {
            ContentResult contentResult = new ContentResult();
            List<string> wrong_bass = new List<string>();
            bool b = true;
            try
            {
                if(!(b = _gidroMorpf.DeleteBass(poolList)))
                {
                    wrong_bass.Add(JsonConvert.SerializeObject( new {Message = "Something went wrong with you data!!!", body = new string[0]}));
                }
            }
            catch(CustomException e)
            {
                wrong_bass.Add(e.Message);
            }
            catch(Exception e)
            {
                b = false;
            }

            
            if(wrong_bass.Count() == 0 && b == true)
            {
                contentResult.StatusCode = 200;
                contentResult.ContentType = "text/html";
                contentResult.Content = "Ok";
            }
            else if(b == false && wrong_bass.Count() == 0)
            {
                contentResult.StatusCode = 500;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_bass);
            }
            else if (wrong_bass.Count() != 0)
            {
                contentResult.StatusCode = 400;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_bass);
            } 

            return contentResult;
        }

        [HttpPost]
        [Route("api/GidroMorpf/BassList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]

        public async Task<ContentResult> UpdateBassList([FromBody] List<GmfTwoBassJson> two_river)
        {
            ContentResult contentResult = new ContentResult();

            foreach(var t in two_river)
            {
                string _new = t.New;
                string _old = t.Old;

                var b = await  _gidroMorpf.UpdateBass(_new, _old);
                if (b == false)
                {
                    contentResult.StatusCode = 400;
                }
            }

            return contentResult;
        }

        [HttpPut]
        [Route("api/GidroMorpf/BassList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]

        public async Task<ContentResult> InsertBassList([FromBody] string[] poolList)
        {
            ContentResult contentResult = new ContentResult();

            bool b = true;
            b = _gidroMorpf.InsertBass(poolList);

            if (b = false)
            {
                contentResult.StatusCode = 400;
            }

            return contentResult;
        }

        [HttpGet]
        [Route("api/GidroMorpf/RegionList")]
        [AutorizationApiFilter("GidroMorph")]

        public async Task<ContentResult> GetRegionList()
        {
            ContentResult contentResult = new ContentResult();

            List<GmfRegionBL> list  = null;
            try
            {
                list = await _gidroMorpf.GetRegionList();    
            }
            catch
            {
                contentResult.StatusCode = 500;
                contentResult.Content = "errore occure in web application";
                contentResult.ContentType = "text/html";
                return contentResult;
            }
            

            var json = JsonConvert.SerializeObject(list);

            contentResult.Content = json;

            return contentResult;
        }

        [HttpPost]
        [Route("api/GidroMorpf/RegionList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        public async Task<ContentResult> UpdateRegionList([FromBody] List<GmfUpdateRegion> regionList)
        {
            ContentResult contentResult = new ContentResult();

            List<string> wrong_region = new List<string>();

            foreach(var reg in regionList)
            {
                var old_region_name = reg.Old;

                var new_region_name = reg.New;

                var b = await _gidroMorpf.UpdateRegion(old_region_name, new_region_name);

                if (b = false)
                {
                    wrong_region.Add(reg.Old + " - " + reg.New);
                }
            }

            if (wrong_region.Count() == 0)
            {
                contentResult.Content = "Ok";
                contentResult.ContentType = "text/html";
                contentResult.StatusCode = 200;
            }
            else
            {
                //var json = JsonConvert.SerializeObject(wrong_region);
                contentResult.Content = String.Join(",", wrong_region);
                contentResult.ContentType = "application/json";
                contentResult.StatusCode = 400;
            }

            return contentResult;
        }

        [HttpDelete]
        [Route("api/GidroMorpf/RegionList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]

        public async Task<ContentResult> DeleteRegionList([FromBody] string[] region_list)
        {
            ContentResult contentResult = new ContentResult();
            List<string> wrong_region = new List<string>();

            int count = 0;

            bool b = true;

            foreach(var reg in region_list)
            {
                try
                {
                    if(!(b = await _gidroMorpf.DeleteRegion(reg)))
                    {
                        count++;
                        wrong_region.Add(JsonConvert.SerializeObject( new {Message = "Something went wrong with you data!!!", body = new string[0]}));
                    }
                }
                catch(CustomException e)
                {
                    wrong_region.Add(e.Message);
                }
                catch(Exception e)
                {
                    b = false;
                }
            }

            if(wrong_region.Count() == 0 && b == true)
            {
                contentResult.StatusCode = 200;
                contentResult.ContentType = "text/html";
                contentResult.Content = "Ok";
            }
            else if(b == false && wrong_region.Count() == 0)
            {
                contentResult.StatusCode = 500;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_region);
            }
            else if (wrong_region.Count() != 0)
            {
                contentResult.StatusCode = 400;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_region);
            } 



            return contentResult;

        }

        [HttpPost]
        [Route("api/GidroMorpf/RiverList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        
        public async Task<ContentResult> UpdateRiverList([FromBody] List<GmfUpdateRiver> riverList)
        {
            ContentResult contentResult = new ContentResult();

            bool b = true;

            var list_new = riverList.Select(x => x.New).ToList();

            var list_old = riverList.Select(x => x.Old).ToList();

            var list_bl_new = _mapper.Map<List<GmfRiverBL>>(list_new);

            var list_bl_old = _mapper.Map<List<GmfRiverBL>>(list_old);

            b = _gidroMorpf.UpdateRiverList(list_bl_new, list_bl_old);

            if (b == false)
            {
                contentResult.StatusCode = 400;
            }

            return contentResult;
        }

        [HttpDelete]
        [Route("api/GidroMorpf/RiverList")]
        [AutorizationApiFilter("GidroMorpfAdmin")]

        public async Task<ContentResult> DeleteRiverList([FromBody] List<GmfRiverPL> riverList)
        {
            ContentResult contentResult = new ContentResult();
            List<string> wrong_river = new List<string>();
            var list_bl = _mapper.Map<List<GmfRiverBL>>(riverList);
            var b = true;

            try
            {
                if(!(b = await  _gidroMorpf.DeleteRiverList(list_bl)))
                {
                    wrong_river.Add(JsonConvert.SerializeObject( new {Message = "Something went wrong with you data!!!", body = new string[0]}));
                }
            }
            catch(CustomException e)
            {
                wrong_river.Add(e.Message);
            }
            catch(Exception e)
            {
                b = false;
            }

            if(wrong_river.Count() == 0 && b == true)
            {
                contentResult.StatusCode = 200;
                contentResult.ContentType = "text/html";
                contentResult.Content = "Ok";
            }
            else if(b == false && wrong_river.Count() == 0)
            {
                contentResult.StatusCode = 500;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_river);
            }
            else if (wrong_river.Count() != 0)
            {
                contentResult.StatusCode = 400;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_river);
            } 



            return contentResult;
        }

        //эндпоинт который предоставляет список всех рек с базы гидроморфологии
        // Get так как метод предостаялет функционал чтения данных

        /// <summary>
        /// эндпоинт который предоставляет список всех рек с участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]
        [Produces("application/json")]
        [Route("api/GidroMorpf/RiverList")]
        [ProducesResponseType(typeof(List<GmfRiverBL?>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You Successfull get list of rivers", typeof(string[]))]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description= "This endpoint provide you data about list of river that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(ResponceFormatFilter<string, string>))]
        public async Task<ContentResult> RiverList([FromHeader(Name = "Content-Type")] string ContentType = "application/json")
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetRiverList();
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;

            return contentResult;
            
        }

        //[HttpPost]
        //[AutorizationApiFilter("GidroMorph")]
        //[Route("api/GidroMorpf/RiverList")]
        //[ServiceFilter(typeof(InputModelFilter<GmfRiverJson, GmfRiverPL>))]
        /*public async Task<ContentResult> RiverListUpdate([FromBody]List<GmfRiverPL> models)
        {
            ContentResult contentResult = new ContentResult();
            var dict_bl = _mapper.Map<List<GmfRiverBL>>(models);
            var b = _gidroMorpf.InsertRiverList(dict_bl);
            return contentResult;
        }*/
        //эндпоинт который предоставляет список всех не помню чего с базы гидроморфологии, ну и не важно так как Кирилл этим эндпоинтом уже не пользуется
        // Get так как метод предостаялет функционал чтения данных

        /// <summary>
        /// эндпоинт который предоставляет список всех пунктов населенных участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        [Route("api/GidroMorpf/ResourceIdList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(string[]), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You Successfull get list of inhabited punkt", typeof(string[]))]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description= "This endpoint provide you data about list of inhabited punkt that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(ResponceFormatFilter<string, string>))]
        public async Task<ContentResult> ResourceIdList([FromHeader(Name = "Content-Type")] string ContentType = "application/json") 
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetResourceIdList();
            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;

            return contentResult;
        }

        // эндпоинт который предоставляет список всех значений с константной таблицы Category в базе гидроморфологии
        // Get так как метод предостаялет функционал чтения данных
        // параметр indicators указывает нужно ли возвращать для записи связанную сущность(строку) из таблицы indicators

        /// <summary>
        /// эндпоинт который предоставляет список всех категорий участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]
        [Route("api/GidroMorpf/CategoryList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GmfCategoryBL>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You Successfull get list of category", typeof(List<GmfCategoryBL>))]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description= "This endpoint provide you data about list of category that take part in gidromorpfology analyze")]
        [ProducesResponseType(500)]
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

        // эндпоинт который предоставляет список всех значений с константной таблицы Category в базе гидроморфологии
        // Get так как метод предостаялет функционал чтения данных
        // параметр year является фильтром по столбцу year в таблице Protocols
        // параметр kod является фильтром по столбцу kod в таблице Protocols
        // параметр bass является фильтром по столбцу bass в таблице Protocols
        // параметр river является фильтром по столбцу river в таблице Protocols
        // параметр punkt является фильтром по столбцу punkt в таблице Protocols
        // параметр pasp является фильтром по столбцу pasp в таблице Protocols
        // параметр region является фильтром по столбцу region в таблице Protocols
        // параметр indicators указывает нужно ли возвращать все поля из таблицы Protocols, или только те что являются уникальными для таблицы и соответсвенно не повторяются, желание фронтендера
        // комбинация параметров является желанием фронтендера
        // если в базе значений по указанным параметрам не оказалось то в теле запроса вернется []
        // если оказалось 1 и более записи , то они возвращаются как массив json

        /// <summary>
        /// эндпоинт который предоставляет список всех протоколов участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]
        [Route("api/GidroMorpf/Protocols")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GmfProtocolBL>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You Successfull get list of protocols", typeof(string[]))]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description= "This endpoint provide you data about list of protocols that take part in gidromorpfology analyze")]

        public async Task<ContentResult> GetProtocols(string year = null,string kod = null, string bass = null, string river = null, string punkt = null,string pasp=null,string region = null, bool indicator = false) 
        {
            ContentResult contentResult = new ContentResult();
            List<GmfProtocolBL> protocol;
            /*if (year is not null && kod is not null)
            {
                protocol = _gidroMorpf.GetProtocols(year, kod,indicator);
            }
            else*/ if(year is null && kod is null && bass is null && river is null && punkt is null && pasp is null && region is null)
            {
                protocol = _gidroMorpf.GetProtocols(indicator);
            }
            else
            {
                protocol = _gidroMorpf.GetProtocols(year,kod, bass, river, punkt,pasp, region, indicator);
            }

            var protocol_pl = _mapper.Map<List<GmfProtocolPL>>(protocol);

            var protocol_json = _mapper.Map<List<GmfProtocolJson>>(protocol_pl);
            
            string json = "";

            if (protocol is null || protocol.Count() == 0)
            {
                json = "[]";
                contentResult.Content = json;
                return contentResult;
            }

            json = JsonConvert.SerializeObject(protocol_json);
            contentResult.Content = json;
            return contentResult;
        }

        // эндпоинт который предоставляет функционал записи списка объектов соответствующих строкам в таблице Protocols в базе гидроморфологии
        // параметр метода models типа List<GmfProtocolPL> модели формируется в фильтре InputModelFilter из тела запроса, проходя там легкий парсинг и приведение типов если они не совпадают
        // я подумал что так будет получше
        // инормативность ответов кончено плохая , надо нармальные сделать статусы кодов сделать и нормальные к ним текстовые сообщения.

        /// <summary>
        /// эндпоинт который предоставляет список всех бассейнов с участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns> 
        [HttpPut]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        [Route("api/GidroMorpf/Protocols")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You Successfully add new or rewrite old list of protocols")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description= "This endpoint provide you availability to add list of protolcs that take part in gidromorpfology analyze")]
        //[SwaggerRequestExample(typeof(List<GmfProtocolJson>), typeof(GmfProtocolExample))]
        //[ServiceFilter(typeof(InputModelFilter<GmfProtocolJson, GmfProtocolPL>))]
        public async Task<ContentResult> PutProtocols([FromBody]List<GmfProtocolJson> models) 
        {
            ContentResult contentResult = new ContentResult();

            var models_pl = _mapper.Map<List<GmfProtocolPL>>(models);

            var models_bl = _mapper.Map<List<GmfProtocolBL>>(models_pl);

            var succ = _gidroMorpf.InsertProtocols(models_bl);

            if (succ)
            {
                contentResult.StatusCode = 200;
            }
            else 
            {
                contentResult.StatusCode = 500;
            }
            

            return contentResult;
        }

        //эндпоинт который предоставляет функционал удаления строк в таблице Protocols в базе гидроморфологии по уникальынм ключам
        // параметр kod соотвествует полю kod в таблице Protocols
        // параметр year соотвествует полю year в таблице Protocols
        // инормативность ответов кончено плохая , надо нармальные сделать статусы кодов сделать и нормальные к ним текстовые сообщения.

        /// <summary>
        /// эндпоинт который предоставляет список всех бассейнов с участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpDelete]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        [Route("api/GidroMorpf/Protocols")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succefully delete protocols")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description = "This endpoint provide you availability to delete list of protocols that doesnt take part in gidromorpfology analyze anymore")]
        public async Task<ContentResult> DeleteProtocols([FromBody]GmfDelProtocols model)
        {
            ContentResult contentResult = new ContentResult();

            var boolean = _gidroMorpf.DeleteProtocols(model.kod, model.year);

            if(boolean)
            {
                contentResult.StatusCode = 200;
            }
            else
            {
                contentResult.StatusCode = 500;
            }

            return contentResult;
        }
        //эндпоинт который предоставляет функционал изменения строк в таблице Protocols в базе гидроморфологии по уникальынм ключам
        // параметр метода models типа List<GmfProtocolPL> формируется в фильтре InputModelFilter из тела запроса, проходя там легкий парсинг и приведение типов если они не совпадают
        // я подумал что так будет получше
        // инормативность ответов кончено плохая , надо нармальные сделать статусы кодов сделать и нормальные к ним текстовые сообщения.

        /// <summary>
        /// эндпоинт который предоставляет возможность записи списка протоколов переданных в теле запроса
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpPost]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        [Route("api/GidroMorpf/Protocols")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully change list of protocols")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you availability to change a list of protocols that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(InputModelFilter<GmfProtocolJson, GmfProtocolPL>))]

        public async Task<ContentResult> UpdateProtocols([FromBody]List<GmfProtocolPL> models)
        {
            ContentResult contentResult = new ContentResult();

            var models_bl = _mapper.Map<List<GmfProtocolBL>>(models);

            var succ = _gidroMorpf.UpdateProtocols(models_bl);

            if (succ)
            {
                contentResult.StatusCode = 200;
            }
            else
            {
                contentResult.StatusCode = 500;
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

        //эндпоинт который предоставляет список всех значений в константной таблице Indicators
        // параметр kod соотвествует полю kod в таблице Protocols
        // параметр year соотвествует полю year в таблице Protocols

        /// <summary>
        /// эндпоинт который предоставляет список всех индикаторов служащих описанием параметров в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]   
        [Route("api/GidroMorpf/IndicatorList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GmfIndicatorBL>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully get list of indicators")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you data about list of indicators that describe protocols parameters that take part in gidromorpfology analyze")]
        public async Task<ContentResult> IndicatorList(string kod = null, string year = null)
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetIndicatorList(kod, year);        

            var json = JsonConvert.SerializeObject(list);
            contentResult.Content = json;
            return contentResult;

        }

        // эндпоинт который предоставляет функционал изменения строк в таблице Protocols в базе гидроморфологии по уникальным ключам
        // формат выдоваемого ответа формируется в фильтре ResponceFormatFilter из списка моделей которые вернул business уровент, проходя там преобразование к формату json или xml, а также выводя только те поля, что фронтендер захотел
        // я подумал что так будет получше
        // инормативность ответов кончено плохая , надо нармальные сделать статусы кодов сделать и нормальные к ним текстовые сообщения.

        /// <summary>
        /// эндпоинт который предоставляет список всех населенных пунктов участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]  
        [Route("api/GidroMorpf/PunktList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GmfPunctBL>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully get list of pukts")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you data about list of inhabited punkt that take part in gidromorpfology analyze")]
        [ServiceFilter(typeof(ResponceFormatFilter<GmfPunctBL, GmfPunctBL>))]
        public async Task<ContentResult> GetPunktList(string pasp = null, bool? trans=null, [FromHeader(Name = "Content-Type")] string ContentType = "application/json")
        {
            ContentResult contentResult = new ContentResult();
            var list = _gidroMorpf.GetPunctList(pasp, trans);

            var list_pl = _mapper.Map<List<GmfPunctPL>>(list);

            var json = JsonConvert.SerializeObject(list_pl);

            List<string> addList = new List<string>();
            addList.Add("GmfPunctBL");
            addList.Add("Kod");
            addList.Add("Bass");
            addList.Add("Punkt");
            addList.Add("River");
            addList.Add("Pasp");
            addList.Add("Region");
            addList.Add("Trans");

            HttpContext.Items.Add("ParamListArray", addList.ToArray());

            contentResult.Content = json;
            contentResult.ContentType = "application/json";
            return contentResult;
        }

        /// <summary>
        /// эндпоинт который предоставляет возможность добавления списока населенных пунктов для участия их в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpPut]
        [AutorizationApiFilter("GidroMorpfAdmin")]  
        [Route("api/GidroMorpf/PunktList")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GmfPunctJson>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully add new or rewriteold punkt")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you availability to change data of list with inhabited punkt that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(InputModelFilter<GmfPunctJson, GmfPunctPL>))]
        public async Task<ContentResult> PutPunktList([FromBody]List<GmfPunctPL> models)
        {
            ContentResult contentResult = new ContentResult();

            var models_bl = _mapper.Map<List<GmfPunctBL>>(models);

            var succ = _gidroMorpf.InputPunctList(models_bl);

            if (succ)
            {
                contentResult.StatusCode = 200;
            }
            else
            {
                contentResult.StatusCode = 500;
            }

            return contentResult;
        }
        /// <summary>
        /// эндпоинт который предоставляет возможность обновления информации о населенных пунктов участвующих в гидроморфологическом анализе
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpPost]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        [Route("api/GidroMorpf/Punct")]
        [Produces("application/json")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully add new or rewriteold punkt")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you availability to change data of list with inhabited punkt that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(InputModelFilter<GmfPunctJson, GmfPunctPL>))]

        public async Task<ContentResult> UpdatePunct([FromBody]List<GmfPunctPL> models)
        {
            ContentResult contentResult = new ContentResult();

            var models_bl = _mapper.Map<List<GmfPunctBL>>(models);

            var succ = _gidroMorpf.InputPunctList(models_bl);

            if (succ)
            {
                contentResult.StatusCode = 200;
            }
            else
            {
                contentResult.StatusCode = 500;
            }

            return contentResult;
        }

        [HttpDelete]
        [AutorizationApiFilter("GidroMorpfAdmin")]
        [Route("api/GidroMorpf/PunctList")]

        public async Task<ContentResult> DeletePunctList([FromBody]int[] models)
        {
            ContentResult contentResult = new ContentResult();
            var wrong_punct = new List<string>();
            bool b = true;
            try
            {
                b = _gidroMorpf.DeletePunctList(models);
                if(!b)
                {
                    wrong_punct.Add(JsonConvert.SerializeObject( new {Message = "Something went wrong with you data!!!", body = new string[0]}));
                }
            }
            catch(CustomException e)
            {
                wrong_punct.Add(e.Message);
            }
            catch(Exception e)
            {
                b = false;
            }

            if(wrong_punct.Count() == 0 && b == true)
            {
                contentResult.StatusCode = 200;
                contentResult.ContentType = "text/html";
                contentResult.Content = "Ok";
            }
            else if(b == false && wrong_punct.Count() == 0)
            {
                contentResult.StatusCode = 500;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_punct);
            }
            else if (wrong_punct.Count() != 0)
            {
                contentResult.StatusCode = 400;
                contentResult.ContentType = "application/json";
                contentResult.Content = string.Join(",", wrong_punct);
            } 

            return contentResult;
        }

        /// <summary>
        /// эндпоинт который предоставляет список результатов оценок гидроморфологического анализа
        /// </summary>
        /// <response code="200">Ok</response>
        /// <response code="400">Bad Request</response>
        /// <response code="500">Internal Server error</response>
        /// <returns></returns>
        [HttpGet]
        [AutorizationApiFilter("GidroMorph")]
        [Route("api/GidroMorpf/Totals")]
        [Produces("application/json")]
        [ProducesResponseType(typeof(List<GmfTotalPL>), 200)]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully get a list of totals")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you list data of totals that contains estimation about protocols that take part in gidromorpfology analyze")]
        //[ServiceFilter(typeof(ResponceFormatFilter<GmfTotalJson, GmfTotalPL>))]
        public async Task<ContentResult> GetTotal(int kn = -1, int god = -1, string region = null, string bass = null, string river = null, string punkt = null, string pasp = null)
        {
            ContentResult contentResult = new ContentResult();

            List<GmfTotalPL> list = null;

            var models_bl =_gidroMorpf.GetTotalList(kn, god, region, bass, river, punkt, pasp);

            var models_pl = _mapper.Map<List<GmfTotalPL>>(models_bl);

            var json = JsonConvert.SerializeObject(models_pl);

            contentResult.Content = json;

            contentResult.ContentType = "application/json";

            contentResult.StatusCode = 200;

            return contentResult;
        }

        [HttpPost]
        [AutorizationApiFilter("GidroMorph")]
        [Route("api/GidroMorpf/Totals")]
        [Produces("application/json")]
        [ProducesResponseType(401)]
        [ProducesResponseType(403)]
        [ProducesResponseType(500)]
        [SwaggerResponse(200, "You succesfully get a list of totals")]
        [SwaggerResponse(401, "You Not autorized", typeof(string))]
        [SwaggerResponse(403, "You dont have right for this endpoint", typeof(string))]
        [SwaggerResponse(500, "Something broken in our ecosystem, please tell us", typeof(string[]))]
        [SwaggerOperation(Description ="This endpoint provide you list data of totals that contains estimation about protocols that take part in gidromorpfology analyze")]

        public async Task<ContentResult> RecalcAllTotals()
        {
            ContentResult contentResult = new ContentResult();

            contentResult.StatusCode = 200;

            var b =  await _gidroMorpf.RecalcAllTotals();

            return contentResult;
        }

        [HttpGet]
        [Route("api/GidroMorpf/Colors")]
        public async Task<ContentResult> GetColorList()
        {
            ContentResult contentResult = new ContentResult();

            
            return contentResult;
        }
    }
}