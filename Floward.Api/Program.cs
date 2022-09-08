using Floward.Application.Commands.OrderCommands;
using Floward.Application.Commands.ProductCommands;
using Floward.Application.Commands.UserCommands;
using Floward.Broker;
using Floward.Broker.Config;
using Floward.Domain.Interfaces.IRepositories;
using Floward.Domain.Interfaces.IRepositories.Base;
using Floward.Domain.Interfaces.MessageBroker;
using Floward.Infrastructure.Data;
using Floward.Infrastructure.Services.Repositories;
using Floward.Infrastructure.Services.Repositories.Base;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());
builder.Services.AddMediatR(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddMediatR(typeof(CreateProductCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(CreateUserCommandHandler).Assembly);
builder.Services.AddMediatR(typeof(AddToCartCommandHandler).Assembly);
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Floward.API",
        Version = "v1"
    });
});
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IProductRepository, ProductRepository>();
builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IBrokerService, BrokerService>();
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

app.Run();
