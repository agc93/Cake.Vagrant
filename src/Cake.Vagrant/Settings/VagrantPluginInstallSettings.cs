using System;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    ///     Additional settings for the <c>vagrant plugin install</c> command.
    /// </summary>
    public class VagrantPluginInstallSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     If the plugin you are installing has another entrypoint, this flag can be used to specify it.
        /// </summary>
        public string EntryPoint { get; set; }

        /// <summary>
        ///     Clears all sources that have been defined so far.
        /// </summary>
        /// <remarks>This is an advanced feature.</remarks>
        public bool? CleanSources { get; set; }

        /// <summary>
        ///     Adds a source from which to fetch a plugin.
        /// </summary>
        /// <remarks>
        ///     Note that this does not only affect the single plugin being installed, by all future plugin as well. This is a
        ///     limitation of the underlying plugin installer Vagrant uses.
        /// </remarks>
        public string Source { get; set; }

        /// <summary>
        ///     The version of the plugin to install. By default, this command will install the latest version.
        /// </summary>
        /// <remarks>
        ///     <para>
        ///         You can set it to a specific version, such as "1.2.3" or you can set it to a version constraint, such as "
        ///         &gt; 1.0.2". You can set it to a more complex constraint by comma-separating multiple constraints: "&gt; 1.0.2,
        ///         &lt; 1.1.0"
        ///     </para>
        /// </remarks>
        /// <value>This will be automatically quoted</value>
        public string Version { get; set; }

        /// <summary>
        ///     Gets the command arguments corresponding to the specified <see cref="IVagrantCommandSettings" />
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(CleanSources, "plugin-clean-sources");
                args.Add(EntryPoint, "entry-point");
                args.Add(Source, "plugin-source");
                if (Version.HasValue()) args.AppendSwitch("--plugin-version", Version.Quote());
            };
        }
    }

    /// <summary>
    ///     Fluent extension methods for the <see cref="VagrantPluginInstallSettings" /> class
    /// </summary>
    public static class VagrantPluginInstallSettingsExtensions
    {
        /// <summary>
        ///     Sets the <see cref="VagrantPluginInstallSettings.EntryPoint" /> for a plugin.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="entryPoint">The entry point for the plugin</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPluginInstallSettings SetEntryPoint(this VagrantPluginInstallSettings settings,
            string entryPoint)
        {
            settings.EntryPoint = entryPoint;
            return settings;
        }

        /// <summary>
        ///     Clears all sources that have been defined so far.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="clean">Whether to clean sources first.</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPluginInstallSettings CleanFirst(this VagrantPluginInstallSettings settings,
            bool clean = true)
        {
            settings.CleanSources = clean;
            return settings;
        }

        /// <summary>
        ///     Adds a source from which to fetch a plugin.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="source">The source from which to install</param>
        /// <returns>The updated settings object</returns>
        public static VagrantPluginInstallSettings FromSource(this VagrantPluginInstallSettings settings, string source)
        {
            settings.Source = source;
            return settings;
        }

        /// <summary>
        ///     The version of the plugin to install.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="version">
        ///     The version or version constraint to use (as per
        ///     <see cref="VagrantPluginInstallSettings.Version" />)
        /// </param>
        /// <returns>The updated settings object</returns>
        public static VagrantPluginInstallSettings ConstrainVersion(this VagrantPluginInstallSettings settings,
            string version)
        {
            settings.Version = version;
            return settings;
        }
    }
}