using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace WpfApp1
{
    class FlightInstrumentsViewModel : INotifyPropertyChanged
    {
        private Model model;
        public FlightInstrumentsViewModel(Model model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                if (e.PropertyName == "CurrentTime")
                {
                    NotifyPropertyChanged("VM_Heading");
                    NotifyPropertyChanged("VM_Altimeter");
                    NotifyPropertyChanged("VM_Airspeed");
                    NotifyPropertyChanged("VM_Yaw");
                    NotifyPropertyChanged("VM_Roll");
                    NotifyPropertyChanged("VM_Pitch");
                }
                
            };
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged.Invoke(this, new PropertyChangedEventArgs(propName));
        }

        //properties
        public float VM_Altimeter//////change name in model
        {
            get { return model.Altimeter_indicated_altitude_ft; }
        }

        public float VM_Airspeed
        {
            get { return model.AirspeedKt; }
        }

        //direction
        public float VM_Heading
        {
            get { return model.HeadingDeg; }
        }

        public float VM_Pitch
        {
            get { return model.PitchDeg; }
        }

        public float VM_Roll
        {
            get { return model.RollDeg; }
        }

        public float VM_Yaw
        {
            get { return model.Yaw; }
        }
    }
}
