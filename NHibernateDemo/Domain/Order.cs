using System;
using Iesi.Collections.Generic;

namespace NHibernateDemo.Domain
{
    public class Order : IEquatable<Order>
    {
        public Order()
        {
            LineItems = new HashedSet<LineItem>();
        }

        public virtual Guid Id { get; set; }
        public virtual DateTimeOffset OrderedOn { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ISet<LineItem> LineItems { get; set; }

        public virtual bool Equals(Order other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Order)) return false;
            return Equals((Order) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Order left, Order right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Order left, Order right)
        {
            return !Equals(left, right);
        }

        public override string ToString()
        {
            return string.Format("OrderedOn: {0}", OrderedOn);
        }
    }
}