
using System.Windows.Controls;
using FIApp.ViewModels;

namespace FIApp.Views
{

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
