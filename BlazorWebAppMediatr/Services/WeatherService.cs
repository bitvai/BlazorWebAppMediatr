
using System.Runtime.CompilerServices;
using MediatR;
using WeatherForecast = BlazorWebAppMediatr.Models.WeatherForecast;

namespace BlazorWebAppMediatr.Services
{

    public class WeatherStreamQueryHandler : IStreamRequestHandler<WeatherStreamQuery, WeatherStreamQueryResult>
    {
        public async IAsyncEnumerable<WeatherStreamQueryResult> Handle(WeatherStreamQuery request, [EnumeratorCancellation] CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                await Task.Delay(1000);
                yield return new WeatherStreamQueryResult() { Forecasts = WeatherForecasts().ToList() };
            }
        }

        private static WeatherForecast[] WeatherForecasts()
        {

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast()
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();
            return forecasts;
        }
    }

    public class WeatherRequestHandler : IRequestHandler<WeatherRequest, WeatherResponse>
    {
        public Task<WeatherResponse> Handle(WeatherRequest request, CancellationToken cancellationToken)
        {
            return Task.FromResult<WeatherResponse>(new WeatherResponse() { Forecasts = WeatherForecasts().ToList() });
        }

        private static WeatherForecast[] WeatherForecasts()
        {

            var startDate = DateOnly.FromDateTime(DateTime.Now);
            var summaries = new[] { "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching" };
            var forecasts = Enumerable.Range(1, 5).Select(index => new WeatherForecast()
            {
                Date = startDate.AddDays(index),
                TemperatureC = Random.Shared.Next(-20, 55),
                Summary = summaries[Random.Shared.Next(summaries.Length)]
            }).ToArray();
            return forecasts;
        }
    }

    public record WeatherStreamQuery : IStreamRequest<WeatherStreamQueryResult>;
    public record WeatherStreamQueryResult
    {
        public List<WeatherForecast> Forecasts { get; set; }
    }

    public record WeatherRequest : IRequest<WeatherResponse>;
    public record WeatherResponse
    {
        public List<WeatherForecast> Forecasts { get; set; }
    }
}


