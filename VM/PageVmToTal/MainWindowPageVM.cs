using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WpfAppLogin.VM.PageVmToTal
{
    public class MainWindowPageVM
    {
        private LoginVm _loginVm;
        private RegisterVM _registerVm;
        public MainWindowPageVM(LoginVm loginVm, RegisterVM registerVm)
        {
            _loginVm = loginVm;
            _registerVm = registerVm;
        }

        public LoginVm LoginVm
        {
            get { return _loginVm; }
            set { _loginVm = value; }
        }
        public RegisterVM RegisterVm
        {
            get { return _registerVm; }
            set { _registerVm = value; }
        }

    }
}
