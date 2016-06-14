using System;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Vagrant.Commands
{
    public abstract class VagrantCommandRunner
    {
        internal VagrantCommandRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings)
        {
            Log = log;
            Runner = runCallback;
            Settings = settings;
        }

        protected VagrantSettings Settings { get; set; }
        protected ICakeLog Log { get; set; }
        protected Action<VagrantSettings, ProcessArgumentBuilder> Runner { get; set; }
    }
}