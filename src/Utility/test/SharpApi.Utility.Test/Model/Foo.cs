using System;
using System.Collections.Generic;

namespace SharpApi.Utility.Test.Model
{
    [Serializable]
    public class Foo:IEquatable<Foo>
    {
        public Guid Id { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public string StringProperty { get; set; }

        public SisterOfFoo MySister { get; set; }

        public bool BoolProperty { get; set; }
        public char CharProperty { get; set; }
        public SonOfFoo Fooino { get; set; }

        public ICollection<SonOfFoo> Fooinos { get; set; }

        public IEnumerable<int> IntItems { get; set; }

        public bool Equals(Foo other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && DateTimeProperty.Equals(other.DateTimeProperty) 
                                       && StringProperty == other.StringProperty
                                       && CharProperty == other.CharProperty;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Foo) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, DateTimeProperty, StringProperty,CharProperty);
        }
    }

    [Serializable]
    public class SisterOfFoo
    {
        public Guid Id { get; set; }
        public IList<SonOfFoo> MySon { get; set; }

        public DateTime BirthDate { get; set; }
    }

    [Serializable]
    public class SonOfFoo
    {
        public Guid Id { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public string StringProperty { get; set; }

        public IList<string> Attributes { get; set; }

        public IDictionary<string,int> Items { get; set; }

        public SonOfSonOfFoo SonOfSonOfFoo { get; set; }
    }

    [Serializable]
    public class SonOfSonOfFoo
    {
        public Guid Id { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public string StringProperty { get; set; }

        public IDictionary<Guid, SonOfSonOfFooItem> Items { get; set; }
    }

    [Serializable]
    public class SonOfSonOfFooItem
    {
        public Guid Id { get; set; }
        public DateTime DateTimeProperty { get; set; }
        public string StringProperty { get; set; }
    }
}
