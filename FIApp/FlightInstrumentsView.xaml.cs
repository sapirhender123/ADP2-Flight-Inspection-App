using System.Windows.Controls;
using FIApp.ViewModels;

namespace FIApp.Views
{

    public partial class FlightInstrumentsView : UserControl
    {
        readonly FlightInstrumentsViewModel vm;

        public FlightInstrumentsView()
        {
            InitializeComponent();
            vm = new FlightInstrumentsViewModel(model);
            DataContext = vm;
        }
    }
}
