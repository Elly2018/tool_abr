using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace Funique.GUI.Pages
{
    /// <summary>
    /// Interaction logic for Action.xaml
    /// </summary>
    public partial class Action : UserControl
    {
        public WindowDataContext ctx;

        public Action()
        {
            InitializeComponent();
        }

        private void UserControl_Loaded(object sender, RoutedEventArgs e)
        {
            JobView.DataContext = ctx;
            ctx.Control.log = (x) =>
            {
                Dispatcher.Invoke(() =>
                {
                    Log.Text += x;
                    Log.ScrollToEnd();
                },
                DispatcherPriority.Background);
            };
        }


        private void RunButton_Click(object sender, RoutedEventArgs e)
        {
            ctx.Control.Call(ctx.Setting.Jobs.ToArray());
        }
        private void KillButton_Click(object sender, RoutedEventArgs e)
        {
            ctx.Control.Kill();
        }
        private void AnalysisButton_Click(object sender, RoutedEventArgs e)
        {
            var ass = ctx.Control.Analysis(ctx.Setting);
            ctx.Setting.Jobs.Clear();
            foreach (var i in ass) ctx.Setting.Jobs.Add(i);
            ctx.Setting.OnPropertyChanged("Jobs");
        }
        private void CleanButton_Click(object sender, RoutedEventArgs e)
        {
            Log.Text = string.Empty;
        }
        private void RunSingleButton_Click(object sender, RoutedEventArgs e)
        {
            JobExecute execute = JobModuleOptionsView.DataContext as JobExecute;
            if (execute == null) return;
            ctx.Control.RunSingle(execute);
        }

        private void ModuleListView_JobSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox lb = e.OriginalSource as ListBox;
            if (lb.SelectedIndex == -1)
            {
                JobModuleOptionsView.DataContext = null;
                return;
            }
            JobExecute target = ctx.Setting.Jobs[lb.SelectedIndex];
            JobModuleOptionsView.DataContext = target;
        }
    }
}
