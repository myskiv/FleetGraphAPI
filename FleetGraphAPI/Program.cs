using FleetGraphAPI.Database;
using FleetGraphAPI.DataLoader;
using FleetGraphAPI.GraphQL.Alerts;
using FleetGraphAPI.GraphQL.Vehicles;
using HotChocolate.AspNetCore;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCors(o =>
        o.AddDefaultPolicy(b =>
            b.AllowAnyHeader()
                .AllowAnyMethod()
                .AllowAnyOrigin()))
    .AddPooledDbContextFactory<ApplicationDbContext>(
        (s, o) => o
            .UseSqlite("Data Source=fleet.db")
            .UseLoggerFactory(s.GetRequiredService<ILoggerFactory>()))

    .AddGraphQLServer()

    .AddQueryType()
    .AddMutationType()
    .AddSubscriptionType()
    .AddFiltering()
    .AddSorting()
    .AddInMemorySubscriptions()

    .AddTypeExtension<VehicleQueries>()
    .AddTypeExtension<VehicleMutations>()
    .AddTypeExtension<VehicleSubscriptions>()
    //.AddTypeExtension<VehicleNode>()
    .AddDataLoader<VehicleByIdDataLoader>();

var app = builder.Build();

app.UseCors();
app.UseWebSockets();
app.UseRouting();

app.MapGraphQL();
await app.RunAsync();