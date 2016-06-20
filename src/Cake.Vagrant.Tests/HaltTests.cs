using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class HaltTests
    {
        [Fact]
        public void Should_Include_Both_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Halt("db", true));
            fixture.Run().Args.Should().Be("halt db --force");
        }

        [Fact]
        public void Should_Include_Force_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Halt(force: true));
            var result = fixture.Run();
            result.Args.Should().Be("halt --force");
        }

        [Fact]
        public void Should_Include_Name_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Halt("web"));
            var result = fixture.Run();
            result.Args.Should().Be("halt web");
        }

        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Halt());
            fixture.Run().Args.Should().Be("halt");
        }
    }
}