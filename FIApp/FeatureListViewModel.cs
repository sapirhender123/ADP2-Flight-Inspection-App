using System;
using System.ComponentModel;

namespace FIApp
{
    class FeatureListViewModel : INotifyPropertyChanged
    {
        public Model model;
        public FeatureListViewModel(Model model)
        {
            this.model = model;
            this.model.PropertyChanged += delegate (Object sender, PropertyChangedEventArgs e)
            {
                NotifyPropertyChanged("FL_" + e.PropertyName);
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

        public string FL_CurrentFeature
        {
            get { return model.CurrentFeature; }
            set
            {
                model.CurrentFeature = value;
                NotifyPropertyChanged("FL_CurrentFeature");
            }
        }
    }
}
