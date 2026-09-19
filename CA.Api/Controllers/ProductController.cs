using CA.Application.Features.Products.Commands;
using CA.Application.Features.Products.DTOs;
using CA.Application.Features.Products.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CA.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductsController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<ActionResult<IEnumerable<ProductDto>>> GetAllProducts()
    {
        var products = await mediator.Send(new GetAllProductsQuery());
        return Ok(products);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<ProductDto>> GetProduct(int id)
    {
        // Implement GetById query if needed
        return Ok();
    }

    [HttpPost]
    public async Task<ActionResult<int>> CreateProduct(CreateProductCommand command)
    {
        var id = await mediator.Send(command);
        return CreatedAtAction(nameof(GetProduct), new { id }, id);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateProduct(int id, UpdateProductCommand command)
    {
        if (id != command.Id)
        {
            return BadRequest(new { message = "ID mismatch" });
        }

        try
        {
            await mediator.Send(command);
            return NoContent();
        }
        catch (Application.Common.Exceptions.NotFoundException)
        {
            return NotFound(new { message = $"Product with ID {id} not found" });
        }
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteProduct(int id)
    {
        var command = new DeleteProductCommand { Id = id };
        
        try
        {
            await mediator.Send(command);
            return NoContent();
        }
        catch (Application.Common.Exceptions.NotFoundException)
        {
            return NotFound(new { message = $"Product with ID {id} not found" });
        }
    }
}