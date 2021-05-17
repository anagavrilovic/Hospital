using Hospital.Model;
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
    public class RegistratedUserFileRepository : IFileRepository<RegistratedUser>
    {
        private string fileName = "registratedUsers.json";

        public RegistratedUserFileRepository() { }

        public void Delete(string id)
        {
            throw new NotImplementedException();
        }

        public ObservableCollection<RegistratedUser> GetAll()
        {
            ObservableCollection<RegistratedUser> users;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                users = (ObservableCollection<RegistratedUser>)serializer.Deserialize(file, typeof(ObservableCollection<RegistratedUser>));
            }

            if (users == null)
                users = new ObservableCollection<RegistratedUser>();

            return users;
        }

        public RegistratedUser GetByID(string id)
        {
            throw new NotImplementedException();
        }

        public void Save(RegistratedUser user)
        {
            ObservableCollection<RegistratedUser> users = GetAll();
            users.Add(user);
            Serialize(users);
        }

        public void Serialize(ObservableCollection<RegistratedUser> users)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, users);
            }
        }
    }
}
