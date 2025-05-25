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
            new DataModel { ProductID = 2, ProductName = "Samsung Galaxy", Brand = "Samsung", Category = "Smartphone", Color = "Blue", Price = 2000 },
            new DataModel { ProductID = 3, ProductName = "Alienware", Brand = "Dell", Category = "Laptop", Color = "Black", Price = 5000 },
            new DataModel { ProductID = 4, ProductName = "Huawei MatePad", Brand = "Huawei", Category = "Tablet", Color = "White", Price = 3500 },
            new DataModel { ProductID = 5, ProductName = "Razer Blade 14", Brand = "Razer", Category = "Laptop", Color = "Green", Price = 4500 }
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
        return View(products);
    }

    public IActionResult ExportToPdf()
    {
        var products = GetProducts();
        return View(products);
    }

}