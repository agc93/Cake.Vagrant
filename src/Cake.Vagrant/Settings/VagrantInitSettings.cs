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

    public static class VagrantInitSettingsExtensions
    {
        public static VagrantInitSettings Force(this VagrantInitSettings settings, bool force = true)
        {
            settings.Force = force;
            return settings;
        }

        public static VagrantInitSettings CreateMinimal(this VagrantInitSettings settings)
        {
            settings.Minimal = true;
            return settings;
        }

        public static VagrantInitSettings OutputToFile(this VagrantInitSettings settings, FilePath filePath)
        {
            settings.OutputFile = filePath.FullPath;
            return settings;
        }

        public static VagrantInitSettings ConstrainVersion(this VagrantInitSettings settings, string version)
        {
            settings.VersionConstraint = version;
            return settings;
        }
    }
}
