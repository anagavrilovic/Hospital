using System;

namespace Hospital
{
   public class User : Person
   {
        public Boolean LogIn()
        {
            throw new NotImplementedException();
        }

        private String username;
        private String password;

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

    }
}