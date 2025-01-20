﻿using Carter;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCarter(configurator: config =>
{
    var catalogModules = typeof(CatalogModule).Assembly.GetTypes()
    .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

    config.WithModules(catalogModules);
});


builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration)
    .AddBasketModule(builder.Configuration);


var app = builder.Build();


app.MapCarter();

app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();




app.Run();
 