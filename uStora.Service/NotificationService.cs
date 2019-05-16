using System;
using System.Collections.Generic;
using System.Linq;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface INotificationService
    {
        IEnumerable<Feedback> GetUnViewedFeedbacks(DateTime date);

        IEnumerable<Feedback> GetAllFeedbacks();

        IEnumerable<ApplicationUser> GetAllUsers();

        IEnumerable<ApplicationUser> GetUnViewedUsers(DateTime date);
    }

    public class NotificationService : INotificationService
    {
        private IFeedbackRepository _feedbackRepository;
        private IApplicationUserRepository _userAppRepository;
        private IUnitOfWork _unitOfWork;

        public NotificationService(IFeedbackRepository feedbackRepository, IApplicationUserRepository userAppRepository, IUnitOfWork unitOfWork)
        {
            _feedbackRepository = feedbackRepository;
            _userAppRepository = userAppRepository;
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<Feedback> GetUnViewedFeedbacks(DateTime date)
        {
            return _feedbackRepository.GetMulti(x => x.CreatedDate > date && x.Status).OrderByDescending(y => y.CreatedDate);
        }

        public IEnumerable<ApplicationUser> GetUnViewedUsers(DateTime date)
        {
            return _userAppRepository.GetMulti(x => x.CreatedDate > date).OrderByDescending(y => y.CreatedDate);
        }

        public IEnumerable<Feedback> GetAllFeedbacks()
        {
            return _feedbackRepository.GetAll().OrderByDescending(y => y.CreatedDate);
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _userAppRepository.GetAll().OrderByDescending(x => x.CreatedDate);
        }
    }
}