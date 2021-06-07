using Hospital.Model;
using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class FeedbackFileRepository : IFeedbackRepository
    {
        private string fileName = "feedback.json";

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public List<Feedback> GetAll()
        {
            List<Feedback> feedbacks;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                feedbacks = (List<Feedback>)serializer.Deserialize(file, typeof(List<Feedback>));
            }

            if (feedbacks == null)
                feedbacks = new List<Feedback>();

            return feedbacks;
        }

        public Feedback GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(Feedback feedback)
        {
            List<Feedback> feedbacks = GetAll();
            feedbacks.Add(feedback);
            Serialize(feedbacks);
        }

        public void Serialize(List<Feedback> feedbacks)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, feedbacks);
            }
        }
    }
}
