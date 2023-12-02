using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projec1_Complete.DAL
{
    public class CategoryDAL
    {
        private ASMProject1Entities db;
        public CategoryDAL()
        {
            db = new ASMProject1Entities();
        }
        public List<Category> GetCategories()
        {
            return db.Categories.ToList();
        }
    }
}
