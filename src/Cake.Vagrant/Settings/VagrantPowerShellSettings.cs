using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    /// Additional settings for the <c>vagrant powershell</c> command.
    /// </summary>
    public class VagrantPowerShellSettings : IVagrantCommandSettings
    {
        /// <summary>
        /// Gets the command arguments corresponding to the specified settings
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (!Command.HasValue())
                {
                    throw new ArgumentOutOfRangeException(nameof(Command),
                        "You must specify a command when invoking the PowerShell command or script execution will block!");
                }
                args.Add(Command.Quote(), "command");
            };
        }

        /// <summary>
        /// The command to run on your machine.
        /// </summary>
        /// <remarks>This is mandatory! Without it, script execution will block!</remarks>
        /// <value>This value will be quoted when invoked.</value>
        public string Command { get; set; }
    }

    /// <summary>
    /// Fluent extensions methods to the <see cref="VagrantPowerShellSettings"/> class
    /// </summary>
    public static class VagrantPowerShellSettingsExtensions
    {
        /// <summary>
        /// Sets the command to run in the PowerShell session
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="command">REQUIRED: The command to run in the session.</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPowerShellSettings RunCommand(this VagrantPowerShellSettings settings, string command)
        {
            settings.Command = command;
            return settings;
        }
    }
}