using System.Collections.Generic;

namespace SharpApi.Utility
{
    public class MockConfigOptions
    {
        public MockConfigOptions()
        {
            ListOptions = new ListConfigOptions
            {
                MinItems = 1,
                MaxItems = 10
            };
            StringOptions = new StringConfigOptions
            {
                MinLenght = 1,
                MaxLenght = 50,
                Vocabulary = null
            };
            DictionaryConfigOptions = new DictionaryConfigOptions
            {
                MinItems = 1,
                MaxItems = 20,
                DictionaryKeyOptions = new DictionaryKeyOptions
                {
                    KeyMode = DictionaryKeyMode.Random,
                    KeyPrefix = string.Empty
                }
            };
        }

        public ListConfigOptions ListOptions { get; set; }

        public StringConfigOptions StringOptions { get; set; }

        public DictionaryConfigOptions DictionaryConfigOptions { get; set; }
    }

    public class NumberConfigOptions<T>
    {
        public T MaxValue { get; set; }
        public T MinValue { get; set; }
    }

    public class ListConfigOptions
    {
        public int MaxItems { get; set; }
        public int MinItems { get; set; }
    }

    public class DictionaryConfigOptions
    {
        public int MaxItems { get; set; }

        public int MinItems { get; set; }

        public DictionaryKeyOptions DictionaryKeyOptions { get; set; }
    }

    public class StringConfigOptions
    {
        public int MaxLenght { get; set; }
        public int MinLenght { get; set; }

        public IEnumerable<string> Vocabulary { get; set; }
    }

    public class DictionaryKeyOptions{
        public DictionaryKeyMode KeyMode { get; set; }
        public string KeyPrefix { get; set; }
    }

    public enum DictionaryKeyMode
    {
        Random,
        Static,
        Mixed
    }
}
