using System;
using System.Threading.Tasks;
using TrafficLightsAPI.Application;
using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Application.Contracts
{
    public interface ITrafficLightService
    {
        Dictionary<string, TrafficLight> GetTrafficLights();
        void SetTrafficLightColor(string direction, TrafficLightColor newColor);

        TrafficLightSettings lightSettings { get; set; }
    }


}
