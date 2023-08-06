using Ahlatci.Shop.Application.Exceptions;
using Ahlatci.Shop.Application.Models.Dtos.Cities;
using Ahlatci.Shop.Application.Models.RequestModels.Cities;
using Ahlatci.Shop.Application.Services.Abstraction;
using Ahlatci.Shop.Application.Wrapper;
using Ahlatci.Shop.Domain.Entities;
using Ahlatci.Shop.Domain.UWork;
using AutoMapper;
using AutoMapper.QueryableExtensions;

namespace Ahlatci.Shop.Application.Services.Implementation
{
    public class CityService : ICityService
    {
        private readonly IMapper _mapper;
        private readonly IUnitWork _uwork;

        public CityService(IMapper mapper, IUnitWork uwork)
        {
            _mapper = mapper;
            _uwork = uwork;
        }

        public async Task<Result<List<CityDto>>> GetAllCities()
        {
            var result = new Result<List<CityDto>>();

            var cityEntites = await _uwork.GetRepository<City>().GetAllAsync();
            var cityDtos = cityEntites.ProjectTo<CityDto>(_mapper.ConfigurationProvider).ToList();

            result.Data = cityDtos;
            return result;
        }

        public async Task<Result<CityDto>> GetCityById(int id)
        {
            var result = new Result<CityDto>();

            var cityEntity = await _uwork.GetRepository<City>().GetById(id);

            var cityDto = _mapper.Map<CityDto>(cityEntity);

            result.Data = cityDto;
            return result;
        }

        public async Task<Result<int>> CreateCity(CreateCityVM createCityVM)
        {
            var result = new Result<int>();

            var cityNameExists = await _uwork.GetRepository<City>().AnyAsync(x => x.Name == createCityVM.Name.ToUpper().Trim());
            if (cityNameExists)
            {
                throw new AlreadyExistsException($"{createCityVM.Name} isminde bir şehir eklenmiştir.");
            }

            var cityEntity = _mapper.Map<City>(createCityVM);

            _uwork.GetRepository<City>().Add(cityEntity);
            await _uwork.CommitAsync();

            result.Data = cityEntity.Id;
            return result;
        }

        public async Task<Result<bool>> DeleteCity(int id)
        {
            var result = new Result<bool>();

            var cityById = await _uwork.GetRepository<City>().GetById(id);
            if(cityById is null)
            {
                throw new NotFoundException($"{id} numaralı şehir bulunamadı.");
            }

            _uwork.GetRepository<City>().Delete(cityById);
            result.Data = await _uwork.CommitAsync();

            return result;
        }

        public async Task<Result<bool>> UpdateCity(UpdateCityVM updateCityVM)
        {
            var result = new Result<bool>();

            var cityIdExists = await _uwork.GetRepository<City>().AnyAsync(x=>x.Id == updateCityVM.Id);
            if (!cityIdExists)
            {
                throw new NotFoundException($"{updateCityVM.Id} numaralı şehir bulunamadı.");
            }

            var cityNameExists = await _uwork.GetRepository<City>().AnyAsync(x => x.Id != updateCityVM.Id && x.Name == updateCityVM.Name.ToUpper().Trim());
            if (cityNameExists)
            {
                throw new AlreadyExistsException($"{updateCityVM.Name} isminde bir şehir eklenmiştir.");
            }

            var existsCityEntity = await _uwork.GetRepository<City>().GetById(updateCityVM.Id.Value);

            _mapper.Map(updateCityVM, existsCityEntity);

            _uwork.GetRepository<City>().Update(existsCityEntity);
            result.Data = await _uwork.CommitAsync();

            return result;
        }
    }
}
