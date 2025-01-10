var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddCatalogModule(builder.Configuration)
    .AddOrderingModule(builder.Configuration)
    .AddBasketModule(builder.Configuration);


var app = builder.Build();

app.MapGet("/", () => "Hello World!");


app
    .UseCatalogModule()
    .UseBasketModule()
    .UseOrderingModule();




app.Run();
 