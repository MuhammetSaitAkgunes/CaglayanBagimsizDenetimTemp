using FluentValidation;
using Microsoft.Extensions.DependencyInjection;
using CaglayanBagimsizDenetim.Application.Interfaces;
using CaglayanBagimsizDenetim.Application.Services;
using System.Reflection;

namespace CaglayanBagimsizDenetim.Application
{
    public static class ServiceRegistration
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            // Bu Assembly içindeki (Application katmanı) tüm AutoMapper profillerini bul ve kaydet.
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddValidatorsFromAssembly(Assembly.GetExecutingAssembly());

            // --- APPLICATION SERVICES ---
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<IOrderService, OrderService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IArticleService, ArticleService>();

            return services;
        }
    }
}