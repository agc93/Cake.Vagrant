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
}