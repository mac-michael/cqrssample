using System;
using System.Diagnostics;
using System.Reflection;
using CQRS.Base.Infrastructure.NHibernate.Conventions;
using FluentNHibernate.Automapping;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using FluentNHibernate.Conventions.Helpers;
using NHibernate;
using NHibernate.Cfg;
using NHibernate.Tool.hbm2ddl;

namespace CQRS.Base.Infrastructure.NHibernate
{
    public class EntityManager : IEntityManager
    {
        public static Action<ISession> GenerateSampleData = s => { };
        public static Func<Assembly[]> GetAssemblies = () => new Assembly[0];

        private readonly IPerRequestSessionFactory _perRequestSessionFactory;

        public EntityManager(IPerRequestSessionFactory perRequestSessionFactory)
        {
            _perRequestSessionFactory = perRequestSessionFactory;
        }

        private static readonly Lazy<ISessionFactory> NHibernateFactory =
           new Lazy<ISessionFactory>(() =>
           {
               var config = Configure();
               var factory = config.BuildSessionFactory();

               using(var session = factory.OpenSession())
               {
                   GenerateSampleData(session);
                   session.Flush();
               }
               return factory;
           });

        private static Configuration Configure()
        {
            var config = Fluently.Configure()
                .Database(
                    SQLiteConfiguration.Standard.UsingFile("database").ShowSql()
                //MsSqlConfiguration.MsSql2008.ConnectionString(b => b.FromConnectionStringWithKey("db"))
                )
                .Mappings(m =>
                {
                    var model = AutoMap.Assemblies(new AutomappingConfiguration(), GetAssemblies());
                    model.Conventions.Add(new SetEnumTypeConvention());
                    model.Conventions.Add(new UseNewSqlDateTime2TypeConvention());
                    model.Conventions.Add(new CollectionAccessConvention());
                    model.Conventions.Add(new SqlTimestampConvention());
                    model.Conventions.Add(new SetTableNameConvention());
                    
                    model.Conventions.Add(DefaultLazy.Never());
                    m.AutoMappings.Add(model);
                    m.AutoMappings.ExportTo("c:\\mapping");
                    m.FluentMappings.ExportTo("c:\\mapping");
                })
                .BuildConfiguration();

            // Generatre schema before each start
            var e = new SchemaExport(config);
            e.SetOutputFile(@"c:\mapping\a.sql");
            e.Execute(true, true, false);

            return config;
        }
        public static ISessionFactory SessionFactory { get { return NHibernateFactory.Value; } }

        public ISession CurrentSession
        {
            get { return _perRequestSessionFactory.Create(); }
        }
    }
}