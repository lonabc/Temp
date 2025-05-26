using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.Model
{
    public  class RegisterModel
    {
        private string _id;
        private string _name;
        private string _password;
        private string _email;
        private string _verificationCode;
        public string verificationCode
        {
            get { return _verificationCode; }
            set { _verificationCode = value; }
        }
        public string name
        {
            get { return _name; }
            set { _name = value; }
        }
        public string id
        {
            get { return _id; }
            set { _id = value; }
        }

        public string password
        {
            get { return _password; }
            set { _password = value; }
        }

        public string email
        {
            get { return _email; }
            set { _email = value; }
        }
    }
}
