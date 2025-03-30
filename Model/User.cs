using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.Model
{
    public class User
    {
        private string _name;
        private string _password;
        private string _Id;
        public string name {
            get { return _name; }
            set { _name = value; }
        }
        public string password
        {
            get { return _password; }
            set { _password = password; }
        }
        public string Id
        {
            get { return _Id; }
            set { _Id = Id; }

        }
    }
}
