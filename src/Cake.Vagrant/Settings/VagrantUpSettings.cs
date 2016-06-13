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
}