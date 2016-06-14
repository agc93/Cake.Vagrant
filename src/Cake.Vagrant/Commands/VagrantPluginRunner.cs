using System;
using System.Collections.Generic;
using System.Linq;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant.Commands
{
    public class VagrantPluginRunner : VagrantCommandRunner
    {
        public VagrantPluginRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        public void Install(string name, Action<VagrantPluginInstallSettings> configure = null)
        {
            Install(new []{name}, configure);
        }

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

        public void Uninstall(params string[] names)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("plugin", "uninstall");
            args.AppendAll(names.ToArray());
            Runner.Invoke(Settings, args);
        }

        public void Update(string name = null)
        {
            var args = new ProcessArgumentBuilder();
            args.AppendAll("plugin", "update");
            if (name.HasValue()) args.Append(name);
            Runner.Invoke(Settings, args);
        }
    }
}