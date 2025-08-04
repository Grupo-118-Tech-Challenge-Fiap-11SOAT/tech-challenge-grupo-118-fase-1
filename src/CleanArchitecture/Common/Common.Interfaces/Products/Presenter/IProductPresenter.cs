using Common.Dto.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace Common.Interfaces.Products.Presenter;

public interface IProductPresenter
{
    /// <summary>
    /// Converts a list of <see cref="Product"/> entities to a list of <see cref="ProductDto"/>.
    /// </summary>
    /// <param name="products">The list of product entities to convert.</param>
    /// <returns>A list of product DTOs.</returns>
    List<ProductDto> Convert(List<Product> products);

    /// <summary>
    /// Converts a single <see cref="Product"/> entity to a <see cref="ProductDto"/>.
    /// </summary>
    /// <param name="product">The product entity to convert.</param>
    /// <returns>The corresponding product DTO.</returns>
    ProductDto Convert(Product product);
}