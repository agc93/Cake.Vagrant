using System;
using Cake.Core;
using Cake.Core.Annotations;

namespace Cake.Vagrant
{
    /// <summary>
    ///     Gets a runner to control Vagrant through the CLI. See the documentation on GitHub for a full API reference.
    /// </summary>
    /// <example>
    /// <code>
    /// <![CDATA[
    /// Vagrant.Init("hashicorp/precise64");
    /// Vagrant.Up(s => s.UseProvider("virtualbox"));
    /// Vagrant.Suspend();
    /// Vagrant.Halt();
    /// Vagrant.Destroy();
    /// ]]>
    /// </code>
    /// </example>
    /// <remarks>Roughly equivalent to the <c>vagrant</c> command.</remarks>
    [CakeAliasCategory("Vagrant")]
    [CakeNamespaceImport("Cake.Vagrant.Settings")]
    [CakeNamespaceImport("Cake.Vagrant.Commands")]
    public static class VagrantAliases
    {
        /// <summary>
        ///     Gets a runner to control Vagrant through the CLI
        /// </summary>
        /// <remarks>Roughly equivalent to the <c>vagrant</c> command.</remarks>
        /// ///
        /// <example>
        ///     <para>Run `vagrant up`:</para>
        ///     <code>
        /// <![CDATA[
        /// Task("Up")
        ///     .Does(() => {
        ///         Vagrant.Up();
        /// });
        /// ]]></code>
        ///     <para>Init using fluent API:</para>
        ///     <code>
        /// <![CDATA[
        /// Task("Fluent-Init")
        /// .Does(() => {
        ///     
        ///     Vagrant.Init("hashicorp/precise64", settings => 
        ///         settings.ConstrainVersion("1.0.1")
        ///         .CreateMinimal()
        ///         .OutputToFile("base.box"));
        /// });
        /// ]]></code>
        /// </example>
        [CakePropertyAlias]
        [CakeAliasCategory("Vagrant")]
        public static VagrantRunner Vagrant(this ICakeContext ctx)
        {
            if (ctx == null) throw new ArgumentNullException(nameof(ctx));
            return new VagrantRunner(ctx.FileSystem, ctx.Environment, ctx.ProcessRunner, ctx.Tools, ctx.Log);
        }
    }
}