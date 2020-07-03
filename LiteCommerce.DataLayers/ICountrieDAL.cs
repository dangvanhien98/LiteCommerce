using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface ICountrieDAL
    {
        List<Countrie> List();

        int Add(Countrie data);

        bool Update(Countrie data, string country);

        int Delete(string[] Countries);

        int Count(string searchValue);

        Countrie Get(string Country);

        List<Countrie> List(int page, int pagesize, string searchValue);
    }
}
