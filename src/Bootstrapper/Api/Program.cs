﻿


var builder = WebApplication.CreateBuilder(args);

//builder.Services.AddCarter(configurator: config =>
//{
//    var catalogModules = typeof(CatalogModule).Assembly.GetTypes()
//    .Where(t => t.IsAssignableTo(typeof(ICarterModule))).ToArray();

//    config.WithModules(catalogModules);
//});

builder.Host.UseSerilog((context,config) => 
    config.ReadFrom.Configuration(context.Configuration) 
);

//common services: carter, mediatr, fluentvalidation
var catalogAssembly = typeof(CatalogModule).Assembly;
var basketAssembly = typeof(BasketModule).Assembly;
builder.Services
    .AddCarterWithAssemblies(catalogAssembly, basketAssembly);

builder.Services
    .AddMediatRWithAssemblies(catalogAssembly, basketAssembly);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration)
    .AddBasketModule(builder.Configuration);

builder.Services
    .AddExceptionHandler<CustomExceptionHandler>();



var app = builder.Build();


app.MapCarter();
app.UseSerilogRequestLogging();
app.UseExceptionHandler(options => { });


app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();



app.Run();
 