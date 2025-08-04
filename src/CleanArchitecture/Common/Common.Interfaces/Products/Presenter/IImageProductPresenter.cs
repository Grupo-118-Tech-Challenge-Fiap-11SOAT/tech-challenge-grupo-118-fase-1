using Common.Dto.Products;
using TechChallengeFastFood.CleanArch.Domain.Entities.Products.Entities;

namespace Common.Interfaces.Products.Presenter;

public interface IImageProductPresenter
{
    /// <summary>
    /// Converts a list of <see cref="ImageProduct"/> entities to a list of <see cref="ImageProductDto"/>.
    /// </summary>
    /// <param name="imageProducts">The list of image product entities to convert.</param>
    /// <returns>A list of image product DTOs.</returns>
    List<ImageProductDto> Convert(List<ImageProduct> imageProducts);

    /// <summary>
    /// Converts a single <see cref="ImageProduct"/> entity to a <see cref="ImageProductDto"/>.
    /// </summary>
    /// <param name="imageProduct">The image product entity to convert.</param>
    /// <returns>The corresponding image product DTO.</returns>
    ImageProductDto Convert(ImageProduct imageProduct);
}