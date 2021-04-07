using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using WpfApp1;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WpfApp1.User_Controls
{
    /// <summary>
    /// Interaction logic for FlightInstrumentsView.xaml
    /// </summary>
    public partial class FlightInstrumentsView : UserControl
    {
        private FlightInstrumentsViewModel vm;
        public FlightInstrumentsView()
        {
            InitializeComponent();
            Model m = new Model();
            vm = new FlightInstrumentsViewModel(m);
            DataContext = vm;
            m.helper();
        }
    }
}
