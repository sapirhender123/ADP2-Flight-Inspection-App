
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
            Model m = new Model();
            vm = new FlightInstrumentsViewModel(m);
            DataContext = vm;
        //    m.connect();
          //  m.start();
            m.helper();
        }
    }
}
