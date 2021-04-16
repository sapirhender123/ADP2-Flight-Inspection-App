
using System.Windows.Controls;
using FIApp.ViewModels;

namespace FIApp.Views
{
    /// <summary>
    /// Interaction logic for JoystickView.xaml
    /// </summary>
    public partial class JoystickView : UserControl
    {
        readonly JoystickViewModel vm;
        public JoystickView()
        {
            InitializeComponent();
            vm = new JoystickViewModel(model);
            DataContext = vm;
        }
    }
}
