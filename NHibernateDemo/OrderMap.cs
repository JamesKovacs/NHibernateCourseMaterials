using FluentNHibernate.Mapping;

namespace NHibernateDemo
{
    public sealed class OrderMap : ClassMap<Order>
    {
        public OrderMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.OrderedOn);
            HasMany(x => x.LineItems).Cascade.AllDeleteOrphan();
            References(x => x.Customer).Cascade.SaveUpdate();
        }
    }
}