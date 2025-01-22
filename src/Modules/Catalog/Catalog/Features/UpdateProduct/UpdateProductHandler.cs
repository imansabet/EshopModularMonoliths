


using FluentValidation;

namespace Catalog.Features.UpdateProduct;


public record UpdateProductCommand(ProductDto Product)
    : ICommand<UpdateProductResult>;


public class UpdateProductCommandValidator
    : AbstractValidator<UpdateProductCommand>
{
    public UpdateProductCommandValidator()
    {
        RuleFor(x => x.Product.Id).NotEmpty().WithMessage("Product Id Is Required");
        RuleFor(x => x.Product.Name)
            .NotEmpty().WithMessage("Name Is Required");
          
        RuleFor(x => x.Product.Price).NotEmpty().WithMessage("Price must be greater than 0.");

    }

}


public record UpdateProductResult(bool IsSuccess);

internal class UpdateProductHandler(CatalogDbContext dbContext) : ICommandHandler<UpdateProductCommand, UpdateProductResult>
{
    public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
    {
        var product = await dbContext.Products
            .FindAsync([command.Product.Id], cancellationToken: cancellationToken);
        if(product is null) 
        {
            throw new Exception($"Product Not Found:{command.Product.Id}");
        }
        UpdateProductWithNewValues(product, command.Product);
        
        dbContext.Products.Update(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new UpdateProductResult(true);
    }

    private void UpdateProductWithNewValues(Product product, ProductDto  productDto)
    {
        product.Update
            (
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price
            );
         

    }
}
