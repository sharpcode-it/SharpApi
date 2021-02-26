using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharpApi.Helpers.ObjectExtensions;
using SharpApi.Helpers.test.Model;
using SharpApi.Utility;
using Xunit;

namespace SharpApi.Helpers.test.ObjectExtensions
{
    public class ObjectExtensionHelperTest
    {
        [Fact]
        public void IsNullTest()
        {
            Assert.False(MockFactory.Mock<int>().IsNull());
        }

        [Fact]
        public void ImplementsTest()
        {
            var foo = MockFactory.Mock<List<Foo>>();

            Assert.True(MockFactory.Mock<Foo>().Implements<IEquatable<Foo>>());

            Assert.False(MockFactory.Mock<Foo>().Implements<IDisposable>());

            Assert.True(MockFactory.Mock<Foo>().GetType().Implements<IEquatable<Foo>>());
        }

        [Fact]
        public void IsListTest()
        {
            Assert.True(MockFactory.Mock<List<int>>().IsList());

            Assert.False(MockFactory.Mock<Collection<int>>().IsList());

        }
    }
}
