using System.Data.Entity.ModelConfiguration; 
using Ikuzo.Domain.Histories;

namespace Ikuzo.Infra.Data.Config
{
    public class GpsHistoryConfig : EntityTypeConfiguration<GpsHistory>
    {
        public GpsHistoryConfig()
        {
            HasKey(i => i.GpsHistoryId); 

            ToTable("GpsHistory");
        }
    }
}
