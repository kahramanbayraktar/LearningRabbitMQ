namespace LearningRabbitMQ.ProducerWebApi.Dtos
{
    // My API Response
    public class ForecastResponseDto
    {
        //public ForecastResponseDto(decimal lat, decimal lon, DateTimeOffset[] times, decimal[] temps)
        //{
        //    Latitude = lat;
        //    Longitude = lon;
        //    Values = new ForecastValue[times.Length];
        //    for (var i = 0; i < times.Length; i++)
        //    {
        //        Values[i] = new ForecastValue(times[i], temps[i]);
        //    }
        //}
        public decimal Latitude { get; set; }
        public decimal Longitude { get; set; }
        public ForecastValue[] Values { get; set; }

        public static explicit operator ForecastResponseDto(OpenMeteoForecastDto openMeteoForecastDto)
        {
            //return new ForecastResponseDto(openMeteoForecastDto.Latitude, openMeteoForecastDto.Longitude,
            //    openMeteoForecastDto.Hourly.Time, openMeteoForecastDto.Hourly.Temperature);
            var result = new ForecastResponseDto
            {
                Latitude = openMeteoForecastDto.Latitude,
                Longitude = openMeteoForecastDto.Longitude,
            };
            result.Values = new ForecastValue[openMeteoForecastDto.Hourly.Time.Length];
            for (var i = 0; i < openMeteoForecastDto.Hourly.Time.Length; i++)
            {
                result.Values[i] = new ForecastValue
                {
                    Time = openMeteoForecastDto.Hourly.Time[i],
                    Temperature = openMeteoForecastDto.Hourly.Temperature[i]
                };
            }
            return result;
        }
    }

    public class ForecastValue
    {
        //public ForecastValue(DateTimeOffset time, decimal temp)
        //{
        //    Time = time;
        //    Temperature = temp;
        //}
        public DateTimeOffset Time { get; set; }
        public decimal Temperature { get; set; }
    }
}
