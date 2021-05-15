using ChatApplication.Data.Commands.UserCommands;
using ChatApplication.Data.Queries.UserQueries;
using ChatApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ChatApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {

        private IUserQueries _userQueries;
        private IUserCommands _userCommands;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserQueries userQueries, IUserCommands userCommands)
        {
            _logger = logger;
            _userQueries = userQueries;
            _userCommands = userCommands;
        }

        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await _userQueries.GetUsersAsync();
            return Ok(users);
        }
        [HttpGet]
        public async Task<IActionResult> GetUser(Guid guid)
        {
            var user = await _userQueries.GetUserAsync(guid);
            return Ok(user);
        }
        [HttpPost]
        public async Task<IActionResult> AddUser(User user)
        {
            await _userCommands.AddUserAsync(user);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteUser(Guid guid)
        {
            _userCommands.DeleteUserAsync(guid);
            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            _userCommands.UpdateUserAsync(user);
            return Ok();
        }
    }
}
