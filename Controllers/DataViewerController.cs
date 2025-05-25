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
            new DataModel { ProductID = 1, ProductName = "IPhone 15", Brand = "Apple", Category = "Smartphone", Color = "Silver", Price = 3000 },
            new DataModel { ProductID = 2, ProductName = "Razer Blade 14", Brand = "Razer", Category = "Laptop", Color = "Green", Price = 4500 },
            new DataModel { ProductID = 3, ProductName = "Samsung Galaxy", Brand = "Samsung", Category = "Smartphone", Color = "Blue", Price = 2000 },
            new DataModel { ProductID = 4, ProductName = "Alienware", Brand = "Dell", Category = "Laptop", Color = "Black", Price = 5000 },
            new DataModel { ProductID = 5, ProductName = "Huawei MatePad", Brand = "Huawei", Category = "Tablet", Color = "White", Price = 3500 },
            new DataModel { ProductID = 6, ProductName = "IPhone 15", Brand = "Apple", Category = "Smartphone", Color = "Silver", Price = 3000 },
            new DataModel { ProductID = 7, ProductName = "Razer Blade 14", Brand = "Razer", Category = "Laptop", Color = "Green", Price = 4500 },
            new DataModel { ProductID = 8, ProductName = "Samsung Galaxy", Brand = "Samsung", Category = "Smartphone", Color = "Blue", Price = 2000 },
            new DataModel { ProductID = 9, ProductName = "Alienware", Brand = "Dell", Category = "Laptop", Color = "Black", Price = 5000 },
            new DataModel { ProductID = 10, ProductName = "Huawei MatePad", Brand = "Huawei", Category = "Tablet", Color = "White", Price = 3500 },
            new DataModel { ProductID = 11, ProductName = "IPhone 15", Brand = "Apple", Category = "Smartphone", Color = "Silver", Price = 3000 },
            new DataModel { ProductID = 12, ProductName = "Razer Blade 14", Brand = "Razer", Category = "Laptop", Color = "Green", Price = 4500 },
            new DataModel { ProductID = 13, ProductName = "Samsung Galaxy", Brand = "Samsung", Category = "Smartphone", Color = "Blue", Price = 2000 },
            new DataModel { ProductID = 14, ProductName = "Alienware", Brand = "Dell", Category = "Laptop", Color = "Black", Price = 5000 },
            new DataModel { ProductID = 15, ProductName = "Huawei MatePad", Brand = "Huawei", Category = "Tablet", Color = "White", Price = 3500 },
            new DataModel { ProductID = 16, ProductName = "IPhone 15", Brand = "Apple", Category = "Smartphone", Color = "Silver", Price = 3000 },
            new DataModel { ProductID = 17, ProductName = "Razer Blade 14", Brand = "Razer", Category = "Laptop", Color = "Green", Price = 4500 },
            new DataModel { ProductID = 18, ProductName = "Samsung Galaxy", Brand = "Samsung", Category = "Smartphone", Color = "Blue", Price = 2000 },
            new DataModel { ProductID = 19, ProductName = "Alienware", Brand = "Dell", Category = "Laptop", Color = "Black", Price = 5000 },
            new DataModel { ProductID = 20, ProductName = "Huawei MatePad", Brand = "Huawei", Category = "Tablet", Color = "White", Price = 3500 }
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
        var worksheet = workbook.Worksheets.Add("DataModel");

        worksheet.Cell(1, 1).Value = "ProductID";
        worksheet.Cell(1, 2).Value = "ProductName";
        worksheet.Cell(1, 3).Value = "Brand";
        worksheet.Cell(1, 4).Value = "Category";
        worksheet.Cell(1, 5).Value = "Color";
        worksheet.Cell(1, 6).Value = "Price";

        for (int i = 0; i < products.Count; i++)
        {
            var p = products[i];
            worksheet.Cell(i + 2, 1).Value = p.ProductID;
            worksheet.Cell(i + 2, 2).Value = p.ProductName;
            worksheet.Cell(i + 2, 3).Value = p.Brand;
            worksheet.Cell(i + 2, 4).Value = p.Category;
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
        var products = GetProducts();
        var pdf = new ViewAsPdf("PDFViewer", products)
        {
            PageSize = Rotativa.AspNetCore.Options.Size.A4
        };

        if (download)
            pdf.FileName = "Products.pdf";

        return pdf;
    }

}