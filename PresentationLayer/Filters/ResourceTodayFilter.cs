using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Text.RegularExpressions;

namespace api_fact_weather_by_city.Filters
{
    public class ResourseTodayFilter : Attribute, IResourceFilter
    {
        private IMemoryCache _cache{get;set;}
        private string? _key {get;set;}

        //на сколько устанавливать кэш
        private int _hour {get;set;}

        //как часто
        private int _frequency{get;set;}

        public ResourseTodayFilter(string? key, int hour)
        {
            _key = key;
            _hour = hour;
        }


        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            _cache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
            //попробуем тут кэш установить
             if(!_cache.TryGetValue<ContentResult>(_key, out _))
             {
                int hour = DateTime.Now.Hour;
                hour = hour / 3;
                hour = hour * 3;

                DateTime dt  = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day, hour + _hour, 00, 00);
                var result = context.Result as ContentResult;

                var cacheOptions = new MemoryCacheEntryOptions()
                {
                    // кэширование в до кокретного времени
                    AbsoluteExpiration = (DateTimeOffset)dt,
                    // низкий приоритет
                    Priority = 0,
                };


                if (result is not null)
                {
                    _cache.Set(_key, result, cacheOptions);
                }
                
             }

        }
 
        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            _cache = context.HttpContext.RequestServices.GetService(typeof(IMemoryCache)) as IMemoryCache;
            ContentResult? contentResult;

            //если кэш есть вернем его
            if(_cache.TryGetValue<ContentResult>(_key, out contentResult))
            {
                context.Result = contentResult;
            }
                
        }
    }
}