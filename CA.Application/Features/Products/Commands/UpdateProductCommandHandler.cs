using AutoMapper;
using CA.Application.Common.Exceptions;
using CA.Domain.Entities;
using CA.Domain.Interfaces;
using MediatR;

namespace CA.Application.Features.Products.Commands;

public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, Unit>
{
    private readonly IRepository<Product> _repository;
    private readonly IMapper _mapper;

    public UpdateProductCommandHandler(IRepository<Product> repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
    {
        var existingProduct = await _repository.GetByIdAsync(request.Id);
        
        if (existingProduct == null)
        {
            throw new NotFoundException($"Product with ID {request.Id} not found.");
        }

        // Update properties
        existingProduct.Name = request.Name;
        existingProduct.Description = request.Description;
        existingProduct.Price = request.Price;
        existingProduct.StockQuantity = request.StockQuantity;
        // UpdatedAt automatic changes save at DbContext to SaveChangesAsync

        await _repository.UpdateAsync(existingProduct);

        return Unit.Value;
    }
}