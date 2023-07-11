using System.Xml.Serialization;

namespace api_fact_weather_by_city.XMLModels
{
    //[XmlType("CustomAttribute")]
    public class AverageTempDayXML
    {
        public AverageTempDayXML(string _cityId, double _averageTemp)
        {
            CityId = _cityId;
            averageTemp = _averageTemp;
        }

        public AverageTempDayXML(string _cityId)
        {
            CityId = _cityId;
        }

        public AverageTempDayXML(){}

        public string? CityId {get;set;}

        public string? CityName {get;set;}

        public double averageTemp {get;set;}

    }

}