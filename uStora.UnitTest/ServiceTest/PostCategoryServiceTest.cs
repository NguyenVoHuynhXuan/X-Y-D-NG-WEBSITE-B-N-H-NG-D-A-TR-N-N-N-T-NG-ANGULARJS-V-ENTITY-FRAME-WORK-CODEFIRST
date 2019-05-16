using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;
using uStora.Service;
using System.Collections.Generic;
using System.Linq;

namespace uStora.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockPostCategoryRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> _listCategory;

        [TestInitialize]
        public void Initial()
        {
            _mockPostCategoryRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockPostCategoryRepository.Object, _mockUnitOfWork.Object);
            _listCategory = new List<PostCategory>() {
                new PostCategory {ID=1,Name = "DM1",Status = true },
                new PostCategory {ID=2,Name = "DM2",Status = true },
                new PostCategory {ID=3,Name = "DM3",Status = true },
                new PostCategory {ID=4,Name = "DM4",Status = true }
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup method
            _mockPostCategoryRepository.Setup(x => x.GetAll(null).ToList()).Returns(_listCategory);

            //call action
            var result = _postCategoryService.GetAll() as List<PostCategory>;

            //compare

            Assert.IsNotNull(result);
            Assert.AreEqual(4, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            PostCategory pc = new PostCategory();
            int id = 1;
            pc.Name = "test";
            pc.Alias = "test";
            pc.Status = true;

            _mockPostCategoryRepository.Setup(x => x.Add(pc)).Returns((PostCategory postCategory) =>
            {
                postCategory.ID = id;
                return postCategory;
            });

            var result = _postCategoryService.Add(pc);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        }
    }
}