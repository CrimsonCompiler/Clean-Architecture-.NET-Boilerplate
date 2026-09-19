using AutoMapper;
using CA.Application.Features.Products.DTOs;
using CA.Domain.Entities;
using CA.Domain.Interfaces;
using MediatR;

namespace CA.Application.Features.Products.Queries;

public class GetAllProductsQueryHandler : IRequestHandler<GetAllProductsQuery, IEnumerable<ProductDto>>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public GetAllProductsQueryHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IEnumerable<ProductDto>> Handle(GetAllProductsQuery request, CancellationToken cancellationToken)
    {
        var products = await _repository.GetAllAsync();
        return _mapper.Map<IEnumerable<ProductDto>>(products);
    }
}