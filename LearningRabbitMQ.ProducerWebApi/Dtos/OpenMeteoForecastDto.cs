using System.Runtime.Serialization;
using System.Text.Json.Serialization;

namespace LearningRabbitMQ.ProducerWebApi.Dtos;

// OpenMeteoAPI Response
public class OpenMeteoForecastDto
{

    [JsonPropertyName("latitude")]
    public decimal Latitude { get; set; }

    [JsonPropertyName("longitude")]
    public decimal Longitude { get; set; }

    [JsonPropertyName("hourly")]
    public Hourly Hourly { get; set; }
}

[DataContract]
public class Hourly
{
    [JsonPropertyName("time")]
    public DateTimeOffset[] Time { get; set; }

    [JsonPropertyName("temperature_2m")] // for HttpClient requests
    public decimal[] Temperature { get; set; }
}
