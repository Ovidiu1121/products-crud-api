using ProductsCrudApi.Products.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace tests.Helpers
{
    public class TestProductFactory
    {

        public static Product CreateProduct(int id)
        {
            return new Product
            {
                Id = id,
                Name ="Mihai"+id,
                Price=200+id,
                Stock=10+id,
                Producer="Emag"+id
            };

        }

        public static List<Product> CreateProducts(int count)
        {

            List<Product>products=new List<Product>();

            for(int i=0;i<count;i++)
            {
                products.Add(CreateProduct(i));
            }

            return products;
        }

        public static List<int> CreatePrices(int count)
        {

            List<int> prices=new List<int>();

            for(int i=0;i<count;i++)
            {
                prices.Add(i);
            }

            return prices;
        }

    }
}
