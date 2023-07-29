using Ahlatci.Shop.Application.Exceptions;
using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Validators.Categories;
using Ahlatci.Shop.Application.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Persistence.Context;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using Ahlatci.Shop.Application.Behaviors;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly AhlatciContext _context;
        private readonly IMapper _mapper;

        public CategoryService(AhlatciContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [PerformanceBehavior]
        public async Task<Result<List<CategoryDto>>> GetAllCategories()
        {
            var result = new Result<List<CategoryDto>>();

            var categoryDtos = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .ToListAsync();
            result.Data = categoryDtos;

            return result;
        }


        [ValidationBehavior(typeof(GetCategoryByIdValidator))]
        public async Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM)
        {
            var result = new Result<CategoryDto>();

            var categoryExists = await _context.Categories.AnyAsync(x=>x.Id == getCategoryByIdVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{getCategoryByIdVM.Id} numaralı kategori bulunamadı.");
            }

            var categoryDto = await _context.Categories
                .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
                .FirstOrDefaultAsync(x => x.Id == getCategoryByIdVM.Id);

            result.Data = categoryDto;
            return result;
        }


        [ValidationBehavior(typeof(CreateCategoryValidator))]
        public async Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM)
        {            
            var result = new Result<int>();

            var categoryEntity = _mapper.Map<CreateCategoryVM, Category>(createCategoryVM);

            //Üretilen entity kategori koleksiyonuna ekleniyor
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
            //Db kayıt işleminden sonra herhangi bir sıkıntı yoksa bu kategori için atanan entity geri döner.
            result.Data =  categoryEntity.Id;
            return result;
        }


        [ValidationBehavior(typeof(DeleteCategoryValidator))]
        public async Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM)
        {
            var result = new Result<int>();

            //Gönderilen id bilgisine karşılık gelen bir kategori var mı?
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == deleteCategoryVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{deleteCategoryVM.Id} numaralı kategori bulunamadı.");
            }

            //Veritabanında kayıtlı kategoriyi getirelim.
            var existsCategory = await _context.Categories.FindAsync(deleteCategoryVM.Id);
            //Silindi olarak işaretleyelim.
            existsCategory.IsDeleted = true;
            //Güncellemeyi veritabanına yansıtalım.
            _context.Categories.Update(existsCategory);
            await _context.SaveChangesAsync();

            result.Data = existsCategory.Id;
            return result;
        }


        [ValidationBehavior(typeof(UpdateCategoryValidator))]
        public async Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            var result = new Result<int>();

            //Gönderilen id bilgisine karşılık gelen bir kategori var mı?
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == updateCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{updateCategoryVM} numaralı kategori bulunamadı.");
            }

            var updatedCategory = _mapper.Map<UpdateCategoryVM, Category>(updateCategoryVM);

            ////Veritabanında kayıtlı kategoriyi getirelim.
            //var existsCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);
            ////Silindi olarak işaretleyelim.
            //existsCategory.Name = updateCategoryVM.CategoryName;

            //Güncellemeyi veritabanına yansıtalım.
            _context.Categories.Update(updatedCategory);
            await _context.SaveChangesAsync();

            result.Data = updatedCategory.Id;
            return result;
        }

    }
}
