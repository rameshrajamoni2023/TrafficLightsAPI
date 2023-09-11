using TrafficLightsAPI.Application.Contracts;
using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Application
{
    public class RedLight : ITrafficLightState
    {
        void ITrafficLightState.ChangeState(TrafficLight trafficLight, TrafficLight sameAxisLight)
        {

            trafficLight.GreenTimer = 0;
            sameAxisLight.GreenTimer = 0;

            trafficLight.YellowTimer = 0;
            sameAxisLight.YellowTimer = 0;

       
            // Check if the cross-traffic is also red for at least 4 seconds
            if (trafficLight.AllCrossTrafficIsRed && trafficLight.RedTimer>0) //to prevent the guy who just turned red
            {
                trafficLight.CurrentColor = TrafficLightColor.Green;
                sameAxisLight.CurrentColor = TrafficLightColor.Green;
                Console.WriteLine("Red Changing to Green");
                trafficLight.SetState(new GreenLight());
                sameAxisLight.SetState(new GreenLight());
            }
            trafficLight.RedTimer++;
            sameAxisLight.RedTimer++;


        }
    }
}
