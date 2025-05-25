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

}