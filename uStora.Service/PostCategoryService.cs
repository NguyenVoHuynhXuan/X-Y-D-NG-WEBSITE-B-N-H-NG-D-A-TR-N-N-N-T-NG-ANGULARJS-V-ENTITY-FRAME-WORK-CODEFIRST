using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;
using System.Collections.Generic;
using uStora.Common.Services.Int64;
using System;

namespace uStora.Service
{
    public interface IPostCategoryService : ICrudService<PostCategory>, IGetDataService<PostCategory>
    {
        IEnumerable<PostCategory> GetAllByParentID(int parentID);

    }

    public class PostCategoryService : IPostCategoryService
    {
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        public PostCategoryService(IPostCategoryRepository postCategoryRepository, IUnitOfWork unitOfWork)
        {
            this._postCategoryRepository = postCategoryRepository;
            this._unitOfWork = unitOfWork;
        }

        public PostCategory Add(PostCategory postCategory)
        {
            return _postCategoryRepository.Add(postCategory);
        }

        public void Update(PostCategory postCategory)
        {
            _postCategoryRepository.Update(postCategory);
        }

        public void Delete(long id)
        {
            _postCategoryRepository.Delete(id);
        }


        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public IEnumerable<PostCategory> GetAllByParentID(int parentID)
        {
            return _postCategoryRepository.GetMulti(x => x.Status && x.ParentID == parentID);
        }


        public void IsDeleted(long id)
        {
            var postCategory = FindById(id);
            postCategory.IsDeleted = true;
            Update(postCategory);
        }

        public PostCategory FindById(long id)
        {
            return _postCategoryRepository.GetSingleById(id);
        }

        public IEnumerable<PostCategory> GetAll(string keyword = null)
        {
            IEnumerable<PostCategory> data = GetAll();
            if (!string.IsNullOrEmpty(keyword))
            {
                data = _postCategoryRepository.GetMulti(x => !x.IsDeleted && !x.Status
                || x.Name.Contains(keyword)
                || x.Description.Contains(keyword));
            }
            return data;
        }
    }
}