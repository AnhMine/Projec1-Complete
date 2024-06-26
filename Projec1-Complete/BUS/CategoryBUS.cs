﻿using Projec1_Complete.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Projec1_Complete.DAL.CategoryDAL;

namespace Projec1_Complete.BUS
{
    public class CategoryBUS
    {
        private CategoryDAL _categoryDAL;
        public CategoryBUS()
        {
            _categoryDAL = new CategoryDAL();
        }
        public List<Category> GetCategories()
        {
            return _categoryDAL.GetCategories();
        }
        public List<ProductAndOrderInfo> GetProductByCateAndPersonId(int id, int id2, int orderid)
        {
            return _categoryDAL.GetProductByCateAndPersonId(id,id2,orderid);
        }
        public List<ProductAndOrderInfo> GetCateGory()
        {
            return _categoryDAL.GetCateGory();
        }


        public List<ProductAndOrderInfo> GetProductsWithOrderInfo(int personID)
        {
            return _categoryDAL.GetProductsWithOrderInfo(personID);
        }
        public List<ProductAndOrderInfo> SearchProductByText(string search, int personId, int orderid)
        {
            return _categoryDAL.SearchProductByText(search, personId, orderid);
        }



    }
}
