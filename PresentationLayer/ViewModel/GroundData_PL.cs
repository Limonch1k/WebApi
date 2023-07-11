namespace api_fact_weather_by_city.ViewModel
{
    public partial class GroundData_PL
    {
        public DateTime Datas { get; set; }

        public decimal CityId { get; set; }

        public decimal AdvanceTime { get; set; }

        public decimal ModelId { get; set; }

        public decimal? WindU { get; set; }

        public decimal? WindV { get; set; }

        public decimal? Pressure { get; set; }

        public decimal? Temp { get; set; }

        public decimal? RHumid { get; set; }

        public decimal? PrecipTotal { get; set; }

        public decimal? Visib { get; set; }

        public decimal? Winddirection { get; set; }

        public DateTime? DateWrite { get; set; }
    }
}
