using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class FeedbackService
    {
        IFeedbackRepository feedbackRepository;

        public FeedbackService(IFeedbackRepository repository)
        {
            feedbackRepository = repository;
        }

        public void SaveFeedBack(Feedback feedback)
        {
            feedbackRepository.Save(feedback);
        }
    }
}
