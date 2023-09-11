using TrafficLightsAPI.Application.Contracts;
using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Application
{
    public class GreenLight : ITrafficLightState
    {
        private readonly int normalGreenDuration = 20; // seconds
        private readonly int peakGreenDuration = 40; // seconds
        private readonly int northRightTurnDuration = 10; // seconds

        void ITrafficLightState.ChangeState(TrafficLight trafficLight, TrafficLight sameAxisLight)
        {
            trafficLight.RedTimer = 0;
            sameAxisLight.RedTimer = 0;

            trafficLight.YellowTimer = 0;
            sameAxisLight.YellowTimer = 0;

           

            if ((trafficLight.IsPeakHour && trafficLight.GreenTimer >= peakGreenDuration) 
                || (trafficLight.GreenTimer >= normalGreenDuration))
            {
                    trafficLight.CurrentColor = TrafficLightColor.Yellow;
                    sameAxisLight.CurrentColor = TrafficLightColor.Yellow;

                    Console.WriteLine("Green Changing to Yellow");
                    trafficLight.SetState(new YellowLight());
                    sameAxisLight.SetState(new YellowLight());

            }



            //Bonus 
            if (trafficLight.Direction == "North") // || trafficLight.Direction == "South")
            {
                if ((trafficLight.IsPeakHour && trafficLight.GreenTimer >= peakGreenDuration - northRightTurnDuration)
                    || (trafficLight.GreenTimer >= normalGreenDuration - northRightTurnDuration))
                {
                    trafficLight.IsNorthRightTurnGreen = true;
                }
                else
                {
                    trafficLight.IsNorthRightTurnGreen = false;
                }

                if (trafficLight.IsNorthRightTurnGreen)  //set south light to red
                {
                    //if (trafficLight.Direction == "South")
                    //{
                    //    trafficLight.CurrentColor = TrafficLightColor.Red;
                    //    Console.WriteLine("Bonus - Green Changing to Red");
                    //    trafficLight.SetState(new RedLight());
                    //}

                    //if (sameAxisLight.Direction == "South")
                    //{
                        sameAxisLight.CurrentColor = TrafficLightColor.Red;
                        Console.WriteLine("Bonus - Green Changing to Red");
                        sameAxisLight.SetState(new RedLight());
                    //}
                }
            }

            trafficLight.GreenTimer++;
            sameAxisLight.GreenTimer++;


        }
    }
}
