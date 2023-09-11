using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Application.Contracts
{
    public interface ITrafficLightState
    {   
        void ChangeState(TrafficLight trafficLight,TrafficLight sameAxisLight);
    }
}
