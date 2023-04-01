using LearningRabbitMQ.ProducerWebApi.Models;
using LearningRabbitMQ.ProducerWebApi.RabbitMQ;
using Microsoft.EntityFrameworkCore;

namespace LearningRabbitMQ.ProducerWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var connectionString = builder.Configuration.GetConnectionString("Forecasts") ?? "Data Source=Db/Forecasts.db";

            builder.Services.AddControllers();

            // In-Memory db
            //builder.Services.AddDbContext<ForecastDb>(options => options.UseInMemoryDatabase("forecasts"));
            
            // Sqlite db
            builder.Services.AddSqlite<ForecastDbContext>(connectionString);

            builder.Services.AddSingleton<IMessageProducer, RabbitMQProducer>();

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            //app.MapGet("/forecasts", async (ForecastDb db) => await db.Forecasts.ToListAsync());

            //app.MapPost("/forecasts", async (ForecastDbContext db, Forecast forecast) =>
            //{
            //    await db.Forecasts.AddAsync(forecast);
            //    await db.SaveChangesAsync();
            //    return Results.Created($"/forecasts/{forecast.Id}", forecast);
            //});

            app.Run();
        }
    }
}