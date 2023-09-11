using TrafficLightsAPI.Model;

namespace TrafficLightsAPI.Data
{
    //[TODO] To be integrated and implemented - entity kept different for model for flexibility
    public class TrafficLightEntity
    {
        public int Id { get; set; }
        public string Direction { get; set; }
        public TrafficLightColor CurrentColor { get; set; }
        public int Timer { get; set; }
        public bool IsPeak { get; set; }
    }
}
