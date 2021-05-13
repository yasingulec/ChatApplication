using ChatApplication.Data.Repository.Users;
using ChatApplication.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ChatApplication.API.Controllers
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeatherForecastController : ControllerBase
    {
        
        private IUserRepository _userRepository;
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IUserRepository userRepository)
        {
            _logger = logger;
            _userRepository = userRepository;
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
            var users = _userRepository.GetUsers();
            return Ok(users);
        }
        [HttpGet]
        public IActionResult GetUser(Guid guid)
        {
            var user = _userRepository.GetUser(guid);
            return Ok(user);
        }
        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _userRepository.AddUser(user);
            return Ok();
        }

        [HttpPost]
        public IActionResult DeleteUser(Guid guid)
        {
            _userRepository.DeleteUser(guid);
            return Ok();
        }
        [HttpPost]
        public IActionResult UpdateUser(User user)
        {
            _userRepository.UpdateUser(user);
            return Ok();
        }
    }
}
