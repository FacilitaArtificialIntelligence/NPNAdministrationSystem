using AdministrationSystem.Api;
using AdministrationSystem.Application;
using AdministrationSystem.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddInfrastructure(builder.Configuration)
        .AddApplication();
}

var app = builder.Build();
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }
    
    app.UseCors("AllowAll");
    app.UseHttpsRedirection();
    app.MapControllers();
    app.UseExceptionHandler();
}

app.Run();
