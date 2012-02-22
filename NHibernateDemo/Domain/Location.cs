using System;

namespace NHibernateDemo.Domain
{
    public class Location : IEquatable<Location>
    {
        public virtual string Street { get; set; }
        public virtual string City { get; set; }
        public virtual string Province { get; set; }
        public virtual string Country { get; set; }

        public virtual bool Equals(Location other)
        {
            if (ReferenceEquals(null, other)) return false;
            if (ReferenceEquals(this, other)) return true;
            return Equals(other.Street, Street) && Equals(other.City, City) && Equals(other.Province, Province) && Equals(other.Country, Country);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != typeof (Location)) return false;
            return Equals((Location) obj);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int result = (Street != null ? Street.GetHashCode() : 0);
                result = (result*397) ^ (City != null ? City.GetHashCode() : 0);
                result = (result*397) ^ (Province != null ? Province.GetHashCode() : 0);
                result = (result*397) ^ (Country != null ? Country.GetHashCode() : 0);
                return result;
            }
        }

        public static bool operator ==(Location left, Location right)
        {
            return Equals(left, right);
        }

        public static bool operator !=(Location left, Location right)
        {
            return !Equals(left, right);
        }
    }
}