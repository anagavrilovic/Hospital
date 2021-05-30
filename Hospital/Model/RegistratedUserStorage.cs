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


        public UserType GetRoleByUsername(string username)
        {
            ObservableCollection<RegistratedUser> registratedUsers = GetAll();

            foreach(RegistratedUser user in registratedUsers)
            {
                if (user.Username.Equals(username))
                    return user.Type;
            }

            return UserType.doctor;
        }


    }
}
