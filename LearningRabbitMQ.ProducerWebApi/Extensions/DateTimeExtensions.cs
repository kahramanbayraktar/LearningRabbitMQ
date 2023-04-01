namespace LearningRabbitMQ.ProducerWebApi.Extensions
{
    public static class DateTimeExtensions
    {
        public static string ToOpenMeteoDate(this DateTimeOffset date)
        {
            var day = date.Day.ToString();
            if (date.Day < 10) day = "0" + day;
            var month = date.Month.ToString();
            if (date.Month < 10) month = "0" + month;
            return $"{date.Year}-{month}-{day}";
        }
    }
}
