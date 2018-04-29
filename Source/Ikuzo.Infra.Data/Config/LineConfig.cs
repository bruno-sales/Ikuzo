using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class LineConfig : EntityTypeConfiguration<Line>
    {
        public LineConfig()
        {
            HasKey(i => i.LineId);

            ToTable("Line");
        }
    }
}
