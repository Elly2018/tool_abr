using Newtonsoft.Json;
using System.ComponentModel;

namespace Funique
{
    public sealed class MultipleInput : INotifyPropertyChanged
    {
        public string _Name = "Default Name";
        public bool _Sync;
        public int _StreamingLoop;
        public string _Input;
        public int _StartNumber;
        public string _Framerate;

        [JsonProperty]
        public string Name
        {
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
            get => _Name;
        }
        [JsonProperty]
        public bool Sync
        {
            set
            {
                _Sync = value;
                OnPropertyChanged("Sync");
            }
            get => _Sync;
        }
        [JsonProperty]
        public int StreamingLoop
        {
            set
            {
                _StreamingLoop = value;
                OnPropertyChanged("StreamingLoop");
            }
            get => _StreamingLoop;
        }
        [JsonProperty]
        public int StartNumber
        {
            set
            {
                _StartNumber = value;
                OnPropertyChanged("StartNumber");
            }
            get => _StartNumber;
        }
        [JsonProperty]
        public string Input
        {
            set
            {
                _Input = value;
                OnPropertyChanged("Input");
            }
            get => _Input;
        }
        [JsonProperty]
        public string Framerate
        {
            set
            {
                _Framerate = value;
                OnPropertyChanged("Framerate");
            }
            get => _Framerate;
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
