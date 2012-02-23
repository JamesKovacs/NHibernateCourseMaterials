using System;

namespace AdventureWorks
{
    public class CustomerAddress
    {
        public virtual Guid Id { get; set; }
        public virtual string AddressType { get; set; }
        public virtual Address Address { get; set; }

        public override string ToString()
        {
            return string.Format("AddressType: {0}, Address: {1}", AddressType, Address);
        }
    }
}