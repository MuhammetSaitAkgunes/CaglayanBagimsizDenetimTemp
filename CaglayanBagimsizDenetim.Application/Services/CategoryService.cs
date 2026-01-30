using AutoMapper;
using CaglayanBagimsizDenetim.Application.DTOs.CategoryDto;
using CaglayanBagimsizDenetim.Application.Interfaces;
using CaglayanBagimsizDenetim.Application.Interfaces.Repositories;
using CaglayanBagimsizDenetim.Application.Wrappers;
using CaglayanBagimsizDenetim.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaglayanBagimsizDenetim.Application.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICacheService _cache;
        private const string CacheKeyPrefix = "categories";
        private static readonly TimeSpan CacheExpiration = TimeSpan.FromMinutes(5);

        public CategoryService(ICategoryRepository categoryRepository, IUnitOfWork unitOfWork, IMapper mapper, ICacheService cache)
        {
            _categoryRepository = categoryRepository;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _cache = cache;
        }

        public async Task<ServiceResult<List<CategoryDto>>> GetAllCategoriesAsync()
        {
            var cachedCategories = await _cache.GetAsync<List<CategoryDto>>($"{CacheKeyPrefix}:all");
            if (cachedCategories != null)
            {
                return ServiceResult<List<CategoryDto>>.Success(cachedCategories);
            }

            var categories = await _categoryRepository.GetAllAsync();
            var categoryDtos = _mapper.Map<List<CategoryDto>>(categories);

            await _cache.SetAsync($"{CacheKeyPrefix}:all", categoryDtos, CacheExpiration);

            return ServiceResult<List<CategoryDto>>.Success(categoryDtos);
        }

        public async Task<ServiceResult<CategoryDto>> GetCategoryByIdAsync(Guid id)
        {
            var category =  await _categoryRepository.GetByIdAsync(id);

            if (category == null)
            {
                return ServiceResult<CategoryDto>.Failure("Category not found", 404);
            }

            var categoryDto = _mapper.Map<CategoryDto>(category);

            return ServiceResult<CategoryDto>.Success(categoryDto);
        }

        public async Task<ServiceResult<Guid>> CreateCategoryAsync(CreateCategoryDto request)
        {
            var categoryEntity = _mapper.Map<Category>(request);

            await _categoryRepository.AddAsync(categoryEntity);
            await _unitOfWork.SaveChangesAsync();

            await _cache.RemoveByPatternAsync($"{CacheKeyPrefix}:*");

            return ServiceResult<Guid>.Success(categoryEntity.Id, 201);
        }

        public async Task<ServiceResult<Guid>> UpdateCategoryAsync(UpdateCategoryDto request)
        {
            // 1. Entity'i Context üzerinden çek (Tracking Açık)
            var categoryEntity = await _categoryRepository.GetByIdAsync(request.Id);

            if (categoryEntity == null)
            {
                return ServiceResult<Guid>.Failure("Category not found", 404);
            }

            // 2. DOĞRU YÖNTEM: Değişiklikleri mevcut entity üzerine uygula.
            // Domain entity'ndeki metodları kullanarak "Encapsulation"ı koru.
            // Böylece 'UpdatedAt' gibi alanlar otomatik tetiklenir ve Guard kontrolleri çalışır.

            // Eğer sadece property değişimi ise: categoryEntity.Name = request.Name;
            // Ama senin yapında Domain Metodu kullanmak en doğrusu:
            categoryEntity.UpdateName(request.Name);
            categoryEntity.UpdateDescription(request.Description);

            // NOT: AutoMapper kullanmak istersen de şöyle yapmalıydın:
            // _mapper.Map(request, categoryEntity); // (Mevcut instance'ı günceller)
            // Ancak bu durumda Domain metodların (UpdateName) çalışmaz, sadece property set edilir.
            // Senin entity yapın için MANUEL atama daha sağlıklı.

            // 3. UpdateAsync çağırmana gerek bile yok!
            // Entity zaten "Tracked" durumda. SaveChanges dediğinde EF Core
            // entity'nin değiştiğini (Dirty State) algılar ve sadece değişen kolonlar için UPDATE yazar.
            // Yine de Repository pattern gereği UpdateAsync çağırabilirsin (içi boş olsa bile).
            _categoryRepository.UpdateAsync(categoryEntity);

            await _unitOfWork.SaveChangesAsync();

            // 4. Cache Temizliği
            await _cache.RemoveByPatternAsync($"{CacheKeyPrefix}:*");

            return ServiceResult<Guid>.Success(categoryEntity.Id);
        }

        public async Task<ServiceResult<bool>> DeleteCategoryAsync(Guid id)
        {
            await _categoryRepository.DeleteAsync(id);
            await _unitOfWork.SaveChangesAsync();

            await _cache.RemoveByPatternAsync($"{CacheKeyPrefix}:{id}");

            return ServiceResult<bool>.Success(true);
        }
    }
}
