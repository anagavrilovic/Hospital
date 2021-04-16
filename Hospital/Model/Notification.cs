using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Notification : INotifyPropertyChanged
    {
        private string id;

        public string Id
        {
            get { return id; }
            set 
            { 
                id = value;
                OnPropertyChanged("Id");
            }
        }

        private string title;

        public string Title
        {
            get { return title; }
            set 
            { 
                title = value;
                OnPropertyChanged("Title");
            }
        }

        private string content;

        public string Content
        {
            get { return content; }
            set 
            { 
                content = value;
                OnPropertyChanged("Content");
            }
        }

        private DateTime date;

        public DateTime Date
        {
            get { return date; }
            set 
            { 
                date = value;
                OnPropertyChanged("Date");
            }
        }


        private bool sekretar;
        private bool lekar;
        private bool upravnik;
        private bool pacijent;

        public bool Pacijent
        {
            get { return pacijent; }
            set { pacijent = value; }
        }

        public bool Upravnik
        {
            get { return upravnik; }
            set { upravnik = value; }
        }

        public bool Lekar
        {
            get { return lekar; }
            set { lekar = value; }
        }

        public bool Sekretar
        {
            get { return sekretar; }
            set { sekretar = value; }
        }

        private Dictionary<string, bool> recipients = new Dictionary<string, bool>();

        public Dictionary<string, bool> Recipients
        {
            get { return recipients; }
            set { recipients = value; }
        }

        override
        public string ToString()
        {
            StringBuilder sb = new StringBuilder("");
            sb.Append(Date.ToString("dd.MM.yyyy HH:mm"));
            sb.Append("\t");
            sb.Append(Title);
            sb.AppendLine();
            sb.Append(Content);

            return sb.ToString();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

    }
}
