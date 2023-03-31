using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;

namespace LearningRabbitMQ.ProducerWebApi.Models
{
    public class Forecast
    {
        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        //public ForecastValues Values { get; set; }

        //public static explicit operator Forecast(OpenMeteoForecastDto openMeteoForecastDto)
        //{
        //    return new Forecast
        //    {
        //        Latitude = openMeteoForecastDto.Latitude,
        //        Longitude = openMeteoForecastDto.Longitude,
        //        Values = new ForecastValues
        //        {
        //            Time = openMeteoForecastDto.Hourly.Time,
        //            Temperature = openMeteoForecastDto.Hourly.Temperature
        //        }
        //    };
        //}
    }

    //[DataContract]
    //public class ForecastValues
    //{
    //    [NotMapped]
    //    public DateTimeOffset[] Time { get; set; }

    //    [NotMapped]
    //    public decimal[] Temperature { get; set; }
    //}

    class ForecastDb : DbContext
    {
        public ForecastDb(DbContextOptions options) : base(options) { }
        public DbSet<Forecast> Forecasts { get; set; } = null!;
    }
}
