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
            List<Feedback> feedbacks = GetAll();
            foreach (Feedback feedback in feedbacks)
            {
                if (feedback.ID.Equals(id))
                {
                    feedbacks.Remove(feedback);
                    Serialize(feedbacks);
                    return;
                }
            }
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
            List<Feedback> feedbacks = GetAll();
            foreach (Feedback feedback in feedbacks)
                if (feedback.ID.Equals(id))
                    return feedback;

            return null;
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

        public String GetNewID()
        {
            int newID = 0;
            while (true)
            {
                if (!CheckIfIDExists(newID.ToString())) return newID.ToString();
                newID++;
            }
        }

        private Boolean CheckIfIDExists(String ID)
        {
            List<Feedback> feedbacks = GetAll();
            foreach (Feedback feedback in feedbacks)
            {
                if (feedback.ID.Equals(ID)) return true;
            }
            return false;
        }
    }
}
