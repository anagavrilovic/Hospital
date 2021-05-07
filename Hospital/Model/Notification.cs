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

        public Notification()
        {

        }

        public Notification(string title, string content)
        {
            this.Title = title;
            this.Content = content;

            var timeNow = DateTime.Now;
            this.Date = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Kind);

            this.Id = this.GetHashCode().ToString();
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
