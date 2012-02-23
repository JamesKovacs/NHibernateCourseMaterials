using System;
using System.Xml.Linq;
using Iesi.Collections.Generic;

namespace NHibernateDemo.Domain
{
    public class Customer : IEquatable<Customer>
    {
        public Customer()
        {
            MemberSince = new DateTime(2000, 1, 1);
            Orders = new HashedSet<Order>();
        }

        public virtual Guid Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string FullName { get; set; }
        public virtual DateTimeOffset MemberSince { get; set; }
        public virtual bool IsGoldMember { get; set; }
        public virtual double Rating { get; set; }
        public virtual string Notes { get; set; }
        public virtual Location Address { get; set; }

        public virtual ISet<Order> Orders { get; set; }

        public virtual Colour FavouriteColour { get; set; }
        public virtual XDocument Description { get; set; }

        // Options#1: Use C#
//        public virtual Order LastOrder { get { return Orders.Last(); } }
        // Option#2: Use a formula in the hbm.xml file
//        public virtual Order LastOrder { get; set; }
        // Option#3: Use collection filtering on Orders

        public override string ToString()
        {
            return string.Format("Id: {0}, FullName: {1}, MemberSince: {2}, IsGoldMember: {3}, Rating: {4}", Id, FullName, MemberSince, IsGoldMember, Rating);
        }

        public virtual void AddOrder(Order order)
        {
            Orders.Add(order);
            order.Customer = this;
        }

        public virtual bool Equals(Customer other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other.Id.Equals(Id);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (!(obj is Customer)) return false;
            return Equals((Customer) obj);
        }

        public override int GetHashCode()
        {
            return Id.GetHashCode();
        }

        public static bool operator ==(Customer left, Customer right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Customer left, Customer right)
        {
            return !Equals(left, right);
        }
    }
}