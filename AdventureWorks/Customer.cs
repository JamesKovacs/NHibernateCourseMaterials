namespace AdventureWorks
{
    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        
        public override string ToString()
        {
            return string.Format("Id: {0}, FirstName: {1}, LastName: {2}", Id, FirstName, LastName);
        }
    }
}