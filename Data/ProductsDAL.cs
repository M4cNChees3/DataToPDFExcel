using DataToPDFExcel.Interfaces;
using DataToPDFExcel.Models;
using Microsoft.Data.SqlClient;
using System.Data;

namespace DataToPDFExcel.Data
{
    public class ProductsDAL : IProductsRepository
    {
        private readonly string _connectionString;

        public ProductsDAL(IConfiguration configuration)
        {
            _connectionString = configuration.GetConnectionString("DefaultConnection");
        }

        public List<ProductsView> GetProductsList()
        {
            var products = new List<ProductsView>();

            using (var connection = new SqlConnection(_connectionString))
            {
                using (var command = new SqlCommand("Procedure_GetAllProducts", connection))
                {
                    connection.Open();
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            products.Add(new ProductsView
                            {
                                ProductID = (int)reader["ProductID"],
                                ProductName = reader["ProductName"].ToString(),
                                BrandName = reader["BrandName"].ToString(),
                                CategoryName = reader["CategoryName"].ToString(),
                                Color = reader["Color"].ToString(),
                                Price = Convert.ToDecimal(reader["Price"])
                            });
                        }
                    }
                }
            }

            return products;

        }

        public List<Brands> GetAllBrands()
        {
            var brands = new List<Brands>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT * FROM Brands", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                brands.Add(new Brands
                {
                    BrandID = reader["BrandID"].ToString(),
                    BrandName = reader["BrandName"].ToString()
                });
            }

            return brands;
        }

        public List<Categories> GetAllCategories()
        {
            var categories = new List<Categories>();

            using var connection = new SqlConnection(_connectionString);
            using var command = new SqlCommand("SELECT * FROM Categories", connection);
            connection.Open();
            using var reader = command.ExecuteReader();
            while (reader.Read())
            {
                categories.Add(new Categories
                {
                    CategoryID = reader["CategoryID"].ToString(),
                    CategoryName = reader["CategoryName"].ToString()
                });
            }

            return categories;
        }

        public Products? GetProductByID(int id)
        {
            Products product = null;

            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("Procedure_GetProductById", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", id);
                con.Open();

                using (var reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        product = new Products
                        {
                            ProductID = (int)reader["ProductID"],
                            ProductName = reader["ProductName"].ToString(),
                            BrandID = reader["BrandID"].ToString(),
                            CategoryID = reader["CategoryID"].ToString(),
                            Color = reader["Color"].ToString(),
                            Price = (int)reader["Price"]
                        };
                    }
                }
            }

            return product;
        }

        public void CreateProduct(Products product)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("Procedure_CreateProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@BrandID", product.BrandID);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                cmd.Parameters.AddWithValue("@Color", product.Color);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void UpdateProduct(Products product)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("Procedure_UpdateProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", product.ProductID);
                cmd.Parameters.AddWithValue("@ProductName", product.ProductName);
                cmd.Parameters.AddWithValue("@BrandID", product.BrandID);
                cmd.Parameters.AddWithValue("@CategoryID", product.CategoryID);
                cmd.Parameters.AddWithValue("@Color", product.Color);
                cmd.Parameters.AddWithValue("@Price", product.Price);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }

        public void DeleteProduct(int id)
        {
            using (var con = new SqlConnection(_connectionString))
            using (var cmd = new SqlCommand("Procedure_DeleteProduct", con))
            {
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.AddWithValue("@ProductID", id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
        }
    }
}
