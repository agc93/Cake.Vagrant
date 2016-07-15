using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cake.Core.IO;
using Cake.Testing;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class WorkingDirectoryTests
    {
        [Fact]
        public void Should_Set_Valid_DirectoryPath()
        {
            var fixture = new VagrantFixture(r => r.FromPath("./vm").Up());
            fixture.FileSystem.CreateDirectory("./vm");

            var result = fixture.Run();

            result.Process.WorkingDirectory.FullPath.Should().Be("/Working/vm");
        }

        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Up());
            var result = fixture.Run();
            result.Process.WorkingDirectory.FullPath.Should().Be("/Working");
        }

        [Fact]
        public void Should_Throw_On_NonExistent_Directory()
        {
            var fixture = new VagrantFixture(r => r.FromPath("./fake").Up());
            var action = new Action(() => fixture.Run());
            action.ShouldThrow<DirectoryNotFoundException>().Where(e => e.Message.Contains("fake"));

        }
    }
}
