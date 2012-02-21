using System;
using System.Data;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateDemo
{
    class Program
    {
        static void Main()
        {
            NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            cfg.Configure(); // OPTIONAL: Only required if you want to read in hibernate.cfg.xml
            cfg.DataBaseIntegration(x =>
                                        {
                                            x.ConnectionStringName = "medassets";
                                            x.Driver<SqlClientDriver>();
                                            x.Dialect<MsSql2008Dialect>();
                                            x.IsolationLevel = IsolationLevel.ReadCommitted;
                                            x.Timeout = 10;
                                            x.LogSqlInConsole = true;
                                        });
            cfg.SessionFactoryName("MedAssets");
            cfg.SessionFactory().GenerateStatistics();
            cfg.AddAssembly(typeof (Customer).Assembly);

//            cfg.GetClassMapping(typeof (ShippingMethod)).IsMutable = true;

            var exporter = new SchemaExport(cfg);
            exporter.Execute(true, true, false);

//            var updater = new SchemaUpdate(cfg);
//            updater.Execute(true, true);

            Guid newId;
            var sessionFactory = cfg.BuildSessionFactory();
            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                var customer = new Customer
                {
                    Name = "John Smith",
                    MemberSince = DateTimeOffset.Now,
                    Rating = 2d / 3,
                    Address = new Location
                    {
                        Street = "123 Somewhere Street",
                        City = "Nowhere",
                        Province = "Alberta",
                        Country = "Canada"
                    }
                };
                Console.WriteLine("Original:");
                Console.WriteLine(customer);
                session.Save(customer);
                newId = customer.Id;
//                var customer = session.Load<Customer>(1);
//                Console.WriteLine(customer);
//                var shippingMethod = new ShippingMethod {Name = "Priority"};
//                session.Save(shippingMethod);
                tx.Commit();
            }

            using(var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(newId);
                Console.WriteLine("Reloaded:");
                Console.WriteLine(customer);
                tx.Commit();
            }
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }
    }

    public class ShippingMethod
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}

