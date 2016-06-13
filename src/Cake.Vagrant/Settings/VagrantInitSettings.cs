using System;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantInitSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {

                if (Force.HasValue) args.Append("--force");
                if (Minimal.HasValue) args.Append("--minimal");
                if (OutputFile.HasValue()) args.AppendSwitch("--output", OutputFile.Quote());
                if (VersionConstraint.HasValue()) args.AppendSwitch("--box-version", VersionConstraint.Quote());
            };
        }

        public bool? Force { get; set; }
        public bool? Minimal { get; set; }
        public string OutputFile { get; set; }
        public string VersionConstraint { get; set; }
    }
}
