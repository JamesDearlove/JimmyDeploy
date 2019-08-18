using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using WindowsAutoSetup.Data;

namespace WindowsAutoSetup
{
    public class Setup
    {
        public void joinDomain()
        {
            DomainInfo info = Config.get().domainInfo();


        }
    }
}
