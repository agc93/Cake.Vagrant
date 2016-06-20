using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class SuspendTests
    {
        [Fact]
        public void Should_Include_Name_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Suspend("web"));
            var result = fixture.Run();
            result.Args.Should().Be("suspend web");
        }

        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Suspend());
            fixture.Run().Args.Should().Be("suspend");
        }
    }
}