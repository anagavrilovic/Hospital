﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IGenericRepository<T>
        where T : class
    {
        ObservableCollection<T> GetAll();
        void Save(T parameter);
        void Delete(string id);
        T GetByID(string id);
        void Serialize(ObservableCollection<T> parameter);

    }
}