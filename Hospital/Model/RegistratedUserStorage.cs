using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    class RegistratedUserStorage
    {

        private String fileName;

        public RegistratedUserStorage(String file = "registratedUsers.json")
        {
            this.fileName = file;
        }

        public ObservableCollection<RegistratedUser> GetAll()
        {
            ObservableCollection<RegistratedUser> users;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                users = (ObservableCollection<RegistratedUser>)serializer.Deserialize(file, typeof(ObservableCollection<RegistratedUser>));
            }

            return users;
        }

        public void Save(RegistratedUser user)
        {
            ObservableCollection<RegistratedUser> users = GetAll();
            users.Add(user);
            DoSerialization(users);
        }

        public void DoSerialization(ObservableCollection<RegistratedUser> users)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, users);
            }
        }

    }
}
