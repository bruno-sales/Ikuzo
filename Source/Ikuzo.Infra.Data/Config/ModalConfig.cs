using System.Data.Entity.ModelConfiguration;
using Ikuzo.Domain.Entities;

namespace Ikuzo.Infra.Data.Config
{
    public class ModalConfig : EntityTypeConfiguration<Modal>
    {
        public ModalConfig()
        {
            HasKey(i => i.ModalId);

            HasRequired(i => i.Line)
                .WithMany(i=>i.Modals)
                .HasForeignKey(i => i.LineId); 

            ToTable("Modal");
        }
    }
}
