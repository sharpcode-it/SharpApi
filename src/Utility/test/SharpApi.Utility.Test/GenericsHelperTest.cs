using System;
using System.Collections.Generic;
using System.Linq;
using SharpApi.Helpers.ObjectExtensions;
using SharpApi.Helpers.ValueTypeExtensions;
using Xunit;
using Foo = SharpApi.Utility.Test.Model.Foo;

namespace SharpApi.Utility.Test
{
    public class GenericsHelperTest
    {
        static GenericsHelperTest()
        {
            MockFactory.Initialize();
        }

        [Fact]
        public void MockTestObject()
        {
            Assert.True(MockFactory.Mock<Foo>().InvokeOn(myFoo=>myFoo.IsType<Foo>()));
        }

        [Fact]
        public void MockListTest()
        {
            Assert.True(MockFactory.Mock<List<Foo>>().InvokeOn(myList => myList.Any()));

            var test = MockFactory.Mock<List<List<Foo>>>();

            Assert.True(test.InvokeOn(myList => myList.Any() && myList.First().IsList()));
        }

        [Fact]
        public void MockValueTypeTest()
        {
            Assert.True(MockFactory.Mock<int>().IsTypeCode(TypeCode.Int32));

            Assert.True(MockFactory.Mock<int>(new NumberConfigOptions<int>
                {
                    MaxValue = 100,
                    MinValue = 1
                })
                .InvokeOn((myNum) => myNum.IsTypeCode(TypeCode.Int32) && myNum.IsWithin(1, 100)));

            Assert.True(MockFactory.Mock<short>().IsTypeCode(TypeCode.Int16));

            //Crash need to fix
            //Assert.True(MockFactory.Mock<short>(new NumberConfigOptions<short>
            //    {
            //        MaxValue = 100,
            //        MinValue = 1
            //    })
            //    .InvokeOn((myNum) => myNum.IsTypeCode(TypeCode.Int16) && myNum.IsWithin(1, 100)));

            Assert.True(MockFactory.Mock<long>().IsTypeCode(TypeCode.Int64));

            //Crash need to fix
            //Assert.True(MockFactory.Mock<long>(new NumberConfigOptions<long>
            //    {
            //        MaxValue = 0,
            //        MinValue = long.MinValue + 1
            //    })
            //    .InvokeOn((myNum) => myNum.IsTypeCode(TypeCode.Int64) && myNum.IsWithin(long.MinValue, 0)));

            Assert.True(MockFactory.Mock<ushort>().IsTypeCode(TypeCode.UInt16));

            Assert.True(MockFactory.Mock<uint>().IsTypeCode(TypeCode.UInt32));

            Assert.True(MockFactory.Mock<ulong>().IsTypeCode(TypeCode.UInt64));

            Assert.True(MockFactory.Mock<string>()
                .InvokeOn((myString) => myString.GetTypeCode() == TypeCode.String));

            //Crash need to fix
            //Assert.True(
            //    MockFactory.Mock<string>(new StringConfigOptions
            //    {
            //        MaxLenght = 10,
            //        MinLenght = 10,
            //        Vocabulary = null
            //    }).InvokeOn(myString =>
            //        myString.GetTypeCode() == TypeCode.String && myString.Length == 10));

            Assert.True(
                MockFactory.Mock<string>(new StringConfigOptions
                {
                    MaxLenght = 5,
                    MinLenght = 1,
                    Vocabulary = null
                }).InvokeOn(myString =>
                    myString.GetTypeCode() == TypeCode.String && myString.IsLenghtWithin(1, 5)));

            Assert.True(MockFactory.Mock<List<int>>(new ListConfigOptions
                {
                    MaxItems = 10,
                    MinItems = 1
                })
                .InvokeOn((myList) => myList.IsListOf<int>() && myList.IsCountBetween(1, 10)));
        }
    }
}
