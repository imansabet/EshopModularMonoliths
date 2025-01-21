using FluentValidation;

namespace Catalog.Features.CreateProduct;

public record CreateProductCommand
    (ProductDto  Product) 
    : ICommand<CreateProductResult>;

public record CreateProductResult(Guid Id);

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand> 
{
    public CreateProductCommandValidator()
    {
        RuleFor(x => x.Product.Name).NotEmpty().WithMessage("Name is required.");
        RuleFor(x => x.Product.Category).NotEmpty().WithMessage("Category is required.");
        RuleFor(x => x.Product.ImageFile).NotEmpty().WithMessage("ImageFile is required.");
        RuleFor(x => x.Product.Price).GreaterThan(0).WithMessage("Price Must be Greater than 0 .");
    }
} 



public class CreateProductHandler
    (IValidator<CreateProductCommand> validator , ILogger<CreateProductHandler> logger
    ,CatalogDbContext dbContext) 
    : ICommandHandler<CreateProductCommand, CreateProductResult>
{
    public async Task<CreateProductResult> Handle(CreateProductCommand command, CancellationToken cancellationToken)
    {
        var result = await validator.ValidateAsync(command, cancellationToken);
        var errors = result.Errors.Select(x => x.ErrorMessage).ToList();
        if (errors.Any()) 
        {
            throw new ValidationException(errors.FirstOrDefault());
        }

        logger.LogInformation("CreateProductCommandHandler.Handle Called with {@Command}",command);

        //actual logic
        var product = CreateNewProduct(command.Product);

        dbContext.Products.Add(product);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new CreateProductResult(product.Id); 
    }

    private Product CreateNewProduct(ProductDto productDto)
    {
        var product = Product.Create(
                Guid.NewGuid(),
                productDto.Name,
                productDto.Category,
                productDto.Description,
                productDto.ImageFile,
                productDto.Price
            );
        return product;
    }
}
 