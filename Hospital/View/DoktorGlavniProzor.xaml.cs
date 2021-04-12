using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Hospital.View
{
    /// <summary>
    /// Interaction logic for DoktorGlavniProzor.xaml
    /// </summary>
    public partial class DoktorGlavniProzor : Window, INotifyPropertyChanged
    {
    
        private string IDnumber;
        private ObservableCollection<string> test;

        public event PropertyChangedEventHandler PropertyChanged;

        public ObservableCollection<string> Test
        {
            get
            {
                return test;
            }
            set
            {
                if (value != test)
                {
                    test = value;
                    OnPropertyChanged("Test");
                }
            }
        }
        protected void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }

        public DoktorGlavniProzor(string IDnumber)
        {
            this.Height = (System.Windows.SystemParameters.PrimaryScreenHeight * 3 / 4);
            this.Width = (System.Windows.SystemParameters.PrimaryScreenWidth * 3 / 4);
            Test = new ObservableCollection<string>();
            this.IDnumber = IDnumber;
            Test.Add("Drugi");
            Test.Add("Drugi");
            Test.Add("Treci");
            Test.Add("Drugi");
            InitializeComponent();
            this.DataContext = this;
            
        }

        private void Pregled(object sender, RoutedEventArgs e)
        {
            Doctor_Examination de = new Doctor_Examination(IDnumber);
            de.Owner = Application.Current.MainWindow;
            de.Show();
            this.Hide();
        }

        private void Logo(object sender, RoutedEventArgs e)
        {

        }

        private void SignOut(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this.Owner).Show();
            this.Close();
        }

        private void Announcment(object sender, RoutedEventArgs e)
        {
           // Uri uri = @"pack://application:,,,/Subfolder/ResourceFile.xaml";
            obavestenje.Source = new BitmapImage(new Uri("pack://application:,,,/Icon/announcment.png", UriKind.Absolute));
            view.Visibility = Visibility.Visible;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            KalendarTermini k = new KalendarTermini(IDnumber);
            k.Owner = this;
            this.Hide();
            k.Show();
        }
    }
}
