using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Funique.GUI.Pages
{
    /// <summary>
    /// Interaction logic for Input.xaml
    /// </summary>
    public partial class Input : UserControl
    {
        public WindowDataContext ctx;

        public Input()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MasterM3U8Input.DataContext = ctx.Setting;
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
