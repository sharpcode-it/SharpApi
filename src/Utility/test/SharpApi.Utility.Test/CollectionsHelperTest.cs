using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities.ObjectModel;
using Xunit;

namespace SharpApi.Utility.Test
{
    public class CollectionsHelperTest
    {
        [Fact]
        public void ShuffleTest()
        {
            MockFactory.Initialize(new MockConfigOptions
            {
                //ListOptions = new ListConfigOptions
                //{
                //    AllowEmptyList = true,
                //    MaxDictionaryItems = 5,
                //    MaxListItems    = 20
                //},
                //StringOptions = new StringConfigOptions
                //{
                //    AllowEmptyString = false,
                //    MaxStringLenght = 100
                //}
            });

            var temp = 0;
            Dictionary<string, string> myList = null;
            try
            {

            do
            {
                myList?.Clear();
                myList = MockFactory.Mock<Dictionary<string, string>>();
                temp++;
            } while (myList.Count < 10);
            //var myShuffledList = myList.Shuffle();
            Assert.True(myList.Count>5);
            }
            catch (Exception e)
            {
                var p = temp;
                Console.WriteLine(e);
                throw;
            }
        }
    }
}
