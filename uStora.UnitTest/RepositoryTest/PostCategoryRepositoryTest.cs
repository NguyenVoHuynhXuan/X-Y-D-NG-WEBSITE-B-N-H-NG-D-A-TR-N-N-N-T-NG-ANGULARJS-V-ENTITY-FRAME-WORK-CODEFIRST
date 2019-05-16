using Microsoft.VisualStudio.TestTools.UnitTesting;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;
using System;
using System.Linq;

namespace uStora.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        private IDbFactory _dbFactory;
        private IPostCategoryRepository _postCategoryRepository;
        private IUnitOfWork _unitOfWork;

        [TestInitialize]
        public void Initial()
        {
            _dbFactory = new DbFactory();
            _postCategoryRepository = new PostCategoryRepository(_dbFactory);
            _unitOfWork = new UnitOfWork(_dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var list = _postCategoryRepository.GetAll().ToList();

            Assert.AreEqual(1, list.Count());
            
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            PostCategory pc = new PostCategory();

            pc.Name = "C# Cơ bản";
            pc.Alias = "c-co-ban";

            pc.Status = true;
            pc.CreatedDate = DateTime.Now;

            var result = _postCategoryRepository.Add(pc);
            _unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}