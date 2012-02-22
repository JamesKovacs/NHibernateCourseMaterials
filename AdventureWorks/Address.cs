namespace AdventureWorks
{
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