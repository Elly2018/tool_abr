using System.ComponentModel;

namespace Funique
{
    public sealed class JobExecute : INotifyPropertyChanged
    {
        public string _Name;
        public string _Argument;
        public string _Description;
        public JobType _Type;

        public string Name
        {
            set
            {
                _Name = value;
                OnPropertyChanged("Name");
            }
            get => _Name;
        }
        public string Description
        {
            set
            {
                _Description = value;
                OnPropertyChanged("Description");
            }
            get => _Description;
        }
        public string Argument
        {
            set
            {
                _Argument = value;
                OnPropertyChanged("Argument");
            }
            get => _Argument;
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

