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
}