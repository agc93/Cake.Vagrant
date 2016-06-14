using System;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Vagrant
{
    [CakeAliasCategory("Vagrant")]
    [CakeNamespaceImport("Cake.Vagrant.Settings")]
    [CakeNamespaceImport("Cake.Vagrant.Commands")]
    public static class VagrantAliases
    {
        [CakePropertyAlias]
        [CakeAliasCategory("Vagrant")]
        public static VagrantRunner Vagrant(this ICakeContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            return new VagrantRunner(ctx.FileSystem, ctx.Environment, ctx.ProcessRunner, ctx.Tools, ctx.Log);
        }
    }
}