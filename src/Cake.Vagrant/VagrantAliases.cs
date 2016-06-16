using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Vagrant
{
    /// <summary>
    /// Gets a runner to control Vagrant through the CLI
    /// </summary>
    /// <remarks>Roughly equivalent to the <c>vagrant</c> command.</remarks>
    [CakeAliasCategory("Vagrant")]
    [CakeNamespaceImport("Cake.Vagrant.Settings")]
    [CakeNamespaceImport("Cake.Vagrant.Commands")]
    public static class VagrantAliases
    {
        /// <summary>
        /// Gets a runner to control Vagrant through the CLI
        /// </summary>
        /// <remarks>Roughly equivalent to the <c>vagrant</c> command.</remarks>
        [CakePropertyAlias]
        [CakeAliasCategory("Vagrant")]
        public static VagrantRunner Vagrant(this ICakeContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            return new VagrantRunner(ctx.FileSystem, ctx.Environment, ctx.ProcessRunner, ctx.Tools, ctx.Log);
        }
    }
}