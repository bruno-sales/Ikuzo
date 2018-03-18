using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class BusConfig : EntityTypeConfiguration<Bus>
    {
        public BusConfig()
        {
            HasKey(i => i.BusId);

            HasRequired(i => i.Line)
                .WithMany(i=>i.Buses)
                .HasForeignKey(i => i.LineId);

            ToTable("Bus");
        }
    }
}
