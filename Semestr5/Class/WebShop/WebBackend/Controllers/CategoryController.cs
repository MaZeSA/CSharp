using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Drawing.Imaging;
using WebBackend.Data;
using WebBackend.Data.Entities;
using WebBackend.Helpers;
using WebBackend.Model;

namespace WebBackend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoryController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly AppEFContext _appEFContext;
        public CategoryController(IMapper mapper, AppEFContext appEFContext)
        {
            _mapper = mapper;
            _appEFContext = appEFContext;
        }

        [HttpGet("list")]
        public async Task<IActionResult> List()
        {
            string damain = $"{Request.Scheme}://{Request.Host.Value}";
            var list = _appEFContext.Categories
                .Select(x => _mapper.Map<CategoryItemVM>(x)).ToList();

            return Ok(list);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] CategoryCreateVM model)
        {
            return Ok();

            string base64 = model.ImageBase64;
            if (base64.Contains(","))
                base64 = base64.Split(',')[1];

            var img = base64.FromBase64StringToImage();

            string fileName = Path.GetRandomFileName() + ".jpg";
            string dirSave = Path.Combine(Directory.GetCurrentDirectory(), "images", fileName);
            img.Save(dirSave, ImageFormat.Jpeg);

            var category = _mapper.Map<CategoryEntity>(model);
            category.Image = fileName;
            _appEFContext.Categories.Add(category);
           _appEFContext.SaveChanges();

           return Ok();
        }


        [HttpPut]
        public async Task<IActionResult> EditCategory(CategoryEntity edit_category)
        {
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Remove(int id)
        {
            return Ok();
        }
    }
}
