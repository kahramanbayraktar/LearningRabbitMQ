using LearningRabbitMQ.ProducerWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace LearningRabbitMQ.ProducerWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddDbContext<ForecastDb>(options => options.UseInMemoryDatabase("forecasts"));

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

            app.MapGet("/forecasts", async (ForecastDb db) => await db.Forecasts.ToListAsync());

            app.MapPost("/forecast", async (ForecastDb db, Forecast forecast) =>
            {
                await db.Forecasts.AddAsync(forecast);
                await db.SaveChangesAsync();
                return Results.Created($"/forecast/{forecast.Id}", forecast);
            });

            app.Run();
        }
    }
}