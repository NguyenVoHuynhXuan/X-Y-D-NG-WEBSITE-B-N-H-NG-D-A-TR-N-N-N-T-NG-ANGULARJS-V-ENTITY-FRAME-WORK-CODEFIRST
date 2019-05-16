using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using uStora.Common.Services.Int32;
using uStora.Common.ViewModels;
using uStora.Data.Infrastructure;
using uStora.Data.Repositories;
using uStora.Model.Models;

namespace uStora.Service
{
    public interface IOrderService : ICrudService<Order>, IGetDataService<Order>
    {
        Order Add(ref Order order, List<OrderDetail> orderDetails);
        IEnumerable<OrderClientViewModel> GetListOrders(string userId);
        IEnumerable<Order> GetUnCompletedOrder();
        void UpdateStatus(int orderId);
    }

    public class OrderService : IOrderService
    {
        private IOrderRepository _orderRepository;
        private IOrderDetailRepository _orderDetailRepository;
        private IUnitOfWork _unitOfWork;

        public OrderService(IOrderRepository orderRepository, IUnitOfWork unitOfWork,
            IOrderDetailRepository orderDetailRepository)
        {
            _orderRepository = orderRepository;
            _unitOfWork = unitOfWork;
            _orderDetailRepository = orderDetailRepository;
        }

        public Order Add(ref Order order, List<OrderDetail> orderDetails)
        {
            try
            {
                _orderRepository.Add(order);
                _unitOfWork.Commit();
                foreach (var orderDetail in orderDetails)
                {
                    orderDetail.OrderID = order.ID;
                    _orderDetailRepository.Add(orderDetail);
                }
                return order;
            }
            catch (DbEntityValidationException e)
            {
                foreach (var eve in e.EntityValidationErrors)
                {
                    Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                        eve.Entry.Entity.GetType().Name, eve.Entry.State);
                    foreach (var ve in eve.ValidationErrors)
                    {
                        Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                            ve.PropertyName, ve.ErrorMessage);
                    }
                }
                throw;
            }
        }

        IEnumerable<OrderClientViewModel> IOrderService.GetListOrders(string userId)
        {
            return _orderRepository.GetListOrder(userId).OrderBy(x => x.PaymentStatus);
        }

        public Order FindById(int id)
        {
            return _orderRepository.GetSingleById(id);
        }

        public IEnumerable<Order> GetAll(string keyword)
        {
            if (string.IsNullOrEmpty(keyword))
                return _orderRepository.GetAll();
            else
                return _orderRepository.GetMulti(x => x.CustomerName.Contains(keyword));
        }

        public IEnumerable<Order> GetUnCompletedOrder()
        {
            return _orderRepository.GetMulti(x => x.PaymentStatus == 0);
        }

        public void Update(Order order)
        {
            _orderRepository.Update(order);
        }

        public Order Add(Order order)
        {
            return _orderRepository.Add(order);
        }

        public void SaveChanges()
        {
            _unitOfWork.Commit();
        }

        public void Delete(int id)
        {
            _orderRepository.Delete(id);
        }
        public void UpdateStatus(int orderId)
        {
            var order = _orderRepository.GetSingleById(orderId);
            order.Status = true;
            _orderRepository.Update(order);
        }

        public void IsDeleted(int id)
        {
            throw new NotImplementedException();
        }

    }
}