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
    public class PackageTests
    {
        [Fact]
        public void Should_Run_With_Defaults()
        {
            var fixture = new VagrantFixture(r => r.Package());
            var result = fixture.Run();
            result.Args.Should().Be("package");
        }

        [Fact]
        public void Should_Include_Name_When_Set()
        {
            var fixture = new VagrantFixture(r => r.Package("web"));
            var result = fixture.Run();
            result.Args.Should().Be("package web");
        }

        [Theory]
        [InlineData("./sample.box")]
        [InlineData("sample.box")]
        public void Should_Include_Output_Path_When_Set(string path)
        {
            var fixture = new VagrantFixture(r => r.Package(s => s.OutputToFile(path)));
            var result = fixture.Run();
            result.Args.Should().Be("package --output sample.box");
        }

        [Fact]
        public void Should_Use_BaseName()
        {
            var fixture = new VagrantFixture(r => r.Package(s => s.UseBaseImage("vbox-vm")));
            var result = fixture.Run();
            result.Args.Should().Be("package --base vbox-vm");
        }

        [Fact]
        public void Should_Include_Single_File()
        {
            var fixture = new VagrantFixture(r => r.Package(s => s.IncludeInPackage("./file")));
            var result = fixture.Run();
            result.Args.Should().Be("package --include file");
        }

        [Fact]
        public void Should_Include_Multiple_Files()
        {
            var fixture = new VagrantFixture(r => r.Package(s => s.IncludeInPackage("./file", "./other-file")));
            var result = fixture.Run();
            result.Args.Should().Be("package --include file,other-file");
        }

        [Fact]
        public void Should_Load_Vagrantfile()
        {
            var fixture = new VagrantFixture(r => r.Package(s => s.IncludeVagrantFile("./src/Vagrantfile")));
            var result = fixture.Run();
            result.Args.Should().Be("package --vagrantfile src/Vagrantfile");
        }
    }
}
