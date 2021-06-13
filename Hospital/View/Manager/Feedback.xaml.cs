using Hospital.ViewModels.Manager;
using System.Windows.Controls;

namespace Hospital.View.Manager
{
    public partial class Feedback : Page
    {
        public Feedback(FeedbackViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}
