﻿using Hospital.Commands.DoctorCommands;
using Hospital.Controller;
using Hospital.Model;
using Hospital.Services;
using Hospital.View.Doctor;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Hospital.ViewModels.Doctor
{
    public class DoctorFeedbackViewModel : ViewModel
    {
        FeedbackService service;
        NavigationController navigationController;
        private Feedback feedback=new Feedback();
        public Feedback Feedback
        {
            get
            {
                return feedback;
            }
            set
            {
                if (value != feedback)
                {
                    feedback = value;
                    OnPropertyChanged("Feedback");
                }
            }
        }
        private Boolean isEnable;
        public Boolean IsEnable
        {
            get
            {
                return isEnable;
            }
            set
            {
                if (value != isEnable)
                {
                    isEnable = value;
                    OnPropertyChanged("IsEnable");
                }
            }
        }
        private FeedbackType selectedType;
        public FeedbackType SelectedType
        {
            get
            {
                return selectedType;
            }
            set
            {
                if (value != selectedType)
                {
                    selectedType = value;
                    OnPropertyChanged("SelectedType");
                }
            }
        }
        private RelayCommand saveCommand;
        public RelayCommand SaveCommand
        {
            get { return saveCommand; }
            set
            {
                saveCommand = value;
            }
        }

        public DoctorFeedbackViewModel(string doctorId, NavigationController navigationController)
        {
            this.navigationController = navigationController;
            Feedback.UserId = doctorId;
            SaveCommand = new RelayCommand(Execute_Save, CanExecute_Command);
            service = new FeedbackService();
            IsEnable = true;
        }
        private void Execute_Save(object obj)
        {   
            if (Feedback.Comment == null)
                return;
            Feedback.FeedbackType = SelectedType;
            service.SaveFeedBack(Feedback);
            ErrorBox box = new ErrorBox("Feedback uspešno sačuvan.");
            IsEnable = false;
        }

        private bool CanExecute_Command(object obj)
        {
            return true;
        }
    }
}
