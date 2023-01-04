using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Funique
{
    public sealed class JobExecute : INotifyPropertyChanged
    {
        string _Name;
        string _Argument;
        ObservableCollection<string> _Arguments = new ObservableCollection<string>();
        Action _BeginProcess;
        Action _DoneProcess;
        string _Description;
        JobType _Type = JobType.FFMPEG;

        public JobExecute() { }

        public JobExecute(string name, string argument)
        {
            Name = name;
            Argument = argument;
        }

        public JobExecute(string name, string description, string argument)
        {
            Name = name;
            Description = description;
            Argument = argument;
        }

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
        /// <summary>
        /// Will execute first
        /// </summary>
        public string Argument
        {
            set
            {
                _Argument = value;
                OnPropertyChanged("Argument");
            }
            get => _Argument;
        }
        /// <summary>
        /// Will execute one by one
        /// </summary>
        public ObservableCollection<string> Arguments
        {
            set
            {
                _Arguments = value;
                OnPropertyChanged("Arguments");
            }
            get => _Arguments;
        }
        public Action BeginProcess
        {
            set
            {
                _BeginProcess = value;
                OnPropertyChanged("BeginProcess");
            }
            get => _BeginProcess;
        }
        public Action DoneProcess
        {
            set
            {
                _DoneProcess = value;
                OnPropertyChanged("DoneProcess");
            }
            get => _DoneProcess;
        }

        public JobType Type
        {
            set
            {
                _Type = value;
                OnPropertyChanged("Type");
            }
            get => _Type;
        }
        public string[] ArgumentList
        {
            get
            {
                List<string> list_args = new List<string>();
                if(!string.IsNullOrEmpty(Argument)) list_args.Add(Argument);
                if(Arguments.Count > 0) list_args.AddRange(Arguments);
                return list_args.ToArray();
            }
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

