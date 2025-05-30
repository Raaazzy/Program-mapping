﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Reflection.Metadata.Ecma335;
using Tamak.Data.Interfaces;
using Tamak.Data.Models;
using Tamak.Service.Implementations;
using Tamak.Service.Interfaces;
using Tamak.ViewModels;

namespace Tamak.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        private readonly IAssortimentService _assortimentService;
        private readonly IUserService _userService;
        private static long UserId = 0;

        public HomeController(IProductService productService, IAssortimentService assortimentService, IUserService userService)
        {
            _productService = productService;
            _assortimentService = assortimentService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> IndexAsync()
        {
            var response0 = _productService.GetProducts();
            var response = await _assortimentService.GetItems(User.Identity.Name);
            var response2 = await _userService.GetUsers();
            var response3 = _assortimentService.GetAssortiments();

            if (response0.StatusCode == Data.Enum.StatusCode.Success && response.StatusCode == Data.Enum.StatusCode.Success && response2.StatusCode == Data.Enum.StatusCode.Success && response3.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allAllProducts = response0.Data;
                obj.allProducts = response.Data;
                obj.allUsers = response2.Data;
                obj.allAssortiments = response3.Data;
                obj.currentUserId = UserId;
                return View(obj);

            }
            return RedirectToAction("Login", "Account");
        }

        [HttpGet]
        public async Task<ActionResult> GetProduct(int id)
        {
            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = (IEnumerable<Data.Models.Product>)response.Data;
                return View(obj);
            }
            return RedirectToAction("Error");
        }

        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var response = await _productService.DeleteProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return RedirectToAction("GetProducts");
            }
            return RedirectToAction("Error");
        }

        [HttpGet]
        public async Task<IActionResult> Save(int id)
        {
            if (id == 0)
            {
                return View();
            }

            var response = await _productService.GetProduct(id);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                HomeViewModel obj = new HomeViewModel();
                obj.allProducts = (IEnumerable<Data.Models.Product>)response.Data;
                return View(obj);
            }

            return RedirectToAction("Error");
        }

        [HttpPost]
        public async Task<IActionResult> Save(ProductViewModel model)
        {
            ModelState.Remove("Avatar");
            ModelState.Remove("Img");
            ModelState.Remove("Category");

            if (ModelState.IsValid)
            {
                var response = await _productService.Save(model);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return Json(new { data = response.Description });
                }
            }
            return RedirectToAction("GetProducts");
        }

        [HttpPost]
        public async Task<IActionResult> ChangeAvaliable(ProductViewModel model)
        {
            ModelState.Remove("Avatar");
            ModelState.Remove("Img");
            ModelState.Remove("Category");
            ModelState.Remove("Name");
            ModelState.Remove("Description");
            ModelState.Remove("Category");
            if (ModelState.IsValid)
            {
                var response = await _productService.ChangeAvaliable(model);
                if (response.StatusCode == Data.Enum.StatusCode.Success)
                {
                    return Json(new { data = response.Description });
                }
            }
            return RedirectToAction("GetProducts");
        }

        [HttpPost]
        public async Task<IActionResult> Add()
        {
            var response = await _assortimentService.Add(User.Identity.Name);
            if (response.StatusCode == Data.Enum.StatusCode.Success)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("GetProducts");
        }

        [HttpGet]
        public async Task<IActionResult> GetAssortiment(HomeViewModel user)
        {
            UserId = user.currentUserId;
            return RedirectToAction("Index");
        }
    }
}
