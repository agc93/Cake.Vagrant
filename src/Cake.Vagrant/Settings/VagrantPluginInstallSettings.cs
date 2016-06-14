using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantPluginInstallSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(CleanSources, "plugin-clean-sources");
                args.Add(EntryPoint, "entry-point");
                args.Add(Source, "plugin-source");
                if (Version.HasValue()) args.AppendSwitch("--plugin-version", Version.Quote());
            };
        }

        public string EntryPoint { get; set; }
        public bool? CleanSources { get; set; }
        public string Source { get; set; }
        public string Version { get; set; }
    }
}
