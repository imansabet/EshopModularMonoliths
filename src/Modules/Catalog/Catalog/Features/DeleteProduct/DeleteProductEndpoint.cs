
using Catalog.Features.CreateProduct;

namespace Catalog.Features.DeleteProduct;

//public record DeleteProductRequest(Guid Id);
//since the only required information is the Id of Product to be deleted which is obtain directly from url paramteres
public record DeleteProductResponse(bool IsSuccess);



public class DeleteProductEndpoint : ICarterModule
{
    public void AddRoutes(IEndpointRouteBuilder app)
    {
        app.MapDelete("/products/{id}", async (Guid id, ISender sender) =>
        {
            var result = await sender.Send(new DeleteProductCommand(id));
            var response = result.Adapt<DeleteProductResponse>();
            return Results.Ok(response);
        })
        .WithName("DeleteProduct")
        .Produces<CreateProductResponse>(StatusCodes.Status200OK)
        .ProducesProblem(StatusCodes.Status400BadRequest)
        .WithSummary("Delete Product")
        .WithDescription("Delete Product");
    }
}
