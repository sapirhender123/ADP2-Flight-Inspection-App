using System;
using System.ComponentModel;

namespace FIApp
{
    class PlayerViewModel : INotifyPropertyChanged
    {
        public Model model;

        public PlayerViewModel(Model model)
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
        public decimal FG_Speed
        {
            get { return model.Speed; }
            set {
                model.Speed = value;
                NotifyPropertyChanged("FG_Speed");
            }
        }

        private String timeString = "00:00:00";
        public int FG_CurrentTime
        {
            get { 
                TimeSpan t = TimeSpan.FromSeconds(model.CurrentTime);
                timeString = string.Format("{0:D2}:{1:D2}:{2:D2}",
                                t.Hours,
                                t.Minutes,
                                t.Seconds);
                NotifyPropertyChanged("FG_TimeString");
                return model.CurrentTime;
            }
            set {
                model.CurrentTime = value;
                NotifyPropertyChanged("FG_CurrentTime");
            }
        }

        public String FG_TimeString
        {
            get {
                return timeString;
            }
        }

        // Slider stuff :-)
        public int Maximum
        {
            set
            {
                model.maxTime_s = value;
                NotifyPropertyChanged("Maximum");
            }
            get
            {
                return model.maxTime_s;
            }
        }
    }
}