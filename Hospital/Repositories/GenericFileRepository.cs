using Hospital.Repositories.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories
{
    public class GenericFileRepository<T> : IGenericRepository<T>
        where T : class
    {
        private string fileName;

        public GenericFileRepository(string fileName)
        {
            this.fileName = fileName;
        }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<T> GetAll()
        {
            ObservableCollection<T> entitites;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                entitites = (ObservableCollection<T>)serializer.Deserialize(file, typeof(ObservableCollection<T>));
            }

            if (entitites == null)
                entitites = new ObservableCollection<T>();

            return entitites;
        }

        public T GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(T parameter)
        {
            ObservableCollection<T> entitites = GetAll();
            entitites.Add(parameter);
            Serialize(entitites);
        }

        public void Serialize(ObservableCollection<T> parameter)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, parameter);
            }
        }
    }
}
