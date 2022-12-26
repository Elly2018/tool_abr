using System.ComponentModel;

namespace Funique
{
    [System.Serializable]
    public sealed class AudioSetting : INotifyPropertyChanged
    {
        string _Source;
        string _Codec;

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(string property)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(property));
            }
        }
    }
}
