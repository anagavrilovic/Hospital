using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class Context
    {
        private Strategy strategy;

        public void SetStrategy(Strategy strategy)
        {
            this.strategy = strategy;
        }

        public void Update()
        {
            strategy.Update();
        }

        public void Delete()
        {
            strategy.Delete();
        }

        public List<IPatientNotification> GetNotifications()
        {
            return strategy.GetNotifications();
        }
    }
}
