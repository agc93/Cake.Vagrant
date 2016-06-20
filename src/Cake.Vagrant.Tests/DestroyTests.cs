using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class DestroyTests
    {
        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Destroy());
            var result = fixture.Run();
            result.Args.Should().Be("destroy --force");
        }

        [Fact]
        public void Should_Include_Name_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Destroy("web"));
            var result = fixture.Run();
            result.Args.Should().Be("destroy web --force");
        }
    }
}
