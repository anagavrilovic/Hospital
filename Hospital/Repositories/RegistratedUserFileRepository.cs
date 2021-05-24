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
            List<RegistratedUser> users = GetAll();
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

        public UserType GetRoleByUsername(string username)
        {
            List<RegistratedUser> registratedUsers = GetAll();

            foreach (RegistratedUser user in registratedUsers)
                if (user.Username.Equals(username))
                    return user.Type;

            return UserType.doctor;
        }

        public List<RegistratedUser> GetAll()
        {
            List<RegistratedUser> users;
            using (StreamReader file = File.OpenText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                users = (List<RegistratedUser>)serializer.Deserialize(file, typeof(List<RegistratedUser>));
            }

            if (users == null)
                users = new List<RegistratedUser>();

            return users;
        }

        public RegistratedUser GetByID(string username)
        {
            List<RegistratedUser> registratedUsers = GetAll();

            foreach (RegistratedUser user in registratedUsers)
                if (user.Username.Equals(username))
                    return user;

            return null;
        }

        public bool IsUsernameUnique(string username)
        {
            List<RegistratedUser> registratedUsers = GetAll();
            foreach (RegistratedUser user in registratedUsers)
                if (user.Username.Equals(username))
                    return false;

            return true;
        }

        public void Save(RegistratedUser user)
        {
            List<RegistratedUser> users = GetAll();
            users.Add(user);
            Serialize(users);
        }

        public void Serialize(List<RegistratedUser> users)
        {
            using (StreamWriter file = File.CreateText(@"..\\..\\Files\\" + fileName))
            {
                JsonSerializer serializer = new JsonSerializer();
                serializer.Serialize(file, users);
            }
        }

        public int CountByRole(UserType userType)
        {
            List<RegistratedUser> registratedUsers = GetAll();
            int count = 0;

            foreach (RegistratedUser user in registratedUsers)
                if (user.Type.Equals(userType))
                    count += 1;

            return count;
        }

    }
}
