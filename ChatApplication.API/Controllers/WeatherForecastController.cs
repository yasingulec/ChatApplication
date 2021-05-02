using ChatApplication.Data.Users;
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
        private IUserOperations _userOperations;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserOperations userOperations)
        {
            _logger = logger;
            _userOperations = userOperations;
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
        public IActionResult GetUsers()
        {
            var users = _userOperations.GetUsers();
            return Ok(users);
        }
        [HttpGet]
        public IActionResult GetUser(Guid guid)
        {
            var user = _userOperations.GetUser(guid);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _userOperations.AddUser(user);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteUser(Guid guid)
        {
            _userOperations.DeleteUser(guid);
            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            _userOperations.UpdateUser(user);
            return Ok();
        }
    }
}
