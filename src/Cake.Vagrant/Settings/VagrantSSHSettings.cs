using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    public class VagrantSSHSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(Command, "command");
                if (DoNotAuthenticate.HasValue && DoNotAuthenticate.Value) args.Append("--plain");
                if (ExtraSSHArguments.HasValue())
                {
                    args.Append("--");
                    args.Append(ExtraSSHArguments);
                }
            };
        }

        public string Command { get; set; }
        public bool? DoNotAuthenticate { get; set; }

        public string ExtraSSHArguments { get; set; }
    }
}