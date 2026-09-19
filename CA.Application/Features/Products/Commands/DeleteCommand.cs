using MediatR;

namespace CA.Application.Features.Products.Commands;

public class DeleteProductCommand : IRequest<Unit>
{
    public int Id { get; set; }
}