using Microsoft.EntityFrameworkCore;
using Simple.TestFin.API.Domain.Entities;

namespace Simple.TestFin.API.Infrastructure.Database.SqlServer;

public class TestFinDbContext : DbContext
{
    public DbSet<CodeValue> CodeValues { get; set; }
    
    public DbSet<RequestLog> RequestLogs { get; set; }
    
    public TestFinDbContext() 
    {
    }

    public TestFinDbContext(DbContextOptions<TestFinDbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder builder)  
    {  
        base.OnModelCreating(builder);  
        builder.ApplyConfigurationsFromAssembly(typeof(TestFinDbContext).Assembly);  
    }
}