using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Funique.GUI.Pages
{
    /// <summary>
    /// Interaction logic for HLS.xaml
    /// </summary>
    public partial class HLS : UserControl
    {
        public WindowDataContext ctx;

        public HLS()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MasterM3U8HLS.DataContext = ctx.Setting;
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
