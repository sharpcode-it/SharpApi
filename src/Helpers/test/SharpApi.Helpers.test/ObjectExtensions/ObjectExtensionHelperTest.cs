using System;
using SharpApi.Helpers.ObjectExtensions;
using SharpApi.Helpers.test.MockUp;
using Xunit;

namespace SharpApi.Helpers.test.ObjectExtensions
{
    public class ObjectExtensionHelperTest
    {
        [Fact]
        public void IsDefaultTest()
        {
            Assert.True((default(string)).IsDefault());

            Assert.True((default(DateTime)).IsDefault());

            Assert.True((default(int)).IsDefault());

            Assert.True((default(long)).IsDefault());

            Assert.True((default(object)).IsDefault());

            Assert.True((Guid.Empty).IsDefault());

            Assert.False((new Foo()).IsDefault());

            Assert.False((Guid.NewGuid()).IsDefault());
        }

        [Fact]
        public void ImplementsTest()
        {
            var myObject = new Foo();

            Assert.True(myObject.Implements<IEquatable<Foo>>());

            Assert.False(myObject.Implements<IDisposable>());
        }
    }
}
