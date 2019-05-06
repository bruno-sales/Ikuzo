using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class TagConfig : EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            HasKey(i => i.TagId);

            HasRequired(i => i.Line)
                .WithMany(i => i.Tags)
                .HasForeignKey(i => i.LineId);

            ToTable("Tag");
        }
    }
}
