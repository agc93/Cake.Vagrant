using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    /// Additional settings for the <c>vagrant ssh</c> command
    /// </summary>
    public class VagrantSSHSettings : IVagrantCommandSettings
    {
        /// <summary>
        /// Gets the command arguments corresponding to the specified <see cref="IVagrantCommandSettings"/>
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (!Command.HasValue())
                {
                    throw new ArgumentOutOfRangeException(nameof(Command), "You must specify an argument when invoking the SSH command or script execution will block");
                }
                args.Add(Command.Quote(), "command");
                //if (DoNotAuthenticate.HasValue && DoNotAuthenticate.Value) args.Append("--plain");
                if (ExtraSSHArguments.HasValue())
                {
                    args.Append("--");
                    args.Append(ExtraSSHArguments);
                }
            };
        }

        /// <summary>
        /// The command to run on your machine.
        /// </summary>
        /// <remarks>This is mandatory! Without it, script execution will block!</remarks>
        /// <value>This value will be quoted when invoked.</value>
        public string Command { get; set; }

        /// <summary>
        /// OBSOLETE: Setting this will cause unexpected results and may block scripts.
        /// </summary>
        [Obsolete("this will block script execution")]
        public bool? DoNotAuthenticate { get; set; }

        /// <summary>
        /// Extra arguments to provide directly to the SSH command
        /// </summary>
        public string ExtraSSHArguments { get; set; }
    }

    /// <summary>
    /// Fluent extension methods to the <see cref="VagrantSSHSettings"/> class
    /// </summary>
    public static class VagrantSSHSettingsExtensions
    {
        /// <summary>
        /// Sets the command to run in the SSH session
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="command">REQUIRED: The command to run in the session.</param>
        /// <returns></returns>
        public static VagrantSSHSettings RunCommand(this VagrantSSHSettings settings, string command)
        {
            settings.Command = command;
            return settings;
        }

        /// <summary>
        /// Sets any additional arguments to pass directly to the SSH invocation
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="args">Arbitrary arguments to pass to ssh</param>
        /// <returns></returns>
        public static VagrantSSHSettings WithArguments(this VagrantSSHSettings settings, params string[] args)
        {
            settings.ExtraSSHArguments = string.Join(" ", args);
            return settings;
        }
    }
}