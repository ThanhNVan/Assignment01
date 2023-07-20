using Assignment01.EntityProviders;
using Assignment01.LogicProviders;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Assignment01.WebApiHost;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase {
    private static readonly string[] Summaries = new[]
    {
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

    private readonly ILogger<WeatherForecastController> _logger;
    private readonly ICategoryLogicProviders _category;

    public WeatherForecastController(ILogger<WeatherForecastController> logger,
                                    ICategoryLogicProviders categoryLogicProviders) {
        _logger = logger;
        this._category = categoryLogicProviders;    
    }

    [HttpGet(Name = "GetWeatherForecast")]
    public IEnumerable<WeatherForecast> Get() {
        return Enumerable.Range(1, 5).Select(index => new WeatherForecast {
            Date = DateTime.Now.AddDays(index),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)]
        })
        .ToArray();
    }

    [HttpGet("hehe")]
    public async Task<ActionResult<List<Category>>> GetCategoryAsync() {
        var result = await this._category.GetListAllAsync();
        if (result.Count > 0 ) {
            return Ok(result);
        }
        return BadRequest();
    }
}