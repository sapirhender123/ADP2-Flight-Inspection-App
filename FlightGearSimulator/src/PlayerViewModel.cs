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
            }
        }

        // Properties
        public float FG_Speed
        {
            get { return model.Speed; }
        }

        public int FG_Time
        {
            get { return model.CurrentTime; }
        }

        // Play buttons callbacks
        private void PlayController_Play(object sender, MouseButtonEventArgs e)
        {
            model.Speed = prev_speed;
        }

        private void PlayController_FastFordward(object sender, MouseButtonEventArgs e)
        {

        }

        private void PlayController_MoveBack(object sender, MouseButtonEventArgs e)
        {

        }

        private void PlayController_Pause(object sender, MouseButtonEventArgs e)
        {
            prev_speed = FG_Speed;
            model.Speed = 0;
        }

        private void PlayController_Stop(object sender, MouseButtonEventArgs e)
        {
            model.CurrentTime = 0;
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