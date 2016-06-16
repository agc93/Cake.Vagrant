using System;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.Vagrant.Commands
{
    /// <summary>
    /// Base class for Vagrant subcommands
    /// </summary>
    public abstract class VagrantCommandRunner
    {
        internal VagrantCommandRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback, VagrantSettings settings)
        {
            Log = log;
            Runner = runCallback;
            Settings = settings;
        }

        /// <summary>
        /// Settings for invocation of the Vagrant CLI
        /// </summary>
        protected VagrantSettings Settings { get; set; }
        /// <summary>
        /// Logging output
        /// </summary>
        protected ICakeLog Log { get; set; }
        /// <summary>
        /// Action to trigger invocation of the CLI
        /// </summary>
        protected Action<VagrantSettings, ProcessArgumentBuilder> Runner { get; set; }
    }
}