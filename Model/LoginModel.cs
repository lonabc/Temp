using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.Model
{
    class LoginModel
    {
        
        private string _name;
        private string _password;

        public string name
        {
            get { return _name; }
            set { _name = value;  }
        }

        public string password
        {
            get { return _password; }
            set { _password = value;  }
        }
    }
}
