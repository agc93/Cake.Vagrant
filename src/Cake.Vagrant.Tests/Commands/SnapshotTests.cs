using System;
using Cake.Vagrant.Settings;
using FluentAssertions;
using Xunit;

namespace Cake.Vagrant.Tests.Commands
{
    public class SnapshotTests
    {
        public class SaveTests
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void Should_Not_Save_Without_Name(string name)
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Save(name));
                Action action = () => fixture.Run();
                action.ShouldThrow<ArgumentNullException>();
            }

            [Fact]
            public void Should_Push_Without_Name()
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Push());
                var result = fixture.Run();
                result.Args.Should().Be("snapshot push");
            }

            [Fact]
            public void Should_Save_With_Name()
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Save("snapshot1"));
                var result = fixture.Run();
                result.Args.Should().Be("snapshot save snapshot1");
            }
        }

        public class RestoreTests
        {
            [Theory]
            [InlineData(null)]
            [InlineData("")]
            [InlineData("   ")]
            public void Should_Not_Restore_Without_Name(string name)
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Restore(name));
                Action action = () => fixture.Run();
                action.ShouldThrow<ArgumentNullException>();
            }

            [Theory]
            [InlineData(true, "--provision")]
            [InlineData(false, "--no-provision")]
            public void Should_Add_Provisioners_When_Set(bool run, string flagValue)
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Restore("ss2", s => s.RunProvisioners(run)));
                var result = fixture.Run();
                result.Args.Should().Be($"snapshot restore ss2 {flagValue}");
            }

            [Fact]
            public void Should_Add_NoDelete_For_Pop()
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Pop(s => s.DoNotDelete()));
                var result = fixture.Run();
                result.Args.Should().Be("snapshot pop --no-delete");
            }

            [Fact]
            public void Should_Not_Add_NoDelete_For_Restore()
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Restore("snapshot2", s => s.DoNotDelete()));
                var result = fixture.Run();
                result.Args.Should().Be("snapshot restore snapshot2");
            }

            [Fact]
            public void Should_Pop_Without_Name()
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Pop());
                var result = fixture.Run();
                result.Args.Should().Be("snapshot pop");
            }

            [Fact]
            public void Should_Restore_With_Name()
            {
                var fixture = new VagrantFixture(r => r.Snapshot.Restore("snapshot2"));
                var result = fixture.Run();
                result.Args.Should().Be("snapshot restore snapshot2");
            }
        }

        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public void Should_Throw_Without_Name(string name)
        {
            var fixture = new VagrantFixture(r => r.Snapshot.Delete(name));
            Action action = () => fixture.Run();
            action.ShouldThrow<ArgumentNullException>();
        }

        [Fact]
        public void Should_Delete_When_Name_Set()
        {
            var fixture = new VagrantFixture(r => r.Snapshot.Delete("old"));
            var result = fixture.Run();
            result.Args.Should().Be("snapshot delete old");
        }
    }
}