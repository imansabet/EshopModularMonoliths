
using Catalog.Features.CreateProduct;
using Catalog.Features.GetProducts;

namespace Catalog.Features.GetProductById;

//public record GetProductByIdRequest();
public record GetProductByIdResponse(ProductDto Product);


public class GetProductByIdEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapGet("/product/{id}",async (Guid id,ISender sender) => 
        {
            var result = await sender.Send(new GetProductByIdQuery(id));

            var response = result.Adapt<GetProductByIdResponse>();
            return Results.Ok(response);
        })
        .WithName("GetProductById")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Get Product By Id")
        .WithDescription("Get Product By Id");
    }
}
