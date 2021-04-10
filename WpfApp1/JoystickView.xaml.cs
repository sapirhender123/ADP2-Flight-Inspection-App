
using System.Windows.Controls;
using FIApp.ViewModels;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for JoystickView.xaml
    /// </summary>
    public partial class JoystickView : UserControl
    {
        private JoystickViewModel vm;
        public JoystickView()
        {
            InitializeComponent();
            Model m = new Model();
            vm = new JoystickViewModel(m);
            DataContext = vm;
           // m.helper();
            //m.connect();
            //m.start();
        }
    }
}
