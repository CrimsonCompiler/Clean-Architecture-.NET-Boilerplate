using CA.Application.Common.Exceptions;
using CA.Domain.Entities;
using CA.Domain.Interfaces;
using MediatR;

namespace CA.Application.Features.Products.Commands;

public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, Unit>
{
    private readonly IRepository<Product> _repository;

    public DeleteProductCommandHandler(IRepository<Product> repository)
    {
        _repository = repository;
    }

    public async Task<Unit> Handle(DeleteProductCommand request, CancellationToken cancellationToken)
    {
        var product = await _repository.GetByIdAsync(request.Id);
        
        if (product == null)
        {
            throw new NotFoundException($"Product with ID {request.Id} not found.");
        }

        await _repository.DeleteAsync(product);

        return Unit.Value;
    }
}