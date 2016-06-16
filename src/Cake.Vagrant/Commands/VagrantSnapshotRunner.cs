using System;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant.Commands
{
    /// <summary>
    /// Wrapper around the <c>vagrant snapshot</c> subcommands
    /// </summary>
    public class VagrantSnapshotRunner : VagrantCommandRunner
    {
        /// <summary>
        /// Creates a new instance of the <see cref="VagrantSnapshotRunner"/> class
        /// </summary>
        /// <param name="log">Logging output</param>
        /// <param name="runCallback">Action to trigger invocation of the CLI</param>
        /// <param name="settings">Settings for invocation of the Vagrant CLI</param>
        public VagrantSnapshotRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        /// <summary>
        /// This takes a snapshot and pushes it onto the snapshot stack.
        /// </summary>
        /// <remarks>
        /// <para>This is a shorthand for <see cref="Save"/> where you do not need to specify a name.</para>
        /// <para>Warning: If you are using <see cref="Push"/> and <see cref="Pop"/>, avoid using <see cref="Save"/> and <see cref="Restore"/> which are unsafe to mix.</para>
        /// </remarks>
        public void Push()
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "push");
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        /// This command is the inverse of <see cref="Push"/>: it will restore the pushed state.
        /// </summary>
        /// <param name="configure">Optional settings to control the restore</param>
        public void Pop(Action<VagrantSnapshotRestoreSettings> configure = null)
        {
            var settings = new VagrantSnapshotRestoreSettings(true);
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "pop");
            settings.GetToolArguments().Invoke(args);
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        /// This command saves a new named snapshot.
        /// </summary>
        /// <remarks>If this command is used, the <see cref="Push"/> and <see cref="Pop"/> subcommands cannot be safely used.</remarks>
        /// <param name="name">Name of the snapshot</param>
        public void Save(string name)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("save", name);
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        /// This command restores the named snapshot.
        /// </summary>
        /// <param name="name">Name of the snapshot to restore</param>
        /// <param name="configure">Optional settings to control the restore</param>
        public void Restore(string name, Action<VagrantSnapshotRestoreSettings> configure = null)
        {
            var settings = new VagrantSnapshotRestoreSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "restore", name ?? string.Empty);
            settings.GetToolArguments().Invoke(args);
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        /// This command will delete the named snapshot.
        /// </summary>
        /// <param name="name">Name of the snapshot to delete</param>
        public void Delete(string name)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("snapshot", "delete", name);
            Runner.Invoke(Settings, args);
        }
    }
}