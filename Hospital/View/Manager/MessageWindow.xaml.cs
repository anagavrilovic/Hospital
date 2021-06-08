using System.ComponentModel;
using System.Windows;

namespace Hospital.View.Manager
{
    public partial class MessageWindow : Window, INotifyPropertyChanged
    {
        private string message;
        public string Message
        {
            get { return message; }
            set
            {
                message = value;
                OnPropertyChanged("Message");
            }
        }

        public MessageWindow(string message)
        {
            InitializeComponent();
            this.DataContext = this;
            this.Message = message;
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
    }
}
