using System;
using System.Collections.Generic;
using uStora.Common.Services.Int64;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface IBrandService : ICrudService<Brand>, IGetDataService<Brand>
    {
        IEnumerable<Brand> GetActivedBrand(string keyword = null);
    }

    public class BrandService : IBrandService
    {
        private IBrandRepository _brandRepository;
        private IUnitOfWork _unitOfWork;

        public BrandService(IBrandRepository brandRepository,
            IUnitOfWork unitOfWork)
        {
            this._brandRepository = brandRepository;
            this._unitOfWork = unitOfWork;
        }

        public Brand Add(Brand brand)
        {
            return _brandRepository.Add(brand);
        }

        public void Delete(long id)
        {
            _brandRepository.Delete(id);
        }

        public IEnumerable<Brand> GetActivedBrand(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _brandRepository.GetMulti(x => x.Status && x.Name.Contains(keyword) || x.Description.Contains(keyword));
            }
            else
            {
                return _brandRepository.GetMulti(x => x.Status);
            }
        }

        public IEnumerable<Brand> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _brandRepository.GetMulti(x => x.Name.Contains(keyword) || x.Description.Contains(keyword) && x.IsDeleted == false);
            }
            else
            {
                return _brandRepository.GetMulti(x => x.Status && x.IsDeleted == false);
            }
        }


        public Brand FindById(long id)
        {
            return _brandRepository.GetSingleById(id);
        }

        public void IsDeleted(long id)
        {
            var brand = FindById(id);
            brand.IsDeleted = true;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Brand brand)
        {
            _brandRepository.Update(brand);
        }
    }
}