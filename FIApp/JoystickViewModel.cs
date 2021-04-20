using System;
using System.ComponentModel;


namespace FIApp.ViewModels
{
    class JoystickViewModel : INotifyPropertyChanged
    {
        public Model model;

        public JoystickViewModel(Model model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                //notify the relevant properties have changed
                NotifyPropertyChanged("VM_Aileron");
                NotifyPropertyChanged("VM_Elevator");
                NotifyPropertyChanged("VM_Rudder");
                NotifyPropertyChanged("VM_Throttle0");
            };
        }


        public event PropertyChangedEventHandler PropertyChanged;
        public void NotifyPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        //properties
        public double VM_Aileron
        {
            get { return model.Aileron; }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
        }
        public double VM_Rudder
        {
            get { return model.Rudder; }
        }
        public double VM_Throttle0
        {
            get { return model.Throttle0; }
        }

    }
}
