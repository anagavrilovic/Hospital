﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
   public interface IPatientNotification
    {
        String ID { get; set; }
        String Name { get; set; }
        String Text { get; set; }
        String Times { get; set; }
        String Duration { get; set; }
        Boolean Active { get; set; }
        Boolean Read { get; set; }
    }
}
