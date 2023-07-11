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
            ctx.Setting.Overwrite = true;
            ctx.Setting.Type = HLSType.FMP4;
            ctx.Setting.PlaylistType = HLSPlaylistType.VOD;
            ctx.Setting.Cache = true;
            ctx.Setting.DeleteFlag = true;
            ctx.Setting.AppendFlag = true;
            ctx.Setting.HWAccelType = HWAccelType.CUDA;
            ctx.Setting.MuxingQueue = 99999;
            ctx.Setting.AudioCodec = "acc";
            ctx.Setting.SubtitleCodec = "srt";
            ctx.Setting.OutputSingleFile = true;
            ctx.Setting.AudioSource = AudioSourceType.FromFile;
            ctx.Setting.OutputM3U8FileName = "v%v/prog_index.m3u8";
            ctx.Setting.OutputSegmentFileName = "v%v/fileSequence%d.m4s";
            ctx.Setting.OutputAudioM3U8FileName = "Audio/Audio.m3u8";
            ctx.Setting.OutputAudioSegmentFileName = "Audio/Audio.acc";
            ctx.Setting.OutputSubtitleM3U8FileName = "Text/Text.m3u8";
            ctx.Setting.OutputSubtitleSegmentFileName = "Text/Text.srt";
            ABRSetting setting1 = new ABRSetting();
            setting1.Name = "Original";
            setting1.FileName = "OG.mp4";
            setting1.CRF = 4;
            setting1.PixFmt = "yuv420p";
            setting1.AudioCodec = "aac";
            setting1.VideoCodec = "hevc";
            setting1.NOG = 1;
            setting1.Keyint = 150;
            setting1.FPS = "50";
            ABRSetting setting2 = new ABRSetting();
            setting2.Name = "6K";
            setting2.FileName = "6K.mp4";
            setting2.Width = 5760;
            setting2.Height = 3240;
            setting2.CRF = 22;
            setting2.PixFmt = "yuv420p";
            setting2.AudioCodec = "aac";
            setting2.VideoCodec = "hevc";
            setting2.NOG = 1;
            setting2.Keyint = 150;
            setting2.FPS = "50";
            ABRSetting setting3 = new ABRSetting();
            setting3.Name = "4K";
            setting3.FileName = "4K.mp4";
            setting3.Width = 3840;
            setting3.Height = 2160;
            setting3.CRF = 22;
            setting3.PixFmt = "yuv420p";
            setting3.AudioCodec = "aac";
            setting3.VideoCodec = "hevc";
            setting3.NOG = 1;
            setting3.Keyint = 150;
            setting3.FPS = "50";
            ctx.Setting.Settings.Clear();
            ctx.Setting.Settings.Add(setting1);
            ctx.Setting.Settings.Add(setting2);
            ctx.Setting.Settings.Add(setting3);
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
