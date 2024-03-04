using api_fact_weather_by_city.ViewModel;
//using Newtonsoft.Json;
using System.Text.Json;
using System.Text.Json.Serialization;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace api_fact_weather_by_city.JSONModels._29._131
{
    public class GmfProtocolJson
    {

        public GmfProtocolJson() 
        {
            Pcat = 0;
            Aestimation = null;
            Bestimation = null;
            MarkEstam = null;
            Prim = null;
            Opisanie = null;
        }
        [JsonConverter(typeof(StringToIntConverter))]
        public int Kn { get; set; }
        [JsonConverter(typeof(StringToIntConverter))]
        public int God { get; set; }
        [JsonConverter(typeof(StringToIntConverter))]
        public int Pcat { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public string Pindicator { get; set; } = null!;
        [JsonConverter(typeof(IntToStringConverter))]
        public string? Aestimation { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public string? Bestimation { get; set; }

        public bool? MarkEstam { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public string? Prim { get; set; }
        [JsonConverter(typeof(IntToStringConverter))]
        public string? Opisanie { get; set; }

        public string Date {get;set;}

        //[JsonProperty(NullValueHandling = NullValueHandling.Include)]
        //public virtual GmfPunctJson KnNavigationJson { get; set; } = null!;

        //[JsonProperty(NullValueHandling = NullValueHandling.Include)]
        //public virtual GmfIndicatorJson PindicatorNavigation { get; set; } = null!;
    }

    public class StringToIntConverter : JsonConverter<int>
    {
        private readonly static JsonConverter<int> s_defaultConverter =
            (JsonConverter<int>)JsonSerializerOptions.Default.GetConverter(typeof(int));

        // Custom serialization logic
        public override void Write(
            Utf8JsonWriter writer, int value, JsonSerializerOptions options)
        {
            writer.WriteNumberValue(value);
        }

        // Fall back to default deserialization logic
        public override int Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var boo = true;
            var str = "";
            var i = 0;
            try
            {
                str = reader.GetString();
            }
            catch (Exception e) 
            {
                boo = false;
            }

            if (boo) 
            {
                return Convert.ToInt32(str);
            }

            boo = true;

            try
            {
                i = reader.GetInt32();
            }
            catch (Exception e) 
            {
                boo = false;
            }

            return  i;

        }
    }

    public class IntToStringConverter : JsonConverter<string>
    {
        private readonly static JsonConverter<string> s_defaultConverter =
            (JsonConverter<string>)JsonSerializerOptions.Default.GetConverter(typeof(string));

        // Custom serialization logic
        public override void Write(
            Utf8JsonWriter writer, string value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value);
        }

        // Fall back to default deserialization logic
        public override string Read(
            ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var boo = true;
            var str = "";
            var i = 0;
            try
            {
                i = reader.GetInt32();
            }
            catch (Exception e) 
            {
                boo = false;
            }

            if (boo)
            {
                return i.ToString();
            }

            boo = true;

            try
            {
                str = reader.GetString();
            }
            catch (Exception e) 
            {
                boo = false;
            }

            return  str;

        }

    }

}
