using API.DTOs;
using AutoMapper;
using Core.Entities;

namespace API.Helpers
{
    public class MappingProfiles : Profile
    {
        public MappingProfiles()
        {
            CreateMap<Product, ProductToReturnDTO>()
                .ForMember(d => d.ProductBrand, o => o.MapFrom(s => s.ProductBrand.Name)) // mapping to get flat products with brand name, not brand class
                .ForMember(d => d.ProductType, o => o.MapFrom(s => s.ProductType.Name)) // same here
                .ForMember(d => d.PictureUrl, o => o.MapFrom<ProductsUrlResolver>()); // mapping to return full url to see image
        }
    }
}
