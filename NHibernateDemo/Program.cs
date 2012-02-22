using System;
using System.Data;
using System.Linq;
using FluentNHibernate.Cfg;
using FluentNHibernate.Testing;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Impl;
using NHibernate.Linq;
using NHibernate.Tool.hbm2ddl;

namespace NHibernateDemo
{
    class Program
    {
        static Configuration cfg;
        static ISessionFactory sessionFactory;

        static void Main()
        {
            InitializeNHibernate();
            ExportDbSchema();
//            MappingGotchas();
//            ImmutableData();
//            Relationships();
//            Inheritance();
            PersistenceSpecs();
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }

        static void PersistenceSpecs()
        {
            sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            {
                new PersistenceSpecification<LineItem>(session)
                    .CheckProperty(x => x.ProductName, "Product1")
                    .CheckProperty(x => x.Quantity, 42)
                    .VerifyTheMappings();

                new PersistenceSpecification<Order>(session)
                    .CheckProperty(x => x.OrderedOn, DateTimeOffset.Now)
                    .CheckReference(x => x.Customer, CreateCustomer())
                    .CheckList(x => x.LineItems, new[] {new LineItem()})
                    .VerifyTheMappings();
                
                new PersistenceSpecification<Customer>(session)
                    .CheckProperty(x => x.FullName, "Bob Smith")
                    .CheckProperty(x => x.Title, "Mr.")
                    .CheckProperty(x => x.IsGoldMember, true)
                    .CheckProperty(x => x.MemberSince, DateTimeOffset.Now)
                    .CheckProperty(x => x.Notes, "Lorem ipsum")
                    .CheckProperty(x => x.Rating, 3.1415)
                    .CheckProperty(x => x.Address, CreateAddress())
//                    .CheckList(x => x.Orders, new[]{CreateOrder(), CreateOrder()})
                    .VerifyTheMappings();
            }
        }

        static void Inheritance()
        {
            sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var dog = new Dog {Name = "Rover"};
                session.Save(dog);
                tx.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var query = from animal in session.Query<Animal>()
                            select animal;
                var animals = query.ToList();
                foreach (var animal in animals)
                {
                    Console.WriteLine(animal);
                }
                tx.Commit();
            }
        }

        static void Relationships()
        {
            sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = CreateCustomer();
                customer.AddOrder(CreateOrder());
                customer.AddOrder(CreateOrder());
                customer.AddOrder(CreateOrder());
                customer.AddOrder(CreateOrder());
                customer.AddOrder(CreateOrder());
                session.Save(customer);
                tx.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = session.QueryOver<Customer>()
//                                      .Fetch(x => x.Orders).Eager
                                      .List<Customer>().First();
                foreach (var order in customer.Orders)
                {
                    Console.WriteLine("Order {0}", order.OrderedOn);
                    foreach (var lineItem in order.LineItems)
                    {
                        Console.WriteLine(lineItem);
                    }
                }
                tx.Commit();
            }
        }

        static Order CreateOrder()
        {
            var order = new Order
                            {
                                OrderedOn = DateTimeOffset.Now,
                                LineItems =
                                    {
                                        new LineItem {ProductName = "Product1", Quantity = 1},
                                        new LineItem {ProductName = "Product2", Quantity = 9},
                                        new LineItem {ProductName = "Product3", Quantity = 3},
                                        new LineItem {ProductName = "Product4", Quantity = 2},
                                    }
                            };
            return order;
        }

        static void MappingGotchas()
        {
            Guid newId;
            sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = CreateCustomer();
                Console.WriteLine("Original:");
                Console.WriteLine(customer);
                session.Save(customer);
                newId = customer.Id;
//                var customer = session.Load<Customer>(1);
//                Console.WriteLine(customer);
                tx.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var customer = session.Load<Customer>(newId);
                customer.FullName = "Jane Doe";
                customer.FullName = "John Smith";
//                session.Update(customer);
                Console.WriteLine("Reloaded:");
                Console.WriteLine(customer);
                tx.Commit();
            }
        }

        static Customer CreateCustomer()
        {
            var customer = new Customer
                               {
                                   FullName = "John Smith",
                                   MemberSince = DateTimeOffset.Now,
                                   Rating = 2d/3,
                                   Address = CreateAddress()
                               };
            return customer;
        }

        static Location CreateAddress()
        {
            return new Location
                       {
                           Street = "123 Somewhere Street",
                           City = "Nowhere",
                           Province = "Alberta",
                           Country = "Canada"
                       };
        }

        static void ImmutableData()
        {
            cfg.GetClassMapping(typeof (ShippingMethod)).IsMutable = true;

            sessionFactory = cfg.BuildSessionFactory();
            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var shippingMethod = new ShippingMethod {Name = "Priority"};
                session.Save(shippingMethod);
                tx.Commit();
            }

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
                var shippingMethod = session.QueryOver<ShippingMethod>().List().First();
                shippingMethod.Name = "Foo";
                tx.Commit();
            }
        }

        static void InitializeNHibernate()
        {
            NHibernateProfiler.Initialize();

            cfg = new Configuration();
            cfg.Configure(); // OPTIONAL: Only required if you want to read in hibernate.cfg.xml
            cfg.DataBaseIntegration(x =>
                                        {
                                            x.ConnectionStringName = "medassets";
                                            x.Driver<SqlClientDriver>();
                                            x.Dialect<MsSql2008Dialect>();
                                            x.IsolationLevel = IsolationLevel.ReadCommitted;
                                            x.Timeout = 10;
                                        });
            cfg.SessionFactoryName("MedAssets");
            cfg.SessionFactory().GenerateStatistics();
//            cfg.AddAssembly(typeof (Customer).Assembly);

            cfg = Fluently.Configure(cfg)
                    .Mappings(x => x.FluentMappings.AddFromAssemblyOf<Customer>())
                    .BuildConfiguration();
        }

        static void ExportDbSchema()
        {
            var exporter = new SchemaExport(cfg);
            exporter.Execute(false, true, false);

//            var updater = new SchemaUpdate(cfg);
//            updater.Execute(true, true);
        }
    }

    public class ShippingMethod
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}