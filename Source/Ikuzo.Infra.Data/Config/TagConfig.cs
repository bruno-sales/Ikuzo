using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class TagConfig : EntityTypeConfiguration<Tag>
    {
        public TagConfig()
        {
            HasKey(i => i.TagId); 
            ToTable("Tag");
        }
    }
}
