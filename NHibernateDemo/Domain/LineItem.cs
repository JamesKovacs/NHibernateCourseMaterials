using System;

namespace NHibernateDemo.Domain
{
    public class LineItem : IEquatable<LineItem>
    {
        public virtual Guid Id { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int Quantity { get; set; }

        public override string ToString()
        {
            return string.Format("ProductName: {0}, Quantity: {1}", ProductName, Quantity);
        }

        public virtual bool Equals(LineItem other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (LineItem)) return false;
            return Equals((LineItem) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(LineItem left, LineItem right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(LineItem left, LineItem right)
        {
            return !Equals(left, right);
        }
    }
}