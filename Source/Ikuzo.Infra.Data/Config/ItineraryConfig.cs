using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class ItineraryConfig : EntityTypeConfiguration<Itinerary>
    {
        public ItineraryConfig()
        {
            HasKey(i => i.ItineraryGuid);

            HasRequired(i => i.Line)
                .WithMany(i=>i.Itineraries)
                .HasForeignKey(i => i.LineId);

            ToTable("Itinerary");
        }
    }
}
