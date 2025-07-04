using Common.Dto.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace Common.Interfaces.Products.Presenter;

public interface IImageProductPresenter
{
    List<ImageProductDto> Convert(List<ImageProduct> products);
    ProductDto Convert(Product product);
}