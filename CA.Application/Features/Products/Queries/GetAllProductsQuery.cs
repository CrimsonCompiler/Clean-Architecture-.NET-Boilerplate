using CA.Application.Features.Products.DTOs;
using MediatR;

namespace CA.Application.Features.Products.Queries;

public class GetAllProductsQuery : IRequest<IEnumerable<ProductDto>>
{
}