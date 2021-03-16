using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using SharpApi.Helpers.ObjectExtensions;
using SharpApi.Helpers.test.Model;
using Xunit;

namespace SharpApi.Helpers.test.ObjectExtensions
{
    public class ObjectExtensionHelperTest
    {
        [Fact]
        public void IsNullTest()
        {
            Assert.False(5.IsNull());
        }

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

            Assert.True(myObject.GetType().Implements<IEquatable<Foo>>());
        }

        [Fact]
        public void IsListTest()
        {
            IList<int> myTestIList = new List<int>();

            Assert.True(myTestIList.IsList());

            Assert.True((myTestIList.ToList()).IsList());

            Assert.False((new Collection<int>()).IsList());

            Assert.True((new Collection<int>()).ToList().IsList());
        }
    }
}
