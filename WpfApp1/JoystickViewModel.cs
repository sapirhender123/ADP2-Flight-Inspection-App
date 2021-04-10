using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;


namespace FIApp.ViewModels
{
    class JoystickViewModel : INotifyPropertyChanged
    {
        private Model model;
        public JoystickViewModel(Model model)
        {
            this.model = model;
            model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e) 
            { 
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
            get {
                return model.Aileron; }
        }
        public double VM_Elevator
        {
            get { return model.Elevator; }
        }
        public double VM_Eudder
        {
             get { return model.Rudder; }
        }
        public double VM_Throttle0
        {
             get { return model.Throttle0; }
        }

    }
}
