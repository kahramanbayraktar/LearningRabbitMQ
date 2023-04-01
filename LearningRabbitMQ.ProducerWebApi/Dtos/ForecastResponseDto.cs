﻿namespace LearningRabbitMQ.ProducerWebApi.Dtos
{
    // My API Response
    public class ForecastResponseDto
    {
        public ForecastResponseDto(decimal lat, decimal lon, DateTimeOffset[] times, decimal[] temps)
        {
            Latitude = lat;
            Longitude = lon;
            Values = new ForecastValue[times.Length];
            for (var i = 0; i < times.Length; i++)
            {
                Values[i] = new ForecastValue(times[i], temps[i]);
            }
        }
        public decimal Latitude { get; }
        public decimal Longitude { get; }
        public ForecastValue[] Values { get; set; }

        public static explicit operator ForecastResponseDto(OpenMeteoForecastDto openMeteoForecastDto)
        {
            return new ForecastResponseDto(openMeteoForecastDto.Latitude, openMeteoForecastDto.Longitude,
                openMeteoForecastDto.Hourly.Time, openMeteoForecastDto.Hourly.Temperature);
        }
    }

    public class ForecastValue
    {
        public ForecastValue(DateTimeOffset time, decimal temp)
        {
            Time = time;
            Temperature = temp;
        }
        public DateTimeOffset Time { get; }
        public decimal Temperature { get; }
    }
}