using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Funique.GUI.Pages
{
    /// <summary>
    /// Interaction logic for Ladder.xaml
    /// </summary>
    public partial class Ladder : UserControl
    {
        public WindowDataContext ctx;

        public Ladder()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ModuleOptionsView.DataContext = null;
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
                    if (ModuleOptionsView.DataContext == cf)
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
                    if (i < ctx.Setting.Settings.Count - 1)
                    {
                        ctx.Setting.Settings.Move(i, i + 1);
                    }
                }
            }
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

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
