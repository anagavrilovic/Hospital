using Hospital.ViewModels.Manager;
using System.Windows.Controls;

namespace Hospital.View
{
    public partial class MedicinesWindow : Page
    {
        public MedicinesWindow(MedicinesWindowViewModel viewModel)
        {
            InitializeComponent();
            this.DataContext = viewModel;
        }
    }
}


