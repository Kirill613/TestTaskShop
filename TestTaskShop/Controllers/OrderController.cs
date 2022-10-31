using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using NuGet.Packaging;
using NLayerApp.DAL.Entities;
using TestTaskShop.Models;
using NLayerApp.DAL.Interfaces;
using AutoMapper;
using TestTaskShop.Filters;

namespace TestTaskShop.Controllers
{
    [Authorize(Roles = "Courier")]
    public class OrderController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public OrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Order
        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            return PartialView("_OrdersPartial", orders);
        }

        // GET: Order/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var order = await _unitOfWork.Orders.GetById(id);

            if (order == null)
            {
                return NotFound();
            }

            var orderProducts = order.ProductOrders.Select(po => po.ProductId).ToList();

            var orderProductsResult = (List<Product>)await _unitOfWork.Products.Find(p => orderProducts.Contains(p.Id));


            var orderDetailsViewModel = new OrderDetailsViewModel
            {
                Order = order,
                OrderProducts = orderProductsResult ?? new List<Product>()
            };

            return View(orderDetailsViewModel);
        }

        [HttpPost]
        [Authorize(Roles = "Administrator")]
        [ValidatePost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(EditOrderViewModel editOrderViewModel)
        {
            var order = _mapper.Map<Order>(editOrderViewModel);
            order.IsActive = false;

            await _unitOfWork.Orders.Upsert(order);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
