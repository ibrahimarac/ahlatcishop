using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly AhlatciContext _context;
        public CategoryService(AhlatciContext context)
        {
            _context = context;
        }

        //Ctrl + M + O

        public async Task<List<CategoryDto>> GetAllCategories()
        {
            var categories = await _context.Categories //IEnumerable<Category>
                 .Select(x => new CategoryDto //Category => CategoryDto dönüşümü yapılıyor
                 {
                     Id = x.Id,
                     Name = x.Name
                 }).ToListAsync();

            return categories;
        }

        public async Task<CategoryDto> GetCategoryById(int id)
        {
            var categoryEntity = await _context.Categories.FindAsync(id);
            var categoryDto = new CategoryDto
            {
                Id = id,
                Name = categoryEntity.Name
            };

            return categoryDto;
        }

        public async Task<int> CreateCategory(CreateCategoryVM createCategoryVM)
        {
            //Yeni bir kategori entity nesnesi
            var categoryEntity = new Category
            {
                Name = createCategoryVM.CategoryName
            };
            //Üretilen entity kategori koleksiyonuna ekleniyor
            await _context.Categories.AddAsync(categoryEntity);
            await _context.SaveChangesAsync();
            //Db kayıt işleminden sonra herhangi bir sıkıntı yoksa bu kategori için atanan entity geri döner.
            return categoryEntity.Id;
        }

        public async Task<int> DeleteCategory(int id)
        {
            //Gönderilen id bilgisine karşılık gelen bir kategori var mı?
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == id);
            if (!categoryExists)
            {
                throw new Exception($"{id} numaralı kategori bulunamadı.");
            }

            //Veritabanında kayıtlı kategoriyi getirelim.
            var existsCategory = await _context.Categories.FindAsync(id);
            //Silindi olarak işaretleyelim.
            existsCategory.IsDeleted = true;
            //Güncellemeyi veritabanına yansıtalım.
            _context.Categories.Update(existsCategory);
            await _context.SaveChangesAsync();

            return existsCategory.Id;
        }

        public async Task<int> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            //Gönderilen id bilgisine karşılık gelen bir kategori var mı?
            var categoryExists = await _context.Categories.AnyAsync(x => x.Id == updateCategoryVM.Id);
            if (!categoryExists)
            {
                throw new Exception($"{updateCategoryVM} numaralı kategori bulunamadı.");
            }

            //Veritabanında kayıtlı kategoriyi getirelim.
            var existsCategory = await _context.Categories.FindAsync(updateCategoryVM.Id);
            //Silindi olarak işaretleyelim.
            existsCategory.Name = updateCategoryVM.CategoryName;
            //Güncellemeyi veritabanına yansıtalım.
            _context.Categories.Update(existsCategory);
            await _context.SaveChangesAsync();

            return existsCategory.Id;
        }

    }
}
