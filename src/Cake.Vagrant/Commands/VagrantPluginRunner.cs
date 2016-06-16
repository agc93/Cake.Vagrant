using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant.Commands
{
    /// <summary>
    ///     Wrapper around the <c>vagrant plugin</c> subcommands
    /// </summary>
    public class VagrantPluginRunner : VagrantCommandRunner
    {
        /// <summary>
        ///     Creates a new instance of the <see cref="VagrantPluginRunner" /> class
        /// </summary>
        /// <param name="log">Logging output</param>
        /// <param name="runCallback">Action to trigger invocation of the CLI</param>
        /// <param name="settings">Settings for invocation of the Vagrant CLI</param>
        public VagrantPluginRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback,
            VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        /// <summary>
        ///     This installs a plugin with the given name or file path.
        /// </summary>
        /// <remarks>If the name is not a path to a file, then the plugin is installed from remote repositories, usually RubyGems.</remarks>
        /// <param name="name">Name or path of the plugin to install</param>
        /// <param name="configure">Optional settings to control the installation process</param>
        /// <example>
        ///     <code>
        /// <![CDATA[
        /// context.Vagrant().Plugin.Install("plugin", 
        ///     s => s.CleanFirst()
        ///             .FromSource("http://source")
        ///             .SetEntryPoint("main.sh"));
        /// ]]></code>
        /// </example>
        public void Install(string name, Action<VagrantPluginInstallSettings> configure = null)
        {
            Install(new[] {name}, configure);
        }

        /// <summary>
        ///     This installs multiple plugins at the same time with the given names or paths
        /// </summary>
        /// <param name="names">A list of names of plugins to install</param>
        /// <param name="configure">Optional settings to control the installation process</param>
        public void Install(IEnumerable<string> names, Action<VagrantPluginInstallSettings> configure = null)
        {
            var settings = new VagrantPluginInstallSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.AppendAll("plugin", "install");
            settings.GetToolArguments().Invoke(args);
            args.AppendAll(names.ToArray());
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        ///     This uninstalls the plugin with the given name. If multiple plugins are given, multiple plugins will be
        ///     uninstalled.
        /// </summary>
        /// <remarks>Any dependencies of the plugin will also be uninstalled assuming no other plugin needs them.</remarks>
        /// <param name="names">Names of the plugins to uninstall</param>
        public void Uninstall(params string[] names)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("plugin", "uninstall");
            args.AppendAll(names.ToArray());
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        ///     This updates the plugins that are installed within Vagrant.
        /// </summary>
        /// <remarks>
        ///     If you specified version constraints when installing the plugin, this command will respect those constraints.
        ///     If you want to change a version constraint, re-install the plugin using
        ///     <see cref="Install(string,System.Action{Cake.Vagrant.Settings.VagrantPluginInstallSettings})" />
        /// </remarks>
        /// <param name="name">If a name is specified, only that single plugin will be updated</param>
        public void Update(string name = null)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("plugin", "update");
            if (name.HasValue()) args.Append(name);
            Runner.Invoke(Settings, args);
        }
    }
}