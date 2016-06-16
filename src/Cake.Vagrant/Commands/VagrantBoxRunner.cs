using System;
using Cake.Core;
using Cake.Core.Diagnostics;
using Cake.Core.IO;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant.Commands
{
    /// <summary>
    ///     Wrapper around the <c>vagrant box</c> subcommands
    /// </summary>
    public class VagrantBoxRunner : VagrantCommandRunner
    {
        internal VagrantBoxRunner(ICakeLog log, Action<VagrantSettings, ProcessArgumentBuilder> runCallback,
            VagrantSettings settings) : base(log, runCallback, settings)
        {
        }

        /// <summary>
        ///     This adds a box with the given address to Vagrant.
        /// </summary>
        /// <param name="address">Can be: shorthand name from Atlas, file path or URL to a box either locally or in a catalog</param>
        /// <param name="configure">Optional settings to use when adding the box</param>
        public void Add(string address, Action<VagrantBoxAddSettings> configure = null)
        {
            var settings = new VagrantBoxAddSettings();
            configure?.Invoke(settings);
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("add");
            args.Append(address.Quote());
            settings.GetToolArguments().Invoke(args);
            Log.Information($"Adding box from {address}...");
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        ///     Lists all installed boxes
        /// </summary>
        public void List()
        {
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("list");
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        ///     This command tells you whether or not the box you are using in your current Vagrant environment is outdated.
        /// </summary>
        /// <param name="global">If <c>true</c>, every installed box will be checked for updates.</param>
        public void Outdated(bool global = false)
        {
            var args = new ProcessArgumentBuilder();
            args.Append("box");
            args.Append("outdated");
            if (global) args.Append("--global");
            Runner.Invoke(Settings, args);
        }

        /// <summary>
        ///     This command removes a box from Vagrant that matches the given name.
        /// </summary>
        /// <remarks>
        ///     If a box has multiple providers, the exact provider must be specified with the
        ///     <see cref="VagrantBoxRemoveSettings.Provider" /> property.
        /// </remarks>
        /// <param name="name">Name of the box to remove</param>
        /// <param name="configure">Optional settings for removing a box.</param>
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

        /// <summary>
        ///     This command repackages the given box and puts it in the current directory so you can redistribute it.
        /// </summary>
        /// <remarks>
        ///     This command is useful for reclaiming a *.box file from an installed Vagrant box.
        /// </remarks>
        /// <param name="name">Name of the machine to repackage</param>
        /// <param name="provider">Name of the provider (from <see cref="List" />)</param>
        /// <param name="version">Version to repackage (from <see cref="List" />)</param>
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

        /// <summary>
        ///     This command updates the box for the current Vagrant environment if there are updates available
        /// </summary>
        /// <remarks>
        ///     Note that updating the box will not update an already-running Vagrant machine. To reflect the changes in the box,
        ///     you will have to destroy and bring back up the Vagrant machine.
        /// </remarks>
        /// <param name="configure">Optional settings to use for updating</param>
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