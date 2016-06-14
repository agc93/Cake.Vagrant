using System;
using Cake.Core.Diagnostics;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Commands
{
    public class VagrantDockerRunner : VagrantCommandRunner
    {
        public VagrantDockerRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

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

        public void Run(string name, string command)
        {
            Exec(name, command);
        }
    }
}
