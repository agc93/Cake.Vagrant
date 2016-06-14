using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core;
using Cake.Vagrant.Settings;

namespace Cake.Vagrant
{
    class Samples
    {
        private static ICakeContext context;

        private void SampleInvocations()
        {
            context.Vagrant().Up();
            context.Vagrant().Init("hashicorp/precise64", settings => 
                settings.ConstrainVersion("1.0.1")
                    .CreateMinimal()
                    .OutputToFile("base.box"));
            context.Vagrant().Up(settings =>
                settings.DestroyOnError()
                    .InstallProvider()
                    .WithProvisioners("chef", "shell")
                    .EnableParallel()
                    .UseProvider("hyperv"));
            context.Vagrant().Snapshot.Save("Cake script");
            context.Vagrant().Suspend();
            context.Vagrant().Resume(settings => settings.WithProvisioners("shell"));
            context.Vagrant().Plugin.Install("plugin", 
                s => s.CleanFirst()
                    .FromSource("http://source")
                    .SetEntryPoint("main.sh"));
            context.Vagrant().SSH(s => s.RunCommand("echo echo"));
            context.Vagrant().Reload(s => s.WithProvisioners("chef"));
        }
    }
}
