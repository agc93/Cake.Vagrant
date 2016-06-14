using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantProvisionerSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (RunProvisioners) args.Append("--provision");
                if (Provisioners.Any())
                {
                    args.AppendSwitch("--provision-with", string.Join(",", Provisioners));
                }
            };
        }

        public bool RunProvisioners { get; set; }
        public IEnumerable<string> Provisioners { get; set; } = new string[] { };
    }
}
