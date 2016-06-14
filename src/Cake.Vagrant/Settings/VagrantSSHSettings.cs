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
                if (!Command.HasValue())
                {
                    throw new ArgumentOutOfRangeException(nameof(Command), "You must specify an argument when invoking the SSH command or script execution will block");
                }
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

    public class VagrantPowerShellSettings : IVagrantCommandSettings
    {
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (!Command.HasValue())
                {
                    throw new ArgumentOutOfRangeException(nameof(Command),
                        "You must specify a command when invoking the PowerShell command or script execution will block!");
                }
                args.Add(Command, "command");
            };
        }

        public string Command { get; set; }
    }
}