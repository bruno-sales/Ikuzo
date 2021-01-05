using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class LineTagConfig : EntityTypeConfiguration<LineTag>
    {
        public LineTagConfig()
        {
            HasKey(i => i.LineTagId);
            HasRequired(i => i.Line).WithMany(i => i.LineTags).HasForeignKey(p => p.LineId);
            HasRequired(i => i.Tag).WithMany().HasForeignKey(p => p.TagId);

            ToTable("LineTag");
        }
    }
}
