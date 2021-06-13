using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Notification
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime Date { get; set; }

        public Notification() {}

        public Notification(string title, string content)
        {
            this.Title = title;
            this.Content = content;

            var timeNow = DateTime.Now;
            this.Date = new DateTime(timeNow.Year, timeNow.Month, timeNow.Day, timeNow.Hour, timeNow.Minute, timeNow.Second, timeNow.Kind);

            this.Id = this.GetHashCode().ToString();
        }
    }
}
