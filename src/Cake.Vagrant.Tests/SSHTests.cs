using System;
using Cake.Vagrant.Settings;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class SSHTests
    {
        [Theory]
        [InlineData("echo echo")]
        [InlineData("cat")]
        public void Should_Set_Command_When_Provided(string command)
        {
            var fixture = new VagrantFixture(r => r.SSH(s => s.RunCommand(command)));
            var result = fixture.Run();
            result.Args.Should().Be($"ssh --command \"{command}\"");
        }

        [Theory]
        [InlineData("oneword")]
        [InlineData("two words")]
        [InlineData("-with /flags")]
        public void Should_Include_Extra_Args_When_Set(string args)
        {
            var fixture = new VagrantFixture(r => r.SSH(s => s.RunCommand("echo").WithArguments(args)));
            var result = fixture.Run();
            result.Args.Should().Be($"ssh --command \"echo\" -- {args}");
        }

        [Fact]
        public void Should_Fail_When_Command_Not_Provided()
        {
            var fixture = new VagrantFixture(r => r.SSH(s => s.WithArguments("arg1")));
            var result = Record.Exception(() => fixture.Run());
            result.Should().BeOfType<ArgumentOutOfRangeException>();
        }
    }
}