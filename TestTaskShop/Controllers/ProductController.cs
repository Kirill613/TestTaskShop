using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis;
using Microsoft.EntityFrameworkCore;
using NLayerApp.DAL.Entities;
using NLayerApp.DAL.Interfaces;
using TestTaskShop.Filters;
using TestTaskShop.Models;

namespace TestTaskShop.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class ProductController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public ProductController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        // GET: Product
        public async Task<IActionResult> Index()
        {
            var products = await _unitOfWork.Products.GetAll();
            return PartialView("_ProductsPartial", products);
        }

        // GET: Product/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            var product = await _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        // GET: Product/Create
        public async Task<IActionResult> Create()
        {
            ViewData["ShopId"] = new SelectList(await _unitOfWork.Shops.GetAll(), "Id", "Id");
            return View();
        }

        // POST: Product/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(AddProductViewModel addProductViewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(addProductViewModel);

                await _unitOfWork.Products.Add(product);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index), "Home");
            }
            ViewData["ShopId"] = new SelectList(await _unitOfWork.Shops.GetAll(), "Id", "Id", addProductViewModel.ShopId);
            return View(addProductViewModel);
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            var product = await _unitOfWork.Products.GetById(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewData["ShopId"] = new SelectList(await _unitOfWork.Shops.GetAll(), "Id", "Id", product.ShopId);
            return View(product);
        }

        // POST: Product/Edit/5

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditProductViewModel editProductViewModel)
        {
            if (id != editProductViewModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(editProductViewModel);

                await _unitOfWork.Products.Upsert(product);
                await _unitOfWork.CompleteAsync();

                return RedirectToAction(nameof(Index), "Home");
            }
            ViewData["ShopId"] = new SelectList(await _unitOfWork.Shops.GetAll(), "Id", "Id", editProductViewModel.ShopId);
            return View(editProductViewModel);
        }

        // POST: Product/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidatePost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(EditProductViewModel editProductViewModel)
        {
            var product = _mapper.Map<Product>(editProductViewModel);
            product.IsDeleted = true;

            await _unitOfWork.Products.Upsert(product);
            await _unitOfWork.CompleteAsync();

            return RedirectToAction(nameof(Index), "Home");
        }
    }
}
