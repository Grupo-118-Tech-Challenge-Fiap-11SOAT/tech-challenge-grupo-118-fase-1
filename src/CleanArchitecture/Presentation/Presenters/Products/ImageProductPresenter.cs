using Common.Dto.Products;
using Common.Interfaces.Products.Presenter;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace TechChallengeFastFood.CleanArch.Presentation.Presenters.Products;

public class ImageProductPresenter : IImageProductPresenter
{
    public List<ImageProductDto> Convert(List<ImageProduct> imageProducts)
    {
        var imageProductDtos = new List<ImageProductDto>();

        imageProducts.ForEach(ip => imageProductDtos.Add(Convert(ip)));
        return imageProductDtos;
    }

    public ImageProductDto Convert(ImageProduct imageProduct)
    {
        return new ImageProductDto(
            imageProduct.Id,
            imageProduct.Position,
            imageProduct.Url);
    }
}