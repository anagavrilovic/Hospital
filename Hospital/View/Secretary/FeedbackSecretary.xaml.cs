﻿using Hospital.Model;
using Hospital.Services;
using Hospital.ViewModels.Secretary;
using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Hospital.View.Secretary
{
    public partial class FeedbackSecretary : Page
    {
        public FeedbackSecretary(FeedbackViewModel feedbackViewModel)
        {
            InitializeComponent();
            this.DataContext = feedbackViewModel;
        }
    }
}
