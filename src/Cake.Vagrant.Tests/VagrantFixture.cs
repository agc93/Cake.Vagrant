using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Testing;
using Cake.Testing.Fixtures;

namespace Cake.Vagrant.Tests
{
    public class VagrantFixture : ToolFixture<VagrantSettings>
    {
        public VagrantFixture(Action<VagrantRunner> runAction) : base("vagrant.exe")
        {
            RunAction = runAction;
        }

        private Action<VagrantRunner> RunAction { get; set; }

        protected override void RunTool()
        {
            var tool = new VagrantRunner(FileSystem, Environment, ProcessRunner, Tools, new FakeLog());
            RunAction?.Invoke(tool);
        }
    }
}
