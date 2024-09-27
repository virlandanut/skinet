using Core.Entities;

namespace Core.Specifications;

public class ProductSpecification : BaseSpecification<Product>
{
    public ProductSpecification(ProductSpecificationParams specificationParams)
        : base(product =>
            (
                string.IsNullOrEmpty(specificationParams.Search)
                || product.Name.ToLower().Contains(specificationParams.Search)
            )
            && (
                specificationParams.Brands.Count == 0
                || specificationParams.Brands.Contains(product.Brand)
            )
            && (
                specificationParams.Types.Count == 0
                || specificationParams.Types.Contains(product.Type)
            )
        )
    {
        ApplyPaging(
            specificationParams.PageSize * (specificationParams.PageIndex - 1),
            specificationParams.PageSize
        );
        switch (specificationParams.Sort)
        {
            case "priceAsc":
                AddOrderBy(product => product.Price);
                break;
            case "priceDesc":
                AddOrderByDesc(product => product.Price);
                break;
            default:
                AddOrderBy(product => product.Name);
                break;
        }
    }
}
