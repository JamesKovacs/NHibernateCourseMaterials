using FluentNHibernate.Mapping;

namespace NHibernateDemo
{
    public sealed class LineItemMap : ClassMap<LineItem>
    {
        public LineItemMap()
        {
            Id(x => x.Id).GeneratedBy.GuidComb();
            Map(x => x.ProductName);
            Map(x => x.Quantity);
        }
    }
}