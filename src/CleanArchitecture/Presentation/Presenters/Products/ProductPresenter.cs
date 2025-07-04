using Common.Dto.Products;
using Common.Interfaces.Products.Presenter;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Products;

public class ProductPresenter : IProductPresenter
{
    public List<ProductDto> Convert(List<Product> products)
    {
        var productDtos = new List<ProductDto>();

        products.ForEach(p => productDtos.Add(Convert(p)));

        return productDtos;
    }

    public ProductDto Convert(Product product)
    {
        return new ProductDto(product.Name,
            product.Description,
            product.Category,
            product.Price,
            product.IsActive,
            product.Id);
    }
}