using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sozluk.Api.Domain.Models;

namespace Sozluk.Infrastructure.Persistence.EntityConfiguration;

public abstract class BaseEntityConfiguration<T>:IEntityTypeConfiguration<T> where T:BaseEntity
{
    public virtual void Configure(EntityTypeBuilder<T> builder)
    {
        builder.HasKey(i => i.Id);//Primary Key
        builder.Property(i => i.Id).ValueGeneratedOnAdd();//auto generate
        builder.Property(i => i.CreateDate).ValueGeneratedOnAdd();
    }
}