using System;
using System.Data;
using System.Linq;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Linq;
using NHibernate.Util;

namespace AdventureWorks
{
    class Program
    {
        static Configuration cfg;
        static ISessionFactory sessionFactory;

        static void Main()
        {
            InitializeNHibernate();
            sessionFactory = cfg.BuildSessionFactory();

            using (var session = sessionFactory.OpenSession())
            using (var tx = session.BeginTransaction())
            {
//                var customer = session.QueryOver<Customer>().List().First();
//                var customer = session.Get<Customer>(1);
//                HibernatingRhinos.Profiler.Appender.ProfilerInfrastructure.FlushAllMessages();
//                Console.WriteLine(customer.FirstName);
//                HibernatingRhinos.Profiler.Appender.ProfilerInfrastructure.FlushAllMessages();
//                var customer = session.Load<Customer>(1);
//                Console.WriteLine(customer.Id);
//                var order = new Order();
//                order.Customer = session.Load<Customer>(1);
//                session.Save(order);
                // HQL
//                var query = session.CreateQuery("select c from Customer c where c.LastName like 'Sm%'");
//                var customers = query.List<Customer>();
//                foreach (var customer in customers)
//                {
//                    Console.WriteLine(customer);
//                }
                // Criteria
//                var criteria = session.CreateCriteria<Customer>()
//                                      .Add(Restrictions.Eq("LastName", "Gee"));
//                var customers = criteria.List<Customer>();
//                foreach (var customer in customers)
//                {
//                    Console.WriteLine(customer);
//                }
//                var criteria = session.QueryOver<Customer>()
//                                        .Where(x => x.LastName == "Gee");
//                var customers = criteria.List<Customer>();
//                foreach (var customer in customers)
//                {
//                    Console.WriteLine(customer);
//                }
                // LINQ
//                var query = session.Query<Customer>().Where(x => x.LastName == "Gee");
                var gee = session.Get<Customer>(1);
                Console.WriteLine(gee);
                var gee2 = session.Get<Customer>(29773);
                Console.WriteLine(gee2);
                var query = from c in session.Query<Customer>()
                            where c.LastName == "Gee"
                            select c;
                var customers = query.ToList();
                foreach (var customer in customers)
                {
                    Console.WriteLine(customer);
                    Console.WriteLine("Is Gee? {0}", customer == gee);
                    Console.WriteLine("Is Gee2? {0}", customer == gee2);
                }
                tx.Commit();
            }

            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }

//        public class Order
//        {
//            public int Id { get; set; }
//            public Customer Customer { get; set; }
//        }

        static void InitializeNHibernate()
        {
            NHibernateProfiler.Initialize();

            cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
                                        {
                                            x.ConnectionStringName = "adventureworks";
                                            x.Driver<SqlClientDriver>();
                                            x.Dialect<MsSql2008Dialect>();
                                            x.IsolationLevel = IsolationLevel.ReadCommitted;
                                            x.Timeout = 10;
                                            x.LogSqlInConsole = true;
                                        });
            cfg.SessionFactoryName("AdventureWorks");
            cfg.SessionFactory().GenerateStatistics();
            cfg.AddAssembly(typeof (Customer).Assembly);
        }
    }
}

