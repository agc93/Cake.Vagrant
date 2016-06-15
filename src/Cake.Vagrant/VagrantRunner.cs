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

        protected override string GetToolName() => "Vagrant by Hashicorp";

        protected override IEnumerable<FilePath> GetAlternativeToolPaths(VagrantSettings settings)
        {
            yield return "C:\\HashiCorp\\Vagrant\\bin\\vagrant.exe";
        }

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

        public void Up(string name = null)
        {
            Up(name, null);
        }

        public void Up(Action<VagrantUpSettings> configure)
        {
            Up(null, configure);
        }

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

        public void SSH(Action<VagrantSSHSettings> configure)
        {
            SSH(null, configure);
        }

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

        public void PowerShell(Action<VagrantPowerShellSettings> configure)
        {
            PowerShell(null, configure);
        }

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

        public void Reload(string name = null)
        {
            Reload(name, null);
        }

        public void Reload(Action<VagrantProvisionerSettings> configure)
        {
            Reload(null, configure);
        }

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

        public void Resume(string name = null)
        {
            Resume(name, null);
        }

        public void Resume(Action<VagrantProvisionerSettings> configure)
        {
            Resume(null, configure);
        }

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

        public void Share(Action<VagrantShareSettings> configure = null)
        {
            var settings = new VagrantShareSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("share");
            settings.GetToolArguments().Invoke(args);
            Run(Settings, args);
        }

        public void Destroy(string name = null, bool force = false)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("destroy");
            if (name.HasValue()) args.Append(name);
            args.Append("--force");
        }

        public void Halt(string name = null, bool force = false)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("halt");
            if (name.HasValue()) args.Append(name);
            if (force) args.Append("--force");
            Run(Settings, args);
        }

        public void Suspend(string name = null)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("suspend");
            if (name.HasValue()) args.Append(name);
            Run(Settings, args);
        }

        public void Package(string name = null)
        {
            Package(name, null);
        }

        public void Package(Action<VagrantPackageSettings> configure)
        {
            Package(null, configure);
        }

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