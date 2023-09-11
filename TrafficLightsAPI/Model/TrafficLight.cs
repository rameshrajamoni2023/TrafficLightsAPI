using TrafficLightsAPI.Application;
using TrafficLightsAPI.Application.Contracts;

namespace TrafficLightsAPI.Model
{
    // I have followed the "State Design Pattern" 
    // I have a class which maintains a reference to the current state.
    // I have classes for behavior of a specific state (e.g., GreenLightState, YellowLightState, RedLightState).
    // For Production Level sytems which are complex - we will have to upgrade this to use multi-threading and related algos.
    // This is a simple proof of concept project

    // [TODO] we can do away with individual timers now - have only one timer property and one RightTurnGreen  

    public class TrafficLight
    {
        public string Direction { get; set; }
        public TrafficLightColor CurrentColor { get; set; }
        public int GreenTimer { get; set; }
        public int YellowTimer { get; set; }

        public int RedTimer { get; set; }

        public bool IsNorthRightTurnGreen { get; set; }  //Bonus - property for right-turn signal

        public bool IsPeakHour {  get; set; }

        public bool AllCrossTrafficIsRed { get; set; }  

        private ITrafficLightState currentState;

        

        public TrafficLight()
        {
            Direction = "";
            CurrentColor = TrafficLightColor.Red;
            GreenTimer = 0;
            YellowTimer = 0;
            RedTimer = 0;
            IsNorthRightTurnGreen = false;
            currentState = new RedLight();
        }

        public TrafficLight(string direction)
        {
            Direction = direction;
            CurrentColor = TrafficLightColor.Red;
            GreenTimer = 0;
            YellowTimer = 0;
            RedTimer = 0;
            IsNorthRightTurnGreen = false;
            currentState = new RedLight();
        }

        public void SetState(ITrafficLightState state)
        {
            currentState = state;
        }

        public void Change(TrafficLight sameAxisLight)
        {
           
            currentState.ChangeState(this,sameAxisLight);
        }



    }


    public enum TrafficLightColor
    {
        Green,
        Yellow,
        Red
    }


}