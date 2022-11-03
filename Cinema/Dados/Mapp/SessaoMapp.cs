using Cinema.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Dados.Mapp
{
    public class SessaoMapp : IEntityTypeConfiguration<SessaoModel>
    {

        public void Configure(EntityTypeBuilder<SessaoModel> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}
