using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using JimmyDeploy.Data;

namespace JimmyDeploy
{
    public class Setup
    {
        public void joinDomain()
        {
            DomainInfo info = Config.get().getDomainInfo();
        }

        public void enableSignin()
        {

        }

        public void disableSignin()
        {

        }

        public void enableAutoLogin()
        {

        }

        public void disableAutoLogin()
        {

        }
    }
}
