using System.Windows;
using System.Windows.Input;

namespace Hospital.View
{
    public partial class MenagerWindow : Window
    {        
        public MenagerWindow()
        {
            InitializeComponent();
        }

        private void BackButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void OnMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            this.DragMove();
        }
    }
}

  
