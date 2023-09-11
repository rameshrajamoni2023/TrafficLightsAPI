using TrafficLightsAPI.Application.Contracts;
using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Application
{
    public class YellowLight : ITrafficLightState
    {
        private readonly int stayInYellowDuration = 5; // seconds
        void ITrafficLightState.ChangeState(TrafficLight trafficLight, TrafficLight sameAxisLight)
        {

            trafficLight.RedTimer = 0;
            sameAxisLight.RedTimer = 0;

            trafficLight.GreenTimer = 0;
            sameAxisLight.GreenTimer = 0;

         
            // Stay in yellow for 5 seconds
            if (trafficLight.YellowTimer >= stayInYellowDuration)
            {
                trafficLight.CurrentColor = TrafficLightColor.Red;
                sameAxisLight.CurrentColor = TrafficLightColor.Red;

                Console.WriteLine("Yellow Changing to Red");
                trafficLight.SetState(new RedLight());
                sameAxisLight.SetState(new RedLight());
            }
            trafficLight.YellowTimer++;
            sameAxisLight.YellowTimer++;

        }
    }
}
