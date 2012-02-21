using System;
using Iesi.Collections.Generic;

namespace NHibernateDemo
{
    public class Order
    {
        public Order()
        {
            LineItems = new HashedSet<LineItem>();
        }

        public virtual Guid Id { get; set; }
        public virtual DateTimeOffset OrderedOn { get; set; }
        public virtual Customer Customer { get; set; }
        public virtual ISet<LineItem> LineItems { get; set; }
    }
}