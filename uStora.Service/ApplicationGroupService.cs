using System;
using System.Collections.Generic;
using System.Linq;
using uStora.Common.Exceptions;
using uStora.Common.Services.Int32;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface IApplicationGroupService : ICrudService<ApplicationGroup>, IGetDataService<ApplicationGroup>
    {
        ApplicationGroup GetDetail(int id);

        IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter);

        bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId);

        IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId);

        IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId);

        void IsDeleted(int id);
    }

    public class ApplicationGroupService : IApplicationGroupService
    {
        private IApplicationGroupRepository _appGroupRepository;
        private IApplicationUserGroupRepository _appUserGroupRepository;
        private IUnitOfWork _unitOfWork;

        public ApplicationGroupService(IApplicationGroupRepository appGroupRepository,
           IUnitOfWork unitOfWork, IApplicationUserGroupRepository appUserGroupRepository)
        {
            _appGroupRepository = appGroupRepository;
            _unitOfWork = unitOfWork;
            _appUserGroupRepository = appUserGroupRepository;
        }

        public ApplicationGroup Add(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name))
                throw new NameDuplicatedException("Tên nhóm không được trùng nhau.");
            return _appGroupRepository.Add(appGroup);
        }

        public bool AddUserToGroups(IEnumerable<ApplicationUserGroup> userGroups, string userId)
        {
            _appUserGroupRepository.DeleteMulti(x => x.UserId == userId);
            foreach (var userGroup in userGroups)
            {
                _appUserGroupRepository.Add(userGroup);
            }
            return true;
        }

        public void Delete(int id)
        {
            var appGroup = FindById(id);
            _appGroupRepository.Delete(appGroup);
        }

        public ApplicationGroup FindById(int id)
        {
            var appGroup = _appGroupRepository.GetSingleById(id);
            return appGroup;
        }

        public IEnumerable<ApplicationGroup> GetAll(string keyword = null)
        {
            return _appGroupRepository.GetMulti(x => x.IsDeleted == false);
        }

        public IEnumerable<ApplicationGroup> GetAll(int page, int pageSize, out int totalRow, string filter)
        {
            var query = _appGroupRepository.GetMulti(x => x.IsDeleted == false);
            if (!string.IsNullOrEmpty(filter))
                query = query.Where(x => x.Name.Contains(filter) || x.IsDeleted == false);

            totalRow = query.Count();
            return query.OrderBy(x => x.Name).Skip(page * pageSize).Take(pageSize);
        }

        public ApplicationGroup GetDetail(int id)
        {
            return _appGroupRepository.GetSingleById(id);
        }

        public IEnumerable<ApplicationGroup> GetListGroupByUserId(string userId)
        {
            return _appGroupRepository.GetListGroupByUserId(userId);
        }

        public IEnumerable<ApplicationUser> GetListUserByGroupId(int groupId)
        {
            return _appGroupRepository.GetListUserByGroupId(groupId);
        }

        public void IsDeleted(int id)
        {
            var group = _appGroupRepository.GetSingleById(id);
            group.IsDeleted = true;
            SaveChanges();
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(ApplicationGroup appGroup)
        {
            if (_appGroupRepository.CheckContains(x => x.Name == appGroup.Name && x.ID != appGroup.ID))
                throw new NameDuplicatedException("Tên không được trùng");
            _appGroupRepository.Update(appGroup);
        }
    }
}