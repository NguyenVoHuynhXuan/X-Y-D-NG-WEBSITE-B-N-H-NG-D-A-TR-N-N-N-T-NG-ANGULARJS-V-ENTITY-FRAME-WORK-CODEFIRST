using System;
using System.Collections.Generic;
using System.Linq;
using uStora.Common.Services.Int32;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface IFeedbackService : ICrudService<Feedback>, IGetDataService<Feedback>
    {
    }

    public class FeedbackService : IFeedbackService
    {
        private IFeedbackRepository _feedbackRepository;
        private IUnitOfWork _unitOfWork;

        public FeedbackService(IFeedbackRepository feedbackRepository, IUnitOfWork unitOfWork)
        {
            this._feedbackRepository = feedbackRepository;
            this._unitOfWork = unitOfWork;
        }

        public Feedback Add(Feedback feedback)
        {
            feedback.Status = true;
            return _feedbackRepository.Add(feedback);
        }

        public void Delete(int id)
        {
            _feedbackRepository.Delete(id);
        }

        public IEnumerable<Feedback> GetAll(string keyword = null)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _feedbackRepository.GetMulti(x => x.Name.Contains(keyword)).OrderByDescending(y => y.CreatedDate);
            }
            else
            {
                return _feedbackRepository.GetAll();
            }
        }

        public Feedback FindById(int id)
        {
            return _feedbackRepository.GetSingleById(id);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Update(Feedback feedback)
        {
            _feedbackRepository.Update(feedback);
        }
    }
}