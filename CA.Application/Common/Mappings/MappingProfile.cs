using AutoMapper;
using CA.Application.Features.Products.Commands;
using CA.Application.Features.Products.DTOs;
using CA.Domain.Entities;

namespace CA.Application.Common.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, ProductDto>();
        CreateMap<CreateProductCommand, Product>();
        CreateMap<UpdateProductCommand, Product>();
    }
}