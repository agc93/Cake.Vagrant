using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Vagrant.Commands
{
    /// <summary>
    ///     Wrapper around the <c>vagrant docker</c> subcommands
    /// </summary>
    public class VagrantDockerRunner : VagrantCommandRunner
    {
        /// <summary>
        ///     Gets a new instance of the <see cref="VagrantDockerRunner" /> class
        /// </summary>
        /// <param name="log">Logging output</param>
        /// <param name="runCallback">Action to trigger invocation of the CLI</param>
        /// <param name="settings">Settings for invocation of the Vagrant CLI</param>
        public VagrantDockerRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback,
            VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        /// <summary>
        ///     Can be used to run one-off commands against a Docker container that is currently running
        /// </summary>
        /// <remarks>If the container is not running, an error will be returned.</remarks>
        /// <param name="name">Name of the container to run against</param>
        /// <param name="command">Command to execute. This will NOT be quoted.</param>
        public void Exec(string name, string command)
        {
            if (!name.HasValue()) throw new ArgumentNullException(nameof(name));
            if (!command.HasValue()) throw new ArgumentNullException(nameof(command));
            var args = new ProcessArgumentBuilder();
            args.Append("docker-exec");
            args.Append(name);
            args.Append("--");
            args.Append(command);
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        ///     Can be used to run one-off commands against a Docker container that is currently running
        /// </summary>
        /// <remarks>If the container is not running, an error will be returned.</remarks>
        /// <param name="name">Name of the container to run against</param>
        /// <param name="command">Command to execute. This will NOT be quoted.</param>
        public void Run(string name, string command)
        {
            Exec(name, command);
        }
    }
}