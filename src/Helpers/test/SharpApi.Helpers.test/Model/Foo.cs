using System;

namespace SharpApi.Helpers.test.Model
{
    [Serializable]
    public class Foo:IEquatable<Foo>
    {
        public Guid Id { get; set; }
        public DateTime CreateDateTime { get; set; }
        public string ClassName { get; set; }

        public bool Equals(Foo other)
        {
            if (other is null) return false;
            if (ReferenceEquals(this, other)) return true;
            return Id.Equals(other.Id) && CreateDateTime.Equals(other.CreateDateTime) && ClassName == other.ClassName;
        }

        public override bool Equals(object obj)
        {
            if (obj is null) return false;
            if (ReferenceEquals(this, obj)) return true;
            return obj.GetType() == this.GetType() && Equals((Foo) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, CreateDateTime, ClassName);
        }
    }
}
