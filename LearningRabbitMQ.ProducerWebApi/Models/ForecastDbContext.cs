using Microsoft.EntityFrameworkCore;

namespace LearningRabbitMQ.ProducerWebApi.Models
{
    public class ForecastDbContext : DbContext
    {
        public ForecastDbContext(DbContextOptions options) : base(options) { }
        public DbSet<Forecast> Forecasts { get; set; } = null!;
    }
}
