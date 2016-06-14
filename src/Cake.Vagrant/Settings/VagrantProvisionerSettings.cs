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

    public static class VagrantProvisionerSettingsExtensions
    {
        public static VagrantProvisionerSettings RunProvisioners(this VagrantProvisionerSettings settings,
            bool run = true)
        {
            settings.RunProvisioners = run;
            return settings;
        }

        public static VagrantProvisionerSettings WithProvisioners(this VagrantProvisionerSettings settings,
            IEnumerable<string> provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        public static VagrantProvisionerSettings WithProvisioners(this VagrantProvisionerSettings settings,
            params string[] provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        private static List<string> AddProvisioners(VagrantProvisionerSettings settings, IEnumerable<string> provisioners)
        {
            var l = settings.Provisioners.ToList();
            l.AddRange(provisioners);
            settings.RunProvisioners = l.Any();
            return l;
        }
    }
}
