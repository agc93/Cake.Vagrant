using System;
using Cake.Core.Diagnostics;
using Cake.Core;
using Cake.Core.IO;
using Cake.Vagrant.Settings;

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

    public class VagrantSnapshotRunner : VagrantCommandRunner
    {
        public VagrantSnapshotRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        public void Push()
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "push");
            Runner.Invoke(Settings, args);
        }

        public void Pop(Action<VagrantSnapshotRestoreSettings> configure = null)
        {
            var settings = new VagrantSnapshotRestoreSettings(true);
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "pop");
            settings.GetToolArguments().Invoke(args);
            Runner.Invoke(Settings, args);
        }

        public void Save(string name)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("save", name);
            Runner.Invoke(Settings, args);
        }

        public void Restore(string name, Action<VagrantSnapshotRestoreSettings> configure = null)
        {
            var settings = new VagrantSnapshotRestoreSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "restore", name ?? string.Empty);
            settings.GetToolArguments().Invoke(args);
            Runner.Invoke(Settings, args);
        }

        public void Delete(string name)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "delete", name);
        }
    }

    public class VagrantPluginRunner : VagrantCommandRunner
    {
        public VagrantPluginRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }
    }
}
