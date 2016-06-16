using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    /// Additional settings for the <c>vagrant box remove</c> command
    /// </summary>
    public class VagrantBoxRemoveSettings : IVagrantCommandSettings
    {
        /// <summary>
        /// Gets the command arguments corresponding to the specified settings
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(RemoveAll, "all");
                args.Add(Force, "force");
                args.Add(BoxVersion, "box-version");
                args.Add(Provider, "provider");
            };
        }

        /// <summary>
        /// Remove all available versions of a box.
        /// </summary>
        public bool? RemoveAll { get; set; }
        /// <summary>
        /// Forces removing the box even if an active Vagrant environment is using it.
        /// </summary>
        public bool? Force { get; set; }
        /// <summary>
        ///  Version of version constraints of the boxes to remove. See <see cref="VagrantBoxAddSettings.Version"/> for more details.
        /// </summary>
        public string BoxVersion { get; set; }
        /// <summary>
        /// The provider-specific box to remove with the given name.
        /// </summary>
        /// <remarks>
        /// This is only required if a box is backed by multiple providers. If there is only a single provider, Vagrant will default to removing it.
        /// </remarks>
        public string Provider { get; set; }
    }

    /// <summary>
    /// Fluent extension methods for the <see cref="VagrantBoxRemoveSettings"/> class
    /// </summary>
    public static class VagrantBoxRemoveSettingsExtensions
    {
        /// <summary>
        /// Sets the version constraint for the box to remove
        /// </summary>
        /// <remarks>See <see cref="VagrantBoxAddSettings.Version"/> for more information</remarks>
        /// <param name="settings">The settings</param>
        /// <param name="version">Version constraint to match against</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxRemoveSettings ConstrainVersion(this VagrantBoxRemoveSettings settings, string version)
        {
            settings.BoxVersion = version;
            return settings;
        }

        /// <summary>
        /// Remove all available versions of a box.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxRemoveSettings RemoveAll(this VagrantBoxRemoveSettings settings)
        {
            settings.RemoveAll = true;
            return settings;
        }

        /// <summary>
        /// The provider-specific box to remove with the given name.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provider">The specific provider to remove</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxRemoveSettings UseProvider(this VagrantBoxRemoveSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        /// <summary>
        /// Forces removing the box even if an active Vagrant environment is using it.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="force"><c>true</c> to force removal. Defaults to <c>true</c></param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxRemoveSettings Force(this VagrantBoxRemoveSettings settings, bool force = true)
        {
            settings.Force = force;
            return settings;
        }
    }
}