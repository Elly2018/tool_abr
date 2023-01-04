using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System.Windows;
using System.Windows.Input;

namespace Funique.GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public WindowDataContext ctx;

        public MainWindow()
        {
            InitializeComponent();
            this.Title = "ABR Helper";
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            ctx = this.DataContext as WindowDataContext;
            PInputs.ctx = ctx;
            PLadder.ctx = ctx;
            PHLS.ctx = ctx;
            PInput.ctx = ctx;
            PDetail.ctx = ctx;
            PAction.ctx = ctx;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.DefaultExt = ".json";
            ofd.Filter = "Json file (.json)|*.json";
            bool? success = ofd.ShowDialog();
            if (success == true)
            {
                ctx.Setting.Load(ofd.FileName);
                PLadder.ModuleListView.SelectedIndex = -1;
                PLadder.ModuleListView.ItemsSource = ctx.Setting.Settings;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".json";
            sfd.Filter = "Json file (.json)|*.json";
            bool? success = sfd.ShowDialog();
            if (success == true)
            {
                ctx.Setting.Save(sfd.FileName);
            }
        }

        private void Open_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            CommonFileDialogResult result = dialog.ShowDialog();
            if (result == CommonFileDialogResult.Ok)
            {
                ctx.Control.SetDirectory(dialog.FileName);
                this.Title = $"ABR Helper, Path=\'{dialog.FileName}\'";
            }
        }

        private void AD_Click(object sender, RoutedEventArgs e)
        {
            ctx.Setting.Input = "http://vrlive-hamivideo.cdn.hinet.net/nxvi/nxv880e_8k.m3u8";
            ctx.Setting.MasterName = "master.m3u8";
            ctx.Setting.ListSize = 6;
            ctx.Setting.Type = HLSType.FMP4;
            ctx.Setting.PlaylistType = HLSPlaylistType.VOD;
            ctx.Setting.DeleteFlag = true;
            ctx.Setting.AppendFlag = true;
            ctx.Setting.MuxingQueue = 99999;
            ctx.Setting.OutputM3U8FileName = "v%v/prog_index.m3u8";
            ctx.Setting.OutputSegmentFileName = "v%v/fileSequence%d.m4s";
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
