using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantBoxUpdateSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (!BoxName.HasValue()) return;
                args.Add(BoxName, "box");
                args.Add(Provider, "provider");
            };
        }


        public string BoxName { get; set; }
        public string Provider { get; set; }
    }

    public static class VagrantBoxUpdateSettingsExtensions
    {
        public static VagrantBoxUpdateSettings UseProvider(this VagrantBoxUpdateSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        public static VagrantBoxUpdateSettings OnlyUpdate(this VagrantBoxUpdateSettings settings, string name)
        {
            settings.BoxName = name;
            return settings;
        }
    }
}