using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using Rotativa.AspNetCore;
using System.Collections.Generic;
using System.IO;

public class DataViewerController : Controller
{
    private List<DataModel> GetProducts()
    {
        return new List<DataModel>
        {
            new DataModel { ID = 1, Name = "IPhone 15", Brand = "Apple", Category = "Smartphone", Price = 3000 },
            new DataModel { ID = 2, Name = "Samsung Galaxy", Brand = "Samsung", Category = "Smartphone", Price = 2000 },
            new DataModel { ID = 3, Name = "Alienware", Brand = "Dell", Category = "Laptop", Price = 5000 },
            new DataModel { ID = 4, Name = "Huawei MatePad", Brand = "Huawei", Category = "Tablet", Price = 3500 },
            new DataModel { ID = 5, Name = "Razer Blade 14", Brand = "Razer", Category = "Laptop", Price = 4500 }
        };
    }

    public IActionResult TableViewer()
    {
        var products = GetProducts();
        return View(products);
    }

    public IActionResult ExportToExcel()
    {
        var products = GetProducts();

        using var workbook = new XLWorkbook();
        var worksheet = workbook.Worksheets.Add("Products");

        // Add header
        worksheet.Cell(1, 1).Value = "ID";
        worksheet.Cell(1, 2).Value = "Name";
        worksheet.Cell(1, 3).Value = "Brand";
        worksheet.Cell(1, 4).Value = "Category";
        worksheet.Cell(1, 5).Value = "Price";

        // Add data
        for (int i = 0; i < products.Count; i++)
        {
            var p = products[i];
            worksheet.Cell(i + 2, 1).Value = p.ID;
            worksheet.Cell(i + 2, 2).Value = p.Name;
            worksheet.Cell(i + 2, 3).Value = p.Brand;
            worksheet.Cell(i + 2, 4).Value = p.Category;
            worksheet.Cell(i + 2, 5).Value = p.Price;
        }

        using var stream = new MemoryStream();
        workbook.SaveAs(stream);
        stream.Position = 0;

        return File(stream.ToArray(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Products.xlsx");
    }

    public IActionResult ExportToPdf(bool download = false)
    {
        var products = GetProducts();
        var pdf = new ViewAsPdf("ProductsPDFViewer", products)
        {
            PageSize = Rotativa.AspNetCore.Options.Size.A4
        };

        if (download)
            pdf.FileName = "Products.pdf";

        return pdf;
    }
}