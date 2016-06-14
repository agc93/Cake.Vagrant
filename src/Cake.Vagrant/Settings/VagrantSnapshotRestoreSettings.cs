using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantSnapshotRestoreSettings : IVagrantCommandSettings
    {
        public VagrantSnapshotRestoreSettings(bool enableNoDeleteOption = false)
        {
            EnableNoDelete = enableNoDeleteOption;
        }

        private bool EnableNoDelete { get; set; }

        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(RunProvisioners, "provision", enableNegation: true);
                if (EnableNoDelete) args.Add(DoNotDelete, "no-delete");
            };
        }

        public bool? RunProvisioners { get; set; }
        public bool? DoNotDelete { get; set; }
    }
}
