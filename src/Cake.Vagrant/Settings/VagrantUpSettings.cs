using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    ///     Additional settings for the <c>vagrant up</c> command
    /// </summary>
    public class VagrantUpSettings : IVagrantCommandSettings
    {
        /// <summary>
        ///     Destroy the newly created machine if a fatal, unexpected error occurs.
        ///     This will only happen on the first vagrant up.
        ///     By default this is set.
        /// </summary>
        /// <value><c>true</c> to destroy the machine. Defaults to <c>true</c></value>
        public bool? DestroyOnError { get; set; }

        /// <summary>
        ///     If the requested provider is not installed, Vagrant will attempt to automatically install it if it can. By default
        ///     this is enabled.
        /// </summary>
        public bool? InstallProvider { get; set; }

        /// <summary>
        ///     Bring multiple machines up in parallel if the provider supports it. Please consult the provider documentation to
        ///     see if this feature is supported.
        /// </summary>
        public bool? Parallel { get; set; }

        /// <summary>
        ///     Bring the machine up with the given provider. By default this is "virtualbox".
        /// </summary>
        public string Provider { get; set; }

        /// <summary>
        ///     Force the provisioners to run.
        /// </summary>
        public bool? RunProvisioners { get; set; }

        /// <summary>
        ///     This will only run the given provisioners.
        /// </summary>
        /// <value>Providers to run. Omit the leading `:`</value>
        public IEnumerable<string> Provisioners { get; set; } = new string[] {};

        /// <summary>
        /// Gets the command arguments corresponding to the specified <see cref="IVagrantCommandSettings"/>
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                args.Add(DestroyOnError, "destroy-on-error", true);
                args.Add(InstallProvider, "install-provider", true);
                args.Add(Parallel, "parallel", true);
                args.Add(Provider, "provider");
                args.Add(RunProvisioners, "provision");
                if (Provisioners.Any())
                {
                    args.AppendSwitch("--provision-with", string.Join(",", Provisioners));
                }
            };
        }
    }

    /// <summary>
    ///     Fluent extension methods to the <see cref="VagrantUpSettings" /> class
    /// </summary>
    public static class VagrantUpSettingsExtensions
    {
        /// <summary>
        ///     Bring the machine up with the given provider. By default this is "virtualbox".
        /// </summary>
        /// <param name="settings">The setings</param>
        /// <param name="provider">Name of the provider to use (e.g. <c>"hyperv"</c>)</param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings UseProvider(this VagrantUpSettings settings, string provider)
        {
            settings.Provider = provider;
            return settings;
        }

        /// <summary>
        ///     Force the provisioners to run when bringing up the machine
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="run"><c>true</c> to run provisioners</param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings RunProvisioners(this VagrantUpSettings settings,
            bool run = true)
        {
            settings.RunProvisioners = run;
            return settings;
        }

        /// <summary>
        ///     This will only run the given provisioners when bringing up the machine
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provisioners">List of provisioner names to run</param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings WithProvisioners(this VagrantUpSettings settings,
            IEnumerable<string> provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        /// <summary>
        ///     This will only run the given provisioners when bringing up the machine
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provisioners">Collection of provisioner names to run</param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings WithProvisioners(this VagrantUpSettings settings,
            params string[] provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        private static List<string> AddProvisioners(VagrantUpSettings settings, IEnumerable<string> provisioners)
        {
            var l = settings.Provisioners.Select(p => p.Trim().Trim(':')).ToList();
            l.AddRange(provisioners);
            settings.RunProvisioners = l.Any();
            return l;
        }

        /// <summary>
        ///     Bring multiple machines up in parallel if the provider supports it.
        /// </summary>
        /// <remarks>Please consult the provider documentation to see if this feature is supported.</remarks>
        /// <param name="settings">The settings</param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings EnableParallel(this VagrantUpSettings settings)
        {
            settings.Parallel = true;
            return settings;
        }

        /// <summary>
        ///     Instructs Vagrant to attempt to install the chosen provider if it is not installed.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="install"><c>true</c> to enable automatic installation. By default this is enabled.</param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings InstallProvider(this VagrantUpSettings settings, bool install = true)
        {
            settings.InstallProvider = install;
            return settings;
        }

        /// <summary>
        ///     Destroy the newly created machine if a fatal, unexpected error occurs.
        ///     This will only happen on the first vagrant up.
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="destroy">><c>true</c> to destroy the machine. Defaults to <c>true</c></param>
        /// <returns>The updated settings object</returns>
        public static VagrantUpSettings DestroyOnError(this VagrantUpSettings settings, bool destroy = true)
        {
            settings.DestroyOnError = destroy;
            return settings;
        }
    }
}