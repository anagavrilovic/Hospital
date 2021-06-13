using Hospital.Factory;
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

        public FeedbackService(IFeedbackRepositoryFactory factory)
        {
            feedbackRepository = factory.CreateFeedbackRepository();
        }

        public void SaveFeedBack(Feedback feedback)
        {
            feedbackRepository.Save(feedback);
        }

        public String GetNewID()
        {
            return feedbackRepository.GetNewID();
        }
    }
}
