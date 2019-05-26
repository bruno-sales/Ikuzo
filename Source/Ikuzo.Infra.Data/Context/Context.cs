using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;
using Ikuzo.Domain.ValueObjects;
using Ikuzo.Infra.Data.Config;

namespace Ikuzo.Infra.Data.Context
{
    public class Context : DbContext
    {
        public  Context() : base("DBContext")
        {
            Database.ExecuteSqlCommand("SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED;");
            Database.CommandTimeout = 6000;

            //Desligando salvar todas as mudanças
            Configuration.AutoDetectChangesEnabled = false;

            //Forçando Consulta de relacionamentos
            Configuration.LazyLoadingEnabled = false;
            Configuration.ProxyCreationEnabled = false;
        }
         

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Remove<OneToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<ManyToManyCascadeDeleteConvention>();
            modelBuilder.Conventions.Remove<DecimalPropertyConvention>();

            modelBuilder.Conventions.Add(new DecimalPropertyConvention(12, 6));
            
            modelBuilder.Configurations.Add(new ModalConfig());
            modelBuilder.Configurations.Add(new LineConfig());
            modelBuilder.Configurations.Add(new GpsConfig());
            modelBuilder.Configurations.Add(new ItineraryConfig());
            modelBuilder.Configurations.Add(new TagConfig());
            modelBuilder.Configurations.Add(new GpsHistoryConfig());

            modelBuilder.Ignore<ValidationResult>();
        }
    }
}
