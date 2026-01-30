using AutoMapper;
using CaglayanBagimsizDenetim.Domain.Entities;
using CaglayanBagimsizDenetim.Application.DTOs;
using CaglayanBagimsizDenetim.Application.DTOs.ArticleDto;
using CaglayanBagimsizDenetim.Application.DTOs.CategoryDto;

namespace CaglayanBagimsizDenetim.Application.Mappings
{
    public class GeneralMapping : Profile
    {
        public GeneralMapping()
        {
            // Entity -> DTO (Veritabanından okurken)
            CreateMap<Product, ProductDto>()
                .ReverseMap(); // DTO -> Entity dönüşümü de gerekirse yap.

            // CreateDto -> Entity (Veritabanına yazarken)
            CreateMap<CreateProductDto, Product>();

            // Order mappings
            CreateMap<Order, OrderDto>();
            CreateMap<CreateOrderDto, Order>();

            // Category mappings
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<CreateCategoryDto, Category>();
            CreateMap<UpdateCategoryDto, Category>();

            // Article mappings
            CreateMap<Article, ArticleDto>().ReverseMap();
            CreateMap<CreateArticleDto, Article>();
            CreateMap<UpdateArticleDto, Article>();

        }
    }
}