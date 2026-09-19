using AutoMapper;
using CA.Domain.Entities;
using CA.Domain.Interfaces;
using MediatR;

namespace CA.Application.Features.Products.Commands;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, int>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public CreateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<int> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        var product = _mapper.Map<Product>(request);
        await _repository.AddAsync(product);
        return product.Id;
    }
}