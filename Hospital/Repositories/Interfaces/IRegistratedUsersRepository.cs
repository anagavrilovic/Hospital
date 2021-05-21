using Hospital.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Repositories.Interfaces
{
    interface IRegistratedUsersRepository : IGenericRepository<RegistratedUser>
    {
        bool IsUsernameUnique(string username);
    }
}
