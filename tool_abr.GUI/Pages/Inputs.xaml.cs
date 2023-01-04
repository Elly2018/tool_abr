using System.Diagnostics;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Funique.GUI.Pages
{
    /// <summary>
    /// Interaction logic for Ladder.xaml
    /// </summary>
    public partial class MultipleInputs : UserControl
    {
        public WindowDataContext ctx;

        public MultipleInputs()
        {
            InitializeComponent();
        }
        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            InputsOptionsView.DataContext = null;
            this.DataContext = ctx;
        }

        private void Inputs_Addnew_Click(object sender, RoutedEventArgs e)
        {
            ctx.Setting.Inputs.Add(new MultipleInput());
            InputsListView.ItemsSource = ctx.Setting.Inputs;
        }
        private void Inputs_Removeabr_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                ContextMenu parentContextMenu = menuItem.CommandParameter as ContextMenu;
                if (parentContextMenu != null)
                {
                    Debug.WriteLine(parentContextMenu.PlacementTarget.GetType().FullName);
                    StackPanel listViewItem = parentContextMenu.PlacementTarget as StackPanel;
                    MultipleInput cf = listViewItem.DataContext as MultipleInput;
                    if (InputsOptionsView.DataContext == cf)
                    {
                        InputsOptionsView.DataContext = null;
                    }
                    ctx.Setting.Inputs.Remove(cf);
                    InputsListView.ItemsSource = ctx.Setting.Inputs;
                }
            }
        }

        private void Inputs_MoveUp_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                ContextMenu parentContextMenu = menuItem.CommandParameter as ContextMenu;
                if (parentContextMenu != null)
                {
                    Debug.WriteLine(parentContextMenu.PlacementTarget.GetType().FullName);
                    StackPanel listViewItem = parentContextMenu.PlacementTarget as StackPanel;
                    MultipleInput cf = listViewItem.DataContext as MultipleInput;
                    int i = ctx.Setting.Inputs.IndexOf(cf);
                    if (i > 0)
                    {
                        ctx.Setting.Inputs.Move(i - 1, i);
                    }
                }
            }
        }
        private void Inputs_MoveDown_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = sender as MenuItem;
            if (menuItem != null)
            {
                ContextMenu parentContextMenu = menuItem.CommandParameter as ContextMenu;
                if (parentContextMenu != null)
                {
                    Debug.WriteLine(parentContextMenu.PlacementTarget.GetType().FullName);
                    StackPanel listViewItem = parentContextMenu.PlacementTarget as StackPanel;
                    MultipleInput cf = listViewItem.DataContext as MultipleInput;
                    int i = ctx.Setting.Inputs.IndexOf(cf);
                    if (i < ctx.Setting.Inputs.Count - 1)
                    {
                        ctx.Setting.Inputs.Move(i, i + 1);
                    }
                }
            }
        }

        private void InputsListView_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;
            if (lb.SelectedIndex == -1)
            {
                InputsOptionsView.DataContext = null;
                return;
            }
            MultipleInput target = ctx.Setting.Inputs[lb.SelectedIndex];
            InputsOptionsView.DataContext = target;
        }

        private void TextBox_IntOnly(object sender, TextCompositionEventArgs e)
        {
            e.Handled = !int.TryParse(e.Text, out _);
        }
    }
}
