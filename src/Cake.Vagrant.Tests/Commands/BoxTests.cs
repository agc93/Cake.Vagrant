using System;
using Cake.Core;
using Cake.Vagrant.Settings;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests.Commands
{
    public class BoxTests
    {
        public class AddTests
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("  ")]
            public void Should_Throw_On_Null_Address(string address)
            {
                var fixture = new VagrantFixture(r => r.Box.Add(address));
                var action = new Action(() => fixture.Run());
                action.ShouldThrow<ArgumentNullException>().Where(e => e.Message.Contains("address"));
            }

            [Theory]
            [InlineData("hashicorp/precise64")]
            [InlineData("http://sample.org/example.box")]
            public void Should_Run_With_Defaults(string address)
            {
                var fixture = new VagrantFixture(r => r.Box.Add(address));
                var result = fixture.Run();
                result.Args.Should().Be($"box add \"{address}\"");
            }

            [Theory]
            [InlineData("1.2.3")]
            [InlineData(">= 1.0, < 2.0")]
            public void Should_Set_Version_Constraint(string version)
            {
                var fixture = new VagrantFixture(r => r.Box.Add("hashicorp/precise64", s => s.ConstrainVersion(version)));
                var result = fixture.Run();
                result.Args.Should().Be($"box add {"hashicorp/precise64".Quote()} --version \"{version}\"");
            }

            [Theory]
            [InlineData("virtualbox")]
            [InlineData("docker")]
            public void Should_Use_Chosen_Provider(string provider)
            {
                var fixture = new VagrantFixture(r => r.Box.Add("hashicorp/precise64", s => s.UseProvider(provider)));
                var result = fixture.Run();
                result.Args.Should().Be($"box add \"hashicorp/precise64\" --provider {provider}");
            }

            [Fact]
            public void Should_Set_Clean_Flag()
            {
                var fixture = new VagrantFixture(r => r.Box.Add("hashicorp/precise64", s => s.CleanFirst()));
                var result = fixture.Run();
                result.Args.Should().Be($"box add {"hashicorp/precise64".Quote()} --clean");
            }

            [Fact]
            public void Should_Set_Force_Flag()
            {
                var fixture = new VagrantFixture(r => r.Box.Add("hashicorp/precise64", s => s.Force()));
                var result = fixture.Run();
                result.Args.Should().Be("box add \"hashicorp/precise64\" --force");
            }

            [Fact]
            public void Should_Set_Insecure_Flag()
            {
                var fixture = new VagrantFixture(r => r.Box.Add("hashicorp/precise64", s => s.AllowInsecure()));
                var result = fixture.Run();
                result.Args.Should().Be("box add \"hashicorp/precise64\" --insecure");
            }
        }

        public class OutdatedTests
        {
            [Fact]
            public void Should_Run_With_Defaults()
            {
                var fixture = new VagrantFixture(r => r.Box.Outdated());
                var result = fixture.Run();
                result.Args.Should().Be("box outdated");
            }

            [Fact]
            public void Should_Specify_Global_When_Set()
            {
                var fixture = new VagrantFixture(r => r.Box.Outdated(true));
                var result = fixture.Run();
                result.Args.Should().Be("box outdated --global");
            }
        }

        public class RemoveTests
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void Should_Throw_When_No_Name_Set(string name)
            {
                var fixture = new VagrantFixture(r => r.Box.Remove(name));
                Action action = () => fixture.Run();
                action.ShouldThrow<ArgumentNullException>();
            }

            [Theory]
            [InlineData("hyperv")]
            [InlineData("docker")]
            public void Should_Include_Provider_When_Set(string provider)
            {
                var fixture = new VagrantFixture(r => r.Box.Remove("utopic", s => s.UseProvider(provider)));
                var result = fixture.Run();
                result.Args.Should().Be($"box remove utopic --provider {provider}");
            }

            [Theory]
            [InlineData("1.0.1")]
            [InlineData(">= 1.0, < 2.0")]
            public void Should_Set_Version_Constraint(string version)
            {
                var fixture = new VagrantFixture(r => r.Box.Remove("vivid", s => s.ConstrainVersion(version)));
                var result = fixture.Run();
                result.Args.Should().Be($"box remove vivid --box-version {version.Quote()}");
            }

            [Fact]
            public void Should_Delete_All_When_Set()
            {
                var fixture = new VagrantFixture(r => r.Box.Remove("precise", s => s.RemoveAll()));
                var result = fixture.Run();
                result.Args.Should().Be("box remove precise --all");
            }

            [Fact]
            public void Should_Include_Force_When_Set()
            {
                var fixture = new VagrantFixture(r => r.Box.Remove("trusty", s => s.Force()));
                var result = fixture.Run();
                result.Args.Should().Be("box remove trusty --force");
            }
        }

        public class RepackageTests
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void Should_Throw_With_Blank_Name(string name)
            {
                var fixture = new VagrantFixture(r => r.Box.Repackage(name, "docker", "1.0.0"));
                Action action = () => fixture.Run();
                action.ShouldThrow<ArgumentNullException>();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void Should_Throw_When_No_Provider_Given(string provider)
            {
                var fixture = new VagrantFixture(r => r.Box.Repackage("vagrant", provider, "1.0.0"));
                Action action = () => fixture.Run();
                action.ShouldThrow<ArgumentNullException>();
            }

            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void Should_Throw_Without_Version(string version)
            {
                var fixture = new VagrantFixture(r => r.Box.Repackage("vagrant", "docker", version));
                Action action = () => fixture.Run();
                action.ShouldThrow<ArgumentNullException>();
            }

            [Fact]
            public void Should_Include_Options()
            {
                var fixture = new VagrantFixture(r => r.Box.Repackage("hashicorp", "docker", "1.0.0"));
                var result = fixture.Run();
                result.Args.Should().Be("box repackage hashicorp docker 1.0.0");
            }
        }

        public class UpdateTests
        {
            [Fact]
            public void Should_Not_Specify_Provider_When_Box_Name_Not_Set()
            {
                var fixture = new VagrantFixture(r => r.Box.Update(s => s.UseProvider("docker")));
                var result = fixture.Run();
                result.Args.Should().Be("box update");
            }

            [Fact]
            public void Should_Run_With_Defaults()
            {
                var fixture = new VagrantFixture(r => r.Box.Update());
                var result = fixture.Run();
                result.Args.Should().Be("box update");
            }

            [Fact]
            public void Should_Specify_Name_When_Set()
            {
                var fixture = new VagrantFixture(r => r.Box.Update(s => s.OnlyUpdate("oneiric")));
                var result = fixture.Run();
                result.Args.Should().Be("box update --box oneiric");
            }

            [Fact]
            public void Should_Specify_Provider_When_Box_Name_Is_Set()
            {
                var fixture = new VagrantFixture(r => r.Box.Update(s => s.OnlyUpdate("natty").UseProvider("docker")));
                var result = fixture.Run();
                result.Args.Should().Be("box update --box natty --provider docker");
            }
        }
    }
}