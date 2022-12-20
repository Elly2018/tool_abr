﻿using Newtonsoft.Json;
using System.ComponentModel;

namespace Funique
{
    [System.Serializable]
    public sealed class ABRSetting : INotifyPropertyChanged
    {
        string _Name = "Default Name";
        int _Width;
        int _Height;
        string _VideoCodec;
        string _AudioCodec;
        int _BitRate;
        int _CRF;

        [JsonProperty] public string Name
        {
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
            get => _Name;
        }
        [JsonProperty] public int Width
        {
            set 
            {
                _Width = value;
                OnPropertyChanged("Width");
            }
            get => _Width;
        }
        [JsonProperty] public int Height
        {
            set 
            {
                _Height = value;
                OnPropertyChanged("Height");
            }
            get => _Height;
        }
        [JsonProperty] public string VideoCodec
        {
            set
            {
                _VideoCodec = value;
                OnPropertyChanged("VideoCodec");
            }
            get => _VideoCodec;
        }
        [JsonProperty] public string AudioCodec
        {
            set
            {
                _AudioCodec = value;
                OnPropertyChanged("AudioCodec");
            }
            get => _AudioCodec;
        }
        [JsonProperty] public int MaxRate
        {
            set
            {
                _BitRate = value;
                OnPropertyChanged("BitRate");
            }
            get => _BitRate;
        }
        [JsonProperty]
        public int CRF
        {
            set
            {
                _CRF = value;
                OnPropertyChanged("CRF");
            }
            get => _CRF;
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