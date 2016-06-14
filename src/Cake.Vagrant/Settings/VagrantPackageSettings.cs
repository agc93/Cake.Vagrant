using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantPackageSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(BaseImageName, "base");
                args.Add(OutputFile, "output");
                args.Add(VagrantFile, "vagrantfile");
                if (AdditionalFilePaths.Any())
                {
                    args.Add(string.Join(",", AdditionalFilePaths), "include");
                }
            };
        }

        public string BaseImageName { get; set; }
        public string OutputFile { get; set; }
        public IEnumerable<string> AdditionalFilePaths { get; set; } = new string[] {};
        public string VagrantFile { get; set; }
    }

    public static class VagrantPackageSettingsExtensions
    {
        public static VagrantPackageSettings UseBaseImage(this VagrantPackageSettings settings, string id)
        {
            settings.BaseImageName = id;
            return settings;
        }

        public static VagrantPackageSettings OutputToFile(this VagrantPackageSettings settings, FilePath path)
        {
            settings.OutputFile = path.FullPath;
            return settings;
        }

        public static VagrantPackageSettings IncludeInPackage(this VagrantPackageSettings settings,
            params FilePath[] files)
        {
            var paths = settings.AdditionalFilePaths.ToList();
            paths.AddRange(files.Select(f => f.FullPath));
            settings.AdditionalFilePaths = paths;
            return settings;
        }

        public static VagrantPackageSettings IncludeVagrantFile(this VagrantPackageSettings settings,
            FilePath vagrantFile)
        {
            settings.VagrantFile = vagrantFile.FullPath;
            return settings;
        }

    }
}
