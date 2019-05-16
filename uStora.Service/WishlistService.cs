using System;
using System.Collections.Generic;
using System.Linq;
using uStora.Common.Services.Int32;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface IWishlistService : ICrudService<Wishlist>, IGetDataService<Wishlist>
    {
        IEnumerable<Wishlist> GetWishlistByUserId(string userId);

        IEnumerable<Wishlist> GetWishlistByUserIdPaging(string userId, string keyword, int page, int pageSize, out int totalRow);

        bool CheckContains(long productId, string userId);

    }

    public class WishlistService : IWishlistService
    {
        private IWishlistRepository _wishlistRepository;
        private IUnitOfWork _unitOfWork;

        public WishlistService(IWishlistRepository wishlistRepository,
            IUnitOfWork unitOfWork)
        {
            _wishlistRepository = wishlistRepository;
            _unitOfWork = unitOfWork;
        }

        public Wishlist Add(Wishlist wishlist)
        {
            wishlist.Status = true;
            return _wishlistRepository.Add(wishlist);
        }

        public void Delete(int id)
        {
            _wishlistRepository.Delete(id);
        }

        public IEnumerable<Wishlist> GetWishlistByUserId(string userId)
        {
            return _wishlistRepository.GetMulti(x => x.UserId == userId);
        }

        public void Update(Wishlist wishlist)
        {
            _wishlistRepository.Update(wishlist);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public bool CheckContains(long productId, string userId)
        {
            var res = _wishlistRepository.CheckContains(x => x.ProductId == productId && x.UserId == userId);
            if (res)
                return true;
            else
                return false;
        }

        public IEnumerable<Wishlist> GetAll(string input)
        {
            if (!string.IsNullOrEmpty(input))
                return _wishlistRepository.GetMulti(x => x.Product.Name.Contains(input), new string[] { "Product" });
            else
                return _wishlistRepository.GetAll(new string[] { "Product" });
        }

        public IEnumerable<Wishlist> GetWishlistByUserIdPaging(string userId, string keyword, int page, int pageSize, out int totalRow)
        {
            var query = _wishlistRepository.GetMulti(x => x.UserId == userId);
            if (string.IsNullOrEmpty(keyword))
            {
                totalRow = query.Count();
                return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            }
            else
            {
                query = _wishlistRepository.GetMulti(x => x.Product.Name.Contains(keyword) || x.Product.Description.Contains(keyword), new string[] { "Product" });
                totalRow = query.Count();
                return query.OrderByDescending(x => x.CreatedDate).Skip((page - 1) * pageSize).Take(pageSize);
            }
        }

        public Wishlist FindById(int id)
        {
            throw new NotImplementedException();
        }
    }
}