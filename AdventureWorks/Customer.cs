using System;
using Iesi.Collections.Generic;

namespace AdventureWorks
{
    public class Customer
    {
        public Customer()
        {
            CustomerAddresses = new HashedSet<CustomerAddress>();
        }

        public virtual int Id { get; set; }
        public virtual string Title { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string MiddleName { get; set; }
        public virtual string LastName { get; set; }
        public virtual string Suffix { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string SalesPerson { get; set; }
        public virtual string EmailAddress { get; set; }
        public virtual string Phone { get; set; }
        public virtual ISet<CustomerAddress> CustomerAddresses { get; set; }
        
        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", Id, FirstName, LastName);
        }
    }

    public class CustomerAddress
    {
        public virtual Guid Id { get; set; }
        public virtual string AddressType { get; set; }
        public virtual Address Address { get; set; }
    }

    public class Address
    {
        public virtual int Id { get; set; }
        public virtual string AddressLine1 { get; set; }
        public virtual string AddressLine2 { get; set; }
        public virtual string City { get; set; }
        public virtual string StateProvince { get; set; }
        public virtual string CountryRegion { get; set; }
        public virtual string PostalCode { get; set; }
    }
}