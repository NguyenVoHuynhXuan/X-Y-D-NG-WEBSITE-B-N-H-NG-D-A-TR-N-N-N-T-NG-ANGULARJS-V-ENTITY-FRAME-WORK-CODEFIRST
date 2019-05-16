using System.Collections.Generic;
using uStora.Common;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface ICommonService
    {
        Footer GetFooter();

        IEnumerable<Slide> GetSlides();

        IEnumerable<ApplicationUser> GetUsers(string filter);
    }

    public class CommonService : ICommonService
    {
        private IFooterRespository _footerRespository;
        private ISystemConfigRepository _systemConfigRepository;
        private IUnitOfWork _unitOfWork;
        private ISlideRepository _slideRepository;
        private IApplicationUserRepository _applicationUserRepository;

        public CommonService(IFooterRespository footerRespository,
            IUnitOfWork unitOfWork, ISlideRepository slideRepository,
            IApplicationUserRepository applicationUserRepository,
            ISystemConfigRepository systemConfigRepository)
        {
            _footerRespository = footerRespository;
            _unitOfWork = unitOfWork;
            _slideRepository = slideRepository;
            _applicationUserRepository = applicationUserRepository;
            _systemConfigRepository = systemConfigRepository;
        }

        public Footer GetFooter()
        {
            return _footerRespository.GetSingleByCondition(x => x.ID == CommonConstants.DefaultFooter);
        }

        public IEnumerable<Slide> GetSlides()
        {
            return _slideRepository.GetMulti(x => x.Status == true);
        }

        public IEnumerable<ApplicationUser> GetUsers(string filter)
        {
            if (!string.IsNullOrEmpty(filter))
            {
                filter = filter.ToLower();
                return _applicationUserRepository.GetMulti(x => x.FullName.ToLower().Contains(filter) || x.UserName.ToLower().Contains(filter) && x.IsDeleted == false);
            }
            else
            {
                return _applicationUserRepository.GetMulti(x => x.IsDeleted == false);
            }
        }
    }
}