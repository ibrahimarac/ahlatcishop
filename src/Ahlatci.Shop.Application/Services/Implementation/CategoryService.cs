using Ahlatci.Shop.Application.Behaviors;
using Ahlatci.Shop.Application.Exceptions;
using Ahlatci.Shop.Application.Models.Dtos;
using Ahlatci.Shop.Application.Models.RequestModels;
using Ahlatci.Shop.Application.Repositories;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Validators.Categories;
using Ahlatci.Shop.Application.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using AutoMapper;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class CategoryService : ICategoryService
    {
        private readonly IMapper _mapper;
        private readonly IRepository<Category> _repository;

        public CategoryService(IMapper mapper, IRepository<Category> repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [PerformanceBehavior]
        public async Task<Result<List<CategoryDto>>> GetAllCategories()
        {
            var result = new Result<List<CategoryDto>>();

            //Repository katmanı veriyi Entity türünden getirir.
            //Ama bizim bu metoddan döndürmemiz gereken dto tipidir.
            var categoryEntites= await _repository.GetAllAsync();
            //Automapper aşağıdaki gibi kullanıldığında bir sonucu başka bir sonuca çevirebilir.
            //_mapper.Map<T,K>(source) source T tipindendir. T tipinden veri K tipinden veriye çevrilir.
            var categoryDtos = _mapper.Map<List<Category>, List<CategoryDto>>(categoryEntites);
            //var categoryDtos = await _context.Categories
            //    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            //    .ToListAsync();
            result.Data = categoryDtos;

            return result;
        }


        [ValidationBehavior(typeof(GetCategoryByIdValidator))]
        public async Task<Result<CategoryDto>> GetCategoryById(GetCategoryByIdVM getCategoryByIdVM)
        {
            var result = new Result<CategoryDto>();

            //var categoryExists = await _context.Categories.AnyAsync(x=>x.Id == getCategoryByIdVM.Id);
            var categoryExists = await _repository.AnyAsync(x => x.Id == getCategoryByIdVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{getCategoryByIdVM.Id} numaralı kategori bulunamadı.");
            }

            //var categoryDto = await _context.Categories
            //    .ProjectTo<CategoryDto>(_mapper.ConfigurationProvider)
            //    .FirstOrDefaultAsync(x => x.Id == getCategoryByIdVM.Id);

            var categoryEntity = await _repository.GetById(getCategoryByIdVM.Id);

            var categoryDto = _mapper.Map<Category, CategoryDto>(categoryEntity);

            result.Data = categoryDto;
            return result;
        }


        [ValidationBehavior(typeof(CreateCategoryValidator))]
        public async Task<Result<int>> CreateCategory(CreateCategoryVM createCategoryVM)
        {            
            var result = new Result<int>();

            var categoryEntity = _mapper.Map<CreateCategoryVM, Category>(createCategoryVM);

            //await _context.Categories.AddAsync(categoryEntity);
            //await _context.SaveChangesAsync();

            await _repository.Add(categoryEntity);

            result.Data =  categoryEntity.Id;
            return result;
        }


        [ValidationBehavior(typeof(DeleteCategoryValidator))]
        public async Task<Result<int>> DeleteCategory(DeleteCategoryVM deleteCategoryVM)
        {
            var result = new Result<int>();

            //Gönderilen id bilgisine karşılık gelen bir kategori var mı?
            //var categoryExists = await _context.Categories.AnyAsync(x => x.Id == deleteCategoryVM.Id);
            var categoryExists = await _repository.AnyAsync(x => x.Id == deleteCategoryVM.Id);
            if (!categoryExists)
            {
                throw new NotFoundException($"{deleteCategoryVM.Id} numaralı kategori bulunamadı.");
            }

            //Veritabanında kayıtlı kategoriyi getirelim.
            //var existsCategory = await _context.Categories.FindAsync(deleteCategoryVM.Id);
            await _repository.Delete(deleteCategoryVM.Id);
            //existsCategory.IsDeleted = true;
            //_context.Categories.Update(existsCategory);
            //await _context.SaveChangesAsync();

            result.Data = deleteCategoryVM.Id;
            return result;
        }


        [ValidationBehavior(typeof(UpdateCategoryValidator))]
        public async Task<Result<int>> UpdateCategory(UpdateCategoryVM updateCategoryVM)
        {
            var result = new Result<int>();

            //Gönderilen id bilgisine karşılık gelen bir kategori var mı?
            //var categoryExists = await _context.Categories.AnyAsync(x => x.Id == updateCategoryVM.Id);
            var categoryExists = await _repository.AnyAsync(x => x.Id == updateCategoryVM.Id);
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
            //_context.Categories.Update(updatedCategory);
            //await _context.SaveChangesAsync();

            await _repository.Update(updatedCategory);

            result.Data = updatedCategory.Id;
            return result;
        }

    }
}
