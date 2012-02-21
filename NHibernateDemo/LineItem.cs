using System;

namespace NHibernateDemo
{
    public class LineItem
    {
        public virtual Guid Id { get; set; }
        public virtual string ProductName { get; set; }
        public virtual int Quantity { get; set; }

        public override string ToString()
        {
            return string.Format("ProductName: {0}, Quantity: {1}", ProductName, Quantity);
        }
    }
}