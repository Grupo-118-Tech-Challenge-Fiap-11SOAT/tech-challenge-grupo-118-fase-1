using Common.Dto.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace Common.Interfaces.Products.Presenter;

public interface IProductPresenter
{
    List<ProductDto> Convert(List<Product> products);
    ProductDto Convert(Product product);
}