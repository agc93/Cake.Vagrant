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
}
