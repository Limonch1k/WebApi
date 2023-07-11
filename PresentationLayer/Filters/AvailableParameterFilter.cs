using BL.IServices;
using BL.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using Static.Service;
using System;
using System.Text.RegularExpressions;

namespace api_fact_weather_by_city.Filters
{
    public class AvailableParameterFilter : Attribute, IResourceFilter
    {

        private IUserServices<UserBL> _user;

        private ILogger _logger; 

        

        public AvailableParameterFilter()
        {
            _user = ServiceHandler.provider.GetService<IUserServices<UserBL>>();
            _logger = ServiceHandler.provider.GetService<ILoggerFactory>().CreateLogger("ParamFilterLogger");
        }


        public void OnResourceExecuted(ResourceExecutedContext context)
        {
            /*foreach (var cookie in context.HttpContext.Request.Cookies)
            {
                context.HttpContext.Response.Cookies.Delete(cookie.Key);
            }*/
        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
            QueryString queryString = context.HttpContext.Request.QueryString;
            _logger.LogInformation("I recieve next query: " + context.HttpContext.Request.Path + queryString.ToString());



            string[] param = context.HttpContext.Request.QueryString.Value[1..].Split("&");

            int UserId = Int32.Parse(context.HttpContext.User.Claims.Where(c => c.Type.Equals("Id")).Select(c => c.Value).SingleOrDefault());

            string[][] variableArrayValue = new string[param.Count()][];
            string[] variableName = new string[param.Count()];

            int counter = 0;

            //в param массив не пригодный для обработки, надо отделить названия от значений
            foreach(var p in param)
            {
                var tempArray = p.Split("=");
                if (tempArray.Count() > 1)
                {
                    variableName[counter] = tempArray[0];
                    variableArrayValue[counter] = tempArray[1].Split(",");
                }
                else
                {
                    variableName[counter] = tempArray[0];
                    variableArrayValue[counter] = new string[] {};
                }
                counter++;
            }

            //проверка что можно , что нельзя

            //проверяем переменную с физическими параметрами.
            string[] ListOfAvailableParam = _user.GetAvailableParam(UserId);

            counter = 0;
            foreach(var name in variableName)
            {
                if (name.Equals("param"))
                {
                    break;
                }

                counter++;
            }

            if (counter != variableArrayValue.Length)
            {

                variableArrayValue[counter] = ListOfAvailableParam.Intersect(variableArrayValue[counter]).ToArray<string>();

                if (variableArrayValue[counter].Length == 0)
                {
                    variableArrayValue[counter] = ListOfAvailableParam;
                }
            }

            //проверяем конкретно что то другое(например станции)

            string[] ListOfAvailablePunkt = _user.GetAvailablePunkt(UserId);

            counter = 0;
            foreach(var name in variableName)
            {
                if (name.Equals("stationId"))
                {
                    break;
                }

                counter++;
            }

            if (counter != variableArrayValue.Length)
            {

                variableArrayValue[counter] = ListOfAvailablePunkt.Intersect(variableArrayValue[counter]).ToArray<string>();

                if (variableArrayValue[counter].Length == 0)
                {
                    variableArrayValue[counter] = ListOfAvailablePunkt;
                }
            }

            //проверяем время

            counter = 0;
            foreach(var name in variableName)
            {
                if (name.Equals("startAt"))
                {

                    break;
                }

                counter++;
            }

            if (counter != variableArrayValue.Length)
            {
                //тут баг со временем исправляем
                variableArrayValue[counter][0] = variableArrayValue[counter][0].Replace("%", " ");
                var temp_start = variableArrayValue[counter][0].Split(" ");
                temp_start[1] = temp_start[1][2..];
                variableArrayValue[counter][0] = string.Join(" ", temp_start);
            }


            //проверяем время с endAt
            counter = 0;
            foreach (var name in variableName)
            {
                if (name.Equals("endAt"))
                {

                    break;
                }

                counter++;
            }

            if (counter != variableArrayValue.Length) 
            {
                //тут баг со временем исправляем
                variableArrayValue[counter][0] = variableArrayValue[counter][0].Replace("%", " ");
                var temp_end = variableArrayValue[counter][0].Split(" ");
                temp_end[1] = temp_end[1][2..];
                variableArrayValue[counter][0] = string.Join(" ", temp_end);
            }
            


            //присвоение того что наменял обратно

            Dictionary<string, StringValues> dict = new Dictionary<string, StringValues>();

            counter = 0;
            foreach(var variable in variableArrayValue)
            {
                dict.Add(variableName[counter], new StringValues(string.Join("," ,variableArrayValue[counter])));
                counter++;
            }

            var query = new QueryCollection(dict);

            context.HttpContext.Request.Query = query;
            
        }
    }
}