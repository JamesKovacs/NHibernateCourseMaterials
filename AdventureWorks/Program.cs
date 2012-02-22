using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Criterion;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Engine;
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
//                GetVsLoad(session);
//                Hql(session);
//                Criteria(session);
//                CriteriaQueryOver(session);
//                Linq(session);

//                Paging(session);
//                MultiCriteria(session);
//                MultiQuery(session);
//                Futures(session);
//                Aggregation(session);
//                CollectionFilters(session);
//                ExtraLazyCollections(session);
//                LazyProperties(session);
                tx.Commit();
            }

            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }

        static void Paging(ISession session)
        {
        }

        static void MultiCriteria(ISession session)
        {
        }

        static void MultiQuery(ISession session)
        {
        }

        static void Futures(ISession session)
        {
        }

        static void Aggregation(ISession session)
        {
//                var customers = session.CreateQuery("from Customer c where size(c.CustomerAddresses) > 0").List<Customer>();
//                var customers = from c in session.Query<Customer>()
//                                where c.CustomerAddresses.Count > 0
//                                select c;
//                Console.WriteLine(customers.Count());

//            var stats = from c in session.Query<Customer>()
//                        group c by c.FirstName into grp
//                        where grp.Count() > 10
//                        orderby grp.Count() ascending 
//                        select new{grp.Key, Count=grp.Count()};
//            foreach (var stat in stats)
//            {
//                    Console.WriteLine("{0}: {1}", stat.Key, stat.Count);
//            }

//            var stats = session.CreateQuery("select c.FirstName, count(c) from Customer c group by c.FirstName having count(c) > 10 order by count(c) asc")
//                                .List<object[]>();
//            foreach (var stat in stats)
//            {
//                Console.WriteLine("{0}: {1}", stat[0], stat[1]);
//            }
        }
        
        static void CollectionFilters(ISession session)
        {
        }

        static void ExtraLazyCollections(ISession session)
        {
        }

        static void LazyProperties(ISession session)
        {
        }

        static void GetVsLoad(ISession session)
        {
            var customer = session.Get<Customer>(1);
//            var customer = session.Load<Customer>(1);
            HibernatingRhinos.Profiler.Appender.ProfilerInfrastructure.FlushAllMessages();
            Console.WriteLine(customer.Id);
            HibernatingRhinos.Profiler.Appender.ProfilerInfrastructure.FlushAllMessages();
            Console.WriteLine(customer.FirstName);
        }

        static void Hql(ISession session)
        {
            var query = session.CreateQuery("select c from Customer c where c.LastName like 'Sm%'");
            var customers = query.List<Customer>();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void Criteria(ISession session)
        {
            var criteria = session.CreateCriteria<Customer>()
                .Add(Restrictions.Eq("LastName", "Gee"));
            var customers = criteria.List<Customer>();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void CriteriaQueryOver(ISession session)
        {
            var criteria = session.QueryOver<Customer>()
                .Where(x => x.LastName == "Gee");
            var customers = criteria.List<Customer>();
            foreach (var customer in customers)
            {
                Console.WriteLine(customer);
            }
        }

        static void Linq(ISession session)
        {
//            var query = session.Query<Customer>().Where(x => x.LastName == "Gee");
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
                                        });
            cfg.SessionFactoryName("AdventureWorks");
            cfg.SessionFactory().GenerateStatistics();
            cfg.AddAssembly(typeof (Customer).Assembly);
        }
    }
}

