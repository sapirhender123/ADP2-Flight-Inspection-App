using System;
using System.ComponentModel;
using System.Windows.Input;

namespace FlightGearSimulator.src
{
    class PlayerViewModel : INotifyPropertyChanged
    {
        readonly IFlightModel model;
        private float prev_speed;

        public PlayerViewModel(IFlightModel model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("FG_" + e.PropertyName);
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
            {
                // The "propName" was changed, notify the relevant items
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propName));
            }
        }

        // Properties
        public float FG_Speed
        {
            get { return model.Speed; }
            set {
                model.Speed = value;
                NotifyPropertyChanged("FG_Speed");
            }
        }

        public int FG_Time
        {
            get { return model.CurrentTime; }
            set {
                model.CurrentTime = value;
                NotifyPropertyChanged("FG_Time");
            }
        }
    // TODO: Actually implement, this is only a stub
    internal interface IFlightModel
    {
        event PropertyChangedEventHandler PropertyChanged;

        int CurrentTime { get; set; }
        float Speed { get; set; }
    }
}