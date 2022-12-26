using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Funique.GUI.Pages
{
    /// <summary>
    /// Interaction logic for Detail.xaml
    /// </summary>
    public partial class Detail : UserControl
    {
        public WindowDataContext ctx;

        public Detail()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            MasterM3U8Detail.DataContext = ctx.Setting;
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
