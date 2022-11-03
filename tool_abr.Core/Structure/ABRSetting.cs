using System.ComponentModel;

namespace Funique
{
    [System.Serializable]
    public sealed class ABRSetting : INotifyPropertyChanged
    {
        string _Name { set; get; } = "Default Name";
        int _Width { set; get; }
        int _Height { set; get; }
        string _VideoCodec { set; get; }
        string _AudioCodec { set; get; }
        int _BitRate { set; get; }

        public string Name
        {
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
            get => _Name;
        }
        public int Width
        {
            set 
            {
                _Width = value;
                OnPropertyChanged("Width");
            }
            get => _Width;
        }
        public int Height
        {
            set 
            {
                _Height = value;
                OnPropertyChanged("Height");
            }
            get => _Height;
        }
        public string VideoCodec
        {
            set
            {
                _VideoCodec = value;
                OnPropertyChanged("VideoCodec");
            }
            get => _VideoCodec;
        }
        public string AudioCodec
        {
            set
            {
                _AudioCodec = value;
                OnPropertyChanged("AudioCodec");
            }
            get => _AudioCodec;
        }
        public int BitRate
        {
            set
            {
                _BitRate = value;
                OnPropertyChanged("BitRate");
            }
            get => _BitRate;
        }

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