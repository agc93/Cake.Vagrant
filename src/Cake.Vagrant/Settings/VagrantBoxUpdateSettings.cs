using System;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    ///     Additional settings for the <c>vagrant box update</c> command
    /// </summary>
    public class VagrantBoxUpdateSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     Name of a specific box to update.
        /// </summary>
        /// <remarks>If this flag is not specified, Vagrant will update the boxes for the active Vagrant environment.</remarks>
        public string BoxName { get; set; }

        /// <summary>
        ///     When <see cref="BoxName" /> is present, this controls what provider-specific box to update.
        /// </summary>
        /// <remarks>This is not required unless the box has multiple providers.</remarks>
        public string Provider { get; set; }

        /// <summary>
        ///     Gets the command arguments corresponding to the specified settings
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (!BoxName.HasValue()) return;
                args.Add(BoxName, "box");
                args.Add(Provider, "provider");
            };
        }
    }

    /// <summary>
    ///     Fluent extension methods for the <see cref="VagrantBoxUpdateSettings" /> class
    /// </summary>
    public static class VagrantBoxUpdateSettingsExtensions
    {
        /// <summary>
        ///     Controls what provider-specific box to update
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provider">The provider name to update</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxUpdateSettings UseProvider(this VagrantBoxUpdateSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        /// <summary>
        ///     Set the name of a specific box to update
        /// </summary>
        /// <remarks>When using this option, the box does not need to be part of the active environment</remarks>
        /// <param name="settings">The settings</param>
        /// <param name="name">Name of the box to update</param>
        /// <returns>The updated settings object</returns>
        public static VagrantBoxUpdateSettings OnlyUpdate(this VagrantBoxUpdateSettings settings, string name)
        {
            settings.BoxName = name;
            return settings;
        }
    }
}