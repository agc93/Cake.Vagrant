using Cake.Vagrant.Settings;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class InitTests
    {
        [Theory]
        [InlineData("hashicorp/precise64")]
        [InlineData("scotch/box")]
        public void Should_Run_With_Defaults(string imageName)
        {
            var fixture = new VagrantFixture(r => r.Init(imageName));
            var result = fixture.Run();
            result.Args.Should().Be($"init {imageName}");
        }

        [Fact]
        public void Should_Use_Force_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Init("hashicorp/precise64", s => s.Force()));
            var result = fixture.Run();
            result.Args.Should().Be($"init --force hashicorp/precise64");
        }

        [Fact]
        public void Should_Use_Minimal_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Init("hashicorp/precise64", s => s.CreateMinimal()));
            var result = fixture.Run();
            result.Args.Should().Be("init --minimal hashicorp/precise64");
        }

        [Fact]
        public void Should_Use_OutputPath_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Init("hashicorp/precise64", s => s.OutputToFile("test/vagrant")));
            var result = fixture.Run();
            result.Args.Should().Be("init --output \"test/vagrant\" hashicorp/precise64");
        }

        [Fact]
        public void Should_Use_Version_Constraint_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Init("hashicorp/precise64", s => s.ConstrainVersion("> 0.1.5")));
            var result = fixture.Run();
            result.Args.Should().Be("init --box-version \"> 0.1.5\" hashicorp/precise64");
        }
    }
}