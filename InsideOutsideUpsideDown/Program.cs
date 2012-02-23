using System;
using System.Linq;
using HibernatingRhinos.Profiler.Appender.NHibernate;
using NHibernate.Cfg;
using NHibernate.Dialect;
using NHibernate.Driver;
using NHibernate.Tool.hbm2ddl;

namespace InsideOutsideUpsideDown
{
    class Program
    {
        static void Main()
        {
            NHibernateProfiler.Initialize();

            var cfg = new Configuration();
            cfg.DataBaseIntegration(x =>
                                        {
                                            x.ConnectionStringName = "scratch";
                                            x.Driver<SqlClientDriver>();
                                            x.Dialect<MsSql2008Dialect>();
                                        });
            cfg.SessionFactory().GenerateStatistics();
            cfg.AddAssembly(typeof (Program).Assembly);

            var exporter = new SchemaExport(cfg);
            exporter.Execute(false, true, false);

            var sessionFactory = cfg.BuildSessionFactory();
            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                var module = new Module { ModuleCode = new ModuleCode { Value = "class Foo; end;" } };
                session.Save(module);
                tx.Commit();
            }
            using(var session = sessionFactory.OpenSession())
            using(var tx = session.BeginTransaction())
            {
                var module = session.QueryOver<Module>().List().First();
                Console.WriteLine(module.ModuleXml);
//                Console.WriteLine(module.ModuleCode);
//                Console.WriteLine(module.ModuleXml);
                tx.Commit();
            }
            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }
    }
}

