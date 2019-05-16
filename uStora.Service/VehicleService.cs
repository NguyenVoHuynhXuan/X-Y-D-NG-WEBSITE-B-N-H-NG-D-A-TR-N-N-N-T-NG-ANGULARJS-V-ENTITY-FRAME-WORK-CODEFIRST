using System;
using System.Collections.Generic;
using uStora.Common.Services.Int32;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface IVehicleService : ICrudService<Vehicle>, IGetDataService<Vehicle>
    {
        void IsDeleted(int id);
    }

    public class VehicleService : IVehicleService
    {
        private IVehicleRepository _vehicleRepository;
        private IUnitOfWork _unitOfWork;

        public VehicleService(IVehicleRepository vehicleRepository,
            IUnitOfWork unitOfWork)
        {
            _vehicleRepository = vehicleRepository;
            _unitOfWork = unitOfWork;
        }

        public Vehicle Add(Vehicle vehicle)
        {
            vehicle.CreatedDate = DateTime.Now;
            return _vehicleRepository.Add(vehicle);
        }

        public void Update(Vehicle vehicle)
        {
            vehicle.UpdatedDate = DateTime.Now;
            _vehicleRepository.Update(vehicle);
        }

        public void Delete(int id)
        {
            _vehicleRepository.Delete(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public Vehicle FindById(int id)
        {
            return _vehicleRepository.GetSingleById(id);
        }

        public IEnumerable<Vehicle> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
                return _vehicleRepository.GetMulti(x => x.ModelID.Contains(keyword) || x.Model.Contains(keyword) || x.DriverName.Contains(keyword) || x.Name.Contains(keyword) && x.IsDeleted == false);
            else
                return _vehicleRepository.GetMulti(x => x.Status && x.IsDeleted == false);
        }

        public void IsDeleted(int id)
        {
            var vehicle = FindById(id);
            vehicle.IsDeleted = true;
            SaveChanges();
        }
    }
}