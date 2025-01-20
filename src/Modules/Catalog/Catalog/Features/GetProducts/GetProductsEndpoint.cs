
using Catalog.Features.CreateProduct;

namespace Catalog.Features.GetProducts;

//public record GetProductsRequest();

public record GetProductsResponse(IEnumerable<ProductDto> Products);


public class GetProductsEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/products", async ( ISender sender) =>
        {
            var result = await sender.Send(new GetProductsQuery());

            var response = result.Adapt<GetProductsResponse>();
            return  Results.Ok(response);
        })
        .WithName("Get Products")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Products")
        .WithDescription("Get Products");
    }
}
