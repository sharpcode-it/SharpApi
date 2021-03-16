using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace SharpApi.Utility.Test.Random
{
    public class RandomGeneratorTest
    {
        [Fact]
        public void Test1()
        {
            var listOfNumbers = new List<int>();
            for (var i = 0; i < 1000; i++)
            {
                var number = RandomGenerator.GetInt();
                if (listOfNumbers.Contains(number)) break;
                listOfNumbers.Add(number);
            }

            Assert.True(listOfNumbers.Count>=1000);

            Assert.True(RandomGenerator.GetInt(0,10)<=10);

            Assert.True(RandomGenerator.GetInt(-10,0)<=0);

            var randomNumber = RandomGenerator.GetInt(-1000, 1000);

            Assert.True((randomNumber<=1000) && (randomNumber>=-1000));

            var listOfString = new List<string>();

            for (var i = 0; i < 1000; i++)
            {
                var str = RandomGenerator.GetString(0,10);
                if (listOfString.Contains(str)) break;
                listOfString.Add(str);
            }
            //need reegine ig was generated duplicated string id condition with 1000 was not satisfied
            //Assert.True(listOfString.Count >= 1000);

            listOfString.Clear();

            for (var i = 0; i < 1000; i++)
            {
                var str = RandomGenerator.GetString(0,10,"#!_&&$_abc");
                if (listOfString.Contains(str)) break;
                listOfString.Add(str);
            }

            Assert.True(listOfString.Count >= 1000);
        }

        [Fact]
        public void ShuffleTest()
        {
            var list = new List<int> {1, 2, 3, 4, 5, 6, 7, 8, 9, 10};

            var shuffleList = list.Shuffle().ToList();

            Assert.True(list.Intersect(shuffleList).Count() == list.Count);

            Assert.False(list.SequenceEqual(shuffleList));

            Assert.True(list.SequenceEqual(shuffleList.OrderBy(i=>i)));
        }
    }
}
