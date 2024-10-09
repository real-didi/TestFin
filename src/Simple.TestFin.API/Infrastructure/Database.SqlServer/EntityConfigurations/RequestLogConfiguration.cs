using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Simple.TestFin.API.Domain.Entities;

namespace Simple.TestFin.API.Infrastructure.Database.SqlServer.EntityConfigurations;

public class RequestLogConfiguration : IEntityTypeConfiguration<RequestLog>
{
    public void Configure(EntityTypeBuilder<RequestLog> builder)
    {
        builder.Property(q => q.Method)
            .HasMaxLength(8)
            .IsRequired();
        
        builder.Property(q => q.Path)
            .HasMaxLength(1024)
            .IsRequired();
    }
}