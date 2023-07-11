using AutoMapper;
using BL.Models;
using BL.IServices;
using DBLayer.Context;
using DB.TableModels;
using DB.DBModels;
using System;
using System.Dynamic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Static.Service;
using DB.IRepository;
using System.Reflection;
using GeneralObject.MyCustomAttribute;
using BusinessLayer.ParametrModel;
using DatabaseLayer.ParamModel;
using Microsoft.Extensions.Logging;

namespace BL.Services
{
    public class MeteoServices<TSourceBL, TSourceDB> : ISynopServicesAsync<TSourceBL> where TSourceBL : class where TSourceDB : class
    {

        private IResorceRepositoryAsync<TSourceDB> _meteo {get;set;}

        private IMapper _mapper {get;set;}

        private ILogger _logger { get; set; }


        public MeteoServices(IMapper mapper, IResorceRepositoryAsync<TSourceDB> meteo, ILoggerProvider loggerProvider)
        {
            _meteo = meteo;
            _mapper = mapper;
            _logger = loggerProvider.CreateLogger("MeteoServicesLogger");
        }

        public Task<IEnumerable<TSourceBL>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<TSourceBL>> GetByStationId(int[] stat_id_list)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TSourceBL>> GenericCallHandlerOfParamObject(MeteoParamModel_BL paramObject) 
        {
            Type t = _meteo.GetType();
            List<string> NameList = new List<string>();
            string methodName = "";

            //var paramList = paramObject.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public)
            //    .OrderBy(prop => { return prop.GetCustomAttribute<OrderbyAttribute>().Order; });


            List<object?> ParamValue = new List<object?>();

            if (paramObject.stationList is not null) 
            {
                methodName += "ResourceId";
            }
            if (paramObject.paramList is not null)
            {
                methodName += "Param";
            }
            if (paramObject.start_dt is not null)
            {
                methodName += "StartDate";
            }
            if (paramObject.end_dt is not null)
            {
                methodName += "EndDate";
            }
            if (true)
            {
                methodName += "Orderby";
            }

            methodName += "Filter";            

            MeteoParamModel_DL meteoParam = _mapper.Map<MeteoParamModel_DL>(paramObject);
            ParamValue.Add(meteoParam);      
            MethodInfo methodInfo = t.GetMethod(methodName);

            object?[] param_array = ParamValue.ToArray();

            var listDB = await (Task<List<TSourceDB>>)methodInfo.Invoke(_meteo, param_array);

            var listBL = _mapper.Map<List<TSourceBL>>(listDB);

            return listBL;
        }

        public async Task<List<TSourceBL>> SelectAll(string[] orderby = null)
        {

            List<TSourceDB> dbMeteo; 
            try
            {
                dbMeteo = await _meteo.GetAllAsync();
            }
            catch(Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in MeteoService at SelectAll method");
            }
            finally 
            {
                dbMeteo = new List<TSourceDB>();
            }

            
            //var db = _mapper.Map<List<TSourceDB>>(dbAVGTemp);


            var bl = _mapper.Map<List<TSourceBL>>(dbMeteo);

            return bl;
        }

        public async Task<List<TSourceBL>> SelectStationParamSpecifyDays(MeteoParamModel_BL paramObject)
        {
            MeteoParamModel_DL meteoParam = _mapper.Map<MeteoParamModel_DL>(paramObject);
            List<TSourceDB> listdb = null;
            try
            {
                listdb = await _meteo.ResourceIdParamStartDateEndDateOrderbyFilter(meteoParam);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                _logger.LogError("an errore has occured in MeteoService at SelectStationParamSpecifyDays method");
            }
            finally 
            {
                //listdb = new List<TSourceDB>();
            }

            var bl = _mapper.Map<List<TSourceBL>>(listdb); 

            return bl;
        }

        public async Task Save()
        {
            _meteo.SaveAsync();
        }

        public async Task Dispose()
        {
            _meteo.DisposeAsync();
        }
    }
}