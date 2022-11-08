using DawnPoets.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DawnPoets.Map
{
    public class ContatoMap : IEntityTypeConfiguration<ContatoModel>
    {
        public void Configure(EntityTypeBuilder<ContatoModel> builder)
        {
            builder.HasKey(x => x.Id);

            builder.HasOne(x => x.User);
        }
    }
}
