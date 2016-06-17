using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    ///     Additional settings for the <c>vagrant snapshot restore</c> command.
    /// </summary>
    public class VagrantSnapshotRestoreSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="VagrantSnapshotRestoreSettings" /> class
        /// </summary>
        /// <param name="enableNoDeleteOption">Whether to include the "--no-delete" argument as a valid argument</param>
        public VagrantSnapshotRestoreSettings(bool enableNoDeleteOption = false)
        {
            EnableNoDelete = enableNoDeleteOption;
        }

        private bool EnableNoDelete { get; }

        /// <summary>
        ///     Force the provisioners to run (or prevent them from doing so).
        /// </summary>
        public bool? RunProvisioners { get; set; }

        /// <summary>
        ///     Prevents deletion of the snapshot after restoring (so that you can restore to the same point again later).
        /// </summary>
        /// <remarks>Only supported by some commands</remarks>
        public bool? DoNotDelete { get; set; }

        /// <summary>
        ///     Gets the command arguments corresponding to the specified <see cref="IVagrantCommandSettings" />
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(RunProvisioners, "provision", true);
                if (EnableNoDelete) args.Add(DoNotDelete, "no-delete");
            };
        }
    }

    /// <summary>
    ///     Fluent extension methods for the <see cref="VagrantSnapshotRestoreSettings" /> class
    /// </summary>
    public static class VagrantSnapshotRestoreSettingsExtensions
    {
        /// <summary>
        ///     Force the provisioners to run (or prevent them from doing so).
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="run">Whether to force provisioners to run. Defaults to <c>true</c></param>
        /// <returns>The updated settings object</returns>
        public static VagrantSnapshotRestoreSettings RunProvisioners(this VagrantSnapshotRestoreSettings settings,
            bool run = true)
        {
            settings.RunProvisioners = run;
            return settings;
        }

        /// <summary>
        ///     Prevents deletion of the snapshot after restoring (so that you can restore to the same point again later).
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>The updated settings object</returns>
        public static VagrantSnapshotRestoreSettings DoNotDelete(this VagrantSnapshotRestoreSettings settings)
        {
            settings.DoNotDelete = true;
            return settings;
        }

        /// <summary>
        ///     Forces deletion of the snapshot after restoring
        /// </summary>
        /// <remarks>Opposite of <see cref="DoNotDelete" />, included for more logical invocation</remarks>
        /// <param name="settings">The settings</param>
        /// <param name="delete">Whether to delete the snapshot. Defaults to <c>true</c>.</param>
        /// <returns></returns>
        public static VagrantSnapshotRestoreSettings Delete(this VagrantSnapshotRestoreSettings settings,
            bool delete = true)
        {
            settings.DoNotDelete = !delete;
            return settings;
        }
    }
}