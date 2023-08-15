﻿using NorthwindApi.Models.DTO;
using NorthwindApi.Models;

namespace NorthwindApi.Controllers
{
    public static class Utiles
    {
        public static SupplierDTO SupplierToDTO(Supplier supplier) =>
           new SupplierDTO
           {
               SupplierId = supplier.SupplierId,
               CompanyName = supplier.CompanyName,
               ContactName = supplier.ContactName,
               ContactTitle = supplier.ContactTitle,
               Country = supplier.Country,
               TotalProducts = supplier.Products.Count,
               //Setting the products property
               Products = supplier.Products.Select(x => ProductToDTO(x)).ToList()
           };
        public static ProductDTO ProductToDTO(Product product) =>
            new ProductDTO
            {
                ProductId = product.ProductId,
                ProductName = product.ProductName,
                SupplierId = product.SupplierId,
                CategoryId = product.CategoryId,
                UnitPrice = product.UnitPrice,
            };

    }
}
