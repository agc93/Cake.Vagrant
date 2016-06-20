using Cake.Vagrant.Settings;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class ResumeTests
    {
        [Fact]
        public void Should_Include_Name_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Resume("web"));
            var result = fixture.Run();
            result.Args.Should().Be("resume web");
        }

        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Resume());
            var result = fixture.Run();
            result.Args.Should().Be("resume");
        }

        [Fact]
        public void Should_Use_Provision_When_Explicitly_Set()
        {
            var fixture = new VagrantFixture(r => r.Resume(s => s.RunProvisioners()));
            var result = fixture.Run();
            result.Args.Should().Be("resume --provision");
        }

        [Fact]
        public void Should_Use_Provisioners()
        {
            var fixture = new VagrantFixture(r => r.Resume(s => s.WithProvisioners("chef", "shell")));
            var result = fixture.Run();
            result.Args.Should().Be("resume --provision --provision-with chef,shell");
        }
    }
}