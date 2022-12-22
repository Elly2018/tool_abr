using Microsoft.Win32;
using Microsoft.WindowsAPICodePack.Dialogs;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using System.Xml.Serialization;
using System.Xml;

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
            ctx = new WindowDataContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MasterM3U8Input.DataContext = ctx.Setting;
            MasterM3U8HLS.DataContext = ctx.Setting;
            MasterM3U8Detail.DataContext = ctx.Setting;
            ModuleOptionsView.DataContext = null;
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            ctx.Control.Call(ctx.Setting);
        }

        private void CombineButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Addnew_Click(object sender, RoutedEventArgs e)
        {
            ctx.Setting.Settings.Add(new ABRSetting());
            ModuleListView.ItemsSource = ctx.Setting.Settings;
        }

        private void Removeabr_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                ContextMenu parentContextMenu = menuItem.CommandParameter as ContextMenu;
                if (parentContextMenu != null)
                {
                    Debug.WriteLine(parentContextMenu.PlacementTarget.GetType().FullName);
                    StackPanel listViewItem = parentContextMenu.PlacementTarget as StackPanel;
                    ABRSetting cf = listViewItem.DataContext as ABRSetting;
                    if(ModuleOptionsView.DataContext == cf)
                    {
                        ModuleOptionsView.DataContext = null;
                    }
                    ctx.Setting.Settings.Remove(cf);
                    ModuleListView.ItemsSource = ctx.Setting.Settings;
                }
            }
        }

        private void MoveUp_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                ContextMenu parentContextMenu = menuItem.CommandParameter as ContextMenu;
                if (parentContextMenu != null)
                {
                    Debug.WriteLine(parentContextMenu.PlacementTarget.GetType().FullName);
                    StackPanel listViewItem = parentContextMenu.PlacementTarget as StackPanel;
                    ABRSetting cf = listViewItem.DataContext as ABRSetting;
                    int i = ctx.Setting.Settings.IndexOf(cf);
                    if (i > 0)
                    {
                        ctx.Setting.Settings.Move(i - 1, i);
                    }
                }
            }
        }
        private void MoveDown_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                ContextMenu parentContextMenu = menuItem.CommandParameter as ContextMenu;
                if (parentContextMenu != null)
                {
                    Debug.WriteLine(parentContextMenu.PlacementTarget.GetType().FullName);
                    StackPanel listViewItem = parentContextMenu.PlacementTarget as StackPanel;
                    ABRSetting cf = listViewItem.DataContext as ABRSetting;
                    int i = ctx.Setting.Settings.IndexOf(cf);
                    if(i < ctx.Setting.Settings.Count - 1)
                    {
                        ctx.Setting.Settings.Move(i, i + 1);
                    }
                }
            }
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
                ModuleListView.SelectedIndex = -1;
                ModuleListView.ItemsSource = ctx.Setting.Settings;
            }
        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog sfd = new SaveFileDialog();
            sfd.DefaultExt = ".json";
            sfd.Filter = "Json file (.json)|*.json";
            bool? success = sfd.ShowDialog();
            if(success == true)
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

        private void ModuleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;
            if (lb.SelectedIndex == -1)
            {
                ModuleOptionsView.DataContext = null;
                return;
            }
            ABRSetting target = ctx.Setting.Settings[lb.SelectedIndex];
            ModuleOptionsView.DataContext = target;
        }
    }
}
