using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.IO;

namespace Cake.Vagrant.Settings
{
    /// <summary>
    /// Additional settings for controlling provisioner execution
    /// </summary>
    public class VagrantProvisionerSettings : IVagrantCommandSettings
    {
        /// <summary>
        /// Gets the command arguments corresponding to the specified <see cref="IVagrantCommandSettings"/>
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        public Action<ProcessArgumentBuilder> GetToolArguments()
        {
            return args =>
            {
                if (RunProvisioners) args.Append("--provision");
                if (Provisioners.Any())
                {
                    args.AppendSwitch("--provision-with", string.Join(",", Provisioners));
                }
            };
        }

        /// <summary>
        /// Gets or sets a value controlling whether to run provisioners on reload.
        /// </summary>
        /// <remarks>Defaults to false</remarks>
        /// <value><c>true</c> to run provisioners, otherwise they will not be run.</value>
        public bool RunProvisioners { get; set; }

        /// <summary>
        ///     This will only run the given provisioners.
        /// </summary>
        /// <value>Providers to run. Omit the leading `:`</value>
        public IEnumerable<string> Provisioners { get; set; } = new string[] { };
    }

    /// <summary>
    /// Fluent extension methods to the <see cref="VagrantProvisionerSettings"/> class
    /// </summary>
    public static class VagrantProvisionerSettingsExtensions
    {
        /// <summary>
        ///     Force the provisioners to run during the operation
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="run"><c>true</c> to run provisioners, <c>false</c> to skip</param>
        /// <returns>The updated settings object</returns>
        public static VagrantProvisionerSettings RunProvisioners(this VagrantProvisionerSettings settings,
            bool run = true)
        {
            settings.RunProvisioners = run;
            return settings;
        }

        /// <summary>
        ///     Adds the given provisioners to run during the operation
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provisioners">List of provisioner names to run</param>
        /// <returns>The updated settings object</returns>
        public static VagrantProvisionerSettings WithProvisioners(this VagrantProvisionerSettings settings,
            IEnumerable<string> provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        /// <summary>
        ///     Adds the given provisioners to run during the operation
        /// </summary>
        /// <param name="settings">The settings</param>
        /// <param name="provisioners">Collection of provisioner names to run</param>
        /// <returns>The updated settings object</returns>
        public static VagrantProvisionerSettings WithProvisioners(this VagrantProvisionerSettings settings,
            params string[] provisioners)
        {
            settings.Provisioners = AddProvisioners(settings, provisioners);
            return settings;
        }

        private static List<string> AddProvisioners(VagrantProvisionerSettings settings, IEnumerable<string> provisioners)
        {
            var l = settings.Provisioners.ToList();
            l.AddRange(provisioners);
            settings.RunProvisioners = l.Any();
            return l;
        }
    }
}
