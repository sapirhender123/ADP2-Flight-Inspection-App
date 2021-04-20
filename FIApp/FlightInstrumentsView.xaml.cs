using System.Windows.Controls;
using FIApp.ViewModels;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for FlightInstrumentsView.xaml
    /// </summary>
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
