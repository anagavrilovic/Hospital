﻿using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Factory
{
   public interface IMedicineRepositoryFactory
    {
        IMedicineRepository CreateMedicineRepository();
    }
}
