using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class Feedback
    {
        private string userId;
        public string UserId
        {
            get
            {
                return userId;
            }
            set
            {
                userId = value;
            }
        }
        private string comment;
        public string Comment
        {
            get
            {
                return comment;
            }
            set
            {
                comment = value;
            }
        }
        private FeedbackType feedbackType;
        public FeedbackType FeedbackType
        {
            get
            {
                return feedbackType;
            }
            set
            {
                feedbackType = value;
            }
        }
    }
}
