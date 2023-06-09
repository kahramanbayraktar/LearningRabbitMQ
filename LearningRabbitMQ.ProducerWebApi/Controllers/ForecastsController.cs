﻿using LearningRabbitMQ.ProducerWebApi.Dtos;
using LearningRabbitMQ.ProducerWebApi.Extensions;
using LearningRabbitMQ.ProducerWebApi.Models;
using LearningRabbitMQ.ProducerWebApi.RabbitMQ;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace LearningRabbitMQ.ProducerWebApi.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class ForecastsController : ControllerBase
    {
        private readonly ForecastDbContext _db;
        private readonly IMessageProducer _messageProducer;

        public ForecastsController(ForecastDbContext db, IMessageProducer messageProducer)
        {
            _db = db;
            _messageProducer = messageProducer;
        }

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpGet]
        public async Task<ActionResult<ForecastResponseDto>> Get(decimal lat, decimal lon, DateTimeOffset day)
        {
            OpenMeteoForecastDto openMeteoForecastDto;

            var forecasts = await _db.Forecasts.Where(x => x.Latitude == lat
                                                && x.Longitude == lon
                                                && x.Day == day).ToListAsync();

            // If there is any record of requested day in db, then we assume we have entire requested data.
            if (forecasts == null || !forecasts.Any())
            {
                var startDate = day.ToOpenMeteoDate();
                var endDay = day.AddDays(1);
                var endDate = endDay.ToOpenMeteoDate();

                HttpClient httpClient = new();

                var requestUrl = $"https://api.open-meteo.com/v1/forecast?latitude={lat}&longitude={lon}&hourly=temperature_2m&start_date={startDate}&end_date={endDate}";

                try
                {
                    // TODO: Doesn't give error details returned by external API e.g. "Parameter 'start_date' is out of allowed range from 2022-06-08 to 2023-04-16"
                    openMeteoForecastDto = await httpClient.GetFromJsonAsync<OpenMeteoForecastDto>(requestUrl);
                    //var openMeteoForecasts = await httpClient.GetAsync(requestUrl);
                }
                catch (Exception)
                {
                    return BadRequest();
                }

                // Save data to db
                for (var i = 0; i < openMeteoForecastDto.Hourly.Time.Count(); i++)
                {
                    Forecast f = new()
                    {
                        Latitude = openMeteoForecastDto.Latitude,
                        Longitude = openMeteoForecastDto.Longitude,
                        Day = day,
                        Time = openMeteoForecastDto.Hourly.Time[i],
                        Temperature = openMeteoForecastDto.Hourly.Temperature[i]
                    };
                    await _db.Forecasts.AddAsync(f);
                }

                await _db.SaveChangesAsync();
            }
            else // Just reformat data in db and return it
            {
                openMeteoForecastDto = new()
                {
                    Latitude = lat,
                    Longitude = lon,
                    Hourly = new Hourly
                    {
                        Time = forecasts.Select(x => x.Time).ToArray(),
                        Temperature = forecasts.Select(x => x.Temperature).ToArray()
                    }
                };
            }

            return (ForecastResponseDto)openMeteoForecastDto;
        }

        [HttpPost]
        public async Task<IActionResult> CreateForecast(ForecastResponseDto forecastDto)
        {
            var forecasts = new List<Forecast>();

            for (var i = 0; i < forecastDto.Values.Count(); i++)
            {
                Forecast forecast = new()
                {
                    Latitude = forecastDto.Latitude,
                    Longitude = forecastDto.Longitude,
                    Day = DateTimeOffset.Now,
                    Time = forecastDto.Values[i].Time,
                    Temperature = forecastDto.Values[i].Temperature
                };
                forecasts.Add(forecast);
                await _db.Forecasts.AddAsync(forecast);
            }
            await _db.SaveChangesAsync();

            _messageProducer.SendMessage(forecasts);

            return Ok();
        }
    }
}
