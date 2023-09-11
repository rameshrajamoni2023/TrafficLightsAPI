using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Timers;
using TrafficLightsAPI.Application.Contracts;
using TrafficLightsAPI.Data;
using TrafficLightsAPI.Model;
using Timer = System.Timers.Timer;

namespace TrafficLightsAPI.Services
{
    public class TrafficLightService : ITrafficLightService
    {
        private readonly ILogger<TrafficLightService> _logger;

        private readonly TrafficLightDbContext _dbcontext;  //[TODO] to be implemented and integrated
       
        private Dictionary<string, TrafficLight> trafficLights = new Dictionary<string, TrafficLight>
        {
            { "North", new TrafficLight { Direction = "North", CurrentColor = TrafficLightColor.Red } },
            { "South", new TrafficLight { Direction = "South", CurrentColor = TrafficLightColor.Red } },
            { "West", new TrafficLight { Direction = "West", CurrentColor = TrafficLightColor.Red } },
            { "East", new TrafficLight { Direction = "East", CurrentColor = TrafficLightColor.Red } }
        };

        
        public TrafficLightSettings lightSettings { get; set; } 

        private Timer timer;
        private readonly int crossTrafficRedDuration = 4; // seconds

        public TrafficLightService(TrafficLightDbContext dbContext, ILogger<TrafficLightService> logger, TrafficLightSettings trafficLightSettings)
        {
            _logger = logger;
            _logger.LogDebug("TrafficLightService() Invoked....");
            _dbcontext = dbContext;
            lightSettings = trafficLightSettings;

            // Initialize the timer for managing traffic light changes
            timer = new Timer(1000);                // 1 second interval
            timer.Elapsed += TimerElapsed;
            timer.AutoReset = true;
            timer.Enabled = true;
            
           
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            _logger.LogDebug("TimerElapsed() Invoked ....Timer {0}", timer);

            //we have clubbed lights along same axis for convenience
            foreach (TrafficLight light in trafficLights.Values)     
            {
                light.IsPeakHour = IsPeakHour(DateTime.Now);
                light.AllCrossTrafficIsRed = AllCrossTrafficRed(light.CurrentColor);

                _logger.LogDebug("Light Direction {0}, CurrentColor {1}, IsPeak {2}, AllCrossTrafficIsRed {3} Timer {4}",
                                light.Direction, light.CurrentColor, light.IsPeakHour, light.AllCrossTrafficIsRed, light.GreenTimer);


                if (light.Direction == "North" || light.Direction == "East")
                {
                    var sameAxisLight = getSameAxisLight(light);
                    light.Change(sameAxisLight);

                    _logger.LogDebug(" {0} and {1} light state change called ", light.Direction, sameAxisLight.Direction);
                }
            }

        }

        private TrafficLight getSameAxisLight(TrafficLight currentLight)
        {
            _logger.LogDebug("getSameAxisLight() Invoked...");

            TrafficLight sameAxisLight = new TrafficLight();

            if (currentLight != null)
            {
                switch (currentLight.Direction)
                {
                    case "North":
                        sameAxisLight = trafficLights["South"];
                        break;
                    case "South":
                        sameAxisLight = trafficLights["North"];
                        break;
                    case "East":
                        sameAxisLight = trafficLights["West"];
                        break;  
                    case "West":
                        sameAxisLight =  trafficLights["East"];
                        break;
                }
            }

            _logger.LogDebug("Current Light is {0} and Same Axis Light is {1} ", currentLight.Direction,sameAxisLight.Direction);

            return sameAxisLight;   
        }

        private bool IsPeakHour(DateTime currentTime)
        {
            // Check if the current time falls within peak hours (0800-1000 and 1700-1900)
            var hour = currentTime.Hour;
            bool isPeakHour = (hour >= 8 && hour < 10) || (hour >= 17 && hour < 19);

            _logger.LogDebug("IsPeakHour() invoked... Hour is {0} and PeakHour is {1} ",hour,isPeakHour);

            return isPeakHour;
        }

        private bool AllCrossTrafficRed(TrafficLightColor currentColor)
        {
            // Calculate the total time spent in the red state by cross-traffic lights
            // and if they are beyond configured duration return true else false

            _logger.LogDebug("AllCrossTrafficRed() invoked....");

            if (currentColor == TrafficLightColor.Red)
            {
                if (trafficLights["North"].CurrentColor == TrafficLightColor.Red
                    && trafficLights["South"].CurrentColor == TrafficLightColor.Red
                    && trafficLights["East"].CurrentColor == TrafficLightColor.Red
                    && trafficLights["West"].CurrentColor == TrafficLightColor.Red
                    && trafficLights["North"].RedTimer >= crossTrafficRedDuration
                    && trafficLights["South"].RedTimer >= crossTrafficRedDuration
                    && trafficLights["East"].RedTimer >= crossTrafficRedDuration
                    && trafficLights["West"].RedTimer >= crossTrafficRedDuration
                   )
                {
                    _logger.LogDebug("Alert -------AllCrossTrafficIsRed --------");
                    return true;

                }
            }
            return false;
        }


        public Dictionary<string, TrafficLight> GetTrafficLights()
        {
            _logger.LogDebug("GetTrafficLights() invoked....");
            return trafficLights;
        }

        public void SetTrafficLightColor(string direction, TrafficLightColor newColor)
        {
            _logger.LogDebug("SetTrafficLightColor() invoked....");

            if (trafficLights.ContainsKey(direction))
            {
                TrafficLight currentLight = trafficLights[direction];
                currentLight.CurrentColor = newColor;
            }
            else
            {
                _logger.LogError("Traffic light direction not found.");
                throw new KeyNotFoundException("Traffic light direction not found.");
            }
        }
    }


}
