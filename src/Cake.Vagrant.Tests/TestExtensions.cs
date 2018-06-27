using System;
using FluentAssertions;
using FluentAssertions.Specialized;

namespace Cake.Vagrant.Tests
{
    public static class TestExtensions
    {
        public static ExceptionAssertions<TAssertion> ShouldThrow<TAssertion>(this Action action) where TAssertion : Exception {
            return action.Should().Throw<TAssertion>();
        }
    }
}