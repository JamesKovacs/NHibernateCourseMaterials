using System;
using Iesi.Collections.Generic;

namespace NHibernateDemo
{
    public class Customer
    {
        public Customer()
        {
            MemberSince = new DateTime(2000, 1, 1);
            Orders = new HashedSet<Order>();
        }

        public virtual Guid Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTimeOffset MemberSince { get; set; }
        public virtual bool IsGoldMember { get; set; }
        public virtual double Rating { get; set; }
        public virtual string Notes { get; set; }
        public virtual Location Address { get; set; }

        public virtual ISet<Order> Orders { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, MemberSince: {2}, IsGoldMember: {3}, Rating: {4}", Id, Name, MemberSince, IsGoldMember, Rating);
        }

        public virtual void AddOrder(Order order)
        {
            Orders.Add(order);
            order.Customer = this;
        }
    }
}