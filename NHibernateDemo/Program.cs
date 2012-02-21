using System;
using System.Data;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;

namespace NHibernateDemo
{
    class Program
    {
        static void Main()
        {
            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
                                        {
                                            x.ConnectionStringName = "medassets";
                                            x.Driver<SqlClientDriver>();
                                            x.Dialect<MsSql2008Dialect>();
                                            x.IsolationLevel = IsolationLevel.ReadCommitted;
                                        });
            cfg.AddAssembly(typeof (Customer).Assembly);
            var sessionFactory = cfg.BuildSessionFactory();
            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
//                var customer = new Customer { Name = "John Smith"};
//                session.Save(customer);
                var customer = session.Load<Customer>(1);
                Console.WriteLine(customer);
                tx.Commit();
            }
            Console.WriteLine("Press <ENTER> to continue...");
            Console.ReadLine();
        }
    }

    public class Customer
    {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }

        public override string ToString()
        {
            return string.Format("Id: {0}, Name: {1}", Id, Name);
        }
    }
}
