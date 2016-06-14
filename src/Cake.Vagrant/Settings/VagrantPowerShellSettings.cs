using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
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

    public static class VagrantPowerShellSettingsExtensions
    {
        public static VagrantPowerShellSettings RunCommand(this VagrantPowerShellSettings settings, string command)
        {
            settings.Command = command;
            return settings;
        }
    }
}