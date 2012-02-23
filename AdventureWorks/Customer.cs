using Iesi.Collections.Generic;

namespace AdventureWorks
{
    public class Customer
    {
        public Customer()
        {
            CustomerAddresses = new HashedSet<CustomerAddress>();
            PasswordHash = "HASH";
            PasswordSalt = "SALT";
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
        public virtual string PasswordHash { get; set; }
        public virtual string PasswordSalt { get; set; }
        public virtual ISet<CustomerAddress> CustomerAddresses { get; set; }
        
        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", Id, FirstName, LastName);
        }
    }
}