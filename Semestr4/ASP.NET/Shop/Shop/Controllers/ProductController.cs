using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Shop.Data;
using Shop.Models;
using Shop.Models.ViewModels;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Shop.Controllers
{
    public class ProductController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ProductController(ApplicationDbContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            IEnumerable<Product> productList = _context.Product.Include(p => p.Category);
            return View(productList);
        }

        public IActionResult IndexCat(int? id)
        {
            IEnumerable<Product> productList = _context.Product.Include(p => p.Category).Where(x=> x.CategoryId== id);
            return View("Index", productList);
        }

        [HttpGet]
        public IActionResult CreateEdit(int? id)
        {
           ProductVM productVM = new ProductVM()
            {
                Product = new Product(),
                CategorySelectList = _context.Category.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            if(id == null)
            {
                return View(productVM);
            }
            else
            {
                productVM.Product = _context.Product.Find(id);
                if(productVM.Product == null)
                {
                    return NotFound();
                }
                return View(productVM);
            }
        }
        [HttpPost]
        public IActionResult CreateEdit(ProductVM productVM)
        {
            if (ModelState.IsValid)
            {
               
                if(productVM.Product.Id == 0)
                {


                    productVM.Product.Image = SaveFile(productVM);
                    _context.Product.Add(productVM.Product);
                    _context.SaveChanges();
                }
                else
                {
                    string newImg = SaveFile(productVM);
                    try
                    {
                        if (newImg != null)
                            if (newImg != productVM.Product.Image)
                                RemoveImage(productVM.Product.Image);
                    }catch (Exception ex) { }

                    productVM.Product.Image = newImg;
                    _context.Product.Update(productVM.Product);
                    _context.SaveChanges();
                }
            }

            return RedirectToAction("Index");
        }

        string SaveFile(ProductVM productVM)
        {
            var files = HttpContext.Request.Form.Files;
            string webRootPath = _webHostEnvironment.WebRootPath;
            if (files.Count > 0)
            {
                string upload = webRootPath + ENV.ImagePath;
                string fileName = Guid.NewGuid().ToString();
                string extantion = Path.GetExtension(files[0].FileName);

                using (var fileStream = new FileStream(Path.Combine(upload, fileName + extantion), FileMode.Create))
                {
                    files[0].CopyTo(fileStream);
                };

                return ENV.ImagePath + fileName + extantion;
            }
            else if (!string.IsNullOrWhiteSpace(productVM.Product.Image))
            {
                return productVM.Product.Image;
            }
            return null;
        }
        void RemoveImage(string img)
        {
            try
            {
                string PATH = _webHostEnvironment.WebRootPath;
                string file = PATH+ img;
                if (img != null)
                    if (System.IO.File.Exists(file))
                    {
                        System.IO.File.Delete(file);
                    }
            }
            catch (Exception ex)
            { }
        }

        [HttpGet]
        public IActionResult Delete(int? id)
        {
            if (id == null || id == 0)
            {
                return NotFound();
            }

            var product = _context.Product.Find(id);
            if (product == null)
            {
                return NotFound();
            }

            _context.Product.Remove(product);
            _context.SaveChanges();
            RemoveImage(product.Image);

            return RedirectToAction("Index");
        }

        public IActionResult Detalis(int? id)
        {
            Product product= _context.Product.Include(p => p.Category).FirstOrDefault(x=> x.Id==id);
            return View(product);
        }
    }
}
