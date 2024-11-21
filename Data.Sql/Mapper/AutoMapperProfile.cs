using AutoMapper;
using Common.Models;
using Data.Sql.Entities;

namespace Data.Sql.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
                //.IncludeMembers(a => a.Categories)
                //.Include<ProductCategory, ProductCategoryDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => Guid.Parse(src.Id!)))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(t => new CategoryDto { Name = t })))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => string.Join(",", src.Tags)));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.Categories.Select(t => t.Name)))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags.Split(',', StringSplitOptions.RemoveEmptyEntries).ToList()));

            CreateMap<ProductCategory, CategoryDto>(/*MemberList.Destination*/)
                //.AfterMap((model, entity) =>
                //{
                //    foreach (var product in entity.Product)
                //    {
                //        product.Categories = entity;
                //    }
                //})
                ;

            CreateMap<ProductCategoryDto, ProductCategory>();
        }
    }
}
