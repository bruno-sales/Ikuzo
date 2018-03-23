using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class GpsConfig : EntityTypeConfiguration<Gps>
    {
        public GpsConfig()
        {
            HasKey(i => i.GpsId);
            ToTable("Gps");
        }
    }
}
