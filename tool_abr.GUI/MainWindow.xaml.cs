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
            ctx = new WindowDataContext();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            MasterM3U8View.DataContext = ctx.Setting;
            ModuleOptionsView.DataContext = null;
            ModuleListView_RightClickMenu();
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }

        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            Debug.WriteLine(ctx.Setting.Arguments);
        }

        void ModuleListView_RightClickMenu()
        {
            ContextMenu cm = new ContextMenu();
            MenuItem Addnew = new MenuItem();
            Addnew.Header = "New";
            Addnew.Click += Addnew_Click;
            cm.Items.Add(Addnew);
            ModuleListView.ContextMenu = cm;
        }

        private void Addnew_Click(object sender, RoutedEventArgs e)
        {
            ctx.Setting.Settings.Add(new ABRSetting());
            ModuleListView.ItemsSource = ctx.Setting.Settings;
        }

        private void Exit_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Load_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ModuleListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;
            ABRSetting target = ctx.Setting.Settings[lb.SelectedIndex];
            ModuleOptionsView.DataContext = target;
        }
    }
}
