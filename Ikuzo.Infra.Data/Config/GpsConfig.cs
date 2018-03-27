using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class GpsConfig : EntityTypeConfiguration<Gps>
    {
        public GpsConfig()
        {
            HasKey(i => i.GpsGuid);

            HasRequired(i => i.Line)
                .WithMany()
                .HasForeignKey(i => i.LineId);

            HasRequired(i => i.Bus)
                .WithMany()
                .HasForeignKey(i => i.BusId);

            ToTable("Gps");
        }
    }
}
