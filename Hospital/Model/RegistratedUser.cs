using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hospital.Model
{
    public class RegistratedUser
    {
        private String username;
        private String password;
        private UserType type;

        public String Username
        {
            get => username;
            set
            {
                username = value;
            }
        }

        public String Password
        {
            get => password;
            set
            {
                password = value;
            }
        }
        public UserType Type
        {
            get => type;
            set
            {
                type = value;
            }
        }
    }
}
