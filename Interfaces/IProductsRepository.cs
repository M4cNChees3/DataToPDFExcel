using DataToPDFExcel.Models;
using System.Collections.Generic;

namespace DataToPDFExcel.Interfaces
{
    public interface IProductsRepository
    {
        List<ProductsView> GetProductsList();
        List<Brands> GetAllBrands();
        List<Categories> GetAllCategories();
        Products? GetProductByID(int id);
        void CreateProduct(Products product);
        void UpdateProduct(Products product);
        void DeleteProduct(int id);
    }
}