using System;
using Cake.Core.IO;
using Cake.Core.Tooling;

namespace Cake.Vagrant
{
    /// <summary>
    ///     Settings to invoke the Vagrant CLI
    /// </summary>
    public class VagrantSettings : ToolSettings
    {
        
    }

    internal interface IVagrantCommandSettings
    {
        /// <summary>
        ///     Gets the command arguments corresponding to the specified <see cref="IVagrantCommandSettings" />
        /// </summary>
        /// <returns>An action to add required command arguments</returns>
        Action<ProcessArgumentBuilder> GetToolArguments();
    }
}