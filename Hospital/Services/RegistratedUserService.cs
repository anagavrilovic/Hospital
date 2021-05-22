using Hospital.Model;
using Hospital.Repositories;
using Hospital.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Services
{
    public class RegistratedUserService
    {
        private IRegistratedUsersRepository registratedUsersRepository;

        public RegistratedUserService()
        {
            registratedUsersRepository = new RegistratedUserFileRepository();
        }

        public bool CreateAccount(MedicalRecord newRecord)
        {
            RegistratedUser registratedUser = new RegistratedUser { Username = newRecord.Patient.Username, Password = newRecord.Patient.Password, Type = UserType.patient };
            if (registratedUsersRepository.IsUsernameUnique(registratedUser.Username))
            {
                registratedUsersRepository.Save(registratedUser);
                return true;
            }

            return false;
        }

        public void DeleteAccount(string username)
        {
            registratedUsersRepository.Delete(username);
        }

        public int CountByRole(UserType userType)
        {
            return registratedUsersRepository.CountByRole(userType);
        }
    }
}
