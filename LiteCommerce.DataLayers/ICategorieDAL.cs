using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICategorieDAL
    {
        /// <summary>
        /// hiển thị danh mục phân trang
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        List<Categorie> List(int page, int pagesize, string searchValue);

        /// <summary>
        /// đếm danh mục tìm kiếm được
        /// </summary>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        int Count(string searchValue);

        Categorie Get(int CategorieID);

        int Add(Categorie data);

        bool Update(Categorie data);

        int Delete(int[] categorieIDs);

    }
}
