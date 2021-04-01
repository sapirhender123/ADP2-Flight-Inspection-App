using System.ComponentModel;

namespace FlightGearSimulator.src
{
    // TODO: Actually implement, this is only a stub
    public interface IFlightModel
    {
        event PropertyChangedEventHandler PropertyChanged;
    }

    class FlightModel : IFlightModel
    {
        public int CurrentTime { get; set; }
        public float Speed { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
