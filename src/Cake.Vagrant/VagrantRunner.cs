using System;
using System.Collections.Generic;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant
{
    public class VagrantRunner : Tool<VagrantSettings>
    {
        private VagrantSettings Settings { get; set; } = new VagrantSettings();
        public VagrantBoxRunner Box { get; set; }
        public VagrantRunner(IFileSystem fileSystem, ICakeEnvironment environment, IProcessRunner processRunner,
            IToolLocator tools, ICakeLog log) : base(fileSystem, environment, processRunner, tools)
        {
            Box = new VagrantBoxRunner(log, Run, Settings);
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

        public void Init(string name, string url = null)
        {
            Init(name, null, null);
        }

        public void Init(string name, Action<VagrantInitSettings> configure)
        {
            Init(name, null, configure);
        }

        public void Init(string name, string url, Action<VagrantInitSettings> configure)
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
            Up((Action<VagrantUpSettings>) null);
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
    }

    public class VagrantBoxRunner
    {
        private VagrantSettings Settings { get; set; }
        internal VagrantBoxRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings)
        {
            Log = log;
            Runner = runCallback;
            Settings = settings;
        }

        private ICakeLog Log { get; set; }

        private Action<VagrantSettings, ProcessArgumentBuilder> Runner { get; set; }

        public void Add(string address)
        {
            Add(address, null);
        }

        public void Add(string address, Action<VagrantBoxAddSettings> configure)
        {
            var settings = new VagrantBoxAddSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("add");
            args.Append(address);
            settings.GetToolArguments().Invoke(args);
            Log.Information($"Adding box from {address}...");
            Runner.Invoke(Settings, args);
        }

        [Obsolete("This doesn't make sense in a scripted environment")]
        public void List()
        {
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("list");
            Runner.Invoke(Settings, args);
        }

        public void Outdated(bool global = false)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("outdated");
            if (global) args.Append("--global");
            Runner.Invoke(Settings, args);
        }

        public void Remove(string name, Action<VagrantBoxRemoveSettings> configure = null)
        {
            var settings = new VagrantBoxRemoveSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("remove");
            args.Append(name);
            settings.GetToolArguments().Invoke(args);
            Runner.Invoke(Settings, args);
        }

        public void Repackage(string name, string provider, string version)
        {
            var args = new ProcessArgumentBuilder();
            var s = new[] {"box", "repackage", name, provider, version};
            foreach (var arg in s)
            {
                args.Append(arg);
            }
            Runner.Invoke(Settings, args);
        }

        public void Update(Action<VagrantBoxUpdateSettings> configure = null)
        {
            var settings = new VagrantBoxUpdateSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("update");
            settings.GetToolArguments().Invoke(args);
            Runner.Invoke(Settings, args);
        }

    }
}