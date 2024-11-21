using AutoMapper;
using Common.Models;
using Data.MongoDb.Entities;

namespace Data.MongoDb.Mapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, ProductDto>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => new MongoDB.Bson.ObjectId(src.Id)))
                .ForMember(dest => dest.CategoryNames, opt => opt.MapFrom(src => src.Categories))
                .ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags));

            CreateMap<ProductDto, Product>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id.ToString()))
                .ForMember(dest => dest.Categories, opt => opt.MapFrom(src => src.CategoryNames))
            /*.ForMember(dest => dest.Tags, opt => opt.MapFrom(src => src.Tags))*/
            ;
            //CreateMap<ProductCategories, List<ProductCategory>>()
            //    .ForMember(dest => dest.)
        }
    }
}
