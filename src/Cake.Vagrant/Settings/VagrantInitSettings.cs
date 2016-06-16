using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    /// Additional settings for the <c>vagrant init</c> command
    /// </summary>
    public class VagrantInitSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     Gets or sets a value indicating whether to force initialization, even if an existing Vagrantfile is found
        /// </summary>
        /// <value>If <c>true</c>, this command will overwite any existing Vagrantfile</value>
        public bool? Force { get; set; }

        /// <summary>
        ///     This enables the creation of a more minimal Vagrantfile that does not contain the instructional comments the normal
        ///     Vagrantfile contains
        /// </summary>
        /// <value><c>true</c> to create a minimal file. Defaults to <c>false</c></value>
        public bool? Minimal { get; set; }

        /// <summary>
        ///     Gets or sets the file to output the Vagrantfile to
        /// </summary>
        /// <value>File path for the new Vagrantfile. If this is "-", the Vagrantfile will be sent to stdout.</value>
        public string OutputFile { get; set; }

        /// <summary>
        ///     The box version or box version constraint to add to the Vagrantfile
        /// </summary>
        public string VersionConstraint { get; set; }

        /// <summary>
        /// Gets the command arguments corresponding to the specified settings
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (Force.HasValue) args.Append("--force");
                if (Minimal.HasValue) args.Append("--minimal");
                if (OutputFile.HasValue()) args.AppendSwitch("--output", OutputFile.Quote());
                if (VersionConstraint.HasValue()) args.AppendSwitch("--box-version", VersionConstraint.Quote());
            };
        }
    }

    /// <summary>
    /// Fluent extension methods for the <see cref="VagrantInitSettings"/> class
    /// </summary>
    public static class VagrantInitSettingsExtensions
    {
        /// <summary>
        ///     Enables overwriting any existing Vagrantfile
        /// </summary>
        /// <param name="settings">Settings</param>
        /// <param name="force">Where to enable <c>--force</c></param>
        /// <returns>The updated settings object</returns>
        public static VagrantInitSettings Force(this VagrantInitSettings settings, bool force = true)
        {
            settings.Force = force;
            return settings;
        }

        /// <summary>
        ///     Enables creation of a more minimal Vagrantfile without instructional comments
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <returns>The updated settings object</returns>
        public static VagrantInitSettings CreateMinimal(this VagrantInitSettings settings)
        {
            settings.Minimal = true;
            return settings;
        }

        /// <summary>
        ///     Sets the <see cref="FilePath" /> to output the new Vagrantfile to
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="filePath">Desired path for the new Vagrantfile</param>
        /// <returns>The updated settings object</returns>
        public static VagrantInitSettings OutputToFile(this VagrantInitSettings settings, FilePath filePath)
        {
            settings.OutputFile = filePath.FullPath;
            return settings;
        }

        /// <summary>
        ///     Sets the box version or box version constraint to add to the Vagrantfile
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="version">Box version or version constraint</param>
        /// <returns>The updated settings object</returns>
        public static VagrantInitSettings ConstrainVersion(this VagrantInitSettings settings, string version)
        {
            settings.VersionConstraint = version;
            return settings;
        }
    }
}