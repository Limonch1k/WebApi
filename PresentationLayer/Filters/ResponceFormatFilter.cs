using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Static.Service;
using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Text.Unicode;
using System.Xml.Serialization;

namespace api_fact_weather_by_city.Filters
{
    public class ResponceFormatFilter<TModel,TModelXML> : IResultFilter
    {

        private ResultExecutingContext _context_ing { get; set; }
        private IMapper _mapper { get; set; }

        private ILogger _logger { get; set; }

        public ResponceFormatFilter() 
        {
            _mapper = ServiceHandler.GetService<IMapper>();
            _logger = ServiceHandler.GetService<ILoggerProvider>().CreateLogger("FotmatFilterLogger");
        }

        public void OnResultExecuting(ResultExecutingContext context)
        {
            _context_ing = context;

            context.HttpContext.Items.TryGetValue("ParamListArray", out var pl);
            string[] paramList = pl as string[];

            _logger.LogInformation("I recieve next fields to select information:");

            foreach (var str in paramList) 
            {
                _logger.LogInformation(str);
            }

            var contentResult = context.Result as ContentResult;
            if (contentResult != null)
            {
                var json = contentResult.Content;
                var model = JsonConvert.DeserializeObject<List<TModel>>(json);

                List<TModelXML> listXML = _mapper.Map<List<TModelXML>>(model);

                string? content = null;
                string? contentType = null;

                CreateResponseFormat(listXML, paramList, out content, out contentType);

                contentResult.Content = content;
                contentResult.ContentType = contentType;
            }
            else
            {
                contentResult.Content = "sorry we have errore";
                _logger.LogInformation("sorry we have errore at ResponceFormatFilter");
            }
        }

        public void OnResultExecuted(ResultExecutedContext context)
        {
            //_context_ed = context;

        }

        private void CreateResponseFormat<TXML>(List<TXML> listXML,string[] paramList, out string? content, out string? contentType)
        {

            if (_context_ing.HttpContext.Request.Headers["Content-Type"].Contains("application/xml"))
            {
                content = GetXMLString(listXML);
                string regex = @"^(?:(?!\b(";
                foreach (var p in paramList)
                {
                    regex += p + "|";
                }

                regex = regex[..^1];
                regex += @")\b).)*$";
                content = Regex.Replace(content, regex, string.Empty, RegexOptions.Multiline);
                contentType = "application/xml";
            }
            else 
            {
                content = GetJsonString(listXML);
                var array = (JArray)JsonConvert.DeserializeObject(content);
                var propertiesList = listXML.GetType().GetGenericArguments()[0].GetProperties();
                
                foreach (var property in propertiesList)
                {
                    if (!paramList.Contains(property.Name)) 
                    {
                        var tokens = array.SelectTokens("$.." + property.Name).ToArray();
                        foreach (var token in tokens) 
                        {
                            token.Parent.Remove();
                        }            
                    }                 
                }

                content = array.ToString();
                contentType = "application/json";

                _logger.LogDebug(content);
            }
        }

        private string GetXMLString<TXML>(List<TXML> list)
        {
            XmlSerializer xmlSerializer = new XmlSerializer(typeof(List<TXML>));

            using (StringWriter text = new StringWriter())
            {
                xmlSerializer.Serialize(text, list);
                return text.ToString();
            }
            return null;
        }

        private string GetJsonString<TXML>(List<TXML> list)
        {
            var options = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.Create(UnicodeRanges.BasicLatin, UnicodeRanges.Cyrillic),
                WriteIndented = true
            };
            string json = System.Text.Json.JsonSerializer.Serialize(list, options);
            return json;
        }
    }
    
}
