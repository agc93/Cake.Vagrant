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

    public static class VagrantPluginInstallSettingsExtensions
    {
        public static VagrantPluginInstallSettings SetEntryPoint(this VagrantPluginInstallSettings settings, string entryPoint)
        {
            settings.EntryPoint = entryPoint;
            return settings;
        }

        public static VagrantPluginInstallSettings CleanFirst(this VagrantPluginInstallSettings settings,
            bool clean = true)
        {
            settings.CleanSources = clean;
            return settings;
        }

        public static VagrantPluginInstallSettings FromSource(this VagrantPluginInstallSettings settings, string source)
        {
            settings.Source = source;
            return settings;
        }

        public static VagrantPluginInstallSettings ConstrainVersion(this VagrantPluginInstallSettings settings,
            string version)
        {
            settings.Version = version;
            return settings;
        }
    }
}
