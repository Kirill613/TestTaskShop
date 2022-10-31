using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using NLayerApp.DAL.Entities;
using TestTaskShop.Models;
using NLayerApp.DAL.Interfaces;

namespace TestTaskShop.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IActionResult> Index()
        {
            var orders = await _unitOfWork.Orders.GetAll();
            var products = await _unitOfWork.Products.GetAll();

            HomeViewModel homeViewModel = new HomeViewModel
            {
                Orders = orders ?? new List<Order>(),
                Products = products ?? new List<Product>()
            };

            return View(homeViewModel);
        }
    }
}