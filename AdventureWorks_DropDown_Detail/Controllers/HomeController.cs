using AdventureWorks_DropDown_Detail.DTO;
using AdventureWorks_DropDown_Detail.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.Query;
using System.Diagnostics;
using System.Reflection;

namespace AdventureWorks_DropDown_Detail.Controllers
{
	public class HomeController : Controller
	{

		public AdventureWorks2019Context _context;

		public HomeController(AdventureWorks2019Context context)
		{
			_context = context;
		}

		public IActionResult Index()
		{

			// Adventureworks veri tabanýndan gelen veriler ile bir dropdown dolduralým

			// Veri tabaýndan gelne Product tablosunu, linq sorgusu ile select yaparak,dropdown'un istediði, selectListItem dizisi haline getirdik
			GetViewModel model = new GetViewModel();
			model.Products = _context.Products.Select(s => new SelectListItem()
			{
				Text = s.Name,
				Value = s.ProductId.ToString()

			}).ToList();

			return View(model);
		}

		[HttpPost]
		public IActionResult Index(GetViewModel model)
		{
			GetViewModel inlinemodel = new GetViewModel();
			inlinemodel.Products = _context.Products.Select(s => new SelectListItem()
			{
				Text = s.Name,
				Value = s.ProductId.ToString()

			}).ToList();
			inlinemodel.SeletedId = model.SeletedId;

			// id ile gelen ürünün detaylarýný geçelim 

			IQueryable<Product> products= _context.Products.Where(s => s.ProductId == model.SeletedId);

			// Reflection ile gelen deðerin tüm verilerini mapleyelim


			List<ProductDetail>detail = new List<ProductDetail>();

			foreach (var item in products)
			{
				foreach (var p in item.GetType().GetProperties())
				{

					detail.Add(new ProductDetail()
					{

						Name = p.Name,
						Value = p.GetValue(item, null)
					}); 
				

					 
				
				}
			}
			inlinemodel.Detail = detail;



			return View(inlinemodel);

		}
		public IActionResult Privacy()
		{
			return View();
		}

		[ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
		public IActionResult Error()
		{
			return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
		}
	}
}
