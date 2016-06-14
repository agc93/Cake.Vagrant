using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantBoxRemoveSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(RemoveAll, "all");
                args.Add(Force, "force");
                args.Add(BoxVersion, "box-version");
                args.Add(Provider, "provider");
            };
        }

        public bool? RemoveAll { get; set; }
        public bool? Force { get; set; }
        public string BoxVersion { get; set; }
        public string Provider { get; set; }
    }

    public static class VagrantBoxRemoveSettingsExtensions
    {
        public static VagrantBoxRemoveSettings ConstrainVersion(this VagrantBoxRemoveSettings settings, string version)
        {
            settings.BoxVersion = version;
            return settings;
        }

        public static VagrantBoxRemoveSettings RemoveAll(this VagrantBoxRemoveSettings settings)
        {
            settings.RemoveAll = true;
            return settings;
        }

        public static VagrantBoxRemoveSettings UseProvider(this VagrantBoxRemoveSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        public static VagrantBoxRemoveSettings Force(this VagrantBoxRemoveSettings settings, bool force = true)
        {
            settings.Force = force;
            return settings;
        }
    }
}