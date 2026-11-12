using Infosys.DBFirstCore.DataAccessLayer.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Text;

namespace Infosys.DBFirstCore.DataAccessLayer
{
    public class QuickKartRepository
    {
        #region Uncomment below line of code

        //private QuickKartDbContext context;

        #endregion

        public QuickKartRepository(QuickKartDbContext quickKartContext)
        {
            // To-Do: Implement your logic here

        }
        public List<Category> GetAllCategories()
        {
            return null;
        }
        public List<Product> GetProductsOnCategoryId(byte categoryId)
        {
            return null;
        }
        public Product FilterProducts(byte categoryId)
        {
            return null;
        }
        public List<Product> FilterProductsUsingLike(string pattern)
        {
            return null;
        }

        #region GetPurchaseDetailsToPrint

        /****** Uncomment the following method 
        After creating an entity of type 'PurchasePrintDetails' ******/
        //public List<PurchasePrintDetails> GetPurchaseDetailsToPrint(string productName, string emailId)
        //{

        //    return null;
        //}

        #endregion

        public bool AddCategory(string categoryName)
        {
            return false;
        }
        public bool AddProductsUsingAddRange(params Product[] products)
        {
            return false;
        }

        #region Optional AddProductsUsingAddRange(List<Products> products)
        public bool AddProductsUsingAddRange(List<Product> products)
        {
            return false;
        }

        #endregion
        public bool UpdateCategory(byte categoryId, string newCategoryName)
        {
            return false;
        }
        public int UpdateProduct(string productId, decimal price)
        {
            return 0;
        }
        public int UpdateProductsUsingUpdateRange(byte categoryId, int quantityProcured)
        {
            return 0;
        }
        public bool DeleteProduct(string productId)
        {
            return false;
        }
        public bool DeleteProductsUsingRemoveRange(string subString)
        {
            return false;
        }
        public int AddCategoryDetailsUsingUSP(string categoryName, out byte categoryId)
        {
            categoryId = 0;

            return 0;
        }

        #region GetProductsUsingTVF
        /****** Uncomment the following method 
        After creating an entity of type 'ProductCategoryName' ******/

        //public List<ProductCategoryName> GetProductsUsingTVF(byte categoryId)
        //{
        //    return null;
        //}

        #endregion
        public string GetNewProductId()
        {
            return null;
        }
    }
}
