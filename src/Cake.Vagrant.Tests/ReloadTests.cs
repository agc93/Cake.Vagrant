using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Vagrant.Settings;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class ReloadTests
    {
        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Reload());
            var result = fixture.Run();
            result.Args.Should().Be("reload");
        }

        [Fact]
        public void Should_Include_Name_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Reload("vm"));
            var result = fixture.Run();
            result.Args.Should().Be("reload vm");
        }

        [Fact]
        public void Should_Set_Provisioners_When_Explicitly_Set()
        {
            var fixture = new VagrantFixture(r => r.Reload(s => s.RunProvisioners()));
            var result = fixture.Run();
            result.Args.Should().Be("reload --provision");
        }

        [Fact]
        public void Should_Use_Provisioners_When_Provided()
        {
            var fixture = new VagrantFixture(r => r.Reload(s => s.WithProvisioners("chef", "shell")));
            var result = fixture.Run();
            result.Args.Should().Be("reload --provision --provision-with chef,shell");
        }
    }
}
