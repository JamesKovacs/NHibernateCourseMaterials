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

        public override string ToString()
        {
            return string.Format("AddressLine1: {0}, AddressLine2: {1}, City: {2}, StateProvince: {3}, CountryRegion: {4}, PostalCode: {5}", AddressLine1, AddressLine2, City, StateProvince, CountryRegion, PostalCode);
        }
    }
}