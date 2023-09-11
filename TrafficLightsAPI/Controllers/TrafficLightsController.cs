using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System.Collections.Generic;
using TrafficLightsAPI.Model;
using TrafficLightsAPI.Services;
using Microsoft.Extensions.Logging;
using System.Runtime.CompilerServices;

namespace TrafficLightsAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TrafficLightController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly TrafficLightService _trafficLightService;
        private readonly ILogger<TrafficLightController> _logger;

        public TrafficLightController(IConfiguration configuration,TrafficLightService trafficLightService, ILogger<TrafficLightController> logger)
        {
            _logger = logger;
            _configuration = configuration;
            _trafficLightService = trafficLightService;
            _trafficLightService.lightSettings = new TrafficLightSettings();
            var trafficLightSettings = _configuration.GetSection("TrafficLightSettings");
            _trafficLightService.lightSettings.IsNorthRightTurnGreenEnabled = trafficLightSettings.GetValue<bool>("IsNorthRightTurnGreenEnabled");
            _logger.LogDebug("TrafficLightController() Invoked....");
        }

        [HttpGet]
        public ActionResult<Dictionary<string, TrafficLight>> GetTrafficLights()
        {
            _logger.LogDebug("GetTrafficLights() Invoked....");

            try
            {
                Dictionary<string, TrafficLight> trafficLights = _trafficLightService.GetTrafficLights();

                //trafficLights = _trafficLightService.GetTrafficLights();

                ////------------------ UNCOMMENT BELOW CODE TO TEST THIS API DIRECTLY----------------------------------//
                //for (int i = 0; i < 120; i++)
                //{
                //    trafficLights = _trafficLightService.GetTrafficLights();
                //    Thread.Sleep(1000);

                //    foreach (var trafficLight in trafficLights)
                //    {
                //        Console.WriteLine("{0} secs Direction {1} ---  Color {2}  --- NorthRightTurn {6} --  RTimer {3}  GTimer {4} YTimer {5} )",
                //            i, trafficLight.Value.Direction, trafficLight.Value.CurrentColor, trafficLight.Value.RedTimer, trafficLight.Value.GreenTimer, trafficLight.Value.YellowTimer, trafficLight.Value.IsNorthRightTurnGreen ? "Green" : "Red");
                //    }
                //    DateTime currentTime = DateTime.Now;
                //    Console.WriteLine("\n----------------------Time {0} secs------------------------------------------\n", currentTime.ToString("mm:ss"));
                //}

                _logger.LogDebug(trafficLights.ToString());
                return Ok(trafficLights);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpPost("{direction}/setcolor")]
        public ActionResult<string> SetTrafficLightColor(string direction, [FromBody] TrafficLightColor newColor)
        {
            _logger.LogInformation("SetTrafficLightColor() Invoked....");
            try
            {
                _trafficLightService.SetTrafficLightColor(direction, newColor);
                
                _logger.LogDebug("Direction {0} ---  Color Changed to {1}  ---   ) \n", direction,newColor);
          
                return Ok(newColor);
            }
            catch (KeyNotFoundException e)
            {
                _logger.LogError(e.Message);
                return NotFound($"Traffic light direction '{direction}' not found.");
            }
            catch (InvalidOperationException e)
            {
                _logger.LogError(e.Message);
                return BadRequest(e.Message);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        [HttpGet("getSettings")]
        public ActionResult<string> GetConfiguration()
        {
            _logger.LogInformation("GetConfiguration() Invoked....");
            try
            {
                // Access configuration settings
                var traffic_settings = _trafficLightService.lightSettings;

                return Ok(traffic_settings);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }


        }
    }
}
