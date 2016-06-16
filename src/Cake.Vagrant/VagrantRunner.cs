using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Vagrant.Commands;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant
{
    /// <summary>
    /// Wrapper around Vagrant's CLI functionality for creating, starting and managing VMs
    /// </summary>
    public class VagrantRunner : Tool<VagrantSettings>
    {

        private VagrantSettings Settings { get; set; } = new VagrantSettings();
        /// <summary>
        /// Provides access to `vagrant box` subcommands
        /// </summary>
        public VagrantBoxRunner Box { get; private set; }
        /// <summary>
        /// Provides access to `vagrant snapshot` subcommands
        /// </summary>
        public VagrantSnapshotRunner Snapshot { get; private set; } 
        /// <summary>
        /// Provides access to `vagrant docker` subcommands
        /// </summary>
        public VagrantDockerRunner Docker { get; private set; }
        /// <summary>
        /// Provides access to `vagrant plugin` subcommands
        /// </summary>
        public VagrantPluginRunner Plugin { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="VagrantRunner"/> class
        /// </summary>
        /// <param name="fileSystem">The file system</param>
        /// <param name="environment">The environment</param>
        /// <param name="processRunner">The process runner</param>
        /// <param name="tools">The tool locator</param>
        /// <param name="log">Logging handler</param>
        public VagrantRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools, ICakeLog log) : base(fileSystem, environment, processRunner, tools)
        {
            Box = new VagrantBoxRunner(log, Run, Settings);
            Snapshot = new VagrantSnapshotRunner(log, Run, Settings);
            Docker = new VagrantDockerRunner(log, Run, Settings);
            Plugin = new VagrantPluginRunner(log, Run, Settings);
        }

        /// <summary>Gets the name of the tool.</summary>
        /// <returns>The name of the tool.</returns>
        protected override string GetToolName() => "Vagrant by Hashicorp";

        /// <summary>
        /// Gets alternative file paths which the tool may exist in
        /// </summary>
        /// <param name="settings">The settings.</param>
        /// <returns>The default tool path.</returns>
        protected override IEnumerable<FilePath> GetAlternativeToolPaths(VagrantSettings settings)
        {
            yield return "C:\\HashiCorp\\Vagrant\\bin\\vagrant.exe";
        }

        /// <summary>Gets the possible names of the tool executable.</summary>
        /// <returns>The tool executable name.</returns>
        protected override IEnumerable<string> GetToolExecutableNames()
        {
            yield return "vagrant.exe";
        }

        /// <summary>
        /// This initializes the current directory to be a Vagrant environment by creating an initial Vagrantfile if one does not already exist.
        /// </summary>
        /// <param name="name">Will prepopulate the config.vm.box setting in the created Vagrantfile with the specified name</param>
        /// <param name="configure">Settings to control the initialising process</param>
        public void Init(string name, Action<VagrantInitSettings> configure)
        {
            Init(name, null, configure);
        }

        /// <summary>
        /// Initializes the current directory for Vagrant by creating an initial Vagrantfile 
        /// </summary>
        /// <param name="name"></param>
        /// <param name="url"></param>
        /// <param name="configure"></param>
        public void Init(string name, string url = null, Action<VagrantInitSettings> configure = null)
        {
            var settings = new VagrantInitSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("init");
            settings.GetToolArguments().Invoke(args);
            args.Append(name);
            if (url.HasValue()) args.Append(url);
            Run(new VagrantSettings(), args);
        }

        /// <summary>
        /// This command creates and configures guest machines according to your Vagrantfile, using the default settings
        /// </summary>
        /// <param name="name">Optional name of the machine to start</param>
        public void Up(string name = null)
        {
            Up(name, null);
        }

        /// <summary>
        /// This command creates and configures the default machine according to your Vagrantfile, and the specified settings
        /// </summary>
        /// <param name="configure">Settings to use when starting your machine</param>
        public void Up(Action<VagrantUpSettings> configure)
        {
            Up(null, configure);
        }

        /// <summary>
        /// This command creates and configures guest machines according to your Vagrantfile, and the specified settings.
        /// </summary>
        /// <param name="name">Name of the machine to create</param>
        /// <param name="configure">Settings to use when creating your machine</param>
        public void Up(string name, Action<VagrantUpSettings> configure)
        {
            var settings = new VagrantUpSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("up");
            if (name.HasValue()) args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }

        /// <summary>
        /// This command opens an SSH connection to your default machine to run the provided command
        /// </summary>
        /// <param name="configure">Settings to use when connecting</param>
        public void SSH(Action<VagrantSSHSettings> configure)
        {
            SSH(null, configure);
        }

        /// <summary>
        /// This command opens an SSH connection to the specified machine to run the provided command
        /// </summary>
        /// <param name="name">Name of the machine to start</param>
        /// <param name="configure">Settings to use when connecting</param>
        public void SSH(string name, Action<VagrantSSHSettings> configure)
        {
            var settings = new VagrantSSHSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("ssh");
            if (name.HasValue()) args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }

        /// <summary>
        /// This command opens a PowerShell connection to your default machine to run the provided command
        /// </summary>
        /// <param name="configure">Settings to use when connecting</param>
        public void PowerShell(Action<VagrantPowerShellSettings> configure)
        {
            PowerShell(null, configure);
        }

        /// <summary>
        /// This command opens a PowerShell connection to the specified machine to run the provided command
        /// </summary>
        /// <param name="name">Name of the machine to start</param>
        /// <param name="configure">Settings to use when connecting</param>
        public void PowerShell(string name, Action<VagrantPowerShellSettings> configure)
        {
            var settings = new VagrantPowerShellSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("powershell");
            if (name.HasValue()) args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }

        /// <summary>
        /// The equivalent of running a halt followed by an up.
        /// </summary>
        /// <remarks>This command is usually required for changes made in the Vagrantfile to take effect.</remarks>
        /// <param name="name">Optional name of the machine to reload</param>
        public void Reload(string name = null)
        {
            Reload(name, null);
        }

        /// <summary>
        /// The equivalent of running a halt followed by an up.
        /// </summary>
        /// <remarks>This command is usually required for changes made in the Vagrantfile to take effect.</remarks>
        /// <param name="configure">Settings to control the execution of provisioners</param>
        public void Reload(Action<VagrantProvisionerSettings> configure)
        {
            Reload(null, configure);
        }

        /// <summary>
        /// The equivalent of running a halt followed by an up.
        /// </summary>
        /// <remarks>This command is usually required for changes made in the Vagrantfile to take effect.</remarks>
        /// <param name="name">Name of the machine to reload</param>
        /// <param name="configure">Settings to control the execution of provisioners</param>
        public void Reload(string name, Action<VagrantProvisionerSettings> configure)
        {
            var settings = new VagrantProvisionerSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("reload");
            if (name.HasValue()) args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }

        /// <summary>
        /// This resumes a Vagrant managed machine that was previously suspended.
        /// </summary>
        /// <param name="name">Optional name of the machine to resume</param>
        public void Resume(string name = null)
        {
            Resume(name, null);
        }

        /// <summary>
        /// This resumes a Vagrant managed machine that was previously suspended.
        /// </summary>
        /// <param name="configure">Settings to control the execution of provisioners</param>
        public void Resume(Action<VagrantProvisionerSettings> configure)
        {
            Resume(null, configure);
        }

        /// <summary>
        /// This resumes a Vagrant managed machine that was previously suspended.
        /// </summary>
        /// <param name="name">Name of the machine to resume</param>
        /// <param name="configure">Settings to control the execution of provisioners</param>
        public void Resume(string name, Action<VagrantProvisionerSettings> configure)
        {
            var settings = new VagrantProvisionerSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("resume");
            if (name.HasValue()) args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }

        /// <summary>
        /// This command stops the running machine Vagrant is managing and destroys all resources that were created during the machine creation process.
        /// </summary>
        /// <param name="name">Optional name of the machine to destroy</param>
        public void Destroy(string name = null)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("destroy");
            if (name.HasValue()) args.Append(name);
            args.Append("--force");
        }

        /// <summary>
        /// This command shuts down the running machine Vagrant is managing.
        /// </summary>
        /// <param name="name">Optional name of the machine to halt</param>
        /// <param name="force">Whether to forcefully halt the machine (equivalent to pulling the power)</param>
        public void Halt(string name = null, bool force = false)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("halt");
            if (name.HasValue()) args.Append(name);
            if (force) args.Append("--force");
            Run(Settings, args);
        }

        /// <summary>
        /// This suspends the guest machine Vagrant is managing, rather than shutting it down
        /// </summary>
        /// <remarks>
        /// A suspend effectively saves the exact point-in-time state of the machine, so that when you resume it later, it begins running immediately from that point, rather than doing a full boot.</remarks>
        /// <param name="name">Optional name of the machine to suspend</param>
        public void Suspend(string name = null)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("suspend");
            if (name.HasValue()) args.Append(name);
            Run(Settings, args);
        }

        /// <summary>
        /// This packages a currently running VirtualBox environment into a re-usable box.
        /// </summary>
        /// <remarks>This command can only be used with other providers based on the provider implementation and if the provider supports it.</remarks>
        /// <param name="name">Optional name of the machine to package</param>
        public void Package(string name = null)
        {
            Package(name, null);
        }

        /// <summary>
        /// This packages a currently running VirtualBox environment into a re-usable box.
        /// </summary>
        /// <remarks>This command can only be used with other providers based on the provider implementation and if the provider supports it.</remarks>
        /// <param name="configure">Settings to use when packaging the box</param>
        public void Package(Action<VagrantPackageSettings> configure)
        {
            Package(null, configure);
        }

        /// <summary>
        /// This packages a currently running VirtualBox environment into a re-usable box.
        /// </summary>
        /// <remarks>
        /// This command can only be used with other providers based on the provider implementation and if the provider supports it.
        /// </remarks>
        /// <param name="name">Name of the machine to package</param>
        /// <param name="configure">Settings to use when packaging the box</param>
        public void Package(string name, Action<VagrantPackageSettings> configure)
        {
            var settings = new VagrantPackageSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("package");
            if (name.HasValue()) args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }
    }
}