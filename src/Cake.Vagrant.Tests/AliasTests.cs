using System;
using Cake.Core;
using Cake.Testing;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests
{
    public class AliasTests
    {
        [Fact]
        public void Should_Fail_On_Null_Context()
        {
            ICakeContext ctx = null;
            var ex = Record.Exception(() => ctx.Vagrant());
            ex.Should().BeOfType<ArgumentNullException>();
        }

        [Fact]
        public void Should_Throw_If_Executable_Was_Not_Found()
        {
            // Given
            var fixture = new VagrantFixture(r => r.Up());
            fixture.GivenDefaultToolDoNotExist();
            // When
            var action = new Action(() => fixture.Run());
            // Then
            action.ShouldThrow<CakeException>().WithMessage("Vagrant by Hashicorp: Could not locate executable.");
        }

        [Theory]
        [InlineData("./tools/vagrant.exe", "/Working/tools/vagrant.exe")]
        public void Should_Use_Executable_From_Tool_Path_If_Provided(string toolPath, string expected)
        {
            // Given
            var fixture = new VagrantFixture(r => r.Up()) {Settings = {ToolPath = toolPath}};
            fixture.GivenSettingsToolPathExist();
            var result = fixture.Run();
            result.Path.FullPath.Should().Be(expected);
        }

        [Fact]
        public void Should_Find_Executable_If_Tool_Path_Not_Provided()
        {
            // Given
            var fixture = new VagrantFixture(r => r.Up());
            var result = fixture.Run();
            result.Path.FullPath.Should().Be("/Working/tools/vagrant.exe");
        }

        [Fact]
        public void Should_Throw_If_Process_Was_Not_Started()
        {
            // Given
            var fixture = new VagrantFixture(r => r.Up());
            fixture.GivenProcessCannotStart();
            var action = new Action(() => fixture.Run());
            action.ShouldThrow<CakeException>().WithMessage("Vagrant by Hashicorp: Process was not started.");
        }

        [Fact]
        public void Should_Throw_If_Process_Has_A_Non_Zero_Exit_Code()
        {
            // Given
            var fixture = new VagrantFixture(r => r.Up());
            fixture.GivenProcessExitsWithCode(1);
            var action = new Action(() => fixture.Run());
            action.ShouldThrow<CakeException>().WithMessage("Vagrant by Hashicorp: Process returned an error (exit code 1).");
        }
    }
}