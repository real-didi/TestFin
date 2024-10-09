using FluentValidation;
using Microsoft.EntityFrameworkCore;
using Simple.TestFin.API.Application.Services;
using Simple.TestFin.API.Application.Services.Interfaces;
using Simple.TestFin.API.Application.Validators;
using Simple.TestFin.API.Domain.Entities;
using Simple.TestFin.API.Domain.Repositories;
using Simple.TestFin.API.Filters;
using Simple.TestFin.API.Infrastructure.Database.SqlServer;
using Simple.TestFin.API.Infrastructure.Database.SqlServer.Repositories;
using Simple.TestFin.API.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddTransient<IDateTimeProvider, DateTimeProvider>();

builder.Services.AddTransient<ICodeValueRepository, CodeValueRepository>();
builder.Services.AddTransient<ICodeValueService, CodeValueService>();

builder.Services.AddTransient<IRequestLoggingRepository, RequestLoggingRepository>();
builder.Services.AddTransient<IRequestLoggingService, RequestLoggingService>();

builder.Services.AddTransient<IValidator<IEnumerable<CodeValue>>, CodeValueCollectionValidator>();

builder.Services.AddDbContext<TestFinDbContext>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("TestFinSql"));
});

builder.Services.AddControllers(options =>
{
    options.Filters.Add(new GlobalExceptionFilter());
});


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<RequestLoggingMiddleware>();

// Migrate DB
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<TestFinDbContext>();
    db.Database.Migrate();
}

app.Run();