using System;
using System.Data;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

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
                                        });
            cfg.SessionFactoryName("MedAssets");
            cfg.SessionFactory().GenerateStatistics();
            cfg.AddAssembly(typeof (Customer).Assembly);

//            cfg.GetClassMapping(typeof (ShippingMethod)).IsMutable = true;

            int newId;
            var sessionFactory = cfg.BuildSessionFactory();
            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                var customer = new Customer { Name = "John Smith", 
                    MemberSince = DateTimeOffset.Now, 
                    Rating=2d/3};
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

    public class Customer
    {
        public Customer()
        {
            MemberSince = new DateTime(2000, 1, 1);
        }

        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
        public virtual DateTimeOffset MemberSince { get; set; }
        public virtual bool IsGoldMember { get; set; }
        public virtual double Rating { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}, MemberSince: {2}, IsGoldMember: {3}, Rating: {4}", Id, Name, MemberSince, IsGoldMember, Rating);
        }
    }

    public class ShippingMethod
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}

