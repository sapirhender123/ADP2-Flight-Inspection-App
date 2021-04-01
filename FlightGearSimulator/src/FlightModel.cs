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
        private int maxTime_s = 2 * 60;
        public FlightModel() {
            (new Thread(() =>
            {
                while (true)
                {
                    Thread.Sleep(1000);
                    CurrentTime += (int)(10 * Speed);
                    if (CurrentTime >= maxTime_s)
                    {
                        CurrentTime = maxTime_s;
                        NotifyPropertyChanged("CurrentTime");
                        return;
                    }

                    NotifyPropertyChanged("CurrentTime");
                    Console.WriteLine(".");
                }
            })).Start();
        }

        public int CurrentTime { get; set; }
        public float Speed { get; set; }
        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                // The "propName" was changed, notify the relevant items
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }
    }
}
