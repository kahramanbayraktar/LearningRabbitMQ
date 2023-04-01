namespace LearningRabbitMQ.ProducerWebApi.Models
{
    // SQLite
    public class Forecast
    {
        //public Forecast(decimal lat, decimal lon)
        //{
        //    Latitude = lat;
        //    Longitude = lon;
        //}

        public int Id { get; set; }

        public decimal Latitude { get; set; }

        public decimal Longitude { get; set; }

        public DateTimeOffset Day { get; set; }

        public DateTimeOffset Time { get; set; }

        public decimal Temperature { get; set; }
    }
}
