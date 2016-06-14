using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantUpSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(DestroyOnError, "destroy-on-error", true);
                args.Add(InstallProvider, "install-provider", true);
                args.Add(Parallel, "parallel", true);
                args.Add(Provider, "provider");
                args.Add(Provision, "provision");
                if (Provisioners.Any())
                {
                    args.AppendSwitch("--provision-with", string.Join(",", Provisioners));
                }
            };
        }

        public bool? DestroyOnError { get; set; }
        public bool? InstallProvider { get; set; }
        public bool? Parallel { get; set; }
        public string Provider { get; set; }
        public bool? Provision { get; set; }
        public IEnumerable<string> Provisioners { get; set; } = new string[] {};
    }

    public static class VagrantUpSettingsExtensions
    {
        public static VagrantUpSettings UseProvider(this VagrantUpSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        public static VagrantUpSettings RunProvisioners(this VagrantUpSettings settings,
            bool run = true)
        {
            settings.Provision = run;
            return settings;
        }

        public static VagrantUpSettings WithProvisioners(this VagrantUpSettings settings,
            IEnumerable<string> provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        public static VagrantUpSettings WithProvisioners(this VagrantUpSettings settings,
            params string[] provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        private static List<string> AddProvisioners(VagrantUpSettings settings, IEnumerable<string> provisioners)
        {
            var l = settings.Provisioners.ToList();
            l.AddRange(provisioners);
            settings.Provision = l.Any();
            return l;
        }

        public static VagrantUpSettings EnableParallel(this VagrantUpSettings settings)
        {
            settings.Parallel = true;
            return settings;
        }

        public static VagrantUpSettings InstallProvider(this VagrantUpSettings settings, bool install = true)
        {
            settings.InstallProvider = install;
            return settings;
        }

        public static VagrantUpSettings DestroyOnError(this VagrantUpSettings settings, bool destroy = true)
        {
            settings.DestroyOnError = destroy;
            return settings;
        }


    }
}