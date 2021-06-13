﻿using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Factory
{
    public class PatientNotesFileFactory : IPatientNotesRepositoryFactory
    {

        public IPatientNotesRepository CreatePatientNotesRepository()
        {
            return new PatientNotesFileRepository();
        }
    }
}
