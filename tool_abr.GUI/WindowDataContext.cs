using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Funique.GUI
{
    public class WindowDataContext : INotifyPropertyChanged
    {
        public M3U8Setting Setting { set; get; } = new M3U8Setting();
        public ABRControl Control { set; get; } = new ABRControl();

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string name = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
        }
    }
}
