using System;
using System.ComponentModel;

namespace FIApp.ViewModels
{
    class FlightInstrumentsViewModel : INotifyPropertyChanged
    {
        readonly Model model;
        public FlightInstrumentsViewModel(Model model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("VM_Heading");
                NotifyPropertyChanged("VM_Altimeter");
                NotifyPropertyChanged("VM_Airspeed");
                NotifyPropertyChanged("VM_Yaw");
                NotifyPropertyChanged("VM_Roll");
                NotifyPropertyChanged("VM_Pitch");

            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //properties
        public double VM_Altimeter//////change name in model
        {
            get { return model.Altimeter_indicated_altitude_ft; }
        }

        public double VM_Airspeed
        {
            get { return model.AirspeedKt; }
        }

        //direction
        public double VM_Heading
        {
            get { return model.HeadingDeg; }
        }

        public double VM_Pitch
        {
            get { return model.PitchDeg; }
        }

        public double VM_Roll
        {
            get { return model.RollDeg; }
        }

        public double VM_Yaw
        {
            get { return model.Yaw; }
        }
    }
}
