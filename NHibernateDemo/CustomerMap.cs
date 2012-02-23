using FluentNHibernate.Mapping;
using NHibernateDemo.Domain;

namespace NHibernateDemo
{
    public sealed class CustomerMap : ClassMap<Customer>
    {
        public CustomerMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.FullName).Not.Nullable();
            Map(x => x.Title);
            Map(x => x.IsGoldMember);
            Map(x => x.MemberSince);
            Map(x => x.Notes).Length(4000);
            Map(x => x.Rating);
//            Component(x => x.Address, m =>
//                                          {
//                                              m.Map(x => x.Street);
//                                              m.Map(x => x.City);
//                                              m.Map(x => x.Province);
//                                              m.Map(x => x.Country);
//                                          });
            Component(x => x.Address);
            HasMany(x => x.Orders).Cascade.AllDeleteOrphan().Inverse().BatchSize(10);
        } 
    }

    public class LocationMap : ComponentMap<Location>
    {
        public LocationMap()
        {
             Map(x => x.Street);
             Map(x => x.City);
             Map(x => x.Province);
             Map(x => x.Country);
        }
    }
}