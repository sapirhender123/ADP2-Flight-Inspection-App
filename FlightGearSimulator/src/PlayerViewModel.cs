﻿using System;
using System.ComponentModel;

namespace FlightGearSimulator.src
{
    class PlayerViewModel : INotifyPropertyChanged
    {
        public FlightModel model;

        public PlayerViewModel(FlightModel model)
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
    }
}