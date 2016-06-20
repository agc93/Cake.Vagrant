using System;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests.Commands
{
    public class DockerTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Should_Throw_On_Null_Command(string command)
        {
            var fixture = new VagrantFixture(r => r.Docker.Exec("web", command));
            var action = new Action(() => fixture.Run());
            action.ShouldThrow<ArgumentNullException>();
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Should_Throw_On_Null_Name(string name)
        {
            var fixture = new VagrantFixture(r => r.Docker.Exec(name, "echo"));
            var action = new Action(() => fixture.Run());
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Should_Exec_Command_Name()
        {
            var fixture = new VagrantFixture(r => r.Docker.Exec("web", "cowsay"));
            var result = fixture.Run();
            result.Args.Should().Be("docker-exec web -- cowsay");
        }

        [Fact]
        public void Should_Run_Command_Name()
        {
            var fixture = new VagrantFixture(r => r.Docker.Run("db", "fortune"));
            var result = fixture.Run();
            result.Args.Should().Be("docker-run db -- fortune");
        }
    }
}