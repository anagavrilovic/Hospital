using Hospital.Model;
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
    public class RegistratedUserFileRepository : IRegistratedUsersRepository
    {
        private string fileName = "registratedUsers.json";

        public RegistratedUserFileRepository() { }

        public void Delete(string username)
        {
            ObservableCollection<RegistratedUser> users = GetAll();
            foreach (RegistratedUser user in users)
            {
                if (user.Username.Equals(username))
                {
                    users.Remove(user);
                    Serialize(users);
                    return;
                }
            }
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

        public RegistratedUser GetByID(string username)
        {
            ObservableCollection<RegistratedUser> registratedUsers = GetAll();

            foreach (RegistratedUser user in registratedUsers)
                if (user.Username.Equals(username))
                    return user;

            return null;
        }

        public bool IsUsernameUnique(string username)
        {
            ObservableCollection<RegistratedUser> registratedUsers = GetAll();
            foreach (RegistratedUser user in registratedUsers)
                if (user.Username.Equals(username))
                    return false;

            return true;
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
