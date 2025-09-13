using ClosedXML.Excel;
using DataToPDFExcel.Data;
using DataToPDFExcel.Interfaces;
using DataToPDFExcel.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Rotativa.AspNetCore;

namespace DataToPDFExcel.Controllers
{
    //-----Products CRUD Section-----//
    public class ProductsController : Controller
    {
        private readonly IProductsRepository _repository;

        public ProductsController(IProductsRepository repository)
        {
            _repository = repository;
        }

        public IActionResult ProductsViewer()
        {
            var products = _repository.GetProductsList();
            return View(products);
        }

        public IActionResult ProductsEntryForm()
        {
            var brands = _repository.GetAllBrands();
            ViewBag.BrandList = new SelectList(brands, "BrandID", "BrandName");
            var categories = _repository.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
            return View();
        }

        [HttpPost]
        public IActionResult ProductsEntryForm(Products product)
        {
            if (ModelState.IsValid)
            {
                _repository.CreateProduct(product);
                return RedirectToAction("ProductsViewer");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult ProductsEditForm(int id)
        {
            var product = _repository.GetProductByID(id);
            var brands = _repository.GetAllBrands();
            ViewBag.BrandList = new SelectList(brands, "BrandID", "BrandName");
            var categories = _repository.GetAllCategories();
            ViewBag.CategoryList = new SelectList(categories, "CategoryID", "CategoryName");
            if (product == null) return NotFound();
            return View(product);
        }

        [HttpPost]
        public IActionResult EditProduct(Products product)
        {
            if (ModelState.IsValid)
            {
                _repository.UpdateProduct(product);
                return RedirectToAction("ProductsViewer");
            }
            return View(product);
        }

        [HttpGet]
        public IActionResult DeleteProduct(int id)
        {
            var product = _repository.GetProductByID(id);
            if (product == null) return NotFound();
            _repository.DeleteProduct(id);
            return RedirectToAction("ProductsViewer");
        }

        //-----Products CRUD Section-----//

        //-----Export File Section-----//

        public IActionResult ExportToExcel()
        {
            var products = _repository.GetProductsList();

            using var workbook = new XLWorkbook();
            var worksheet = workbook.Worksheets.Add("ProductsView");

            worksheet.Cell(1, 1).Value = "ID";
            worksheet.Cell(1, 2).Value = "Name";
            worksheet.Cell(1, 3).Value = "Brand";
            worksheet.Cell(1, 4).Value = "Category";
            worksheet.Cell(1, 5).Value = "Color";
            worksheet.Cell(1, 6).Value = "Price";

            for (int i = 0; i < products.Count; i++)
            {
                var p = products[i];
                worksheet.Cell(i + 2, 1).Value = p.ProductID;
                worksheet.Cell(i + 2, 2).Value = p.ProductName;
                worksheet.Cell(i + 2, 3).Value = p.BrandName;
                worksheet.Cell(i + 2, 4).Value = p.CategoryName;
                worksheet.Cell(i + 2, 5).Value = p.Color;
                worksheet.Cell(i + 2, 6).Value = p.Price;
            }

            using var stream = new MemoryStream();
            workbook.SaveAs(stream);
            stream.Position = 0;

            return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
        }

        public IActionResult ExportToPdf(bool download = false)
        {
            var products = _repository.GetProductsList();
            var pdf = new ViewAsPdf("ProductsPDFViewer", products)
            {
                PageSize = Rotativa.AspNetCore.Options.Size.A4
            };

            if (download)
                pdf.FileName = "Products.pdf";

            return pdf;
        }

        //-----Export File Section-----//

    }
}
